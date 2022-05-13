#include <unistd.h>
#include <sys/types.h>
#include <sys/mman.h>
#include <stdio.h>
#include <fcntl.h>
#include <assert.h>
#include <time.h>
#include <stdlib.h>
#include <sys/time.h>
#include <string.h>
#include "../mytypes.h"
#include "lcd_func.h"
#include "serial_io.h" 

/*********************************************************************************************************/
#ifdef TS_7800

#define LCD_BUF_LINES 30
#define LCD_BUF_COLS 32
static char buffer[LCD_BUF_LINES][LCD_BUF_COLS];
static char *buf_ptr;
static int cur_buf_line;
static int cur_disp_line;
static int cur_buf_size;

volatile unsigned int *lcd;

#define MIRROR_PINS

/*********************************************************************************************************/
inline int getLCDbit(int n) 
{
#ifdef MIRROR_PINS
	switch (n) 
	{
		case 3: n =4 ; break;
		case 4:  n =3 ; break;
		case 5: n =6 ; break;
		case 6: n =5 ; break;
		case 7: n =8 ; break;
		case 8: n =7 ; break;
		case 9: n =10 ; break;
		case 10: n =9 ; break;
		case 11: n =12 ; break;
		case 12: n =11 ; break;
		case 13: n =14 ; break;
		case 14: n =13 ; break;
	}
#endif
	return (*lcd >> (16+n-1)) & 1;
}

//inline
/*********************************************************************************************************/
void putLCDbit(int n,int val) 
{
	volatile int i;
	unsigned int tmp;

#ifdef MIRROR_PINS
	switch (n) 
	{
		case 3: n =4 ; break;
		case 4:  n =3 ; break;
		case 5: n =6 ; break;
		case 6: n =5 ; break;
		case 7: n =8 ; break;
		case 8: n =7 ; break;
		case 9: n =10 ; break;
		case 10: n =9 ; break;
		case 11: n =12 ; break;
		case 12: n =11 ; break;
		case 13: n =14 ; break;
		case 14: n =13 ; break;
	}
#endif
	if (val) 
	{
		tmp = *(lcd+1);
		tmp |= (1 << (16+n-1));
		*(lcd+1) = tmp;
	} else 
	{
		tmp = *(lcd+1);
		tmp &= ~(1 << (16+n-1));
		*(lcd+1) = tmp;
	}
	i = *(lcd+1); // force bus cycle to flush write now
	if (i != tmp) printf("put %X, got %X\n",tmp,i); // don't really need this
}

/*********************************************************************************************************/
inline void lcd_instr(int i) 
{
	putLCDbit(4,!i);
}

/*********************************************************************************************************/
inline void lcd_data_out(int data) 
{
	int i;
	for (i=0;i<8;i++) 
	{
		putLCDbit(i+7,data & 1);
		data >>= 1;
	}
}

/*********************************************************************************************************/
inline void _lcd_write(int w) 
{
	if (!w) 
	{
		lcd_data_out(0xFF);
	}
	putLCDbit(5,!w);
}

/*********************************************************************************************************/
inline void lcd_enable(int e) 
{
	putLCDbit(6,e);
}

/*********************************************************************************************************/
inline void lcd_wait_not_busy()
{
	int i = 1000,val;
	volatile int ii;

	if(!lcd_enabled)
		return;

	do 
	{
		lcd_instr(1);
		_lcd_write(0);
		for (ii=0;ii<20000;ii++); // wait 800nS minimum
		lcd_enable(1);
		for (ii=0;ii<20000;ii++); // wait 800nS minimum
		//usleep(10000);
		val = getLCDbit(14);
		lcd_enable(0);
		for (ii=0;ii<20000;ii++); // wait 800nS minimum
	} while (--i && val);
	if (i == 0) 
	{
		printf("LCD timeout %X\n",val);
	}
}

/*********************************************************************************************************/
inline void lcd_cmd(int cmd) 
{
	volatile int i;

	_lcd_write(1);
	lcd_data_out(cmd);
	lcd_instr(1);
	for (i=0;i<1000;i++);
	lcd_enable(1);
	//usleep(10000);
	for (i=0;i<10000;i++);
	lcd_enable(0);
	lcd_wait_not_busy();
}

