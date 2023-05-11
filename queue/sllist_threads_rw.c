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
#include "sllist_threads_rw.h"
/******************************************************************************/
int sllist_init (sllist_t *llistp)
{
	int rtn;

	llistp->first = NULL;
	if ((rtn = pthread_rdwr_init_np(&(llistp->rwlock), NULL)) !=0)
		fprintf(stderr, "pthread_rdwr_init_np error %d",rtn), exit(1);
//	printf("sizeof O_DATA: %lu\n",sizeof(O_DATA));
	return 0;
}
/******************************************************************************/
int sllist_get_size(sllist_t *llistp)
{
	int size = 0;
	sllist_node_t *cur, *prev;

	pthread_rdwr_wlock_np(&(llistp->rwlock));

	for (cur=prev=llistp->first; cur != NULL; prev=cur, cur=cur->nextp)
	{
		size++;
	}
	pthread_rdwr_wunlock_np(&(llistp->rwlock));
	return size;
}
/******************************************************************************/
int sllist_add_data(int index, sllist_t *llistp, S_DATA *datap2)
{
	sllist_node_t *cur, *prev, *new;
	int found = FALSE;
	S_DATA *datap;

	pthread_rdwr_wlock_np(&(llistp->rwlock));
	datap = malloc(sizeof(S_DATA));
	memcpy(datap,datap2,sizeof(S_DATA));

	for (cur=prev=llistp->first; cur != NULL; prev=cur, cur=cur->nextp)
	{
		//printf("%s ",cur->datap->name);
	}
	//printf("\n");

	new = (sllist_node_t *)malloc(sizeof(sllist_node_t));
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
int sllist_insert_data(int index, sllist_t *llistp, S_DATA *datap2)
{
	sllist_node_t *cur, *prev, *new;
	int found = FALSE;
	S_DATA *datap;

	pthread_rdwr_wlock_np(&(llistp->rwlock));
	datap = malloc(sizeof(S_DATA));
	memcpy(datap,datap2,sizeof(S_DATA));

	for (cur=prev=llistp->first; cur != NULL; prev=cur, cur=cur->nextp)
	{
		if (cur->index == index)
		{
			free(cur->datap);
			cur->datap = datap;
			//printf("insert: %d %s\r\n",index, cur->datap->sensor);
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
		new = (sllist_node_t *)malloc(sizeof(sllist_node_t));
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
int sllist_remove_data(int index, S_DATA **datapp, sllist_t *llistp)
{
	sllist_node_t *cur, *prev;

	/* Initialize to "not found" */
	*datapp = (S_DATA*)NULL;

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
int sllist_removeall_data(sllist_t *llistp)
{
	sllist_node_t *cur, *prev;

	/* Initialize to "not found" */
	S_DATA *datapp = (S_DATA*)NULL;

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
int sllist_find_data(int index, S_DATA **datapp, sllist_t *llistp)
{
	sllist_node_t *cur, *prev;
	int status = -1;

	/* Initialize to "not found" */
	*datapp = (S_DATA *)NULL;
//printf("index: %d\n",index);
	pthread_rdwr_rlock_np(&(llistp->rwlock));

	/* Look through index for our entry */
	for (cur=prev=llistp->first; cur != NULL; prev=cur, cur=cur->nextp)
	{
		//printf("%d\n",index);
		if (cur->index == index)
		{
			*datapp = cur->datap;
			status = 0;
			//printf("find: %d %s %s\n", index, cur->datap->name, cur->datap->filesize);
			break;
		}
		else if (cur->index > index)
		{
			printf("bad break\n");
			break;
		}
	}
	pthread_rdwr_runlock_np(&(llistp->rwlock));
	//printf("status: %d\n",status);
	return status;
}
/******************************************************************************/
int sllist_change_data(int index, S_DATA *datap, sllist_t *llistp)
{
	sllist_node_t *cur, *prev;
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
int sllist_show(sllist_t *llistp)
{
//	char list_buf[100];
	//char *ptr;
	//int iptr;
	sllist_node_t *cur;
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
	printf("showing S_DATA\r\n");
	pthread_rdwr_rlock_np(&(llistp->rwlock));
	//printf("%02x \n",cur);
	cur=llistp->first;
	//printf("%d\n",cur->datap->index);

//	printf("port\tonoff\tinput_port\ttype\ttime_delay\tlabel\r\n");

	for (cur=llistp->first; cur != NULL; cur=cur->nextp)
	{
		//printf("%d ",cur->datap->index);
		//if(cur->datap->label[0] != 0)
			if(1)
		{
			/*
			printf("%2d\t%2d\t%2d\t%2d\t%2d\t%2d\t%2d\r\n",
				cur->datap->sensor_no, cur->datap->month, cur->datap->day, cur->datap->hour, cur->datap->minute, 
				 cur->datap->second, cur->datap->value);


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

int sllist_reorder(sllist_t *llistp)
{
	sllist_node_t *cur, *prev, *temp;
	int i = 0;
	int j = 0;
	int min_idx;
	int list_size;

	printf("re-ordering S_DATA\r\n");

	pthread_rdwr_rlock_np(&(llistp->rwlock));
	cur=llistp->first;
	//list_size = sllist_get_size(llistp);
//	printf("port\tonoff\tinput_port\ttype\ttime_delay\tlabel\r\n");
/*
	for (cur=prev=llistp->first; cur != NULL; prev=cur, cur=cur->nextp)
	{
		min_idx = i;
		if(cur->nextp == NULL)
			break;
		for(j = i + 1;j < list_size;j++)
			if(strcmp(
		printf("%s %d %d\n",cur->datap->name,cur->datap->order,cur->index,temp->index);
		i++;
	}
*/
	pthread_rdwr_runlock_np(&(llistp->rwlock));
	return 0;
}
int sllist_printfile(int fp, sllist_t *llistp)
{
	char list_buf[50];
	char *ptr;
	int iptr;
	sllist_node_t *cur;
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

