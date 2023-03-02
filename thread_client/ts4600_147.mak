GCC_BIN  = $(GCC_BASE)bin/
GCC_LIB  = $(GCC_BASE)arm-linux/lib/
#GCC_INC  = $(GCC_BASE)arm-linux/include/
GCC_INC  =	/usr/include/
AS       = $(GCC_BIN)arm-linux-as
#CC       = $(GCC_BIN)arm-linux-gcc
CC		= gcc
CPP      = $(GCC_BIN)arm-linux-g++
LD       = $(GCC_BIN)arm-linux-gcc
OBJCOPY  = $(GCC_BIN)arm-linux-objcopy

TS-7200_CC_FLAGS = -mcpu=arm920t -mapcs-32 -mthumb-interwork
ASM_FLAGS = -almns=listing.txt
PROJECT_INC_LIB = -I$(PORT) -I$(SOURCE)

# to debug use: make print-<variable_name>
# e.g. make print-GCC_BASE
print-%:
	@echo '$*=$($*)'
#	@echo $(.SOURCE)
#	@echo $(.TARGET)

######################################################################################
# Main makefile project configuration
#    PROJECT      = <name of the target to be built>
#    MCU_CC_FLAGS = <one of the CC_FLAGS above>
#    MCU_LIB_PATH = <one of the LIB_PATH above>
#    OPTIMIZE_FOR = < SIZE or nothing >
#    DEBUG_LEVEL  = < -g compiler option or nothing >
#    OPTIM_LEVEL  = < -O compiler option or nothing >
######################################################################################
PROJECT           = main
MCU_CC_FLAGS      = $(TS-7200_CC_FLAGS)
MCU_LIB_PATH      = $(TS-7200_LIB_PATH)
OPTIMIZE_FOR      =
DEBUG_LEVEL       =
OPTIM_LEVEL       =
PROJECT_OBJECTS   = main.o
PROJECT_LIB_PATHS = -L/lib
PROJECT_LIBRARIES =
PROJECT_SYMBOLS   = -DTOOLCHAIN_GCC_ARM -DNO_RELOC='0'

######################################################################################
# Main makefile system configuration
######################################################################################
SYS_OBJECTS =
SYS_LIB_PATHS = -L$(MCU_LIB_PATH)
ifeq (OPTIMIZE_FOR, SIZE)
SYS_LIBRARIES = -lstdc++_s -lsupc++_s -lm -lc_s -lg_s -lnosys
SYS_LD_FLAGS  = --specs=nano.specs -u _printf_float -u _scanf_float
else
SYS_LIBRARIES = -lstdc++ -lsupc++ -lm -lc -lg -lnosys
SYS_LD_FLAGS  =
endif

###############################################################################
# Command line building
###############################################################################
ifdef DEBUG_LEVEL
CC_DEBUG_FLAGS = -g$(DEBUG_LEVEL)
CC_SYMBOLS = -DDEBUG $(PROJECT_SYMBOLS)
else
CC_DEBUG_FLAGS =
CC_SYMBOLS = -DNODEBUG $(PROJECT_SYMBOLS)
endif

ifdef OPTIM_LEVEL
CC_OPTIM_FLAGS = -O$(OPTIM_LEVEL)
else
CC_OPTIM_FLAGS =
endif

INCLUDE_PATHS = -I.$(GCC_INC)
LIBRARY_PATHS = $(PROJECT_LIB_LIB) $(SYS_LIB_PATHS)
#CC_FLAGS = $(MCU_CC_FLAGS) $(CC_OPTIM_FLAGS) $(CC_DEBUG_FLAGS) -Wall -fno-exceptions -ffunction-sections -fdata-sections 
#-pthread -static-libgcc
# use -static-libgcc instead of -static to get rid of warning: "Using getprotobyname in statically linked apps requires
# at runtime the shared libraries from the glibc version used for linking"
LD_FLAGS = $(MCU_CC_FLAGS) -Wl,--gc-sections $(SYS_LD_FLAGS) -Wl,-Map=$(PROJECT).map
LD_SYS_LIBS = $(SYS_LIBRARIES)

CC_FLAGS = -static -g -DCL_147 -DUSE_CARDS -Wstrict-prototypes -mcpu=arm9
GNULDFLAGS_T = ${GNULDFLAGS} -pthread
#CC_FLAGST = ${CC_FLAGS} + GNULDFLAGS_T
GNUSFLAGS = -D_SVID_SOURCE -D_XOPEN_SOURCE
GNUNOANSI = -g -gnu99 -Wstrict-prototypes

#####################################################
#CFLAGS = ${GNUCFLAGS}
#LDFLAGS = ${GNULDFLAGS}

BULD_TARGET = $(PROJECT)

all : sched147

nbus.o: ../nbus/nbus.c ../nbus/nbus.h
	${CC} ${CC_FLAGS} ${INCLUDE_PATHS} -c ../nbus/nbus.c

cmd_tasks.o: cmd_tasks.c
	${CC} ${CC_FLAGS} ${INCLUDE_PATHS} -c cmd_tasks.c

tasks.o: tasks.c tasks.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c tasks.c

sched.o: sched.c tasks.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c sched.c

ioports.o: ioports.c ioports.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c ioports.c

assign_client_table.o: ../assign_client_table.c
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c ../assign_client_table.c

serial_io.o: serial_io.c serial_io.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c serial_io.c

ollist_threads_rw.o: queue/ollist_threads_rw.c queue/ollist_threads_rw.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c queue/ollist_threads_rw.c

rdwr.o: queue/rdwr.c queue/rdwr.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c queue/rdwr.c

config_file.o: cs_client/config_file.c  queue/ollist_threads_rw.h
	${CC} ${CC_FLAGS} ${INCLUDE_PATHS} -c cs_client/config_file.c

load_cmds.o: ../load_cmds.c
	${CC} ${CC_FLAGS} ${INCLUDE_PATHS} -c ../load_cmds.c

cllist_threads_rw.o: queue/cllist_threads_rw.c queue/cllist_threads_rw.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c queue/cllist_threads_rw.c

cconfig_file.o: cs_client/cconfig_file.c queue/cllist_threads_rw.h
	${CC} -DMAKE_TARGET ${CC_FLAGS} ${INCLUDE_PATHS} -c cs_client/cconfig_file.c
	
sched147: tasks.o cmd_tasks.o sched.o ioports.o assign_client_table.o load_cmds.o ollist_threads_rw.o rdwr.o serial_io.o config_file.o cllist_threads_rw.o cconfig_file.o nbus.o
	${CC} -static -pthread tasks.o cmd_tasks.o sched.o ioports.o ollist_threads_rw.o rdwr.o serial_io.o config_file.o assign_client_table.o load_cmds.o cllist_threads_rw.o cconfig_file.o nbus.o -o sched147 -lm

clean :
	rm -f *.o *~ *# core  sched147