/*********************************************************************************************************/
inline void lcd_data(int data) 
{
	volatile int i;

	lcd_data_out(data);
	lcd_instr(0);
	_lcd_write(1);
	for (i=0;i<1000;i++);
	lcd_enable(1);
	for (i=0;i<10000;i++); // wait 800nS minimum
	//usleep(10000);
	lcd_enable(0);
	lcd_wait_not_busy();
}

/*********************************************************************************************************/
inline void lcd_cmd_cls()
{
	lcd_cmd(0x01);
}

/*********************************************************************************************************/
void lcd_write(char *w)
{
	char *temp = w;
	int out;
	out = (int)*w;
	_lcd_write(out);
}

/*********************************************************************************************************/
void lcd_init(void)
{
	lcd_enabled = 1;
	lcdinit();
	lcd_wait_not_busy();
	memset(buffer,0,LCD_BUF_LINES*LCD_BUF_COLS);
	buf_ptr = buffer[0];
	cur_disp_line = cur_buf_line = cur_buf_size = 0;
}

/*********************************************************************************************************/
void lcd_home(void)
{
	if(!lcd_enabled)
		return;
	lcd_cmd(HOME);
}

/*********************************************************************************************************/
void lcd_cls(void)
{
	if(!lcd_enabled)
		return;
	lcd_cmd(CLEAR);
	lcd_cmd(HOME);
	memset(buffer,0,LCD_BUF_LINES*LCD_BUF_COLS);
	buf_ptr = buffer[0];
	cur_disp_line = cur_buf_line = cur_buf_size = 0;
}
/**********************************************************************************************************/
int myprintf1(char *str)
{

	char *ptr = str;
	char temp[10];
	char temp2[35];
	//printString2(str);

	if(!lcd_enabled)
	{
		printf("%s\n",str);
		return 1;
	}

	cur_buf_line = cur_buf_size;
//	printf("%d\n",cur_buf_line);
	inc_bufptrs();
	memset(buffer[cur_buf_line],0,LCD_BUF_COLS);
	sprintf((char*)buffer[cur_buf_line],"%d:%s",cur_disp_line,str);
//	sprintf((char*)buffer[cur_buf_line],"%s",str);
	display_current(0);

//	sprintf(temp2,"%d: %s",cur_disp_line,str);
	return 1;
}

/**********************************************************************************************************/
int myprintf2(char *str, int x)
{
	char *ptr = str;
	char temp[35];
	char temp2[10];
	int i;
	return 1;
	if(!lcd_enabled)
	{
//		printf("%s %d\n",str,x);
		return 1;
	}

	sprintf(temp,"%d:%s",cur_disp_line, str);
	sprintf(temp2,"% d",x);
	strcat(temp,temp2);
	cur_buf_line = cur_buf_size;
	inc_bufptrs();
	memset(buffer[cur_buf_line],0,LCD_BUF_COLS);
	strcpy((char *)buffer[cur_buf_line],temp);
	display_current(0);
	return 0;
}

/**********************************************************************************************************/
int myprintf3(char *str, int x, int y)
{
	char *ptr = str;
	char temp[35];
	char temp2[10];
	int i;

	if(!lcd_enabled)
	{
//		printf("%s %d %d\n",str,x,y);
		return 1;
	}

	cur_buf_line = cur_buf_size;
//	printf("%d\n",cur_buf_line);
	inc_bufptrs();
	memset(buffer[cur_buf_line],0,LCD_BUF_COLS);

	sprintf(temp,"%d:%s",cur_disp_line,str);
	sprintf(temp2,"% d",x);
	strcat(temp,temp2);
	sprintf(temp2,"% d",y);
	strcat(temp,temp2);
	strcpy((char *)buffer[cur_buf_line],temp);
	display_current(0);
	return 0;
}

/**********************************************************************************************************/
static void inc_bufptrs(void)
{
	int i;
	if(cur_buf_size > LCD_BUF_LINES-2)
		for(i = 0;i < cur_buf_size;i++)
		{
			memcpy(buffer[i],buffer[i+1],LCD_BUF_COLS);
//			printf("%s\n",buffer[i]);
		}

	cur_buf_size = (cur_buf_size>=LCD_BUF_LINES-1?LCD_BUF_LINES-1:++cur_buf_size);

	if(++cur_buf_line >= LCD_BUF_LINES-1)
	{
		cur_buf_line = LCD_BUF_LINES-1;
	}

	if(++cur_disp_line > LCD_BUF_LINES)
		cur_disp_line = 0;

//	printf("cur_buf_size: %d cur_buf_line %d (inc)\r\n",cur_buf_size,cur_buf_line);
}

