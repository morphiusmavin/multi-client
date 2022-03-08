#if 1
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
#include "../cmd_types.h"
#include "../mytypes.h"
#include "ioports.h"
#include "serial_io.h"
#include "queue/ollist_threads_rw.h"
#include "tasks.h"
//#include "cs_client/config_file.h"
#include "lcd_func.h"
#define TOGGLE_OTP otp->onoff = (otp->onoff == 1?0:1)

pthread_cond_t       threads_ready;
pthread_mutex_t     tcp_write_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     tcp_read_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     io_mem_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     serial_write_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     serial_read_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     serial_write_lock2=PTHREAD_MUTEX_INITIALIZER;
//pthread_mutex_t     serial_read_lock2=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     msg_queue_lock=PTHREAD_MUTEX_INITIALIZER;
int total_count;

UCHAR (*fptr[NUM_TASKS])(int) = { get_host_cmd_task, monitor_input_task, 
monitor_fake_input_task, timer_task, timer2_task, serial_recv_task, 
tcp_monitor_task, basic_controls_task};

int threads_ready_count=0;
pthread_cond_t    threads_ready=PTHREAD_COND_INITIALIZER;
pthread_mutex_t   threads_ready_lock=PTHREAD_MUTEX_INITIALIZER;
static UCHAR check_inputs(int index, int test);
//extern CMD_STRUCT cmd_array[NO_CMDS];
ollist_t oll;
PARAM_STRUCT ps;

//extern pthread_t serial_thread;	// workaround for closing serial task

//extern int olLoadConfig(char *filename, ollist_t *oll, size_t size, char *errmsg);

static UCHAR read_serial_buffer[SERIAL_BUFF_SIZE];
static UCHAR write_serial_buffer[SERIAL_BUFF_SIZE];
static int no_serial_buff;
char password[PASSWORD_SIZE];
CLIENT_TABLE2 client_table[MAX_CLIENTS];

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
static int msg_queue_ptr;

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
				//printf("done mon input tasks\r\n"); 
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
				//printf("done mon fake input tasks\r\n");
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

	while(TRUE)
	{
		if(shutdown_all)
		{
			//printf("done timer2\n");
			return 0;
		}
		uSleep(0,TIME_DELAY/2);
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
			sprintf(tempx,"%dh %dm %ds ",trunning_hours, trunning_minutes, trunning_seconds);
			send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx, SERVER_UPTIME);
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
	UCHAR time_buffer[20];
	int index = 0;
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
	int temp;
/*
	memset(write_serial_buffer,0,SERIAL_BUFF_SIZE);
	temp = 0;
	for(i = 0;i < SERIAL_BUFF_SIZE;i++)
	{
		write_serial_buffer[i] = cmd;
		if(++cmd > 0x7e)
			cmd = 0x21;
	}
	i = 0;
	
	while(TRUE)
	{
		if(shutdown_all)
		{
			//printf("done timer_task\r\n");
			//printString2("done timer");
			return 0;
		}
		uSleep(0,TIME_DELAY/2);
	}
*/
	i = 0;
	while(TRUE)
	{
		memset(time_buffer,0,sizeof(time_buffer));
		sprintf(time_buffer,"____ABCDEF145JM %d\0",i);
		time_buffer[0] = _145;
		time_buffer[1] = (UCHAR)i;
		time_buffer[2] = (UCHAR)(i >> 4);

		send_msg(22,(UCHAR*)time_buffer,SEND_MSG);
		i++;
		//printf("%d ",i);
		uSleep(5,0);
		uSleep(0,TIME_DELAY/6);
		if(shutdown_all)
		{
//			printf("done timer_task\r\n");
			//printString2("done timer");
			return 0;
		}
	}
	return 1;
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
		uSleep(0,TIME_DELAY/2);
		//printf("serial: %d\r\n",temp++);
		if(shutdown_all)
		{
			//printf("done serial\n");
			return 0;
		}
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
				send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx,SEND_MSG);
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

