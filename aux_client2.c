// aux_client2.c - calls aux_client.c via ipc 
#if 1
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <strings.h> // bzero()
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

#include "cmd_types.h"
#define SEND_CMD_HOST_QKEY	1235

typedef unsigned char UCHAR;
typedef unsigned int UINT;
typedef UCHAR* PUCHAR;
typedef unsigned long ULONG;
UCHAR tempx[1000];

struct msgqbuf 
{
	long mtype;
	UCHAR mtext[1000];
};
#endif
/*********************************************************************/
int main(int argc, char **argv)
{
	char buff[20];
	int i;
	UCHAR dest;
	int sock_qid;
	key_t sock_key;
	UCHAR cmd;
	UCHAR onoff;
	struct msgqbuf msg;
	int msg_len;

	int msgtype = 1;
	msg.mtype = msgtype;

	sock_key = SEND_CMD_HOST_QKEY;
	sock_qid = msgget(sock_key, IPC_CREAT | 0666);

	if(argc < 4)
	{
		printf("usage: %s <cmd> <dest> <onoff>\n",argv[0]);
		printf("%s will do a cmd on dest with param of on or off\n",argv[0]);
		printf("dest: 8 = server, 2 = cabin, 3 = testbench...\n");
		exit(1);
	}
	cmd = atoi(argv[1]);
	dest = atoi(argv[2]);
	onoff = atoi(argv[3]);
	if(onoff < 0 || onoff > 1)
	{
		printf("onoff must be either '1' or '0'\n");
		exit(1);
	}

	msg.mtype = msgtype;
	memset(msg.mtext,0,sizeof(msg.mtext));
	msg.mtext[0] = cmd;
	msg.mtext[1] = dest;
	msg.mtext[2] = onoff;

	//printf("cmd: %d dest: %d onoff: %d\n",cmd,dest,onoff);
	
	if (msgsnd(sock_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
	{
		printf("queue failed\n");
		perror("msgsnd error");
		exit(EXIT_FAILURE);
	}

    return 0;
}