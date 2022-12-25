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
	sleep(1);
	nbuslock();
	return show1;
}

int main (int argc, char **argv)
{
	uint16_t val, val2;
	int i,j;
	val2 = 0x300;
	printf("show...\n");
	nbuslock();
	val = show(val2);
	nbusunlock();
	return 0;
}
