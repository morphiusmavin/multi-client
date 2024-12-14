// aux_client.c - runs on 148 as a client of _SERVER  
#if 1
#include <arpa/inet.h> // inet_addr()
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <strings.h> // bzero()
#include <sys/types.h>
#include <sys/socket.h>
#include <unistd.h> // read(), write(), close()
#include <sys/ipc.h>
#include <sys/msg.h>
#include <sys/mman.h>
#include <fcntl.h>
#include <assert.h>
#include <time.h>
#include <sys/time.h>
#include <ctype.h>
#include <sys/stat.h>
#include <netinet/in.h>
#include <netdb.h>
#include <errno.h>
#include "../mytypes.h"
#include "../cmd_types.h"
#define MAX 80
#define PORT 5193
#define SA struct sockaddr
#define SEND_CMD_HOST_QKEY	1235

typedef unsigned char UCHAR;
typedef unsigned int UINT;
typedef UCHAR* PUCHAR;
typedef unsigned long ULONG;
static UCHAR tempx[1000];
static UCHAR pre_preamble[] = {0xF8,0xF0,0xF0,0xF0,0xF0,0xF0,0xF0,0x00};
extern CMD_STRUCT cmd_array[];
static int sockfd;
static int error_no;
/*********************************************************************/
void print_cmd(UCHAR cmd)
{
	char tempx[30];

	if(cmd > NO_CMDS)
		printf("unknown cmd: %d\n",cmd);

	sprintf(tempx, "cmd: %d %s\0",cmd,cmd_array[cmd].cmd_str);
	printf("%s\r\n",cmd_array[cmd].cmd_str);
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
		if(error_no++ > 4)
		{
			close(sockfd);
			exit(1);
		}
		return -1;
	}else error_no = 0;
	ret = recv_tcp(&low,1,1);
	ret = recv_tcp(&high,1,1);
	/* printf("%02x %02x\n",low,high); */
	len = 0;
	len = (int)(high);
	len <<= 4;
	len |= (int)low;

	return len;
}
#endif
/*********************************************************************/
int main(void)
{
    struct sockaddr_in servaddr, cli;
	int c;
	int i;
	UCHAR dest;
	int sock_qid;
	key_t sock_key;
	UCHAR cmd;
	struct msgqbuf msg;
	int msg_len;
	UCHAR onoff;
	int rc;
	error_no = 0;

	int msgtype = 1;
	msg.mtype = msgtype;

	sock_key = SEND_CMD_HOST_QKEY;
	sock_qid = msgget(sock_key, IPC_CREAT | 0666);

    // socket create and verification
    sockfd = socket(AF_INET, SOCK_STREAM, 0);
    if (sockfd == -1) {
        printf("socket creation failed...\n");
        exit(0);
    }
    else
        printf("Socket successfully created..\n");
    bzero(&servaddr, sizeof(servaddr));

    // assign IP, PORT
    servaddr.sin_family = AF_INET;
    servaddr.sin_addr.s_addr = inet_addr("192.168.88.146");
    servaddr.sin_port = htons(PORT);

    // connect the client socket to server socket
    if (connect(sockfd, (SA*)&servaddr, sizeof(servaddr))
        != 0) {
        printf("connection with the server failed...\n");
        exit(0);
    }
    else
        printf("connected to the server..\n");

	while(1)
	{
		cmd = 0;
		memset(tempx,0,sizeof(tempx));
		//printf("wait for msg_len\n");
		msg_len = get_msg();
		//printf("sock_mgt\n");
		printf("msg_len: %d\n",msg_len);

		if(msg_len < 0)
		{
			//printf("bad msg\r\n");
			printf("b");
			cmd = BAD_MSG;
			sleep(1);
		}else
		{
			rc = recv_tcp(&tempx[0],msg_len+1,1);
			printf("rc: %d\n",rc);
			cmd = tempx[0];
			//printf("client get_host_cmd_task\n");
			print_cmd(cmd);
			if(cmd == SHUTDOWN_IOBOX || cmd == REBOOT_IOBOX || cmd == SHELL_AND_RENAME || cmd == EXIT_TO_SHELL)
			{
				close(sockfd);
				exit(1);
			}
			memcpy(tempx,tempx+1,msg_len);
			tempx[msg_len] = 0;

			for(i = 0;i < msg_len;i++)
				printf("%02x ",tempx[i]);
			printf("\n");
			for(i = 0;i < msg_len;i++)
				printf("%c",tempx[i]);
			printf("\n");
/*
			memset(msg.mtext,0,sizeof(msg.mtext));
			msg.mtext[0] = cmd;
			msg.mtext[1] = (UCHAR)msg_len;
			msg.mtext[2] = (UCHAR)(msg_len >> 4);
			memcpy(msg.mtext + 3,tempx,msg_len);
*/

		}
	}
    return 0;
}
