#if 1
#ifdef SERVER
#warning "server defined"
#endif
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
#include <netdb.h>
#include <errno.h>
#include <dirent.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include <semaphore.h>
#include "../cmd_types.h"
#include "../mytypes.h"
#include "ioports.h"
#include "serial_io.h"
#include "queue/ollist_threads_rw.h"
#include "tasks.h"
//#include "cs_client/config_file.h"
#include "lcd_func.h"
#define TOGGLE_OTP otp->onoff = (otp->onoff == 1?0:1)

#define handle_error_en(en, msg) \
	   do { errno = en; perror(msg); exit(EXIT_FAILURE); } while (0)

#define handle_error(msg) \
	   do { perror(msg); exit(EXIT_FAILURE); } while (0)

pthread_cond_t       threads_ready;
pthread_mutex_t     tcp_write_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     tcp_read_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     io_mem_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     serial_write_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     serial_read_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     serial_write_lock2=PTHREAD_MUTEX_INITIALIZER;
//pthread_mutex_t     serial_read_lock2=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     msg_queue_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     msg_client_queue_lock=PTHREAD_MUTEX_INITIALIZER;
int total_count;

UCHAR (*fptr[NUM_TASKS])(int) = { get_host_cmd_task, monitor_input_task, 
monitor_fake_input_task, timer_task, timer2_task, WinClReadTask, WinClWriteTask,
ReadTask1, SendTask1, ReadTask2, SendTask2, serial_recv_task, 
tcp_monitor_task, basic_controls_task};

int threads_ready_count=0;
pthread_cond_t    threads_ready=PTHREAD_COND_INITIALIZER;
pthread_mutex_t   threads_ready_lock=PTHREAD_MUTEX_INITIALIZER;
static UCHAR check_inputs(int index, int test);
//extern CMD_STRUCT cmd_array[58];
ollist_t oll;
PARAM_STRUCT ps;

//extern pthread_t serial_thread;	// workaround for closing serial task

//extern int olLoadConfig(char *filename, ollist_t *oll, size_t size, char *errmsg);

static UCHAR read_serial_buffer[SERIAL_BUFF_SIZE];
static UCHAR write_serial_buffer[SERIAL_BUFF_SIZE];
static int no_serial_buff;
char password[PASSWORD_SIZE];

static int serial_rec;
static void set_output(O_DATA *otp, int onoff);
static UCHAR inportstatus[OUTPORTF_OFFSET-OUTPORTA_OFFSET+1];
static UCHAR fake_inportstatus1[OUTPORTF_OFFSET-OUTPORTA_OFFSET+1];
static UCHAR fake_inportstatus2[OUTPORTF_OFFSET-OUTPORTA_OFFSET+1];
static int mask2int(UCHAR mask);
extern int shutdown_all;
static int raw_data_array[RAW_DATA_ARRAY_SIZE];
static int raw_data_ptr;
int avg_raw_data(int prev_data);
int max_ips;
IP ip[40];
static UCHAR msg_queue[MSG_QUEUE_SIZE];
CLIENT_TABLE1 client_table[MAX_CLIENTS];

static int msg_queue_ptr;
static int msg_client_queue_ptr;
static CLIENTS clients[MSG_CLIENT_QUEUE_SIZE];
static int windows_client_sock = -1;

#define ON 1
#define OFF 0

/****************************************************************************************************/
static double curtime(void)
{
	struct timeval tv;
	if(gettimeofday (&tv, NULL) == 0)
		return tv.tv_sec + tv.tv_usec / 1000000.0;
	else return 0.0;
}
/*
 clock_t start = clock();
   //... do work here
   clock_t end = clock();
   double time_elapsed_in_seconds = (end - start)/(double)CLOCKS_PER_SEC;
   return 0;
*/
/****************************************************************************************************/
void init_ips(void)
{
	int i,j,k;
	O_DATA *otp;
	O_DATA **otpp = &otp;
	char errmsg[20];
	char tempx[20];
	UCHAR ucbuff[5];
	UCHAR xchar = 0x21;

	for(i = 0;i < 20;i++)
	{
		ip[i].port = 99;
		ip[i].input = 0;
		ip[i].function = 0;
		memset(ip[i].label,0,OLABELSIZE);
	}

	j = 0;
	// find all the outputs that have inputs assigned
	// to them and make a list to go in ip[] struct array
	for(i = 0;i < 20;i++)
	{
		for(k = 0;k < 20;k++)
		{
			if(ollist_find_data_op(i,k,&otp,&oll) > -1)
			{
				ip[j].port = k;
				ip[j].input = i;
//				printf("%d %d ",ip[j].port,ip[j].input);
//				printf("%s %d %d %d\r\n",otp->label, otp->port, otp->input_port, otp->input_type);
				strcpy(ip[j++].label,otp->label);
//				printf("%s\r\n",ip[j-1].label);
			}
		}
	}

	max_ips = 0;
	for(i = 0;i < 20;i++)
	{
		if(ip[i].port < 20)
		{
			//printf("%d: \tport: %d input: %d %s \n",i,ip[i].port,ip[i].input,ip[i].label);
			max_ips++;
		}
	}
	// add the list of inputs that do not apply to any output ports 
	// instead these inputs call functions in basic_controls_task
	// so the ip struct has port set to 0, input set to input port 
	// and function set to the CMD_STRUCT type in cmd_array
	//j = LoadSpecialInputFunctions(ip,max_ips);
	max_ips += j;

	if(i < 0)
	{
//		printf("%s\r\n",errmsg);
		myprintf1(errmsg);
	}
	i = 0;
}

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
				myprintf1("uSleep error\0");
//             return -1;
			}
		}
		else
		{
/* nanosleep succeeded, so exit the loop */
			break;
		}
	} while ( req.tv_sec > 0 || req.tv_nsec > 0 );

	return 0;									  /* Return success */
}


/*********************************************************************/
static int mask2int(UCHAR mask)
{
	int i = 0;
	do
	{
		mask >>= 1;
		i++;
	}while(mask);
	return i - 1;
}

