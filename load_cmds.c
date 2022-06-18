#include <sys/ipc.h>
#include <sys/msg.h>
#include "mytypes.h"
#include "cmd_types.h"

CMD_STRUCT cmd_array[NO_CMDS] =
{
	{	NON_CMD,"NON_CMD\0" },
	{	ALL_LIGHTS_ON,"ALL_LIGHTS_ON\0" },
	{	ALL_LIGHTS_OFF,"ALL_LIGHTS_OFF\0" },
	{	ALL_NORTH_ON,"ALL_NORTH_ON\0" },
	{	ALL_SOUTH_ON,"ALL_SOUTH_ON\0" },
	{	ALL_MIDDLE_ON,"ALL_MIDDLE_ON\0" },
	{	ALL_NORTH_OFF,"ALL_NORTH_OFF\0" },
	{	ALL_SOUTH_OFF,"ALL_SOUTH_OFF\0" },
	{	ALL_MIDDLE_OFF,"ALL_MIDDLE_OFF\0" },
	{	ALL_EAST_ON,"ALL_EAST_ON\0" },
	{	ALL_EAST_OFF,"ALL_EAST_OFF\0" },
	{	ALL_WEST_ON,"ALL_WEST_ON\0" },
	{	ALL_WEST_OFF,"ALL_WEST_OFF\0" },
	{	BLANK,"BLANK\0" },
	{	ALL_OFFICE_ON,"ALL_OFFICE_ON\0" },
	{	ALL_OFFICE_OFF,"ALL_OFFICE_OFF\0" },
	{	WORK_ON,"WORK_ON\0" },
	{	WORK_OFF,"WORK_OFF\0" },
	{	GET_TEMP4,"GET_TEMP4\0" },
	{	SHUTDOWN_IOBOX,"SHUTDOWN_IOBOX\0" },
	{	REBOOT_IOBOX,"REBOOT_IOBOX\0" },
	{	WAIT_REBOOT_IOBOX,"WAIT_REBOOT_IOBOX\0" },
	{	SET_TIME,"SET_TIME\0" },
	{	GET_TIME,"GET_TIME\0" },
	{	DISCONNECT,"DISCONNECT\0" },
	{	BAD_MSG,"BAD_MSG\0" },
	{	SEND_TIMEUP,"SEND_TIMEUP\0" },
	{	SET_PARAMS,"SET_PARAMS\0" },
	{	EXIT_PROGRAM,"EXIT_PROGRAM\0" },
	{	UPTIME_MSG,"UPTIME_MSG\0" },
	{	SEND_CONFIG,"SEND_CONFIG\0" },
	{	SEND_STATUS,"SEND_STATUS\0" },
	{	GET_VERSION,"GET_VERSION\0" },
	{	UPDATE_CONFIG,"UPDATE_CONFIG\0" },
	{	SEND_CLIENT_LIST,"SEND_CLIENT_LIST\0" },
	{	GET_CONFIG2,"GET_CONFIG2\0" },
	{	SHELL_AND_RENAME,"SHELL_AND_RENAME\0" },
	{	UPDATE_STATUS,"UPDATE_STATUS\0" },
	{	SEND_MSG,"SEND_MSG\0" },
	{	CLIENT_RECONNECT,"CLIENT_RECONNECT\0" },
	{	DB_LOOKUP,"DB_LOOKUP\0" },
	{	SET_TIMER,"SET_TIMER\0" },
	{	START_TIMER1,"START_TIMER1\0" },
	{	START_TIMER2,"START_TIMER2\0" },
	{	STOP_TIMER,"STOP_TIMER\0" },
	{	UPDATE_CLIENT_LIST,"UPDATE_CLIENT_LIST\0" }
};
