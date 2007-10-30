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

char buff[512];

SceUID fd;

//#define printf(format, args...) \
//	sprintf(buff, format, ## args); \
//	fd = sceIoOpen("ms0:/hook-log.txt", PSP_O_WRONLY | PSP_O_CREAT | PSP_O_APPEND, 0777); \
//	sceIoWrite(fd, buff, strlen(buff)); \
//	sceIoClose(fd);

#define J_OPCODE	0x08000000
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

void sceUtilitySavedataUpdate_patched(int unknown);
void (sceUtilitySavedataUpdate_ptr)(int unknown);
u32 sceUtilitySavedataUpdate_j;
u32 sceUtilitySavedataUpdate_bd;

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

		if (strcmp(entry->libname, szLib) == 0)
		{
			if(entry->stubcount > 0)
			{
				//SHELL_PRINT("Function Exports:\n");
				for(count = 0; count < entry->stubcount; count++)
				{
					if (vars[count] == nid)
					{
						printf("Found: %08X\n", vars[count+total]);
						return vars[count+total];
					}
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

void set_hook()
{
   sceKernelStartModule_ptr = (void *)FindProc("sceModuleManager", "ModuleMgrForUser", 0x50F0C1EC);
   sceKernelStartModule_j = ((u32*)sceKernelStartModule_ptr)[0];
   sceKernelStartModule_bd = ((u32*)sceKernelStartModule_ptr)[1];
   REDIRECT_FUNCTION(FindProc("sceModuleManager", "ModuleMgrForUser", 0x50F0C1EC), sceKernelStartModule_patched);
   ClearCaches();
}

void set_savehook()
{
	sceUtilitySavedataInitStart_ptr = (void*)FindProc("sceUtility_Driver", "sceUtility", 0x50C4CD57);
	sceUtilitySavedataInitStart_j = ((u32*)sceUtilitySavedataInitStart_ptr)[0];
	sceUtilitySavedataInitStart_bd = ((u32*)sceUtilitySavedataInitStart_ptr)[1];
	REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x50C4CD57), sceUtilitySavedataInitStart_patched);

	sceUtilitySavedataGetStatus_ptr = (void*)FindProc("sceUtility_Driver", "sceUtility", 0x8874DBE0);
	sceUtilitySavedataGetStatus_j = ((u32*)sceUtilitySavedataGetStatus_ptr)[0];
	sceUtilitySavedataGetStatus_bd = ((u32*)sceUtilitySavedataGetStatus_ptr)[1];
	REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x8874DBE0), sceUtilitySavedataGetStatus_patched);

	sceUtilitySavedataShutdownStart_ptr = (void*)FindProc("sceUtility_Driver", "sceUtility", 0x9790B33C);
	sceUtilitySavedataShutdownStart_j = ((u32*)sceUtilitySavedataShutdownStart_ptr)[0];
	sceUtilitySavedataShutdownStart_bd = ((u32*)sceUtilitySavedataShutdownStart_ptr)[1];
	REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x9790B33C), sceUtilitySavedataShutdownStart_patched);
}


int sceKernelStartModule_patched(SceUID modid, SceSize argsize, void* argp, int* status, SceKernelSMOption* options)
{
	int k1 = pspSdkSetK1(0);

	SceModule* loadedModule = sceKernelFindModuleByUID(modid);
	if (strstr(loadedModule->modname, "sceUtility"))
	{
		set_savehook();
	}

	pspSdkSetK1(k1);

	((u32*)sceKernelStartModule_ptr)[0] = sceKernelStartModule_j;
	((u32*)sceKernelStartModule_ptr)[1] = sceKernelStartModule_bd;

	ClearCaches();

	int ret = sceKernelStartModule_ptr(modid, argsize, argp, status, options);

	REDIRECT_FUNCTION(FindProc("sceModuleManager", "ModuleMgrForUser", 0x50F0C1EC), sceKernelStartModule_patched);
	ClearCaches();

	return ret;
}

int sceUtilitySavedataInitStart_patched(SceUtilitySavedataParam* params)
{
	int k1 = pspSdkSetK1(0);

	currentParams = params;
	if (params->mode == 1)
	{
		// Grab the data being saved and put it on memory stick

		int fd = sceIoOpen("ms0:/plain.bin", PSP_O_CREAT | PSP_O_TRUNC | PSP_O_WRONLY, 0777);
		if (fd)
		{
			int written = 0;
			int size = params->dataBufSize;
			while(written < size)
			{
				int ret;

				ret = sceIoWrite(fd, (void*)(params->dataBuf + written), size - written);
				if(ret <= 0)
				{
					break;
				}

				written += ret;
			}

			sceIoClose(fd);
		}
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
		int fd = sceIoOpen("ms0:/plain.bin", PSP_O_RDONLY, 0777);
		if (fd >= 0)
		{
			int size = currentParams->dataBufSize;

			int readbytes = 0;

			while (readbytes < size)
			{
				int ret = sceIoRead(fd, (void*)(currentParams->dataBuf + readbytes), size - readbytes);
				if (ret == 0)
				{
					break;
				}

				readbytes += ret;
			}

			sceIoClose(fd);
		}
		// read data into params->dataBuf
		pspSdkSetK1(k1);
	}

	REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x8874DBE0), sceUtilitySavedataGetStatus_patched);
	ClearCaches();

	return ret;
}

int threadMain(SceSize args, void *argp)
{
	//loadUsb();
	printf("in thread 90\n");
	//set_hook();
	set_savehook();

	return 0;
}

int module_start(SceSize args, void *argp)
{
	
	int main_thid = sceKernelCreateThread("myhook", threadMain, 6, 0x04000, 0, NULL);

	printf("Created thread 100\n");

	if(main_thid >= 0) sceKernelStartThread(main_thid, args, argp);
	printf("started 103\n");

	return 0;

	sceKernelExitDeleteThread(0);
}

int module_stop(SceSize args, void *argp)
{
	return 0;
}

