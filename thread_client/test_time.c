#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <sys/time.h>
#include <unistd.h>

// this has to be called with 'sudo' to run
// in supervisory mode for stime to work 
// however, it doesn't set the system time 

void get_time();

int main(int argc, char *argv[])
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
	time_t T;
	struct tm tm;
	int ret;
	
	if(argc != 4)
	{
		printf("usage: %s dd/mm/yyyy hr:mn:ss [P][A]M\n",argv[0]);
		get_time();
		exit(1);
	}
/*	
	printf("%s\n",argv[1]);
	printf("%s\n",argv[2]);
	printf("%s\n",argv[3]);
*/
	strcpy(tempx,argv[1]);
	strcat(tempx," ");
	strcat(tempx,argv[2]);
	strcat(tempx," ");
	strcat(tempx,argv[3]);
	//strcpy(tempx,"12/13/2022 1:02:50 PM\0");
	printf("now setting the system time to: %s\n",tempx);
	msg_len = strlen(tempx);
	memset(temp_time,0,sizeof(temp_time));
	i = 0;
	pch = &tempx[0];

	while(*(pch++) != '/' && i < msg_len)
	{
		i++;
//		printf("%c",*pch);
	}
	memcpy(&temp_time[0],&tempx[0],i);
	i = atoi(temp_time);
//	printf("\nmon: %d\n",i - 1);
	pt->tm_mon = i - 1;
	i = 0;

	while(*(pch++) != '/' && i < msg_len)
	{
		i++;
//		printf("%c",*pch);
	}
	memset(temp_time,0,sizeof(temp_time));
	memcpy(temp_time,pch-i-1,i);
//	printf("%s\n",temp_time);
	i = atoi(temp_time);
	pt->tm_mday = i;
//	printf("day: %d\n",i);
	//		return 0;

	i = 0;
	while(*(pch++) != ' ' && i < msg_len)
	{
		i++;
//		printf("%c\n",*pch);
	}

	memset(temp_time,0,sizeof(temp_time));
	memcpy(temp_time,pch-3,2);
	i = atoi(temp_time);
	i += 100;
	pt->tm_year = i;
//	printf("year: %d\n",i-100);
	//		return 0;
	i = 0;

	while(*(pch++) != ':' && i < msg_len)
		i++;
	memset(temp_time,0,sizeof(temp_time));
	memcpy(temp_time,pch-i-1,i);
//	printf("%s \n",temp_time);
	i = atoi(temp_time);
	pt->tm_hour = i;
//	printf("hour: %d\n",i);
	//		return 0;

	i = 0;
	while(*(pch++) != ':' && i < msg_len)
		i++;
	memset(temp_time,0,sizeof(temp_time));
	memcpy(temp_time,pch-3,2);
//	printf("%s \n",temp_time);
	i = atoi(temp_time);
	pt->tm_min = i;
//	printf("min: %d\n",i);

	i = 0;
	while(*(pch++) != ' ' && i < msg_len)
		i++;
	memset(temp_time,0,sizeof(temp_time));
	memcpy(temp_time,pch-3,2);
//	printf("%s \n",temp_time);
	i = atoi(temp_time);
	pt->tm_sec = i;
//	printf("sec: %d\n",i);

//	printf("%c %x\n",*pch,*pch);
	if(*pch == 'P')
	{
		printf("PM\n");
		if(pt->tm_hour != 12)
			pt->tm_hour += 12;
	}else if(*pch == 'A' && pt->tm_hour == 12)
		pt->tm_hour -= 12;
	printf("hour: %d\n",pt->tm_hour);

	curtime2 = mktime(pt);
	printf("%d\n",curtime2);
	printf("%d\n",*pcurtime2);
	ret = stime(pcurtime2);
	printf("ret: %d\n",ret);
	gettimeofday(&mtv, NULL);
	curtime2 = mtv.tv_sec;
	strftime(tempx,30,"%m-%d-%Y %T\0",localtime(&curtime2));
//	printf("%s\n",tempx);
	usleep(10000);
	get_time();
	return 0;
}

void get_time()
{
	struct timeval mtv;
	time_t T;
	struct tm tm;
	char tempx[200];
	time_t curtime2;
	
	printf("\nnow get the time...\n\n");
// get the system time to see if it got set 
	gettimeofday(&mtv, NULL);
	curtime2 = mtv.tv_sec;
	strftime(tempx,30,"%m-%d-%Y %T\0",localtime(&curtime2));
	printf("%s\n",tempx);

	strftime(tempx,30,"%H",localtime(&curtime2));  // show as 24-hour (00 -> 23)
	printf("%s\n",tempx);
	strftime(tempx,30,"%I",localtime(&curtime2));	// show as 12-hour (01 -> 12)
	printf("%s\n",tempx);
	T = time(NULL);
	tm = *localtime(&T);
	printf("%02d:%02d:%02d\n", tm.tm_hour, tm.tm_min, tm.tm_sec);
}
