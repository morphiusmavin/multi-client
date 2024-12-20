// takes a text file with a list of .dat files from the dat_files directory in the D_DATA format and concatinates 
// them all one big dat file specified in the cmd line

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

int dlAppendConfig(char *filename1, char *filename2, int first_pass, char *errmsg); 

int silent_mode;
char *filename_array2[300];

/////////////////////////////////////////////////////////////////////////////
int main(int argc, char *argv[])
{
	D_DATA *dtp;
	D_DATA **dtpp = &dtp;

	char filename1[30];
	char filename2[30];
	char errmsg[30];
	int rc;
	int ending_line;
	int starting_line;
	int total_lines;
	int i;
	int j;
	char tempx[50];

	char *fptr1;
	FILE *fp1;
	silent_mode = 0;

	if(argc < 6)
	{
		printf("usage: <filename of bin file to create> <text file of list of bin source files > <starting line> <endding line> <silent mode>\n");
		printf("where bin file can be anyname, list of source files is what's in dat_files directory\n");
		printf("and starting line and ending line must be less than no. files in dir\n");
		printf("and silent mode can be number > 0  to turn off debug info\n");
		return 0;
	}
	strcpy(filename1,argv[1]);
	strcpy(filename2,argv[2]);
	starting_line = atoi(argv[3]);
	ending_line = atoi(argv[4]);
	silent_mode = atoi(argv[5]);

	if(silent_mode == 0)
		printf("%s %s %d %d %d\n",filename1, filename2,starting_line,ending_line,silent_mode);

	i = 0;
	fptr1 = (char *)filename2;
	fp1 = fopen (fptr1, "r");

	memset(tempx,0,sizeof(tempx));

	i = 0;
	total_lines = 0;
	while(fscanf(fp1, "%s", tempx)!=EOF)
	{
		//printf("%d: %s ",i,tempx);
		filename_array2[i] = malloc(30);
		strcpy(filename_array2[i],"dat_files/");
		strcat(filename_array2[i],tempx);
		if(silent_mode == 0)
			printf("%s\n",filename_array2[i]);
		i++;
		total_lines++;
	}
	fclose(fp1);
	if(silent_mode ==0)
		printf("no. actual lines: %d starting line %d ending line: %d\n",i,starting_line,ending_line);

	if(starting_line > ending_line)
	{		
		printf("starting is past ending lines %d %d\n",starting_line, ending_line);
		exit(1);
	}
	if(starting_line + ending_line > total_lines)
	{		
		printf("starting and ending greater than actual amount: %d %d\n",starting_line, ending_line);
		exit(1);
	}

	if(silent_mode == 0)
	for(i = starting_line;i < ending_line;i++)
		printf("%d: %s\n",i,filename_array2[i]);

	do
	{
		rc = dlAppendConfig(filename1, filename_array2[starting_line], 1, errmsg);
		if(silent_mode == 0)
			printf("%s\n",errmsg);
		starting_line++;
		if(silent_mode == 0)
			printf("return code: %d\n",rc);
	}while(rc != 0 && starting_line < total_lines);

	if(rc < 0)
	{
		printf("error on 1st pass\n");
		return 0;
	}
	for(i = 0;i < ending_line-1;i++)
	{
		rc = dlAppendConfig(filename1, filename_array2[i + starting_line+1], 0, errmsg);
		if(silent_mode == 0)
			printf("%s\n",errmsg);
		if(rc < 0)
		{
			printf("error on %d pass %d\n",rc,i);
		}
	}

	if(silent_mode == 0)
		printf("done\n");

	for(i = 0;i < j;i++)
	{
//		printf(" %d %s\n ",strlen(filename_array2[i]),filename_array2[i]);
		free(filename_array2[i]);
	}

	return 0;
}
/////////////////////////////////////////////////////////////////////////////
int dlAppendConfig(char *filename1, char *filename2, int first_pass, char *errmsg)
{
	char *fptr1;
	char *fptr2;
	int fp1 = -1;
	int fp2 = -1;
	int i,j,k;
	fptr1 = (char *)filename1;
	fptr2 = (char *)filename2;
	D_DATA io;
	D_DATA *pio = &io;
	UCHAR id = 0xAA;
	long cur_no_recs1;
	long filesize;
	long ftest;
	int ds_index = 0;
	UCHAR digsig;


	// file to create combining other 2 files 
	if(first_pass == 1)
		fp1 = open((const char *)fptr1, O_RDWR | O_CREAT | O_TRUNC, S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
	else
		fp1 = open((const char *)fptr1, O_RDWR | S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
	if(fp1 < 0)
	{
		strcpy(errmsg,strerror(errno));
		printf("error1\n");
		close(fp1);
		return -2;
	}

	//printf("%s\n",filename2);
	// file to get records from
	fp2 = open((const char *)fptr2, O_RDONLY | S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
	if(fp2 < 0)
	{
		strcpy(errmsg,strerror(errno));
		printf("error2\n");
		close(fp2);
		close(fp1);
		return -2;
	}
	// calculate the no. of recs.
	filesize = lseek(fp2,0,SEEK_END);
	if(silent_mode == 0)
		printf("filesize: %ld \t",filesize);
	if(filesize < 2)
	{
		if(silent_mode == 0)
			printf("bad filesize: %ld ",filesize);
		strcpy(errmsg,"bad filesize");
		return -4;
	}
	cur_no_recs1 = (filesize-1)/sizeof(D_DATA);

	if(silent_mode == 0)
		printf("%s:\t cur no recs: %ld  ", filename2, cur_no_recs1);
	ftest = filesize - 1;
	if(ftest % sizeof(D_DATA))
	{
		if(silent_mode == 0)
			printf("bad file %ld %ld ",ftest,ftest % sizeof(D_DATA));
		strcpy(errmsg,"bad file");
		return -5;
	}

	if(cur_no_recs1 < 28)
	{
		if(silent_mode == 0)
			printf("bad file: %s\n",filename2);
		strcpy(errmsg,"bad filesize");
		return -6;
	}
	lseek(fp2,0,SEEK_SET);
	read(fp2,&digsig,1);
	if(silent_mode == 0)
		printf("digsig: %02x ",digsig);
	if(digsig != 170)
	{
		if(silent_mode == 0)
			printf("bad digsig\n");
		strcpy(errmsg,"bad digsig");
		return -7;
	}
	lseek(fp2,1,SEEK_SET);

	j = 0;
	k = 0;

	if(first_pass == 1)
	{
		write(fp1,(void *)&digsig,1);
		lseek(fp1,1,SEEK_SET);
	}
	else 
		lseek(fp1,0,SEEK_END);

	for(i = 0;i < cur_no_recs1;i++)
	{
		k += read(fp2, (void*)pio,sizeof(D_DATA));
		//printf("%d\n",&pio->value);
		j += write(fp1,(void*)pio, sizeof(D_DATA));
		//printf("%d %d\n",j,k);
	}
	//printf("j: %d k: %d\n",j,k);
	close(fp1);
	close(fp2);
	strcpy(errmsg,"Success\0");
	return 0;
}
