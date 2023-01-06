#include <stdio.h>
#include <stdint.h>
#include <unistd.h>
#include "nbus.h"


uint16_t show(unsigned int val)
{
	unsigned short show1;
	show1 = winpeek16a(val);
	nbusunlock();
	printf("%02x\n", show1);
	nbuslock();
	return show1;
}

void do_bits(int which,int onoff)
{
	unsigned int val2;
	unsigned short val;
	val2 = 0x300;
	nbuslock();
	val = winpeek16a(val2);
	if(onoff == 1)
		winpoke16a(val2, val | ( 1 << which));
	else
		winpoke16a(val2, val & ~( 1 << which));
	val = show(val2);
	nbusunlock();
}

int main (int argc, char **argv)
{
	int i;
	char input_char = 0;
	unsigned short temp;
	unsigned int temp2;
	printf("starting...\n");

	for(i = 0;i < 16;i++)
	{
		do_bits(i,1);
		usleep(100000);
		do_bits(i,0);
		usleep(100000);
	}
	return 0;
}

