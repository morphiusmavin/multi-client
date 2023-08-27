/*
CS		1		8		VDD/Vref
CH0		2		7		CLK
CH1		3		6		Dout
VSS		4		5		Din

CS, CLK, Din are inputs (on the chip) while the 
Dout is output. So CS, CLK & DIN are outputs on 
the DIO while DOUT is input on the DIO 

14, 13, 11 are the leftmost pins of the DIO next to the edge. 
and 15 is the middle of the back row
So let:

pin 11	MCP_CS
pin 13	MCP_DIN
pin 14	MCP_DOUT
pin 15	MCP_CLK
*/

#define HIGH	1
#define LOW		0
#define HIZ		2
#define OUT		HIGH
#define IN		LOW

#define MCP_CS 11
#define MCP_CLK 15
#define MCP_DOUT 14
#define MCP_DIN 13

UCHAR start_seq[10];
UCHAR mcp_data[13];



