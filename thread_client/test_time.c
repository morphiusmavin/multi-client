#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

int main(void)
{
	time_t curtime2;
	time_t *pcurtime2 = &curtime2;
	char tempx[200];
	char *pch;
	int i,msg_len,j;
	char temp_time[5];
	struct timeval mtv;
	struct tm t;
	struct tm *pt = &t;
	
	memset(temp_time,0,sizeof(temp_time));
	i = 0;
	pch = &tempx[0];

	while(*(pch++) != '/' && i < msg_len)
	{
		i++;
	printf("%c",*pch);
	}
	memcpy(&temp_time[0],&tempx[0],i);
	i = atoi(temp_time);
	printf("\nmon: %d\n",i - 1);
	pt->tm_mon = i - 1;
	i = 0;

	while(*(pch++) != '/' && i < msg_len)
	{
		i++;
		printf("%c",*pch);
	}
	memset(temp_time,0,sizeof(temp_time));
	memcpy(temp_time,pch-i-1,i);
	printf("%s\n",temp_time);
	i = atoi(temp_time);
	pt->tm_mday = i;
	printf("day: %d\r\n",i);
	//		return 0;

	i = 0;
	while(*(pch++) != ' ' && i < msg_len)
	{
		i++;
		printf("%c\r\n",*pch);
	}

	memset(temp_time,0,sizeof(temp_time));
	memcpy(temp_time,pch-3,2);
	i = atoi(temp_time);
	i += 100;
	pt->tm_year = i;
	printf("year: %d\r\n",i-100);
	//		return 0;
	i = 0;

	while(*(pch++) != ':' && i < msg_len)
		i++;
	memset(temp_time,0,sizeof(temp_time));
	memcpy(temp_time,pch-i-1,i);
	printf("%s \n",temp_time);
	i = atoi(temp_time);
	pt->tm_hour = i;
	printf("hour: %d\r\n",i);
	//		return 0;

	i = 0;
	while(*(pch++) != ':' && i < msg_len)
		i++;
	memset(temp_time,0,sizeof(temp_time));
	memcpy(temp_time,pch-3,2);
	printf("%s \n",temp_time);
	i = atoi(temp_time);
	pt->tm_min = i;
	printf("min: %d\r\n",i);

	i = 0;
	while(*(pch++) != ' ' && i < msg_len)
		i++;
	memset(temp_time,0,sizeof(temp_time));
	memcpy(temp_time,pch-3,2);
	printf("%s \n",temp_time);
	i = atoi(temp_time);
	pt->tm_sec = i;
	printf("sec: %d\r\n",i);

	printf("%c %x\n",*pch,*pch);
	if(*pch == 'P')
	{
		printf("PM\n");
		pt->tm_hour += 12;
	}
	printf("hour: %d\n",pt->tm_hour);

	curtime2 = mktime(pt);
	stime(pcurtime2);
	uSleep(0,TIME_DELAY/3);
	gettimeofday(&mtv, NULL);
	curtime2 = mtv.tv_sec;
	strftime(tempx,30,"%m-%d-%Y %T\0",localtime(&curtime2));
	printf("%s\n",tempx);
	return 0;
}
/*
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
*/