#ifndef __TASKS_H
#define  __TASKS_H

#define NUM_SOCK_TASKS			3
#define NUM_SCHED_TASKS			7
#define DEFAULT                 0
#define TIME_SLICE              1
#define FIFO                    2
#define PRIOR                   3
#define PTIME_SLICE             4
#define PFIFO                   5
#define INHERIT                 6

#define PROTOPORT			5193				  /* default protocol port number */
#define QLEN				6					  /* size of request queue        */
//#define MSG_QUEUE_SIZE		50
#define SEND_CMD_HOST_QKEY	1234
#define RECV_CMD_HOST_QKEY	1300
#define BASIC_CONTROLS_QKEY	1301

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

enum sock_task_types
{
	GET_HOST_CMD1,
	TCP_MONITOR,
	RECV_MSG
} SOCK_TASK_TYPES;

enum sched_task_types
{
	GET_HOST_CMD2,
	MONITOR_INPUTS,
	MONITOR_INPUTS2,
	TIMER,
	TIMER2,
	SERIAL_RECV,
	BASIC_CONTROLS
} SCHED_TASK_TYPES;



void send_sock_msg(UCHAR *send_msg, int msg_len, UCHAR cmd, int dest);
UCHAR recv_msg_task(int test);
UCHAR get_host_cmd_task1(int test);
UCHAR get_host_cmd_task2(int test);
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
void add_msg_queue(UCHAR cmd, UCHAR onoff);
UCHAR get_msg_queue(void);
void add_lcd_queue(UCHAR *item, int len);
UCHAR get_lcd_queue(void);
int tcp_connect(void);
int uSleep(time_t sec, long nanosec);
int put_sock(UCHAR *buf,int buflen, int block, char *errmsg);
int get_sock(UCHAR *buf, int buflen, int block, char *errmsg);
void set_sock(int open);
int get_msg(void);
void send_msg(int msg_len, UCHAR *msg, UCHAR msg_type, UCHAR dest);
void *work_routine(void *arg);
int send_tcp(UCHAR *str,int len);
int recv_tcp(UCHAR *str, int strlen,int block);
void close_tcp(void);
int test_sd(int test);
int test_sock(void);
void init_ips(void);
void send_status_msg(char *msg);
void set_gps_baudrate(int baudrate);
void print_cmd(UCHAR cmd);
void assign_client_table(void);
void sort_countdown(void);

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
	int index;
	int port;
	int onoff;
	int hour;
	int minute;
	int second;
	int seconds_away;
}COUNTDOWN;

#define COUNTDOWN_SIZE 40

// global variables
int trunning_days, trunning_hours, trunning_minutes, trunning_seconds;

REAL_BANKS real_banks[40];

static int serial_recv_on;
static char dat_names[NUM_DAT_NAMES][DAT_NAME_STR_LEN];
float convertF(int raw_data);
extern char oFileName[20];
extern char cFileName[20];

UCHAR reboot_on_exit;
//UCHAR upload_buf[UPLOAD_BUFF_SIZE];
static int same_msg;
int basic_controls_qid;
int sock_qid;
int sched_qid;
key_t sock_key;
key_t sched_key;
key_t basic_controls_key;
int this_client_index;
int next_client;

#endif
