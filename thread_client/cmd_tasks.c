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
#include "queue/cllist_threads_rw.h"
#include "tasks.h"
#include "cs_client/config_file.h"
#include "lcd_func.h"

extern pthread_mutex_t     tcp_read_lock;
extern pthread_mutex_t     tcp_write_lock;
static struct  sockaddr_in sad;  /* structure to hold server's address  */
#define TOGGLE_OTP otp->onoff = (otp->onoff == 1?0:1)

extern CMD_STRUCT cmd_array[];

//extern illist_t ill;
extern ollist_t oll;
extern cllist_t cll;

UCHAR msg_buf[UPLOAD_BUFF_SIZE];
UCHAR msg_buf2[UPLOAD_BUFF_SIZE];
extern PARAM_STRUCT ps;
extern char password[PASSWORD_SIZE];
int shutdown_all;
static UCHAR pre_preamble[] = {0xF8,0xF0,0xF0,0xF0,0xF0,0xF0,0xF0,0x00};

#endif

void print_cmd(UCHAR cmd)
{
	char tempx[30];

	sprintf(tempx, "cmd: %d %s\0",cmd,cmd_array[cmd].cmd_str);
	printf("%s\r\n",cmd_array[cmd].cmd_str);
}

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
//	C_DATA *ctp;
//	C_DATA **ctpp = &ctp;
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
//	size_t csize;
	size_t osize;
	struct dirent **namelist;
	DIR *d;
	struct dirent *dir;
	UCHAR tempx[SERIAL_BUFF_SIZE];
	UCHAR tempx2[SERIAL_BUFF_SIZE];
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
	lcd_enabled = 0;
//	UCHAR time_buffer[20];
	UCHAR write_serial_buffer[SERIAL_BUFF_SIZE];
	timer_on = 0;
	timer_seconds = 20;

	memset(write_serial_buffer, 0, SERIAL_BUFF_SIZE);
	// since each card only has 20 ports then the 1st 2 port access bytes
	// are 8-bit and the 3rd is only 4-bits, so we have to translate the
	// inportstatus array, representing 3 byts of each 2 (3x8x2 = 48) to
	// one of the 40 actual bits as index

	// the check_inputs & change_outputs functions
	// use the array to adjust from index to bank
	// since there are only 4 bits in banks 3 & 5
	//printf("starting...\n");

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
	i = NO_CLLIST_RECS;
	//printf("no. port bits: %d\r\n",i);
//	csize = sizeof(C_DATA);
//	csize *= i;

	trunning_days = trunning_hours = trunning_minutes = trunning_seconds = 0;
/*
	trunning_days = 1;
	trunning_hours = 5;
	trunning_minutes = 14;
	trunning_seconds = 0;
*/
//	program_start_time = curtime();

	ollist_init(&oll);
	if(access(oFileName,F_OK) != -1)
	{
		olLoadConfig(oFileName,&oll,osize,errmsg);
		if(rc > 0)
		{
//			myprintf1(errmsg);
			printf("%s\r\n",errmsg);
		}
	}
	init_ips();
