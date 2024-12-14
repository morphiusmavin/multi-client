// aux_client2a.c - calls aux_client.c via ipc - same as aux_client2 but sends a string 
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
#include "../mytypes.h"
#include "../cmd_types.h"

extern void *memcpy(void *dest, const void *src, size_t n);

#define SEND_CMD_HOST_QKEY	1235

typedef unsigned char UCHAR;
typedef unsigned int UINT;
typedef UCHAR* PUCHAR;
typedef unsigned long ULONG;

extern CMD_STRUCT cmd_array[];

void print_cmd(UCHAR cmd)
{
	char tempx[30];
	
	if(cmd > NO_CMDS)
		printf("unknown cmd: %d\n",cmd);

	sprintf(tempx, "cmd: %d %s\0",cmd,cmd_array[cmd].cmd_str);
	printf("%s\r\n",cmd_array[cmd].cmd_str);
}


#endif
/*********************************************************************/
int main(int argc, char **argv)
{
	int i;
	UCHAR dest;
	int sock_qid;
	key_t sock_key;
	UCHAR cmd;
	struct msgqbuf msg;
	int msg_len;
	UCHAR str[200];

	int msgtype = 1;
	msg.mtype = msgtype;

	sock_key = SEND_CMD_HOST_QKEY;
	sock_qid = msgget(sock_key, IPC_CREAT | 0666);

	if(argc < 4)
	{
		printf("usage: %s <cmd> <dest> <string>\n",argv[0]);
		printf("%s will do a cmd on dest with string param \n",argv[0]);
		printf("dest: 8 = server, 2 = cabin, 3 = testbench...\n");
		exit(1);
	}
	cmd = atoi(argv[1]);
	print_cmd(cmd);
	dest = atoi(argv[2]);
	strcpy(str,argv[3]);
	msg_len = strlen(str);
	printf("%d %s\n",msg_len,str);
	msg.mtype = msgtype;
	memset(msg.mtext,0,sizeof(msg.mtext));
	msg.mtext[0] = cmd;
	msg.mtext[1] = dest;
	msg.mtext[2] = (UCHAR)(msg_len & 0x0F);
	msg.mtext[3] = (UCHAR)((msg_len & 0xF0) >> 4);
	printf("%02x %02x\n",msg.mtext[2],msg.mtext[3]);
	memcpy(&msg.mtext[4],str,msg_len);

	printf("dest: %d msg_len: %d\n",dest,strlen(str));
	msg_len = strlen(str);
	printf("dest: %d msg_len: %d\n",dest,msg_len);

	if (msgsnd(sock_qid, (void *) &msg, msg_len, MSG_NOERROR) == -1) 
	{
		printf("queue failed\n");
		perror("msgsnd error");
		exit(EXIT_FAILURE);
	}

    return 0;
}

