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
#include "../ioports.h"

extern int cLoadConfig(char *filename, C_DATA *curr_o_array,size_t size,char *errmsg);
extern int GetFileFormat2(char *filename);

int main(int argc, char *argv[])
{
	C_DATA *curr_o_array;
	C_DATA *pod;
	char *fptr1;
	int num_valids = 0;

	int i,j;
	int comma_delim;
//	size_t isize;
	size_t osize;
	char errmsg[60];

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

	i = NO_CLLIST_RECS;

	osize = sizeof(C_DATA);
	osize *= i;
	printf("\nsizeof C_DATA: %lu\n",sizeof(C_DATA));

	curr_o_array = (C_DATA *)malloc(osize);
	memset((void *)curr_o_array,0,osize);

	if(cLoadConfig(fptr1,curr_o_array,osize,errmsg) < 0)
	{
		printf("%s\n",errmsg);
		return -1;
	}

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
				printf("%d,%d,%d,%d,%d,%d,%d,%d,%d,%d,%s\n",
				pod->index,
				pod->client_no,
				pod->cmd,
				pod->dest,
				pod->msg_len, 
				pod->fn_ptr,
				pod->data_ptr,
				pod->hours,
				pod->minutes,
				pod->seconds,
				pod->label);
			}else
			{
				printf("%d %d %d %d %d %d %d %d %d %d %s\n",
				pod->index,
				pod->client_no,
				pod->cmd,
				pod->dest,
				pod->msg_len, 
				pod->fn_ptr,
				pod->data_ptr,
				pod->hours,
				pod->minutes,
				pod->seconds,
				pod->label);
			}
		}
		pod++;
	}

	pod = curr_o_array;
	num_valids = 0;
	for(i = 0;i < NO_CLLIST_RECS;i++)
	{
		if(pod->label[0] != 0)
			num_valids++;
		pod++;
	}
	printf("num valid records: %d\n",num_valids);
	printf("\n\n");
	printf("sizeof: %ld \n",sizeof(C_DATA));

	free(curr_o_array);
}

