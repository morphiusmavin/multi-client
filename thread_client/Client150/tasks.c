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
#include <sys/ipc.h>
#include <sys/msg.h>
#include "../../cmd_types.h"
#include "../../mytypes.h"
#include "../ioports.h"
#include "../serial_io.h"
#include "../queue/ollist_threads_rw.h"
#include "../queue/cllist_threads_rw.h"
#include "../tasks.h"
//#include "cs_client/config_file.h"
#define TOGGLE_OTP otp->onoff = (otp->onoff == 1?0:1)

pthread_mutex_t     io_mem_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     serial_write_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     serial_read_lock=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     serial_write_lock2=PTHREAD_MUTEX_INITIALIZER;
//pthread_mutex_t     serial_read_lock2=PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t     msg_queue_lock=PTHREAD_MUTEX_INITIALIZER;
int total_count;

static UCHAR check_inputs(int index, int test);
//extern CMD_STRUCT cmd_array[NO_CMDS];
ollist_t oll;
cllist_t cll;
PARAM_STRUCT ps;

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

static COUNTDOWN count_down[COUNTDOWN_SIZE];
int curr_countdown_size;

#define ON 1
#define OFF 0
enum input_types
{
	STARTER_INPUT,				// 0 	STARTER & COOLINGFAN don't go to the tray
	COOLINGFAN_INPUT,			// 1 	first card starts @ 280h
	BRAKE_INPUT,				// 2
	SEAT_SWITCH,				// 3
	DOOR_SWITCH,				// 4
	WIPER_HOME,					// 5  	red wire from the wiper motor
	WIPER1_INPUT,				// 6  	switch to turn on slow wipers
	WIPER2_INPUT,				// 7 	switch to turn on fast wipers
	WIPER_OFF_INPUT,			// 8
	BACKUP_INPUT,				// 9
	HEADLAMP_INPUT,				// 10 	this starts the DB-15 under the dash
	RUNNING_LIGHTS_INPUT,		// 11 	HEADLAMP_INPUT -> BRIGHTS_INPUT comes
	MOMENTARY_INPUT,			// 12 	from the AM turn signal switch
	LEFTBLINKER_INPUT,			// 13 	20 is the start of the 2nd card @ 300h
	RIGHTBLINKER_INPUT,			// 14
	BRIGHTS_INPUT,				// 15
	ESTOP_INPUT = 27,			// 27	emergency stop
	ROCKER1_INPUT,				// 28
	ROCKER2_INPUT,				// 29
	ROCKER3_INPUT,				// 30
	ROCKER4_INPUT,				// 31
	ROCKER5_INPUT,				// 32
}INPUT_TYPES;

// * 25 not working only because wire from DB-15 not going to cable on dip on TS-7800

enum output_types
{
	COOP1_LIGHTa,
	COOP1_HEATERa,
	COOP2_LIGHTa,
	COOP2_HEATERa,
	OUTDOOR_LIGHT1a,
	OUTDOOR_LIGHT2a,
	UNUSED150_1a,
	UNUSED150_2a,
	TEST_OUTPUT10a,
	TEST_OUTPUT11a,
	UNUSED150_3a,
	UNUSED150_4a,
	UNUSED150_5a,
	UNUSED150_6a,
	UNUSED150_7a,
	UNUSED150_8a,
	UNUSED150_9a,
	UNUSED150_10a,
	TEST_OUTPUT12a,
	TEST_OUTPUT13a
	
}OUTPUT_TYPES;

int switch_status[10];

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
				return -1;
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
#ifndef USE_CARDS
	printf("not using cards\n");
	return;
#endif
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
#ifndef USE_CARDS
	printf("not using cards\n");

	while(TRUE)
	{
		uSleep(1,0);
		if(shutdown_all)
			return 0;
	}
#endif

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
								add_msg_queue(ip[i].function,0);
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

#ifndef USE_CARDS
	printf("not using cards\n");
	return;
#endif

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
#ifndef USE_CARDS
	printf("not using cards\n");

	while(TRUE)
	{
		uSleep(1,0);
		if(shutdown_all)
			return 0;
	}
#endif
while(TRUE)
	{
		uSleep(1,0);
		if(shutdown_all)
			return 0;
	}
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
							add_msg_queue(ip[i].function,0);
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

#ifndef USE_CARDS
	printf("not using cards\n");
	return 0;
