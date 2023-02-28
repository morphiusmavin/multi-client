/* Copyright 2009-2010, Unpublished Work of Technologic Systems
 * All Rights Reserved.
 *
 * THIS WORK IS AN UNPUBLISHED WORK AND CONTAINS CONFIDENTIAL,
 * PROPRIETARY AND TRADE SECRET INFORMATION OF TECHNOLOGIC SYSTEMS.
 * ACCESS TO THIS WORK IS RESTRICTED TO (I) TECHNOLOGIC SYSTEMS 
 * EMPLOYEES WHO HAVE A NEED TO KNOW TO PERFORM TASKS WITHIN THE SCOPE
 * OF THEIR ASSIGNMENTS AND (II) ENTITIES OTHER THAN TECHNOLOGIC
 * SYSTEMS WHO HAVE ENTERED INTO APPROPRIATE LICENSE AGREEMENTS.  NO
 * PART OF THIS WORK MAY BE USED, PRACTICED, PERFORMED, COPIED, 
 * DISTRIBUTED, REVISED, MODIFIED, TRANSLATED, ABRIDGED, CONDENSED, 
 * EXPANDED, COLLECTED, COMPILED, LINKED, RECAST, TRANSFORMED, ADAPTED
 * IN ANY FORM OR BY ANY MEANS, MANUAL, MECHANICAL, CHEMICAL, 
 * ELECTRICAL, ELECTRONIC, OPTICAL, BIOLOGICAL, OR OTHERWISE WITHOUT
 * THE PRIOR WRITTEN PERMISSION AND CONSENT OF TECHNOLOGIC SYSTEMS.
 * ANY USE OR EXPLOITATION OF THIS WORK WITHOUT THE PRIOR WRITTEN
 * CONSENT OF TECHNOLOGIC SYSTEMS COULD SUBJECT THE PERPETRATOR TO
 * CRIMINAL AND CIVIL LIABILITY.
 */

/*	syscon for DIO
	0x8 	15:0 	DIO 15:0 input data
	0xa 	15:0 	DIO 15:0 output data
	0xc 	15:0 	DIO 15:0 data direction (1 - output)
	0xe 	15:0 	DIO 31:16 input data
	0x10 	15:0 	DIO 31:16 output data
	0x12 	15:0 	DIO 31:16 data direction (1 - output)
	0x14 	15:0 	DIO 47:32 input data
	0x16 	15:0 	DIO 47:32 output data
	0x18 	15:0 	DIO 47:32 data direction (1 - output)
	0x1a 	15:0 	DIO 63:48 input data
	0x1c 	15:0 	DIO 63:48 output data
	0x1e 	15:0 	DIO 63:48 data direction (1 - output)
	0x20 	15:6 	Reserved
			5:0 	DIO 69:64 input data
	0x22 	15:6 	Reserved
			5:0 	DIO 69:64 output data
	0x24 	15:6 	Reserved
			5:0 	DIO 69:64 data direction ( 1 - output)
	0x26 	15:0 	EVGPIO DR
	0x28 	15:0 	EVGPIO DDR
	
starting from right near edge of card (closest to eth port)
4
6
7
8
10
11
13
14 (dot)

starting from right near inside of card 
x
x
x
x
15
x
x
gnd

looking at card w/ ports on right (14 is dot)
gnd	x	x	15	x	x	x	x
14	13	11	10	8	7	6	4
	
*/
/*******************************************************************************
* Program: 
*    Get DIO (dio.c)
*    Technologic Systems TS-7600/4600
* 
* Summary:
*   This program will accept any pin number between 0 and 69 and attempt to get
*     or set those pins in a c program rather than scripted.  You will need the 
*     TS-7600/4600. 
*
*   While we encourage the proper use of locking the nbus, it is not needed
*     when the nbus.c api is used as the peek and poke routines will
*     automatically handle locking and unlocking.
*
* Usage:
*   ./dio <get|set> <pin#> <set_value (0|1|2)>
*
* 0 - GND
* 1 - 3.3V
* 2 - Z (High Impedance)
*
* Examples:
*   To read an input pin (such as 1 through 8 on the TS-752):
*      ts7600:~/# ./dio get 40
*      Result of getdiopin(38) is: 1 
*
*   To set an output pin (such as 1 through 3 or relays on the TS-752):
*      ts7600:~/# ./dio set 33 0
*      Pin#33 has been set to 0   [You may verify with DVM]
*
* Compile with:
*   gcc -mcpu=arm9 dio.c nbus.c -o dio
*
* Version history:
*  05/23/2012 - KB
*    Initial rev for ts7600/4600, based on 7500
*******************************************************************************/
#include "nbus.h"
#include <assert.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>

#define DIO_Z 2

/*******************************************************************************
* setdiopin: accepts a DIO register and value to place in that DIO pin.
*   Values can be 0 (low), 1 (high), or 2 (z - high impedance).
*******************************************************************************/
void setdiopin(int pin, int val)
{
	if(pin >= 0 && pin <= 69) {
		if(val == 0) {
			nbus_poke16(0x28, pin | 0x80);
			nbus_poke16(0x26, pin & ~0x80);
		} else if(val == 1) {
			nbus_poke16(0x28, pin | 0x80);
			nbus_poke16(0x26, pin | 0x80);
		} else if(val == 2) {
			nbus_poke16(0x28, pin & ~0x80);
		}
	}
}
/*******************************************************************************
* getdiopin: accepts a DIO pin number and returns its value.  
*******************************************************************************/
int getdiopin(int pin)
{
	unsigned short reg;
	if(pin >= 0 && pin <= 69) {
		/* This shift math is a little odd, but it works like so:
		 * Reg 0x8 is the start of the GPIO input regs in the syscon
		 * Each input reg is 6 bytes away from each other (3 16bit regs)
		 * Pin #X divided by 16 will return an int that is how many
		 *   input regs the reg needed is away from the first, 0 index
		 * Multiply the number of input regs away from 0x8 the needed
		 *   reg is by 6 to get the actual reg address
		 *
		 * For the return, we simply need to return a 1 or 0
		 * shift the bits to what pin was requested, & 1
		 */
		reg = nbus_peek16(0x8+(6*(pin/16)));
		return ((reg >> (pin%16)) & 1);
	}
}



/*******************************************************************************
* Main: accept input from the command line and act accordingly.
*******************************************************************************/
int main(int argc, char **argv)
{
	int pin;
	int val;
	int returnedValue;
         
	// Check for invalid command line arguments
	if ((argc > 4) | (argc < 3)) {
		printf("Usage: %s <get|set> <pin#> <set_value (0|1|2)>\n", argv[0]);
		return 1;
	}
   
	// We only want to get val if there are more than 3 command line arguments
	if (argc == 3) pin = strtoul(argv[2], NULL, 0);
	else {
		pin = strtoul(argv[2], NULL, 0);
		val = strtoul(argv[3], NULL, 0);
	}
   
	// Parse through the command line arguments, check for valid inputs, and exec
	if (!(strcmp(argv[1], "get")) && (argc == 3)) {
		returnedValue = getdiopin(pin);
      
		printf("pin#%d = %d \n", pin, returnedValue);
	} else if(!(strcmp(argv[1], "set")) && (argc == 4) && (val <= 2)) {
		setdiopin(pin, val);
		printf("pin#%d set to %d\n", pin, val);
	} else {
		printf("Usage: %s <get|set> <pin#> <set_value (0|1|2)>\n", argv[0]);
		return 1;
	}
	return 0;
}
