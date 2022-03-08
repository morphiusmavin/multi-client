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
#include "tasks.h"
#include "ioports.h"
#include "serial_io.h"
//#include "queue/illist_threads_rw.h"
#include "queue/ollist_threads_rw.h"
//#include "queue/rt_llist_threads_rw.h"
#include "cs_client/config_file.h"
#include "lcd_func.h"

extern pthread_mutex_t     tcp_read_lock;
extern pthread_mutex_t     tcp_write_lock;
int config_file_ok = -1;

#define TOGGLE_OTP otp->onoff = (otp->onoff == 1?0:1)

//extern illist_t ill;
extern ollist_t oll;

UCHAR msg_buf[UPLOAD_BUFF_SIZE];
UCHAR msg_buf2[UPLOAD_BUFF_SIZE/2];
extern PARAM_STRUCT ps;
extern char password[PASSWORD_SIZE];
int shutdown_all;
static UCHAR pre_preamble[] = {0xF8,0xF0,0xF0,0xF0,0xF0,0xF0,0xF0,0x00};

extern CMD_STRUCT cmd_array[];
#if 0
CMD_STRUCT cmd_array[NO_CMDS] =
{
	{	NON_CMD,"NON_CMD\0" },
	{	ALL_LIGHTS_ON,"ALL_LIGHTS_ON\0" },
	{	ALL_LIGHTS_OFF,"ALL_LIGHTS_OFF\0" },
	{	ALL_NORTH_ON,"ALL_NORTH_ON\0" },
	{	ALL_SOUTH_ON,"ALL_SOUTH_ON\0" },
	{	ALL_MIDDLE_ON,"ALL_MIDDLE_ON\0" },
	{	ALL_NORTH_OFF,"ALL_NORTH_OFF\0" },
	{	ALL_SOUTH_OFF,"ALL_SOUTH_OFF\0" },
	{	ALL_MIDDLE_OFF,"ALL_MIDDLE_OFF\0" },
	{	ALL_EAST_ON,"ALL_EAST_ON\0" },
	{	ALL_EAST_OFF,"ALL_EAST_OFF\0" },
	{	ALL_WEST_ON,"ALL_WEST_ON\0" },
	{	ALL_WEST_OFF,"ALL_WEST_OFF\0" },
	{	BLANK,"BLANK\0" },
	{	GET_TEMP4,"GET_TEMP4\0" },
	{	SHUTDOWN_IOBOX,"SHUTDOWN_IOBOX\0" },
	{	REBOOT_IOBOX,"REBOOT_IOBOX\0" },
	{	SET_TIME,"SET_TIME\0" },
	{	GET_TIME,"GET_TIME\0" },
	{	DISCONNECT,"DISCONNECT\0" },
	{	BAD_MSG,"BAD_MSG\0" },
	{	CURRENT_TIME,"CURRENT_TIME\0" },
	{	SET_PARAMS,"SET_PARAMS\0" },
	{	EXIT_PROGRAM,"EXIT_PROGRAM\0" },
	{	SERVER_UPTIME,"SERVER_UPTIME\0" },
	{	SEND_CONFIG,"SEND_CONFIG\0" },
	{	NAV_UP,"NAV_UP\0" },
	{	NAV_DOWN,"NAV_DOWN\0" },
	{	NAV_SIDE,"NAV_SIDE\0" },
	{	NAV_CLICK,"NAV_CLICK\0" },
	{	NAV_CLOSE,"NAV_CLOSE\0" },
	{	SEND_STATUS,"SEND_STATUS\0" },
	{	SERVER_UP,"SERVER_UP\0" },
	{	SERVER_DOWN,"SERVER_DOWN\0" },
	{	UPLOAD_NEW,"UPLOAD_NEW\0" },
	{	UPLOAD_OTHER,"UPLOAD_OTHER\0" },
	{	UPLOAD_NEW_PARAM,"UPLOAD_NEW_PARAM\0" },
	{	GET_VERSION,"GET_VERSION\0" },
	{	UPDATE_CONFIG,"UPDATE_CONFIG\0" },
	{	SEND_CLIENT_LIST,"SEND_CLIENT_LIST\0" },
	{	GET_CONFIG2,"GET_CONFIG2\0" },
	{	CLIENT_CONNECTED,"CLIENT_CONNECTED\0" },
	{	SERVER_CONNECTED,"SERVER_CONNECTED\0" },
	{	SET_KEYMODE,"SET_KEYMODE\0" },
	{	SHELL_AND_RENAME,"SHELL_AND_RENAME\0" },
	{	TEST_IO_PORT,"TEST_IO_PORT\0" },
	{	SEND_DEBUG_MSG,"SEND_DEBUG_MSG\0" },
	{	UPDATE_STATUS,"UPDATE_STATUS\0" },
	{	UPDATE_ALL,"UPDATE_ALL\0" },
	{	SEND_MSG,"SEND_MSG\0" }
};
#endif
void print_cmd(UCHAR cmd)
{
	char tempx[30];

	sprintf(tempx, "cmd: %d %s\0",cmd,cmd_array[cmd].cmd_str);
	printf("%s\r\n",cmd_array[cmd].cmd_str);
}

