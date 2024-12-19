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

int dlAppendConfig(char *filename1, char *filename2, char *append2file, int no_recs, char *errmsg);

dllist_t dll;

static int silent;


/////////////////////////////////////////////////////////////////////////////
int main(int argc, char *argv[])
{
	D_DATA *dtp;
	D_DATA **dtpp = &dtp;

	char filename1[30];
	char filename2[30];
	char filename3[30];
	char errmsg[30];
	int rc;
	silent = 0;

	if(argc > 1)
		silent = 1;

	strcpy(filename1,"test1.dat\0");
	strcpy(filename2,"test2.dat\0");
	strcpy(filename3,"test3.dat\0");

	printf("%s %s %s\n",filename1, filename2, filename3);
	printf("sizeof D_DATA: %ld\n",sizeof(D_DATA));

//	memset(dtp,0,sizeof(D_DATA));
	rc = dlAppendConfig(filename1, filename2, filename3, 1, errmsg);
	return 0;
}
/////////////////////////////////////////////////////////////////////////////
int dlAppendConfig(char *filename1, char *filename2, char *append2file, int no_recs, char *errmsg)
{
	char *fptr1;
	char *fptr2;
	char *fptr3;
	int fp1 = -1;
	int fp2 = -1;
	int fp3 = -1;
	int i,j,k;
	fptr1 = (char *)filename1;
	fptr2 = (char *)filename2;
	fptr3 = (char *)append2file;
	D_DATA io;
	D_DATA *pio = &io;
	UCHAR id = 0xAA;
	long cur_no_recs1;
	long cur_no_recs2;
	int ds_index = 0;
	UCHAR digsig;
	long total_no_recs = 0;

	// file to create combining other 2 files 
	fp3 = open((const char *)fptr3, O_RDWR | O_CREAT | O_TRUNC, S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
	if(fp3 < 0)
	{
		strcpy(errmsg,strerror(errno));
		printf("error1\n");
		close(fp3);
		return -2;
	}

	// first file to get records from
	fp2 = open((const char *)fptr2, O_RDWR | S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
	if(fp2 < 0)
	{
		strcpy(errmsg,strerror(errno));
		printf("error2\n");
		close(fp2);
		return -2;
	}
//	printf("%s\n %s\n %s\n",filename1, filename2, append2file);

	// calculate the no. of recs.
	cur_no_recs2 = lseek(fp2,0,SEEK_END);
	printf("filesize: %ld\n",cur_no_recs2);
	lseek(fp2,0,SEEK_SET);
	cur_no_recs2--;
	cur_no_recs2 /= sizeof(D_DATA);
	printf("%s: %ld\n", filename2, cur_no_recs2);
	total_no_recs = cur_no_recs2;
	read(fp2, &digsig,1);
//	printf("digsign: %02x\n",digsig);

	// open 2nd file to get recs from and calc no. recs.
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
	total_no_recs += cur_no_recs1;
	printf("%s: %ld\n",filename1, cur_no_recs1);

	j = 0;
	k = 0;
	i = read(fp1, &digsig, 1);
//	printf("digsign: %02x %d\n",digsig,i);

	write(fp3,(void *)&digsig,1);

	lseek(fp1,1,SEEK_SET);

	for(i = 0;i < cur_no_recs1;i++)
	{
		k += read(fp1, (void*)pio, sizeof(D_DATA));
		//printf("%d\n",&pio->value);
		j += write(fp3,(void*)pio, sizeof(D_DATA));
		//printf("%d %d\n",j,k);
	}
	printf("j: %d k: %d\n",j,k);
	close(fp1);

	k = 0;
	j  = 0;
	lseek(fp2,0,SEEK_SET);
	read(fp2, &digsig, 1);
	printf("digsign: %02x\n",digsig);

	lseek(fp2,1,SEEK_SET);

	for(i = 0;i < cur_no_recs2;i++)
	{
		k += read(fp2, (void*)pio, sizeof(D_DATA));
//		printf("%d\n",&pio->value);
		j += write(fp3,(void*)pio,sizeof(D_DATA));
	}
	printf("j: %d k: %d\n",j,k);
	close(fp2);
	lseek(fp3,0,SEEK_SET);
	j = 0;

	read(fp3,(void *)&digsig,1);
	//printf("%02x \n",digsig);
	if(silent == 0)
	for(i = 0;i < total_no_recs;i++)
	{
		printf("%d ",i);
		j += read(fp3, (const void*)pio, sizeof(D_DATA));
			printf("%2d\t%2d\t%2d\t%2d\t%2d\t%2d\t%2d\r\n",
				pio->sensor_no, pio->month, pio->day, pio->hour, pio->minute, pio->second, pio->value);
	}

	printf("j: %d k: %d\n",j,k);
	close(fp3);
	strcpy(errmsg,"Success\0");
	return 0;
}
