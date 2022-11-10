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
	int port;					// which port to turn on/off
	int state;					// 0 - off; 1 - on
	int type;					// 0 - short duration; 1 - long duration (on/off hours minutes)
	int on_hour;				// use this if type 0 or 1
	int on_minute;				//	"	"
	int off_hour;				// use this if type 1 only
	int off_minute;				//  "  	"
	int duration_seconds;		// use these if type 0
	int duration_minutes;
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
int cllist_reorder(cllist_t *llistp);

