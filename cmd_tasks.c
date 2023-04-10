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
#include "cmd_types.h"
#include "mytypes.h"
#include "ioports.h"
#include "serial_io.h"
#include "queue/ollist_threads_rw.h"
#include "queue/cllist_threads_rw.h"
#include "queue/dllist_threads_rw.h"
#include "tasks.h"
#include "raw_data.h"
#include "cs_client/config_file.h"

static struct  sockaddr_in sad;  /* structure to hold server's address  */
#define TOGGLE_OTP otp->onoff = (otp->onoff == 1?0:1)

extern CMD_STRUCT cmd_array[];

//extern illist_t ill;
extern ollist_t oll;
extern cllist_t cll;
extern dllist_t dll;
extern int ds_index;
extern int ds_reset;
int cs_index;

//UCHAR msg_buf[SERIAL_BUFF_SIZE];
extern PARAM_STRUCT ps;
extern char password[PASSWORD_SIZE];
int shutdown_all;

extern int curr_countdown_size;
extern void sort_countdown(void);
extern void display_sort(void);
extern char *lookup_raw_data(int val);
extern int avg_raw_data(int sample_size);

inline int pack4chars(char c1, char c2, char c3, char c4) {
    return ((int)(((unsigned char)c1) << 24)
            |  (int)(((unsigned char)c2) << 16)
            |  (int)(((unsigned char)c3) << 8)
            |  (int)((unsigned char)c4));
}
#endif

struct msgqbuf msg;		// this has to be shared by send_sock_msg & get_host_cmd_task
int msgtype = 1;

void print_cmd(UCHAR cmd)
{
	char tempx[30];

	if(cmd > NO_CMDS)
		printf("unknown cmd: %d\n",cmd);

	sprintf(tempx, "cmd: %d %s\0",cmd,cmd_array[cmd].cmd_str);
	printf("%s\r\n",cmd_array[cmd].cmd_str);
}