#endif

/*********************************************************************/
// task to get commands from the host
UCHAR get_host_cmd_task(int test)
{
//	I_DATA tempi1;
	O_DATA tempo1;
//	RI_DATA tempr1;
//	I_DATA *itp;
	O_DATA *otp;
	O_DATA **otpp = &otp;
	int rc = 0; 
	int rc1 = 0;
	UCHAR cmd = 0x21;
	UCHAR onoff;
	char errmsg[50];
	char filename[15];
	char *fptr;
	size_t size;
	int i;
	int j;
	int k;
	size_t isize;
	size_t osize;
	struct dirent **namelist;
//	DIR *d;
	struct dirent *dir;
	UCHAR tempx[SERIAL_BUFF_SIZE];
	char temp_time[5];
	char *pch;
	int fname_index;
	UCHAR uch_fname_index;
	UCHAR mask;
	time_t curtime2;
	time_t *pcurtime2 = &curtime2;
	int fp;
	off_t fsize;
	long cur_fsize;
	struct timeval mtv;
	struct tm t;
	struct tm *pt = &t;
	int msg_len;
	serial_recv_on = 1;
//	time_set = 0;
	shutdown_all = 0;
	char version[15] = "sched v1.03\0";
	UINT utemp;
//	UCHAR time_buffer[20];
	UCHAR write_serial_buffer[SERIAL_BUFF_SIZE];

	memset(write_serial_buffer, 0, SERIAL_BUFF_SIZE);
	// since each card only has 20 ports then the 1st 2 port access bytes
	// are 8-bit and the 3rd is only 4-bits, so we have to translate the
	// inportstatus array, representing 3 byts of each 2 (3x8x2 = 48) to
	// one of the 40 actual bits as index

	// the check_inputs & change_outputs functions
	// use the array to adjust from index to bank
	// since there are only 4 bits in banks 3 & 5

	for(i = 0;i < 20;i++)
	{
		real_banks[i].i = i;
		real_banks[i].bank = i/8;
		real_banks[i].index = i - real_banks[i].bank*8;
	}

	for(i = 20;i < 40;i++)
	{
		real_banks[i].i = i;
		real_banks[i].bank = (i+4)/8;
		real_banks[i].index = i - (real_banks[i].bank*8)+4;
	}

	memset(dat_names,0,sizeof(dat_names));

/*
	i = NUM_PORT_BITS;
	isize = sizeof(I_DATA);
	isize *= i;
*/
	i = NUM_PORT_BITS;
	//printf("no. port bits: %d\r\n",i);
	osize = sizeof(O_DATA);
	osize *= i;
	//printf("osize: %d\r\n",osize);

	trunning_hours = trunning_minutes = trunning_seconds = 0;

//	program_start_time = curtime();

	ollist_init(&oll);
	if(access(oFileName,F_OK) != -1)
	{
		config_file_ok = olLoadConfig(oFileName,&oll,osize,errmsg);
		if(rc > 0)
		{
//			myprintf1(errmsg);
			printf("%s\r\n",errmsg);
		}
	}
	init_ips();
//printf("starting...\n");
	same_msg = 0;
//	lcd_init();

// flash green and red led's to signal we are up (if LCD screen not attached)
#if 0
	for(i = 0;i < 5;i++)
	{
		red_led(1);
		usleep(10000);
		red_led(0);
		green_led(1);
		usleep(10000);
		green_led(0);
		red_led(1);
		usleep(10000);
		red_led(0);
		green_led(1);
		usleep(10000);
		green_led(0);
	}
#endif

	//myprintf1(version);
/*
	i = (UINT)msg_buf[2];	// bank 
	j = (UINT)msg_buf[4];	// port 
	k = (UINT)msg_buf[6];	// onoff
*
	printf("starting io test\r\n");

	for(j = 0;j < 20;j++)
	{
		for(k = 0;k < 2;k++)
		{
			change_output(j, k);
			usleep(100000);
			printf("%d %d\r\n",j,k);
		}
	}
	printf("done testing io\r\n");
*/	
/*
	while(TRUE)
	{
		uSleep(1,0);
	}
*/
	while(TRUE)
	{
		cmd = 0;
		if(test_sock() == 1)
//		if(1)
		{
			memset(msg_buf,0,sizeof(msg_buf));
			//printf("wait for msg_len\n");
//			msg_len = get_msg();
			//printf("msg_len: %d\n",msg_len);
//			printHexByte(msg_len);
			if(msg_len < 0)
			{
//				printf("bad msg\r\n");
				cmd = BAD_MSG;
				usleep(10000);
			}else
			{
//				rc = recv_tcp(&msg_buf[0],msg_len+1,1);
				//printf("rc: %d\n",rc);
				cmd = msg_buf[0];
				for(i = 1;i < rc;i++)
					tempx[i-1] = msg_buf[i];
			}

			if(cmd > 0)
			{
//				sprintf(tempx, "cmd: %d %s\0",cmd,cmd_array[cmd].cmd_str);
				printf("msg to svr: %s\r\n",cmd_array[cmd].cmd_str);
//				if(cmd < LCD_TEST_MODE)
//					myprintf1(cmd_array[cmd].cmd_str);
			}

			if(cmd > 0)
			{
				rc = 0;
				
				switch(cmd)
				{
					case ALL_LIGHTS_ON:
					case ALL_LIGHTS_OFF:
					case ALL_NORTH_ON:
					case ALL_SOUTH_ON:
					case ALL_MIDDLE_ON:
					case ALL_NORTH_OFF:
					case ALL_SOUTH_OFF:
					case ALL_MIDDLE_OFF:
					case ALL_EAST_ON:
					case ALL_EAST_OFF:
					case ALL_WEST_ON:
					case ALL_WEST_OFF:
					case SHUTDOWN_IOBOX:
					case REBOOT_IOBOX:
					case UPLOAD_NEW:
					case UPLOAD_NEW_PARAM:
					case SHELL_AND_RENAME:
					case UPLOAD_OTHER:
						//printf("sending que: %02x\r\n",cmd);
						memset(tempx,0,sizeof(tempx));
						//send_serialother(cmd,(UCHAR *)tempx);
						add_msg_queue(cmd);
						break;
					default:
						break;
				}

 				switch(cmd)
				{
					case SEND_MSG:
						for(i = 0;i < msg_len;i++)
							printf("%c",tempx[i]);
						printf("\n");
						break;

					case SET_PARAMS:
						send_param_msg();
						break;

					case SET_TIME:
						curtime2 = 0L;
						j = 0;
						memset(tempx,0,sizeof(tempx));

//						for(i = 2;i < msg_len;i+=2)
//							memcpy((void*)&tempx[j++],(char*)&msg_buf[i],1);
						for(i = 0;i < msg_len/2+2;i++)
						{
							tempx[i] = msg_buf2[i];
//							write_serial2(tempx[i]);
						}
						tempx[msg_len/2-2] = 'M';
						memset(temp_time,0,sizeof(temp_time));
						i = 0;
						pch = &tempx[0];

						while(*(pch++) != '/' && i < msg_len)
						{
							i++;
//							printf("%c",*pch);
						}
						memcpy(&temp_time[0],&tempx[0],i);
						i = atoi(temp_time);
//						printf("mon: %d\r\n",i - 1);
						pt->tm_mon = i - 1;
						i = 0;

						while(*(pch++) != '/' && i < msg_len)
						{
							i++;
//							printf("%c",*pch);
						}
						memset(temp_time,0,sizeof(temp_time));
						memcpy(temp_time,pch-i-1,i);
//						printf("%s\n",temp_time);
						i = atoi(temp_time);
						pt->tm_mday = i;
//						printf("day: %d\r\n",i);
				//		return 0;

						i = 0;
						while(*(pch++) != ' ' && i < msg_len)
						{
							i++;
//							printf("%c\r\n",*pch);
						}

						memset(temp_time,0,sizeof(temp_time));
						memcpy(temp_time,pch-3,2);
						i = atoi(temp_time);
						i += 100;
						pt->tm_year = i;
//						printf("year: %d\r\n",i-100);
				//		return 0;
						i = 0;

						while(*(pch++) != ':' && i < msg_len)
							i++;
						memset(temp_time,0,sizeof(temp_time));
						memcpy(temp_time,pch-i-1,i);
//						printf("%s \n",temp_time);
						i = atoi(temp_time);
						pt->tm_hour = i;
//						printf("hour: %d\r\n",i);
				//		return 0;

						i = 0;
						while(*(pch++) != ':' && i < msg_len)
							i++;
						memset(temp_time,0,sizeof(temp_time));
						memcpy(temp_time,pch-3,2);
//						printf("%s \n",temp_time);
						i = atoi(temp_time);
						pt->tm_min = i;
//						printf("min: %d\r\n",i);

						i = 0;
						while(*(pch++) != ' ' && i < msg_len)
							i++;
						memset(temp_time,0,sizeof(temp_time));
						memcpy(temp_time,pch-3,2);
//						printf("%s \n",temp_time);
						i = atoi(temp_time);
						pt->tm_sec = i;
//						printf("sec: %d\r\n",i);
//						printf("%c %x\n",*pch,*pch);
						if(*pch == 'P')
						{
//							printf("PM\n");
							pt->tm_hour += 12;
						}

						curtime2 = mktime(pt);
						stime(pcurtime2);
/*
						gettimeofday(&mtv, NULL);
						curtime2 = mtv.tv_sec;
						strftime(tempx,30,"%m-%d-%Y %T\0",localtime(&curtime2));
						printf("%s\n",tempx);
*/
//						time_set = 1;
//#endif
						break;

					case GET_TIME:
						gettimeofday(&mtv, NULL);
						curtime2 = mtv.tv_sec;
						strftime(tempx,30,"%m-%d-%Y %T\0",localtime(&curtime2));
						myprintf1(tempx);
//						send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx,GET_TIME);
						break;

					case BAD_MSG:
//						shutdown_all = 1;
//						myprintf1("bad msg");
						break;

					case DISCONNECT:
						if(test_sock() > 0)
						{
							close_tcp();
							myprintf1("disconnected\0");
						}
						break;

					case UPDATE_CONFIG:
						utemp = (UINT)msg_buf[3];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[2];
						ps.rpm_mph_update_rate = utemp;

						utemp = (UINT)msg_buf[5];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[4];
						ps.fpga_xmit_rate = utemp;

						utemp = (UINT)msg_buf[7];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[6];
						ps.high_rev_limit = utemp;

						utemp = (UINT)msg_buf[9];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[8];
						ps.low_rev_limit = utemp;

						utemp = (UINT)msg_buf[11];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[10];
						ps.cooling_fan_on = utemp;
						// start loading serial buffer to send to STM32
						// as a SEND_CONFIG2 msg
						// only need to send temp data - low byte 1st

						utemp = (UINT)msg_buf[13];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[12];
						ps.cooling_fan_off = utemp;

						utemp = (UINT)msg_buf[15];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[14];
						ps.lights_on_value = utemp;

						utemp = (UINT)msg_buf[17];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[16];
						ps.lights_off_value = utemp;

						utemp = (UINT)msg_buf[19];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[18];
						ps.adc_rate = utemp;

						utemp = (UINT)msg_buf[21];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[20];
						ps.rt_value_select = utemp;

						utemp = (UINT)msg_buf[23];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[22];
						ps.lights_on_delay = utemp;
					
						utemp = (UINT)msg_buf[25];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[24];
						ps.engine_temp_limit = utemp;

						utemp = (UINT)msg_buf[27];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[26];
						ps.batt_box_temp = utemp;

						utemp = (UINT)msg_buf[29];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[28];
						ps.test_bank = utemp;

						utemp = (UINT)msg_buf[31];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[30];
						ps.password_timeout = utemp;

						utemp = (UINT)msg_buf[33];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[32];
						ps.password_retries = utemp;

						utemp = (UINT)msg_buf[35];
						utemp <<= 8;
						utemp |= (UINT)msg_buf[34];
						ps.baudrate3 = utemp;

						j = 0;

						memset(password,0,PASSWORD_SIZE);
/*
						for(i = 0;i < 7;i+= 2)
						{
							password[j] = write_serial_buffer[j + 20] = msg_buf[i+36];
							j++;
						}
						write_serial_buffer[j] = 0;
						password[4] = 0;
*/
						usleep(500);
						i = WriteParams("param.conf", &ps, &password[0], errmsg);

						break;

					case GET_CONFIG2:
						break;

					case GET_VERSION:
						send_status_msg(version);
						break;


					case SERVER_UP:
						memset(tempx,0,sizeof(tempx));
//						send_serialother(SERVER_UP,(UCHAR *)tempx);
						break;

					case TEST_IO_PORT:
						i = (UINT)msg_buf[2];	// bank 
						j = (UINT)msg_buf[4];	// port 
						k = (UINT)msg_buf[6];	// onoff
//						sprintf(tempx,"bank: %d port: %d %d",(int)msg_buf[2],
//								(int)msg_buf[4],(int)msg_buf[6]);
//						myprintf1(tempx);
						change_output(i*8 + j, k);
						break;

					case EXIT_PROGRAM:
exit_program:

						j = 0;
						if(reboot_on_exit == 1)
						{
							myprintf1("exit to shell\0");
						}
						else if(reboot_on_exit == 2)
						{
							myprintf1("rebooting...\0");
						}
						else if(reboot_on_exit == 3)
						{
							myprintf1("shutting down...\0");
						}
						else if(reboot_on_exit == 4)
						{
							myprintf1("upload new...\0");
						}

						// save the current list of events
						i = WriteParams("param.conf", &ps, &password[0], errmsg);
						if(i < 0)
						{
							myprintf1(errmsg);
						}

						// pulse the LED on the IO box before shutting down
#if 0
						for(i = 0;i < 20;i++)
						{
							setdioline(7,0);
							uSleep(0,TIME_DELAY/30);
							setdioline(7,1);
							uSleep(0,TIME_DELAY/30);
						}
						setdioline(7,0);
						uSleep(2,0);
						setdioline(7,1);

						// pulse the LED's on the card FWIW

						for(i = 0;i < 20;i++)
						{
							red_led(1);
							usleep(20000);
							red_led(0);
							green_led(1);
							usleep(20000);
							green_led(0);
						}
#endif
						usleep(10000000);
						shutdown_all = 1;
						return 0;
						break;
				}								  // end of switch
			}									  // if rc > 0
		}else									  // if test_sock
		{
			uSleep(1,1000);
			if(shutdown_all  == 1)
			{
//				printf("shutting down cmd host\r\n");
				return 0;
			}
		}
	}
	return test + 1;
}

