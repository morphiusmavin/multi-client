typedef unsigned char UCHAR;
static void writeCommandTo1620( UCHAR cmd, UCHAR data );
static void writeTempTo1620( UCHAR reg, int temp );
int readTempFrom1620(int which);
void initDS1620(void);
/*
DIO pins on bottom right of card when 
looking down at it with eth port to 
the right

gnd	x	x	15	x	x	x	x
14	13	11	10	8	7	6	4

pin 4	DQ
pin 6	CLK
pin 7 	RST_0
pin 8	RST_1
pin 10	RST_2
pin 11	MCP_CS
pin 13	MCP_DIN
pin 14	MCP_DOUT
pin 15	MCP_CLK
		
5/6/23 if accessing the MCP3002 then I need 4 lines 

the DQ & CLK go to all the DS1620's (upto 7)
and each RST is just the chip enable for 
each DS1620 chip 

nbus_poke16(0x28, pin | 0x80);		set pin as output
nbus_poke16(0x28, pin & ~0x80);		set pin as input

nbus_poke16(0x26, pin & ~0x80);		turn output off
nbus_poke16(0x26, pin | 0x80);		turn output on
*/
#define DQ		4
#define CLK		6
#define RST_0	7
#define RST_1	8
#define RST_2	10
#define HIGH	1
#define LOW		0
#define HIZ		2
#define OUT		HIGH
#define IN		LOW
#define DS1620_CMD_READTEMP		0xAA
#define DS1620_CMD_WRITETH		0x01
#define DS1620_CMD_WRITETL		0x02
#define DS1620_CMD_READTH		0xA1
#define DS1620_CMD_READTL		0xA2
#define DS1620_CMD_READCNTR		0xA0
#define DS1620_CMD_READSLOPE	0xA9
#define DS1620_CMD_STARTCONV	0xEE
#define DS1620_CMD_STOPCONV		0x22
#define DS1620_CMD_WRITECONF	0x0C
#define DS1620_CMD_READCONF		0xAC
