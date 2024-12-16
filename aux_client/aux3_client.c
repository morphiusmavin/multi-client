// aux3_client.c 
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
#include <arpa/inet.h>
#include <netdb.h>
#include <errno.h>
#include <sys/types.h>
/* #include <sys/ipc.h> */
#include <sys/msg.h>
#include "../mytypes.h"
#include "../cmd_types.h"
#define MAX 80
#define PORT 5193
#define SA struct sockaddr

typedef unsigned char UCHAR;
typedef unsigned int UINT;
typedef UCHAR* PUCHAR;
typedef unsigned long ULONG;
UCHAR tempx[1000];
static UCHAR pre_preamble[] = {0xF8,0xF0,0xF0,0xF0,0xF0,0xF0,0xF0,0x00};

#define SEND_CMD_HOST_QKEY	1235

static int sock_qid;
static key_t sock_key;
static int sockfd;

int put_sock(UCHAR *buf,int buflen, int block, char *errmsg);
int get_sock(UCHAR *buf, int buflen, int block, char *errmsg);
void send_msg(int msg_len, UCHAR *msg, UCHAR msg_type, int dest);
int get_msgb(void);
void send_msgb(int msg_len, UCHAR *msg, UCHAR msg_type);
int recv_tcp(UCHAR *str, int strlen,int block);
int send_tcp(UCHAR *str,int len);
int get_msg(void);

extern CMD_STRUCT cmd_array[];

