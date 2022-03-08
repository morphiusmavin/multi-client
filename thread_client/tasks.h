#ifndef __TASKS_H
#define  __TASKS_H

#define NUM_TASKS           	8
#define DEFAULT                 0
#define TIME_SLICE              1
#define FIFO                    2
#define PRIOR                   3
#define PTIME_SLICE             4
#define PFIFO                   5
#define INHERIT                 6
#define NO_CMDS					49
#define MAX_CLIENTS				12

#define PROTOPORT			5193				  /* default protocol port number */
#define QLEN				6					  /* size of request queue        */
#define MSG_QUEUE_SIZE		50

// params for usleep()
#define _5SEC	 	5000000 
#define _1SEC	 	1000000 
#define _100MS		100000 	
#define _10MS		10000 	
#define _1MS		1000 	

#define _2SEC	 	2000000 
#define _200MS		200000 	
#define _20MS		20000 	
#define _2MS		2000 	

#define _5SEC	 	5000000 
#define _500MS		500000 	
#define _50MS		50000 	
#define _5MS		5000 	

// uSleep(0,100000000L); - roughly 100ms using uSleep();

int global_socket;

enum task_types
{
	GET_HOST_CMD,
	MONITOR_INPUTS,
	MONITOR_INPUTS2,
	TIMER,
	TIMER2,
	TCP_MONITOR,
	SERIAL_RECV,
	BASIC_CONTROLS
} TASK_TYPES;

enum client_types
{
	WINDOWS_CLIENT,
	TS_CLIENT,
	SERVER,
	LINUX1,
	LINUX2
}CLIENT_TYPES;

UCHAR get_host_cmd_task(int test);
UCHAR monitor_input_task(int test);
UCHAR monitor_fake_input_task(int test);
UCHAR timer_task(int test);
UCHAR timer2_task(int test);
UCHAR serial_recv_task(int test);
UCHAR serial_recv_task2(int test);
UCHAR tcp_monitor_task(int test);
UCHAR basic_controls_task(int test);
int change_output(int index, int onoff);
int change_input(int index, int onoff);
void basic_controls(UCHAR code);
void send_serialother(UCHAR cmd, UCHAR *buf);
//void send_serialother2(UCHAR cmd, int size, UCHAR *buf);
void send_lcd(UCHAR *buf, int size);
void send_param_msg(void);
void add_msg_queue(UCHAR cmd);
UCHAR get_msg_queue(void);
void add_lcd_queue(UCHAR *item, int len);
UCHAR get_lcd_queue(void);
int tcp_connect(void);
int uSleep(time_t sec, long nanosec);
int put_sock(UCHAR *buf,int buflen, int block, char *errmsg);
int get_sock(UCHAR *buf, int buflen, int block, char *errmsg);
void set_sock(int open);
int get_msg(void);
void send_msg(int msg_len, UCHAR *msg, UCHAR msg_type);
void *work_routine(void *arg);
int send_tcp(UCHAR *str,int len);
int recv_tcp(UCHAR *str, int strlen,int block);
void close_tcp(void);
int test_sd(int test);
int test_sock(void);
void init_ips(void);
void send_status_msg(char *msg);
void set_gps_baudrate(int baudrate);
//double getDistance(double lat1, double lon1, double lat2, double lon2, int units);
typedef struct
{
	int i;
	int bank;
	int index;
}REAL_BANKS;

typedef struct
{
	int index;
	int onoff;
}SPECIAL_CMD_ARR;

typedef struct
{
	int socket;
	char ip[4];
	char label[30];
	int type;
}CLIENT_TABLE;

// global variables
static UCHAR trunning_hours, trunning_minutes, trunning_seconds;

REAL_BANKS real_banks[40];

static int serial_recv_on;
static char dat_names[NUM_DAT_NAMES][DAT_NAME_STR_LEN];
float convertF(int raw_data);
extern char oFileName[20];
extern char iFileName[20];

extern UCHAR reboot_on_exit;
//UCHAR upload_buf[UPLOAD_BUFF_SIZE];
static int same_msg;

#endif
