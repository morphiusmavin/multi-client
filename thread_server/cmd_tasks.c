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
#include "../cmd_types.h"
#include "../mytypes.h"
#include "tasks.h"
#include "ioports.h"
#include "serial_io.h"
#include "queue/ollist_threads_rw.h"
#include "queue/cllist_threads_rw.h"
#include "cs_client/config_file.h"

CLIENT_TABLE client_table[MAX_CLIENTS];
#define TOGGLE_OTP otp->onoff = (otp->onoff == 1?0:1)

extern ollist_t oll;
extern cllist_t cll;

extern PARAM_STRUCT ps;
extern char password[PASSWORD_SIZE];
int shutdown_all;

extern CMD_STRUCT cmd_array[];

void print_cmd(UCHAR cmd)
{
	char tempx[30];

	if(cmd > NO_CMDS)
		printf("unknown cmd: %d\n",cmd);

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
//	O_DATA *otp;
//	O_DATA **otpp = &otp;
	C_DATA *ctp;
	C_DATA **ctpp = &ctp;
	//C_DATA *cdata_temp;
	int rc = 0; 
	int rc1 = 0;
	UCHAR cmd = 0x21;
	UCHAR onoff, port;
	char errmsg[50];
	char filename[15];
	char *fptr;
	size_t size;
	int i;
	int j;
	int k;
	size_t osize;
	size_t csize;
	time_t T;
	struct tm tm;
	UCHAR tempx[UPLOAD_BUFF_SIZE];
	char tempy[30];
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
	struct tm *pt = &t;
	int msg_len;
	serial_recv_on = 1;
//	time_set = 0;
	shutdown_all = 0;
	char version[15] = "sched v1.03\0";
	UINT utemp;
	struct msgqbuf msg;
	int msgtype = 1;

	//UCHAR write_serial_buffer[SERIAL_BUFF_SIZE];
	int temp;
	char label[30];
	int index;
	
	assign_client_table();

	//memset(write_serial_buffer, 0, SERIAL_BUFF_SIZE);
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
	i = NUM_PORT_BITS;
	//printf("no. port bits: %d\r\n",i);
	osize = sizeof(O_DATA);
	osize *= i;
	//printf("osize: %d\r\n",osize);

	i = NO_CLLIST_RECS;
	//printf("no. port bits: %d\r\n",i);
	csize = sizeof(C_DATA);
	csize *= i;

/*
	trunning_days = 1;
	trunning_hours = 5;
	trunning_minutes = 14;
	trunning_seconds = 0;
*/
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
		//cllist_show(&cll);
	}else printf("can't find %s\n",cFileName);

	init_ips();
	same_msg = 0;
	//cdata_temp = (C_DATA *)malloc(sizeof(C_DATA));
	//memset(cdata_temp, 0, sizeof(C_DATA));

	while(TRUE)
	{
		cmd = 0;
//		get msg from sock_mgt or WinClReadTask
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
		memset(tempx, 0, sizeof(tempx));
		cmd = msg.mtext[0];
		//print_cmd(cmd);
		msg_len |= (int)(msg.mtext[2] << 4);
		msg_len = (int)msg.mtext[1];
		
		//printf("msg_len: %d\n",msg_len);
		memcpy(tempx,msg.mtext+3,msg_len);
		onoff = tempx[0];

		if(cmd > 0)
		{
			rc = 0;
			switch(cmd)
			{
				case DESK_LIGHT:
				case EAST_LIGHT:
				case NORTHWEST_LIGHT:
				case SOUTHEAST_LIGHT:
				case MIDDLE_LIGHT:
				case WEST_LIGHT:
				case NORTHEAST_LIGHT:
				case SOUTHWEST_LIGHT:
				case SHUTDOWN_IOBOX:
				case REBOOT_IOBOX:
				case WATER_HEATER:
				case WATER_PUMP:
				case WATER_VALVE1:
				case WATER_VALVE2:
				case WATER_VALVE3:
				case SHELL_AND_RENAME:
					//printf("sending que: %02x\r\n",cmd);
					memset(tempx,0,sizeof(tempx));
					//send_serialother(cmd,(UCHAR *)tempx);
					add_msg_queue(cmd, onoff);
					break;
				default:
					break;
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
						cllist_find_data(i, ctpp, &cll);
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
					printf("sort\n");
					sort_countdown();
					break;

				case DISPLAY_CLLIST_SORT:
					display_sort();
					break;

				case REPLY_CLLIST:
/*
						printf("msg_len: %d\n",msg_len);
						for(i = 0;i < msg_len;i++)
							printf("%c",tempx[i]);
						printf("\n");
*/
						cmd = REPLY_CLLIST;
						msg.mtext[0] = cmd;
						msg_len = strlen(tempx);
						msg.mtext[1] = (UCHAR)msg_len;
						msg.mtext[2] = (UCHAR)(msg_len >> 4);
						strncpy(msg.mtext+3,tempx,msg_len);

						if (msgsnd(sock_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
						{
							perror("msgsnd error");
							printf("exit from REPLY_CLLIST\n");
							exit(EXIT_FAILURE);
						}
						uSleep(0,TIME_DELAY/4);
					break;

				case GET_ALL_CLLIST:
					for(i = 0;i < 20;i++)
					{
						cllist_find_data(i, ctpp, &cll);
						if(ctp->port > -1)
						{
							sprintf(tempx,"%02d %02d %02d %02d %02d %02d %02d %02d %02d %s",ctp->index, ctp->port, ctp->state, ctp->on_hour, ctp->on_minute, ctp->on_second, 
										ctp->off_hour, ctp->off_minute, ctp->off_second, ctp->label);
							printf("%s\n",tempx);
							cmd = REPLY_CLLIST;
							msg.mtext[0] = cmd;
							msg_len = strlen(tempx);
							msg.mtext[1] = (UCHAR)msg_len;
							msg.mtext[2] = (UCHAR)(msg_len >> 4);
							strncpy(msg.mtext+3,tempx,msg_len);

							if (msgsnd(sock_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
							{
								perror("msgsnd error");
								printf("exit from GET_ALL_CLLIST list\n");
								exit(EXIT_FAILURE);
							}
							uSleep(0,TIME_DELAY/4);
						}
					}
					break;

				case SET_CLLIST:
/*
					printf("msg_len: %d\n",msg_len);
					for(i = 0;i < msg_len;i++)
						printf("%02x ",tempx[i]);
					printf("\n");
*/
					index = (int)tempx[0];
					cllist_find_data(index, ctpp, &cll);
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
					memcpy(label,&tempx[10],30);
					strcpy(ctp->label,label);
					if(ctp->on_hour == 0 && ctp->on_minute == 0 && ctp->on_second == 0 && ctp->off_hour == 0 
							&& ctp->off_minute == 0 && ctp->off_second == 0)
						ctp->port = -1;
					cllist_change_data(index,ctp,&cll);
					memset(tempx,0,sizeof(tempx));
					printf("done\n");
					break;

				case SAVE_CLLIST:
					clWriteConfig(cFileName,&cll,csize,errmsg);
					break;

				case YESIMHERE:
					msg.mtext[0] = cmd;
					msg_len = strlen(tempx);
					msg.mtext[1] = (UCHAR)msg_len;
					msg.mtext[2] = (UCHAR)(msg_len >> 4);
					strncpy(msg.mtext+3,tempx,msg_len);

					if (msgsnd(sock_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
//					if (msgsnd(sock_qid, (void *) &msg, msg_len, MSG_NOERROR) == -1) 
					{
						// keep getting "Invalid Argument" - cause I didn't set the mtype
						perror("msgsnd error");
						printf("exit from YESIMHERE list\n");
						exit(EXIT_FAILURE);
					}
					break;

				case SEND_CLIENT_LIST:
					//printf("SEND_CLIENT_LIST from cmd_task (sched)\n");
					msg.mtext[0] = cmd;
					msg.mtext[1] = 8;
					msg_len = 2;
					msg.mtext[2] = (UCHAR)msg_len;
					msg.mtext[3] = (UCHAR)(msg_len >> 4);

					if (msgsnd(sock_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
					{
						// keep getting "Invalid Argument" - cause I didn't set the mtype
						perror("msgsnd error");
						printf("exit from send client list\n");
						exit(EXIT_FAILURE);
					}
					break;
				
				case CLIENT_RECONNECT:
					for(i = 0;i < MAX_CLIENTS;i++)
					{
						if(client_table[i].socket > 0)
						{
							memset(tempx,0,sizeof(tempx));
							sprintf(tempx,"%d %s %d", i, client_table[i].ip, client_table[i].socket);
							//printf("should be sending a msg to clients %s\n",tempx);
//							send_msg(client_table[i].socket, strlen(tempx)*2,tempx,CLIENT_RECONNECT);
							uSleep(0,TIME_DELAY/2);
						}
					}
					break;

				case UPTIME_MSG:
					//printf("%s\n",tempx);
					msg.mtext[0] = UPTIME_MSG;
					msg_len = strlen(tempx);
					msg.mtext[1] = (UCHAR)msg_len;
					msg.mtext[2] = (UCHAR)(msg_len >> 4);
					memcpy(msg.mtext+3,tempx,msg_len);
					if (msgsnd(sock_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
					{
						// keep getting "Invalid Argument" - cause I didn't set the mtype
						perror("msgsnd error");
						printf("exit from UPTIME_MSG list\n");
						exit(EXIT_FAILURE);
					}
					break;

				case SEND_TIMEUP:
					memset(tempx,0,sizeof(tempx));
					sprintf(tempx,"8 %d %d %d %d",trunning_days, trunning_hours, trunning_minutes, trunning_seconds);
					//printf("send timeup: %s\n",tempx);
					msg.mtext[0] = UPTIME_MSG;
					msg_len = strlen(tempx);
					msg.mtext[1] = (UCHAR)msg_len;
					msg.mtext[2] = (UCHAR)(msg_len >> 4);
					memcpy(msg.mtext+3,tempx,msg_len);
					if (msgsnd(sock_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
					{
						// keep getting "Invalid Argument" - cause I didn't set the mtype
						perror("msgsnd error");
						printf("exit from SEND_TIMEUP list\n");
						exit(EXIT_FAILURE);
					}
					break;

				case SEND_MESSAGE:
					for(i = 0;i < msg_len;i++)
						printf("%c",tempx[i]);
					printf("\n");
					break;

				case SEND_STATUS:
					//printf("send status (sched)\n");
					temp = 0;
					temp = (int)(tempx[1] << 4);
					temp |= (int)tempx[0];
					printf("temp: %d ",temp);
					temp = 0;
					temp = (int)(tempx[3] << 4);
					temp |= (int)tempx[2];
					printf("temp: %d\n",temp);
					break;

				case SET_PARAMS:
//					send_param_msg();
					break;

				case SET_TIME:
					curtime2 = 0L;
					j = 0;
/*
printf("set time %d\n",msg_len);
for(i = 0;i < msg_len+2;i++)
	printf("%c",tempx[i]);
*/
//					tempx[msg_len-2] = 'M';
					memset(temp_time,0,sizeof(temp_time));
					i = 0;
					pch = &tempx[0];

					while(*(pch++) != '/' && i < msg_len)
					{
						i++;
					}
					memcpy(&temp_time[0],&tempx[0],i);
					i = atoi(temp_time);
					pt->tm_mon = i - 1;
//printf("month: %d\n",pt->tm_mon);
					i = 0;

					while(*(pch++) != '/' && i < msg_len)
					{
						i++;
					}
					memset(temp_time,0,sizeof(temp_time));
					memcpy(temp_time,pch-i-1,i);
					i = atoi(temp_time);
//printf("day: %d\n",pt->tm_mday);
					pt->tm_mday = i;
					i = 0;
					while(*(pch++) != ' ' && i < msg_len)
					{
						i++;
					}
					memset(temp_time,0,sizeof(temp_time));
					memcpy(temp_time,pch-3,2);
					i = atoi(temp_time);
					i += 100;
					pt->tm_year = i;
//printf("year: %d\n",pt->tm_year);
					i = 0;

					while(*(pch++) != ':' && i < msg_len)
						i++;
					memset(temp_time,0,sizeof(temp_time));
					memcpy(temp_time,pch-i-1,i);
					i = atoi(temp_time);
					pt->tm_hour = i;
//printf("hour: %d\n",pt->tm_hour);
					i = 0;
					while(*(pch++) != ':' && i < msg_len)
						i++;
					memset(temp_time,0,sizeof(temp_time));
					memcpy(temp_time,pch-3,2);
					i = atoi(temp_time);
					pt->tm_min = i;
//printf("minute: %d\n",pt->tm_min);
					i = 0;
					while(*(pch++) != ' ' && i < msg_len)
						i++;
					memset(temp_time,0,sizeof(temp_time));
					memcpy(temp_time,pch-3,2);
					i = atoi(temp_time);
					pt->tm_sec = i;
//printf("second: %d\n",pt->tm_sec);
//printf("*%c %c\n",*(pch-1),*pch);

//printf("%c\n",*pch);
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
					gettimeofday(&mtv, NULL);
					curtime2 = mtv.tv_sec;
					strftime(tempx,30,"%m-%d-%Y %T\0",localtime(&curtime2));
					printf("time: %s\n",tempx);
					break;

				case GET_TIME:
					gettimeofday(&mtv, NULL);
					curtime2 = mtv.tv_sec;
					strftime(tempx,30,"%m-%d-%Y %T\0",localtime(&curtime2));
					printf(tempx);
					printf("\n");

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
					printf("disconnected\n");
					break;

				case UPDATE_CONFIG:
					utemp = (UINT)tempx[3];
					utemp <<= 8;
					utemp |= (UINT)tempx[2];
					ps.rpm_mph_update_rate = utemp;

					utemp = (UINT)tempx[5];
					utemp <<= 8;
					utemp |= (UINT)tempx[4];
					ps.fpga_xmit_rate = utemp;

					utemp = (UINT)tempx[7];
					utemp <<= 8;
					utemp |= (UINT)tempx[6];
					ps.high_rev_limit = utemp;

					utemp = (UINT)tempx[9];
					utemp <<= 8;
					utemp |= (UINT)tempx[8];
					ps.low_rev_limit = utemp;

					utemp = (UINT)tempx[11];
					utemp <<= 8;
					utemp |= (UINT)tempx[10];
					ps.cooling_fan_on = utemp;
					// start loading serial buffer to send to STM32
					// as a SEND_CONFIG2 msg
					// only need to send temp data - low byte 1st

					utemp = (UINT)tempx[13];
					utemp <<= 8;
					utemp |= (UINT)tempx[12];
					ps.cooling_fan_off = utemp;

					utemp = (UINT)tempx[15];
					utemp <<= 8;
					utemp |= (UINT)tempx[14];
					ps.lights_on_value = utemp;

					utemp = (UINT)tempx[17];
					utemp <<= 8;
					utemp |= (UINT)tempx[16];
					ps.lights_off_value = utemp;

					utemp = (UINT)tempx[19];
					utemp <<= 8;
					utemp |= (UINT)tempx[18];
					ps.adc_rate = utemp;

					utemp = (UINT)tempx[21];
					utemp <<= 8;
					utemp |= (UINT)tempx[20];
					ps.rt_value_select = utemp;

					utemp = (UINT)tempx[23];
					utemp <<= 8;
					utemp |= (UINT)tempx[22];
					ps.lights_on_delay = utemp;
				
					utemp = (UINT)tempx[25];
					utemp <<= 8;
					utemp |= (UINT)tempx[24];
					ps.engine_temp_limit = utemp;

					utemp = (UINT)tempx[27];
					utemp <<= 8;
					utemp |= (UINT)tempx[26];
					ps.batt_box_temp = utemp;

					utemp = (UINT)tempx[29];
					utemp <<= 8;
					utemp |= (UINT)tempx[28];
					ps.test_bank = utemp;

					utemp = (UINT)tempx[31];
					utemp <<= 8;
					utemp |= (UINT)tempx[30];
					ps.password_timeout = utemp;

					utemp = (UINT)tempx[33];
					utemp <<= 8;
					utemp |= (UINT)tempx[32];
					ps.password_retries = utemp;

					utemp = (UINT)tempx[35];
					utemp <<= 8;
					utemp |= (UINT)tempx[34];
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

					//i = WriteParams("param.conf", &ps, &password[0], errmsg);
				default:	
					break;
					
			}								  // end of switch
		}									  // if rc > 0
		uSleep(0,TIME_DELAY/8);
		if(shutdown_all == 1)
		{
//				printf("shutting down cmd host\r\n");
			return 0;
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
//	send_msg(strlen((char*)msg)*2,(UCHAR*)msg, SEND_STATUS);
}
