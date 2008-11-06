#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <io.h>
#include <fcntl.h>
#define WIN32_LEAN_AND_MEAN		// 从 Windows 头中排除一些不常使用的部分，如GDI
#include <windows.h>

#include "ecc_edc.h"
#include "tools.h"
#include "iso9660.h"


#define PATCH_ERR_FILE_ISO		-1
#define PATCH_ERR_FILE_PATCH	-2
#define PATCH_ERR_FILE_PATCHLST	-3
#define PATCH_ERR_OFFSET		-4
#define PATCH_ERR_TEMPFILE		-5
#define PATCH_ERR_BYTES			-6
#define PATCH_ERR_LINETOOLONG	-7
#define PATCH_ERR_UNK_COMMAND	-8
#define PATCH_ERR_UNK_MODE		-9
#define PATCH_ERR_DIRECT		-10

char *error_string[] = 
{
	"no error",
	"can't access ISO file",
	"can't access patch file",
	"can't access patch list file",
	"start offset is incorrect",
	"can't create temp file",
	"raw bytes is incorrect",
	"patch line too long",
	"unknown command",
	"unknown iso mode",
	"direct mode err"
};

//iso_offset 32bit整数。能表示的数字最大约0x80000000，这个足够表示iso的，iso9660一般最多0x2???????
//如果换成64bit大整数，vb调用起来就略麻烦了，也暂时没必要。
//函数返回patch的字节数。如果错误返回负数，为各种可能的常量。
int WINAPI iso_patch_file(int iso_type, char *iso_file, int need_ecc_edc, int iso_offset, char *patch_file)
{
	int fhISO, fhPatch;
	int sector_start;
	int sector_length;
	BYTE sector[2352];

	// check that the supplied ISO offset is sane
	if (iso_offset <= 0) 
	{
		return PATCH_ERR_OFFSET;
	}
	sector_start = iso_offset % 2352;
	if( sector_start < ISO9660_DATA_START[iso_type] ||
		sector_start >= ISO9660_DATA_START[iso_type] + ISO9660_DATA_SIZE[iso_type] ) 
	{
		return PATCH_ERR_OFFSET;
	}

	fhPatch = _open(patch_file, _O_BINARY | _O_RDONLY);
	if(fhPatch == -1)
	{ 
		return PATCH_ERR_FILE_PATCH;
	}

	fhISO = _open(iso_file, _O_BINARY | _O_RDWR);
	if(fhISO == -1)
	{ 
		_close(fhPatch); 
		return PATCH_ERR_FILE_ISO;
	}

	sector_length = ISO9660_DATA_SIZE[iso_type] +  ISO9660_DATA_START[iso_type] - sector_start;

	eccedc_init();

	int total_patched_bytes = 0;
	int size_read;
	int nTmp;
	// nTmp is start of sector
	nTmp = iso_offset - (iso_offset % 2352);

	// seek to start of sector
	_lseek(fhISO, nTmp, SEEK_SET);

	// Until done reading bytes...
	for (;_eof(fhPatch)==0;)
	{
		// Read the current sector
		_read(fhISO,sector,2352);

		// Read the data out of the input file and into the sector
		size_read = _read(fhPatch,
						&sector[sector_start],
						sector_length);
	
		if ((sector[0x12] & 8) == 0)
		{
			sector[0x12] = 8;
			sector[0x16] = 8;
		}

		if (need_ecc_edc != 0)
		{
			// Fixup ECC/EDC
			eccedc_generate(sector, iso_type);
		}
		
		_lseek(fhISO, -2352, SEEK_CUR); // Seek to beginning of sector
		_write(fhISO, sector, 2352); // Write the crap
		total_patched_bytes += size_read; // Update byte count
		sector_start = ISO9660_DATA_START[iso_type];
		sector_length = ISO9660_DATA_SIZE[iso_type];
	}

	_close(fhPatch);
	_close(fhISO);

	return total_patched_bytes;
}

