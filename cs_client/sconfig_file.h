extern sllist_t dll;
int slLoadConfig(char *filename, sllist_t *dll, size_t size,char *errmsg);
int slWriteConfig(char *filename,  sllist_t *dll, int no_recs, char *errmsg);
int sgetFileCreationTime2(char *path,char *str);
int sGetnRecs(char *filename, char *errmsg);
