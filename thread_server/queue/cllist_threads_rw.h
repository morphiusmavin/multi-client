/********************************************************
 * An example source module to accompany...
 *
 * "Using POSIX Threads: Programming with Pthreads"
 *     by Brad nichols, Dick Buttlar, Jackie Farrell
 *     O'Reilly & Associates, Inc.
 *
 ********************************************************
 * llist_threads_rw.h --
 *
 * Include file for linked list with reader/writer locks
 * for threads support
 */
#include <pthread.h>
#include "../../mytypes.h"
//#include "../tasks.h"
#include "rdwr.h"

#ifndef TRUE
#define TRUE    1
#endif

#ifndef FALSE
#define FALSE   0
#endif

typedef struct c_data
{
	char label[OLABELSIZE];
	int index;				// just the key 
	int client_no;			// the client this msg applies to (could just as well be the index)
	UCHAR cmd;				// command to send
	int dest;				// destination (any other client or server in the client_table list)
	int msg_len;			// length of attached message if any
	int fn_ptr;				// a ptr into an array of functions on the client (depends on cmd)
	int data_ptr;			// a length into a huge array where data is stored for each client.
	int hours;				// time interval if to send anything on a predermined interval.
	int minutes;			// if all '0's then one-shot 
	int seconds;
} C_DATA;

typedef struct cllist_node {
  int index;
  C_DATA *datap;
  struct llist_node *nextp;
} cllist_node_t;

typedef struct cllist {
  cllist_node_t *first;
  pthread_rdwr_t rwlock;
} cllist_t;

int cllist_init (cllist_t *llistp);
int cllist_insert_data (int index, cllist_t *llistp,C_DATA *datap);
int cllist_remove_data(int index, C_DATA **datapp, cllist_t *llistp);
int cllist_removeall_data(cllist_t *llistp);
int cllist_find_data(int index, C_DATA **datapp, cllist_t *llistp);
int cllist_find_data_ip(int index, C_DATA **datapp, cllist_t *llistp);
int cllist_find_data_op(int index, int port, C_DATA **datapp, cllist_t *llistp);
int cllist_toggle_output(int index, cllist_t *llistp);
int cllist_change_output(int index, cllist_t *llistp, int onoff);
int cllist_change_data(int index, C_DATA *datap, cllist_t *llistp);
int cllist_show(cllist_t *llistp);
int cllist_printfile(int fp, cllist_t *llistp);