/*********************************************************************/
// change the outputs according to the type
// type 0 is normal, 1 is reverse momentary-contact, 2 is time delayed
// type 1 makes a momentary-contact switch act like a toggle switch
// (a button press and release changes state where type 0 state 
// toggles with the switch)
static void set_output(O_DATA *otp, int onoff)
{
	O_DATA **otpp = &otp;
	UCHAR buff[1];
	char tempx[20];

	//printf("test\r\n");

	switch(otp->type)
	{
		case 0:
/*
			if(otp->polarity == 1)
				otp->onoff = (onoff == 1?0:1);
			else
				otp->onoff = onoff;
*/
			otp->onoff = onoff;
			change_output(otp->port,otp->onoff);
			ollist_insert_data(otp->port,&oll,otp);
			//printf("type 0 port: %d onoff: %d\r\n", otp->port, otp->onoff);
			break;
		case 1:
			if(otp->reset == 0)
			{
//				printf("type 1 port: %d onoff: %d\r\n", otp->port, otp->onoff);
				otp->reset = 1;
//				if(otp->polarity == 0)
				TOGGLE_OTP;
				change_output(otp->port,otp->onoff);
				ollist_insert_data(otp->port,&oll,otp);
//				printf("type 1 port: %d onoff: %d reset: %d pol: %d\r\n\r\n", otp->port,
//										otp->onoff, otp->reset, otp->polarity);
			}
			else if(otp->reset == 1)
			{
//				if(otp->polarity == 1)
//					TOGGLE_OTP;
				otp->reset = 0;
//				printf("type 1 port: %d onoff: %d reset: %d pol: %d\r\n", otp->port,
//							otp->onoff, otp->reset, otp->polarity);
			}
			break;
		case 2:
		case 3:
//			printf("type %d port: %d onoff: %d reset: %d\r\n",otp->type, otp->port, otp->onoff, otp->reset);
			if(otp->reset == 0)
			{
				otp->reset = 1;
				otp->time_left = otp->time_delay;
//							otp->onoff = onoff;
//				TOGGLE_OTP;
				otp->onoff = 1;
				change_output(otp->port,1);
				ollist_insert_data(otp->port,&oll,otp);
			}
			break;
		case 4:		// no type 4 anymore
			if(otp->reset == 0)
			{
				otp->reset = 1;
//				if(otp->polarity == 0)
//				TOGGLE_OTP;
				otp->onoff = 0;
				change_output(otp->port,otp->onoff);
				ollist_insert_data(otp->port,&oll,otp);
			}
			else if(otp->reset == 1)
			{
//				if(otp->polarity == 1)
//					TOGGLE_OTP;
				otp->reset = 3;
//				printf("type 4 port: %d onoff: %d reset: %d \r\n\r\n", otp->port,
//										otp->onoff, otp->reset);
			}
			break;
		default:
			break;
	}
/*
	if(otp->port == LHEADLAMP || otp->port == RHEADLAMP)
	{
		if(otp->onoff == 1)		// this sets the 'auto mode' for the lights to on 
		{
			lights_on = 3;
		}else
		{
			lights_on = 0;
		}
	}
*/
}
/*********************************************************************/
void send_serialother(UCHAR cmd, UCHAR *buf)
{
	int i;
	pthread_mutex_lock( &serial_write_lock);

	write_serial(cmd);
	for(i = 0;i < SERIAL_BUFF_SIZE;i++)
	{
		write_serial(buf[i]);
//		printHexByte(buf[i]);
	}
	pthread_mutex_unlock(&serial_write_lock);
}
/*********************************************************************/
// if an input switch is changed, update the record for that switch
// and if an output applies to that input, change the output
// and record the event in llist - each input can be assigned any output
UCHAR monitor_input_task(int test)
{
//	I_DATA *itp;
//	I_DATA **itpp = &itp;
	O_DATA *otp;
	O_DATA **otpp = &otp;

	int status = -1;
	int bank, index;
	UCHAR result,result2, mask, onoff;
	int i, rc, flag;
	int input_port;
	char tempx[20];

//	TODO: what if more than 1 button is pushed in same bank or diff bank at same time?
/*
	while(TRUE)
	{
		uSleep(1,0);
		printf("%02x %02x %02x\r\n", InPortByteD(), InPortByteE(), InPortByteF());
		if(shutdown_all)
			return 0;
	}
*/
	pthread_mutex_lock( &io_mem_lock);

/*
	inportstatus[0] =  ~InPortByteA();
	inportstatus[1] =  ~InPortByteB();
	inportstatus[2] =  ~InPortByteC();

	inportstatus[3] =  ~InPortByteD();
	inportstatus[4] =  ~InPortByteE();
	inportstatus[5] =  ~InPortByteF();
*/

	inportstatus[0] =  ~InPortByteD();
	inportstatus[1] =  ~InPortByteE();
	inportstatus[2] =  ~InPortByteF();

	pthread_mutex_unlock( &io_mem_lock);

//	printf("monitor\r\n");

	while(TRUE)
	{
		for(bank = NUM_PORTS;bank < NUM_PORTS+3;bank++)
		{
			usleep(_500MS);
			usleep(_500MS);
			pthread_mutex_lock( &io_mem_lock);
			result = InPortByte(bank);
			//printf("%d: %02x ",bank-3, result);
			//if(bank == 5)
				//printf("\r\n");
			pthread_mutex_unlock( &io_mem_lock);
			result = ~result;

			if(result != inportstatus[bank])  
			{
				mask = result ^ inportstatus[bank];
				//printf("result: %02x\r\n",result);
				//printf("enter 1: %02x %d\r\n\r\n",inportstatus[bank],bank);
/*
				if(mask > 0x80)
				{
					myprintf1("bad mask\0");
					//printf("bad mask 1 %02x\r\n",mask);
					continue;
				}
*/
				index = mask2int(mask);

				if((mask & result) == mask)
				{
					onoff = ON;
				}
				else
				{
					onoff = OFF;
				}
				// since each card only has 20 ports then the 1st 2 port (A & B on 
				// 1st card; D & E on 2nd) are 8-bit and the 3rd is only 4-bits, 
				// so we have to translate the inportstatus array, representing 
				// 3 byts of each 2 (3x8x2 = 48) to one of the 40 actual bits as index
				for(i = 20;i < 40;i++)
				{
					if(real_banks[i].bank == bank && real_banks[i].index == index)
					{
						index = real_banks[i].i;
					}
				}
				// go threw the list of valid input ports and find the output port 
				// assigned to it - if input_type != 0 then call the function  
				// according to the input_type as one of the commands in cmd_array
				for(i = 0;i < max_ips;i++)
				{
//					printf("%d %d %d\r\n",ip[i].port,ip[i].input,index);
					if(ip[i].input == index)
					{
						if(ip[i].function == 0)
						{
							ollist_find_data(ip[i].port,&otp,&oll);
							set_output(otp, onoff);
//							sprintf(tempx,"+%d %d", index, otp->port);
//							myprintf1(tempx);
						}else
						{
							if(onoff == ON)
							{
								add_msg_queue(ip[i].function);
	//							sprintf(tempx,"-%d %d", ip[i].input, ip[i].function);
	//							myprintf1(tempx);
	//							printf("msg queue: %d %d\r\n",ip[i].input, ip[i].function);
							}
						}
					}
				}
				inportstatus[bank] = result;
//				printf("leave 1: %02x\r\n\r\n",inportstatus[bank]);
			}
		}
		uSleep(0,TIME_DELAY/200);
//		uSleep(0,TIME_DELAY/2);
		if(shutdown_all)
		{
//				printf("done mon input tasks\r\n"); 
//				myprintf1("done mon input");
				//printString2("done mon");
			return 0;
		}
	}
	return 1;
}

/*********************************************************************/
// this is used so that inputs can be changed programatically by software
// as if an input button or switch were changed
int change_input(int index, int onoff)
{
	int bank;
	UCHAR mask = 1;
	UCHAR state = 0;

	bank = real_banks[index].bank;
	index = real_banks[index].index;

	mask <<= index;

	if(onoff)
	{
		fake_inportstatus2[bank] |= mask;
	}
	else
	{
		fake_inportstatus2[bank] &= ~mask;
	}
}
/*********************************************************************/
// do the same thing as monitor_input_tasks but with the fake arrays
// set by change_inputs()
UCHAR monitor_fake_input_task(int test)
{
//	I_DATA *itp;
//	I_DATA **itpp = &itp;
	O_DATA *otp;
	O_DATA **otpp = &otp;

	int status = -1;
	int bank, index;
	UCHAR result, mask, onoff;
	int i, rc, flag;

//	TODO: what if more than 1 button is pushed in same bank or diff bank at same time?

	for(i = 0;i < 6;i++)
	{
		fake_inportstatus1[i] = 0;
		fake_inportstatus2[i] = 0;
	}

	while(TRUE)
	{
		for(bank = 0;bank < NUM_PORTS;bank++)
		{
			result = fake_inportstatus2[bank];

			if(result != fake_inportstatus1[bank])
			{
				mask = result ^ fake_inportstatus1[bank];
//				printf("enter 2: %02x\r\n",fake_inportstatus1[bank]);

//				printf("mask: %02x\r\n",mask);
				if(mask > 0x80)
				{
					myprintf1("bad mask\0");
					printf("bad mask 1 %02x\r\n",mask);
					continue;
				}
				index = mask2int(mask);

				if((mask & result) == mask)
				{
					onoff = ON;
	 				fake_inportstatus2[bank] |= mask;
				}
				else
				{
					onoff = OFF;
	 				fake_inportstatus2[bank] &= ~mask;
				}

				for(i = 0;i < 40;i++)
				{
					if(real_banks[i].bank == bank && real_banks[i].index == index)
					{
						index = real_banks[i].i;
					}
				}

				for(i = 0;i < max_ips;i++)
				{
//					printf("%d %d %d\r\n",ip[i].port,ip[i].input,index);
					if(ip[i].input == index)
					{
						if(ip[i].function == 0)
						{
							ollist_find_data(ip[i].port,&otp,&oll);
							set_output(otp, onoff);
						}else 
						{
							add_msg_queue(ip[i].function);
						}
					}
				}
 				fake_inportstatus1[bank] = fake_inportstatus2[bank];

//				printf("leave 2: %02x %02x\r\n\r\n",fake_inportstatus1[bank],fake_inportstatus2[bank]);
			}
		}
		uSleep(0,TIME_DELAY/200);
		uSleep(0,TIME_DELAY/2);
		if(shutdown_all)
		{
//				printf("done mon fake input tasks\r\n");
//				myprintf1("done mon input");
			return 0;
		}
	}
	return 1;
}

