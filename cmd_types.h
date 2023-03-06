// msg's sent from client to TS-7200

enum cmd_types
{
	NON_CMD,
	DESK_LIGHT,
	EAST_LIGHT,
	NORTHWEST_LIGHT,
	SOUTHEAST_LIGHT,
	MIDDLE_LIGHT,
	WEST_LIGHT,
	NORTHEAST_LIGHT,
	SOUTHWEST_LIGHT,
	WATER_PUMP,
	WATER_VALVE1,
	WATER_VALVE2,
	WATER_VALVE3,
	WATER_HEATER,		// last one on garage
	BENCH_24V_1,		// start of 147
	BENCH_24V_2,
	BENCH_12V_1,
	BENCH_12V_2,
	BLANK,
	BENCH_5V_1,
	BENCH_5V_2,
	BENCH_3V3_1,
	BENCH_3V3_2,
	BENCH_LIGHT1,
	BENCH_LIGHT2,
	BATTERY_HEATER,		// last one for 147
	CABIN1,				// start of 154
	CABIN2,
	CABIN3,
	CABIN4,
	CABIN5,
	CABIN6,
	CABIN7,
	CABIN8,				// last one for 154
	COOP1_LIGHT,		// start of 150
	COOP1_HEATER,
	COOP2_LIGHT,
	COOP2_HEATER,
	OUTDOOR_LIGHT1,
	OUTDOOR_LIGHT2,
	UNUSED150_1,
	UNUSED150_2,
	UNUSED150_3,
	UNUSED150_4,
	UNUSED150_5,
	UNUSED150_6,
	UNUSED150_7,
	UNUSED150_8,
	UNUSED150_9,
	UNUSED150_10,
	GET_TEMP4,
	SHUTDOWN_IOBOX,
	REBOOT_IOBOX,
	SET_TIME,
	GET_TIME,
	DISCONNECT,
	BAD_MSG,
	SEND_TIMEUP,
	SET_PARAMS,
	UPTIME_MSG,
	SEND_CONFIG,
	SEND_STATUS,
	GET_VERSION,
	UPDATE_CONFIG,
	SEND_CLIENT_LIST,
	GET_CONFIG2,
	SHELL_AND_RENAME,
	EXIT_TO_SHELL,
	UPDATE_STATUS,
	SEND_MESSAGE,
	CLIENT_RECONNECT,
	SET_NEXT_CLIENT,
	SEND_NEXT_CLIENT,
	UPDATE_CLIENT_INFO,
	GET_CLLIST,
	GET_ALL_CLLIST,
	REPLY_CLLIST,
	SET_CLLIST,
	SAVE_CLLIST,
	NO_CLLIST_REC,
	SHOW_CLLIST,
	CLEAR_CLLIST,
	SORT_CLLIST,
	DISPLAY_CLLIST_SORT,
	RELOAD_CLLIST,
	SET_VALID_DS,
	SET_DS_INTERVAL,
	RENAME_D_DATA,
	DLLIST_SHOW,
	DLLIST_SAVE
}CMD_TYPES;

