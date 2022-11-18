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
#include "../queue/cllist_threads_rw.h"
#include "../ioports.h"
#include "cconfig_file.h"

static char open_br = '<';
static char close_br = '>';
static char open_br_slash[2] = "</";
static char nl = 0x0A;
static char tabx = 0x09;
static char first_line[] = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>";
static char table[6] = "Table\0";
static char tick = '\'';
static char space = 0x21;

/////////////////////////////////////////////////////////////////////////////
// CONFIG_FILE is the define used for compiling the list_db and init_db programs
// so (i/ol)(Load/Write)Config is used by sched/tasks etc
#ifndef CONFIG_FILE
/////////////////////////////////////////////////////////////////////////////
int clLoadConfig(char *filename, cllist_t *oll, size_t size,char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i = 0;
	fptr = (char *)filename;
	UCHAR id;
	C_DATA o_data;
	int ret = 0;

	fp = open((const char *)fptr, O_RDWR);
	if(fp < 0)
	{
		strcpy(errmsg,strerror(errno));
		close(fp);
		printf("%s  %s\n",errmsg,filename);
		return -2;
	}

	i = lseek(fp,0,SEEK_SET);
	i = 0;
	read(fp,&id,1);
	if(id != 0xAA)
	{
		strcpy(errmsg,"invalid file format - id is not 0x55\0");
		close(fp);
		printf("invalid file format\n");
		return -1;
	}
	for(i = 0;i < NO_CLLIST_RECS;i++)
	{
		ret += read(fp,&o_data,sizeof(C_DATA));
		cllist_insert_data(i, oll, &o_data);
	}
//	printf("fp:%d  read: %d bytes in clLoadConfig\n",fp,ret);
	close(fp);
	strcpy(errmsg,"Success\0");
	return 0;
}
/////////////////////////////////////////////////////////////////////////////
int clWriteConfig(char *filename,  cllist_t *oll, size_t size,char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i,j,k;
	fptr = (char *)filename;
	C_DATA io;
	C_DATA *pio = &io;
	UCHAR id = 0xAA;

//#ifdef NOTARGET
	fp = open((const char *)fptr, O_RDWR | O_CREAT | O_TRUNC, S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
//#else
//	fp = open((const char *)fptr, O_WRONLY | O_CREAT, 666);
//#endif
	if(fp < 0)
	{
		strcpy(errmsg,strerror(errno));
		close(fp);
		return -2;
	}

	j = 0;
//	printf("fp = %d\n",fp);
//	printf("seek=%lu\n",lseek(fp,0,SEEK_SET));
	i = lseek(fp,0,SEEK_SET);
	write(fp,&id,1);
	for(i = 0;i < size/sizeof(C_DATA);i++)
	{
		cllist_find_data(i,&pio,oll);
		j += write(fp,(const void*)pio,sizeof(C_DATA));

	}

	close(fp);
	strcpy(errmsg,"Success\0");
	return 0;
}
#endif
/////////////////////////////////////////////////////////////////////////////
int cLoadConfig(char *filename, C_DATA *curr_o_array,size_t size,char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i = 0;
	fptr = (char *)filename;
	UCHAR id;

	fp = open((const char *)fptr, O_RDWR);
	if(fp < 0)
	{
		strcpy(errmsg,strerror(errno));
		close(fp);
		printf("%s  %s\n",errmsg,filename);
		return -2;
	}

	i = lseek(fp,0,SEEK_SET);
	i = 0;
	read(fp,&id,1);
	if(id != 0xAA)
	{
		strcpy(errmsg,"invalid file format - id is not 0xAA\0");
		close(fp);
		return -1;
	}
	i = read(fp,(void*)curr_o_array,size);
//	printf("fp:%d  read: %d bytes in oLoadConfig\n",fp,i);
	close(fp);
	strcpy(errmsg,"Success\0");
	return 0;
}
///////////////////// Write/LoadConfig functions used by init/list_db start here (see make_db) ///////////////////////
/////////////////////////////////////////////////////////////////////////////
int cWriteConfig(char *filename, C_DATA *curr_o_array,size_t size,char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i,j,k;
	fptr = (char *)filename;
	C_DATA io;
	C_DATA *pio = &io;
	C_DATA *curr_o_array2 = curr_o_array;
	UCHAR id = 0xAA;

//#ifdef NOTARGET
	fp = open((const char *)fptr, O_RDWR | O_CREAT | O_TRUNC, S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
//#else
//	fp = open((const char *)fptr, O_WRONLY | O_CREAT, 666);
//#endif
	if(fp < 0)
	{
		strcpy(errmsg,strerror(errno));
		close(fp);
		printf("%s  %s\n",errmsg,filename);
		return -2;
	}

	j = 0;
//	printf("fp = %d\n",fp);
//	printf("seek=%lu\n",lseek(fp,0,SEEK_SET));
	i = lseek(fp,0,SEEK_SET);
	write(fp,&id,1);
	for(i = 0;i < size/sizeof(C_DATA);i++)
	{
//		memset(pio,0,sizeof(IC_DATA));
		pio = curr_o_array2;
		j += write(fp,(const void*)pio,sizeof(C_DATA));
		curr_o_array2++;
	}

	close(fp);
	strcpy(errmsg,"Success\0");
	return 0;
}
/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
int GetFileFormat2(char *filename)
{
	char *fptr;
	int fp = -1;
	int i = 0;
	fptr = (char *)filename;
	UCHAR id;
	char errmsg[40];
	fp = open((const char *)fptr, O_RDONLY, S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
//	fp = open((const char *)fptr, O_RDONLY);
	if(fp < 0)
	{
		strcpy(errmsg,strerror(errno));
		close(fp);
		printf("%s  %s\n",errmsg,filename);
		return -2;
	}

	i = lseek(fp,0,SEEK_SET);
	i = 0;
	read(fp,&id,1);
//	printf("%x %s\n",id,filename);
	if(id == 0xAA)	// output file
		return 1;
	else return -1;
	close(fp);
	strcpy(errmsg,"Success\0");
	return 0;
}
/////////////////////////////////////////////////////////////////////////////
int getFileCreationTime2(char *path,char *str)
{
// MM:DD-HH:MM:SS
    struct stat attr;
    stat(path, &attr);
    strncpy(str,ctime(&attr.st_mtime),24);
    *(str + 19) = 0;
    strcpy(str,str+4);
	return 0;
}
/////////////////////////////////////////////////////////////////////////////
int cWriteConfigXML(char *filename, C_DATA *curr_o_array,size_t size,char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i,j,k;
	fptr = (char *)filename;
	C_DATA io;
	C_DATA *pio = &io;
	C_DATA *curr_o_array2 = curr_o_array;
	char labels[11][20] = {"C_DATA","label","port","onoff","input_port",
			"input_type","type","time_delay","time_left","pulse_time","reset"};
	char temp[5];
	char tempx[30];

//#ifdef NOTARGET
	fp = open((const char *)fptr, O_RDWR | O_CREAT | O_TRUNC, S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
//#else
//	fp = open((const char *)fptr, O_WRONLY | O_CREAT, 666);
//#endif
	if(fp < 0)
	{
		strcpy(errmsg,strerror(errno));
		close(fp);
		printf("%s  %s\n",errmsg,filename);
		return -2;
	}

	j = 0;
//	printf("fp = %d\n",fp);
//	printf("seek=%lu\n",lseek(fp,0,SEEK_SET));
	i = lseek(fp,0,SEEK_SET);

	write(fp,(const void*)&first_line[0],strlen(first_line));
	write(fp,(const void*)&nl,1);
	write(fp,(const void*)&open_br,1);
	write(fp,(const void*)&table[0],strlen(table));
	write(fp,(const void*)&close_br,1);
	write(fp,(const void*)&nl,1);

	for(i = 0;i < size/sizeof(C_DATA);i++)
	{
//		memset(pio,0,sizeof(IO_DATA));
		pio = curr_o_array2;

		write(fp,(const void*)&tabx,1);
		write(fp,(const void*)&open_br,1);
		write(fp,(const void*)&labels[0],strlen(labels[0]));
		write(fp,(const void*)&close_br,1);
		write(fp,(const void*)&nl,1);

		for(j = 1;j < 11;j++)
		{
			write(fp,(const void*)&tabx,1);
			write(fp,(const void*)&tabx,1);
			write(fp,(const void*)&open_br,1);
			write(fp,(const void*)&labels[j],strlen(labels[j]));
			write(fp,(const void*)&close_br,1);
/*
			switch(j)
			{
				case 1:
				sprintf(tempx,pio->label,strlen(pio->label));
				write(fp,(const void*)&tempx[0],strlen(tempx));
				break;
				case 2:
				sprintf(temp,"%d",pio->cmd);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 3:
				sprintf(temp,"%d",pio->dest);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 4:
				sprintf(temp,"%d",pio->msg_len);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 5:
				sprintf(temp,"%d",pio->hours);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 6:
				sprintf(temp,"%d",pio->minutes);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 7:
				sprintf(temp,"%d",pio->seconds);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
			}
*/
			write(fp,(const void*)&open_br_slash,2);
			write(fp,(const void*)&labels[j],strlen(labels[j]));
			write(fp,(const void*)&close_br,1);
			write(fp,(const void*)&nl,1);
		}
		write(fp,(const void*)&tabx,1);
		write(fp,(const void*)&open_br_slash,2);
		write(fp,(const void*)&labels[0],strlen(labels[0]));
		write(fp,(const void*)&close_br,1);
		write(fp,(const void*)&nl,1);
		curr_o_array2++;
	}
//	write(fp,(const void*)&nl,1);
	write(fp,(const void*)&open_br_slash,2);
	write(fp,(const void*)&table[0],strlen(table));
	write(fp,(const void*)&close_br,1);
	write(fp,(const void*)&nl,1);

	close(fp);
	strcpy(errmsg,"Success\0");
	return 0;
}
/////////////////////////////////////////////////////////////////////////////
