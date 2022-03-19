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

extern int clLoadConfig(char *filename, cllist_t *oll, size_t size,char *errmsg);
extern int GetFileFormat2(char *filename);
extern int cllist_show(cllist_t *llistp);

cllist_t oll;

int main(int argc, char *argv[])
{
	C_DATA *otp;
	C_DATA **otpp = &otp;
	char *fptr1;
	int num_valids = 0;

	int i,j,k;
	int comma_delim;
	size_t osize;
	char errmsg[60];
	int rc;

	comma_delim = 0;
	if(argc < 2)
	{
		printf("usage: %s[odata filename][csv format]\n",argv[0]);
		return 1;
	}
	else if(argc > 2)
	{
		comma_delim = 1;
		printf("outputing odata as csv format\n");
	}
	fptr1 = argv[1];

	if(GetFileFormat2(fptr1) != 1)
	{
		printf("%s does not have proper file format for input file\n",fptr1);
		return 1;
	}

	cllist_init(&oll);
	if(access(fptr1,F_OK) != -1)
	{
		rc = clLoadConfig(fptr1,&oll,osize,errmsg);
		if(rc > 0)
		{
			printf(errmsg);
		}
	}

/*
	for(i = 0;i < 40;i++)
	{
		cllist_find_data(i,&otp,&oll);
		if(otp->input_port != 0xFF)
			printf("port: %2d\t\tinput: %2d\t\t %s\n",otp->port,otp->input_port,otp->label);
	}
	printf("\n");
*/
	j = 0;
	for(i = 0;i < NO_CLLIST_RECS;i++)
	{
		printf("%2d\t\t%2d\t %s\n",i,j,otp->label);
	}
	printf("\n");

	return 0;
}

