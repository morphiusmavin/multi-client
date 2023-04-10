/********************************************************
 * An example source module to accompany...
 *
 * "Using POSIX Threads: Programming with Pthreads"
 *     by Brad nichols, Dick Buttlar, Jackie Farrell
 *     O'Reilly & Associates, Inc.
 *
 ********************************************************
 * llist_threads_rw.c
 *
 * Linked list library with reader/writer locks for
 * threads support
 */

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "../mytypes.h"
//#include "../serial_io.h"
#include "cllist_threads_rw.h"
/******************************************************************************/
int cllist_init (cllist_t *llistp)
{
	int rtn;

	llistp->first = NULL;
	if ((rtn = pthread_rdwr_init_np(&(llistp->rwlock), NULL)) !=0)
		fprintf(stderr, "pthread_rdwr_init_np error %d",rtn), exit(1);
//	printf("sizeof O_DATA: %lu\n",sizeof(O_DATA));
	return 0;
}

/******************************************************************************/
int cllist_get_size(cllist_t *llistp)
{
	int size = 0;
	cllist_node_t *cur, *prev;

	pthread_rdwr_wlock_np(&(llistp->rwlock));

	for (cur=prev=llistp->first; cur != NULL; prev=cur, cur=cur->nextp)
	{
		size++;
	}
	pthread_rdwr_wunlock_np(&(llistp->rwlock));
	return size;
}
/******************************************************************************/
int cllist_add_data(int index, cllist_t *llistp, C_DATA *datap2)
{
	cllist_node_t *cur, *prev, *new;
	int found = FALSE;
	C_DATA *datap;

	pthread_rdwr_wlock_np(&(llistp->rwlock));
	datap = malloc(sizeof(C_DATA));
	memcpy(datap,datap2,sizeof(C_DATA));

	for (cur=prev=llistp->first; cur != NULL; prev=cur, cur=cur->nextp)
	{
		//printf("%d ",cur->datap->value);
	}
	//printf("\n");

	new = (cllist_node_t *)malloc(sizeof(cllist_node_t));
	new->index = index;
	new->datap = datap;
	new->nextp = cur;
	if (cur==llistp->first)
		llistp->first = new;
	else
		prev->nextp = new;

	pthread_rdwr_wunlock_np(&(llistp->rwlock));
	return index;
}
/******************************************************************************/
int cllist_insert_data(int index, cllist_t *llistp,C_DATA *datap2)
{
	cllist_node_t *cur, *prev, *new;
	int found = FALSE;
	C_DATA *datap;

	pthread_rdwr_wlock_np(&(llistp->rwlock));
	datap = malloc(sizeof(C_DATA));
	memcpy(datap,datap2,sizeof(C_DATA));

	for (cur=prev=llistp->first; cur != NULL; prev=cur, cur=cur->nextp)
	{
		if (cur->index == index)
		{
			free(cur->datap);
			cur->datap = datap;
			printf("insert: %d %s\r\n",index, cur->datap->label);
			found=TRUE;
			break;
		}
		else if (cur->index > index)
		{
			break;
		}
	}
	if (!found)
	{
		new = (cllist_node_t *)malloc(sizeof(cllist_node_t));
		new->index = index;
		new->datap = datap;
		new->nextp = cur;
		if (cur==llistp->first)
			llistp->first = new;
		else
			prev->nextp = new;
	}
	pthread_rdwr_wunlock_np(&(llistp->rwlock));
	return index;
}

