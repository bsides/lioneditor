//////////////////////////////////////////////////
//* Custom Firmware Extender For 3.71 Firmware *//
//////////////////////////////////////////////////

#include <psptypes.h>
#include <pspkernel.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <psputility.h>

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

// hooked function, function pointer, and saved instructions for generic_sce_function_name
SceUID sceKernelLoadModule_patched(const char* path, int flags, SceKernelLMOption* option);
SceUID (*sceKernelLoadModule_ptr)(const char* path, int flags, SceKernelLMOption* option);
u32 sceKernelLoadModule_j;
u32 sceKernelLoadModule_bd;

int sceUtilitySavedataInitStart_patched(SceUtilitySavedataParam* params);
int (*sceUtilitySavedataInitStart_ptr)(SceUtilitySavedataParam* params);
u32 sceUtilitySavedataInitStart_j;
u32 sceUtilitySavedataInitStart_bd;


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
   sceKernelLoadModule_ptr = (void *)FindProc("sceModuleManager", "ModuleMgrForUser", 0x977DE386);
   sceKernelLoadModule_j = ((u32*)sceKernelLoadModule_ptr)[0];
   sceKernelLoadModule_bd = ((u32*)sceKernelLoadModule_ptr)[1];
   REDIRECT_FUNCTION(FindProc("sceModuleManager", "ModuleMgrForUser", 0x977DE386), sceKernelLoadModule_patched);
   ClearCaches();
}

void set_savehook()
{
	sceUtilitySavedataInitStart_ptr = (void*)FindProc("sceUtility_Driver", "sceUtility", 0x50C4CD57);
	sceUtilitySavedataInitStart_j = ((u32*)sceUtilitySavedataInitStart_ptr)[0];
	sceUtilitySavedataInitStart_bd = ((u32*)sceUtilitySavedataInitStart_ptr)[1];
	REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x50C4CD57), sceUtilitySavedataInitStart_patched);
}

SceUID sceKernelLoadModule_patched(const char* path, int flags, SceKernelLMOption* option)
{
   // do stuff
   if (strstr(path, "utility.prx"))
   {
	   set_savehook();
   }

   printf("hooked!\n");

   // un-set the hook by returning the original jump/branch code to the NID resolution address
   ((u32*)sceKernelLoadModule_ptr)[0] = sceKernelLoadModule_j;
   ((u32*)sceKernelLoadModule_ptr)[1] = sceKernelLoadModule_bd;
   ClearCaches();
   // get the return value
   int ret = sceKernelLoadModule_ptr(path, flags, option);
   // re-hook the function
   REDIRECT_FUNCTION(FindProc("sceModuleManager", "ModuleMgrForUser", 0x977DE386), sceKernelLoadModule_patched);
   ClearCaches();
   return ret;
}

int sceUtilitySavedataInitStart_patched(SceUtilitySavedataParam* params)
{
	int k1 = pspSdkSetK1(0);

	//printf("hooked savedatainitstart\n");

	if (params->mode)
	{
		//FILE* fp = fopen("ms0:/plain.bin", "wb");
		//fwrite(params->dataBuf, sizeof(unsigned char), params->dataBufSize, fp);
		//fclose(fp);

		int fd = sceIoOpen("ms0:/plain.bin", PSP_O_CREAT | PSP_O_TRUNC | PSP_O_WRONLY, 0777);
		if (fd)
		{
			int written = 0;
			int size = params->dataBufSize;
			while(written < size)
			{
				int ret;

				ret = sceIoWrite(fd, (void *) (params->dataBuf + written), size - written);
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

