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

typedef struct s_data
{
	int sensor;
	int filesize;
	int order;
	char name[30];
	char label[30];
} S_DATA;

typedef struct sllist_node {
  int index;
  S_DATA *datap;
  struct llist_node *nextp;
} sllist_node_t;

typedef struct sllist {
  sllist_node_t *first;
  pthread_rdwr_t rwlock;
} sllist_t;

int sllist_init (sllist_t *llistp);
int sllist_get_size(sllist_t *llistp);
int sllist_insert_data (int index, sllist_t *llistp,S_DATA *datap);
int sllist_add_data(int index, sllist_t *llistp,S_DATA *datap2);
int sllist_remove_data(int index, S_DATA **datapp, sllist_t *llistp);
int sllist_removeall_data(sllist_t *llistp);
int sllist_find_data(int index, S_DATA **datapp, sllist_t *llistp);
int sllist_change_data(int index, S_DATA *datap, sllist_t *llistp);
int sllist_show(sllist_t *llistp);
int sllist_printfile(int fp, sllist_t *llistp);
int sllist_reorder(sllist_t *llistp);