/*********************************************************************/
// get preamble & msg len from client
// preamble is: {0xF8,0xF0,0xF0,0xF0,0xF0,0xF0,0xF0,0x00,
// msg_len(lowbyte),msg_len(highbyte),0x00,0x00,0x00,0x00,0x00,0x00}
// returns message length
int get_msg(int sd)
{
	int len;
	UCHAR low, high;
	int ret;
	int i;

	UCHAR preamble[10];
	ret = recv_tcp(sd, preamble,8,1);
	//printf("ret: %d\n",ret);
	if(ret < 0)
	{
		printHexByte(ret);
	}
	if(memcmp(preamble,pre_preamble,8) != 0)
	{
		printf("bad preamble\n");
		uSleep(5,0);
		return -1;
	}
	ret = recv_tcp(sd, &low,1,1);
	ret = recv_tcp(sd, &high,1,1);
	//printf("%02x %02x\n",low,high);
	len = 0;
	len = (int)(high);
	len <<= 4;
	len |= (int)low;

	return len;
}
/*********************************************************************/
void send_msg(int sd, int msg_len, UCHAR *msg, UCHAR msg_type)
{
	int ret;
	int i;
	UCHAR temp[2];

	if(test_sock())
	{
		ret = send_tcp(sd, &pre_preamble[0],8);
		temp[0] = (UCHAR)(msg_len & 0x0F);
		temp[1] = (UCHAR)((msg_len & 0xF0) >> 4);
		//printf("%02x %02x\n",temp[0],temp[1]);
		send_tcp(sd, (UCHAR *)&temp[0],1);
		send_tcp(sd, (UCHAR *)&temp[1],1);
		send_tcp(sd, (UCHAR *)&msg_type,1);

		for(i = 0;i < msg_len;i++)
		{
			send_tcp(sd, (UCHAR *)&msg[i],1);
//			send_tcp((UCHAR *)&ret,1);
		}
//		printf("%d ",msg_len);
	}
}
/*********************************************************************/
// get/send_msgb is what the old server used to communicate with the
// windows client - it gets each relavent byte as 2 bytes from the 
// windows machine since that's how the tcp libraries that I'm using
// are done - took me forever to figure this out
int get_msgb(int sd)
{
	int len;
	UCHAR low, high;
	int ret;
	int i;

	UCHAR preamble[20];
	ret = recv_tcp(sd, preamble,16,1);
	if(ret < 0)
	{
		printHexByte(ret);
	}
	if(memcmp(preamble,pre_preamble,8) != 0)
		return -1;

	low = preamble[8];
	high = preamble[9];
	len = (int)(high);
	len <<= 8;
	len |= (int)low;

	return len;
}

