#include <stdio.h>
#include <stdint.h>
#include <unistd.h>
#include "nbus.h"

// gcc -opc104 pc104.c nbus.c -mcpu=arm9 (on target 4600)

uint16_t show(uint16_t val, int iter)
{
	uint16_t show1;
	show1 = winpeek32(val);
	nbusunlock();
	printf("%d: %02x\n",iter, show1);
	sleep(1);
	nbuslock();
	return show1;
}

int main (int argc, char **argv)
{
	uint16_t val, val2;
	int i,j;
	val2 = 0x300;
	printf("starting...\n");
	j = 0;
	nbuslock();
	for(i = 0; i < 8; i++) 
//if(1)
	{
		val = show(val2,j);
		winpoke32(val2, val | ( 1 << 0));
		val = show(val2,j);
		winpoke32(val2, val & ~( 1 << 0));
		val = show(val2,j);
		j++;
/*
		winpoke32(val2, val & ~( 1 << 0));
		val = show(val2);
		winpoke32(val2, val | ( 1 << 1));
		val = show(val2);
		winpoke32(val2, val & ~( 1 << 1));
		val = show(val2);
		winpoke32(val2, val | ( 1 << 2));
		val = show(val2);
		winpoke32(val2, val & ~( 1 << 2));
		val = show(val2);
*/
		val2++;
	}
	nbusunlock();
	return 0;
/*
		sleep(1);
		val2++;
		nbuslock();
		winpoke32(val2, val & ~(1 << 1));
		val = winpeek32(val2);
		nbusunlock();
		printf("%02x\n",val);
		val2++;
		winpoke32(val2, val | ( 1 << 2));
		val = winpeek32(val2);
		nbusunlock();
		printf("%02x\n",val);
		sleep(1);
		nbuslock();
		val2++;
		winpoke32(val2, val & ~(1 << 2));
		val = winpeek32(val2);
		nbusunlock();
		printf("%02x\n",val);
		val2++;
		winpoke32(val2, val | ( 1 << 3));
		val = winpeek32(val2);
		nbusunlock();
		printf("%02x\n",val);
		sleep(1);
		nbuslock();
		val2++;
		winpoke32(val2, val & ~(1 << 3));
		val = winpeek32(val2);
		nbusunlock();
		printf("%02x\n",val);
		val2++;
		winpoke32(val2, val | ( 1 << 4));
		val = winpeek32(val2);
		nbusunlock();
		printf("%02x\n",val);
		sleep(1);
		nbuslock();
		val2++;
		winpoke32(val2, val & ~(1 << 4));
		val = winpeek32(val2);
		nbusunlock();
		printf("%02x\n",val);
	}
*/
}
