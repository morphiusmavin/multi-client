#include <unistd.h>
#include <sys/types.h>
#include <sys/mman.h>
#include <stdio.h>
#include <fcntl.h>
#include <assert.h>
#include <time.h>
#include <stdlib.h>
#include <sys/time.h>
#include <string.h>
#include <errno.h>
#include <sys/ipc.h>
#include <sys/stat.h>
#include <sys/msg.h>
#include <semaphore.h>
#include <sys/types.h>
#include "../mytypes.h"
#define REMOTE_QKEY 1300

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
				printf("uSleep error\n");
//             return -1;
			}
		}
		else
		{
/* nanosleep succeeded, so exit the loop */
			break;
		}
	} while ( req.tv_sec > 0 || req.tv_nsec > 0 );

	return 0;	  /* Return success */
}

/**********************************************************************/
int main(void)
{
	int i,j,key;
	UCHAR temp;
	int tog;
	temp = 0x21;
	key_t remote_key;
	int remote_qid;
	remote_key = REMOTE_QKEY;
	remote_qid = msgget(remote_key, IPC_CREAT | 0666);
	struct msgqbuf msg;
	int msgtype = 1;
	msg.mtype = msgtype;

	for(;;)
	{
		if(msgrcv(remote_qid, (void *)&msg, sizeof(msg.mtext),
				msgtype, MSG_NOERROR | IPC_NOWAIT) == -1)
		{
			if(errno != ENOMSG)
			{
				perror("msgrcv");
				exit(EXIT_FAILURE);
			}
		}else
		{
			printf("%s\n",msg.mtext);
		}
	}
	return 0;
}