/******************************************************************************/
int cllist_remove_data(int index, C_DATA **datapp, cllist_t *llistp)
{
	cllist_node_t *cur, *prev;

	/* Initialize to "not found" */
	*datapp = (C_DATA*)NULL;

	pthread_rdwr_wlock_np(&(llistp->rwlock));

	for (cur=prev=llistp->first; cur != NULL; prev=cur, cur=cur->nextp)
	{
		if (cur->index == index)
		{
			*datapp = cur->datap;
			prev->nextp = cur->nextp;
			free(cur);
			break;
		}
		else if (cur->index > index)
		{
			break;
		}
	}
	pthread_rdwr_wunlock_np(&(llistp->rwlock));
	return 0;
}
/******************************************************************************/
int cllist_removeall_data(cllist_t *llistp)
{
	cllist_node_t *cur, *prev;

	/* Initialize to "not found" */
	C_DATA *datapp = (C_DATA*)NULL;

	pthread_rdwr_wlock_np(&(llistp->rwlock));

	for (cur=prev=llistp->first; cur != NULL; prev=cur, cur=cur->nextp)
	{
		datapp = cur->datap;
//		printf("%s_\n",datapp->label);
		prev->nextp = cur->nextp;
		free(cur);
	}
	pthread_rdwr_wunlock_np(&(llistp->rwlock));
	return 0;
}
/******************************************************************************/
int cllist_find_data(int index, C_DATA **datapp, cllist_t *llistp)
{
	cllist_node_t *cur, *prev;
	int status = -1;

	/* Initialize to "not found" */
	*datapp = (C_DATA *)NULL;
//printf("index: %d\n",index);
	pthread_rdwr_rlock_np(&(llistp->rwlock));

	/* Look through index for our entry */
	for (cur=prev=llistp->first; cur != NULL; prev=cur, cur=cur->nextp)
	{
		if (cur->index == index)
		{
			*datapp = cur->datap;
			status = 0;
			//printf("find: %d %d %d %s\r\n", index, cur->datap->index, cur->datap->port, cur->datap->label);
			break;
		}
		else if (cur->index > index)
		{
			//printf("bad break\n");
			break;
		}
	}
	pthread_rdwr_runlock_np(&(llistp->rwlock));
	//printf("status: %d\n",status);
	return status;
}
/******************************************************************************/
int cllist_change_data(int index, C_DATA *datap, cllist_t *llistp)
{
	cllist_node_t *cur, *prev;
	int status = -1; /* assume failure */
	pthread_rdwr_wlock_np(&(llistp->rwlock));
	for (cur=prev=llistp->first; cur != NULL; prev=cur, cur=cur->nextp)
	{
		if (cur->index == index)
		{
			//printf("cur: %s\n",cur->datap->label);
			cur->datap = datap;
			//free(cur);
			status = 0;
			break;
		}
		else if (cur->index > index)
		{
			break;
		}

	}
	pthread_rdwr_wunlock_np(&(llistp->rwlock));
	return status;
}
/******************************************************************************/
int cllist_show(cllist_t *llistp)
{
//	char list_buf[100];
	//char *ptr;
	//int iptr;
	cllist_node_t *cur;
	//char list_buf[100];
	int i = 0;

/*
	char label[OLABELSIZE];
	int port;					// which port to turn on/off
	int type;					// 0 - short duration; 1 - long duration (on/off hours minutes)
	int on_hour;				// use this if type 0 or 1
	int on_minute;				//	"	"
	int off_hour;				// use this if type 1 only
	int off_minute;				//  "  	"
	int duration_seconds;		// use these if type 0
	int duration_minutes;
*/
	printf("showing C_DATA\r\n");
	pthread_rdwr_rlock_np(&(llistp->rwlock));
	//printf("%02x \n",cur);
	cur=llistp->first;
	//printf("%d\n",cur->datap->index);

//	printf("port\tonoff\tinput_port\ttype\ttime_delay\tlabel\r\n");

	for (cur=llistp->first; cur != NULL; cur=cur->nextp)
	{
		//printf("%d ",cur->datap->index);
		if(cur->datap->label[0] != 0)
		{
			printf("%2d\t%2d\t%2d\t%2d\t%2d\t%2d\t%2d\t%2d\t%2d\t%s\r\n",
				cur->datap->index, cur->datap->port, cur->datap->state, cur->datap->on_hour, cur->datap->on_minute, 
				 cur->datap->on_second, cur->datap->off_hour, cur->datap->off_minute, 
				 cur->datap->off_second, cur->datap->label);

/*
			printf("%2d\t%2d\t%2d\t\t%2d\t%2d\t%s\r\n",
				(int)cur->datap->index, cur->datap->client_no, (int)cur->datap->cmd, 
				 cur->datap->dest, cur->datap->msg_len, cur->datap->label);

			memset(list_buf,0,100);
			sprintf(list_buf,"%2d  %2d  %2d  %2d  %2d  %2d  %2d  %2d   %s",(int)cur->datap->port,\
			 (int)cur->datap->onoff, (int)cur->datap->polarity, (int)cur->datap->type,
			 	 (int)cur->datap->time_delay, (int)cur->datap->time_left,
			 	 	(int)cur->datap->pulse_time, (int)cur->datap->reset,
			 	  cur->datap->label);
			 ptr = list_buf;
			 iptr = 0;

			 do
			 {
			 	iptr++;
			 }while(*(ptr++) != 0);
			send_tcp((UCHAR *)list_buf,iptr);

//			send_tcp((UCHAR *)&list_buf[0],100);
			printf("iptr: %d\r\n",iptr);
//			printString2(list_buf);
*/
		}
	}
	pthread_rdwr_runlock_np(&(llistp->rwlock));
	return 0;
}
int cllist_reorder(cllist_t *llistp)
{
//	char list_buf[100];
	char *ptr;
	int iptr;
	cllist_node_t *cur;
	char list_buf[100];
	int i = 0;

	printf("re-ordering C_DATA\r\n");

	pthread_rdwr_rlock_np(&(llistp->rwlock));
	cur=llistp->first;

//	printf("port\tonoff\tinput_port\ttype\ttime_delay\tlabel\r\n");

	for (cur=llistp->first; cur != NULL; cur=cur->nextp)
	{
		if(cur->datap->label[0] != 0)
		{
//			cur->datap->index = i++;
/*
			printf("%2d\t%2d\t%2d\t\t%2d\t%2d\t%s\r\n",
				(int)cur->datap->index, cur->datap->client_no, (int)cur->datap->cmd, 
				 cur->datap->dest, cur->datap->msg_len, cur->datap->label);
*/
		}
	}
	pthread_rdwr_runlock_np(&(llistp->rwlock));
	return 0;
}
int cllist_printfile(int fp, cllist_t *llistp)
{
	char list_buf[50];
	char *ptr;
	int iptr;
	cllist_node_t *cur;
#if 0
	pthread_rdwr_rlock_np(&(llistp->rwlock));

	for (cur=llistp->first; cur != NULL; cur=cur->nextp)
	{
		if(cur->datap->label[0] != 0)
		{
//			printf("port: %2d\tonoff: %2d\t%s\n",(int)cur->datap->port, (int)cur->datap->onoff,cur->datap->label);
			memset(list_buf,0,50);
			sprintf(list_buf,"port: %2d onoff: %2d %s",(int)cur->datap->port,\
			 (int)cur->datap->onoff,cur->datap->label);
			 ptr = list_buf;
			 iptr = 0;
			 do
			 {
			 	iptr++;
			 }while(*(ptr++) != 0);
			//write(fp,list_buf,50);

		}
	}
	pthread_rdwr_runlock_np(&(llistp->rwlock));
#endif
	return 0;
}

