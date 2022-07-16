#if 1
#ifdef SERVER
#warning "server defined"
#endif
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
//#include "cs_client/config_file.h"

#define handle_error_en(en, msg) \
	   do { errno = en; perror(msg); exit(EXIT_FAILURE); } while (0)

#define handle_error(msg) \
	   do { perror(msg); exit(EXIT_FAILURE); } while (0)

static UCHAR pre_preamble[] = {0xF8,0xF0,0xF0,0xF0,0xF0,0xF0,0xF0,0x00};

pthread_cond_t       threads_ready;
pthread_mutex_t     tcp_write_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     tcp_read_lock=PTHREAD_MUTEX_INITIALIZER;

UCHAR (*fptr[NUM_SOCK_TASKS])(int) = 
{ 
	WinClReadTask, 
	WinClReadTask, 
	ReadTask, 
	ReadTask, 
	ReadTask,
	ReadTask,
	ReadTask,
	ReadTask,
	get_host_cmd_task, 
	tcp_monitor_task
};

int threads_ready_count=0;
pthread_cond_t    threads_ready=PTHREAD_COND_INITIALIZER;
pthread_mutex_t   threads_ready_lock=PTHREAD_MUTEX_INITIALIZER;
static UCHAR check_inputs(int index, int test);
extern CMD_STRUCT cmd_array[];
int shutdown_all;

CLIENT_TABLE client_table[MAX_CLIENTS];

#define ON 1
#define OFF 0

/****************************************************************************************************/
void print_cmd(UCHAR cmd)
{
	char tempx[30];

	if(cmd > NO_CMDS)
		printf("unknown cmd: %d\n",cmd);
	
	sprintf(tempx, "cmd: %d %s\0",cmd,cmd_array[cmd].cmd_str);
	printf("%s\r\n",cmd_array[cmd].cmd_str);
}


/****************************************************************************************************/
static double curtime(void)
{
	struct timeval tv;
	if(gettimeofday (&tv, NULL) == 0)
		return tv.tv_sec + tv.tv_usec / 1000000.0;
	else return 0.0;
}
/*
 clock_t start = clock();
   //... do work here
   clock_t end = clock();
   double time_elapsed_in_seconds = (end - start)/(double)CLOCKS_PER_SEC;
   return 0;
*/
/****************************************************************************************************/

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
				printf("uSleep error\0");
//             return -1;
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

