//////////////////////////////////////////////////
//* Custom Firmware Extender For 3.71 Firmware *//
//////////////////////////////////////////////////

#include <psptypes.h>
#include <pspkernel.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <psputility.h>
#include <pspsdk.h>
#include <psputilsforkernel.h>

PSP_MODULE_INFO("myhook", 0x1000, 1, 1);

PSP_MAIN_THREAD_ATTR(0);
PSP_MAIN_THREAD_STACK_SIZE_KB(0);

/* 
char buff[512];

SceUID fd;

#define printf(format, args...) \
	sprintf(buff, format, ## args); \
	fd = sceIoOpen("ms0:/hook-log.txt", PSP_O_WRONLY | PSP_O_CREAT | PSP_O_APPEND, 0777); \
	sceIoWrite(fd, buff, strlen(buff)); \
	sceIoClose(fd); 
*/

#define J_OPCODE 0x08000000
#define NOP	0x00000000
#define REDIRECT_FUNCTION(a, f) _sw(J_OPCODE | (((u32)(f) >> 2)  & 0x03ffffff), a);  _sw(NOP, a+4);

int sceKernelStartModule_patched(SceUID modid, SceSize argsize, void* argp, int* status, SceKernelSMOption* options);
int (*sceKernelStartModule_ptr)(SceUID modid, SceSize argsize, void* argp, int* status, SceKernelSMOption* options);
u32 sceKernelStartModule_j;
u32 sceKernelStartModule_bd;

int sceUtilitySavedataInitStart_patched(SceUtilitySavedataParam* params);
int (*sceUtilitySavedataInitStart_ptr)(SceUtilitySavedataParam* params);
u32 sceUtilitySavedataInitStart_j;
u32 sceUtilitySavedataInitStart_bd;

int sceUtilitySavedataGetStatus_patched(void);
int (*sceUtilitySavedataGetStatus_ptr)(void);
u32 sceUtilitySavedataGetStatus_j;
u32 sceUtilitySavedataGetStatus_bd;

int sceUtilitySavedataShutdownStart_patched(void);
int (*sceUtilitySavedataShutdownStart_ptr)(void);
u32 sceUtilitySavedataShutdownStart_j;
u32 sceUtilitySavedataShutdownStart_bd;

SceUtilitySavedataParam* currentParams = NULL;

static u32 FindProc(const char* szMod, const char* szLib, u32 nid) 
{ 
    SceModule* modP = sceKernelFindModuleByName(szMod); 
	struct SceLibraryEntryTable* entry;
	void* entTab;
	int entLen;

    if (modP == NULL) 
    { 
        printf("Failed to find mod '%s'\n", szMod); 
        return 0; 
    } 

    //SceLibraryEntryTable* entP = (SceLibraryEntryTable*)modP->ent_top; 

	entTab = modP->ent_top;
	entLen = modP->ent_size;

	int i = 0;

	while(i < entLen)
	{
		int count;
		int total;
		unsigned int *vars;

		entry = (struct SceLibraryEntryTable *) (entTab + i);


		total = entry->stubcount + entry->vstubcount;
		vars = entry->entrytable;

		if ((strcmp(entry->libname, szLib) == 0) && (entry->stubcount > 0))
		{
			for(count = 0; count < entry->stubcount; count++)
			{
				if (vars[count] == nid)
				{
					return vars[count+total];
				}
			}
		}

		i += (entry->len * 4);
	}
	
	return 0;

}

void ClearCaches()
{
	sceKernelIcacheInvalidateAll();
	sceKernelDcacheWritebackInvalidateAll();
}

void set_startModulehook()
{
   sceKernelStartModule_ptr = (void *)FindProc("sceModuleManager", "ModuleMgrForUser", 0x50F0C1EC);
   sceKernelStartModule_j = ((u32*)sceKernelStartModule_ptr)[0];
   sceKernelStartModule_bd = ((u32*)sceKernelStartModule_ptr)[1];
   REDIRECT_FUNCTION(FindProc("sceModuleManager", "ModuleMgrForUser", 0x50F0C1EC), sceKernelStartModule_patched);
   ClearCaches();
}