#endif

	//printf("change output: %d\n",index);
	pthread_mutex_lock( &io_mem_lock);

	bank = real_banks[index].bank;
	index = real_banks[index].index;
	//printf("bank: %d index: %d\r\n",bank,index);
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
	//printf("change output: %d %d\r\n",index,onoff);

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

	trunning_days = trunning_hours = trunning_minutes = trunning_seconds = 0;

	while(TRUE)
	{
		uSleep(1,0);
		//printf("%d : %d -",trunning_minutes, trunning_seconds);
		if(++trunning_seconds > 59)
		{
			trunning_seconds = 0;
			if(++trunning_minutes > 59)
			{
				trunning_minutes = 0;
				if(++trunning_hours > 23)
				{
					trunning_hours = 0;
					trunning_days++;
				}
			}
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
void swap(COUNTDOWN* xp, COUNTDOWN* yp)
{
	COUNTDOWN temp = *xp;
	*xp = *yp;
	*yp = temp;
}
/*********************************************************************/
void remove_top_countdown()
{
	int i;
	
	for(i = 0;i < curr_countdown_size;i++)
	{
		memcpy(&count_down[i],&count_down[i+i],sizeof(COUNTDOWN));
	}
	curr_countdown_size--;
}
/*********************************************************************/
void sort_countdown(void)
{
	C_DATA *ctp;
	C_DATA **ctpp = &ctp;

	int i,j,k,n,min_idx;

	int hour, minute, second;
	time_t T = time(NULL);
	struct tm tm = *localtime(&T);
	COUNTDOWN *ct;
	COUNTDOWN tct;
	int current_seconds = tm.tm_hour * 3600 + tm.tm_min * 60 + tm.tm_sec;
	printf("curr sec: %d\n",current_seconds);
	k = 0;
	for(i = 0;i < 20;i++)
	{
		j = cllist_find_data(i, ctpp, &cll);
		if(ctp->port > -1)
		{
			count_down[k].port = ctp->port;
			count_down[k].hour = ctp->on_hour;
			count_down[k].minute = ctp->on_minute;
			count_down[k].second = ctp->on_second;
			count_down[k].onoff = 1;
			count_down[k].index = i;
			k++;
			count_down[k].port = ctp->port;
			count_down[k].hour = ctp->off_hour;
			count_down[k].minute = ctp->off_minute;
			count_down[k].second = ctp->off_second;
			count_down[k].onoff = 0;
			count_down[k].index = i;
			k++;
		}
		
	}
	curr_countdown_size = k;
	for(i = 0;i < curr_countdown_size;i++)
	{
		count_down[i].seconds_away = count_down[i].hour * 3600 + count_down[i].minute * 60 + count_down[i].second;
	}
/*
	printf("\n");
	for(i = 0;i < curr_countdown_size;i++)
	{
		printf("%d: %d %d %d %d %d %d\n",count_down[i].index, count_down[i].seconds_away, count_down[i].port, count_down[i].onoff,count_down[i].hour,count_down[i].minute,count_down[i].second);
	}
	printf("\n");
*/
	for (i = 0; i < curr_countdown_size - 1; i++) 		// do the sort
	{
		// Find the minimum element in unsorted array
		min_idx = i;
		for (j = i + 1; j < curr_countdown_size; j++)
			if (count_down[j].seconds_away < count_down[min_idx].seconds_away)
				min_idx = j;

		// Swap the found minimum element
		// with the first element
		swap(&count_down[min_idx], &count_down[i]);
	}
	for(i = 0;i < curr_countdown_size;i++)
	{
		// seconds_away > current_seconds then seconds_away is in the future
		if(count_down[i].seconds_away > current_seconds)	
			count_down[i].seconds_away -= current_seconds;

		else 
		{
			count_down[i].seconds_away = -1;
		}
	}
/*
	printf("\n");
	for(i = 0;i < curr_countdown_size;i++)
	{
		printf("%d: %d %d %d %d %d %d\n",count_down[i].index, count_down[i].seconds_away, count_down[i].port, count_down[i].onoff,count_down[i].hour,count_down[i].minute,count_down[i].second);
	}
*/
}
/*********************************************************************/
void display_sort()
{
	int i;
	printf("index\tsec away\tport\tonoff\thour\tmin\tsec\n");
	for(i = 0;i < curr_countdown_size;i++)
	{
		if(count_down[i].seconds_away > -1)
			printf("%d:\t%d\t\t%d\t%d\t%d\t%d\t%d\n",count_down[i].index, count_down[i].seconds_away, count_down[i].port, count_down[i].onoff,count_down[i].hour,count_down[i].minute,count_down[i].second);
	}
}
/*********************************************************************/
// this happens once a second
UCHAR timer_task(int test)
{
	int i,j;
	int onoff;
	time_t T;
	struct tm tm;

	memset(write_serial_buffer,0,SERIAL_BUFF_SIZE);

	uSleep(2,0);
	//printf("starting timer task\n");

	sort_countdown();

	while(TRUE)
	{
		uSleep(1,0);
		if(curr_countdown_size > 0)
		{
			for(i = 0;i < curr_countdown_size;i++)
			{
				//printf("%d ",count_down[i].seconds_away);
				if(count_down[i].seconds_away > -1)
				{
					if(--count_down[i].seconds_away == 0)
					{
						T = time(NULL);
						tm = *localtime(&T);
						printf("%02d:%02d:%02d\n", tm.tm_hour, tm.tm_min, tm.tm_sec);
						onoff = count_down[i].onoff;
						add_msg_queue(count_down[i].port+34, onoff);
						printf("%d\n",onoff);
						//remove_top_countdown();
					}
				}
			}
		}

		if(shutdown_all)
		{
			//printf("done timer_task\r\n");
			//printString2("done timer");
			//close_serial();
			//close_serial2();
			//close_serial3();
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
//		printf("can't open comm port 1");
		//return 0;
	}

	if(fd = init_serial2() < 0)
	{
//		printf("can't open comm port 2");
	}else printf("com port2: %d\n",fd);

	usleep(100000);

	//printString2("trying to open comm3");

//	if(fd = init_serial3(ps.baudrate3) < 0)
	if(fd = init_serial3(2) < 0)	// 2 = 19200
	{
//		printf("can't open comm port 3");
		//printString2("can't open comm3");
	}else
	{
		printf("com port3: %d\n",fd);
		//printString2("comm3 open");
		//printString3("comm3 open");
	}

	ch = ch2 = 0x7e;

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
				//add_msg_queue(ALL_LIGHTS_ON);
//				sprintf(tempx,"all lights on\0");
			break;
			case 'B':
				//add_msg_queue(ALL_LIGHTS_OFF);
//				sprintf(tempx,"all lights off\0");
			break;
			case 'C':
				//add_msg_queue(ALL_NORTH_ON);
//				sprintf(tempx,"all north on\0");
			break;
			case 'D':
				//add_msg_queue(ALL_SOUTH_ON);
//				sprintf(tempx,"all lights off\0");
			break;
			case '0':
				//add_msg_queue(ALL_MIDDLE_ON);
			break;
			case '1':
				//add_msg_queue(ALL_NORTH_OFF);
			break;
			case '2':
				//add_msg_queue(ALL_SOUTH_OFF);
			break;
			case '3':
				//add_msg_queue(ALL_MIDDLE_OFF);
			break;
			case '4':
				//add_msg_queue(ALL_EAST_ON);
			break;
			case '5':
				//add_msg_queue(ALL_EAST_ON);
			break;
			case '6':
				//add_msg_queue(ALL_EAST_OFF);
			break;
			case '7':
				//add_msg_queue(ALL_WEST_ON);
			break;
			case '8':
				//add_msg_queue(ALL_WEST_OFF);
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
/*********************************************************************/
void add_msg_queue(UCHAR cmd, UCHAR onoff)
{
	struct msgqbuf msg;
	int msgtype = 1;
	msg.mtype = msgtype;
	msg.mtext[0] = cmd;
	msg.mtext[1] = onoff;
	pthread_mutex_lock(&msg_queue_lock);

	if (msgsnd(basic_controls_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
	{
		// keep getting "Invalid Argument" - cause I didn't set the mtype
		perror("msgsnd error");
		exit(EXIT_FAILURE);
	}

	pthread_mutex_unlock(&msg_queue_lock);
//	printf("add: %d %x\r\n",msg_queue_ptr,cmd);
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
	struct msgqbuf msg;
	int msgtype = 1;

//	memset(msg_queue,0,sizeof(msg_queue));
	msg.mtype = msgtype;
	memset(switch_status,0,sizeof(switch_status));

/*
	for(i = 0;i < 10;i++)	// test the 2nd relay module
	{
		change_output(TEST_OUTPUT10+i,1);
		uSleep(1,0);
		change_output(TEST_OUTPUT10+i,0);
		uSleep(1,0);
	}
*/

	while(TRUE)
	{
		if (msgrcv(basic_controls_qid, (void *) &msg, sizeof(msg.mtext), msgtype,
//		MSG_NOERROR | IPC_NOWAIT) == -1) 
		MSG_NOERROR) == -1) 
		{
			if (errno != ENOMSG) 
			{
				perror("msgrcv");
				printf("msgrcv error\n");
				exit(EXIT_FAILURE);
			}
		}
		cmd = msg.mtext[0];
		onoff = msg.mtext[1];

		//printf("basic controls: ");
		//print_cmd(cmd);
		//usleep(_5MS);

		switch(cmd)
		{
			case  COOP1_LIGHT:
				change_output(COOP1_LIGHTa,onoff);
				usleep(_100MS);
				break;

			case COOP1_HEATER:
				change_output(COOP1_HEATERa,onoff);
				usleep(_100MS);
				break;

			case COOP2_LIGHT:
				change_output(COOP2_LIGHTa,onoff);
				usleep(_100MS);
				break;

			case COOP2_HEATER:
				change_output(COOP2_HEATERa,onoff);
				usleep(_100MS);
				break;

			case OUTDOOR_LIGHT1:
				change_output(OUTDOOR_LIGHT1a,onoff);
				usleep(_100MS);
				break;

			case OUTDOOR_LIGHT2:
				change_output(OUTDOOR_LIGHT2a,onoff);
				usleep(_100MS);
				break;

			case UNUSED150_1:
				change_output(UNUSED150_1a,onoff);
				usleep(_100MS);
				break;

			case UNUSED150_2:
				change_output(UNUSED150_2a,onoff);
				usleep(_100MS);
				break;

			case UNUSED150_3:
				change_output(UNUSED150_3a,onoff);
				usleep(_100MS);
				break;

			case UNUSED150_4:
				change_output(UNUSED150_4a,onoff);
				usleep(_100MS);
				break;

			case UNUSED150_5:
				change_output(UNUSED150_5a,onoff);
				usleep(_100MS);
				break;

			case UNUSED150_6:
				change_output(UNUSED150_6a,onoff);
				usleep(_100MS);
				break;

			case UNUSED150_7:
				change_output(UNUSED150_7a,onoff);
				usleep(_100MS);
				break;

			case UNUSED150_8:
				change_output(UNUSED150_8a,onoff);
				usleep(_100MS);
				break;

			case UNUSED150_9:
				change_output(UNUSED150_9a,onoff);
				usleep(_100MS);
				break;
			case UNUSED150_10:
				change_output(UNUSED150_10a,onoff);
				usleep(_100MS);
				break;

			case EXIT_TO_SHELL:
				snprintf(tempx, strlen(tempx), "exit to shell");
//				send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx,REBOOT_IOBOX, _SERVER);
				uSleep(0,TIME_DELAY/16);
				printf("tasks: exit to shell\n");
				shutdown_all = 1;
				reboot_on_exit = 1;
//				close_tcp();
				break;

			case REBOOT_IOBOX:
				snprintf(tempx, strlen(tempx), "reboot iobox");
//				send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx,REBOOT_IOBOX, _SERVER);
				uSleep(0,TIME_DELAY/16);
				printf("tasks: reboot iobox\n");
				shutdown_all = 1;
				reboot_on_exit = 2;
//				close_tcp();
				break;

			case SHUTDOWN_IOBOX:
				snprintf(tempx, strlen(tempx), "shutdown iobox");
//				send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx,SHUTDOWN_IOBOX,_SERVER);
				uSleep(0,TIME_DELAY/16);
				printf("tasks: shutdown iobox\n");
				shutdown_all = 1;
				reboot_on_exit = 3;
//				close_tcp();
				break;

			case SHELL_AND_RENAME:
				snprintf(tempx, strlen(tempx), "shell and rename");
//				send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx,SHUTDOWN_IOBOX,_SERVER);
				uSleep(0,TIME_DELAY/16);
				printf("tasks: shell and rename\n");
				shutdown_all = 1;
				reboot_on_exit = 6;
//				close_tcp();
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


