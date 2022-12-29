#include <stdio.h>
#include <stdlib.h>

#define XBEEDEVICE "/dev/ttyxuart1"

int main(int argc, char **argv)
{
    int fd, c, res;
    struct termios oldtio, newtio;
    char buf[255] = {0};
    
    fd = open(XBEEDEVICE, O_RDWR | O_NOCTTY ); 
    if(fd <0) 
    {
            perror(XBEEDEVICE); 
            return -1;
    }
    
    tcgetattr(fd, &oldtio); /* save current port settings */
    
    bzero(&newtio, sizeof(newtio));
    newtio.c_cflag = CRTSCTS | CS8 | CLOCAL | CREAD;
    newtio.c_iflag = IGNPAR;
    newtio.c_oflag = 0;
    /* set input mode (non-canonical, no echo,...) */
    newtio.c_lflag = 0;
     
    newtio.c_cc[VTIME]    = 0;   /* inter-character timer unused */
    newtio.c_cc[VMIN]     = 2;   /* blocking read until 2 chars received */
    
    tcflush(fd, TCIFLUSH);
    tcsetattr(fd, TCSANOW, &newtio);

    write(fd, "abc", 3);

    res = read(fd, buf, 255);
    buf[res] = 0;

    if(0 == strncmp(buf, "OK", 2))
    {
            printf("XBee Detected\n");
    }
    else
    {
            printf("Could not find XBee\n");
    }

    tcsetattr(fd, TCSANOW, &oldtio);

    return 0;
}