#endif
/*********************************************************************/
// task to get commands from the host
UCHAR get_host_cmd_task(int test)
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

	//printf("sock_mgnt starting %d\n",test);

	while(TRUE)
	{
		cmd = 0;
		// recv msg's from sched
		if (msgrcv(sock_qid, (void *) &msg, sizeof(msg.mtext), msgtype,
//		MSG_NOERROR | IPC_NOWAIT) == -1) 
		MSG_NOERROR) == -1) 
		{
			if (errno != ENOMSG) 
			{
				perror("msgrcv");
				printf("msgrcv error\n");
				exit(EXIT_FAILURE);
			}
		}
		memset(write_serial_buff,0,sizeof(write_serial_buff));
		cmd = msg.mtext[0];							// first byte is cmd
		msg_len |= (int)(msg.mtext[2] << 4);		// 
		msg_len = (int)msg.mtext[1];				// 
		write_serial_buff[0] = cmd;
		//printf("msg_len: %d\n",msg_len);
		memcpy(write_serial_buff,msg.mtext+3,msg_len);
/*
		for(i = 1;i < msg_len+1;i++)
			printf("%02x ",write_serial_buff[i]);
*/
//		if(cmd > 0)
		if(1)
		{
			rc = 0;
			//print_cmd(cmd);
			switch(cmd)
			{
				case SET_TIME:
					printf("set time\n");
					break;
					
				case SEND_CLIENT_LIST:
					//printf("SEND_CLIENT_LIST from sock_mgt\n");
					for(i = 0;i < MAX_CLIENTS;i++)
					{
						//printf("...%d %s %d\n", i, client_table[i].ip, client_table[i].socket);
						if(client_table[i].socket > 0 && client_table[i].type != WINDOWS_CLIENT)
						{
							memset(tempx,0,sizeof(tempx));
							sprintf(tempx,"%d %s %d", i, client_table[i].ip, client_table[i].socket);
							//printf("%s\n",tempx);

							for(j = 0;j < MAX_CLIENTS;j++)
							{
								if(client_table[j].type == WINDOWS_CLIENT && client_table[j].socket > 0)
								{
									send_msgb(client_table[j].socket, strlen(tempx)*2,tempx,SEND_CLIENT_LIST);
									uSleep(0,TIME_DELAY/20);
									printf("%d\n",client_table[j].socket);
								}
							}

						}
					}
					break;
				
				case UPTIME_MSG:	// sent from client
					//printf("uptime msg (sock): %s\n",write_serial_buff);
					//printf("%ld %ld\n",ttrunning_minutes, ttrunning_seconds);
					//printf("%d %d\n",client_table[0].socket, client_table[1].socket);
					if(client_table[0].socket > 0);
						send_msgb(client_table[0].socket, strlen(write_serial_buff)*2,(UCHAR *)write_serial_buff,UPTIME_MSG);
					if(client_table[1].socket > 0);
						send_msgb(client_table[1].socket, strlen(write_serial_buff)*2,(UCHAR *)write_serial_buff,UPTIME_MSG);
//					printf("uptime: %s\n",write_serial_buff);
					break;

				case SEND_TIMEUP:
					msg.mtype = msgtype;
					memset(msg.mtext,0,sizeof(msg.mtext));
/*
					msg.mtext[0] = cmd;
					msg.mtext[1] = (UCHAR)msg_len;
					msg.mtext[2] = (UCHAR)(msg_len >> 4);
*/
					memcpy(msg.mtext,write_serial_buff,msg_len);
//					uSleep(1,0);

					if (msgsnd(sched_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
					{
						// keep getting "Invalid Argument" - cause I didn't set the mtype
						perror("msgsnd error");
						exit(EXIT_FAILURE);
					}
					break;

				case SEND_MESSAGE:
					for(i = 0;i < msg_len;i++)
						printf("%c",tempx[i]);
					printf("\n");

					msg.mtype = msgtype;
					memset(msg.mtext,0,sizeof(msg.mtext));
/*
					msg.mtext[0] = cmd;
					msg.mtext[1] = (UCHAR)msg_len;
					msg.mtext[2] = (UCHAR)(msg_len >> 4);
*/
					memcpy(msg.mtext,write_serial_buff,msg_len);
//					uSleep(1,0);

					if (msgsnd(sched_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
					{
						// keep getting "Invalid Argument" - cause I didn't set the mtype
						perror("msgsnd error");
						exit(EXIT_FAILURE);
					}
					break;

				case SEND_STATUS:
					//printf("sock_mgt send status\n");
					temp = 0;
					temp = (int)(write_serial_buff[3] << 4);
					temp |= (int)write_serial_buff[2];
					printf("temp: %d\n",temp);
					break;

				case BAD_MSG:
//						shutdown_all = 1;
					break;

				case GET_TEMP4:
					if(client_table[dest].socket > 0)
					{
						//printf("dest: %d sock: %d msg_len: %d\n",dest,client_table[dest].socket,msg_len);
						send_msg(client_table[dest].socket, msg_len, (UCHAR*)&write_serial_buff[0],cmd);
					}
					break;

				case GET_VERSION:
					//send_status_msg(version);
					break;
					
				case SHUTDOWN_IOBOX:
				case REBOOT_IOBOX:
				case SHELL_AND_RENAME:
				case EXIT_TO_SHELL:
					shutdown_all = 1;
					return 0;
					break;

			}								  // end of switch
		}									  // if rc > 0
		uSleep(0,TIME_DELAY/16);
		if(shutdown_all == 1)
		{
			uSleep(0,TIME_DELAY/16);
			//printf("cmd_host shutdown\n");
			return 0;
		}
	}
	return test + 1;
}

/*********************************************************************/
UCHAR WinClReadTask(int test)
{
	//printf("winclread: %d\n",test);
	int index = lookup_taskid(test);
	//printf("wclread: %d %d\n",test, index);

	int i,j,k,rc,msg_len;
	char tempx[105];
	char msg_buf[105];
	UCHAR cmd;
	int win_client_to_client_sock = -1;
	struct msgqbuf msg;
	int msgtype = 1;
	msg.mtype = msgtype;
	msg_len = -1;
	k = 0;
//	printf("win cl read task\n");
//	return 0;

	while(TRUE)
	{
startover:
		if(client_table[index].socket > 0)
		{
//			either one of these will work 
//			printf("msg from windows client %d\n",client_table[i].socket);
//			printf("msg from windows client %d\n",windows_client_sock);
// in the bitstream of a,b,c,d,e...
// a = command, c = index into client table 
// d is the start of the message 
			msg_len = get_msgb(client_table[index].socket);

			int rc = recv_tcp(client_table[index].socket, &msg_buf[0], msg_len, 1);
			cmd = msg_buf[0];
			//print_cmd(cmd);

			win_client_to_client_sock = msg_buf[2];		// offset into client table
			//printf("win_client_to_client_sock: %d\n",win_client_to_client_sock);
/*
			for(i = 2;i < rc;i+=2)
				printf("%02x ",msg_buf[i]);
			printf("\n");
*/
			memset(tempx,0,sizeof(tempx));
			k = 0;
			for(j = 4;j < msg_len+4;j+=2)
				tempx[k++] = msg_buf[j];
			msg_len /= 2;
			msg_len -= 3;
//			printf("msg_len from win client: %d\n",msg_len);
/*
			for(j = 0;j < msg_len;j++)
				printf("%02x ",tempx[j]);
			printf("\n");

			for(j = 0;j < msg_len;j++)
				printf("%c",tempx[j]);
			printf("\n");
*/
			if(cmd == DISCONNECT)
			{
				close(client_table[index].socket);
				client_table[index].socket = -1;
				printf("disconnected...\n");
				goto startover;
				// need a cmd that quits the server
			}
			// if this is for the server then tempx[0] will be _SERVER (from CLIENT_LIST enum)
			// so send a queue msg to get_host_cmd_task
			msg.mtype = msgtype;
			memset(msg.mtext,0,sizeof(msg.mtext));
			msg.mtext[0] = cmd;
			msg.mtext[1] = (UCHAR)msg_len;
			msg.mtext[2] = (UCHAR)(msg_len >> 4);
			memcpy(msg.mtext + 3,tempx,msg_len);
			// send msg's to sched 

			if(win_client_to_client_sock == _SERVER)
			{
				//printf("msg to cmd_host on server: %s %d\n",msg.mtext + 3,cmd);
				//print_cmd(cmd);
				if (msgsnd(sched_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
				{
					// keep getting "Invalid Argument" - cause I didn't set the mtype
					perror("msgsnd error");
					exit(EXIT_FAILURE);
				}

				if(cmd == SHUTDOWN_IOBOX || cmd == REBOOT_IOBOX || cmd == SHELL_AND_RENAME || cmd == EXIT_TO_SHELL)
				{
					//printf("shutdown or reboot %d\n",index);
					for(i = 0;i < MAX_CLIENTS;i++)
					{
						close(client_table[i].socket);
						client_table[i].socket = -1;
						// the break statement only goes back up to 
						// "read task 2"
						uSleep(0,TIME_DELAY/4);
					}
					shutdown_all = 1;
					return 0;
				}

			}else
			{
				// get the socket of the client to send msg to 
				// the windows client sends as the 1st byte the index into 
				// the client_table[] array 
				uSleep(0,TIME_DELAY/16);

				//printf("msg to client: sock: %d %s %d\n",client_table[win_client_to_client_sock].socket, 
					//client_table[win_client_to_client_sock].label, client_table[win_client_to_client_sock].qid);
				//print_cmd(cmd);	
/*
				printf("msg.mtext: ");
				for(i = 0;i < msg_len+4;i++)
					printf("%02x ",msg.mtext[i]);
				printf("\n");
*/
				// this sends a msg to the appropriate client's SendTask
				if(client_table[win_client_to_client_sock].socket > 0)
				{
					send_msg(client_table[win_client_to_client_sock].socket, strlen(tempx), (UCHAR*)tempx,cmd);
				}else printf("bad socket\n");
				//printf("sent: %s\n", msg.mtext);				
				//printf("\n");
			}
		}
		//printf("*");
		uSleep(0,TIME_DELAY/16);

		if(shutdown_all)
		{
			uSleep(0,TIME_DELAY/16);
			//printf("\nshutting down WinClReadTask\n");
			return 0;
		}
	}
}
/*********************************************************************/
int lookup_taskid(int index)
{
	int i;
	//printf("lookup_taskid %d\n",index);
	for(i = 0;i < MAX_CLIENTS;i++)
	{
		//printf("task_id: %d\n",client_table[i].task_id);
		if(client_table[i].task_id >= 0 && client_table[i].task_id == index)
		{
			//printf("found %d %s\n",i,client_table[i].label);
			return index;
		}
	}
	return -1;
}
/*********************************************************************/
// read from the client - each copy of this thread, depending on 'index'
// represents one of the logged in clients 
// the format for the recv'd msg is:
// preamble, msg_len, msg_type (cmd) & dest (the intended recipient
// whether it be the windows client, the server or another client 
UCHAR ReadTask(int test)
{
	//printf("readtask: %d\n",test);
	int index = lookup_taskid(test);
	//printf("readtask: %d %d\n",test, index);

	char tempx[SERIAL_BUFF_SIZE];
	int msg_len;
	int ret;
	UCHAR cmd;
	UCHAR dest;
	int i;
	int temp;
	struct msgqbuf msg;
	int msgtype = 1;
	msg.mtype = msgtype;
//	uSleep(1,0);
//	printf("readtask: %s\n",client_table[index].label);
//	return 0;

	while(TRUE)
	{
startover1:
		if(client_table[index].socket > 0)
		{
			//printf("read task %d\n",index);
			msg_len = get_msg(client_table[index].socket);
			ret = recv_tcp(client_table[index].socket, &tempx[0],msg_len+2,1);
			//printf("ret: %d msg_len: %d\n",ret,msg_len);
			cmd = tempx[0];
			dest = tempx[1];
			//printf("dest: %d\n",dest);
/*
			for(i = 0;i < msg_len+2;i++)
				printf("%02x ",tempx[i]);
			printf("\n");

			for(i = 0;i < msg_len+2;i++)
				printf("%c",tempx[i]);
			printf("\n");
*/
			//printf("cmd: %d\n",cmd);
			//printf("read task: %d\n",index);
			//print_cmd(cmd);
			memmove(tempx,tempx+2,msg_len);
/*
			for(i = 0;i < msg_len;i++)
				printf("%02x ",tempx[i]);
			printf("\n");
*/
			if(cmd == SHUTDOWN_IOBOX || cmd == REBOOT_IOBOX || cmd == SHELL_AND_RENAME || cmd == EXIT_TO_SHELL)
			{
				//printf("shutdown or reboot %d\n",index);
				close(client_table[index].socket);
				client_table[index].socket = -1;
				// the break statement only goes back up to 
				// "read task 2"
				// tell the sched which client was shutdown 
				// (deleted)
				goto startover1;
//				break;
			}

			if(cmd == CLIENT_RECONNECT)
			{
				printf("client reconnecting...\n");
				close(client_table[index].socket);
				client_table[index].socket = -1;
				// the break statement only goes back up to 
				// "read task 2"
				goto startover1;
			}
			//printf("%02x %02x %02x\n",tempx[0],tempx[1],tempx[2]);
/*
			for(i = 0;i < ret;i++)
			{
				printf("%02x ",tempx[i]);
			}
*/
			if(dest == _SERVER)		// from one of the clients to the server
			{
				//printf("dest: server\n");
				memset(msg.mtext,0,sizeof(msg.mtext));
				msg.mtext[0] = cmd;
				msg.mtext[1] = (UCHAR)msg_len;
				msg.mtext[2] = (UCHAR)(msg_len >> 4);
				memcpy(msg.mtext + 3,tempx,msg_len);
				//printf("msg to cmd_host from client %d\n",dest);
/*
				for(i = 0;i < msg_len+3;i++)
					printf("%02x ",msg.mtext[i]);
				printf("\n");
*/
				if (msgsnd(sched_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
				{
					perror("msgsnd error");
					exit(EXIT_FAILURE);
				}
			}else if(dest < MAX_CLIENTS)		// from one of the clients to another client 
			{
				if(client_table[dest].socket > 0)
					send_msg(client_table[dest].socket, strlen(tempx), (UCHAR*)tempx,cmd);
			}else printf("no destination\n");
		}

		if(shutdown_all)
		{
			//printf("leaving read task\n");
			uSleep(0,TIME_DELAY/16);
			return 0;
		}
		uSleep(0,TIME_DELAY/16);
	}
}
/*********************************************************************/
// serial receive task

/*********************************************************************/
// match up the ip address (146,145, etc) to the entry in the table 
// and return the socket id if it's logged on, else return -1
int get_client_sock(char *recip)
{
	int i;
	int sock = -1;

	for(i = 0;i < MAX_CLIENTS;i++)
	{
		if(strncmp(client_table[i].ip,recip,3) == 0)
		{
			//printf("%s %d\n",client_table[i].label,(sock = client_table[i].socket));
			sock = client_table[i].socket;
			return sock;
		}
	}
	return sock;
}
/*********************************************************************/
int get_client_index(char *recip)
{
	int i;
	int index = -1;

	for(i = 0;i < MAX_CLIENTS;i++)
	{
		if(strncmp(client_table[i].ip,recip,3) == 0)
		{
			//printf("%s %d\n",client_table[i].label,(sock = client_table[i].socket));
			index = i;
			return index;
		}
	}
	return index;
}
/*********************************************************************/
char *get_client_recip(int sock)
{
	int i;

	for(i = 0;i < MAX_CLIENTS;i++)
	{
		if(client_table[i].socket == sock)
		{
			return client_table[i].ip;
		}
	}
	return (char *)0;
}

/*********************************************************************/
// task to monitor for a client requesting a connection
UCHAR tcp_monitor_task(int test)
{
	int ret = -1;
	struct timeval tv;
	int client_socket[MAX_CLIENTS];
	int master_socket, new_socket, activity, sd, addrlen;
	struct  hostent   *ptrh;				  /* pointer to a host table entry */
	struct  protoent  *ptrp;				  /* pointer to a protocol table entry */
	struct  sockaddr_in address;			  /* structure to hold server's address */
//	struct  sockaddr_in cad;				  /* structure to hold client's address */
	int     port;							  /* protocol port number */
//	int     alen;							  /* length of address */
	UCHAR cmd;
	int max_sd;
	port = PROTOPORT;
	tv.tv_sec = 2;
	tv.tv_usec = 50000;
	int s,i,j,k;
	fd_set readfds;
	int opt = TRUE;
	char tempx[100];
	UCHAR msg_buf[20];
	int valread;
	int msg_len;
	char address_string[20];
	char ch;
	char recip[4];
	int to_sock;
	struct msgqbuf msg;
	int msgtype = 1;
	msg.mtype = msgtype;
	int winclipaddr = -1;
	
// 156 & 157 are the next 2 avail - 155 is the firstpi.local

	s = pthread_setcancelstate(PTHREAD_CANCEL_ENABLE,NULL);
//	if(s != 0)
//		handle_err_en(s, "pthread_setcancelstate");
//		printf("setcancelstate\r\n");
	printf("starting tcp_monitor_task %d\n",test);
	memset((char  *)&address,0,sizeof(address));	  /* clear sockaddr structure   */
	address.sin_family = AF_INET;				  /* set family to Internet     */
	address.sin_addr.s_addr = INADDR_ANY;		  /* set the local IP address */

	address.sin_port = htons((u_short)port);
	
	for (i = 0; i < MAX_CLIENTS; i++)
	{
		client_socket[i] = 0;
	}

// getprotobyname doesn't work on TS-7200 because there's no /etc/protocols file
// so just use '6'
// but on TS-7800 there is a /etc/protocols file 
/*
	if ( ((int)(ptrp = getprotobyname("tcp"))) == 0)
	{
		//printString2("cannot map tcp to protocol number");
		printf("cannot map tcp to protocol number\r\n");
		exit(EXIT_FAILURE);
	}
	master_socket = socket (PF_INET, SOCK_STREAM, ptrp->p_proto);
*/
// getprotobyname doesn't work on TS-7200 because there's no /etc/protocols file
// so just use '6' as the tcp protocol number

//	master_socket = socket (PF_INET, SOCK_STREAM, 6);
	if((master_socket = socket(PF_INET, SOCK_STREAM, 6)) == 0)
//	if((master_socket = socket(AF_INET, SOCK_STREAM, 0)) == 0)
	{
		perror("socket failed");
		exit(EXIT_FAILURE);
	}
//#endif

	//printf("master socket: %d\n",master_socket);
	//set master socket to allow multiple connections ,
	//this is just a good habit, it will work without this
	if( setsockopt(master_socket, SOL_SOCKET, SO_REUSEADDR, (char *)&opt,
		sizeof(opt)) < 0 )
	{
		perror("setsockopt");
		exit(EXIT_FAILURE);
	}

/* Bind a local address to the socket */
	if (bind(master_socket, (struct sockaddr *)&address, sizeof (address)) < 0)
	{
		//printString2("bind failed");
		printf("bind failed\r\n");
//		exit(1);
	}
//printf("bind ok\n");
/* Specify a size of request queue */
	if (listen(master_socket, QLEN) < 0)
	{
		//printString2("listen failed");
		printf("listen failed\r\n");
//			exit(1);
	}
//printf("done w/ listen\n");
//	alen = sizeof(cad);
	addrlen = sizeof(address);

/* Main server loop - accept and handle requests */
	k = 0;
	while (TRUE)
	{
//		printf("test %d ",k++);
		//clear the socket set
		FD_ZERO(&readfds);
	
		//add master socket to set
		FD_SET(master_socket, &readfds);
		max_sd = master_socket;
		client_table[_SERVER].socket = max_sd;

		//add child sockets to set
		for ( i = 0 ; i < MAX_CLIENTS ; i++)
		{
			//socket descriptor
//			sd = client_socket[i];
			sd = client_table[i].socket;
/*
			if(sd > 0)
			{
				printf("sd: %d %s\n",sd,client_table[i].ip);
			}
*/
			//if valid socket descriptor then add to read list
			if(sd > 0)
				FD_SET( sd , &readfds);
				
			//highest file descriptor number, need it for the select function
			if(sd > max_sd)
				max_sd = sd;
		}
	
		//wait for an activity on one of the sockets , timeout is NULL ,
		//so wait indefinitely
//		printf("wait for activity\n");
		activity = select( max_sd + 1 , &readfds , NULL , NULL , NULL);
	
		if ((activity < 0) && (errno!=EINTR))
		{
			printf("select error");
		}
			
		//If something happened on the master socket ,
		//then its an incoming connection
		if (FD_ISSET(master_socket, &readfds))
		{
			if ((new_socket = accept(master_socket,
					(struct sockaddr *)&address, (socklen_t*)&addrlen))<0)
			{
				perror("accept");
				exit(EXIT_FAILURE);
			}
			memset(address_string, 0, sizeof(address_string));
			strncpy(address_string,inet_ntoa(address.sin_addr),sizeof(address_string));
			//inform user of socket number - used in send and receive commands
			//printf("New connection , socket: %d ip: %s port: %d\n",
					//new_socket, address_string, ntohs(address.sin_port));

			i = j = 0;
			//printf("%s\n",address_string);

			while(i < 3 && j < 17 && address_string[j] != 0)
			{
				if(address_string[j] == '.')
					i++;
				//printf("%c",address_string[j]);
				j++;
			}
			memset(tempx,0,sizeof(tempx));
			strncpy(tempx,&address_string[j],3);
			//printf("tempx: %s\n",tempx);

			if(winclipaddr < 0)
			winclipaddr = get_client_index(tempx);

			// later on we want to have more than 1 windows client be able
			// to log in
			for(i = 0;i < MAX_CLIENTS;i++)
			{
				if(strncmp(client_table[i].ip,tempx,3) == 0)
				{
					client_table[i].socket = new_socket;
					printf("index: %d type: %d label: %s socket: %d\n",i, client_table[i].type, client_table[i].label,client_table[i].socket);
					memset(tempx,0,sizeof(tempx));
					sprintf(tempx,"%d %s %d", i, client_table[i].ip, client_table[i].socket);
					printf("should be sending msg to win cl: %s\n",tempx);
					uSleep(0,TIME_DELAY/16);

					// send msg to 1st win client (149)
					if(client_table[0].socket > 0);
						send_msgb(client_table[0].socket, strlen(tempx)*2,tempx,SEND_CLIENT_LIST);
					if(client_table[1].socket > 0);
						send_msgb(client_table[1].socket, strlen(tempx)*2,tempx,SEND_CLIENT_LIST);

					if(client_table[i].qid == 0)
					{
						client_table[i].qid = msgget(client_table[i].qkey, IPC_CREAT | 0666);
						//printf("new connection qid: %d : %d\n",i,client_table[i].qid);
					}
					// tell the sched which clients have logged in/out
					// deleted
				}
			}
			if(shutdown_all)
			{
				uSleep(1,0);
				printf("shutting down tcp monitor\n");
				return 0;
			}
		}
		//else its some IO operation on some other socket
		for (i = 0; i < MAX_CLIENTS; i++)
		{
			sd = client_socket[i];

			if (FD_ISSET( sd , &readfds))
			{
				uSleep(0,TIME_DELAY/16);
				// this never happens when doing a SEND_MSG but when it does a SHUTDOWN 
				// it's because we were not closing the socket
				printf("*socket: %d ",sd);
				client_socket[i] = 0;
			}
		}
		uSleep(0,TIME_DELAY/8);
		if(shutdown_all)
		{
			uSleep(1,0);
			printf("shutting down tcp monitor\n");
			return 0;
		}
/*
		if (setsockopt (global_socket, SOL_SOCKET, SO_RCVTIMEO, (char *)&tv, sizeof(struct timeval)) < 0)
			return -2;
		if (setsockopt (global_socket, SOL_SOCKET, SO_SNDTIMEO, (char *)&tv, sizeof(struct timeval)) < 0)
			return -3;
*/
	}
	return 1;
}
/*********************************************************************/
/*
void close_tcp(void)
{
	if(1)
	{
//		printf("closing socket...\r\n");
		close(global_socket);
//		printf("socket closed!\r\n");
		global_socket = -1;
	}else
	{
		printf("socket already closed\0");
//		printf("socket already closed\r\n");
	}
}
*/
/**********************************************************************/
void *work_routine(void *arg)
{
	int *my_id=(int *)arg;
	int i;
	UCHAR pattern = 0;
	int not_done=1;
	i = not_done;
	shutdown_all = 0;

	pthread_mutex_lock(&threads_ready_lock);
	threads_ready_count++;
	if (threads_ready_count == NUM_SOCK_TASKS)
	{
/* I was the last thread to become ready.  Tell the rest. */
		pthread_cond_broadcast(&threads_ready);
	}
	else
	{
/* At least one thread isn't ready.  Wait. */
		while (threads_ready_count != NUM_SOCK_TASKS)
		{
			pthread_cond_wait(&threads_ready, &threads_ready_lock);
		}
	}
	pthread_mutex_unlock(&threads_ready_lock);

	while(not_done)
	{
		(*fptr[*my_id])(*my_id);
		i--;
		not_done--;
	}
	return(NULL);
}
/*********************************************************************/
/*********************************************************************/
// get preamble & msg len from client
// preamble is: {0xF8,0xF0,0xF0,0xF0,0xF0,0xF0,0xF0,0x00,
// msg_len(lowbyte),msg_len(highbyte),0x00,0x00,0x00,0x00,0x00,0x00}
// returns message length
int get_msg(int sd)
{
	int len;
	UCHAR low, high;
	int ret;
	int i;

	UCHAR preamble[10];
	ret = recv_tcp(sd, preamble,8,1);
	//printf("ret: %d\n",ret);
	if(ret < 0)
	{
//		printHexByte(ret);
		printf("%02x ",ret);
	}
	if(memcmp(preamble,pre_preamble,8) != 0)
	{
		printf("bad preamble\n");
		return -1;
	}
	ret = recv_tcp(sd, &low,1,1);
	ret = recv_tcp(sd, &high,1,1);
	//printf("%02x %02x\n",low,high);
	len = 0;
	len = (int)(high);
	len <<= 4;
	len |= (int)low;

	return len;
}
/*********************************************************************/
void send_msg(int sd, int msg_len, UCHAR *msg, UCHAR msg_type)
{
	int ret;
	int i;
	UCHAR temp[2];

	ret = send_tcp(sd, &pre_preamble[0],8);
	temp[0] = (UCHAR)(msg_len & 0x0F);
	temp[1] = (UCHAR)((msg_len & 0xF0) >> 4);
	//printf("%02x %02x\n",temp[0],temp[1]);
	send_tcp(sd, (UCHAR *)&temp[0],1);
	send_tcp(sd, (UCHAR *)&temp[1],1);
	send_tcp(sd, (UCHAR *)&msg_type,1);

	for(i = 0;i < msg_len;i++)
		send_tcp(sd, (UCHAR *)&msg[i],1);
}
/*********************************************************************/
// get/send_msgb is what the old server used to communicate with the
// windows client - it gets each relavent byte as 2 bytes from the 
// windows machine since that's how the tcp libraries that I'm using
// are done - took me forever to figure this out
int get_msgb(int sd)
{
	int len;
	UCHAR low, high;
	int ret;
	int i;

	UCHAR preamble[20];
	ret = recv_tcp(sd, preamble,16,1);
	if(ret < 0)
	{
//		printHexByte(ret);
		printf("%02x ",ret);
	}
	if(memcmp(preamble,pre_preamble,8) != 0)
		return -1;

	low = preamble[8];
	high = preamble[9];
	len = (int)(high);
	len <<= 8;
	len |= (int)low;

	return len;
}

/*********************************************************************/
void send_msgb(int sd, int msg_len, UCHAR *msg, UCHAR msg_type)
{
	int len;
	int ret;
	int i;

	ret = send_tcp(sd, &pre_preamble[0],8);
	msg_len++;
	send_tcp(sd, (UCHAR *)&msg_len,1);
	ret = 0;
	send_tcp(sd, (UCHAR *)&ret,1);

	for(i = 0;i < 6;i++)
		send_tcp(sd, (UCHAR *)&ret,1);

	send_tcp(sd, (UCHAR *)&msg_type,1);

	ret = 0;
	send_tcp(sd, (UCHAR *)&ret,1);

	for(i = 0;i < msg_len;i++)
	{
		send_tcp(sd, (UCHAR *)&msg[i],1);
		send_tcp(sd, (UCHAR *)&ret,1);
	}
}

/*********************************************************************/
int recv_tcp(int sd, UCHAR *str, int strlen,int block)
{
	int ret = -1;
	char errmsg[20];
	memset(errmsg,0,20);
//		printf("start get_sock\n");
//		pthread_mutex_lock( &tcp_read_lock);
	ret = get_sock(sd, str,strlen,block,&errmsg[0]);
//		pthread_mutex_unlock(&tcp_read_lock);
//		printf("end get_sock\n");
//printf("%s\n",str);
	if(ret < 0 && (strcmp(errmsg,"Success") != 0))
	{
		printf(errmsg);
	}
	return ret;
}

/*********************************************************************/
int send_tcp(int sd, UCHAR *str,int len)
{
	int ret = 0;
	char errmsg[60];
	memset(errmsg,0,60);
//	pthread_mutex_lock( &tcp_write_lock);
	ret = put_sock(sd, str,len,1,&errmsg[0]);
//	pthread_mutex_unlock(&tcp_write_lock);
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
int put_sock(int sd, UCHAR *buf,int buflen, int block, char *errmsg)
{
	int rc = 0;
	char extra_msg[10];
	if(block)
// block
		rc = send(sd,buf,buflen,MSG_WAITALL);
	else
// don't block
		rc = send(sd,buf,buflen,MSG_DONTWAIT);
	if(rc < 0 && errno != 11)
	{
		strcpy(errmsg,strerror(errno));
		sprintf(extra_msg," %d",errno);
		strcat(errmsg,extra_msg);
		strcat(errmsg," put_sock");
//		close_tcp();
	}else strcpy(errmsg,"Success\0");
	return rc;
}

/*********************************************************************/
int get_sock(int sd, UCHAR *buf, int buflen, int block, char *errmsg)
{
	int rc;
	char extra_msg[10];
	if(block)
		rc = recv(sd,buf,buflen,MSG_WAITALL);
	else
		rc = recv(sd,buf,buflen,MSG_DONTWAIT);
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
