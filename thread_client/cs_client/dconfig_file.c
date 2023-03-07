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
#include "../../mytypes.h"
#include "../../ioports.h"
#include "../queue/dllist_threads_rw.h"
#include "dconfig_file.h"

/* change */
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
int dlLoadConfig(char *filename, dllist_t *dll, size_t size,char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i = 0;
	int j;
	fptr = (char *)filename;
	UCHAR id;
	D_DATA d_data;
	int no_recs;
	int ret = 0;
	//void *ptr;
	//UCHAR tempx[60];
	no_recs = dGetnRecs(filename, errmsg);
	
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
	printf("sizeof: %d\n",sizeof(D_DATA));
	for(i = 0;i < no_recs;i++)
	{
		ret += read(fp,&d_data,sizeof(D_DATA));
		//ret += read(fp,&tempx[0],sizeof(D_DATA));
/*		
		for(j = 0;j < 52;j++)
		{
			printf("%02x ",tempx[j]);
		}
		printf("\n");
*/
		//printf("%d ",ret);
		dllist_insert_data(i, dll, &d_data);
	}
	//printf("fp:%d  read: %d bytes in clLoadConfig\n",fp,ret);
	close(fp);
	strcpy(errmsg,"Success\0");
	return no_recs;
}
/////////////////////////////////////////////////////////////////////////////
int dlWriteConfig(char *filename,  dllist_t *dll, int no_recs, char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i,j,k;
	fptr = (char *)filename;
	D_DATA io;
	D_DATA *pio = &io;
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
//	printf("nrecs: %d\n",size/sizeof(D_DATA));
//	for(i = 0;i < size/sizeof(D_DATA);i++)
	for(i = 0;i < no_recs;i++)
	{
		dllist_find_data(i,&pio,dll);
		j += write(fp,(const void*)pio,sizeof(D_DATA));
	}

	close(fp);
	strcpy(errmsg,"Success\0");
	return 0;
}
#endif
/////////////////////////////////////////////////////////////////////////////
int dLoadConfig(char *filename, D_DATA *curr_o_array,size_t size,char *errmsg)
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
int dWriteConfig(char *filename, D_DATA *curr_o_array,size_t size,char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i,j,k;
	fptr = (char *)filename;
	D_DATA io;
	D_DATA *pio = &io;
	D_DATA *curr_o_array2 = curr_o_array;
	UCHAR id = 0xAA;

	printf("dWriteConfig - size: %d\n",size);

//#ifdef NOTARGET
	fp = open((const char *)fptr, O_RDWR | O_CREAT | O_TRUNC, S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
//#else
//	fp = open((const char *)fptr, O_WRONLY | O_CREAT, 666);
//#endif
/*
	if(fp < 0)
	{
		strcpy(errmsg,strerror(errno));
		close(fp);
		printf("%s  %s\n",errmsg,filename);
		return -2;
	}
*/
	j = 0;
	//printf("fp = %d\n",fp);
	//printf("seek=%lu\n",lseek(fp,0,SEEK_SET));
	i = lseek(fp,0,SEEK_SET);
	write(fp,&id,1);
//	for(i = 0;i < size/sizeof(D_DATA);i++)
	for(i = 0;i < size;i++)
	{
//		memset(pio,0,sizeof(IC_DATA));
		pio = curr_o_array2;
		j += write(fp,(const void*)pio,sizeof(D_DATA));
		//printf("%d ",j);
		curr_o_array2++;
	}

	close(fp);
	strcpy(errmsg,"Success\0");
	//printf("Success\n");
	return 0;
}
/////////////////////////////////////////////////////////////////////////////
int dGetnRecs(char *filename, char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i = 0;
	fptr = (char *)filename;
	UCHAR id;
	size_t fsize;
	int nrecs;

	fp = open((const char *)fptr, O_RDWR);
	if(fp < 0)
	{
		strcpy(errmsg,strerror(errno));
		close(fp);
		printf("%s  %s\n",errmsg,filename);
		return -2;
	}

	fsize = lseek(fp,0,SEEK_END);
	fsize--;
	nrecs = fsize/sizeof(D_DATA);
	printf("fsize: %d nrecs: %d\n",fsize,nrecs);
	fsize = lseek(fp,0,SEEK_SET);
	i = 0;
	read(fp,&id,1);
	if(id != 0xAA)
	{
		strcpy(errmsg,"invalid file format - id is not 0xAA\0");
		close(fp);
		return -1;
	}
//	printf("fp:%d  read: %d bytes in oLoadConfig\n",fp,i);
	close(fp);
	strcpy(errmsg,"Success\0");
	return nrecs;
}
///////////////////// Write/LoadConfig functions used by init/list_db start here (see make_db) ///////////////////////
/////////////////////////////////////////////////////////////////////////////
int dWriteConfig2(char *filename, D_DATA *curr_o_array,size_t size,char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i,j,k;
	fptr = (char *)filename;
	D_DATA io;
	D_DATA *pio = &io;
	D_DATA *curr_o_array2 = curr_o_array;
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
	for(i = 0;i < size/sizeof(D_DATA);i++)
	{
//		memset(pio,0,sizeof(IC_DATA));
		pio = curr_o_array2;
		j += write(fp,(const void*)pio,sizeof(D_DATA));
		curr_o_array2++;
	}

	close(fp);
	strcpy(errmsg,"Success\0");
	return 0;
}
/////////////////////////////////////////////////////////////////////////////
int dGetFileFormat2(char *filename)
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
int dgetFileCreationTime2(char *path,char *str)
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
int dWriteConfigXML(char *filename, D_DATA *curr_o_array,size_t size,char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i,j,k;
	fptr = (char *)filename;
	D_DATA io;
	D_DATA *pio = &io;
	D_DATA *curr_o_array2 = curr_o_array;
	char labels[11][20] = {"D_DATA","label","index","port","state","on_hour","on_minute","on_second","off_hour","off_minute","off_second"};
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

	for(i = 0;i < size/sizeof(D_DATA);i++)
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
				sprintf(temp,"%d",pio->index);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 3:
				sprintf(temp,"%d",pio->port);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 4:
				sprintf(temp,"%d",pio->state);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 5:
				sprintf(temp,"%d",pio->on_hour);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 6:
				sprintf(temp,"%d",pio->on_minute);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 7:
				sprintf(temp,"%d",pio->on_second);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 8:
				sprintf(temp,"%d",pio->off_hour);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 9:
				sprintf(temp,"%d",pio->off_minute);
				write(fp,(const void*)&temp[0],strlen(temp));
				break;
				case 10:
				sprintf(temp,"%d",pio->off_second);
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