/*********************************************************************/
// pass in the index into the total list of outputs
// since each card only has 20 outputs, the last 4 bits of PORT C & F are ignored
// index 0->19 = PORTA(0:7)->PORTC(0:4)
// index 24->39 = PORTD(0:7)->PORTF(0:4)
int change_output(int index, int onoff)
{
	int bank;
	char tempx[10];

	//printf("change output: %d %d\r\n",index,onoff);
	pthread_mutex_lock( &io_mem_lock);

	bank = real_banks[index].bank;
	index = real_banks[index].index;
	//printf("bank: %d\r\n",bank);
	switch(bank)
	{
/*
		case 0:
			OutPortA(onoff, index);			  // 0-7
			break;
		case 1:
			OutPortB(onoff, index);			  // 0-7
			break;
		case 2:
			OutPortC(onoff, index);			  // 0-3
			break;
*/
		case 0:
			OutPortD(onoff, index);			  // 0-7
			break;
		case 1:
			OutPortE(onoff, index);			  // 0-7
			break;
		case 2:
			OutPortF(onoff, index);			  // 0-3
			break;
		default:
			break;
	}
	pthread_mutex_unlock(&io_mem_lock);
//	printf("change output: %d %d\r\n",index,onoff);

//	sprintf(tempx,"%d %d %d", bank, index, onoff);
//	myprintf1(tempx);

	return index;
}
#endif
/*********************************************************************/
// this happens 10x a second
UCHAR timer2_task(int test)
{
	int i,j;
	char tempx[20];
	static int prev_light_sensor_value;
	static int time_lapse;
	O_DATA *otp;
	O_DATA **otpp = &otp;
	O_DATA *otp2;
	O_DATA **otpp2 = &otp2;
	int bank = 0;
	UCHAR mask;
	int index = 0;

	time_lapse = 0;

//	printf("timer 2 task\n");
	while(TRUE)
	{
		if(shutdown_all)
			return 0;
		usleep(_500MS);
	}

	while(TRUE)
	{
		uSleep(1,0);

		if(++trunning_seconds > 59)
		{
			trunning_seconds = 0;
			//printf("trunning minutes: %d\r\n",trunning_minutes);
			if(++trunning_minutes > 59)
			{
				trunning_minutes = 0;
				trunning_hours++;
			}
		}
		time_lapse = 0;

		if(test_sock())
		{
//			send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx, SERVER_UPTIME);
			sprintf(tempx,"%dh %dm %ds ",trunning_hours, trunning_minutes, trunning_seconds);
		}
		if(shutdown_all)
		{
//			printf("done timer2 task\r\n");
			//printString2("done time2");
			return 0;
		}
	}
	return 1;
}
/*********************************************************************/
// this happens once a second
UCHAR timer_task(int test)
{
	int i;
	char time_buffer[100];
	char tempx[100];
//	UCHAR tempx[SERIAL_BUFF_SIZE];
	int index = 3;
	int bank = 0;
	int fp;
	UCHAR mask;
//	time_t curtime2;
	struct timeval mtv;
	O_DATA *otp;
	O_DATA **otpp = &otp;
	O_DATA *otp2;
	O_DATA **otpp2 = &otp2;
//	static int test_ctr = 0;
//	static int test_ctr2 = 0;
//	static UCHAR nav = NAV_DOWN;
	UCHAR cmd = 0x21;
	UCHAR ucbuff[6];
	char recip[4];
	int temp;
	int msg_len;
	int ret;
    int msgflg = IPC_CREAT | 0666;
	struct msgbuf {
		long mtype;
		char mtext[50];
	};
	struct msgbuf msg;
	time_t t;
	int msgtype = 1;

	memset(write_serial_buffer,0,SERIAL_BUFF_SIZE);
	memset(time_buffer,0,sizeof(time_buffer));
	temp = 0;
	for(i = 0;i < SERIAL_BUFF_SIZE;i++)
	{
		write_serial_buffer[i] = cmd;
		if(++cmd > 0x7e)
			cmd = 0x21;
	}
	i = 0;
	cmd = 0x21;
	index = 3;
//printf("timer task\n");
	while(TRUE)
	{
/*
		if(client_table[index].socket > 0)
		{
			msg.mtype = msgtype;

			time(&t);
			snprintf(msg.mtext, sizeof(msg.mtext), "a message at %s", ctime(&t));

			if (msgsnd(client_table[index].qid, (void *) &msg, sizeof(msg.mtext), IPC_NOWAIT) == -1) 
			{
				perror("msgsnd error");
				exit(EXIT_FAILURE);
			}
			printf("sent: %s\n", msg.mtext);				
		}
*/
		uSleep(5,0);

		if(shutdown_all)
		{
			return 0;
		}
	}

	while(TRUE)
	{
		if(client_table[index].socket > 0)
		{
			msg_len = get_msg(client_table[index].socket);
			ret = recv_tcp(client_table[index].socket, &time_buffer[0],msg_len+1,1);
			strncpy(recip,&time_buffer[1],3);
	//		printf("recip: %s\n",recip);
	//		printf("ret: %d\n",ret);
			cmd = time_buffer[0];
	//		printf("cmd: %d\n",cmd);
			if(ret > 200)
				break;
			memset(tempx,0,sizeof(tempx));
			for(i = 5;i < ret+1;i++)
			{
				tempx[i-5] = time_buffer[i];
	//			printf("%02x ",tempx[i]);
			}
			printf("%s\n",tempx);
		}
		if(shutdown_all)
		{
			return 0;
		}
		uSleep(0,TIME_DELAY/16);
	}

	while(TRUE)
	{
		if(test_sock())
		{
			windows_client_sock = get_client_sock("149");
			if(windows_client_sock > 0)
			{
//				printf("windows client socket: %d\n",windows_client_sock);
				for(i = 0;i < 12;i++)
				{
					if(client_table[i].socket > 0)
					{
						memset(time_buffer,0,sizeof(time_buffer));
						sprintf(time_buffer,"%d %s %d", i, client_table[i].ip, client_table[i].socket);
						send_msgb(windows_client_sock, strlen(time_buffer)*2,time_buffer,SEND_CLIENT_LIST);
						printf("%s %s\n",client_table[i].label, time_buffer);
					}
					uSleep(5,TIME_DELAY/4);
//					uSleep(1,TIME_DELAY/3);
					//printf("\n");
				}
				uSleep(2,0);
			}	
		}
		if(shutdown_all)
		{
//			printf("done timer_task\r\n");
			//printString2("done timer");
			return 0;
		}
		uSleep(0,TIME_DELAY/2);		// 1/2 sec
		uSleep(2,0);
	}
	return 1;
}
/*********************************************************************/
UCHAR WinClReadTask(int test)
{
	int i,j,k,rc,msg_len;
	char tempx[200];
	char msg_buf[200];
	UCHAR cmd;
	int win_client_to_client_sock = -1;
	struct msgbuf 
	{
		long mtype;
		char mtext[50];
	};
	struct msgbuf msg;
	time_t t;
	int msgtype = 1;

	msg_len = -1;
	k = 0;
	//printf("win cl read task\n");
	while(TRUE)
	{
		if(windows_client_sock > 0)
		{
//			either one of these will work 
//			printf("msg from windows client %d\n",client_table[i].socket);
			printf("msg from windows client %d\n",windows_client_sock);
//			rc = recv_tcp(windows_client_sock, msg_buf,16,1);
//			rc = recv_tcp(windows_client_sock, msg_buf+16,16,1);
//			for(i = 0;i < 32;i++)
//				printf("%02x ",msg_buf[i]);
//			msg_len = get_msgb(client_table[i].socket);
			msg_len = get_msgb(windows_client_sock);
			printf("1: msg_len: %d\n",msg_len);
//			windows_client_sock = sd;

			int rc = recv_tcp(windows_client_sock, &msg_buf[0], msg_len, 1);
			cmd = msg_buf[0];
			//printf("cmd: %d\n",cmd);
			print_cmd(cmd);

			memset(tempx,0,sizeof(tempx));
			k = 0;
			for(j = 2;j < msg_len+2;j+=2)
				tempx[k++] = msg_buf[j];

			for(j = 0;j < msg_len;j++)
				printf("%02x ",tempx[j]);
			if(cmd == DISCONNECT)
			{
				close(windows_client_sock);
				client_table[windows_client_sock].socket = -1;
				windows_client_sock = -1;
				shutdown_all = 1;
				exit(1);	// for some reason this locks up if I just do a break
				break;
			}
			win_client_to_client_sock = tempx[0];
			// get the socket of the client to send msg to 
			// the windows client sends as the 1st byte the index into 
			// the client_table[] array 
			uSleep(0,TIME_DELAY/8);
			printf("sock: %d %s\n",client_table[win_client_to_client_sock].socket, client_table[win_client_to_client_sock].label);
//			send_msg(client_table[win_client_to_client_sock].socket,strlen(tempx),(UCHAR*)tempx,cmd);

			msg.mtype = msgtype;

			//time(&t);
//			snprintf(msg.mtext, sizeof(msg.mtext), "a message at %s", ctime(&t));

			memset(msg.mtext,0,sizeof(msg.mtext));
			msg.mtext[0] = cmd;
			strcpy(msg.mtext + 1,"from win client \0");

			//printf("%s\n",msg.mtext+1);

			if (msgsnd(client_table[win_client_to_client_sock].qid, (void *) &msg, sizeof(msg.mtext), IPC_NOWAIT) == -1) 
			{
				perror("msgsnd error");
				exit(EXIT_FAILURE);
			}
			printf("sent: %s\n", msg.mtext);				
			printf("\n");
		}
		//printf("*");
		uSleep(0,TIME_DELAY/8);

		if(shutdown_all)
		{
			printf("shutting down WinClReadTask\n");
			return 0;
		}
	}
}
/*********************************************************************/
UCHAR WinClWriteTask(int test)
{
	int i,j,k,rc;
	char tempx[200];
	char msg_buf[200];

	//printf("win cl write task\n");
	while(TRUE)
	{
/*
		if(windows_client_sock > 0)
		{
			printf("msg from windows client %s\n",client_table[i].label);
			if(client_table[i].socket > 0)
			{
				memset(tempx,0,sizeof(tempx));
				sprintf(tempx,"%d %s %d", i, client_table[i].ip, client_table[i].socket);
				send_msgb(windows_client_sock, strlen(tempx)*2,tempx,SEND_CLIENT_LIST);
				printf("%s %s\n",client_table[i].label, tempx);
			}
		}
*/
		uSleep(1,TIME_DELAY/16);
		//printf("#");

		if(shutdown_all)
		{
			return 0;
		}
	}
}
/*********************************************************************/
UCHAR ReadTask1(int test)
{
	int index = 3;			// 148
	char tempx[100];
	int msg_len;
	int ret;
	UCHAR cmd;
	int i;
	char recip[4];

	//printf("starting read task2\n");

	while(TRUE)
	{
		if(client_table[index].socket > 0)
		{
			printf("read task 1\n");
			msg_len = get_msg(client_table[index].socket);
			ret = recv_tcp(client_table[index].socket, &tempx[0],msg_len+1,1);
			strncpy(recip,&tempx[1],3);
			printf("recip: %s\n",recip);
			printf("ret: %d\n",ret);
			cmd = tempx[0];
			//printf("cmd: %d\n",cmd);
			print_cmd(cmd);
			if(ret > 200)
				break;
			memmove(tempx,tempx+5,ret-5);
/*
			for(i = 0;i < ret;i++)
			{
				printf("%02x ",tempx[i]);
			}
*/
			printf("%s\n",tempx);
		}
		//printf("#");
		if(shutdown_all)
		{
			return 0;
		}
		uSleep(0,TIME_DELAY/16);
	}
}
/*********************************************************************/
UCHAR SendTask1(int test)
{
	int index = 3;		// 148
	int msg_len;
	char msg_buf[100];
	char recip[4];
	int i;
	UCHAR cmd = 0;
	struct msgbuf 
	{
		long mtype;
		char mtext[50];
	};
	struct msgbuf msg;
	time_t t;
	int msgtype = 1;

	uSleep(0,TIME_DELAY/16);
	//printf("starting send task1\n");

	i = 0;
 	while(TRUE)
	{
		if(client_table[index].socket > 0)
		{
//			client_table[i].qid = msgget(client_table[i].qkey, IPC_CREAT | 0666);
			if (msgrcv(client_table[index].qid, (void *) &msg, sizeof(msg.mtext), msgtype,
			MSG_NOERROR | IPC_NOWAIT) == -1) 
			{
				if (errno != ENOMSG) 
				{
					perror("msgrcv");
					exit(EXIT_FAILURE);
				}
				//printf("No message available for msgrcv()\n");
			} else
			{
				printf("message received: %d %s %d\n", msg.mtext[0], msg.mtext+1,errno);
				cmd = 1;
				perror(msg_buf);
			}

//			if(errno != ENOMSG)
			if(cmd)
			{
				printf("sending msg\n");
				memset(msg_buf,0,sizeof(msg_buf));
				sprintf(msg_buf,"148_ABCDEF145JKLM %d\0",i);
				msg_buf[3] = 0;
				send_msg(client_table[index].socket, 22,(UCHAR*)msg_buf,SEND_MSG);
				i++;
				cmd = 0;
			}
//			uSleep(0,TIME_DELAY/4);
//			uSleep(1,0);
		}
		if(shutdown_all)
		{
			return 0;
		}
		//printf("^");

		uSleep(0,TIME_DELAY/16);
//		to_sock = get_client_sock(recip);
	}
}
/*********************************************************************/
UCHAR ReadTask2(int test)
{
	int index = _145;
	char tempx[100];
	int msg_len;
	int ret;
	UCHAR cmd;
	int i;
	int temp;

	while(TRUE)
	{
		if(client_table[index].socket > 0)
		{
//			printf("read task 2\n");
			msg_len = get_msg(client_table[index].socket);
			ret = recv_tcp(client_table[index].socket, &tempx[0],msg_len+1,1);
/*
			strncpy(recip,&tempx[1],3);
			printf("recip: %s\n",recip);
*/
			printf("ret: %d\n",ret);
			cmd = tempx[0];
			//printf("cmd: %d\n",cmd);
			print_cmd(cmd);
			if(cmd == SHUTDOWN_IOBOX || cmd == REBOOT_IOBOX)
			{
				printf("shutdown or reboot\n");
				close(client_table[index].socket);
				client_table[index].socket = -1;
				break;
			}
			if(ret > 200)
				break;
			//printf("%02x %02x %02x\n",tempx[0],tempx[1],tempx[2]);
			for(i = 0;i < ret;i++)
			{
				printf("%02x ",tempx[i]);
			}
			temp = (int)(tempx[3] << 4);
			temp |= (int)tempx[2];
			printf("\n%d\n",temp);
			memmove(tempx,tempx+5,ret-5);
			printf("%s\n\n",tempx);
		}
		//printf("&");

		if(shutdown_all)
		{
			return 0;
		}
		uSleep(0,TIME_DELAY/16);
	}
}
/*********************************************************************/
UCHAR SendTask2(int test)
{
	int index = _145;
	int msg_len;
	char msg_buf[100];
	char recip[4];
	int i;
	UCHAR cmd = 0;
	int pass = 0;
    int msgflg = IPC_CREAT | 0666;
	struct msgbuf {
		long mtype;
		char mtext[50];
	};
	struct msgbuf msg;
//	time_t t;
	int msgtype = 1;

	i = 0;
	while(TRUE)
	{
		if(client_table[index].socket > 0)
		{
//			client_table[i].qid = msgget(client_table[i].qkey, IPC_CREAT | 0666);
			if (msgrcv(client_table[index].qid, (void *) &msg, sizeof(msg.mtext), msgtype,
			MSG_NOERROR | IPC_NOWAIT) == -1) 
			{
				if (errno != ENOMSG) 
				{
					perror("msgrcv");
					exit(EXIT_FAILURE);
				}
				//printf("No message available for msgrcv()\n");
			} else
			{
				cmd = (UCHAR)msg.mtext[0];
				memmove(msg.mtext,msg.mtext+1,49);
				printf("message received: %s %d\n", msg.mtext,errno);
				//printf("cmd: %d\n",cmd);
				print_cmd(cmd);
				perror(msg_buf);
				pass = 1;
			}

//			if(errno != ENOMSG)
			if(pass)
			{
				printf("sending msg\n");
				send_msg(client_table[index].socket, strlen(msg.mtext), (UCHAR*)msg.mtext,cmd);
//				if(cmd == SHUTDOWN_IOBOX || cmd == REBOOT_IOBOX)
//					client_table[index].socket = -1;
				pass = 0;
			}
			uSleep(0,TIME_DELAY/16);

		} else uSleep(0,TIME_DELAY/16);

		if(shutdown_all)
		{
			return 0;
		}
		
//		to_sock = get_client_sock(recip);
	}
}
/*********************************************************************/
// serial receive task
UCHAR serial_recv_task(int test)
{
	serial_rec = 0;
	int i = 0;
	int j = 0;
	int k = 0;
	int s;
	UCHAR ch, ch2;
	UCHAR cmd;
	UCHAR low_byte, high_byte;
	int rpm, mph, indoor_temp;
	int fd;
	char errmsg[20];
	char tempx[30];
	UCHAR send_buffer[SERIAL_BUFF_SIZE];
	UINT temp;
	UCHAR ucbuff[6];
	int brights = 0;
	int running_lights = 0;
	for(i = 0;i < RAW_DATA_ARRAY_SIZE;i++)
		raw_data_array[i] = 60;
	raw_data_ptr = 0;
	temp = 0;

//printf("serial task\r\n");

	memset(errmsg,0,20);
	//usleep(_5SEC);	// delay 5 seconds because it hangs when trying
					// to send to STM32 when starting up


	while(TRUE)
	{
		uSleep(5,0);
//		printf("serial: %d\r\n",temp++);
		if(shutdown_all)
			return 0;
	}

	if(fd = init_serial() < 0)
	{
		myprintf1("can't open comm port 1\0");
//		printf("can't open comm port 1");
		//return 0;
	}
/*
	if(fd = init_serial2() < 0)
	{
		myprintf1("can't open comm port 2\0");
//		printf("can't open comm port 2");
	}
*/
	usleep(100000);

	//printString2("trying to open comm3");
#ifdef TS_7800
	if(fd = init_serial3(ps.baudrate3) < 0)
//	if(fd = init_serial3(2) < 0)
	{
		myprintf1("can't open comm port 3\0");
//		printf("can't open comm port 3");
		//printString2("can't open comm3");
	}else
	{
		//printString2("comm3 open");
		//printString3("comm3 open");
	}
#endif
	ch = ch2 = 0x7e;
//	myprintf1("serial ports opened\0");

//	red_led(0);
//	green_led(0);

	s = pthread_setcancelstate(PTHREAD_CANCEL_ENABLE, NULL);

	// send msg to STM32 so it can play a set of beeps & blips
/*	
	usleep(200000);
	send_serialother(SERVER_UP,&send_buffer[0]);
	usleep(200000);
	send_serialother(SERVER_UP,&send_buffer[0]);
	usleep(200000);
	tempx[0] = 0;
	send_serialother(SEND_DEBUG_MSG,(UCHAR *)tempx);
//	send_serialother(GET_VERSION,(UCHAR *)tempx);

	tempx[0] = 1;
	send_serialother(SEND_DEBUG_MSG,(UCHAR *)tempx);
*/

	while(TRUE)
	{
		pthread_mutex_lock( &serial_read_lock); 

		// serial data is SERIAL_BUFF_SIZE preceded by an 0xFF

		i = 0;
		do{
			ch = read_serial_buffer[i] = read_serial(errmsg);
			i++;
		}while(read_serial_buffer[i-1] != '+' && i <= 30);

		pthread_mutex_unlock(&serial_read_lock);

		ch = read_serial_buffer[0];

		switch(ch)
		{
			case 'A':
				add_msg_queue(ALL_LIGHTS_ON);
//				sprintf(tempx,"all lights on\0");
			break;
			case 'B':
				add_msg_queue(ALL_LIGHTS_OFF);
//				sprintf(tempx,"all lights off\0");
			break;
			case 'C':
				add_msg_queue(ALL_NORTH_ON);
//				sprintf(tempx,"all north on\0");
			break;
			case 'D':
				add_msg_queue(ALL_SOUTH_ON);
//				sprintf(tempx,"all lights off\0");
			break;
			case '0':
				add_msg_queue(ALL_MIDDLE_ON);
			break;
			case '1':
				add_msg_queue(ALL_NORTH_OFF);
			break;
			case '2':
				add_msg_queue(ALL_SOUTH_OFF);
			break;
			case '3':
				add_msg_queue(ALL_MIDDLE_OFF);
			break;
			case '4':
				add_msg_queue(ALL_EAST_ON);
			break;
			case '5':
				add_msg_queue(ALL_EAST_ON);
			break;
			case '6':
				add_msg_queue(ALL_EAST_OFF);
			break;
			case '7':
				add_msg_queue(ALL_WEST_ON);
			break;
			case '8':
				add_msg_queue(ALL_WEST_OFF);
			break;
			case '9':
//				add_msg_queue(
			break;
			case '*':
//				add_msg_queue(
			break;
			case '#':
//				add_msg_queue(
			break;
			default:
			break;
		}
		cmd = read_serial_buffer[0];

/*
		memset(tempx,0,sizeof(tempx));
		memccpy(tempx,read_serial_buffer,'+',30);
		if(test_sock())
			send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx, SEND_MSG);
*/
/*
		write_serial(0xFF);
		for(i = 0;i < 20;i++)
		{
			write_serial(tempx[i]);
			usleep(1000);
		}
*/
		if(cmd >= KP_1 && cmd <= KP_0)
		{
			switch(cmd)
			{
				case KP_0:
					scroll_up();
					break;

				case KP_1:
					break;

				case KP_2:
					break;

				case KP_3:
					break;

				case KP_4:
					break;

				case KP_5:
					break;

				case KP_6:
					break;

				case KP_7:
					break;

				case KP_8:
					break;

				case KP_9:
					break;

				case KP_A:
					break;

				case KP_B:
					break;

				case KP_C:
					break;

				case KP_D:
					break;

				case KP_POUND:
					send_serialother(SERVER_UP, (UCHAR *)&tempx[0]);
					myprintf1("server up sent");
					break;

				case KP_AST:
					send_serialother(SERVER_UP, (UCHAR *)&tempx[0]);
					myprintf1("server up sent");
					break;

				default:
					break;
			}
		}

		if(cmd >= NAV_UP && cmd <= NAV_CLOSE || cmd == KP_POUND)
		{
			sprintf(tempx,"nav cmd: %d",cmd);
//			printString2(tempx);
			if(test_sock())
//				send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx,SEND_MSG);
usleep(100);
			else		// if client not connected then these keys can 
						// have alt. functions
			{
				switch(cmd)
				{
					case NAV_UP:	// 'A' key
						break;
					case NAV_DOWN:	// 'B' key
						break;
					case NAV_SIDE:	// '*' key
						break;
					case NAV_CLICK:	// 'C' key
						break;
					case NAV_CLOSE:	// 'D' key
						break;
					case KP_POUND:
						break;
				}
			}
		}else 

		if(cmd == GET_CONFIG2)
		{
			temp = read_serial_buffer[2];
			temp <<= 8;
			temp |= read_serial_buffer[1];
			sprintf(tempx,"fan on: %.1f\0", convertF(temp));
			//printString2(tempx);

			temp = read_serial_buffer[4];
			temp <<= 8;
			temp |= read_serial_buffer[3];
			sprintf(tempx,"fan off: %.1f\0", convertF(temp));
			//printString2(tempx);

			temp = read_serial_buffer[6];
			temp <<= 8;
			temp |= read_serial_buffer[5];
			sprintf(tempx,"engine temp limit: %.1f\0", convertF(temp));
			//printString2(tempx);
		}else

		if(shutdown_all)
		{
			close_serial();
			close_serial2();
#ifdef TS_7800			
			close_serial3();
#endif
			return 0;
		}

	}
	return 1;
}

