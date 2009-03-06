#ifndef _TOOLS_H
#define _TOOLS_H

int dump_bin(char *filename, char *pBuf, int nBytesToWrite);
int load_bin(char *filename, char *pBuf, int nBuffer);

int HexStr2Byte(char *pString, char *buf, int buf_len);
int get_filelen(char *filename);
int get_path(char *path_old, char *path_new, int path_new_len);
void MyTrace(const char *fmt, ...);
#endif 
