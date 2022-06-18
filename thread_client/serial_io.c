#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <errno.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <termios.h>
#include <sys/time.h>
#include "../mytypes.h"
#include "serial_io.h"

#define BAUDRATE B115200
#define BAUDRATE2 B115200
#define BAUDRATE3 B115200

#ifdef TS_7800
#define MODEMDEVICE "/dev/ttyS0"				  // 7800 uses ttyS1 as the 2nd serial port
#define MODEMDEVICE2 "/dev/ttyS1"
#define MODEMDEVICE3 "/dev/ttts4"
#warning "TS_7800 defined"
#else  
#warning "TS_7800 not defined"
#define MODEMDEVICE "/dev/ttyAM0"				  // 7200 uses ttyAM0 if console disabled
#define MODEMDEVICE2 "/dev/ttyxuart1"
#define MODEMDEVICE3 "/dev/ttyxuart2"
#endif	// end of ifdef TS_7800

#define _POSIX_SOURCE 1							  /* POSIX compliant source */
#define FALSE 0
#define TRUE 1

static int set_interface_attribs (int fd, int speed, int parity);
static void set_blocking (int fd, int should_block);
static struct termios oldtio;
static struct termios oldtio2;
static struct termios oldtio3;
extern PARAM_STRUCT ps;


int test_init(void)
{
	int fd, c, res;
    struct termios oldtio, newtio;
    char buf[255] = {0};
    
    fd = open(MODEMDEVICE2, O_RDWR | O_NOCTTY ); 
    if(fd <0) 
    {
            perror(MODEMDEVICE2); 
            return -1;
    }
	global_handle2 = fd;
    
    tcgetattr(fd, &oldtio); /* save current port settings */
    
    bzero(&newtio, sizeof(newtio));
    newtio.c_cflag = CRTSCTS | CS8 | CLOCAL | CREAD;
    newtio.c_iflag = IGNPAR;
    newtio.c_oflag = 0;
    /* set input mode (non-canonical, no echo,...) */
    newtio.c_lflag = 0;
     
    newtio.c_cc[VTIME]    = 0;   /* inter-character timer unused */
    newtio.c_cc[VMIN]     = 2;   /* blocking read until 2 chars received */
    
    tcflush(fd, TCIFLUSH);
    tcsetattr(fd, TCSANOW, &newtio);


//	restore 	
//    tcsetattr(fd, TCSANOW, &oldtio);

    return fd;
}
/************************************************************************************/
static int set_interface_attribs (int fd, int speed, int parity)
{
	struct termios tty;
	memset (&tty, 0, sizeof tty);
	if (tcgetattr (fd, &tty) != 0)
	{
		printf("tcgetattr error\n", errno);
		perror(" ");
		return -1;
	}

//	cfsetospeed (&tty, speed);
//	cfsetispeed (&tty, speed);

	tty.c_cflag = (tty.c_cflag & ~CSIZE) | CS8;	  // 8-bit chars
// disable IGNBRK for mismatched speed tests; otherwise receive break
// as \000 chars
	tty.c_iflag &= ~IGNBRK;						  // disable break processing
	tty.c_lflag = 0;							  // no signaling chars, no echo,
// no canonical processing
	tty.c_oflag = 0;							  // no remapping, no delays
	tty.c_cc[VMIN]  = 2;						  // read doesn't block
	tty.c_cc[VTIME] = 20;						  // 0.5 seconds read timeout

	tty.c_iflag &= ~(IXON | IXOFF | IXANY);		  // shut off xon/xoff ctrl

	tty.c_cflag |= (CLOCAL | CREAD);			  // ignore modem controls,
// enable reading
	tty.c_cflag &= ~(PARENB | PARODD);			  // shut off parity
	tty.c_cflag |= parity;
	tty.c_cflag &= ~CSTOPB;
	tty.c_cflag &= ~CRTSCTS;

	if (tcsetattr (fd, TCSANOW, &tty) != 0)
	{
		printf("tcgetattr error\n", errno);
		perror(" ");
		return -1;
	}
	return 0;
}

