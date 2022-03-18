#ifndef CONFIG_FILE
#warning "CONFIG_FILE not defined"
extern cllist_t oll;
int clLoadConfig(char *filename, cllist_t *oll, size_t size,char *errmsg);
int clWriteConfig(char *filename, cllist_t *oll, size_t size,char *errmsg);
#else
#warning "CONFIG_FILE defined"
#endif
int cLoadConfig(char *filename, C_DATA *curr_o_array,size_t size,char *errmsg);
int cWriteConfig(char *filename, C_DATA *curr_o_array,size_t size,char *errmsg);
int cWriteConfigXML(char *filename, C_DATA *curr_o_array,size_t size,char *errmsg);
int GetFileFormat2(char *filename);
int getFileCreationTime2(char *path,char *str);
int WriteParams(char *filename, PARAM_STRUCT *ps, char *password, char *errmsg);
int LoadParams(char *filename, PARAM_STRUCT *ps, char *password, char *errmsg);
int LoadSpecialInputFunctions(IP *ip, int no_current_ips);