int WINAPI iso_patch_sector(char *iso_file, int iso_offset, char *patch_file)
{
	int fhISO, fhPatch;

	//**** 初始化＋合法性检查, 不合法返回错误 
	if (iso_offset <= 0) return PATCH_ERR_OFFSET;

	fhPatch = _open(patch_file, _O_BINARY | _O_RDONLY);
	if(fhPatch == -1){ return PATCH_ERR_FILE_PATCH;}

	fhISO = _open(iso_file, _O_BINARY | _O_RDWR);
	if(fhISO == -1){ _close(fhPatch); return PATCH_ERR_FILE_ISO;}

	//**** 写入
	int total_patched_bytes = 0;


	static char buff[1024*1024];
	int nBuff;
	_lseek(fhPatch, 0, SEEK_END);
	nBuff = _tell(fhPatch);
	_lseek(fhPatch, 0, SEEK_SET);
	if(nBuff > sizeof(buff)) return PATCH_ERR_DIRECT;
	_read(fhPatch,buff,nBuff);

	_lseek(fhISO, iso_offset, SEEK_SET);
	_write(fhISO, buff, nBuff);							//写入修改后的扇区数据
	total_patched_bytes += nBuff;						//一个 patch 数据文件实际写入字节数计数

	_close(fhPatch);
	_close(fhISO);

	return total_patched_bytes;
}


int WINAPI iso_patch_byte(int iso_type, char *iso_file, int need_ecc_edc, int iso_offset, char *patch_buf, int patch_bytes)
{
	int nRet;
	char *pTmpFile = "$$$patch.$$$";
	nRet = dump_bin(pTmpFile, patch_buf, patch_bytes);
	if (nRet < 0) return PATCH_ERR_TEMPFILE;

	nRet = iso_patch_file(iso_type, iso_file, need_ecc_edc, iso_offset, pTmpFile);
	_unlink(pTmpFile);

	return nRet;
}