/************************************************************************************/
static void set_blocking (int fd, int should_block)
{
	struct termios tty;
	memset (&tty, 0, sizeof tty);
	if (tcgetattr (fd, &tty) != 0)
	{
		printf("tcgetattr error\n", errno);
		perror(" ");
		return;
	}

	tty.c_cc[VMIN]  = should_block ? 1 : 0;
	tty.c_cc[VTIME] = 10;						  // 0.5 seconds read timeout

	if (tcsetattr (fd, TCSANOW, &tty) != 0)
		printf("term error\n", errno);
}

/************************************************************************************/
int init_serial(void)
{
	UCHAR test = 0x21;
	global_handle = -1;

	global_handle = open (MODEMDEVICE, O_RDWR | O_NOCTTY | O_SYNC);
	if (global_handle <0)
	{
		perror(MODEMDEVICE);
		exit(-1);
	}
	//printf("global_handle = %d\n",global_handle);

	if(tcgetattr(global_handle,&oldtio) != 0)	  /* save current port settings */
	{
		printf("tcgetattr error\n", errno);
		close(global_handle);
		exit(1);
	}
	sleep(2); //required to make flush work, for some reason
	tcflush(global_handle,TCIOFLUSH);

	set_interface_attribs (global_handle, BAUDRATE, 0);
// if set to blocking, the serial_task won't exit properly
// and then the sched program won't quit	
	set_blocking (global_handle, 1);	 // blocking
//	set_blocking (global_handle, 0);	// non-blocking
	return global_handle;
}

/************************************************************************************/
int write_serial(UCHAR byte)
{
	int res;
//	printf("fd = %d\n",global_handle);
	res = write(global_handle, &byte, 1);
	return res;
}
/************************************************************************************/
int write_serial2(UCHAR byte)
{
	int res = -1;
//	if(ps.test_bank == 0)
//	return -1;
	if(1)
	{
		res = write(global_handle2, &byte, 1);
	}
	return res;
}
/************************************************************************************/
int write_serial3(UCHAR byte)
{
	int res = -1;
//	if(ps.test_bank == 0)
	if(1)
	{
		res = write(global_handle3, &byte, 1);
		//printf("%02x ",byte);
	}
	return res;
}
#if 0
/************************************************************************************/
int read_serial_buff(UCHAR *buff, char *errmsg)
{
	int res;
	res = read(global_handle2,&buff[0],SERIAL_BUFF_SIZE);
	if(res < 0)
//		printf("\nread error: %s\n",strerror(errno));
		strcpy(errmsg,strerror(errno));

	return res;
}
/************************************************************************************/
int write_serial_buff(UCHAR *buff, char *errmsg)
{
	int res;
//	printf("fd = %d\n",global_handle);
	res = write(global_handle2,&buff[0],SERIAL_BUFF_SIZE);
	if(res < 0)
//		printf("\nread error: %s\n",strerror(errno));
		strcpy(errmsg,strerror(errno));
	return res;
}
#endif
/************************************************************************************/
void printString(const char myString[])
{
	UCHAR i = 0;
	return;
	while(myString[i])
	{
		write_serial(myString[i]);
		i++;
	}
}

/************************************************************************************/
UCHAR read_serial(char *errmsg)
{
	int res;
	UCHAR byte;
	res = read(global_handle,&byte,1);
	if(res < 0)
	{
//		printf("\nread error: %s\n",strerror(errno));
		printString2("error\0");
		strcpy(errmsg,strerror(errno));
	}
	return byte;
}
/************************************************************************************/
UCHAR read_serial2(char *errmsg)
{
	int res;
	UCHAR byte = 0;
//	if(ps.test_bank == 0)
	if(1)
	{
		res = read(global_handle2,&byte,1);
		if(res < 0)
		{
			printf("\r\nread error: %s\r\n",strerror(errno));
			strcpy(errmsg,strerror(errno));
		}
	}
	return byte;
}
/************************************************************************************/
UCHAR read_serial3(char *errmsg)
{
	int res;
	UCHAR byte = 0;
	res = read(global_handle3,&byte,1);
	if(res < 0)
	{
		printf("\r\nread error: %s\r\n",strerror(errno));
		strcpy(errmsg,strerror(errno));
	}
	return byte;
}
/************************************************************************************/
void close_serial(void)
{
//	printf("closing serial port\n");
	tcsetattr(global_handle,TCSANOW,&oldtio);
	close(global_handle);
}

