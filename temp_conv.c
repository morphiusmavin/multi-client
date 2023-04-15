#include <stdio.h>
#include "raw_data.h"

char *lookup_raw_data(int val)
{
	int i = 0;
	while(raw_data[i].raw != val && i++ < 360);
	return raw_data[i].str;
}

int main(void)
{
	float F, C, fval;
	int i;

	for(i = 0;i < 360;i++)
	{
		printf("%s\n",lookup_raw_data(raw_data[i].raw));
	}

	for(i = 0;i < 360;i++)
	{
		fval = (float)raw_data[i].raw;
		//printf("%.2f\t\t",fval);

		if(fval >= 0.0 && fval <= 250.0)
		{
			C = fval/2.0;

		}
		else if(fval >= 403 && fval <= 511)
		{
			C = (fval - 512.0)/2.0;
		}
		F = C*9.0;
		F /= 5.0;
		F += 32.0;
		printf("%.1f\t\t%s\t\t%.1f \n",C,raw_data[i].str,F);

	}
	return 0;
}

// C = 5/9(F-32)

// F = (9/5)C + 32
