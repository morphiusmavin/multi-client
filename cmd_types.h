// msg's sent from client to TS-7200

enum cmd_types
{
	NON_CMD,
	ALL_LIGHTS_ON,
	ALL_LIGHTS_OFF,
	ALL_NORTH_ON,
	ALL_SOUTH_ON,
	ALL_MIDDLE_ON,
	ALL_NORTH_OFF,
	ALL_SOUTH_OFF,
	ALL_MIDDLE_OFF,
	ALL_EAST_ON,
	ALL_EAST_OFF,
	ALL_WEST_ON,
	ALL_WEST_OFF,
	BLANK,
	GET_TEMP4,
	SHUTDOWN_IOBOX,
	REBOOT_IOBOX,
	SET_TIME,
	GET_TIME,
	DISCONNECT,
	BAD_MSG,
	SEND_TIMEUP,
	SET_PARAMS,
	EXIT_PROGRAM,
	UPTIME_MSG,
	SEND_CONFIG,
	SEND_STATUS,
	UPLOAD_NEW,
	UPLOAD_OTHER,
	UPLOAD_NEW_PARAM,
	GET_VERSION,
	UPDATE_CONFIG,
	SEND_CLIENT_LIST,
	GET_CONFIG2,
	SHELL_AND_RENAME,
	TEST_IO_PORT,
	UPDATE_STATUS,
	UPDATE_ALL,
	SEND_MSG
}CMD_TYPES;