/************************************************************************************/
int init_serial2(void)
{
	UCHAR test = 0x21;
	global_handle2 = -1;

//	if(ps.test_bank == 0)
	if(1)
	{
// undef this section if console disabled just so it can compile with the console enabled
		global_handle2 = open (MODEMDEVICE2, O_RDWR | O_NOCTTY | O_SYNC);
		if (global_handle2 <0)
		{
			perror(MODEMDEVICE2);
			exit(-1);
		}
		if(tcgetattr(global_handle2,&oldtio2) != 0)	  /* save current port settings */
		{
			printf("tcgetattr (2) error\n", errno);
			close(global_handle2);
			exit(1);
		}
		//printf("global_handle2 = %d\n",global_handle2);

		set_interface_attribs (global_handle2, BAUDRATE2, 0);
		set_blocking (global_handle2, 1);	 // blocking
//		set_blocking (global_handle2, 0);	// non-blocking
	}

	return global_handle2;
}

/************************************************************************************/

int init_serial3(int baudrate)
{
	global_handle3 = -1;
	int actual_baudrate = B4800;
	char tempx[30];
	strcpy(tempx,"COMM3 br: ");
	
//	if(ps.test_bank == 0)
	switch(baudrate)
	{
		case 0:
			actual_baudrate = B4800;
			strcat(tempx,"4800");
			break;
		case 1:
			actual_baudrate = B9600;
			strcat(tempx,"9600");
			break;
		case 2:
			actual_baudrate = B19200;
			strcat(tempx,"19200");
			break;
		case 3:
			actual_baudrate = B38400;
			strcat(tempx,"38400");
			break;
		default:
			actual_baudrate = B115200;
			strcat(tempx,"115200");
			break;
	}
	if(1)
	{
// undef this section if console disabled just so it can compile with the console enabled
		global_handle3 = open (MODEMDEVICE3, O_RDWR | O_NOCTTY | O_SYNC);
		if (global_handle3 < 0)
		{
			//perror(MODEMDEVICE3);
			//printString2("error on MODEMDEVICE3 open");
			return -1;
		}
		if(tcgetattr(global_handle3,&oldtio3) != 0)	  /* save current port settings */
		{
			printf("tcgetattr (2) error\0", errno);
			close(global_handle3);
			//printString2("error on tcgetattr");
			return -1;
		}
		set_interface_attribs (global_handle3, BAUDRATE3, 0);
//		set_interface_attribs (global_handle3, actual_baudrate, 0);
//		printf(tempx);
		set_blocking (global_handle3, 1);	 // blocking
//		set_blocking (global_handle2, 0);	// non-blocking
	}

	return global_handle3;
}

/************************************************************************************/
void printHexByte(UCHAR byte) 
{
	return;
	/* Prints a byte as its hexadecimal equivalent */
	UCHAR nibble;
//	nibble = (byte & 0b11110000) >> 4;
	nibble = (byte & 0xF0) >> 4;
	write_serial3(nibbleToHexCharacter(nibble));
//	nibble = byte & 0b00001111;
	nibble = byte & 0x0F;
	write_serial3(nibbleToHexCharacter(nibble));
	write_serial3(0x20);
}

/************************************************************************************/
char nibbleToHexCharacter(UCHAR nibble) 
{
		                           /* Converts 4 bits into hexadecimal */
	if (nibble < 10) 
	{
		return ('0' + nibble);
	}
	else 
	{
		return ('A' + nibble - 10);
	}
}

/************************************************************************************/
void printString2(char *myString)
{
	int i = 0;

//	if(ps.test_bank == 0)
	if(1)
	{
		while(myString[i])
		{
			write_serial2(myString[i]);
			i++;
		}
		write_serial2('\r');
		write_serial2('\n');
	}
}

/************************************************************************************/
void printString3(char *myString)
{
	int i = 0;
//	if(ps.test_bank == 0)
	if(0)
	{
		while(myString[i])
		{
			write_serial3(myString[i]);
			i++;
		}
		write_serial3('\r');
		write_serial3('\n');
	}
}

/************************************************************************************/
void close_serial2(void)
{
	if(1)
	{
		tcsetattr(global_handle2,TCSANOW,&oldtio2);
		close(global_handle2);
	}
}

void close_serial3(void)
{
	if(1)
	{
		tcsetattr(global_handle3,TCSANOW,&oldtio3);
		close(global_handle3);
	}
}
