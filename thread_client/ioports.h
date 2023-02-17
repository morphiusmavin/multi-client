#ifndef __IOPORTS_H
#define __IOPORTS_H
//#warning "IOPORTS defined"
#define			VUCHAR volatile unsigned char

//#define NUM_PORTS (OUTPORTF_OFFSET-OUTPORTA_OFFSET)+1
#define 		NUM_PORTS 3
//#define 		NUM_PORT_BITS (NUM_PORTS*8)-8	// PORTC & F only has 6 bits each
#define 		NUM_PORT_BITS  		20
#define			OUTPORTA_OFFSET		0
#define			OUTPORTB_OFFSET		1
#define			OUTPORTC_OFFSET		2
// these were 3->5 which was causing the strange bug where 
// other ports were being set and the cll memory was corrupting
#define			OUTPORTD_OFFSET		0
#define			OUTPORTE_OFFSET		1
#define			OUTPORTF_OFFSET		2

#define			CARD1	0x280
#define			CARD2	0x300

#define			ROC_1	CARD1			// relay output control registers
#define			ROC_2	CARD1 + 1
#define			ROC_3	CARD1 + 2		// bank 2 only uses first 4 bits

#define			DIR_1	CARD1 + 4		// digital input reading registers
#define			DIR_2	CARD1 + 5
#define			DIR_3	CARD1 + 6		// bank 2 only uses first 4 bits

#define			ROC_4	CARD2
#define			ROC_5	CARD2 + 1
#define			ROC_6	CARD2 + 2

#define			DIR_4	CARD2 + 4
#define			DIR_5	CARD2 + 5
#define			DIR_6	CARD2 + 6

#define			DELAYTIME 50

#define			COMMQ_BAD_CH		160u

void OutPortA(int onoff, int bit);
UCHAR InPortByteA(void);

void OutPortB(int onoff, int bit);
UCHAR InPortByteB(void);

void OutPortC(int onoff, int bit);
UCHAR InPortByteC(void);

#endif