int iso_patch_list_core(int iso_type, char *iso_file, int need_ecc_edc, char *patch_list_file, int isTestMode, int *ErrorLine)
{
	#define MAX_PATCH_BYTE	32*1024				//一行直接写入的字节最多是多少字节
	#define MAX_FILENAME	1024
	#define MAX_LINE		MAX_PATCH_BYTE+10	//一行最多是多少字节
	char buf_line[MAX_LINE];
	char buf_byte[MAX_PATCH_BYTE];				//直接写入的字节
	char patch_file[MAX_FILENAME];
	int  buf_byte_len;
	char buf_tmp[1024];
	char *off, *param;
	int iso_offset;
	int data_type;
	int patch_bytes_current, patch_bytes_total;

	FILE *fp;
	fp = fopen(patch_list_file, "r");
	if (fp == NULL) return PATCH_ERR_FILE_PATCHLST;
	*ErrorLine = 1;
	patch_bytes_total = 0;

	MyTrace("Mode         : ");
	if (iso_type == ISO9660_M1)
		MyTrace("MODE1\n");
	else if(iso_type == ISO9660_M2F1)
		MyTrace("MODE2 FORM1\n");
	else if(iso_type == ISO9660_M2F2)
		MyTrace("MODE2 FORM2\n");
	else
	{
		MyTrace("bad mode %d\n", iso_type);
		return PATCH_ERR_UNK_MODE;
	}
	MyTrace("ECC/EDC calc : ");
	if(need_ecc_edc)
		MyTrace("on\n");
	else
		MyTrace("off\n");

    MyTrace("list file    : %s\n", patch_list_file);
	MyTrace("iso  file    : %s\n", iso_file);
	MyTrace("\n");

	if(isTestMode)
		MyTrace("loading  list ...\n");
	else
		MyTrace("patching list ...\n");


	MyTrace("-----------------------------------------------------------------------------\n");
	MyTrace("line | offset   <- data      \t| length    \n");
	MyTrace("-----------------------------------------------------------------------------\n");

	for(;;*ErrorLine = *ErrorLine + 1)
	{
		if(feof(fp))break;
		memset(buf_line, 0, sizeof(buf_line));
		fgets(buf_line, MAX_LINE-1, fp);

		if( lstrlen(buf_line) >= MAX_LINE-1) return PATCH_ERR_LINETOOLONG;
		if(buf_line[lstrlen(buf_line)-1] == 0x0a) buf_line[lstrlen(buf_line)-1] = 0;

		if(buf_line[0] == 0)continue;
		if(buf_line[0] == ';')continue;
		if(buf_line[0] == '#')continue;

		data_type = 0;
		if(buf_line[8] == ',')data_type = 1;
		if(buf_line[8] == '*')data_type = 2;
		if(buf_line[8] == '@')data_type = 3;	//直接写入RAW 2352扇区
		if (data_type == 0) return PATCH_ERR_UNK_COMMAND;

		buf_line[8] = 0;	//形成off字符串
		off = buf_line;
		param = buf_line + 8 + 1;
		sscanf(off, "%x", &iso_offset);
		sprintf(buf_tmp, "%08X", iso_offset);
 		if(lstrcmpi(buf_tmp, off) != 0)
		{
			fclose(fp);
			return PATCH_ERR_OFFSET;
		}
		if (iso_offset <= 0) return PATCH_ERR_OFFSET;
		int sector_start = iso_offset % 2352;
		if(data_type != 3)
			if(sector_start < ISO9660_DATA_START[iso_type] || sector_start >= ISO9660_DATA_START[iso_type]+ISO9660_DATA_SIZE[iso_type]) return PATCH_ERR_OFFSET;

		if(data_type == 1)	//命令处理
		{
			get_path(patch_list_file, buf_tmp, sizeof(buf_tmp));
			sprintf(patch_file, "%s%s", buf_tmp, param);

			if(isTestMode)
			{
				MyTrace("%4d | %08X", *ErrorLine, iso_offset);
				MyTrace(" <- %s", param);
				patch_bytes_current = get_filelen(patch_file);
			}
			else
			{
				MyTrace("%4d | %08X", *ErrorLine, iso_offset);
				MyTrace(" <- %s ...", param);
				patch_bytes_current = iso_patch_file(iso_type, iso_file, need_ecc_edc, iso_offset, patch_file);
				if (patch_bytes_current < 0) return patch_bytes_current;
				patch_bytes_total += patch_bytes_current;
			}

		}
		else if (data_type == 2)
		{
			memset(buf_byte, 0, sizeof(buf_byte));
			buf_byte_len = HexStr2Byte(param, buf_byte, sizeof(buf_byte));
			if(buf_byte_len < 0) return PATCH_ERR_BYTES;

			if(isTestMode)
			{
				MyTrace("%4d | %08X", *ErrorLine, iso_offset);
				MyTrace(" <- hex string   ");
				patch_bytes_current = buf_byte_len;
			}
			else
			{
				MyTrace("%4d | %08X", *ErrorLine, iso_offset);
				MyTrace(" <- hex string ...");
				patch_bytes_current = iso_patch_byte(iso_type, iso_file, need_ecc_edc, iso_offset, buf_byte, buf_byte_len);
				if (patch_bytes_current < 0) return patch_bytes_current;
				patch_bytes_total += patch_bytes_current;
			}

		}
		else
		{
			get_path(patch_list_file, buf_tmp, sizeof(buf_tmp));
			sprintf(patch_file, "%s%s", buf_tmp, param);

			if(isTestMode)
			{
				MyTrace("%4d | %08X", *ErrorLine, iso_offset);
				MyTrace(" <-RAW %s", param);
				patch_bytes_current = get_filelen(patch_file);
			}
			else
			{
				MyTrace("%4d | %08X", *ErrorLine, iso_offset);
				MyTrace(" <-RAW %s ...", param);
				patch_bytes_current = iso_patch_sector(iso_file, iso_offset, patch_file);
				if (patch_bytes_current < 0) return patch_bytes_current;
				patch_bytes_total += patch_bytes_current;
			}

		}
		
		MyTrace("\t| %d\n", patch_bytes_current);
	}

	fclose(fp);
	*ErrorLine = 0;
	return patch_bytes_total;
}
int WINAPI iso_patch_list(int iso_type, char *iso_file, int need_ecc_edc, char *patch_list_file)
{

	int nRet;
	int nErrLine = 0;

	//先模拟写入，测试合法性
	nRet = iso_patch_list_core(iso_type, iso_file, need_ecc_edc, patch_list_file, 1, &nErrLine);
	if (nRet < 0)
	{
		MyTrace("%4d | error(%d) %s\n", nErrLine, nRet, error_string[abs(nRet)] );
		return nRet;
	}
	
	MyTrace("-----------------------------------------------------------------------------\n");
	MyTrace("Press any key to start patching, press CTRL+BREAK to abort.");
	getch();
	MyTrace("\n");

	//全部合法了以后真实写入
	nRet = iso_patch_list_core(iso_type, iso_file, need_ecc_edc, patch_list_file, 0, &nErrLine);
	if (nRet < 0)
	{
		MyTrace("%4d | error(%d) %s\n", nErrLine, nRet, error_string[abs(nRet)] );
		return nRet;
	}

	MyTrace("-----------------------------------------------------------------------------\n");
	return nRet;
}