// client calls 'connect' to get accept call below to stop
// blocking and return sd2 socket descriptor

static int sock_open;

/*********************************************************************/
// match up the ip address (146,145, etc) to the entry in the table 
// and return the socket id if it's logged on, else return -1
int get_client_sock(char *recip)
{
	int i;
	int sock = -1;

	for(i = 0;i < MAX_CLIENTS;i++)
	{
		if(strncmp(client_table[i].ip,recip,3) == 0)
		{
			//printf("%s %d\n",client_table[i].label,(sock = client_table[i].socket));
			sock = client_table[i].socket;
			return sock;
		}
	}
	return sock;
}
/*********************************************************************/
char *get_client_recip(int sock)
{
	int i;

	for(i = 0;i < MAX_CLIENTS;i++)
	{
		if(client_table[i].socket == sock)
		{
			return client_table[i].ip;
		}
	}
	return (char *)0;
}

/*********************************************************************/
// task to monitor for a client requesting a connection
UCHAR tcp_monitor_task(int test)
{
	int ret = -1;
	struct timeval tv;
	int client_socket[MAX_CLIENTS];
	int master_socket, new_socket, activity, sd, addrlen;
	struct  hostent   *ptrh;				  /* pointer to a host table entry */
	struct  protoent  *ptrp;				  /* pointer to a protocol table entry */
	struct  sockaddr_in address;			  /* structure to hold server's address */
//	struct  sockaddr_in cad;				  /* structure to hold client's address */
	int     port;							  /* protocol port number */
//	int     alen;							  /* length of address */
	UCHAR cmd;
	int max_sd;
	port = PROTOPORT;
	sock_open = 0;
	tv.tv_sec = 2;
	tv.tv_usec = 50000;
	int s,i,j,k;
	fd_set readfds;
	int opt = TRUE;
	char tempx[100];
	UCHAR msg_buf[20];
	int valread;
	int msg_len;
	char address_string[20];
	char ch;
	char recip[4];
	int to_sock;
	
	assign_client_table();
	
#if 0

	memset(client_table,0,sizeof(CLIENT_TABLE1)*MAX_CLIENTS);

	strcpy(client_table[_149].ip,"149\0");
	strcpy(client_table[_149].label,"Second_Windows7\0");
	client_table[_149].socket = -1;
	client_table[_149].type = WINDOWS_CLIENT;
	client_table[_149].qkey = 1235;
	client_table[_149].qid = 0;

	strcpy(client_table[_159].ip,"159\0");
	strcpy(client_table[_159].label,"Win7-x64\0");
	client_table[_159].socket = -1;
	client_table[_159].type = WINDOWS_CLIENT;
	client_table[_159].qkey = 1236;
	client_table[_159].qid = 0;

	strcpy(client_table[_145].ip,"145\0");
	strcpy(client_table[_145].label,"TS_client1\0");
	client_table[_145].socket = -1;
	client_table[_145].type = TS_CLIENT;
	client_table[_145].qkey = 1238;
	client_table[_145].qid = 0;

	strcpy(client_table[_147].ip,"147\0");
	strcpy(client_table[_147].label,"TS_client2\0");
	client_table[_147].socket = -1;
	client_table[_147].type = TS_CLIENT;
	client_table[_147].qkey = 1239;
	client_table[_147].qid = 0;

	strcpy(client_table[_150].ip,"150\0");
	strcpy(client_table[_150].label,"TS_client3\0");
	client_table[_150].socket = -1;
	client_table[_150].type = TS_CLIENT;
	client_table[_150].qkey = 1240;
	client_table[_150].qid = 0;

	strcpy(client_table[_151].ip,"151\0");
	strcpy(client_table[_151].label,"TS_client4\0");
	client_table[_151].socket = -1;
	client_table[_151].type = TS_CLIENT;
	client_table[_151].qkey = 1241;
	client_table[_151].qid = 0;

	strcpy(client_table[_152].ip,"152\0");
	strcpy(client_table[_152].label,"TS_client5\0");
	client_table[_152].socket = -1;
	client_table[_152].type = TS_CLIENT;
	client_table[_152].qkey = 1242;
	client_table[_152].qid = 0;

	strcpy(client_table[_153].ip,"153\0");
	strcpy(client_table[_153].label,"TS_client6\0");
	client_table[_153].socket = -1;
	client_table[_153].type = TS_CLIENT;
	client_table[_153].qkey = 1243;
	client_table[_153].qid = 0;

	strcpy(client_table[_154].ip,"154\0");
	strcpy(client_table[_154].label,"TS_client7\0");
	client_table[_154].socket = -1;
	client_table[_154].type = TS_CLIENT;
	client_table[_154].qkey = 1244;
	client_table[_154].qid = 0;

#endif

// 156 & 157 are the next 2 avail - 155 is the firstpi.local

	s = pthread_setcancelstate(PTHREAD_CANCEL_ENABLE,NULL);
//	if(s != 0)
//		handle_err_en(s, "pthread_setcancelstate");
//		printf("setcancelstate\r\n");

	memset((char  *)&address,0,sizeof(address));	  /* clear sockaddr structure   */
	address.sin_family = AF_INET;				  /* set family to Internet     */
	address.sin_addr.s_addr = INADDR_ANY;		  /* set the local IP address */

	address.sin_port = htons((u_short)port);
	
	global_socket = -1;
	for (i = 0; i < MAX_CLIENTS; i++)
	{
		client_socket[i] = 0;
	}

// getprotobyname doesn't work on TS-7200 because there's no /etc/protocols file
// so just use '6'
#ifndef MAKE_TARGET
	if ( ((int)(ptrp = getprotobyname("tcp"))) == 0)
	{
		//printString2("cannot map tcp to protocol number");
		printf("cannot map tcp to protocol number\r\n");
//			exit (1);
	}
	master_socket = socket (PF_INET, SOCK_STREAM, ptrp->p_proto);

// getprotobyname doesn't work on TS-7200 because there's no /etc/protocols file
// so just use '6' as the tcp protocol number
#else
	master_socket = socket (PF_INET, SOCK_STREAM, 6);
#endif
	if (master_socket < 0)
	{
		//printString2("socket creation failed");
		printf("socket creation failed\r\n");
//			exit(1);
	}
//printf("master socket: %d\n",master_socket);
	//set master socket to allow multiple connections ,
	//this is just a good habit, it will work without this
	if( setsockopt(master_socket, SOL_SOCKET, SO_REUSEADDR, (char *)&opt,
		sizeof(opt)) < 0 )
	{
		perror("setsockopt");
		exit(EXIT_FAILURE);
	}

/* Bind a local address to the socket */
	if (bind(master_socket, (struct sockaddr *)&address, sizeof (address)) < 0)
	{
		//printString2("bind failed");
		printf("bind failed\r\n");
//		exit(1);
	}
//printf("bind ok\n");
/* Specify a size of request queue */
	if (listen(master_socket, QLEN) < 0)
	{
		//printString2("listen failed");
		printf("listen failed\r\n");
//			exit(1);
	}
//printf("done w/ listen\n");
//	alen = sizeof(cad);
	addrlen = sizeof(address);

/* Main server loop - accept and handle requests */
	k = 0;
	while (TRUE)
	{
//		printf("test %d ",k++);
		//clear the socket set
		FD_ZERO(&readfds);
	
		//add master socket to set
		FD_SET(master_socket, &readfds);
		max_sd = master_socket;
			
		//add child sockets to set
		for ( i = 0 ; i < MAX_CLIENTS ; i++)
		{
			//socket descriptor
//			sd = client_socket[i];
			sd = client_table[i].socket;
				
			//if valid socket descriptor then add to read list
			if(sd > 0)
				FD_SET( sd , &readfds);
				
			//highest file descriptor number, need it for the select function
			if(sd > max_sd)
				max_sd = sd;
		}
	
		//wait for an activity on one of the sockets , timeout is NULL ,
		//so wait indefinitely
//		printf("wait for activity\n");
		activity = select( max_sd + 1 , &readfds , NULL , NULL , NULL);
	
		if ((activity < 0) && (errno!=EINTR))
		{
			printf("select error");
		}
			
		//If something happened on the master socket ,
		//then its an incoming connection
		if (FD_ISSET(master_socket, &readfds))
		{
			if ((new_socket = accept(master_socket,
					(struct sockaddr *)&address, (socklen_t*)&addrlen))<0)
			{
				perror("accept");
				exit(EXIT_FAILURE);
			}
			memset(address_string, 0, sizeof(address_string));
			strncpy(address_string,inet_ntoa(address.sin_addr),sizeof(address_string));
			//inform user of socket number - used in send and receive commands
			printf("New connection , socket: %d ip: %s port: %d\n",
					new_socket, address_string, ntohs(address.sin_port));

			i = j = 0;
			//printf("%s\n",address_string);

			while(i < 3 && j < 17 && address_string[j] != 0)
			{
				if(address_string[j] == '.')
					i++;
//				printf("%c",address_string[j]);
				j++;
			}
			memset(tempx,0,sizeof(tempx));
			strncpy(tempx,&address_string[j],3);
//			printf("%s\n",tempx);

			// later on we want to have more than 1 windows client be able
			// to log in
			for(i = 0;i < MAX_CLIENTS;i++)
			{
				if(strncmp(client_table[i].ip,tempx,3) == 0)
				{
					client_table[i].socket = new_socket;
					//printf("index: %d type: %d label: %s socket: %d\n",i, client_table[i].type, client_table[i].label,client_table[i].socket);

					if(windows_client_sock < 0)
					{
						windows_client_sock = get_client_sock("149");
//						if(windows_client_sock > 0)
//							printf("windows client logged in %d\n",windows_client_sock);
					}
					//printf("%d\n",windows_client_sock);
					
					if(windows_client_sock > 0)
					{
						memset(tempx,0,sizeof(tempx));
						sprintf(tempx,"%d %s %d", i, client_table[i].ip, client_table[i].socket);
						send_msgb(windows_client_sock, strlen(tempx)*2,tempx,SEND_CLIENT_LIST);
					}
					client_table[i].qid = msgget(client_table[i].qkey, IPC_CREAT | 0666);
/*
					if(client_table[i].type == TS_CLIENT)
					if(client_table[i].type == OTHER)
					{
						usleep(10);
					}
*/
					usleep(10000);
				}
			}
			//add new socket to array of sockets
/*
			for (i = 0; i < MAX_CLIENTS; i++)
			{
				//if position is empty
				if( client_socket[i] == 0 )
				if(client_table[i] < 0)
				{
					client_socket[i] = new_socket;
					//printf("\nAdding to list of sockets as %d\n" , i);
					break;
				}
			}
*/
		}
		//else its some IO operation on some other socket
		for (i = 0; i < MAX_CLIENTS; i++)
		{
			sd = client_socket[i];

			if (FD_ISSET( sd , &readfds))
			{
				uSleep(0,TIME_DELAY/16);
				// this never happens when doing a SEND_MSG but when it does a SHUTDOWN 
				// it's because we were not closing the socket
				printf("*socket: %d ",sd);
				client_socket[i] = 0;
			}
		}
		uSleep(0,TIME_DELAY/8);
		if(shutdown_all)
		{
//				printf("tcp task closing\r\n");
			return 0;
		}
/*
		if (setsockopt (global_socket, SOL_SOCKET, SO_RCVTIMEO, (char *)&tv, sizeof(struct timeval)) < 0)
			return -2;
		if (setsockopt (global_socket, SOL_SOCKET, SO_SNDTIMEO, (char *)&tv, sizeof(struct timeval)) < 0)
			return -3;
*/
	}
	return 1;
}