int set_savehook()
{
	sceUtilitySavedataInitStart_ptr = (void*)FindProc("sceUtility_Driver", "sceUtility", 0x50C4CD57);
	if (sceUtilitySavedataInitStart_ptr == 0)
	{
		return -1;
	}
	sceUtilitySavedataInitStart_j = ((u32*)sceUtilitySavedataInitStart_ptr)[0];
	sceUtilitySavedataInitStart_bd = ((u32*)sceUtilitySavedataInitStart_ptr)[1];
	REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x50C4CD57), sceUtilitySavedataInitStart_patched);

	sceUtilitySavedataGetStatus_ptr = (void*)FindProc("sceUtility_Driver", "sceUtility", 0x8874DBE0);
	if (sceUtilitySavedataGetStatus_ptr == 0)
	{
		return -1;
	}
	sceUtilitySavedataGetStatus_j = ((u32*)sceUtilitySavedataGetStatus_ptr)[0];
	sceUtilitySavedataGetStatus_bd = ((u32*)sceUtilitySavedataGetStatus_ptr)[1];
	REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x8874DBE0), sceUtilitySavedataGetStatus_patched);

	sceUtilitySavedataShutdownStart_ptr = (void*)FindProc("sceUtility_Driver", "sceUtility", 0x9790B33C);
	if (sceUtilitySavedataShutdownStart_ptr == 0)
	{
		return -1;
	}
	sceUtilitySavedataShutdownStart_j = ((u32*)sceUtilitySavedataShutdownStart_ptr)[0];
	sceUtilitySavedataShutdownStart_bd = ((u32*)sceUtilitySavedataShutdownStart_ptr)[1];
	REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x9790B33C), sceUtilitySavedataShutdownStart_patched);

	return 0;
}


int sceKernelStartModule_patched(SceUID modid, SceSize argsize, void* argp, int* status, SceKernelSMOption* options)
{
	int k1 = pspSdkSetK1(0);
	int success = 0;

	SceModule* loadedModule = sceKernelFindModuleByUID(modid);
	if (strstr(loadedModule->modname, "sceUtility"))
	{
		if (set_savehook() != 0)
		{
			success = 1;
		}
	}

	pspSdkSetK1(k1);

	((u32*)sceKernelStartModule_ptr)[0] = sceKernelStartModule_j;
	((u32*)sceKernelStartModule_ptr)[1] = sceKernelStartModule_bd;

	ClearCaches();

	int ret = sceKernelStartModule_ptr(modid, argsize, argp, status, options);

	if (success == 0)
	{
		REDIRECT_FUNCTION(FindProc("sceModuleManager", "ModuleMgrForUser", 0x50F0C1EC), sceKernelStartModule_patched);
		ClearCaches();
	}

	return ret;
}

int writeDataToFile(const char* path, void* data, int size)
{
	int fd = sceIoOpen(path, PSP_O_CREAT | PSP_O_TRUNC | PSP_O_WRONLY, 0777);
	if (fd >= 0)
	{
		int written = 0;
		while (written < size)
		{
			int ret;
			ret = sceIoWrite(fd, data + written, size - written);
			if (ret <= 0)
			{
				break;
			}
			
			written += ret;
		}

		sceIoClose(fd);

		return 0;
	}

	return fd;
}

int readDataFromFile(const char* path, void* data, int size)
{
	int fd = sceIoOpen(path, PSP_O_RDONLY, 0777);
	if (fd >= 0)
	{
		int readbytes = 0;

		while (readbytes < size)
		{
			int ret = sceIoRead(fd, data + readbytes, size - readbytes);
			if (ret == 0)
			{
				break;
			}

			readbytes += ret;
		}

		sceIoClose(fd);

		return 0;
	}

	return fd;
}


int makeDecryptedDirectory(const char* dir)
{
	int ret = sceIoChdir("ms0:/decryptedSaves");
	if (ret < 0)
	{
		sceIoMkdir("ms0:/decryptedSaves", 0777);
		sceIoChdir("ms0:/decryptedSaves");
	}

	ret = sceIoChdir(dir);
	if (ret < 0)
	{
		sceIoMkdir(dir, 0777);
	}

	return ret;
}

int saveDecryptedSavedata(SceUtilitySavedataParam* params)
{
	char path[256] = { '\0' };
	sprintf(path, "%s%s", params->gameName, params->saveName);
	makeDecryptedDirectory(path);

	sprintf(path, "ms0:/decryptedSaves/%s%s/%s", params->gameName, params->saveName, params->fileName);
	writeDataToFile(path, params->dataBuf, params->dataBufSize);

	if (params->icon0FileData.buf)
	{
		sprintf(path, "ms0:/decryptedSaves/%s%s/ICON0.PNG", params->gameName, params->saveName);
		writeDataToFile(path, params->icon0FileData.buf, params->icon0FileData.bufSize);
	}
	if (params->icon1FileData.buf)
	{
		sprintf(path, "ms0:/decryptedSaves/%s%s/ICON1.PNG", params->gameName, params->saveName);
		writeDataToFile(path, params->icon1FileData.buf, params->icon1FileData.bufSize);
	}
	if (params->pic1FileData.buf)
	{
		sprintf(path, "ms0:/decryptedSaves/%s%s/PIC1.PNG", params->gameName, params->saveName);
		writeDataToFile(path, params->pic1FileData.buf, params->pic1FileData.bufSize);
	}
	if (params->snd0FileData.buf)
	{
		sprintf(path, "ms0:/decryptedSaves/%s%s/SND1.PNG", params->gameName, params->saveName);
		writeDataToFile(path, params->snd0FileData.buf, params->snd0FileData.bufSize);
	}
	
	sprintf(path, "ms0:/decryptedSaves/%s%s/PARAM", params->gameName, params->saveName);
	writeDataToFile(path, &(params->sfoParam), sizeof(params->sfoParam));

	return 0;
}