/*
	printf("%s\n",cFileName);

	cllist_init(&cll);
	if(access(cFileName,F_OK) != -1)
	{
		clLoadConfig(cFileName,&cll,csize,errmsg);
		if(rc > 0)
		{
//			myprintf1(errmsg);
			printf("%s\r\n",errmsg);
		}
	}else printf("can't access %s\n",cFileName);
	
	cllist_show(&cll);
	rc = cllist_find_data(4,ctpp,&cll);
	printf("%d %d %d %s\n",ctp->index,ctp->client_no,ctp->cmd, ctp->label);
	ctp->cmd = 2;
	rc = cllist_find_data(4,ctpp,&cll);
	cllist_insert_data(4,&cll,ctp);
	printf("%d %d %d %s\n",ctp->index,ctp->client_no,ctp->cmd, ctp->label);
	cllist_show(&cll);
*/
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

	printf("%s\n",version);
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
	k = i = 0;
	cmd = 0x21;

	while(TRUE)
	{
		cmd = 0;

		if(shutdown_all == 1)
		{
			printf("shutting down cmd host\r\n");
			return 0;
		}

		if(test_sock() == 1)
//		if(1)
		{
			memset(msg_buf,0,sizeof(msg_buf));
			//printf("wait for msg_len\n");
			msg_len = get_msg();
			printf("msg_len: %d\n",msg_len);
//			printHexByte(msg_len);
			if(msg_len < 0)
			{
//				printf("bad msg\r\n");
				cmd = BAD_MSG;
				usleep(10000);
			}else
			{
				rc = recv_tcp(&msg_buf[0],msg_len+1,1);
				printf("rc: %d\n",rc);
/*
				rc = cllist_find_data(msg_buf[0],ctpp,&cll);
				printf("%d %d %d %d %s\n",ctp->index,ctp->client_no,ctp->cmd, ctp-> dest, ctp->label);
				printf("this: %s %s\n",client_table[ctp->dest].ip, client_table[ctp->dest].label);
				printf("dest: %s %s\n",client_table[ctp->client_no].ip, client_table[ctp->client_no].label);
				cmd = ctp->cmd;
*/
				cmd = msg_buf[0];
				print_cmd(cmd);
				memset(tempx,0,sizeof(tempx));
				memcpy(tempx,msg_buf+1,msg_len);
			}

			if(cmd > 0)
			{
//				printf("cmd: %d %s\n",cmd,cmd_array[cmd].cmd_str);
//				printf("%s\r\n",cmd_array[cmd].cmd_str);
//				if(cmd < LCD_TEST_MODE)
//					myprintf1(cmd_array[cmd].cmd_str);
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
					case WAIT_REBOOT_IOBOX:
					case SHELL_AND_RENAME:
						//printf("sending que: %02x\r\n",cmd);
						memset(tempx,0,sizeof(tempx));
						//send_serialother(cmd,(UCHAR *)tempx);
						add_msg_queue(cmd);
						break;
					default:
						break;
				}

				if(cmd == WAIT_REBOOT_IOBOX || cmd == REBOOT_IOBOX || cmd == SHUTDOWN_IOBOX)
				{
					return 1;
				}

 				switch(cmd)
				{
					case SET_TIMER:
						timer_seconds = tempx[0];
						printf("%02x %02x %02x\n",tempx[0], tempx[1], tempx[2]);
						printf("timer set to: %d seconds\n",timer_seconds);
						break;

					case START_TIMER:
						timer_on = 1;
						printf("timer on\n");
						break;

					case STOP_TIMER:
						timer_on = 0;
						printf("timer off\n");
						break;

					case CLIENT_RECONNECT:
						printf("cl reconn\n");
						close_tcp();
						break;

					case UPDATE_CLIENT_LIST:
						printf("tempx: %d %d\n", tempx[1], tempx[2]);
						client_table[tempx[1]].socket = tempx[2];
						break;
					
					case SEND_TIMEUP:
						memset(tempx,0,sizeof(tempx));
						sprintf(tempx,"%d days %dh %dm %ds",trunning_days, trunning_hours, 
							trunning_minutes, trunning_seconds);
						send_msg(strlen((char*)tempx),(UCHAR*)tempx, UPTIME_MSG, _SERVER);
						printf("%s\n",tempx);
						break;

					case UPTIME_MSG:
						printf("%s\n",tempx);
						break;

					case SEND_CLIENT_LIST:
						for(i = 0;i < MAX_CLIENTS;i++)
						{
							usleep(100);
						}
						break;
					case SEND_STATUS:
						k++;
						j += 10;

						memset(tempx,0,sizeof(tempx));
						tempx[0] = (UCHAR)k;
						tempx[1] = (UCHAR)(k >> 4);
						tempx[2] = (UCHAR)j;
						tempx[3] = (UCHAR)(j >> 4);
						tempx[4] = 0;

						send_msg(4,(UCHAR*)tempx, SEND_STATUS, _SERVER);
//						send_msg(strlen((char*)tempx),(UCHAR*)tempx, SEND_STATUS, _SERVER);
						printf("k: %d j: %d\n",k,j);
//						printf("send status\n");
						break;

					case SEND_MSG:
						printf("SEND_MSG\n");
						for(i = 0;i < msg_len;i++)
							printf("%02x ",tempx[i]);
						printf("\n");
						
						for(i = msg_len;i > 0;i--)
							tempx2[i] = tempx[i];
						
						for(i = 0;i < msg_len;i++)
							printf("%02x ",tempx[i]);
						printf("\n");
						send_msg(strlen((char*)tempx2),(UCHAR*)tempx2, SEND_STATUS, _SERVER);
						break;

					case GET_TEMP4:
						printf("%s\n",tempx);
						break;

					case SET_PARAMS:
//						send_param_msg();
						break;

					case SET_TIME:
						curtime2 = 0L;
						j = 0;

//						for(i = 2;i < msg_len;i+=2)
//							memcpy((void*)&tempx[j++],(char*)&msg_buf[i],1);
/*
						for(i = 0;i < msg_len/2+2;i++)
						{
							tempx[i] = msg_buf2[i];
//							write_serial2(tempx[i]);
						}
*/
						tempx[msg_len-2] = 'M';
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
//						printf("\nmon: %d\n",i - 1);
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
uSleep(0,TIME_DELAY/3);
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
						printf(tempx);
						send_msg(strlen((char*)tempx),(UCHAR*)tempx,GET_TIME, _SERVER);
						break;

					case BAD_MSG:
//						shutdown_all = 1;
//						myprintf1("bad msg");
						break;

					case DISCONNECT:
						if(test_sock() > 0)
						{
							close_tcp();
							printf("disconnected\0");
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
/*
					case EXIT_PROGRAM:
exit_program:
						j = 0;
						if(reboot_on_exit == 1)
						{
							printf("exit to shell\0");
						}
						else if(reboot_on_exit == 2)
						{
							printf("rebooting...\0");
						}
						else if(reboot_on_exit == 3)
						{
							printf("shutting down...\0");
						}
						else if(reboot_on_exit == 4)
						{
							printf("upload new...\0");
						}

						// save the current list of events
						i = WriteParams("param.conf", &ps, &password[0], errmsg);
						if(i < 0)
						{
							printf(errmsg);
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
*/
					default:
						printf("default in main loop\n");
						break;
				}								  // end of switch
			}									  // if rc > 0
		}else									  // if test_sock
		{
			uSleep(1,1000);
		}
	}
	return test + 1;
}

