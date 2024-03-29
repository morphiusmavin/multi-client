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
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include "../../cmd_types.h"
#include "../../mytypes.h"
#include "../tasks.h"
#include "../ioports.h"
#include "../serial_io.h"
#include "../queue/ollist_threads_rw.h"
#include "../queue/cllist_threads_rw.h"
#include "../tasks.h"
#include "../cs_client/config_file.h"

static struct  sockaddr_in sad;  /* structure to hold server's address  */
#define TOGGLE_OTP otp->onoff = (otp->onoff == 1?0:1)

CMD_STRUCT cmd_array[NO_CMDS];

//extern illist_t ill;
extern ollist_t oll;
extern cllist_t cll;

UCHAR msg_buf[SERIAL_BUFF_SIZE];
UCHAR msg_buf2[SERIAL_BUFF_SIZE];
extern PARAM_STRUCT ps;
extern char password[PASSWORD_SIZE];
int shutdown_all;
struct msgqbuf msg;
int msgtype = 1;

extern int curr_countdown_size;
extern void sort_countdown(void);
extern void display_sort(void);

inline int pack4chars(char c1, char c2, char c3, char c4) {
    return ((int)(((unsigned char)c1) << 24)
            |  (int)(((unsigned char)c2) << 16)
            |  (int)(((unsigned char)c3) << 8)
            |  (int)((unsigned char)c4));
}
#endif

void print_cmd(UCHAR cmd)
{
	char tempx[30];
	
	if(cmd > NO_CMDS)
		printf("unknown cmd: %d\n",cmd);

	sprintf(tempx, "cmd: %d %s\0",cmd,cmd_array[cmd].cmd_str);
	printf("%s\r\n",cmd_array[cmd].cmd_str);
}

