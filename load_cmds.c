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
	{	CHICK_WATER,"CHICK_WATER\0" },
	{	SET_CHICK_WATER_ON,"SET_CHICK_WATER_ON\0" },
	{	SET_CHICK_WATER_OFF,"SET_CHICK_WATER_OFF\0" },
	{	CHICK_WATER_ENABLE,"CHICK_WATER_ENABLE\0" },
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
	{	DB_LOOKUP,"DB_LOOKUP\0" },
	{	SET_TIMER,"SET_TIMER\0" },
	{	START_TIMER1,"START_TIMER1\0" },
	{	START_TIMER2,"START_TIMER2\0" },
	{	STOP_TIMER,"STOP_TIMER\0" },
	{	SET_NEXT_CLIENT,"SET_NEXT_CLIENT\0" },
	{	SEND_NEXT_CLIENT,"SEND_NEXT_CLIENT\0" },
	{	UPDATE_CLIENT_INFO,"UPDATE_CLIENT_INFO\0" },
	{	AREYOUTHERE,"AREYOUTHERE\0" },
	{	YESIMHERE,"YESIMHERE\0" }
};