static struct  sockaddr_in sad;				  /* structure to hold server's address  */
static int sock_open;
static struct timeval tv;
/*********************************************************************/
int tcp_connect(void)
{
	static struct  sockaddr_in sad;  /* structure to hold server's address  */
	struct  hostent  *ptrh;   /* pointer to a host table entry       */
	struct  protoent *ptrp;   /* point to a protocol table entry     */
	static struct timeval tv;
	int port;
	char host[20] = "192.168.88.146";

	memset((char *)&sad,0,sizeof(sad));  /* clear sockaddr structure */
	sad.sin_family = AF_INET;            /* set family to Internet   */
	sad.sin_addr.s_addr = INADDR_ANY;
	port = PROTOPORT;

	if (port > 0) sad.sin_port = htons((u_short)port);
	else
	{
		printf("bad port number %d\n", port);
	}
	ptrh = gethostbyname(host);
	if( ((char *)ptrh) == NULL)
	{
		printf("invalid host:  %s\n", host);
	}

	memcpy(&sad.sin_addr, ptrh->h_addr, ptrh->h_length);

	 /* Map TCP transport protocol name to protocol number */
	//getprotobyname doesn't work on TS-7200 because there's no /etc/protocols file
/*
	if ( ((int)(ptrp = getprotobyname("tcp"))) == 0)
	{
		printf("cannot map \"tcp\" to protocol number");
	}
*/
	global_socket = socket (PF_INET, SOCK_STREAM, 6);

	if (global_socket < 0)
	{
		printf("socket creation failed\n");
	}

	//printf("host = %s global_socket = %d\n",host,global_socket);
	//printf("trying to connect...\n");

	if (connect(global_socket, (struct sockaddr *)&sad, sizeof(sad)) < 0)
	{
		printf("connect failed\n");
	}
	else
	{
		printf("connected\n");

//#ifndef MAKE_TARGET
		if (setsockopt (global_socket, SOL_SOCKET, SO_RCVTIMEO, (char *)&tv, sizeof(struct timeval)) < 0)
			return -2;
		if (setsockopt (global_socket, SOL_SOCKET, SO_SNDTIMEO, (char *)&tv, sizeof(struct timeval)) < 0)
			return -3;
//#endif
		sock_open = 1;
		return global_socket;
	}
}

/*********************************************************************/
// task to monitor for a client requesting a connection
UCHAR tcp_monitor_task(int test)
{
	int s;
	s = pthread_setcancelstate(PTHREAD_CANCEL_ENABLE,NULL);
	assign_client_table();

	while (TRUE)
	{
		if(shutdown_all)
		{
			//close_tcp();
			//printf("done tcp_mon\r\n");
			return 0;
		}
		else
		{
			if(test_sock() != 1)
			{
				if(tcp_connect() < 0)
				{
					printf("can't connect\n");
				}
			}
		}
		uSleep(0,TIME_DELAY/2);
	}
	return 1;
}

/*********************************************************************/
int test_sock(void)
{
	return sock_open;
}

void set_sock(int open)
{
	sock_open = open;
}
/*********************************************************************/
void close_tcp(void)
{
	if(sock_open)
	{
		sock_open = 0;
		//printf("closing socket...\r\n");
		close(global_socket);
		//printf("socket closed!\r\n");
		global_socket = -1;
	}else
	{
		//printf("socket already closed\r\n");
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
				//send_serialother(SERVER_DOWN,(UCHAR *)tempx);
				snprintf(tempx, strlen(tempx), "shutdown iobox");
				send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx,SHUTDOWN_IOBOX);
				uSleep(1,0);
				//printf("shutdown iobox\n");
				shutdown_all = 1;
				reboot_on_exit = 3;
				close_tcp();
				break;

			case REBOOT_IOBOX:
				//send_serialother(SERVER_DOWN,(UCHAR *)tempx);
				snprintf(tempx, strlen(tempx), "reboot iobox");
				send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx,REBOOT_IOBOX);
				uSleep(1,0);
				//printf("reboot iobox\n");
				shutdown_all = 1;
				reboot_on_exit = 2;
				close_tcp();
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
			//printf("stopping basic_controls_task\n");
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