/*********************************************************************/
// get preamble & msg len from client
// preamble is: {0xF8,0xF0,0xF0,0xF0,0xF0,0xF0,0xF0,0x00,
// msg_len(lowbyte),msg_len(highbyte),0x00,0x00,0x00,0x00,0x00,0x00}
// returns message length
int get_msg(void)
{
	int len;
	UCHAR low, high;
	int ret;
	int i;

	UCHAR preamble[10];
	ret = recv_tcp(preamble,8,1);
	//printf("ret: %d\n",ret);
	if(ret < 0)
	{
		printHexByte(ret);
	}
	if(memcmp(preamble,pre_preamble,8) != 0)
	{
		printf("bad preamble\n");
		uSleep(1,0);
		return -1;
	}
	ret = recv_tcp(&low,1,1);
	ret = recv_tcp(&high,1,1);
	//printf("%02x %02x\n",low,high);
	len = 0;
	len = (int)(high);
	len <<= 4;
	len |= (int)low;

	return len;
}

/*********************************************************************/
/*********************************************************************/
// send the preamble, msg len, msg_type & dest (dest is index into client table)
void send_msg(int msg_len, UCHAR *msg, UCHAR msg_type, UCHAR dest)
{
	int ret;
	int i;
	UCHAR temp[2];

	if(dest > MAX_CLIENTS)
		return;

	if(test_sock())
	{
		ret = send_tcp(&pre_preamble[0],8);
		temp[0] = (UCHAR)(msg_len & 0x0F);
		temp[1] = (UCHAR)((msg_len & 0xF0) >> 4);
		//printf("%02x %02x\n",temp[0],temp[1]);
		send_tcp((UCHAR *)&temp[0],1);
		send_tcp((UCHAR *)&temp[1],1);
		send_tcp((UCHAR *)&msg_type,1);
		send_tcp((UCHAR *)&dest,1);

		for(i = 0;i < msg_len;i++)
		{
			send_tcp((UCHAR *)&msg[i],1);
//			send_tcp((UCHAR *)&ret,1);
		}
//		printf("%d ",msg_len);
	}
}
/*********************************************************************/
int recv_tcp(UCHAR *str, int strlen,int block)
{
	int ret = 0;
	char errmsg[20];
	memset(errmsg,0,20);
	if(test_sock())
	{
//		pthread_mutex_lock( &tcp_read_lock);
		ret = get_sock(str,strlen,block,&errmsg[0]);
		//printf("ret: %d\n",ret);
//		pthread_mutex_unlock(&tcp_read_lock);
		if(ret < 0 && (strcmp(errmsg,"Success") != 0))
		{
			printf(errmsg);
		}
	}
	else
	{
		strcpy(errmsg,"sock closed");
		printf(errmsg);
		ret = -1;
	}
	return ret;
}
/*********************************************************************/
int send_tcp(UCHAR *str,int len)
{
	int ret = 0;
	char errmsg[30];
	memset(errmsg,0,30);
	pthread_mutex_lock( &tcp_write_lock);
	ret = put_sock(str,len,1,&errmsg[0]);
	pthread_mutex_unlock(&tcp_write_lock);
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
int put_sock(UCHAR *buf,int buflen, int block, char *errmsg)
{
	int rc = 0;
	char extra_msg[10];
	if(test_sock())
	{
		if(block)
// block
			rc = send(global_socket,buf,buflen,MSG_WAITALL);
		else
// don't block
			rc = send(global_socket,buf,buflen,MSG_DONTWAIT);
		if(rc < 0 && errno != 11)
		{
			strcpy(errmsg,strerror(errno));
			sprintf(extra_msg," %d",errno);
			strcat(errmsg,extra_msg);
			strcat(errmsg," put_sock");
			close_tcp();
			printf("closing tcp socket\n");
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
int get_sock(UCHAR *buf, int buflen, int block, char *errmsg)
{
	int rc;
	char extra_msg[10];
	if(block)
		rc = recv(global_socket,buf,buflen,MSG_WAITALL);
	else
		rc = recv(global_socket,buf,buflen,MSG_DONTWAIT);
	//printf("rc: %d\n",rc);
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
	send_msg(strlen((char*)msg)*2,(UCHAR*)msg, SEND_STATUS,_SERVER);
	printf("%s\n",msg);
}
