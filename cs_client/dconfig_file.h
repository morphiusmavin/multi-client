#ifndef CONFIG_FILE
#warning "CONFIG_FILE not defined"
extern dllist_t dll;
int dlLoadConfig(char *filename, dllist_t *dll, size_t size,char *errmsg);
int dlWriteConfig(char *filename,  dllist_t *dll, int no_recs, char *errmsg);
#else
#warning "CONFIG_FILE defined"
#endif
int dlLoadConfig(char *filename, dllist_t *dll, size_t size, char *errmsg);
int dWriteConfig(char *filename, D_DATA *curr_o_array,size_t size,char *errmsg);
int dWriteConfigXML(char *filename, D_DATA *curr_o_array,size_t size,char *errmsg);
int dGetFileFormat2(char *filename);
int dgetFileCreationTime2(char *path,char *str);
int dWriteParams(char *filename, PARAM_STRUCT *ps, char *password, char *errmsg);
int dGetnRecs(char *filename, char *errmsg);
