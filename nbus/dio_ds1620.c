#include <assert.h>
#include <string.h>
#include <stdlib.h>
#include<unistd.h>
#include<sys/types.h>
#include<sys/mman.h>
#include<stdio.h>
#include<fcntl.h>
#include<time.h>
#include <sys/time.h>
#include "nbus.h"
#include "dio_ds1620.h"

static int current_ds[7];
static int current_ds_ptr;
/*********************************************************************************/
static void mydelay(unsigned long i)
{
	unsigned long j;
	struct timespec sleepTime;
	struct timespec rettime;
	sleepTime.tv_sec = 0;
	sleepTime.tv_nsec = 948540;
	for(j = 0;j < i;j++)
	{
		nanosleep(&sleepTime, &rettime);
//		printf(".");
	}
}
/*********************************************************************************/
int get_pin(int pin)		// see dio.c
{
	unsigned short reg;
	if(pin >= 0 && pin <= 69) 
	{
		reg = nbus_peek16(0x8+(6*(pin/16)));
		return ((reg >> (pin%16)) & 1);
	}
	return 0;
}
/*********************************************************************************/
void set_dir(int pin, int dir)
{
	if(dir > 0)
		nbus_poke16(0x28, pin | 0x80);
	else nbus_poke16(0x28, pin & ~0x80);
}
/*********************************************************************************/
void set_pin(int pin, int val)
{
	if(val > 0)
		nbus_poke16(0x26, pin | 0x80);
	else nbus_poke16(0x26, pin & ~0x80);
}
/*********************************************************************************/
void initDS1620(void)
{
	current_ds[0] = RST_0;
	current_ds[1] = RST_1;
	current_ds[2] = RST_2;
	current_ds[3] = RST_3;
	current_ds[4] = RST_4;
	current_ds[5] = RST_5;
	current_ds[6] = RST_6;
	current_ds_ptr = 0;

	int pin = DQ;
	set_dir(pin,HIZ);
	pin = CLK;
	set_dir(pin,OUT);
	pin = RST_0;
	set_dir(pin,OUT);
	pin = RST_1;
	set_dir(pin,OUT);
	pin = RST_2;
	set_dir(pin,OUT);
	pin = RST_3;
	set_dir(pin,OUT);
	pin = RST_4;
	set_dir(pin,OUT);
	pin = RST_5;
	set_dir(pin,OUT);

	writeCommandTo1620( DS1620_CMD_WRITECONF, 0x02 );			// CPU mode; continous conversion
//	writeByteTo1620( DS1620_CMD_STARTCONV );					// Start conversion
}
/*********************************************************************************/
static void shiftOutByte( UCHAR val )
{
	int i;
	// Send UCHAR, LSB first
	for( i = 0; i < 8; i++ )
	{
		set_pin(CLK,LOW);

		// Set bit
		if( val & (1 << i))
		{
			set_pin(DQ,HIGH);
			mydelay(4);
		}
		else
		{
			set_pin(DQ,LOW);
			mydelay(4);
		}
		set_pin(CLK,HIGH);
	}
}
/*********************************************************************************/
// call this about 30ms before a call to readTempFrom1620
void writeByteTo1620( UCHAR cmd )
{
	set_pin(current_ds[current_ds_ptr],HIGH);

	shiftOutByte( cmd );

	set_pin(current_ds[current_ds_ptr],LOW);
}
/*********************************************************************************/
// just used in the init function
static void writeCommandTo1620( UCHAR cmd, UCHAR data )
{
	set_pin(current_ds[current_ds_ptr],HIGH);

	shiftOutByte( cmd );	// send command
	shiftOutByte( data );	// send 8 bit data

	set_pin(current_ds[current_ds_ptr],LOW);
}
/*********************************************************************************/
// not currently used btw
static void writeTempTo1620( UCHAR reg, int temp )
{
	UCHAR lsb = temp;					// truncate to high UCHAR
	UCHAR msb = temp >> 8;				// shift high -> low UCHAR

	set_pin(current_ds[current_ds_ptr],HIGH);

	shiftOutByte( reg );	// send register select
	shiftOutByte( lsb );	// send LSB 8 bit data
	shiftOutByte( msb );	// send MSB 8 bit data (only bit 0 is used)

	set_pin(current_ds[current_ds_ptr],LOW);
}
/*********************************************************************************/
int readTempFrom1620(int which)
{
	int i;
	int state;

	current_ds_ptr = which;
	set_pin(current_ds[current_ds_ptr],HIGH);

	shiftOutByte( DS1620_CMD_READTEMP );						// send register select

	set_dir(DQ,IN);
	int raw = 0;

	for( i=0; i<9; i++ )										// read 9 bits
	{
		set_pin(CLK,LOW);

		mydelay(5);
		state = get_pin(DQ);
		if(state == HIGH)
			raw |= (1 << i);									// add value
		set_pin(CLK,HIGH);
	}

	set_pin(current_ds[current_ds_ptr],LOW);

	set_dir(DQ,OUT);
//	return (double)(raw/(double)2);								// divide by 2 and return
	return raw;
}
/*********************************************************************************/
#if 0
int main(void)
{
	int i;
	int val;

	mydelay(1000);

	initDS1620();

	mydelay(1000);
	printf("starting...\n");
	for(i = 0;i < 100;i++)
	{
		writeByteTo1620(DS1620_CMD_STARTCONV);
		mydelay(50);
		val = readTempFrom1620();
		printf("%d: %d\n",i,val);
		mydelay(50);
		writeByteTo1620(DS1620_CMD_STOPCONV);
		mydelay(100);
	}
	return 0;
}
#endif