void print_cmd(UCHAR cmd)
{
	char tempx[30];
	
	if(cmd > NO_CMDS)
		printf("unknown cmd: %d\n",cmd);

	sprintf(tempx, "cmd: %d %s\0",cmd,cmd_array[cmd].cmd_str);
	printf("%s\r\n",cmd_array[cmd].cmd_str);
}
/*********************************************************************/
int get_msg(void)
{
	int len;
	UCHAR low, high;
	int ret;
	int i;

	UCHAR preamble[10];
	ret = recv_tcp(preamble,8,1);
	/* printf("ret: %d\n",ret); */
	if(ret < 0)
	{
		printf("%02x ",ret);
	}
	if(memcmp(preamble,pre_preamble,8) != 0)
	{
		printf("bad preamble\n");
		for(i = 0;i < 10;i++)
			printf("%02x ",preamble[i]);
		printf("\n");
		sleep(1);
		return -1;
	}
	ret = recv_tcp(&low,1,1);
	ret = recv_tcp(&high,1,1);
	/* printf("%02x %02x\n",low,high); */
	len = 0;
	len = (int)(high);
	len <<= 4;
	len |= (int)low;

	return len;
}
/*********************************************************************/
void send_msg(int msg_len, UCHAR *msg, UCHAR msg_type, int dest)
{
	int ret;
	int i;
	UCHAR temp[2];

	ret = send_tcp(&pre_preamble[0],8);
	temp[0] = (UCHAR)(msg_len & 0x0F);
	temp[1] = (UCHAR)((msg_len & 0xF0) >> 4);
	/* printf("%02x %02x\n",temp[0],temp[1]);	*/
	send_tcp((UCHAR *)&temp[0],1);
	send_tcp((UCHAR *)&temp[1],1);
	send_tcp((UCHAR *)&msg_type,1);
	send_tcp((UCHAR *)&dest,1);

	for(i = 0;i < msg_len;i++)
		send_tcp((UCHAR *)&msg[i],1);
}
/*********************************************************************/
int get_msgb(void)
{
	int len;
	UCHAR low, high;
	int ret;
	int i;

	UCHAR preamble[20];
	ret = recv_tcp(preamble,16,1);
	if(ret < 0)
	{
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
void send_msgb(int msg_len, UCHAR *msg, UCHAR msg_type)
{
	int len;
	int ret;
	int i;

	ret = send_tcp(&pre_preamble[0],8);
	msg_len++;
	send_tcp((UCHAR *)&msg_len,1);
	ret = 0;
	send_tcp((UCHAR *)&ret,1);

	for(i = 0;i < 6;i++)
		send_tcp((UCHAR *)&ret,1);

	send_tcp((UCHAR *)&msg_type,1);

	ret = 0;
	send_tcp((UCHAR *)&ret,1);

	for(i = 0;i < msg_len;i++)
	{
		send_tcp((UCHAR *)&msg[i],1);
		send_tcp((UCHAR *)&ret,1);
	}
}

/*********************************************************************/
int recv_tcp(UCHAR *str, int strlen,int block)
{
	int ret = -1;
	char errmsg[20];
	memset(errmsg,0,20);
	ret = get_sock(str,strlen,block,&errmsg[0]);
	if(ret < 0 && (strcmp(errmsg,"Success") != 0))
	{
		printf(errmsg);
	}
	return ret;
}

/*********************************************************************/
int send_tcp(UCHAR *str,int len)
{
	int ret = 0;
	char errmsg[60];
	memset(errmsg,0,60);
	ret = put_sock(str,len,1,&errmsg[0]);
	if(ret < 0 && (strcmp(errmsg,"Success") != 0))
	{
		printf(errmsg);
	}
	return ret;
}

/*********************************************************************/
int put_sock( UCHAR *buf,int buflen, int block, char *errmsg)
{
	int rc = 0;
	char extra_msg[10];
	if(block)

		rc = send(sockfd, buf,buflen,MSG_WAITALL);
	else

		rc = send(sockfd, buf,buflen,MSG_DONTWAIT);

	if(rc < 0)
	{

		strcpy(errmsg,strerror(errno));
		sprintf(extra_msg," %d\n",errno);
		strcat(errmsg,extra_msg);
		strcat(errmsg,"\nput_sock\n");

	}else strcpy(errmsg,"Success\0");
	return rc;
}

/*********************************************************************/
int get_sock(UCHAR *buf, int buflen, int block, char *errmsg)
{
	int rc;
	char extra_msg[10];
	if(block)
		rc = recv(sockfd, buf,buflen,MSG_WAITALL);
	else
		rc = recv(sockfd, buf,buflen,MSG_DONTWAIT);
	if(rc < 0 && errno != 11)
	{
		strcpy(errmsg,strerror(errno));
		sprintf(extra_msg," %d",errno);
		strcat(errmsg,extra_msg);
		strcat(errmsg," get_sock");
	}else strcpy(errmsg,"Success\0");
	return rc;
}
#endif
/*********************************************************************/
int main(void)
{
	int c;
	int i;
	UCHAR dest;
	UCHAR cmd;
	int msg_len;
	UCHAR onoff;
	struct msgqbuf msg;
	UCHAR msg_buf[200];

	int msgtype = 1;
	msg.mtype = msgtype;
/*
    struct sockaddr_in servaddr, cli;

	sockfd = -1;
	int r1 = 1;

	sock_key = SEND_CMD_HOST_QKEY;
	sock_qid = msgget(sock_key, IPC_CREAT | 0666);

    sockfd = socket(AF_INET, SOCK_STREAM, 0);
    if (sockfd == -1) {
        printf("socket creation failed...\n");
        exit(0);
    }
    else
        printf("Socket successfully created..\n");
    bzero(&servaddr, sizeof(servaddr));

    servaddr.sin_family = AF_INET;
    servaddr.sin_addr.s_addr = inet_addr("192.168.88.146");
    servaddr.sin_port = htons(PORT);


    if (connect(sockfd, (SA*)&servaddr, sizeof(servaddr))
        != 0) {
        printf("connection with the server failed...\n");
        exit(0);
    }
    else
        printf("connected to the server..\n");
*/
	printf("running w/o TCP\n");
	while(1)
	{
		memset(msg.mtext,0,sizeof(msg.mtext));

		if (msgrcv(sock_qid, (void *) &msg, sizeof(msg.mtext), msgtype, MSG_NOERROR) == -1) 
		{
			if (errno != ENOMSG) 
			{
				perror("msgrcv");
				printf("msgrcv error\n");
				exit(EXIT_FAILURE);
			}
		}
		for(i = 0;i < 20;i++);
		{
			printf("%02x ",msg.mtext[i]);
		}
		printf("\n");

		cmd = msg.mtext[0];							// first byte is cmd
		print_cmd(cmd);
		dest = (int)msg.mtext[1];					// 2nd byte is dest
		msg_len = (int)msg.mtext[2];				// 3rd is low byte of msg_len
		msg_len |= (int)(msg.mtext[3] << 4);		// 4th is high byte of msg_len
/*
		for(i = 0;i < msg_len+4;i++)
			printf("%02x ",msg.mtext[i]);
*/
		printf("\nmsg_len: %d dest: %d\n",msg_len,dest);

		memset(msg_buf,0,sizeof(msg_buf));
		memcpy(msg_buf,&msg.mtext[4],msg_len);
		msg_len = msg_len>255?255:msg_len;
/*
		for(i = 0;i < msg_len;i++)
			printf("%02x ",msg_buf[i]);
*/
		for(i = 0;i < msg_len;i++)
			printf("%02x ",msg.mtext[i+4]);
		printf("\n");
		for(i = 0;i < msg_len;i++)
			printf("%c",msg.mtext[i+4]);
		printf("\n");
		// dest is used in ReadTask to know where to send msg 
		send_msg(msg_len, msg_buf, cmd, dest);
	}
	return 0;
}