/*********************************************************************/
void send_msgb(int sd, int msg_len, UCHAR *msg, UCHAR msg_type)
{
	int len;
	int ret;
	int i;

	if(test_sock())
	{
		ret = send_tcp(sd, &pre_preamble[0],8);
		msg_len++;
		send_tcp(sd, (UCHAR *)&msg_len,1);
		ret = 0;
		send_tcp(sd, (UCHAR *)&ret,1);

		for(i = 0;i < 6;i++)
			send_tcp(sd, (UCHAR *)&ret,1);

		send_tcp(sd, (UCHAR *)&msg_type,1);

		ret = 0;
		send_tcp(sd, (UCHAR *)&ret,1);

		for(i = 0;i < msg_len;i++)
		{
			send_tcp(sd, (UCHAR *)&msg[i],1);
			send_tcp(sd, (UCHAR *)&ret,1);
		}
	}
}

/*********************************************************************/
int recv_tcp(int sd, UCHAR *str, int strlen,int block)
{
	int ret = 0;
	char errmsg[20];
	memset(errmsg,0,20);
	if(test_sock())
	{
//		printf("start get_sock\n");
//		pthread_mutex_lock( &tcp_read_lock);
		ret = get_sock(sd, str,strlen,block,&errmsg[0]);
//		pthread_mutex_unlock(&tcp_read_lock);
//		printf("end get_sock\n");
//printf("%s\n",str);
		if(ret < 0 && (strcmp(errmsg,"Success") != 0))
		{
			printf(errmsg);
		}
	}
	else
	{
		strcpy(errmsg,"sock closed");
		ret = -1;
	}
	return ret;
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
		if(same_msg == 0)
			printf(errmsg);
		same_msg = 1;
	}
	else same_msg = 0;
	return ret;
}

