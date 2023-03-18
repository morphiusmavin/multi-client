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
#include "../mytypes.h"
//#include "../tasks.h"
#include "rdwr.h"

#ifndef TRUE
#define TRUE    1
#endif

#ifndef FALSE
#define FALSE   0
#endif

typedef struct d_data
{
	int sensor_no;
	int month;
	int day;
	int hour;
	int minute;
	int second;
	int value;
} D_DATA;

typedef struct dllist_node {
  int index;
  D_DATA *datap;
  struct llist_node *nextp;
} dllist_node_t;

typedef struct dllist {
  dllist_node_t *first;
  pthread_rdwr_t rwlock;
} dllist_t;

int dllist_init (dllist_t *llistp);
int dllist_get_size(dllist_t *llistp);
int dllist_insert_data (int index, dllist_t *llistp,D_DATA *datap);
int dllist_add_data(int index, dllist_t *llistp,D_DATA *datap2);
int dllist_remove_data(int index, D_DATA **datapp, dllist_t *llistp);
int dllist_removeall_data(dllist_t *llistp);
int dllist_find_data(int index, D_DATA **datapp, dllist_t *llistp);
int dllist_change_data(int index, D_DATA *datap, dllist_t *llistp);
int dllist_show(dllist_t *llistp);
int dllist_printfile(int fp, dllist_t *llistp);
int dllist_reorder(dllist_t *llistp);

