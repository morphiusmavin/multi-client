/* When compiling use the following gcc command:
 * gcc -oexample example.c nbus.c -mcpu=arm9
 *
 * nbus.c and nbus.h must be in the same folder where the gcc command is being run from
 */
#include <stdio.h>
#include <stdint.h>
#include <unistd.h>
#include "nbus.h"

int main (int argc, char **argv)
{
	uint16_t val;
	int i,j;
	nbuslock();

	/* Set DIO 7 low
	* Set output value to 0
	*/
	val = nbus_peek16(0xa);
	nbus_poke16(0xa, val & ~(1 << 7));
	// Set dio 7 direction to output
	val = nbus_peek16(0xc);
	nbus_poke16(0xc, val | (1 << 7));

	/* Set DIO 7 high
	* DDR is already set to output, so
	* set output value
	*/
	val = nbus_peek16(0xa);

	nbus_poke16(0xa, val | (1 << 7));

	// Toggle Red LED 10 times
	val = nbus_peek16(0x2);

	/* The NBUS lock should be held as little as possible
	* since other peripherals will need access.  When 
	* going into an operation like a sleep, a flush, or
	* any other syscal that will stall the system without
	* actually needing the lock, it should be released first.
	*/
	nbusunlock();
	printf("Starting loop\n");
	nbuslock();
	nbus_poke16(0x2, val & ~(1 << 14));
	nbus_poke16(0x2, val & ~(1 << 15));

	for(i = 0; i < 10; i++) 
	{
		nbusunlock();
		sleep(1);
		nbuslock();
		if(i % 2) 
		{
			nbus_poke16(0x2, val | ( 1 << 14));
			val = nbus_peek16(0x2);
			nbusunlock();
			sleep(1);
			nbuslock();
			nbus_poke16(0x2, val & ~(1 << 14));
			val = nbus_peek16(0x2);
		} else 
		{
			nbus_poke16(0x2, val | (1 << 15));
			val = nbus_peek16(0x2);
			nbusunlock();
			sleep(1);
			nbuslock();
			nbus_poke16(0x2, val & ~( 1 << 15));
			val = nbus_peek16(0x2);
		}

	/* nbuspreempt() can be used to check if there
	* are other processes waiting to use the bus. If there
	* are, then the bus is unlocked, given to other processes
	* and then the bus is re-locked.  When nbuspreempt()
	* returns the calling process will have the lock again
	*/
	nbuspreempt();
	}
	nbusunlock();

	return 0;
}
