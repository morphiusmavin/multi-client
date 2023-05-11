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
#include "../mytypes.h"
#include "nbus.h"
#include "dio_mcp3002.h"

UCHAR start_seq[10];
UCHAR mcp_data[10];

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
	mydelay(1);
}
/*********************************************************************************/
void set_pin(int pin, int val)
{
	if(val > 0)
		nbus_poke16(0x26, pin | 0x80);
	else nbus_poke16(0x26, pin & ~0x80);
	mydelay(1);
}
/*********************************************************************************/
void init_MCP3002()
{
	int pin = MCP_CS;
	set_dir(pin,OUT);
	pin = MCP_CLK;
	set_dir(pin,OUT);
	pin = MCP_DOUT;
	set_dir(pin,IN);
	pin = MCP_DIN;
	set_dir(pin,OUT);

	start_seq[0] = 1;
	start_seq[1] = 1;		// single-ended
	start_seq[2] = 0;		// channel (0/1)
	start_seq[3] = 1;		// MSBF

}
/*********************************************************************************/
#if 0
int main(void)
{
	int i;
	int val;

	mydelay(1000);

	printf("starting...\n");

	for(i = 0;i < 15;i++)
	{
		set_pin(MCP_CLK, HIGH);
		mydelay(100);
		if(i < 4)
		{
			set_pin(MCP_DIN, start_seq[i]);		// dout here goes to the Din pin
		}
		if(i == 4)		// switch to other channel 
			if(start_seq[2] == 0)
				start_seq[2] = 1;
			else start_seq[2] = 0;
		set_pin(MCP_CLK, LOW);
		mydelay(100);
		if(i > 4)
		{
			mcp_data[5-i] = get_pin(MCP_DOUT);
		}
		mydelay(100);
	}
	for(i = 0;i < 10;i++)
	{
		printf("%d ",mcp_data[i]);
	}
	printf("\n");
}
#endif
