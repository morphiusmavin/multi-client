#include <stdio.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include "mytypes.h"
#include "cmd_types.h"

CMD_STRUCT cmd_array[NO_CMDS] =
{
	{	NON_CMD,"NON_CMD\0" },
	{	DESK_LIGHT,"DESK_LIGHT\0" },
	{	EAST_LIGHT,"EAST_LIGHT\0" },
	{	NORTHWEST_LIGHT,"NORTHWEST_LIGHT\0" },
	{	SOUTHEAST_LIGHT,"SOUTHEAST_LIGHT\0" },
	{	MIDDLE_LIGHT,"MIDDLE_LIGHT\0" },
	{	WEST_LIGHT,"WEST_LIGHT\0" },
	{	NORTHEAST_LIGHT,"NORTHEAST_LIGHT\0" },
	{	SOUTHWEST_LIGHT,"SOUTHWEST_LIGHT\0" },
	{	WATER_HEATER,"WATER_HEATER\0" },
	{	WATER_PUMP,"WATER_PUMP\0" },
	{	WATER_VALVE1,"WATER_VALVE1\0" },
	{	WATER_VALVE2,"WATER_VALVE2\0" },
	{	WATER_VALVE3,"WATER_VALVE3\0" },
	{	BENCH_24V_1,"24V_1\0" },
	{	BENCH_24V_2,"24V_2\0" },
	{	BENCH_12V_1,"12V_1\0" },
	{	BENCH_12V_2,"12V_2\0" },
	{	BLANK,"BLANK\0" },
	{	BENCH_5V_1,"5V_1\0" },
	{	BENCH_5V_2,"5V_2\0" },
	{	BENCH_3V3_1,"3V3_1\0" },
	{	BENCH_3V3_2,"3V3_2\0" },
	{	BENCH_LIGHT1,"BENCH_LIGHT1\0" },
	{	BENCH_LIGHT2,"BENCH_LIGHT2\0" },
	{	BATTERY_HEATER,"BATTERY_HEATER\0" },
	{	CABIN1,"CABIN1\0" },
	{	CABIN2,"CABIN2\0" },
	{	CABIN3,"CABIN3\0" },
	{	CABIN4,"CABIN4\0" },
	{	CABIN5,"CABIN5\0" },
	{	CABIN6,"CABIN6\0" },
	{	CABIN7,"CABIN7\0" },
	{	CABIN8,"CABIN8\0" },
	{	COOP1_LIGHT,"COOP1_LIGHT\0" },
	{	COOP1_HEATER,"COOP1_HEATER\0" },
	{	COOP2_LIGHT,"COOP2_LIGHT\0" },
	{	COOP2_HEATER,"COOP2_HEATER\0" },
	{	OUTDOOR_LIGHT1,"OUTDOOR_LIGHT1\0" },
	{	OUTDOOR_LIGHT2,"OUTDOOR_LIGHT2\0" },
	{	UNUSED150_1,"UNUSED150_1\0" },
	{	UNUSED150_2,"UNUSED150_2\0" },
	{	UNUSED150_3,"UNUSED150_3\0" },
	{	UNUSED150_4,"UNUSED150_4\0" },
	{	UNUSED150_5,"UNUSED150_5\0" },
	{	UNUSED150_6,"UNUSED150_6\0" },
	{	UNUSED150_7,"UNUSED150_7\0" },
	{	UNUSED150_8,"UNUSED150_8\0" },
	{	UNUSED150_9,"UNUSED150_9\0" },
	{	UNUSED150_10,"UNUSED150_10\0" },
	{	GET_TEMP4,"GET_TEMP4\0" },
	{	SHUTDOWN_IOBOX,"SHUTDOWN_IOBOX\0" },
	{	REBOOT_IOBOX,"REBOOT_IOBOX\0" },
	{	SET_TIME,"SET_TIME\0" },
	{	GET_TIME,"GET_TIME\0" },
	{	DISCONNECT,"DISCONNECT\0" },
	{	BAD_MSG,"BAD_MSG\0" },
	{	SEND_TIMEUP,"SEND_TIMEUP\0" },
	{	SET_PARAMS,"SET_PARAMS\0" },
	{	UPTIME_MSG,"UPTIME_MSG\0" },
	{	SEND_CONFIG,"SEND_CONFIG\0" },
	{	SEND_STATUS,"SEND_STATUS\0" },
	{	GET_VERSION,"GET_VERSION\0" },
	{	UPDATE_CONFIG,"UPDATE_CONFIG\0" },
	{	SEND_CLIENT_LIST,"SEND_CLIENT_LIST\0" },
	{	GET_CONFIG2,"GET_CONFIG2\0" },
	{	SHELL_AND_RENAME,"SHELL_AND_RENAME\0" },
	{	EXIT_TO_SHELL,"EXIT_TO_SHELL\0"},
	{	UPDATE_STATUS,"UPDATE_STATUS\0" },
	{	SEND_MESSAGE,"SEND_MESSAGE\0" },
	{	CLIENT_RECONNECT,"CLIENT_RECONNECT\0" },
	{	SET_NEXT_CLIENT,"SET_NEXT_CLIENT\0" },
	{	SEND_NEXT_CLIENT,"SEND_NEXT_CLIENT\0" },
	{	UPDATE_CLIENT_INFO,"UPDATE_CLIENT_INFO\0" },
	{	AREYOUTHERE,"AREYOUTHERE\0" },
	{	YESIMHERE,"YESIMHERE\0" },
	{	GET_CLLIST,"GET_CLLIST\0" },
	{	GET_ALL_CLLIST,"GET_ALL_CLLIST\0" },
	{	REPLY_CLLIST,"REPLY_CLLIST\0" },
	{	SET_CLLIST,"SET_CLLIST\0" },
	{	SAVE_CLLIST,"SAVE_CLLIST\0" },
	{	NO_CLLIST_REC,"NO_CLLIST_REC\0" },
	{	SHOW_CLLIST,"SHOW_CLLIST\0" },
	{	CLEAR_CLLIST,"CLEAR_CLLIST\0" },
	{	SORT_CLLIST,"SORT_CLLIST\0" },
	{	DISPLAY_CLLIST_SORT,"DISPLAY_CLLIST_SORT\0" },
	{	RELOAD_CLLIST,"RELOAD_CLLIST\0" }
};

int main(void)
{
	int i;
	for(i = 0;i < NO_CMDS;i++)
		printf("%02d\t\t%02x\t\t%s\n",cmd_array[i].cmd, cmd_array[i].cmd, cmd_array[i].cmd_str);
}
