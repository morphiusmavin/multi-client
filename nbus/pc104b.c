#include <stdio.h>
#include <stdint.h>
#include <unistd.h>
#include "nbus.h"


int main (int argc, char **argv)
{
	uint16_t val, val2;
	int i,j;
	val2 = 0x300;
	nbuslock();
	winpoke32(val2,0);
	nbusunlock();
	return 0;
}
