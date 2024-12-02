#include <arpa/inet.h> // inet_addr()
#include <netdb.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <strings.h> // bzero()
#include <sys/socket.h>
#include <unistd.h> // read(), write(), close()
#include "cmd_types.h"
#define MAX 80
#define PORT 5193
#define SA struct sockaddr

typedef unsigned char UCHAR;
typedef unsigned int UINT;
typedef UCHAR* PUCHAR;
typedef unsigned long ULONG;

static UCHAR pre_preamble[] = {0xF8,0xF0,0xF0,0xF0,0xF0,0xF0,0xF0,0x00};

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

/*********************************************************************/
void func(int sockfd)
{
    char buff[MAX];
    int n;
    for (;;) {
        bzero(buff, sizeof(buff));
        printf("Enter the string : ");
        n = 0;
        while ((buff[n++] = getchar()) != '\n')
            ;
        write(sockfd, buff, sizeof(buff));
        bzero(buff, sizeof(buff));
        read(sockfd, buff, sizeof(buff));
        printf("From Server : %s", buff);
        if ((strncmp(buff, "exit", 4)) == 0) {
            printf("Client Exit...\n");
            break;
        }
    }
}

/*********************************************************************/
int main()
{
    int sockfd, connfd;
    struct sockaddr_in servaddr, cli;
	char buff[20];
	int c;

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

    // function for chat
    // func(sockfd);

	//while((c = getchar()) != '\n' && c != EOF)
	while((c = getchar()) != 'q')
	{
		bzero(buff, sizeof(buff));
		switch(c)
		{
		case 'a':
			strcpy(buff,"ABCDE\0");
			send_msg(sockfd, strlen(buff), &buff[0], SEND_STATUS, 8);		// SEND_STATUS
		break;
		case 'b':
			strcpy(buff,"FGHIJ\0");
			send_msg(sockfd, strlen(buff), &buff[0], SEND_TIMEUP, 8);		// SEND_TIMEUP
		break;
		case 'c':
			buff[0] = 0;
			send_msg(sockfd, 0, &buff[0], SEND_STATUS, 2);					// cabin
		break;
		case 'd':
			buff[0] = 0;
			send_msg(sockfd, 0, &buff[0], SET_TIME, 2);
		break;
		case 'e':
			buff[0] = 0;
			send_msg(sockfd, 0, &buff[0], SET_TIME, 3);						// testbench
		break;
		case 'f':
			buff[0] = 0;
			send_msg(sockfd, 1, &buff[0], BENCH_LIGHT1, 3);
		break;
		case 'g':
			buff[0] = 1;
			send_msg(sockfd, 1, &buff[0], BENCH_LIGHT1, 3);
		break;
		case 'h':
			buff[0] = 0;
			send_msg(sockfd, 1, &buff[0], EAST_LIGHT, 8);
			break;
		case 'i':
			buff[0] = 1;
			send_msg(sockfd, 1, &buff[0], EAST_LIGHT, 8);
			break;
		case 'q':
		close(sockfd);
		return 0;
		break;
		default:
		break;
		}
		printf("%c", c);
	}
	if(c == 'q' || c == 'Q')
		close(sockfd);
    return 0;
}