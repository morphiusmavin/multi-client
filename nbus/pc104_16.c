#include <stdio.h>
#include <stdint.h>
#include <unistd.h>
#include "nbus.h"


uint16_t show(unsigned int val)
{
	unsigned short show1;
	show1 = winpeek16(val);
	nbusunlock();
	printf("%02x\n", show1);
	sleep(1);
	nbuslock();
	return show1;
}

void do_bits(int which,int onoff)
{
	unsigned int val2;
	unsigned short val;
	val2 = 0x300;	// 0x300 and 0x302 work as expected but 0x301 addresses 0x300 
	nbuslock();
	val = winpeek16(val2);
	if(onoff == 1)
		winpoke16(val2, val | ( 1 << which));
	else
		winpoke16(val2, val & ~( 1 << which));
	val = show(val2);
	nbusunlock();
}

int main (int argc, char **argv)
{
	char input_char = 0;
	unsigned char temp,temp2;
	printf("starting...\n");
	nbuslock();
	temp = 0;
	winpoke8(0x300,temp);
	winpoke8(0x301,temp);
	winpoke8(0x302,temp);
	nbusunlock();
	unsigned int val2;
	do
	{
		input_char = getchar();
		switch(input_char)
		{
			case 'a':				// turn on bit 0
				do_bits(0,1);
				break;
			case 'b':				// turn off bit 0
				do_bits(0,0);
				break;
			case 'c':				// turn on bit 1
				do_bits(1,1);
				break;
			case 'd':				// turn off bit 1
				do_bits(1,0);
				break;
			case 'e':				// turn on bit 2
				do_bits(2,1);
				break;
			case 'f':				// turn off bit 2
				do_bits(2,0);
				break;
			case 'g':
				do_bits(3,1);
				break;
			case 'h':
				do_bits(3,0);
				break;
			case 'i':
				do_bits(4,1);
				break;
			case 'j':
				do_bits(4,0);
				break;
			case 'k':
				do_bits(5,1);
				break;
			case 'l':
				do_bits(5,0);
				break;
			case 'm':
				do_bits(6,1);
				break;
			case 'n':
				do_bits(6,0);
				break;
			case 'o':
				do_bits(7,1);
				break;
			case 'p':
				do_bits(7,0);
				break;
			case 'r':
				do_bits(8,1);
				break;
			case 's':
				do_bits(8,0);
				break;
			case 't':
				do_bits(9,1);
				break;
			case 'u':
				do_bits(9,0);
				break;
		}
	}while(input_char != 'q');
	return 0;
}