/*********************************************************************/
// send a msg back to the sock to send out to tcp
void send_sock_msg(UCHAR *send_msg, int msg_len, UCHAR cmd, int dest)
{
	int i;
	memset(msg.mtext,0,sizeof(msg.mtext));
	msg.mtext[0] = cmd;
	msg.mtext[1] = dest;
	msg.mtext[2] = (UCHAR)msg_len;
	msg.mtext[3] = (UCHAR)(msg_len >> 4);
	//printf("send_sock_msg\n");
	//print_cmd(cmd);
	//printf("msg_len: %d\n",msg_len);
	memcpy(msg.mtext + 4,send_msg,msg_len);
	//printf("msg to cmd_host from client %d\n",dest);
/*
	for(i = 0;i < msg_len+3;i++)
		printf("%02x ",msg.mtext[i]);
	printf("\n");
*/
	if (msgsnd(sock_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
	{
		perror("msgsnd error");
		exit(EXIT_FAILURE);
	}
}
/*********************************************************************/
// task to get commands from the host

UCHAR get_host_cmd_task2(int test)
{
//	I_DATA tempi1;
	O_DATA tempo1;
//	RI_DATA tempr1;
//	I_DATA *itp;
	O_DATA *otp;
	O_DATA **otpp = &otp;
	C_DATA *ctp;
	C_DATA **ctpp = &ctp;
	int rc = 0; 
	int rc1 = 0;
	UCHAR cmd = 0x21;
	UCHAR onoff;
	char errmsg[50];
	char filename[15];
	size_t size;
	int i;
	int j;
	int k;
	size_t csize;
	size_t osize;
	UCHAR tempx[SERIAL_BUFF_SIZE];
	UCHAR tempx2[SERIAL_BUFF_SIZE];
	char temp_time[5];
	char *pch, *pch2;
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
	struct tm tm;
	time_t T;
	struct tm *pt = &t;
	int msg_len;
	serial_recv_on = 1;
//	time_set = 0;
	shutdown_all = 0;
	char version[15] = "sched v1.03\0";
	UINT utemp;
//	UCHAR time_buffer[20];
	next_client = 0;
	char label[30];
	int index;

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
	csize = sizeof(C_DATA);
	csize *= i;

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
			printf("%s\r\n",errmsg);
		}
	}
	
	cllist_init(&cll);
	if(access(cFileName,F_OK) != -1)
	{
		clLoadConfig(cFileName,&cll,csize,errmsg);
		if(rc > 0)
		{
			printf("%s\r\n",errmsg);
		}
		cllist_show(&cll);
	}

	init_ips();
	same_msg = 0;

	//printf("%s\n",version);
	j = k = i = 0;
	cmd = 0x21;

	while(TRUE)
	{
		cmd = 0;

		if(shutdown_all == 1)
		{
			//printf("shutting down cmd host\r\n");
			return 0;
		}

		if(1)
		{
			if (msgrcv(sched_qid, (void *) &msg, sizeof(msg.mtext), msgtype, MSG_NOERROR) == -1) 
			{
				if (errno != ENOMSG) 
				{
					perror("msgrcv");
					printf("msgrcv error\n");
					exit(EXIT_FAILURE);
				}
			}
			//printf("sched cmd host: ");
			cmd = msg.mtext[0];
			//print_cmd(cmd);
			msg_len |= (int)(msg.mtext[2] << 4);
			msg_len = (int)msg.mtext[1];
			
			//printf("msg_len: %d\n",msg_len);
			memset(tempx,0,sizeof(tempx));
			memcpy(tempx,msg.mtext+3,msg_len);
			onoff = tempx[0];

			if(cmd > 0)
			{
//				printf("cmd: %d %s\n",cmd,cmd_array[cmd].cmd_str);
//				printf("%s\r\n",cmd_array[cmd].cmd_str);

				rc = 0;

				switch(cmd)
				{
					case COOP1_LIGHT:
					case COOP1_HEATER:
					case COOP2_LIGHT:
					case COOP2_HEATER:
					case OUTDOOR_LIGHT1:
					case OUTDOOR_LIGHT2:
					case UNUSED150_1:
					case UNUSED150_2:
					case UNUSED150_3:
					case UNUSED150_4:
					case UNUSED150_5:
					case UNUSED150_6:
					case UNUSED150_7:
					case UNUSED150_8:
					case UNUSED150_9:
					case UNUSED150_10:
					case SHUTDOWN_IOBOX:
					case REBOOT_IOBOX:
					case SHELL_AND_RENAME:
					case EXIT_TO_SHELL:
						//printf("sending que: %02x\r\n",cmd);
						memset(tempx,0,sizeof(tempx));
						//send_serialother(cmd,(UCHAR *)tempx);
						add_msg_queue(cmd, onoff);
						break;
					default:
						break;
				}

				if(cmd == SHELL_AND_RENAME || cmd == REBOOT_IOBOX || cmd == SHUTDOWN_IOBOX || cmd == EXIT_TO_SHELL)
				{
					//printf("sending shutdown send sock msg: ");
					//print_cmd(cmd);
					send_sock_msg(tempx, 1, cmd, 8);
					return 1;
				}

 				switch(cmd)
				{
					case RELOAD_CLLIST:
						cllist_init(&cll);
						if(access(cFileName,F_OK) != -1)
						{
							clLoadConfig(cFileName,&cll,csize,errmsg);
							if(rc > 0)
							{
								printf("%s\r\n",errmsg);
							}
							cllist_show(&cll);
						}
						break;

					case CLEAR_CLLIST:
						for(i = 0;i < 20;i++)
						{
							j = cllist_find_data(i, ctpp, &cll);
							if(j == -1)
							{
								printf("bad find: %d\n",index);
								break;
							}
							ctp->port = -1;
							ctp->state = 0;
							ctp->on_hour = 0;
							ctp->on_minute = 0;
							ctp->on_second = 0;
							ctp->off_hour = 0;
							ctp->off_minute = 0;
							ctp->off_second = 0;
							strcpy(ctp->label,"test");
							cllist_change_data(i,ctp,&cll);
						}
						curr_countdown_size = 0;
						break;

					case SHOW_CLLIST:
						printf("show cllist\n");
						j = cllist_show(&cll);
						if(j == -1)
						{
							printf("bad find: %d\n",index);
							break;
						}
						break;

					case SORT_CLLIST:
						sort_countdown();
						break;

					case DISPLAY_CLLIST_SORT:
						display_sort();
						break;

					case SET_CLLIST:
/*					
						printf("set cllist\n");
						printf("msg_len: %d\n",msg_len);
						for(i = 0;i < msg_len/2;i++)
							printf("%02x ",tempx[i]);
						printf("\n");
*/
						index = (int)tempx[0];
						j = cllist_find_data(index, ctpp, &cll);
						if(j == -1)
						{
							printf("bad find: %d\n",index);
							break;
						}
						ctp->index = index;
						ctp->port = (int)tempx[1];
						ctp->state = (int)tempx[2];
						ctp->on_hour = (int)tempx[3];
						ctp->on_minute = (int)tempx[4];
						ctp->on_second = (int)tempx[5];
						ctp->off_hour = (int)tempx[6];
						ctp->off_minute = (int)tempx[7];
						ctp->off_second = (int)tempx[8];
						memset(label,0,sizeof(label));
						memcpy(label,&tempx[10],CLABELSIZE);
						strcpy(ctp->label,label);
						if(ctp->on_hour == 0 && ctp->on_minute == 0 && ctp->on_second == 0 && ctp->off_hour == 0 
								&& ctp->off_minute == 0 && ctp->off_second == 0)
							ctp->port = -1;
						cllist_change_data(index,ctp,&cll);
						memset(tempx,0,sizeof(tempx));
						printf("done\n");
					break;

					case SAVE_CLLIST:
						printf("%d %s\n",csize,cFileName);
						clWriteConfig(cFileName,&cll,csize,errmsg);
						break;
					
					case GET_ALL_CLLIST:
						for(i = 0;i < 20;i++)
						{
							j = cllist_find_data(i, ctpp, &cll);
							if(j == -1)
								break;
							if(ctp->port > -1)
							{
								sprintf(tempx,"%02d %02d %02d %02d %02d %02d %02d %02d %02d %s",ctp->index, ctp->port, ctp->state, ctp->on_hour, ctp->on_minute, ctp->on_second, 
										ctp->off_hour, ctp->off_minute, ctp->off_second, ctp->label);
								printf("%s\n",tempx);
								cmd = REPLY_CLLIST;
								msg_len = strlen(tempx);
								send_sock_msg(tempx, msg_len, cmd, 8);
								/*
								msg.mtext[0] = cmd;
								msg_len = strlen(tempx);
								msg.mtext[1] = (UCHAR)msg_len;
								msg.mtext[2] = (UCHAR)(msg_len >> 4);
								strncpy(msg.mtext+3,tempx,msg_len);

								if (msgsnd(sock_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
								{
									perror("msgsnd error");
									printf("exit from send client list\n");
									exit(EXIT_FAILURE);
								}
								*/
								uSleep(0,TIME_DELAY/4);
							}
						}
						break;

					case AREYOUTHERE:
						printf("yes im here\n");
						break;

/*	testing how the winCl sends ints & longs 
					case DB_LOOKUP:
						printf("tempx: %02x %02x %02x %02x\n",tempx[0],tempx[1],tempx[2],tempx[3]);
						long temp = pack4chars(tempx[3],tempx[2],tempx[1],tempx[0]);
						printf("%d\n",temp);
						break;

	testing how winCl sends var. no. of bytes 
					case DB_LOOKUP:
						printf("tempx: %02x %02x %02x %02x\n",tempx[0],tempx[1],tempx[2],tempx[3]);
						trunning_days = tempx[0];
						trunning_hours = tempx[1];
						trunning_minutes = tempx[2];
						trunning_seconds = tempx[3];
*/						
						break;

					case SET_NEXT_CLIENT:
						next_client = tempx[0];
						if(next_client == 8)
						{
							next_client = 0;
							printf("stop\n");
						}else printf("next client: %s\n", client_table[next_client].label);
						j = 0;
						break;

					case SEND_NEXT_CLIENT:
						cmd = 0x21;
						for(i = 0;i < SERIAL_BUFF_SIZE;i++)
						{
							tempx[i] = cmd;
							if(++cmd > 0x7e)
								cmd = 0x21;
						}
						uSleep(0,TIME_DELAY/10);
						j++;
						if(j > 10)
							j = 0;
						break;

					case CLIENT_RECONNECT:
						printf("cl reconn\n");
//						close_tcp();
						break;

					case UPDATE_CLIENT_INFO:
						this_client_index = tempx[0];
						printf("this client index: %d\n",this_client_index);
						break;
					
					case SEND_TIMEUP:
						memset(tempx,0,sizeof(tempx));
						sprintf(tempx,"%d %d %d %d %d",this_client_index, trunning_days, trunning_hours, trunning_minutes, trunning_seconds);
						//printf("send timeup: %s\n",tempx);
						msg_len = strlen(tempx);
						uSleep(0,TIME_DELAY/8);
						send_sock_msg(tempx, msg_len, UPTIME_MSG, 8);
						break;

					case UPTIME_MSG:
						printf("%s\n",tempx);
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

						//send_msg(4,(UCHAR*)tempx, SEND_STATUS, _SERVER);
//						send_msg(strlen((char*)tempx),(UCHAR*)tempx, SEND_STATUS, _SERVER);
						printf("k: %d j: %d\n",k,j);
//						printf("send status\n");
						break;

					case SEND_MESSAGE:
						//printf("SEND_MESSAGE\n");
						for(i = 0;i < msg_len;i++)
							printf("%c",tempx[i]);
						printf("\n");
						send_sock_msg(tempx, msg_len, cmd, 8);
/*						
						for(i = msg_len;i > 0;i--)
							tempx2[i] = tempx[i];
						
						for(i = 0;i < msg_len;i++)
							printf("%02x ",tempx[i]);
						printf("\n");
						send_msg(strlen((char*)tempx2),(UCHAR*)tempx2, SEND_STATUS, _SERVER);
*/
						break;

					case GET_TEMP4:
						printf("%s\n",tempx);
						break;

					case SET_PARAMS:
//						send_param_msg();
						break;

					case SET_TIME:
						printf("set time\n");
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
						//tempx[msg_len-2] = 'M';
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
							printf("PM\n");
							if(pt->tm_hour != 12)
								pt->tm_hour += 12;
						}else if(*pch == 'A' && pt->tm_hour == 12)
							pt->tm_hour -= 12;
						printf("hour: %d\n",pt->tm_hour);

						curtime2 = mktime(pt);
						stime(pcurtime2);
						uSleep(0,TIME_DELAY/3);
						gettimeofday(&mtv, NULL);
						curtime2 = mtv.tv_sec;
						strftime(tempx,30,"%m-%d-%Y %T\0",localtime(&curtime2));
						printf("%s\n",tempx);
						break;

					case GET_TIME:
						gettimeofday(&mtv, NULL);
						curtime2 = mtv.tv_sec;
						strftime(tempx,30,"%m-%d-%Y %T\0",localtime(&curtime2));
						printf(tempx);
						strftime(tempx,30,"%H",localtime(&curtime2));  // show as 24-hour (00 -> 23)
						printf(tempx);
						printf("\n");
						strftime(tempx,30,"%I",localtime(&curtime2));	// show as 12-hour (01 -> 12)
						printf(tempx);
						printf("\n");
						T = time(NULL);
						tm = *localtime(&T);
						printf("%02d:%02d:%02d\n", tm.tm_hour, tm.tm_min, tm.tm_sec);
						break;

					case BAD_MSG:
//						shutdown_all = 1;
						break;

					case DISCONNECT:
/*
						if(test_sock() > 0)
						{
							close_tcp();
							printf("disconnected\0");
						}
*/
						break;

					case UPDATE_CONFIG:
#if 0
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
						usleep(500);
						i = WriteParams("param.conf", &ps, &password[0], errmsg);
#endif
						break;

					case GET_CONFIG2:
						break;

					case GET_VERSION:
						send_status_msg(version);
						break;

					default:
						//printf("default in main loop\n");
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
//	send_msg(strlen((char*)msg)*2,(UCHAR*)msg, SEND_STATUS,_SERVER);
	printf("%s\n",msg);
}
