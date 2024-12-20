// read contents of dat file created in D_DATA format with test2d.c

#include <sys/types.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>
#include <errno.h>
#include <fcntl.h>
#include <ctype.h>
#include <sys/stat.h>
#include <assert.h>
#include "../mytypes.h"
#include "dllist_threads_rw.h"

int dlAppendConfig(char *filename1, int no_recs, char *errmsg);

dllist_t dll;

static int silent;


/////////////////////////////////////////////////////////////////////////////
int main(int argc, char *argv[])
{
	D_DATA *dtp;
	D_DATA **dtpp = &dtp;

	char filename1[30];
	char errmsg[30];
	int rc;
	silent = 0;

	if(argc == 0)
	{	
		printf("useage: %s <input file>",argv[0]);
		return 0;
	}

	strcpy(filename1,argv[1]);

	printf("%s \n",filename1);

//	memset(dtp,0,sizeof(D_DATA));
	rc = dlAppendConfig(filename1, 1, errmsg);
	return 0;
}
/////////////////////////////////////////////////////////////////////////////
int dlAppendConfig(char *filename1, int no_recs, char *errmsg)
{
	char *fptr1;
	int fp1 = -1;
	int i,j,k;
	fptr1 = (char *)filename1;
	D_DATA io;
	D_DATA *pio = &io;
	UCHAR id = 0xAA;
	long cur_no_recs1;
	int ds_index = 0;
	UCHAR digsig;

	// open file to get recs from and calc no. recs.
	fp1 = open((const char *)fptr1, O_RDWR | S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
	if(fp1 < 0)
	{
		strcpy(errmsg,strerror(errno));
		close(fp1);
		return -2;
	}

	lseek(fp1,0,SEEK_SET);
	cur_no_recs1 = lseek(fp1,0,SEEK_END);
	printf("filesize: %ld\n",cur_no_recs1);
	lseek(fp1,0,SEEK_SET);
	cur_no_recs1--;
	cur_no_recs1 /= sizeof(D_DATA);
	printf("%s: %ld\n",filename1, cur_no_recs1);

	j = 0;
	k = 0;
	i = read(fp1, &digsig, 1);
	printf("digsign: %02x %d\n",digsig,i);

	lseek(fp1,1,SEEK_SET);

	for(i = 0;i < cur_no_recs1;i++)
	{
		printf("%d ",i);
		j += read(fp1, (const void*)pio, sizeof(D_DATA));
			printf("%2d\t%2d\t%2d\t%2d\t%2d\t%2d\t%2d\r\n",
				pio->sensor_no, pio->month, pio->day, pio->hour, pio->minute, pio->second, pio->value);
	}

	printf("j: %d k: %d\n",j,k);
	close(fp1);
	strcpy(errmsg,"Success\0");
	return 0;
}
