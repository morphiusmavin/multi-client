#if 1
#include <unistd.h>
#include <sys/mman.h>
#include <fcntl.h>
#include <assert.h>
#include <time.h>
#include <sys/time.h>
#include <ctype.h>
#include <stdlib.h>
#include <stdio.h> 
#include <string.h>
#include <sched.h>
#include <sys/types.h>
#include <pthread.h>
#define closesocket close
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h>
#include <errno.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include <semaphore.h>
#include "../cmd_types.h"
#include "../mytypes.h"
#include "ioports.h"
#include "serial_io.h"
#include "queue/ollist_threads_rw.h"
#include "queue/cllist_threads_rw.h"
#include "tasks.h"
#include "cs_client/config_file.h"
#endif

int shutdown_all;

static UCHAR pre_preamble[] = {0xF8,0xF0,0xF0,0xF0,0xF0,0xF0,0xF0,0x00};

UCHAR msg_buf[SERIAL_BUFF_SIZE];
UCHAR msg_buf2[SERIAL_BUFF_SIZE];
CMD_STRUCT cmd_array[NO_CMDS];

pthread_mutex_t     tcp_write_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     tcp_read_lock=PTHREAD_MUTEX_INITIALIZER;

/*********************************************************************/
//#if 0
void print_cmd(UCHAR cmd)
{
	char tempx[30];
	
	if(cmd > NO_CMDS)
		printf("unknown cmd: %d\n",cmd);

	sprintf(tempx, "cmd: %d %s\0",cmd,cmd_array[cmd].cmd_str);
	printf("%s\r\n",cmd_array[cmd].cmd_str);
	
}
//#endif
/*********************************************************************/
int uSleep(time_t sec, long nanosec)
{
/* Setup timespec */
	struct timespec req;
	req.tv_sec = sec;
	req.tv_nsec = nanosec;

/* Loop until we've slept long enough */
	do
	{
/* Store remainder back on top of the original required time */
		if( 0 != nanosleep( &req, &req ) )
		{
/* If any error other than a signal interrupt occurs, return an error */
			if(errno != EINTR)
			{
				return -1;
			}
		}
		else
		{
/* nanosleep succeeded, so exit the loop */
			break;
		}
	} while ( req.tv_sec > 0 || req.tv_nsec > 0 );

	return 0;									  /* Return success */
}

/*********************************************************************/
// task to get commands from the sched cmd host
UCHAR recv_msg_task(int test)
{
	struct msgqbuf msg;
	int msgtype = 1;
	int dest;
	UCHAR cmd;
	int msg_len;

	//printf("starting recv_msg_task\n");

	while(TRUE)
	{
		uSleep(0,TIME_DELAY/16);
		if (msgrcv(sock_qid, (void *) &msg, sizeof(msg.mtext), msgtype,	MSG_NOERROR) == -1) 
		{
			if (errno != ENOMSG) 
			{
				perror("msgrcv");
				printf("msgrcv error\n");
				exit(EXIT_FAILURE);
			}
		}
		cmd = msg.mtext[0];							// first byte is cmd
		printf("cmd in recv msg task: ");
		print_cmd(cmd);
		dest = (int)msg.mtext[1];
		msg_len = (int)msg.mtext[2];				// 3rd is low byte of msg_len
		msg_len |= (int)(msg.mtext[3] << 4);		// 4th is high byte of msg_len
		msg_buf[0] = cmd;
		printf("msg_len: %d dest: %d\n",msg_len,dest);
		memcpy(msg_buf,msg.mtext+4,msg_len);
		msg_len = msg_len>255?255:msg_len;

		send_msg(msg_len, msg_buf, cmd, dest);

		if(cmd == SHUTDOWN_IOBOX || cmd == REBOOT_IOBOX || cmd == SHELL_AND_RENAME || cmd == EXIT_TO_SHELL)
		{
			printf("recv msg task shutdown 1\n");
			return 0;
		}

		if(shutdown_all == 1)
		{
			uSleep(0,TIME_DELAY/16);
			//printf("recv msg task shutdown 2\n");
			return 0;
		}
	}
	return test + 1;
}

