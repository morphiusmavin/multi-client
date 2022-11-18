// create odata.dat file - 1st and 2nd param are names of dat files to create
// any param as a 3rd will create the xml file (2nd param should be <filename>.xml)
// the data put into odata.dat file is read from the files: odata.csv
// the format for the odata.dat file is:
/*

the labels can be any text string and the 1st column must be in consecutive order with a total
 of 40 starting at 0, going to 39, no more or no less
*/

// TODO: if no csv file exists then just create a bare-bones dat file

#include <sys/types.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>
#include <errno.h>
#include <fcntl.h>
#include <ctype.h>
#include <assert.h>
#include <ctype.h>
#include "../queue/cllist_threads_rw.h"
#include "../ioports.h"
#include "cconfig_file.h"
#include "../../mytypes.h"

extern int cWriteConfigXML(char *filename, C_DATA *curr_o_array,size_t size,char *errmsg);
extern int cWriteConfig(char *filename, C_DATA *curr_o_array,size_t size,char *errmsg);
extern int cLoadConfig(char *filename, C_DATA *curr_o_array,size_t size,char *errmsg);
int copy_labels(char *filename, C_DATA *curr_o_data);

static char buf[3000];
static char label_array[NO_CLLIST_RECS][30];
static char int_array[NO_CLLIST_RECS][10];

/***********************************************************************************/
int main(int argc, char *argv[])
{
	C_DATA *curr_o_array;
	C_DATA *pod;
	int i,j,k;
	size_t osize;
	char errmsg[60];
	int ret;
	int do_xml = 0;
	char filename[30];
	char org_odata[30];

	if(argc < 2)
	{
		printf("usage: %s[odata filename]\n",argv[0]);
		printf("or add 2nd param to create xml format file\n");
		return 1;
	}
	strcpy(org_odata,argv[1]);
	printf("%s\n",org_odata);

	if(argc > 2)
	{
		printf("creating XML file\n");
		do_xml = 1;
	}

	printf("\nsizeof C_DATA: %lu\n",sizeof(C_DATA));
	i = NO_CLLIST_RECS;
	printf("NO_CLLIST_RECS: %d\n",i);
	osize = sizeof(C_DATA);
	osize *= i;
	printf("total size of c_data: %lu\n",osize);
	curr_o_array = (C_DATA *)malloc(osize);
	memset((void *)curr_o_array,0,osize);

	pod = curr_o_array;

	if(copy_labels(org_odata, curr_o_array) != 0)
	{
		printf("error in copy_labels\n");
		exit(0);
	}
	for(i = 0;i < NO_CLLIST_RECS;i++)
	{
		if(int_array[i][0] != i)
		{
			printf("bad index at: %d for C_DATA\n",i);
			printf("should be %d but is %d\n",i,int_array[i][0]);
			printf("all port no. must be in consecutive order starting at zero!\n");
			return 1;
		}
	}

	pod = curr_o_array;
	for(i = 0;i < osize/sizeof(C_DATA);i++)
	{
		pod->index = i;
		pod->port = int_array[i][1];
		pod->state = int_array[i][2];
		pod->on_hour = int_array[i][3];
		pod->on_minute = int_array[i][4];
		pod->on_second = int_array[i][5];
		pod->off_hour = int_array[i][6];
		pod->off_minute = int_array[i][7];
		pod->off_second = int_array[i][8];
		pod++;
	}

	if(do_xml == 1)
		ret = cWriteConfigXML(org_odata,curr_o_array,osize,errmsg);
	else
		ret = cWriteConfig(org_odata,curr_o_array,osize,errmsg);
	if(ret < 0)
		printf("cWriteConfig: %s\n",errmsg);
	else printf("%s %s has %lu records \n",errmsg,org_odata,osize/sizeof(C_DATA));

	pod = curr_o_array;
	if(do_xml == 0)
	{
//		printf("port\tonoff\tinput_port\tinput_type\ttype\ttime_delay\ttime_left\tpulse_time\treset\tlabel\n\n");
		for(i = 0;i < osize/sizeof(C_DATA);i++)
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
			pod++;
		}
		printf("sizeof: %ld\n",sizeof(C_DATA));
	}
	free(curr_o_array);
}

/***********************************************************************************/
int copy_labels(char *filename2, C_DATA *curr_o_data)
{
	char *fptr3;
	int i,j,k,m,n,p;
	char tempx[100];
	char tempy[10];
	char *pch;
	char *pch2;
	int fp;
	off_t fsize;
	C_DATA *pod;
	char errmsg[60];
	char filename[30];

	strcpy(filename,filename2);
	printf("filename: %s\n",filename);
	pch = &filename[0];
	i = 0;
	while(*pch != '.' && i < 30)
	{
		i++;
		printf("%c",*pch);
		pch++;
	}
	if(i > 25)
	{
		printf("filename doesn't have dot ext\n");
		return 1;
	}
	*pch = 0;
	strcat(filename,".csv\0");
	printf("\ncsv filename: %s\n",filename);

	fptr3 = filename;
	fp = open((const char *)fptr3, O_RDWR);
	if(fp < 0)
	{
		strcpy(errmsg,strerror(errno));
		close(fp);
		printf("%s  %s\n",errmsg,filename);
		printf("no csv file found!\n");
		return 1;
	}
	fsize = lseek(fp,0,SEEK_END);
	printf("fsize: %lu\n",fsize);

	i = lseek(fp,0,SEEK_SET);
	i = read(fp,(void*)&buf[0],fsize);
	close(fp);
	j = 0;
	k = 0;
	i = 0;
	pch = &buf[0];
	// search for 1st nl because the csv files have 1 nl at beginning

	while(*pch != '\n' && i < 2000)
	{
		pch++;
		i++;
		printf("%d ",i);
	}

	memset(int_array,0,sizeof(int_array));
	pch2 = pch;

	memset(label_array,0,sizeof(label_array));
	k = 0;
	for(j = 0;j < NO_CLLIST_RECS;j++)
	{
		m = 0;
		n = 0;
		p = 0;
		memset(tempx,0,sizeof(tempx));
		pch2 = pch;
		while(!isalpha(*pch))
		{
//			printf("%c",*pch);
			pch++;
			k++;
			tempx[m++] = *pch;
			n++;
			if(*pch == ',')
			{
				memset(tempy,0,sizeof(tempy));
				memcpy(tempy,(void*)(pch-n+1),n-1);
//				printf("%s ",tempy);
				int_array[j][p++] = atoi(tempy);
				n = 0;
				pch2 = pch;
			}
//			printf("\n");
		}
		tempx[m-1] = 0;
//		printf("%s ",tempx);
//		for(p = 0;p < 9;p++)
//			printf("%d ",int_array[j][p]);
//		printf("\n\n");
		pch2 = pch;
		i = 0;
		while(*pch2 != '\n' && k < fsize)
		{
			i++;
			pch2++;
			k++;
		}
		strncpy(&label_array[j][0],pch,i);
//		printf("%s\n",&label_array[j][0]);
		pch = pch2;
	}
//	printf("fp:%d  read: %d bytes in oLoadConfig\n",fp,i);

	pod = curr_o_data;

	for(i = 0;i < NO_CLLIST_RECS;i++)
	{
		strcpy(pod->label,label_array[i]);
		pod++;
	}
	return 0;
}
