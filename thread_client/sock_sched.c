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
#include "ioports.h"
#include "serial_io.h"
#include "tasks.h"
#include "queue/ollist_threads_rw.h"
int threads_ready_count=0;
pthread_cond_t    threads_ready=PTHREAD_COND_INITIALIZER;
pthread_mutex_t   threads_ready_lock=PTHREAD_MUTEX_INITIALIZER;

UCHAR (*fptr[NUM_SOCK_TASKS])(int) = { 
	get_host_cmd_task1,
	tcp_monitor_task,
	recv_msg_task };

/*
 * Thread initialization
 */
/*
 * Thread progress info
 */
/**********************************************************************/
void *work_routine(void *arg)
{
	int *my_id=(int *)arg;
	int i;
	UCHAR pattern = 0;
	int not_done=1;
	i = not_done;

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
		(*fptr[*my_id])(i);
		i--;
		not_done--;
	}
	return(NULL);
}

UCHAR reboot_on_exit;

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
	reboot_on_exit = 1;

	//printf("starting sock_sched...\n");

	id_arg = (int *)malloc(NUM_SOCK_TASKS*sizeof(int));

	basic_controls_key = BASIC_CONTROLS_QKEY;
	send_cmd_host_key = SEND_CMD_HOST_QKEY;
	recv_cmd_host_key = RECV_CMD_HOST_QKEY;

	basic_controls_qid = msgget(basic_controls_key, IPC_CREAT | 0666);
	send_cmd_host_qid = msgget(send_cmd_host_key, IPC_CREAT | 0666);
	recv_cmd_host_qid = msgget(recv_cmd_host_key, IPC_CREAT | 0666);

	for(i = 0;i < NUM_SOCK_TASKS;i++)
		id_arg[i] = i;

	_threads[GET_HOST_CMD1].sched = TIME_SLICE;
	_threads[TCP_MONITOR].sched = TIME_SLICE;
	_threads[RECV_MSG].sched = TIME_SLICE;

	strcpy(_threads[GET_HOST_CMD1].label,"GET_HOST_CMD1\0");
	strcpy(_threads[TCP_MONITOR].label,"TCP_MONITOR\0");
	strcpy(_threads[RECV_MSG].label,"RECV_MSG\0");

/* spawn the threads */
	for (i = 0; i < NUM_SOCK_TASKS; i++)
//	for (i = 5;i < 6;i++)
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
		//printf("%s %d %d \n", _threads[i].label,_threads[i].prio_min, _threads[i].prio_max);
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
	}

	//printf("\nmain()\t\t\t\t%d threads created. Main running fifo at max\n", NUM_SOCK_TASKS);

/* wait until all threads have finished */
//	serial_thread = _threads[SERIAL_RECV].pthread;

	for (i = 0; i < NUM_SOCK_TASKS; i++)
	{
		if (pthread_join(_threads[i].pthread, NULL) !=0)
		{
			printf("exit\r\n");
			perror("main() pthread_join failed"),exit(1);
		}
printf("closing task: %s\n",_threads[i].label);
/*
		if(i == 0)
		{
			pthread_cancel(_threads[SERIAL_RECV].pthread);
			pthread_cancel(_threads[TCP_MONITOR].pthread);
		}
*/
//		printf("closing task :%d %s\r\n",i,_threads[i].label);
	}

//	RS232_CloseComport(1);
//	strcpy(str2,"close");
//	send(sd,str2,5,0);
//	printf("reboot_on_exit: %d\n",reboot_on_exit);
//	usleep(100000);
//	closesocket(sd);
//	printf("socket closed\n");
//	llist_show(&ll);
	if(reboot_on_exit == 1)
	{
		printf("sched: exit to shell\r\n");
		return 1;
	}
	else if(reboot_on_exit == 2)
	{
		printf("sched: reboot\r\n");
		return 2;
	}
	else if(reboot_on_exit == 3)
	{
		printf("sched: shutdown\r\n");
		return 3;
	}
	else if(reboot_on_exit == 4)
	{
		printf("upload new sched\r\n");
		return 4;
	}
	else if(reboot_on_exit == 5)
	{
		printf("upload new param\r\n");
		return 5;
	}
	else if(reboot_on_exit == 6)
	{
		printf("shell and rename\n");
		return 6;
	}
}
