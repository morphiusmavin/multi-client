#include <stdio.h>
#include <string.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include <semaphore.h>
#include "mytypes.h"

#ifdef SERVER
#warning "SERVER DEFINED"
extern CLIENT_TABLE1 client_table[MAX_CLIENTS];
void assign_client_table(void)
{
	int i;
	memset(client_table,0,sizeof(CLIENT_TABLE1)*MAX_CLIENTS);

/*
WINCL_READ_TASK1,		// 8
WINCL_READ_TASK2,		// 9
WINCL_READ_TASK3,		// 10
READ_TASK1,				// 11
SEND_TASK1,				// 12
READ_TASK2,				// 13
SEND_TASK2,				// 14
READ_TASK3,				// 15
SEND_TASK3,				// 16
READ_TASK4,				// 17
SEND_TASK4				// 18
*/
	
// 0
	strcpy(client_table[_149].ip,"149\0");
	strcpy(client_table[_149].label,"Second_Windows7\0");
	client_table[_149].socket = -1;
	client_table[_149].type = WINDOWS_CLIENT;
	client_table[_149].qkey = 1235;
	client_table[_149].qid = 0;
	client_table[_149].task_id = 7;
// 1
	strcpy(client_table[_153].ip,"153\0");
	strcpy(client_table[_153].label,"LAPTOP-C0BNP1TE\0");
	client_table[_153].socket = -1;
	client_table[_153].type = WINDOWS_CLIENT;
	client_table[_153].qkey = 1243;
	client_table[_153].qid = 0;
	client_table[_153].task_id = 8;
// 2
/*
	strcpy(client_table[_159].ip,"159\0");
	strcpy(client_table[_159].label,"Win7-x64\0");
	client_table[_159].socket = -1;
	client_table[_159].type = WINDOWS_CLIENT;
	client_table[_159].qkey = 1236;
	client_table[_159].qid = 0;
	client_table[_159].task_id = 10;
*/
// 3
	strcpy(client_table[_154].ip,"154\0");
	strcpy(client_table[_154].label,"TS_client1\0");
	client_table[_154].socket = -1;
	client_table[_154].type = TS_CLIENT;
	client_table[_154].qkey = 1238;
	client_table[_154].qid = 0;
	client_table[_154].task_id = 9;
// 4
	strcpy(client_table[_147].ip,"147\0");
	strcpy(client_table[_147].label,"TS_client2\0");
	client_table[_147].socket = -1;
	client_table[_147].type = TS_CLIENT;
	client_table[_147].qkey = 1239;
	client_table[_147].qid = 0;
	client_table[_147].task_id = 10;
// 5
	strcpy(client_table[_150].ip,"150\0");
	strcpy(client_table[_150].label,"TS_client3\0");
	client_table[_150].socket = -1;
	client_table[_150].type = TS_CLIENT;
	client_table[_150].qkey = 1240;
	client_table[_150].qid = 0;
	client_table[_150].task_id = 11;
// 6
	strcpy(client_table[_151].ip,"151\0");
	strcpy(client_table[_151].label,"TS_client4\0");
	client_table[_151].socket = -1;
	client_table[_151].type = TS_CLIENT;
	client_table[_151].qkey = 1241;
	client_table[_151].qid = 0;
	client_table[_151].task_id = 12;
// 7
	strcpy(client_table[_152].ip,"152\0");
	strcpy(client_table[_152].label,"TS_client5\0");
	client_table[_152].socket = -1;
	client_table[_152].type = TS_CLIENT;
	client_table[_152].qkey = 1242;
	client_table[_152].qid = 0;
	client_table[_152].task_id = 13;
// 8
	strcpy(client_table[_145].ip,"145\0");
	strcpy(client_table[_145].label,"TS_client6\0");
	client_table[_145].socket = -1;
	client_table[_145].type = TS_CLIENT;
	client_table[_145].qkey = 1244;
	client_table[_145].qid = 0;
	client_table[_145].task_id = 14;
// 9
	strcpy(client_table[_SERVER].ip,"146\0");
	strcpy(client_table[_SERVER].label,"TS_Server\0");
	client_table[_SERVER].socket = -1;
	client_table[_SERVER].type = TS_SERVER;
	client_table[_SERVER].qkey = 1245;
	client_table[_SERVER].qid = 0;
	client_table[_SERVER].task_id = -1;
	
//	for(i = 0;i < MAX_CLIENTS;i++)
//		printf("%d %d %s\n",i, client_table[i].task_id,client_table[i].label);
	
}
#else
#warning "SERVER NOT DEFINED"
extern CLIENT_TABLE2 client_table[MAX_CLIENTS];
void assign_client_table(void)
{
	memset(client_table,0,sizeof(CLIENT_TABLE2)*MAX_CLIENTS);

// 0
	strcpy(client_table[_149].ip,"149\0");
	strcpy(client_table[_149].label,"Second_Windows7\0");
	client_table[_149].socket = -1;
	client_table[_149].type = WINDOWS_CLIENT;
// 1
	strcpy(client_table[_153].ip,"153\0");
	strcpy(client_table[_153].label,"LAPTOP-C0BNP1TE\0");
	client_table[_153].socket = -1;
	client_table[_153].type = WINDOWS_CLIENT;
// 2
	strcpy(client_table[_159].ip,"159\0");
	strcpy(client_table[_159].label,"Win7-x64\0");
	client_table[_159].socket = -1;
	client_table[_159].type = WINDOWS_CLIENT;
// 3
	strcpy(client_table[_154].ip,"154\0");
	strcpy(client_table[_154].label,"TS_client1\0");
	client_table[_154].socket = -1;
	client_table[_154].type = TS_CLIENT;
// 4
	strcpy(client_table[_147].ip,"147\0");
	strcpy(client_table[_147].label,"TS_client2\0");
	client_table[_147].socket = -1;
	client_table[_147].type = TS_CLIENT;
// 5
	strcpy(client_table[_150].ip,"150\0");
	strcpy(client_table[_150].label,"TS_client3\0");
	client_table[_150].socket = -1;
	client_table[_150].type = TS_CLIENT;
// 6
	strcpy(client_table[_151].ip,"151\0");
	strcpy(client_table[_151].label,"TS_client4\0");
	client_table[_151].socket = -1;
	client_table[_151].type = TS_CLIENT;
// 7
	strcpy(client_table[_152].ip,"152\0");
	strcpy(client_table[_152].label,"TS_client5\0");
	client_table[_152].socket = -1;
	client_table[_152].type = TS_CLIENT;
// 8
	strcpy(client_table[_145].ip,"145\0");
	strcpy(client_table[_145].label,"TS_client7\0");
	client_table[_145].socket = -1;
	client_table[_145].type = TS_CLIENT;
// 9
	strcpy(client_table[_SERVER].ip,"146\0");
	strcpy(client_table[_SERVER].label,"TS_Server\0");
	client_table[_SERVER].socket = -1;
	client_table[_SERVER].type = TS_SERVER;

}
#endif
