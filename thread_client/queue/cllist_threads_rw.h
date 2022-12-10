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
	char label[CLABELSIZE];
	int index;
	int port;					// which port to turn on/off
	int state;					// 0 - off; 1 - on
	int on_hour;
	int on_minute;
	int on_second;
	int off_hour;
	int off_minute;
	int off_second;
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
int cllist_change_data(int index, C_DATA *datap, cllist_t *llistp);
int cllist_show(cllist_t *llistp);
int cllist_printfile(int fp, cllist_t *llistp);
int cllist_reorder(cllist_t *llistp);

