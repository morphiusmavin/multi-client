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

/*********************************************************************************/
void init_MCP3002()
{
	mydelay(10);
	set_dir(MCP_CS,OUT);
	mydelay(10);
	set_dir(MCP_CLK,OUT);
	mydelay(10);
	set_dir(MCP_DOUT,IN);
	mydelay(10);
	set_dir(MCP_DIN,OUT);
	mydelay(10);
	set_pin(MCP_CS, HIGH);
	mydelay(10);
	set_pin(pin, LOW);
	mydelay(10);
	set_pin(pin, HIGH);
	mydelay(10);
	set_pin(MCP_CLK, HIGH);
	mydelay(10);

	start_seq[0] = 1;		// start bit
	start_seq[1] = 1;		// single-ended (sgl/diff) 
	start_seq[2] = 0;		// channel (0/1) (odd/sign)
	start_seq[3] = 1;		// MSBF (most sig bit first)
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
