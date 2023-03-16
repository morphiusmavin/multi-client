
/********************************************************
 * An example source module to accompany...
 *
 * "Using POSIX Threads: Programming with Pthreads"
 *     by Brad nichols, Dick Buttlar, Jackie Farrell
 *     O'Reilly & Associates, Inc.
 *
 ********************************************************
 * sched.c
 *
 * Examples in setting scheduling policy
 */
#include <unistd.h>
#include <sys/mman.h>
#include <fcntl.h>
#include <assert.h>
#include <time.h>
#include <sys/time.h>
#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>
#include <sched.h>
#include <sys/types.h>
#include <pthread.h>
#include <string.h>
#define closesocket close
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h>
#include <sys/ipc.h>
#include "../mytypes.h"
#include "../serial_io.h"
#include "../tasks.h"

extern void *work_routine(void *arg);

//pthread_t serial_thread;	// workaround for closing serial thread (serial read is blocking)

typedef struct
{
	pthread_t pthread;
	int sched;
	int prio_min;
	int prio_max;
	char label[30];
}THREADS;

/********************************************************************************************************/
int main(int argc, char **argv)
{
	int rtn, t, i, j, *id_arg, prio_min, prio_max;
	THREADS _threads[NUM_SOCK_TASKS];
	int sched = PTIME_SLICE;
	pthread_attr_t pthread_custom_attr;
	struct sched_param priority_param;
//	char str2[10];
//	char buf[100];
	UCHAR test1 = 0x21;
	UCHAR test2;
	int sd;
	int rc;
	key_t sock_key;
	key_t sched_key;

	id_arg = (int *)malloc(NUM_SOCK_TASKS*sizeof(int));

	// get the queue id for the get_host_cmd_task
	// CMD_HOST_QKEY

	sock_key = SEND_CMD_HOST_QKEY;
	sched_key = RECV_CMD_HOST_QKEY;

	sock_qid = msgget(sock_key, IPC_CREAT | 0666);
	sched_qid = msgget(sched_key, IPC_CREAT | 0666);

	for(i = 0;i < NUM_SOCK_TASKS;i++)
		id_arg[i] = i;

/*
 TIME_SLICE              1
 FIFO                    2
 PRIOR                   3
 PTIME_SLICE             4
 PFIFO                   5
*/

	_threads[GET_HOST_CMD1].sched = TIME_SLICE;
	_threads[TCP_MONITOR].sched = TIME_SLICE;
	_threads[SOCK_TIMER].sched = TIME_SLICE;

	_threads[WINCL_READ_TASK1].sched = TIME_SLICE;
	_threads[WINCL_READ_TASK2].sched = TIME_SLICE;

	_threads[READ_TASK1].sched = TIME_SLICE;
	_threads[READ_TASK2].sched = TIME_SLICE;
	_threads[READ_TASK3].sched = TIME_SLICE;
	_threads[READ_TASK4].sched = TIME_SLICE;
	_threads[READ_TASK5].sched = TIME_SLICE;
	_threads[READ_TASK6].sched = TIME_SLICE;

	strcpy(_threads[GET_HOST_CMD1].label,"GET_HOST_CMD\0");
	strcpy(_threads[TCP_MONITOR].label,"TCP_MONITOR\0");
	strcpy(_threads[SOCK_TIMER].label,"SOCK_TIMER\0");
	strcpy(_threads[WINCL_READ_TASK1].label,"WINCL_READ_TASK1\0");
	strcpy(_threads[WINCL_READ_TASK2].label,"WINCL_READ_TASK2\0");

	strcpy(_threads[READ_TASK1].label,"READ_TASK1\0");
	strcpy(_threads[READ_TASK2].label,"READ_TASK2\0");
	strcpy(_threads[READ_TASK3].label,"READ_TASK3\0");
	strcpy(_threads[READ_TASK4].label,"READ_TASK4\0");
	strcpy(_threads[READ_TASK5].label,"READ_TASK5\0");
	strcpy(_threads[READ_TASK6].label,"READ_TASK6\0");
/* spawn the threads */

	assign_client_table();
	printf("\n");

	for (i = 0; i < NUM_SOCK_TASKS; i++)
	{
/*
		if (sched == DEFAULT)
		{
			if (pthread_create(&(threads[i]), NULL, work_routine, (void *) &(id_arg[i])) !=0)
				perror("main() pthread create with NULL attr obj failed"),exit(1);

		} else
*/
		priority_param.sched_priority = sched_get_priority_max(SCHED_FIFO);
		pthread_setschedparam(pthread_self(), SCHED_FIFO, &priority_param);
/*
		if (sched == INHERIT)
		{
			pthread_attr_init(&pthread_custom_attr);
			pthread_attr_setinheritsched(&pthread_custom_attr, PTHREAD_INHERIT_SCHED);
		} else
*/

		pthread_attr_init(&pthread_custom_attr);
		pthread_attr_setinheritsched(&pthread_custom_attr, PTHREAD_EXPLICIT_SCHED);

		switch(_threads[i].sched)
		{
			case FIFO:
				pthread_attr_setschedpolicy(&pthread_custom_attr, SCHED_FIFO);
				_threads[i].prio_min = sched_get_priority_min(SCHED_FIFO);
				_threads[i].prio_max = sched_get_priority_max(SCHED_FIFO);
				priority_param.sched_priority = _threads[i].prio_min + \
					1*(_threads[i].prio_max - _threads[i].prio_min)/(NUM_SOCK_TASKS-1);
				break;
			case TIME_SLICE:
				pthread_attr_setschedpolicy(&pthread_custom_attr, SCHED_RR);
				_threads[i].prio_min = sched_get_priority_min(SCHED_RR);
				_threads[i].prio_max = sched_get_priority_max(SCHED_RR);
				priority_param.sched_priority = _threads[i].prio_min;
				break;
			case PFIFO:
				pthread_attr_setschedpolicy(&pthread_custom_attr, SCHED_FIFO);
				_threads[i].prio_max = sched_get_priority_max(SCHED_FIFO);
				_threads[i].prio_min = sched_get_priority_min(SCHED_FIFO);
				priority_param.sched_priority = _threads[i].prio_max;
				break;
			case PTIME_SLICE:
				pthread_attr_setschedpolicy(&pthread_custom_attr, SCHED_RR);
				_threads[i].prio_max = sched_get_priority_max(SCHED_RR);
				_threads[i].prio_min = sched_get_priority_min(SCHED_RR);
				priority_param.sched_priority = _threads[i].prio_min + \
					4*(_threads[i].prio_max - _threads[i].prio_min)/(NUM_SOCK_TASKS-1);
				break;
			default:
				break;
		}
		//printf("%d %s \n", i, _threads[i].label);
//		printf("%d %s %d %d \n", i, _threads[i].label,_threads[i].prio_min, _threads[i].prio_max);
//			if(i == 5)
//				priority_param.sched_priority = _threads[i].prio_max;
//			else
//		priority_param.sched_priority = _threads[i].prio_max - i - 2;

//		priority_param.sched_priority = _threads[i].prio_max;

//		printf("setting priority(%d-%d) of thread %s to %d\n", _threads[i].prio_min, \
			_threads[i].prio_max, _threads[i].label,priority_param.sched_priority);

		if (pthread_attr_setschedparam(&pthread_custom_attr, &priority_param)!=0)
			fprintf(stderr,"pthread_attr_setschedparam failed\n");

		if (pthread_create(&(_threads[i].pthread),
			&pthread_custom_attr,
			work_routine,
			(void *) &(id_arg[i])) !=0)
			perror("main() pthread create with attr obj failed"),exit(1);
//			printf("id_arg: %d\n",&id_arg[i]);
	}

/* wait until all threads have finished */
//	serial_thread = _threads[SERIAL_RECV].pthread;

	for (i = 0; i < NUM_SOCK_TASKS; i++)
	{
		if (pthread_join(_threads[i].pthread, NULL) !=0)
		{
			printf("exit\r\n");
			perror("main() pthread_join failed"),exit(1);
		}

		if(i == 0)
		{
			pthread_cancel(_threads[SERIAL_RECV].pthread);
			pthread_cancel(_threads[TCP_MONITOR].pthread);
		}
	}

	return 1;
}
