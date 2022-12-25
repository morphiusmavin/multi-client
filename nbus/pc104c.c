#include <stdio.h>
#include <stdint.h>
#include <unistd.h>
#include "nbus.h"


uint16_t show(uint16_t val)
{
	uint16_t show1;
	show1 = winpeek32(val);
	nbusunlock();
	printf("%02x\n",show1);
//	sleep(1);
	nbuslock();
	return show1;
}

void inc(uint16_t inc)
{
	uint16_t val2,val;
	val2 = 0x300 + inc;
//	printf("show...\n");
	nbuslock();
	val = show(val2);
	nbusunlock();
}

int main (int argc, char **argv)
{
	int i;
	for(i = 0;i < 20;i++)
		inc(i);
	return 0;
}