/*********************************************************************/
int put_sock(int sd, UCHAR *buf,int buflen, int block, char *errmsg)
{
	int rc = 0;
	char extra_msg[10];
	if(test_sock())
	{
		if(block)
// block
			rc = send(sd,buf,buflen,MSG_WAITALL);
		else
// don't block
			rc = send(sd,buf,buflen,MSG_DONTWAIT);
		if(rc < 0 && errno != 11)
		{
			strcpy(errmsg,strerror(errno));
			sprintf(extra_msg," %d",errno);
			strcat(errmsg,extra_msg);
			strcat(errmsg," put_sock");
			close_tcp();
		}else strcpy(errmsg,"Success\0");
	}
	else
	{
// this keeps printing out until the client logs on
		strcpy(errmsg,"sock closed");
		rc = -1;
	}
	return rc;
}

/*********************************************************************/
int get_sock(int sd, UCHAR *buf, int buflen, int block, char *errmsg)
{
	int rc;
	char extra_msg[10];
	if(block)
		rc = recv(sd,buf,buflen,MSG_WAITALL);
	else
		rc = recv(sd,buf,buflen,MSG_DONTWAIT);
	if(rc < 0 && errno != 11)
	{
		strcpy(errmsg,strerror(errno));
		sprintf(extra_msg," %d",errno);
		strcat(errmsg,extra_msg);
		strcat(errmsg," get_sock");
	}else strcpy(errmsg,"Success\0");
	return rc;
}
/*********************************************************************/
void send_param_msg(void)
{
	char tempx[40];

	sprintf(tempx, "%d %d %d %d %d %d %d %d %d %d %d %d %d %d %d %d %d %s\0",
														ps.rpm_mph_update_rate,
														ps.fpga_xmit_rate,
														ps.high_rev_limit,
														ps.low_rev_limit,
														ps.cooling_fan_on,
														ps.cooling_fan_off,
														ps.lights_on_value,
														ps.lights_off_value,
														ps.adc_rate,
														ps.rt_value_select,
														ps.lights_on_delay,
														ps.engine_temp_limit,
														ps.batt_box_temp,
														ps.test_bank,
														ps.password_timeout,
														ps.password_retries,
														ps.baudrate3,
														password);
//	send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx, SEND_CONFIG);
}
/*********************************************************************/
void send_status_msg(char *msg)
{
//	send_msg(strlen((char*)msg)*2,(UCHAR*)msg, SEND_STATUS);
}