/**********************************************************************************************************/
static void display_current(int special_case)
{
	int i;
	char *s;

	lcd_cmd(CLEAR);
	lcd_cmd(HOME);

//	printf("cur_buf_size: %d cur_buf_line %d\r\n",cur_buf_size,cur_buf_line);

	if(cur_buf_size == 0)
	{
		s = buffer[0];
		while(*s)
			lcd_data(*(s++));
	}else
	{
		if(special_case == 0)
		{
			s = buffer[cur_buf_line-1];
			while(*s)
				lcd_data(*(s++));

			lcd_wait_not_busy();
			lcd_cmd(ROW2);
			lcd_wait_not_busy();
			s = buffer[cur_buf_line];
			while(*s)
				lcd_data(*(s++));
			lcd_wait_not_busy();
		}
		else if(special_case == 1)
		{
			s = buffer[cur_buf_size-1];
			while(*s)
				lcd_data(*(s++));
			lcd_wait_not_busy();
			lcd_cmd(ROW2);
			lcd_wait_not_busy();
			s = buffer[cur_buf_size];
			while(*s)
				lcd_data(*(s++));
			lcd_wait_not_busy();
		}

	}
//	printf("%s\n",buffer[cur_buf_line]);
}

/*********************************************************************************************************/
void scroll_up(void)
{
	if(cur_buf_size < 3)
		return;

//	printf("line: %d size: %d\n",cur_buf_line,cur_buf_size);

	cur_buf_line--;
	if(cur_buf_line < 1)
		cur_buf_line = cur_buf_size-1;

//	printf("scroll_up: %d\n",cur_buf_line);
	display_current(0);
}

/*********************************************************************************************************/
void scroll_down(void)
{
	if(cur_buf_size < 3)
		return;
//	printf("line: %d size: %d\n",cur_buf_line,cur_buf_size);

	if(++cur_buf_line > cur_buf_size-1)
	{
		display_current(1);
		cur_buf_line = 1;
	}
//	printf("line: %d size: %d\n",cur_buf_line,cur_buf_size);

//	printf("scroll_down: %d\n",cur_buf_line);
	display_current(0);
}

/**********************************************************************************************************/
void shift_left(void)
{
	if(!lcd_enabled)
		return;
	lcd_cmd(SHIFTLEFT);
}

/**********************************************************************************************************/
void shift_right(void)
{
	if(!lcd_enabled)
		return;
	lcd_cmd(SHIFTRIGHT);
}

/**********************************************************************************************************/
static void lcdinit(void)
{
	volatile unsigned int *dioptr;
	int fd = open("/dev/mem", O_RDWR|O_SYNC);
	char *s;

	dioptr = (unsigned int *)mmap(0, getpagesize(),
		PROT_READ|PROT_WRITE, MAP_SHARED, fd, LCDBASEADD);
			RW_REG(dioptr) &= ~BASELINE_MASK; /* baseline should always be zero */

	lcd = dioptr + 1;

	lcd_cmd(0x38);
	lcd_cmd(0x38);
	lcd_cmd(0x38);
	lcd_cmd(0x6);
	lcd_cmd_cls();
	lcd_cmd(0xC);
	lcd_cmd(0x2);

}

#ifndef MAKE_TARGET
/*********************************************************************************************************/
static void mydelay(unsigned long i)
{
	unsigned long j;

	do
	{
		for(j = 0;j < 10000;j++);
		i--;
	}while(i > 0);
}

void print_mem(void)
{
	int i;
/*
	phdr = &gpio[PHDR];
	padr = &gpio[PADR];
	paddr = &gpio[PADDR];
	phddr = &gpio[PHDDR];
	dio_addr = &gpio[DIOADR];
	dio_ddr = &gpio[DIODDR];

#define PADR    0							// address offset of LCD
#define PADDR   (0x10 / sizeof(UINT))		// address offset of DDR LCD
#define PHDR    (0x40 / sizeof(UINT))		// bits 3-5: EN, RS, WR
#define PHDDR   (0x44 / sizeof(UINT))		// DDR for above
#define DIODDR	(0x14 / sizeof(UINT))
#define DIOADR	(0x04 / sizeof(UINT))
#define PORTFB  (0x30 / sizeof(UINT))
#define PORTFD	(0x34 / sizeof(UINT))

*/
}

