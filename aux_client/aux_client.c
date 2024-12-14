// aux_client.c - runs on 148 as a client of _SERVER  
#if 1
#include <arpa/inet.h> // inet_addr()
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <strings.h> // bzero()
#include <sys/socket.h>
#include <unistd.h> // read(), write(), close()
#include <sys/types.h>
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
#include <sys/types.h>
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
UCHAR tempx[1000];
static UCHAR pre_preamble[] = {0xF8,0xF0,0xF0,0xF0,0xF0,0xF0,0xF0,0x00};
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
	//if(rc < 0 && errno != 11)
	if(rc < 0)
	{
		//printf("sd: %d\n",sd);
/*
		strcpy(errmsg,strerror(errno));
		sprintf(extra_msg," %d\n",errno);
		strcat(errmsg,extra_msg);
		strcat(errmsg,"\nput_sock\n");
*/
		printf("error in put_sock\n");
		return -1;
//		close_tcp();
	}else strcpy(errmsg,"Success\0");
	return rc;
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
		printf("error in send_tcp\n");
	}
	return ret;
}

/*********************************************************************/
void send_msg(int sd, int msg_len, UCHAR *msg, UCHAR msg_type, UCHAR dest)
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
	send_tcp(sd, (UCHAR *)&dest,1);

	for(i = 0;i <= msg_len-1;i++)
	{
		send_tcp(sd, (UCHAR *)&msg[i],1);
		//printf("%c",msg[i]);
	}
}
#endif
/*********************************************************************/
int main(void)
{
    int sockfd, connfd;
    struct sockaddr_in servaddr, cli;
	char buff[20];
	int c;
	int i;
	UCHAR dest;
	int sock_qid;
	key_t sock_key;
	UCHAR cmd;
	struct msgqbuf msg;
	int msg_len;
	UCHAR onoff;

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
		if (msgrcv(sock_qid, (void *) &msg, sizeof(msg.mtext), msgtype, MSG_NOERROR) == -1) 
		{
			if (errno != ENOMSG) 
			{
				perror("msgrcv");
				printf("msgrcv error\n");
				exit(EXIT_FAILURE);
			}
		}

		printf("\n");
		for(i = 0;i < 4;i++)
		{
			printf("%02x ",msg.mtext[i]);
		}
		printf("\n");
		
		cmd = msg.mtext[0];
		print_cmd(cmd);
		msg_len = (int)msg.mtext[1];
		msg_len |= (int)(msg.mtext[2] << 4);

		//printf("msg_len: %d\n",msg_len);
		memset(tempx,0,sizeof(tempx));
		memcpy(tempx,msg.mtext+3,msg_len);
		onoff = tempx[0];
		if(cmd == SHUTDOWN_IOBOX || cmd == REBOOT_IOBOX || cmd == SHELL_AND_RENAME || cmd == EXIT_TO_SHELL)
		{
			printf("exiting program\n");
			close(sockfd);
			exit(1);
		}
		printf("dest: %d\n", dest);
		if(onoff == 1)
			printf("ON\n");
		else printf("OFF\n");
		memset(tempx,0,sizeof(tempx));
		send_msg(sockfd, 1, &onoff, cmd, dest);
	}
    return 0;
}
