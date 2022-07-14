######################################################################################
# GNU GCC ARM Embeded Toolchain base directories and binaries
######################################################################################
#use this one when compiling on 115
#GCC_BASE = /home/dan/dev/arm/opt/crosstool/gcc-3.4.4-glibc-2.3.2/arm-linux/
#or just set the CROSS_COMPILE env var
GCC_BASE = $(CROSS_COMPILE)
# use this one when compiling on 103
#GCC_BASE = /home/dan/dev/arm/crosstool/gcc3/arm-linux/
# need to use /usr/local/sbin/setARMpath2.sh
GCC_BIN  = $(GCC_BASE)bin/
GCC_LIB  = $(GCC_BASE)arm-linux/lib/
GCC_INC  = $(GCC_BASE)arm-linux/include/
AS       = $(GCC_BIN)arm-linux-as
CC       = $(GCC_BIN)arm-linux-gcc
CPP      = $(GCC_BIN)arm-linux-g++
LD       = $(GCC_BIN)arm-linux-gcc
OBJCOPY  = $(GCC_BIN)arm-linux-objcopy

#TS-7200_CC_FLAGS = -mcpu=arm920t -march=armv5t
#TS-7200_CC_FLAGS = -mcpu=arm920t -mapcs-32 -mthumb-interwork
#TS-7200_CC_FLAGS = -mcpu=arm920t
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

#GNUCFLAGS = -g -ansi -Wstrict-prototypes	doesn't compile "// ..comments.."
# gcc3 and gcc4 doesn't support thumb-interworking, but compiles anyway
# gcc3/4 doesn't work with -mapcs-32
#CC_FLAGS = -static -g -DTS_7800 -DUSE_CARDS -Wstrict-prototypes -mcpu=arm920t
CC_FLAGS = -static -g -DTS_7800 -Wstrict-prototypes -mcpu=arm920t
#CC_FLAGS = -static -g -Wstrict-prototypes -mcpu=arm920t
#CC_FLAGS = -static -g -Wstrict-prototypes -mcpu=arm920t -mapcs-32 -mthumb-interwork
#this works for either TS-7200 or TS-7800
#CC_FLAGS = -g -Wstrict-prototypes -mcpu=arm920t -mapcs-32
#thought this was needed for TS-7800
#CC_FLAGS = -g -Wstrict-prototypes -march=armv5t -mcpu=arm9
#CC_FLAGS = -g -Wstrict-prototypes  -march-armv5t	this won't work without the mcpu...
#CC_FLAGS = -g -float=soft -arch=armv5t
GNULDFLAGS_T = ${GNULDFLAGS} -pthread
#CC_FLAGST = ${CC_FLAGS} + GNULDFLAGS_T
GNUSFLAGS = -D_SVID_SOURCE -D_XOPEN_SOURCE
GNUNOANSI = -g -gnu99 -Wstrict-prototypes

#####################################################
#CFLAGS = ${GNUCFLAGS}
#LDFLAGS = ${GNULDFLAGS}

BULD_TARGET = $(PROJECT)

all : sched sock_mgt
#all : test_dio1
#test_ioports test_inports # test_ports test_spi
#llist_test_threads llist_test_threads_rw

sched.o: sched.c tasks.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c sched.c

ioports.o: ioports.c ioports.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c ioports.c

assign_client_table.o: ../assign_client_table.c
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c ../assign_client_table.c

tasks.o: tasks.c tasks.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c tasks.c

serial_io.o: serial_io.c serial_io.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c serial_io.c

ollist_threads_rw.o: queue/ollist_threads_rw.c queue/ollist_threads_rw.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c queue/ollist_threads_rw.c

rdwr.o: queue/rdwr.c queue/rdwr.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c queue/rdwr.c

config_file.o: cs_client/config_file.c  queue/ollist_threads_rw.h
	${CC} ${CC_FLAGS} ${INCLUDE_PATHS} -c cs_client/config_file.c

#lcd_func.o: lcd_func.c lcd_func.h
#	${CC} ${CC_FLAGS} ${INCLUDE_PATHS} -c lcd_func.c

cmd_tasks.o: cmd_tasks.c
	${CC} ${CC_FLAGS} ${INCLUDE_PATHS} -c cmd_tasks.c

load_cmds.o: ../load_cmds.c
	${CC} ${CC_FLAGS} ${INCLUDE_PATHS} -c ../load_cmds.c

cllist_threads_rw.o: queue/cllist_threads_rw.c queue/cllist_threads_rw.h
	${CC} ${INCLUDE_PATHS} ${CC_FLAGS} -c queue/cllist_threads_rw.c

cconfig_file.o: cs_client/cconfig_file.c queue/cllist_threads_rw.h
	${CC} -DMAKE_TARGET ${CC_FLAGS} ${INCLUDE_PATHS} -c cs_client/cconfig_file.c
	
sock_mgt.o : sock_mgt.c 
	${CC} ${CC_FLAGS} ${INCLUDE_PATHS} -c sock_mgt.c

sock_sched.o : sock_sched.c 
	${CC} ${CC_FLAGS} ${INCLUDE_PATHS} -c sock_sched.c

sched: tasks.o cmd_tasks.o sched.o ioports.o assign_client_table.o load_cmds.o ollist_threads_rw.o rdwr.o serial_io.o config_file.o cllist_threads_rw.o cconfig_file.o
	${CC} -static -pthread tasks.o cmd_tasks.o sched.o ioports.o ollist_threads_rw.o rdwr.o serial_io.o config_file.o assign_client_table.o load_cmds.o cllist_threads_rw.o cconfig_file.o -o sched -lm

sock_mgt: sock_mgt.o sock_sched.o load_cmds.o
	${CC} -static -pthread sock_sched.o assign_client_table.o sock_mgt.o load_cmds.o -o sock_mgt

clean :
	rm -f *.o *~ *# core  sched