/*********************************************************************/
// send a msg back to the sock to send to server sock
void send_sock_msg(UCHAR *send_msg, int msg_len, UCHAR cmd, int dest)
{
	int i;
	msg.mtype = msgtype;
	memset(msg.mtext,0,sizeof(msg.mtext));
	msg.mtext[0] = cmd;
	msg.mtext[1] = dest;
	msg.mtext[2] = (UCHAR)msg_len;
	msg.mtext[3] = (UCHAR)(msg_len >> 4);
	
	//printf("send_sock_msg :");
	//print_cmd(cmd);
	//printf("msg_len: %d\n",msg_len);
	memcpy(msg.mtext + 4,send_msg,msg_len);
	//printf("msg to cmd_host from client %d\n",dest);
/*
	for(i = 0;i < msg_len+3;i++)
		printf("%02x ",msg.mtext[i]);
	printf("\n");
*/
	// if client send msg to recv_msg_task
	// if server send to cmd host on sock 
	if (msgsnd(sock_qid, (void *) &msg, sizeof(msg.mtext), MSG_NOERROR) == -1) 
	{
		perror("msgsnd error");
		//exit(EXIT_FAILURE);

	}
}
/*********************************************************************/
// task to get commands from the sock
UCHAR get_host_cmd_task(int test)
{
	O_DATA *otp;
	O_DATA **otpp = &otp;
	C_DATA *ctp;
	C_DATA **ctpp = &ctp;
	D_DATA *dtp;
	D_DATA **dtpp = &dtp;
	C_DATA *cttp;
	int rc = 0; 
	UCHAR cmd = 0x21;
	UCHAR onoff;
	char errmsg[50];
	char filename[15];
	int i;
	int j;
	int k;
	size_t csize;
	size_t osize;
	size_t dsize;
	UCHAR tempx[SERIAL_BUFF_SIZE];
	char temp_time[5];
	char *pch, *pch2;
	time_t curtime2;
	time_t *pcurtime2 = &curtime2;
	int fp;
	off_t fsize;
	struct timeval mtv;
	struct tm t;
	struct tm tm;
	time_t T;
	struct tm *pt = &t;
	int msg_len;
	shutdown_all = 0;
	char version[15] = "sched v1.03\0";
	UINT utemp;
	next_client = 0;
	char label[30];
	int index;
	UCHAR mask;
	UCHAR mask2;
	int sample_size = 10;
	msg.mtype = msgtype;

#ifdef SERVER_146
	printf("starting server\n");
	this_client_id = _SERVER;
#endif
#ifdef CL_150
	printf("starting 150\n");
	this_client_id = _150;
#endif
#ifdef CL_147
	printf("starting 147\n");
	this_client_id = _147;
#endif 
#ifdef CL_154
	printf("starting 154\n");
	this_client_id = _154;
#endif 
#ifdef CL_151
	printf("starting 151\n");
	this_client_id = _151;
#endif 
#if 1
	// since each card only has 20 ports then the 1st 2 port access bytes
	// are 8-bit and the 3rd is only 4-bits, so we have to translate the
	// inportstatus array, representing 3 byts of each 2 (3x8x2 = 48) to
	// one of the 40 actual bits as index
	// 2/15/23 - working with 4600 w/ just one card - 

	// the check_inputs & change_outputs functions
	// use the array to adjust from index to bank
	// since there are only 4 bits in banks 3 & 5
	//printf("starting...\n");

	real_banks[0].i = 0;
	real_banks[0].bank = 0;
	real_banks[0].index = 0;

	real_banks[1].i = 1;
	real_banks[1].bank = 0;
	real_banks[1].index = 1;

	real_banks[2].i = 2;
	real_banks[2].bank = 0;
	real_banks[2].index = 2;

	real_banks[3].i = 3;
	real_banks[3].bank = 0;
	real_banks[3].index = 3;

	real_banks[4].i = 4;
	real_banks[4].bank = 0;
	real_banks[4].index = 4;

	real_banks[5].i = 5;
	real_banks[5].bank = 0;
	real_banks[5].index = 5;

	real_banks[6].i = 6;
	real_banks[6].bank = 0;
	real_banks[6].index = 6;

	real_banks[7].i = 7;
	real_banks[7].bank = 0;
	real_banks[7].index = 7;

	real_banks[8].i = 8;
	real_banks[8].bank = 1;
	real_banks[8].index = 0;

	real_banks[9].i = 9;
	real_banks[9].bank = 1;
	real_banks[9].index = 1;

	real_banks[10].i = 10;
	real_banks[10].bank = 1;
	real_banks[10].index = 2;

	real_banks[11].i = 11;
	real_banks[11].bank = 1;
	real_banks[11].index = 3;

	real_banks[12].i = 12;
	real_banks[12].bank = 1;
	real_banks[12].index = 4;

	real_banks[13].i = 13;
	real_banks[13].bank = 1;
	real_banks[13].index = 5;

	real_banks[14].i = 14;
	real_banks[14].bank = 1;
	real_banks[14].index = 6;

	real_banks[15].i = 15;
	real_banks[15].bank = 1;
	real_banks[15].index = 7;

	real_banks[16].i = 16;
	real_banks[16].bank = 2;
	real_banks[16].index = 0;

	real_banks[17].i = 17;
	real_banks[17].bank = 2;
	real_banks[17].index = 1;

	real_banks[18].i = 18;
	real_banks[18].bank = 2;
	real_banks[18].index = 2;

	real_banks[19].i = 19;
	real_banks[19].bank = 2;
	real_banks[19].index = 3;

/*
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
		cs_index = cllist_get_size(&cll);
		printf("%d no recs in cllist\n",cs_index);
		cllist_show(&cll);
	}else printf("can't fine %s\n",cFileName);

	dllist_init(&dll);
	if(access(dFileName,F_OK) != -1)
	{
		dlLoadConfig(dFileName,&dll,dsize,errmsg);
		if(rc > 0)
		{
			printf("%s\r\n",errmsg);
		}
		//dllist_show(&dll);
	}else
	{
		memset(dtp,0,sizeof(D_DATA));
		printf("can't open data.dat\n");
		dlWriteConfig(dFileName, &dll,1,errmsg);
	}
	ds_index = dllist_get_size(&dll);

	init_ips();
	same_msg = 0;

	//printf("%s\n",version);
	j = k = i = 0;
	cmd = 0x21;
	//strcpy(password,"asdf1234\0");
	LoadParams("config.bin", &ps, password, errmsg);
	//printf("%s\n",errmsg);
	//printf("%d %d\n",ps.ds_interval, ps.ds_enable);
#endif

	while(TRUE)
	{
		cmd = 0;
		if(shutdown_all == 1)
		{
			//printf("shutting down cmd host\r\n");
			return 0;
		}
		uSleep(0,TIME_DELAY/10);
		// msg from sock 
		if (msgrcv(sched_qid, (void *) &msg, sizeof(msg.mtext), msgtype, MSG_NOERROR) == -1) 
		{
			if (errno != ENOMSG) 
			{
				perror("msgrcv");
				printf("msgrcv error\n");
				exit(EXIT_FAILURE);
			}
		}
		cmd = msg.mtext[0];
		//printf("sched cmd host: ");
		//print_cmd(cmd);
		msg_len = (int)msg.mtext[1];
		msg_len |= (int)(msg.mtext[2] << 4);

		//printf("msg_len: %d\n",msg_len);
		memset(tempx,0,sizeof(tempx));
		memcpy(tempx,msg.mtext+3,msg_len);
		onoff = tempx[0];

/*
for(i = 0;i < msg_len;i++)
	printf("%02x ",tempx[i]);

printf("\n");
*/
		if(cmd > 0)
		{
			rc = 0;

			switch(cmd)
			{
#ifdef SERVER_146
				case DESK_LIGHT:
				case EAST_LIGHT:
				case NORTHWEST_LIGHT:
				case SOUTHEAST_LIGHT:
				case MIDDLE_LIGHT:
				case WEST_LIGHT:
				case NORTHEAST_LIGHT:
				case SOUTHWEST_LIGHT:
				case WATER_HEATER:
				case WATER_PUMP:
				case WATER_VALVE1:
				case WATER_VALVE2:
				case WATER_VALVE3:
#endif
#ifdef CL_150
#warning "CL_150 defined"
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
#endif
#ifdef CL_147
#warning "CL_147 defined"
				case BENCH_24V_1:
				case BENCH_24V_2:
				case BENCH_12V_1:
				case BENCH_12V_2:
				case BENCH_5V_1:
				case BENCH_5V_2:
				case BENCH_3V3_1:
				case BENCH_3V3_2:
				case BENCH_LIGHT1:
				case BENCH_LIGHT2:
				case BATTERY_HEATER:
#endif 
#ifdef CL_154
#warning "CL_154 defined"
				case CABIN1:
				case CABIN2:
				case CABIN3:
				case CABIN4:
				case CABIN5:
				case CABIN6:
				case CABIN7:
				case CABIN8:
#endif 
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
				send_sock_msg(tempx, 1, cmd, _SERVER);
				WriteParams("config.bin", &ps, password, errmsg);
				return 1;
			}

			switch(cmd)
			{
				case GET_TEMP4:
					printf("ds_index: %d\n",ds_index);
					if(ds_index > 0 && ps.ds_enable > 0)
					{
						dllist_find_data(ds_index, dtpp, &dll);
						printf("%d:%d:%d - %s\n",dtp->hour, dtp->minute, dtp->second, lookup_raw_data(dtp->value));
						//printf("%d:%d:%d %d\n",dtp->hour, dtp->minute, dtp->second, dtp->value);
					}else printf("not enabled\n");
					//printf("avg: %d\n",avg_raw_data(sample_size));	not working yet
					break;

				case SEND_CLIENT_LIST:
					//printf("send client list :");
					send_sock_msg(tempx, msg_len, SEND_CLIENT_LIST, _SERVER);
					break;

				case DLLIST_SAVE:
					//dlWriteConfig("ddata.dat", &dll, index, errmsg);
					ds_reset = 1;
					break;

				case DLLIST_SHOW:
					dllist_show(&dll);
					break;

				case SET_DS_INTERVAL:
					ps.ds_interval = (int)tempx[0];
					if(ps.ds_interval == 8)
						ps.ds_enable = 0;
					else ps.ds_enable = 1;
					printf("ds interval: %d\n",ps.ds_interval);
					break;

				case SET_VALID_DS:
					//printf("set valid ds: %d\n",tempx[0]);
					mask = 1;
					for(i = 0;i < 7;i++)
						ps.valid_ds[i] = 0;
					for(i = 0;i < 6;i++)
					{
						if((mask & tempx[0]) == mask)
							ps.valid_ds[i] = 1;
						mask <<= 1;
					}
					break;

				case RELOAD_CLLIST:
					cllist_init(&cll);
					if(access(cFileName,F_OK) != -1)
					{
						csize = cs_index * sizeof(C_DATA);
						clLoadConfig(cFileName,&cll,csize,errmsg);
						if(rc > 0)
						{
							printf("%s\r\n",errmsg);
						}
						cllist_show(&cll);
					}
					cs_index = 0;
					break;

				case CLEAR_CLLIST:
					cllist_init(&cll);
					cs_index = 0;
					/*
					for(i = 0;i < cs_index;i++)
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
					*/
					curr_countdown_size = 0;
					break;

				case SHOW_CLLIST:
					//printf("show cllist\n");
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
					cttp = (C_DATA *)malloc(sizeof(C_DATA));
					cttp->index = (int)tempx[0];
					cttp->port = (int)tempx[1];
					cttp->state = (int)tempx[2];
					cttp->on_hour = (int)tempx[3];
					cttp->on_minute = (int)tempx[4];
					cttp->on_second = (int)tempx[5];
					cttp->off_hour = (int)tempx[6];
					cttp->off_minute = (int)tempx[7];
					cttp->off_second = (int)tempx[8];
					memset(label,0,sizeof(label));
					memcpy(label,&tempx[10],CLABELSIZE);
					strcpy(cttp->label,label);
//					if(cttp->on_hour == 0 && cttp->on_minute == 0 && cttp->on_second == 0 && cttp->off_hour == 0 
	//						&& cttp->off_minute == 0 && cttp->off_second == 0)
					
					//cllist_change_data(index,ctp,&cll);
					cllist_add_data(cttp->index, &cll, cttp);
					memset(tempx,0,sizeof(tempx));
					cs_index++;
					free(cttp);
					printf("done\n");
				break;

				case SAVE_CLLIST:
					csize = cs_index * sizeof(C_DATA);
					printf("cs_index: %d\n",cs_index);
					clWriteConfig(cFileName,&cll,csize,errmsg);
					break;

				case GET_ALL_CLLIST:
					printf("%d no recs in cllist\n",cs_index);
					for(i = 0;i < cs_index;i++)
					{
						j = cllist_find_data(i, ctpp, &cll);
						if(j == -1)
							break;
						//if(ctp->port > -1)
						if(1)
						{
							sprintf(tempx,"%02d %02d %02d %02d %02d %02d %02d %02d %02d %s",ctp->index, ctp->port, ctp->state, ctp->on_hour, ctp->on_minute, ctp->on_second, 
									ctp->off_hour, ctp->off_minute, ctp->off_second, ctp->label);
							printf("%s\n",tempx);
							cmd = REPLY_CLLIST;
							msg_len = strlen(tempx);
							send_sock_msg(tempx, msg_len, cmd, _149);
							uSleep(0,TIME_DELAY/2);
						}
					}
					break;
#if 1

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

				case SEND_TIMEUP:
					memset(tempx,0,sizeof(tempx));
					sprintf(tempx,"%d %d %d %d %d",this_client_id, trunning_days, trunning_hours, trunning_minutes, trunning_seconds);
					//printf("send timeup: %s\n",tempx);
					msg_len = strlen(tempx);
					uSleep(0,TIME_DELAY/4);
					send_sock_msg(tempx, msg_len, UPTIME_MSG, _SERVER);
					break;

				case UPTIME_MSG:
					//printf("uptime msg: %s\n",tempx);
					send_sock_msg(tempx, msg_len, UPTIME_MSG, _SERVER);
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
					send_sock_msg(tempx, msg_len, cmd, _SERVER);
					break;

				case SET_TIME:
					//printf("set time\n");
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
						//printf("PM\n");
						if(pt->tm_hour != 12)
							pt->tm_hour += 12;
					}else if(*pch == 'A' && pt->tm_hour == 12)
						pt->tm_hour -= 12;
					//printf("hour: %d\n",pt->tm_hour);

					curtime2 = mktime(pt);
					stime(pcurtime2);
					uSleep(0,TIME_DELAY/3);
					gettimeofday(&mtv, NULL);
					curtime2 = mtv.tv_sec;
					strftime(tempx,30,"%m-%d-%Y %T\0",localtime(&curtime2));
					//printf("%s\n",tempx);
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
#endif
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
					printf("ds_interval: %d\n",ps.ds_interval);
					printf("valid: \n");
					for(i = 0;i < 7;i++)
						printf("%d ",ps.valid_ds[i]);
					printf("\nenabled: %d\n",ps.ds_enable);
					break;

				default:
					//printf("default in main loop\n");
					break;
			}								  // end of switch
		}									  // if rc > 0
	}
	return test + 1;
}

void send_param_msg(void)
{
	char tempx[40];
/*
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
*/
//	send_msg(strlen((char*)tempx)*2,(UCHAR*)tempx, SEND_CONFIG);
}
/*********************************************************************/
void send_status_msg(char *msg)
{
//	send_msg(strlen((char*)msg)*2,(UCHAR*)msg, SEND_STATUS,_SERVER);
	printf("%s\n",msg);
}