int main(void)
{
	int i,j,key;

	i = j = 0;
	lcd_init();

	printf("c - clear screen\n");
	printf("l - left shift\n");
	printf("r - right shift\n");
	printf("h - home cursor\n");
	printf("u - scroll_up\n");
	printf("d - scroll_down\n");
	printf("p - print new string to buffer\n");
	printf("q - quit\n");

//	buffer[LCD_BUF_LINES][LCD_BUF_COLS];

	myprintf1("line 1 this is a test      a\0");
	myprintf1("line 2 this is a test      b\0");
	myprintf1("line 3 this is a test      c\0");
	myprintf1("line 4 this is a test      d\0");
	myprintf1("line 5 this is a test      e\0");
	myprintf1("line 6 this is a test      f\0");
	myprintf1("line 7 this is a test      g\0");
	myprintf1("line 8 this is a test      h\0");
	myprintf1("line 9 this is a test      i\0");
	myprintf1("line 10 this is a test      j\0");
/*
	myprintf1("line 11 this is a test       k\0");
	mydelay(1000);
	myprintf1("line 12 this is a test       l\0");
	mydelay(1000);
	myprintf1("line 13 this is a test       m\0");
	mydelay(1000);
	myprintf1("line 14 this is a test       n\0");
	mydelay(1000);
	myprintf1("line 15 this is a test       o\0");
	mydelay(1000);
	myprintf1("line 16 this is a test       p\0");
	mydelay(1000);
	myprintf1("line 17 this is a test       q\0");
	mydelay(1000);
	myprintf1("line 18 this is a test       r\0");
	mydelay(1000);
	myprintf1("line 19 this is a test       s\0");
	mydelay(1000);
	myprintf1("line 20 this is a test       t\0");
	mydelay(1000);
*/
	do
	{
		key = getc(stdin);
		switch(key)
		{
			case 'c':
				lcd_cls();
				memset(buffer,0,LCD_BUF_LINES*LCD_BUF_COLS);
				buf_ptr = buffer[0];
				cur_buf_line = cur_buf_size = 0;
				i = j = 0;
				break;
			case 'l':
				for(i = 0;i < 6;i++)
				{
					mydelay(5);
					shift_left();
				}
				break;
			case 'r':
				for(i = 0;i < 6;i++)
				{
					mydelay(5);
					shift_right();
				}
				break;
			case 'm':
				printf("c - clear screen\n");
				printf("l - left shift\n");
				printf("r - right shift\n");
				printf("h - home cursor\n");
				printf("u - scroll_up\n");
				printf("d - scroll_down\n");
				printf("p - print new string to buffer\n");
				printf("q - quit\n");
				break;
			case 'p':
				myprintf3("new string \0",i,j);
				i++;
				j += 2;
				break;
			case 'h':
				lcd_home();
				break;
			case 'i':	
				for(i = 0;i < cur_buf_size+1;i++)
				{
					printf("%s\n",buffer[i]);
				}
				break;
			case 'u':
				scroll_up();
				break;
			case 'd':
				scroll_down();
				break;
			case 't':
				myprintf1("line 11 this is a test       k\0");
				myprintf1("line 12 this is a test       l\0");
				myprintf1("line 13 this is a test       m\0");
				myprintf1("line 14 this is a test       n\0");
				myprintf1("line 15 this is a test       o\0");
				break;
			case 'v':
				myprintf1("line 16 hello a test       p\0");
				myprintf1("line 17 hello a test       q\0");
				myprintf1("line 18 hello a test       r\0");
				myprintf1("line 19 hello a test       s\0");
				myprintf1("line 20 hello a test       t\0");
				break;
			case 'w':
				myprintf1("16 hello a test       p\0");
				myprintf1("17 hello a test       q\0");
				myprintf1("18 hello a test       r\0");
				myprintf1("19 hello a test       s\0");
				myprintf1("20 hello a test       t\0");
				break;
			case '1':
				break;
			case '2':
				break;
			case '3':
				break;
			case '4':
				break;
			case '5':
				break;
			case '6':
				break;
			case '7':
				break;
			case '8':
				break;
			case '9':
				break;
			default:
				break;
		}
	}while(key != 'q');
}
#endif
#else
int myprintf1(char *str)
{

	char *ptr = str;
	//printf("%s\n",str);
	return 1;
}
void lcd_cls(void)
{
	return;
}
void scroll_up(void)
{
	return;
}
#endif
