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
#include "serial_io.h"
#include "tasks.h"
#include "queue/ollist_threads_rw.h"

extern int init_mem(void);
extern void close_mem(void);
/*
 * Thread initialization
 */
/*
 * Thread progress info
 */
extern void *work_routine(void *arg);

char oFileName[20];
char cFileName[20];
char dFileName[20];

pthread_cond_t  threads_ready;
int threads_ready_count=0;
pthread_cond_t    threads_ready=PTHREAD_COND_INITIALIZER;
pthread_mutex_t   threads_ready_lock=PTHREAD_MUTEX_INITIALIZER;

typedef struct
{
	pthread_t pthread;
	int sched;
	int prio_min;
	int prio_max;
	char label[30];
}THREADS;

UCHAR (*fptr[NUM_SCHED_TASKS])(int) = { 
	get_host_cmd_task, 
	monitor_input_task, 
	poll_ds1620_task, 
	timer_task, 
	timer2_task, 
	serial_recv_task, 
	basic_controls_task};

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
	if (threads_ready_count == NUM_SCHED_TASKS)
	{
/* I was the last thread to become ready.  Tell the rest. */
		pthread_cond_broadcast(&threads_ready);
	}
	else
	{
/* At least one thread isn't ready.  Wait. */
		while (threads_ready_count != NUM_SCHED_TASKS)
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
/********************************************************************************************************/
int main(int argc, char **argv)
{
	int rtn, t, i, j, *id_arg, prio_min, prio_max;
	THREADS _threads[NUM_SCHED_TASKS];
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
	key_t basic_controls_key;

	if(argc < 2)
	{
		strcpy(oFileName,"odata.dat\0");
		strcpy(cFileName,"cdata.dat\0");
	}
	else if(argc == 2)
	{
		strcpy(oFileName,argv[1]);
		strcpy(cFileName,"cdata.dat\0");
	}
	else if(argc == 3)
	{
		strcpy(oFileName,argv[1]);
		strcpy(cFileName,argv[2]);
	}
	strcpy(dFileName,"ddata.dat");

	id_arg = (int *)malloc(NUM_SCHED_TASKS*sizeof(int));

	// get the queue id for the get_host_cmd_task
	// CMD_HOST_QKEY

	sock_key = SEND_CMD_HOST_QKEY;
	sched_key = RECV_CMD_HOST_QKEY;
	basic_controls_key = BASIC_CONTROLS_QKEY;

	sock_qid = msgget(sock_key, IPC_CREAT | 0666);
	sched_qid = msgget(sched_key, IPC_CREAT | 0666);
	basic_controls_qid = msgget(basic_controls_key, IPC_CREAT | 0666);

	for(i = 0;i < NUM_SCHED_TASKS;i++)
		id_arg[i] = i;

/*
 TIME_SLICE              1
 FIFO                    2
 PRIOR                   3
 PTIME_SLICE             4
 PFIFO                   5
*/
	_threads[GET_HOST_CMD2].sched = TIME_SLICE;
	_threads[MONITOR_INPUTS].sched = PFIFO;
	_threads[MONITOR_INPUTS2].sched = PFIFO;
	_threads[TIMER].sched = PTIME_SLICE;
	_threads[TIMER2].sched = PTIME_SLICE;

	_threads[SERIAL_RECV].sched = TIME_SLICE;
	_threads[BASIC_CONTROLS].sched = TIME_SLICE;

	strcpy(_threads[GET_HOST_CMD2].label,"GET_HOST_CMD\0");
	strcpy(_threads[MONITOR_INPUTS].label,"MONITOR_INPUTS\0");
	strcpy(_threads[MONITOR_INPUTS2].label,"MONITOR_INPUTS2\0");
	strcpy(_threads[TIMER].label,"TIMER\0");
	strcpy(_threads[TIMER2].label,"TIMER2\0");

	strcpy(_threads[SERIAL_RECV].label,"SERIAL_RECV\0");
	strcpy(_threads[BASIC_CONTROLS].label,"MSG_QUEUE\0");

/* spawn the threads */

	assign_client_table();

	for (i = 0; i < NUM_SCHED_TASKS; i++)
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
					1*(_threads[i].prio_max - _threads[i].prio_min)/(NUM_SCHED_TASKS-1);
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
					4*(_threads[i].prio_max - _threads[i].prio_min)/(NUM_SCHED_TASKS-1);
				break;
			default:
				break;
		}
//		printf("%d %s \n", i, _threads[i].label);
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

	for (i = 0; i < NUM_SCHED_TASKS; i++)
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