/*********************************************************************/
// get msg from tcp and relay onto sched's host cmd loop using queue
UCHAR get_host_cmd_task1(int test)
{
	int rc = 0; 
	int rc1 = 0;
	UCHAR cmd = 0x21;
	char errmsg[50];
	char filename[15];
	char *fptr;
	size_t size;
	int i;
	int j;
	int k;
	UCHAR tempx[200];
	UCHAR write_serial_buff[SERIAL_BUFF_SIZE];
	char temp_time[5];
	char *pch;
	time_t curtime2;
	time_t *pcurtime2 = &curtime2;
	int fp;
	struct timeval mtv;
	struct tm t;
	struct tm *pt = &t;
	int msg_len;
	shutdown_all = 0;
	char version[15] = "sched v1.03\0";
	struct msgqbuf msg;
	int msgtype = 1;
	msg.mtype = msgtype;
	int temp;
	int dest;
	shutdown_all = 0;
/*
	this_client_index = -1;
	printf("this client index: %d\n",this_client_index);
	printf("this label: %s\n",client_table[this_client_index].label);

	memset(msg.mtext,0,sizeof(msg.mtext));
	msg.mtext[0] = UPDATE_CLIENT_INFO;
	msg_len = 1;
	msg.mtext[1] = (UCHAR)msg_len;
	msg.mtext[2] = (UCHAR)(msg_len >> 4);
	msg.mtext[3] = (UCHAR)this_client_index;
	memcpy(msg.mtext + 3,tempx,msg_len);

	if (msgsnd(sched_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
	{
		perror("msgsnd error");
		exit(EXIT_FAILURE);
	}
	uSleep(1,0);
*/
	uSleep(1,0);
	while(TRUE)
	{
		cmd = 0;
		memset(msg_buf,0,sizeof(msg_buf));
		//printf("wait for msg_len\n");
		msg_len = get_msg();
		//printf("msg_len: %d\n",msg_len);
/*
		for(i = 1;i < msg_len+1;i++)
			printf("%02x ",tempx[i]);
*/
		if(msg_len < 0)
		{
			printf("bad msg\r\n");
			cmd = BAD_MSG;
			usleep(10000);
		}else
		{
			rc = recv_tcp(&msg_buf[0],msg_len+1,1);
			//printf("rc: %d\n",rc);
/*
			rc = cllist_find_data(msg_buf[0],ctpp,&cll);
			printf("%d %d %d %d %s\n",ctp->index,ctp->client_no,ctp->cmd, ctp-> dest, ctp->label);
			printf("this: %s %s\n",client_table[ctp->dest].ip, client_table[ctp->dest].label);
			printf("dest: %s %s\n",client_table[ctp->client_no].ip, client_table[ctp->client_no].label);
			cmd = ctp->cmd;
*/
			cmd = msg_buf[0];
			print_cmd(cmd);
			memset(tempx,0,sizeof(tempx));
			memcpy(tempx,msg_buf+1,msg_len);

			memset(msg.mtext,0,sizeof(msg.mtext));
			msg.mtext[0] = cmd;
			msg.mtext[1] = (UCHAR)msg_len;
			msg.mtext[2] = (UCHAR)(msg_len >> 4);
			memcpy(msg.mtext + 3,tempx,msg_len);

			if (msgsnd(sched_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
			{
				perror("msgsnd error");
				exit(EXIT_FAILURE);
			}
			if(cmd == SHUTDOWN_IOBOX || cmd == REBOOT_IOBOX || cmd == SHELL_AND_RENAME || cmd == EXIT_TO_SHELL)
			{
/*
				uSleep(0,TIME_DELAY/16);
				close_tcp();
				//uSleep(0);
*/
				shutdown_all = 1;
				return 0;
			}

		}
		uSleep(0,TIME_DELAY/16);
		if(shutdown_all == 1)
		{
			uSleep(0,TIME_DELAY/16);
			printf("cmd_host shutdown\n");
			return 0;
		}
	}
	return test + 1;
}

// client calls 'connect' to get accept call below to stop
// blocking and return sd2 socket descriptor

static struct  sockaddr_in sad;				  /* structure to hold server's address  */
static int sock_open;
static struct timeval tv;
/*********************************************************************/
int tcp_connect(void)
{
	static struct  sockaddr_in sad;  /* structure to hold server's address  */
	struct  hostent  *ptrh;   /* pointer to a host table entry       */
	struct  protoent *ptrp;   /* point to a protocol table entry     */
	static struct timeval tv;
	int port;
	char host[20] = "192.168.88.146";	// TS-SERVER I'm currently using 

	memset((char *)&sad,0,sizeof(sad));  /* clear sockaddr structure */
	sad.sin_family = AF_INET;            /* set family to Internet   */
	sad.sin_addr.s_addr = INADDR_ANY;
	port = PROTOPORT;

	//printf("trying to connect...\n");

	if (port > 0) sad.sin_port = htons((u_short)port);
	else
	{
		printf("bad port number %d\n", port);
	}
	ptrh = gethostbyname(host);
	if( ((char *)ptrh) == NULL)
	{
		printf("invalid host:  %s\n", host);
	}

	memcpy(&sad.sin_addr, ptrh->h_addr, ptrh->h_length);

	 /* Map TCP transport protocol name to protocol number */
	//getprotobyname doesn't work on TS-7200 because there's no /etc/protocols file
/*
	if ( ((int)(ptrp = getprotobyname("tcp"))) == 0)
	{
		printf("cannot map \"tcp\" to protocol number");
	}
*/
	global_socket = socket (PF_INET, SOCK_STREAM, 6);

	if (global_socket < 0)
	{
		printf("socket creation failed\n");
	}

	//printf("host = %s global_socket = %d\n",host,global_socket);
	//printf("trying to connect...\n");

	if (connect(global_socket, (struct sockaddr *)&sad, sizeof(sad)) < 0)
	{
		printf("connect failed\n");
	}
	else
	{
		//printf("connected\n");

//#ifndef MAKE_TARGET
		if (setsockopt (global_socket, SOL_SOCKET, SO_RCVTIMEO, (char *)&tv, sizeof(struct timeval)) < 0)
			return -2;
		if (setsockopt (global_socket, SOL_SOCKET, SO_SNDTIMEO, (char *)&tv, sizeof(struct timeval)) < 0)
			return -3;
//#endif
		set_sock(global_socket);
		return global_socket;
	}
}

/*********************************************************************/
// task to monitor for a client requesting a connection
UCHAR tcp_monitor_task(int test)
{
	int s;
	s = pthread_setcancelstate(PTHREAD_CANCEL_ENABLE,NULL);
	int i;
	assign_client_table();
/*
	for(i = 0;i < MAX_CLIENTS;i++)
	{
		printf("%s %d\n",client_table[i].label,client_table[i].socket);
*/
	//printf("starting tcp_monitor...\n");

	while (TRUE)
	{
		if(shutdown_all)
		{
			//close_tcp();
			//printf("done tcp_mon\r\n");
			return 0;
		}
		else
		{
			if(test_sock() <= 0)
			{
				if(tcp_connect() < 0)
				{
					printf("can't connect\n");
				}
			}
			uSleep(0,TIME_DELAY/16);
		}
		uSleep(0,TIME_DELAY/2);
	}
	return 1;
}

/*********************************************************************/
int test_sock(void)
{
	return sock_open;
}

void set_sock(int open)
{
	sock_open = open;
}
/*********************************************************************/
void close_tcp(void)
{
	if(sock_open)
	{
		printf("closing socket...\r\n");
		close(global_socket);
		printf("socket closed!\r\n");
		global_socket = -1;
		set_sock(-1);
	}else
	{
		//printf("socket already closed\r\n");
	}
}

/*********************************************************************/
// get preamble & msg len from client
// preamble is: {0xF8,0xF0,0xF0,0xF0,0xF0,0xF0,0xF0,0x00,
// msg_len(lowbyte),msg_len(highbyte),0x00,0x00,0x00,0x00,0x00,0x00}
// returns message length
int get_msg(void)
{
	int len;
	UCHAR low, high;
	int ret;
	int i;

	UCHAR preamble[10];
	ret = recv_tcp(preamble,8,1);
	//printf("ret: %d\n",ret);
	if(ret < 0)
	{
		printf("ret < 0");
	}
	if(memcmp(preamble,pre_preamble,8) != 0)
	{
		printf("bad preamble\n");
		uSleep(1,0);
		return -1;
	}
	ret = recv_tcp(&low,1,1);
	ret = recv_tcp(&high,1,1);
//	printf("%02x %02x\n",low,high);
	len = 0;
	len = (int)(high);
	len <<= 4;
	len |= (int)low;

	return len;
}

/*********************************************************************/
/*********************************************************************/
// send the preamble, msg len, msg_type & dest (dest is index into client table)
void send_msg(int msg_len, UCHAR *msg, UCHAR msg_type, UCHAR dest)
{
	int ret;
	int i;
	UCHAR temp[2];

	if(dest > MAX_CLIENTS)
		return;

	if(test_sock())
	{
		ret = send_tcp(&pre_preamble[0],8);
		temp[0] = (UCHAR)(msg_len & 0x0F);
		temp[1] = (UCHAR)((msg_len & 0xF0) >> 4);
		//printf("%02x %02x\n",temp[0],temp[1]);
		send_tcp((UCHAR *)&temp[0],1);
		send_tcp((UCHAR *)&temp[1],1);
		send_tcp((UCHAR *)&msg_type,1);
		send_tcp((UCHAR *)&dest,1);

		for(i = 0;i < msg_len;i++)
		{
			send_tcp((UCHAR *)&msg[i],1);
//			send_tcp((UCHAR *)&ret,1);
		}
		//printf("%d ",msg_len);
	}
}
/*********************************************************************/
int recv_tcp(UCHAR *str, int strlen,int block)
{
	int ret = 0;
	char errmsg[20];
	memset(errmsg,0,20);
	if(test_sock())
	{
//		pthread_mutex_lock( &tcp_read_lock);
		ret = get_sock(str,strlen,block,&errmsg[0]);
		//printf("ret: %d\n",ret);
//		pthread_mutex_unlock(&tcp_read_lock);
		if(ret < 0 && (strcmp(errmsg,"Success") != 0))
		{
			printf(errmsg);
		}
	}
	else
	{
		strcpy(errmsg,"sock closed");
		printf(errmsg);
		ret = -1;
	}
	return ret;
}
/*********************************************************************/
int send_tcp(UCHAR *str,int len)
{
	int ret = 0;
	char errmsg[30];
	memset(errmsg,0,30);
	pthread_mutex_lock( &tcp_write_lock);
	ret = put_sock(str,len,1,&errmsg[0]);
	pthread_mutex_unlock(&tcp_write_lock);
	if(ret < 0 && (strcmp(errmsg,"Success") != 0))
	{
		if(same_msg == 0)
			printf(errmsg);
		same_msg = 1;
	}
	else same_msg = 0;
	return ret;
}
/*********************************************************************/
int put_sock(UCHAR *buf,int buflen, int block, char *errmsg)
{
	int rc = 0;
	char extra_msg[10];
	if(test_sock())
	{
		if(block)
// block
			rc = send(global_socket,buf,buflen,MSG_WAITALL);
		else
// don't block
			rc = send(global_socket,buf,buflen,MSG_DONTWAIT);
		if(rc < 0 && errno != 11)
		{
			strcpy(errmsg,strerror(errno));
			sprintf(extra_msg," %d",errno);
			strcat(errmsg,extra_msg);
			strcat(errmsg," put_sock");
			close_tcp();
			printf("closing tcp socket\n");
		}else strcpy(errmsg,"Success\0");
	}
	else
	{
// this keeps printing out until the client logs on
		strcpy(errmsg,"sock closed");
		rc = -1;
	}
	return rc;
}
/*********************************************************************/
int get_sock(UCHAR *buf, int buflen, int block, char *errmsg)
{
	int rc;
	char extra_msg[10];
	if(block)
		rc = recv(global_socket,buf,buflen,MSG_WAITALL);
	else
		rc = recv(global_socket,buf,buflen,MSG_DONTWAIT);
	//printf("rc: %d\n",rc);
	if(rc < 0 && errno != 11)
	{
		strcpy(errmsg,strerror(errno));
		sprintf(extra_msg," %d",errno);
		strcat(errmsg,extra_msg);
		strcat(errmsg," get_sock");
	}else strcpy(errmsg,"Success\0");
	return rc;
}
/*********************************************************************/
