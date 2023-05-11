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
#include "../ioports.h"
#include "../queue/sllist_threads_rw.h"
#include "sconfig_file.h"
/////////////////////////////////////////////////////////////////////////////
int slLoadConfig(char *filename, sllist_t *dll, size_t size,char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i = 0;
	int j;
	fptr = (char *)filename;
	UCHAR id;
	S_DATA s_data;
	int no_recs;
	int ret = 0;
	//void *ptr;
	//UCHAR tempx[60];
	no_recs = sGetnRecs(filename, errmsg);
	
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
	//printf("sizeof: %d\n",sizeof(D_DATA));
	for(i = 0;i < no_recs;i++)
	{
		ret += read(fp,&s_data,sizeof(S_DATA));
		//ret += read(fp,&tempx[0],sizeof(S_DATA));
/*		
		for(j = 0;j < 52;j++)
		{
			printf("%02x ",tempx[j]);
		}
		printf("\n");
*/
		//printf("%d ",ret);
		sllist_insert_data(i, dll, &s_data);
	}
	//printf("fp:%d  read: %d bytes in clLoadConfig\n",fp,ret);
	close(fp);
	strcpy(errmsg,"Success\0");
	return no_recs;
}
/////////////////////////////////////////////////////////////////////////////
int slWriteConfig(char *filename,  sllist_t *dll, int no_recs, char *errmsg)
{
	char *fptr;
	int fp = -1;
	int i,j,k;
	fptr = (char *)filename;
	S_DATA io;
	S_DATA *pio = &io;
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
//	printf("nrecs: %d\n",size/sizeof(S_DATA));
//	for(i = 0;i < size/sizeof(S_DATA);i++)
	for(i = 0;i < no_recs;i++)
	{
		sllist_find_data(i,&pio,dll);
		j += write(fp,(const void*)pio,sizeof(S_DATA));
	}

	close(fp);
	strcpy(errmsg,"Success\0");
	return 0;
}
/////////////////////////////////////////////////////////////////////////////
int sGetnRecs(char *filename, char *errmsg)
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
	nrecs = fsize/sizeof(S_DATA);
	//printf("fsize: %d nrecs: %d\n",fsize,nrecs);
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
/////////////////////////////////////////////////////////////////////////////
int sgetFileCreationTime2(char *path,char *str)
{
// MM:DD-HH:MM:SS
    struct stat attr;
    stat(path, &attr);
    strncpy(str,ctime(&attr.st_mtime),24);
    *(str + 19) = 0;
    strcpy(str,str+4);
	return 0;
}
