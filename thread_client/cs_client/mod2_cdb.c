// anything as a 2nd param will output csv w/ commas
#include <sys/types.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>
#include <errno.h>
#include <fcntl.h>
#include <ctype.h>
#include <assert.h>
#include "../queue/cllist_threads_rw.h"
#include "cconfig_file.h"
#include "../ioports.h"

extern int cLoadConfig(char *filename, C_DATA *curr_o_array,size_t size,char *errmsg);
extern int clLoadConfig(char *filename, cllist_t *oll, size_t size,char *errmsg);
extern int clWriteConfig(char *filename, cllist_t *oll, size_t size,char *errmsg);

extern int GetFileFormat2(char *filename);
cllist_t cll;

int main(int argc, char *argv[])
{
	C_DATA *curr_o_array;
	C_DATA *pod;
	char *fptr1;
	int num_valids = 0;
	int nrecs;
	int ret;

	int i,j;
	int comma_delim;
//	size_t isize;
	size_t osize;
	char errmsg[60];
	C_DATA *ctp;
	C_DATA **ctpp = &ctp;

	comma_delim = 0;
	if(argc < 2)
	{
		printf("usage: %s[odata filename][csv format]\n",argv[0]);
		printf("csv format is just an extra param\n");
		return 1;
	}
	else if(argc > 2)
	{
		comma_delim = 1;
		printf("outputing odata as csv format\n");
	}
	fptr1 = argv[1];
	printf("filename: %s \n",fptr1);

	if(GetFileFormat2(fptr1) != 1)
	{
		printf("%s does not have proper file format for input file\n",fptr1);
		return 1;
	}

	cllist_init(&cll);
	nrecs = cGetnRecs(fptr1, errmsg);

	osize = sizeof(C_DATA);
	osize *= nrecs;
	printf("\nsizeof C_DATA: %lu\n",sizeof(C_DATA));

	if(access(fptr1,F_OK) != -1)
	{
		ret = clLoadConfig(fptr1,&cll,osize,errmsg);
		if(ret > 0)
		{
//			myprintf1(errmsg);   
			printf("%s\r\n",errmsg);
		}
	}else printf("can't access %s\n",fptr1);

	cllist_show(&cll);
	int port = 6;
	ret = cllist_find_data(port,ctpp,&cll);
	ctp->on_hour = 10;
	//printf("my label: %s\n",ctp->label);
	strcpy(ctp->label,"test lkjh");
	//cllist_insert_data(port,&cll,ctpp);

	cllist_change_data(port,ctp,&cll);
	cllist_show(&cll);
//	free(curr_o_array);
	exit(0);
/*
	port = 18;
	ret = cllist_find_data(index,ctpp,&cll);
	ctp->port = 12;
	ctp->type = 7;
	strcpy(ctp->label,"test3");
	cllist_insert_data(index,&cll,ctp);
	port++;

	ret = cllist_find_data(index,ctpp,&cll);
	ctp->port = 11;
	ctp->type = 8;
	strcpy(ctp->label,"test4");
	cllist_insert_data(index,&cll,ctp);
	port++;

	ret = cllist_find_data(index,ctpp,&cll);
	ctp->port = 10;
	ctp->type = 9;
	strcpy(ctp->label,"test5");
	cllist_insert_data(index,&cll,ctp);
*/
	cllist_show(&cll);
/*
	ctp->index = 10;
	ctp->cmd = 6;
	strcpy(ctp->label,"test2");
//	rc = cllist_find_data(4,ctpp,&cll);
	cllist_insert_data(nrecs+1,&cll,ctp);

	cllist_reorder(&cll);
	cllist_show(&cll);
//	cllist_insert_data(4,&cll,ctp);
//	cllist_insert_data(nrecs+1, &cll, datap2);
	osize += sizeof(C_DATA);
	clWriteConfig(fptr1,&cll,osize,errmsg);
*/
	return 0;

	pod = curr_o_array;

	printf("\n");
/*
	if(comma_delim == 0)
		printf("port\tonoff\tinput_port\tinput_type\ttype\ttime_delay\tlabel\n\n");
*/
	for(i = 0;i < osize/sizeof(C_DATA);i++)
	{
		if(pod->label[0] != 0)
		{
			if(comma_delim == 1)
			{
				printf("%d,%d,%d,%d,%d,%d,%d,%d,%d,%s\n",
				pod->index,
				pod->port,
				pod->state,
				pod->on_hour,
				pod->on_minute,
				pod->on_second,
				pod->off_hour, 
				pod->off_minute,
				pod->off_second,
				pod->label);
			}else
			{
				printf("%d %d %d %d %d %d %d %d %d %s\n",
				pod->index,
				pod->port,
				pod->state,
				pod->on_hour,
				pod->on_minute,
				pod->on_second,
				pod->off_hour, 
				pod->off_minute,
				pod->off_second,
				pod->label);
			}
		}
		pod++;
	}

	pod = curr_o_array;
	pod++;
	pod++;
	pod->port = 9;
	pod = curr_o_array;

	num_valids = 0;
	for(i = 0;i < nrecs;i++)
	{
		if(pod->label[0] != 0)
			num_valids++;
		pod++;
	}
//	osize -= sizeof(C_DATA);
	ret = cWriteConfig(fptr1,curr_o_array,osize,errmsg);
	printf("ret: %d\n",ret);
	printf("num valid records: %d\n",num_valids);
	printf("\n\n");
	printf("sizeof: %ld \n",sizeof(C_DATA));

	free(curr_o_array);
}

