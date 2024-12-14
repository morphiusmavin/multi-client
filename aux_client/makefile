###########################################################
# An example source module to accompany...
#
# "Using POSIX Threads: Programming with Pthreads"
#     by Brad nichols, Dick Buttlar, Jackie Farrell
#     O'Reilly & Associates, Inc.
#
###########################################################
# simple/Makefile
#
# makefile for the example programs 
#

# Tru64 UNIX compile flags
T64CC = cc
T64CFLAGS = -g -std1 -warnprotos -pthread
T64LDFLAGS = -non_shared
T64LDFLAGS_T = ${T64LDFLAGS} -pthread
T64SFLAGS =
T64NOANSI = ${T64CFLAGS}


# GCC/Linux compile flags
GNUCC = gcc
#GNUCFLAGS = -g -ansi -Wstrict-prototypes
GNUCFLAGS = -g -Wstrict-prototypes
GNULDFLAGS = -static
GNULDFLAGS_T = ${GNULDFLAGS} -pthread
GNUSFLAGS = -D_SVID_SOURCE -D_XOPEN_SOURCE
GNUNOANSI = -g -Wstrict-prototypes

#####################################################
CC = ${GNUCC}
CFLAGS = ${GNUCFLAGS}
LDFLAGS = ${GNULDFLAGS}
LDFLAGS_T = ${GNULDFLAGS_T}
SFLAGS = ${GNUSFLAGS}
NOANSI = ${GNUNOANSI}

all : aux2_client aux_client aux_client2 aux_client3
#all : aux_client aux_client2 aux_client3

aux2_client: aux2_client.o load_cmds.o
	${CC} ${CFLAGS} ${LDFLAGS_T} aux2_client.o load_cmds.o -o aux2_client

aux_client: aux_client.o load_cmds.o
	${CC} aux_client.o load_cmds.o -o aux_client

aux_client2: aux_client2.o load_cmds.o
	${CC} aux_client2.o load_cmds.o -o aux_client2

aux_client3: aux_client3.o load_cmds.o
	${CC} aux_client3.o load_cmds.o -o aux_client3
	
clean :
	mv load_cmds.o temp
	rm -f *.o *~ *# core  a.out\
	aux2_client aux_client aux_client2 aux_client3
	mv temp load_cmds.o


