#include <stdio.h>
#include <string.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include <semaphore.h>
#include "mytypes.h"

extern CLIENT_TABLE client_table[MAX_CLIENTS];
void assign_client_table(void)
{
	int i;
	memset(client_table,0,sizeof(CLIENT_TABLE)*MAX_CLIENTS);
// 0
	strcpy(client_table[_149].ip,"149\0");
	strcpy(client_table[_149].label,"Second_Windows7\0");
	client_table[_149].socket = -1;
	client_table[_149].type = WINDOWS_CLIENT;
	client_table[_149].qkey = 1235;
	client_table[_149].qid = 0;
	client_table[_149].task_id = 0;
// 1
	strcpy(client_table[_159].ip,"159\0");
	strcpy(client_table[_159].label,"Win7-x64\0");
	client_table[_159].socket = -1;
	client_table[_159].type = WINDOWS_CLIENT;
	client_table[_159].qkey = 1236;
	client_table[_159].qid = 0;
	client_table[_159].task_id = 1;
// 2
	strcpy(client_table[_154].ip,"154\0");
	strcpy(client_table[_154].label,"Client154\0");
	client_table[_154].socket = -1;
	client_table[_154].type = TS_CLIENT;
	client_table[_154].qkey = 1238;
	client_table[_154].qid = 0;
	client_table[_154].task_id = 2;
// 3
	strcpy(client_table[_147].ip,"147\0");
	strcpy(client_table[_147].label,"Client147\0");
	client_table[_147].socket = -1;
	client_table[_147].type = TS_CLIENT;
	client_table[_147].qkey = 1239;
	client_table[_147].qid = 0;
	client_table[_147].task_id = 3;
// 4
	strcpy(client_table[_150].ip,"150\0");
	strcpy(client_table[_150].label,"Client150\0");
	client_table[_150].socket = -1;
	client_table[_150].type = TS_CLIENT;
	client_table[_150].qkey = 1240;
	client_table[_150].qid = 0;
	client_table[_150].task_id = 4;
// 5
	strcpy(client_table[_151].ip,"151\0");
	strcpy(client_table[_151].label,"Client151\0");
	client_table[_151].socket = -1;
	client_table[_151].type = TS_CLIENT;
	client_table[_151].qkey = 1241;
	client_table[_151].qid = 0;
	client_table[_151].task_id = 5;
// 6
	strcpy(client_table[_155].ip,"155\0");
	strcpy(client_table[_155].label,"Client155\0");
	client_table[_155].socket = -1;
	client_table[_155].type = TS_CLIENT;
	client_table[_155].qkey = 1242;
	client_table[_155].qid = 0;
	client_table[_155].task_id = 6;
// 7
	strcpy(client_table[_145].ip,"145\0");
	strcpy(client_table[_145].label,"Client145\0");
	client_table[_145].socket = -1;
	client_table[_145].type = TS_CLIENT;
	client_table[_145].qkey = 1244;
	client_table[_145].qid = 0;
	client_table[_145].task_id = 7;
// 8
	strcpy(client_table[_SERVER].ip,"146\0");
	strcpy(client_table[_SERVER].label,"Server146\0");
	client_table[_SERVER].socket = -1;
	client_table[_SERVER].type = TS_SERVER;
	client_table[_SERVER].qkey = 1245;
	client_table[_SERVER].qid = 0;
	client_table[_SERVER].task_id = 8;
}
