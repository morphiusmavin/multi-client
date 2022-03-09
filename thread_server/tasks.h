#ifndef __TASKS_H
#define  __TASKS_H

#define NUM_TASKS           	14
#define DEFAULT                 0
#define TIME_SLICE              1
#define FIFO                    2
#define PRIOR                   3
#define PTIME_SLICE             4
#define PFIFO                   5
#define INHERIT                 6

#define PROTOPORT			5193				  /* default protocol port number */
#define QLEN				6					  /* size of request queue        */
#define MSG_QUEUE_SIZE		50
#define MSG_CLIENT_QUEUE_SIZE 10

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
#define CMD_HOST_QKEY	1234


// uSleep(0,100000000L); - roughly 100ms using uSleep();

int global_socket;

enum task_types
{
	GET_HOST_CMD,
	MONITOR_INPUTS,
	MONITOR_INPUTS2,
	TIMER,
	TIMER2,
	WINCL_READ_TASK,
	WINCL_WRITE_TASK,
	READ_TASK1,
	SEND_TASK1,
	READ_TASK2,
	SEND_TASK2,
	TCP_MONITOR,
	SERIAL_RECV,
	BASIC_CONTROLS
} TASK_TYPES;

UCHAR get_host_cmd_task(int test);
UCHAR monitor_input_task(int test);
UCHAR monitor_fake_input_task(int test);
UCHAR timer_task(int test);
UCHAR timer2_task(int test);
UCHAR WinClReadTask(int test);
UCHAR WinClWriteTask(int test);
UCHAR ReadTask1(int test);
UCHAR SendTask1(int test);
UCHAR ReadTask2(int test);
UCHAR SendTask2(int test);
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
UCHAR get_client_msg_queue(void);
void add_client_msg_queue(UCHAR cmd);

int uSleep(time_t sec, long nanosec);
int put_sock(int sd, UCHAR *buf,int buflen, int block, char *errmsg);
int get_sock(int sd, UCHAR *buf, int buflen, int block, char *errmsg);
int get_msg(int sd);
void send_msg(int sd, int msg_len, UCHAR *msg, UCHAR msg_type);
int get_msgb(int sd);
void send_msgb(int sd, int msg_len, UCHAR *msg, UCHAR msg_type);
void *work_routine(void *arg);
int send_tcp(int sd, UCHAR *str,int len);
int recv_tcp(int sd, UCHAR *str, int strlen,int block);
void close_tcp(void);
int test_sd(int test);
int test_sock(void);
void init_ips(void);
void send_status_msg(char *msg);
void set_gps_baudrate(int baudrate);
void print_cmd(UCHAR cmd);
void assign_client_table(void);
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
	char ip[4];
	int socket;
	UCHAR msg_type;
	int msg_len;
	char message[20];
}CLIENTS;

// global variables
static UCHAR trunning_hours, trunning_minutes, trunning_seconds;

REAL_BANKS real_banks[40];

static int serial_recv_on;
static char dat_names[NUM_DAT_NAMES][DAT_NAME_STR_LEN];
float convertF(int raw_data);
extern char oFileName[20];
extern char iFileName[20];
int cmd_host_qid;

extern UCHAR reboot_on_exit;
//UCHAR upload_buf[UPLOAD_BUFF_SIZE];
static int same_msg;

#endif
