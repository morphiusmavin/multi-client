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
/*
#include <stdio.h>

typedef struct
{
int temp;
int digital_output;
} TEMP_DIGITAL_OUTPUT;

// Define the input values
TEMP_DIGITAL_OUTPUT input_data[] = {
{125, 0x00FA},
{25, 0x0032},
{0.5, 0x0001},
{0, 0x0000},
{-0.5, 0x01FF},
{-25, 0x01CE},
{-55, 0x0192}
};

int main()
{
	int i, count;
	float sum_temp = 0;

	// Calculate the average temperature
	count = sizeof(input_data) / sizeof(input_data[0]);
	for (i = 0; i < count; i++) 
	{
		if (input_data.digital_output & 0x8000) { // Check if the temperature is negative
			input_data.temp = -(~(input_data.digital_output - 1) & 0x7FFF); // Convert the two's complement
		} else 
		{
			input_data.temp = input_data.digital_output & 0x7FFF; // Positive temperature
		}
		sum_temp += input_data.temp;
		}
		float average_temp = sum_temp / count;

		// Print the results
		printf("Input values:\n");
		for (i = 0; i < count; i++) {
		printf("%d°C 0x%04Xh\n", input_data.temp, input_data.digital_output);
	}
	printf("\nAverage temperature: %.2f°C\n", average_temp);

	return 0;
}
*/