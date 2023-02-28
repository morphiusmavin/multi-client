#include <stdio.h>
#include <fcntl.h>
#include <sys/select.h>
#include <sys/stat.h>
#include <unistd.h>

int main(int argc, char **argv)
{
	char proc_irq[32];
	int ret, irqfd = 0;
	int buf; // Holds irq junk data
	fd_set fds;

	if(argc < 2) {
		printf("Usage: %s <irq number>\n", argv[0]);
		return 1;
	}

	snprintf(proc_irq, sizeof(proc_irq), "/proc/irq/%d/irq", atoi(argv[1]));
	irqfd = open(proc_irq, O_RDONLY| O_NONBLOCK, S_IREAD);

	if(irqfd == -1) {
		printf("Could not open IRQ %s\n", argv[1]);
		return 1;
	}
	
	while(1) {
		FD_SET(irqfd, &fds); //add the fd to the set
		// See if the IRQ has any data available to read
		ret = select(irqfd + 1, &fds, NULL, NULL, NULL);
		
		if(FD_ISSET(irqfd, &fds))
		{
			FD_CLR(irqfd, &fds);  //Remove the filedes from set
			printf("IRQ detected\n");
			
			// Clear the junk data in the IRQ file
			read(irqfd, &buf, sizeof(buf));
		}
		
		//Sleep, or do any other processing here
		usleep(10000);
	}
	
	return 0;
}