int loadDecryptedSavedata(SceUtilitySavedataParam* params)
{
	char path[256] = { '\0' };
	sprintf(path, "%s%s", params->gameName, params->saveName);

	int ret = makeDecryptedDirectory(path);
	if (ret < 0)
	{
		return ret;
	}

	sprintf(path, "ms0:/decryptedSaves/%s%s/%s", params->gameName, params->saveName, params->fileName);
	readDataFromFile(path, params->dataBuf, params->dataBufSize);

	if (params->icon0FileData.buf)
	{
		sprintf(path, "ms0:/decryptedSaves/%s%s/ICON0.PNG", params->gameName, params->saveName);
		readDataFromFile(path, params->icon0FileData.buf, params->icon0FileData.bufSize);
	}
	if (params->icon1FileData.buf)
	{
		sprintf(path, "ms0:/decryptedSaves/%s%s/ICON1.PNG", params->gameName, params->saveName);
		readDataFromFile(path, params->icon1FileData.buf, params->icon1FileData.bufSize);
	}
	if (params->pic1FileData.buf)
	{
		sprintf(path, "ms0:/decryptedSaves/%s%s/PIC1.PNG", params->gameName, params->saveName);
		readDataFromFile(path, params->pic1FileData.buf, params->pic1FileData.bufSize);
	}
	if (params->snd0FileData.buf)
	{
		sprintf(path, "ms0:/decryptedSaves/%s%s/SND1.PNG", params->gameName, params->saveName);
		readDataFromFile(path, params->snd0FileData.buf, params->snd0FileData.bufSize);
	}

	sprintf(path, "ms0:/decryptedSaves/%s%s/PARAM", params->gameName, params->saveName);
	readDataFromFile(path, &(params->sfoParam), sizeof(params->sfoParam));

	return 0;
}

int sceUtilitySavedataInitStart_patched(SceUtilitySavedataParam* params)
{
	int k1 = pspSdkSetK1(0);

	currentParams = params;
	if (params->mode == 1)
	{
		// Grab the data being saved and put it on memory stick
		writeDataToFile("ms0:/plain.bin", params->dataBuf, params->dataBufSize);
	}

	((u32*)sceUtilitySavedataInitStart_ptr)[0] = sceUtilitySavedataInitStart_j;
	((u32*)sceUtilitySavedataInitStart_ptr)[1] = sceUtilitySavedataInitStart_bd;
	ClearCaches();

	pspSdkSetK1(k1);

	int ret = sceUtilitySavedataInitStart_ptr(params);

	REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x50C4CD57), sceUtilitySavedataInitStart_patched);
	ClearCaches();


	return ret;
}

int sceUtilitySavedataShutdownStart_patched(void)
{
	currentParams = NULL;

	((u32*)sceUtilitySavedataShutdownStart_ptr)[0] = sceUtilitySavedataShutdownStart_j;
	((u32*)sceUtilitySavedataShutdownStart_ptr)[1] = sceUtilitySavedataShutdownStart_bd;
	ClearCaches();

	int ret = sceUtilitySavedataShutdownStart_ptr();

	REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x9790B33C), sceUtilitySavedataShutdownStart_patched);
	ClearCaches();

	return ret;
}

int sceUtilitySavedataGetStatus_patched(void)
{
	((u32*)sceUtilitySavedataGetStatus_ptr)[0] = sceUtilitySavedataGetStatus_j;
	((u32*)sceUtilitySavedataGetStatus_ptr)[1] = sceUtilitySavedataGetStatus_bd;
	ClearCaches();

	int ret = sceUtilitySavedataGetStatus_ptr();

	// Inject our own data
	if ((currentParams != NULL) && (currentParams->mode == 0) && (ret == 3))
	{
		int k1 = pspSdkSetK1(0);
		readDataFromFile("ms0:/plain.bin", currentParams->dataBuf, currentParams->dataBufSize);
		pspSdkSetK1(k1);
	}

	REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x8874DBE0), sceUtilitySavedataGetStatus_patched);
	ClearCaches();

	return ret;
}

int threadMain(SceSize args, void *argp)
{
	if (set_savehook() == -1)
	{
		set_startModulehook();
	}

	return 0;
}

int module_start(SceSize args, void *argp)
{
	
	int main_thid = sceKernelCreateThread("myhook", threadMain, 6, 0x04000, 0, NULL);

	if(main_thid >= 0) sceKernelStartThread(main_thid, args, argp);

	return 0;

	sceKernelExitDeleteThread(0); // unreachable?
}

int module_stop(SceSize args, void *argp)
{
	return 0;
}