/*********************************************************************/
int test_sock(void)
{
//	return sock_open;
return 1;
}

/*********************************************************************/
void close_tcp(void)
{
	if(sock_open)
	{
		sock_open = 0;
//		printf("closing socket...\r\n");
		close(global_socket);
//		printf("socket closed!\r\n");
		global_socket = -1;
	}else
	{
		myprintf1("socket already closed\0");
//		printf("socket already closed\r\n");
	}
}

/**********************************************************************/
void *work_routine(void *arg)
{
	int *my_id=(int *)arg;
	int i;
	UCHAR pattern = 0;
	int not_done=1;
	i = not_done;
	shutdown_all = 0;

	pthread_mutex_lock(&threads_ready_lock);
	threads_ready_count++;
	if (threads_ready_count == NUM_TASKS)
	{
/* I was the last thread to become ready.  Tell the rest. */
		pthread_cond_broadcast(&threads_ready);
	}
	else
	{
/* At least one thread isn't ready.  Wait. */
		while (threads_ready_count != NUM_TASKS)
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
/*********************************************************************/
void add_msg_queue(UCHAR cmd)
{
//	while(msg_queue_ptr >= MSG_QUEUE_SIZE);
	pthread_mutex_lock(&msg_queue_lock);
	if(msg_queue_ptr < MSG_QUEUE_SIZE)
	{
		msg_queue_ptr++;
		msg_queue[msg_queue_ptr] = cmd;
	}
	pthread_mutex_unlock(&msg_queue_lock);
//	printf("add: %d %x\r\n",msg_queue_ptr,cmd);
}
/*********************************************************************/
UCHAR get_msg_queue(void)
{
	UCHAR cmd;
	pthread_mutex_lock(&msg_queue_lock);
	if(msg_queue_ptr > 0)
	{
		cmd = msg_queue[msg_queue_ptr];
		msg_queue_ptr--;
	}else cmd = 0;
	pthread_mutex_unlock(&msg_queue_lock);
//	if(cmd != 0)
//		printf("get: %d %x\r\n",msg_queue_ptr,cmd);
	return cmd;
}
/*********************************************************************/
/*
	char ip[4];
	int socket;
	UCHAR msg_type;
	int msg_len;
	char message[20];
*/
/*********************************************************************/
UCHAR basic_controls_task(int test)
{
	int i,j;
	UCHAR onoff;
	O_DATA *otp;
	O_DATA **otpp = &otp;
	int rc;
	int index;
	size_t isize;
	size_t osize;
	char errmsg[50];
	UCHAR cmd;
	char tempx[SERIAL_BUFF_SIZE];

	memset(msg_queue,0,sizeof(msg_queue));
	msg_queue_ptr = 0;
//printf("starting basic_controls_task\n");
	while(TRUE)
	{
		// wait for a new cmd to arrive in the msg_queue
		do{
			cmd = get_msg_queue();
			usleep(_5MS);
		}while(cmd == 0 && shutdown_all == 0);

		usleep(_5MS);

		switch(cmd)
		{
			case  ALL_LIGHTS_ON:
				index = NORTHEAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				//printf("%s\r\n",otp->label);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = NORTHWEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				//printf("%s\r\n",otp->label);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = EAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				//printf("%s\r\n",otp->label);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = SOUTHEAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				//printf("%s\r\n",otp->label);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = SOUTHWEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				//printf("%s\r\n",otp->label);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = WEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				//printf("%s\r\n",otp->label);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = MIDDLE_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				//printf("%s\r\n",otp->label);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = DESK_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				//printf("%s\r\n",otp->label);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				break;

			case ALL_LIGHTS_OFF:
				index = NORTHEAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = NORTHWEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = EAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = SOUTHEAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = SOUTHWEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = WEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = MIDDLE_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = DESK_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				break;

			case ALL_NORTH_ON:
				index = NORTHEAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
//				ollist_insert_data(index,&oll,otp);
				set_output(otp,1);
				usleep(_100MS);
				index = NORTHWEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				break;

			case ALL_SOUTH_ON:
				index = SOUTHEAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = SOUTHWEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				break;

			case ALL_MIDDLE_ON:
				index = EAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = WEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = MIDDLE_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				break;

			case ALL_NORTH_OFF:
				index = NORTHEAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = NORTHWEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				break;

			case ALL_SOUTH_OFF:
				index = SOUTHEAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = SOUTHWEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				break;

			case ALL_MIDDLE_OFF:
				index = EAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = WEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = MIDDLE_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				break;
			
			case ALL_EAST_ON:
				index = EAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = SOUTHEAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = NORTHEAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				break;

			case ALL_EAST_OFF:
				index = EAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = SOUTHEAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = NORTHEAST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				break;

			case ALL_WEST_ON:
				index = WEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = SOUTHWEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				index = NORTHWEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 1;
				set_output(otp,1);
				usleep(_100MS);
				break;

			case ALL_WEST_OFF:

				index = WEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = SOUTHWEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
				index = NORTHWEST_LIGHT;
				rc = ollist_find_data(index,otpp,&oll);
				otp->onoff = 0;
				set_output(otp,0);
				usleep(_100MS);
/*
				printf("test:\r\n");		// testing all outputs
				for(i = 0;i < 20;i++)
				{
					change_output(i,1);
					usleep(_200MS);
				}
					usleep(_500MS);
				for(i = 0;i < 20;i++)
				{
					change_output(i,0);
					usleep(_200MS);
				}

				for(i = 0;i < 5;i++);		// this doesn't work for some reason
				{
					rc = ollist_find_data(index,otpp,&oll);
					otp->onoff = 1;
					set_output(otp,1);
					usleep(_500MS);
//					printf("on: %d\r\n",index);
					index++;
				}
				usleep(_1SEC);
				index = EAST_LIGHT;
				for(i = 0;i < 5;i++);
				{
					rc = ollist_find_data(index,otpp,&oll);
					otp->onoff = 0;
					set_output(otp,0);
					usleep(_500MS);
//					printf("off: %d\r\n",index);
					index++;
				}

				printf("done\r\n");
*/				
				break;

			case SHUTDOWN_IOBOX:
				send_serialother(SERVER_DOWN,(UCHAR *)tempx);
				myprintf1("shutdown iobox\0");
				shutdown_all = 1;
				reboot_on_exit = 3;
				break;

			case REBOOT_IOBOX:
				send_serialother(SERVER_DOWN,(UCHAR *)tempx);
				myprintf1("reboot iobox\0");
				shutdown_all = 1;
				reboot_on_exit = 2;
				break;

			case UPLOAD_NEW:
				//send_serialother(SERVER_DOWN,(UCHAR *)tempx);
				shutdown_all = 1;
				reboot_on_exit = 4;
				break;

			case UPLOAD_NEW_PARAM:
				//send_serialother(SERVER_DOWN,(UCHAR *)tempx);
				shutdown_all = 1;
				reboot_on_exit = 5;
				break;

			case SHELL_AND_RENAME:
				//send_serialother(SERVER_DOWN,(UCHAR *)tempx);
				shutdown_all = 1;
				reboot_on_exit = 6;
				break;

			case UPLOAD_OTHER:
				shutdown_all = 1;
				reboot_on_exit = 1;
//				printf("upload other\r\n");
				break;

			default:
				break;
		}	// end of switch

		if(shutdown_all == 1)
		{
			return 0;
		}

	}
	return 1;
}

int avg_raw_data(int prev_data)
{
	int i;
	int temp_data = 0;
/*
	int high, low;
	high = 0;
	low = 500;
	int high_index, low_index;
	for(i = 0;i < RAW_DATA_ARRAY_SIZE;i++)
	{
		if(raw_data_array[i] > high)
		{
			high = raw_data_array[i];
			high_index = i;
		}
		if(raw_data_array[i] < low)
		{
			low = raw_data_array[i];
			low_index = i;
		}
	}
	raw_data_array[low_index] = raw_data_array[high_index] = prev_data;
*/
	for(i = 0;i < RAW_DATA_ARRAY_SIZE;i++)
	{
		temp_data += raw_data_array[i];
	}
	temp_data /= RAW_DATA_ARRAY_SIZE;
	return temp_data;
}

float convertF(int raw_data)
{
	float T_F, T_celcius;
	int ret;
	if ((raw_data & 0x100) != 0)
	{
		raw_data = - (((~raw_data)+1) & 0xff); /* take 2's comp */
	}
	T_celcius = raw_data * 0.5;
	T_F = (T_celcius * 1.8) + 32;
	ret = (int)T_F;
	return ret;	// returns 257 -> -67
}

