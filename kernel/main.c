/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>
    FindProc() adapted from code Copyright (c) 2005 James F <tyranid@gmail.com>

    This file is part of LionEditor.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

#include <psptypes.h>
#include <pspkernel.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <psputility.h>
#include <pspsdk.h>
#include <psputilsforkernel.h>

#ifdef DEBUG
#define PATH(x) "host0:/" x
#else
#define PATH(x) "ms0:/" x
#endif

#define FILE_NOT_FOUND 10

PSP_MODULE_INFO("FFTSaveHook", 0x1000, 1, 1);

PSP_MAIN_THREAD_ATTR(0);
PSP_MAIN_THREAD_STACK_SIZE_KB(0);

// Hooking routine by FreePlay
#define J_OPCODE 0x08000000
#define NOP    0x00000000
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

// Count the number of bits in a byte
int countBits(unsigned char b)
{
    int count = 0;

    int i = 0;
    for (i = 0; i < 8; i++) count += ((b >> i) & 0x01);

    return count;
}

// Calculate the checksum for a FFT savegame
// data should be 0x2A3C bytes long
// csum should be 11 bytes long
void calcChecksum(unsigned char* data, unsigned char* csum)
{
    int curbyte = 0;

    int cbyte = 0;
    for (cbyte = 0; cbyte < 10; cbyte++)
    {
        int cval = 0;
        int cbit = 0;
        for (cbit = 0; cbit < 8; cbit++)
        {
            int gbitsum = 0;
            int gbyte = 0;
            for (gbyte = 0; gbyte < 128; gbyte++)
            {
                gbitsum += countBits(data[curbyte]);
                curbyte++;
            }

            cval = cval << 1;
            cval += gbitsum%2;
        }

        csum[cbyte] = cval;
    }
}

// Calculates a checksum and adds it to the game
// If adding the checksum to the game changes the checksum, 
// it resolves the error
// savegame should be 0x2A3C bytes long
void addChecksum(unsigned char* savegame)
{
    unsigned char csum[10];

    savegame[0x118] = 0x00;
    calcChecksum(savegame, csum);

    savegame[0x27] = csum[0] ^ 0xFF;

    int i = 0;
    for (i = 0; i < 10; i++)
    {
        savegame[0x118 + i] = csum[i];
    }

    unsigned char csum2[10];
    calcChecksum(savegame, csum2);

    if ((csum[0] != csum2[0]) && (countBits(csum[0]) == countBits(csum2[0])))
    {
        savegame[0x118] = csum2[0];
    }

    return;
}

// Fixes the checksums on all 15 savegames
// data should be at least 162180 bytes long
void fixChecksums(unsigned char* data)
{
    int i = 0;
    for (i = 0; i < 15; i++)
    {
        addChecksum(data + i*0x2a3c);
    }
}

// Looks for a particular module in the list of currently loaded modules, 
// and returns its UID
static u32 FindProc(const char* szMod, const char* szLib, u32 nid) 
{
    // This function based on PSPLink's code
    SceModule* modP = sceKernelFindModuleByName(szMod); 
    struct SceLibraryEntryTable* entry;
    void* entTab;
    int entLen;

    if (modP == NULL) 
    { 
        printf("Failed to find mod '%s'\n", szMod); 
        return 0; 
    } 

    entTab = modP->ent_top;
    entLen = modP->ent_size;

    int i = 0;

    while(i < entLen)
    {
        int count;
        int total;
        unsigned int *vars;

        entry = (struct SceLibraryEntryTable*)(entTab + i);


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

// Hooks sceKernelStartModule so we can listen for when sceUtility is started
void set_startModulehook()
{
   sceKernelStartModule_ptr = (void *)FindProc("sceModuleManager", "ModuleMgrForUser", 0x50F0C1EC);
   sceKernelStartModule_j = ((u32*)sceKernelStartModule_ptr)[0];
   sceKernelStartModule_bd = ((u32*)sceKernelStartModule_ptr)[1];
   REDIRECT_FUNCTION(FindProc("sceModuleManager", "ModuleMgrForUser", 0x50F0C1EC), sceKernelStartModule_patched);
   ClearCaches();
}

// Hooks sceUtilitySavedata* functions so we can inject/extract our decrypted data
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

    printf("sceUtilitySavedata* hooks added\n");

    return 0;
}

// Overload for sceKernelStartModule
// If sceUtility gets started, it will add hooks to the savedata functions
// and release the hook on sceKernelStartModule
int sceKernelStartModule_patched(SceUID modid, SceSize argsize, void* argp, int* status, SceKernelSMOption* options)
{
    int k1 = pspSdkSetK1(0);
    static int success = 0;

    SceModule* loadedModule = sceKernelFindModuleByUID(modid);
    if ((success == 0) && strstr(loadedModule->modname, "sceUtility"))
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

// Writes data of length size to path
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

// Reads data of length size from path
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

// Create a directory on the filesystem
int makeDecryptedDirectory(const char* dir)
{
    int d = sceIoDopen(PATH("decryptedSaves"));
    if (d >= 0)
    {
        sceIoDclose(d);
    }
    else
    {
        sceIoMkdir(PATH("decryptedSaves"), 0777);
    }

    char buf[256];
    sprintf(buf, PATH("decryptedSaves/%s"), dir);

    d = sceIoDopen(buf);
    if (d >= 0)
    {
        sceIoDclose(d);
    }
    else
    {
        sceIoMkdir(buf, 0777);
    }

    return 0;
}

// When the game tries to save, grab its data and save it somewhere else on the memory stick
int saveDecryptedSavedata(SceUtilitySavedataParam* params)
{
    printf("saveDecryptedSavedata\n");

    char path[256] = { '\0' };
    sprintf(path, "%s%s", params->gameName, params->saveName);
    makeDecryptedDirectory(path);

    sprintf(path, PATH("decryptedSaves/%s%s/%s"), params->gameName, params->saveName, params->fileName);
    writeDataToFile(path, params->dataBuf, params->dataBufSize);

    if (params->icon0FileData.buf)
    {
        sprintf(path, PATH("decryptedSaves/%s%s/ICON0.PNG"), params->gameName, params->saveName);
        writeDataToFile(path, params->icon0FileData.buf, params->icon0FileData.bufSize);
    }
    if (params->icon1FileData.buf)
    {
        sprintf(path, PATH("decryptedSaves/%s%s/ICON1.PNG"), params->gameName, params->saveName);
        writeDataToFile(path, params->icon1FileData.buf, params->icon1FileData.bufSize);
    }
    if (params->pic1FileData.buf)
    {
        sprintf(path, PATH("decryptedSaves/%s%s/PIC1.PNG"), params->gameName, params->saveName);
        writeDataToFile(path, params->pic1FileData.buf, params->pic1FileData.bufSize);
    }
    if (params->snd0FileData.buf)
    {
        sprintf(path, PATH("decryptedSaves/%s%s/SND0.AC3"), params->gameName, params->saveName);
        writeDataToFile(path, params->snd0FileData.buf, params->snd0FileData.bufSize);
    }
    
    sprintf(path, PATH("decryptedSaves/%s%s/PARAM"), params->gameName, params->saveName);
    writeDataToFile(path, &(params->sfoParam), sizeof(params->sfoParam));

    return 0;
}



// When a game tries to load a save, inject our own data if available
int loadDecryptedSavedata(SceUtilitySavedataParam* params)
{
    char path[256] = { '\0' };
    sprintf(path, "%s%s", params->gameName, params->saveName);

    makeDecryptedDirectory(path);

    sprintf(path, PATH("decryptedSaves/%s%s/%s"), params->gameName, params->saveName, params->fileName);
    if (readDataFromFile(path, params->dataBuf, params->dataBufSize) < 0)
    {
        return FILE_NOT_FOUND;
    }

    fixChecksums(params->dataBuf);

    // I don't think this stuff needs to be done
    if (params->icon0FileData.buf)
    {
        sprintf(path, PATH("decryptedSaves/%s%s/ICON0.PNG"), params->gameName, params->saveName);
        readDataFromFile(path, params->icon0FileData.buf, params->icon0FileData.bufSize);
    }
    if (params->icon1FileData.buf)
    {
        sprintf(path, PATH("decryptedSaves/%s%s/ICON1.PNG"), params->gameName, params->saveName);
        readDataFromFile(path, params->icon1FileData.buf, params->icon1FileData.bufSize);
    }
    if (params->pic1FileData.buf)
    {
        sprintf(path, PATH("decryptedSaves/%s%s/PIC1.PNG"), params->gameName, params->saveName);
        readDataFromFile(path, params->pic1FileData.buf, params->pic1FileData.bufSize);
    }
    if (params->snd0FileData.buf)
    {
        sprintf(path, PATH("decryptedSaves/%s%s/SND0.AC3"), params->gameName, params->saveName);
        readDataFromFile(path, params->snd0FileData.buf, params->snd0FileData.bufSize);
    }

    sprintf(path, PATH("/%s%s/PARAM"), params->gameName, params->saveName);
    readDataFromFile(path, &(params->sfoParam), sizeof(params->sfoParam));

    return 0;
}

// Overload for sceUtilitySavedataInitStart
// If the game running is not one we're interested in (US/PAL/JAP versions of FFT)
// it removes the hooks to the savedata functions 
int sceUtilitySavedataInitStart_patched(SceUtilitySavedataParam* params)
{
    int badgame = 0;
    int k1 = pspSdkSetK1(0);

    currentParams = params;

    if ((strstr(params->gameName, "ULUS10297") >= 0) ||
        (strstr(params->gameName, "ULES00850") >= 0) ||
        (strstr(params->gameName, "ULJM05194") >= 0))
    {
        if (params->mode == 1)
        {
            // Grab the data being saved and put it on memory stick
            saveDecryptedSavedata(params);
        }
	}
    else
    {
        badgame = 1;
    }

    ((u32*)sceUtilitySavedataInitStart_ptr)[0] = sceUtilitySavedataInitStart_j;
    ((u32*)sceUtilitySavedataInitStart_ptr)[1] = sceUtilitySavedataInitStart_bd;
    ClearCaches();

    pspSdkSetK1(k1);

    int ret = sceUtilitySavedataInitStart_ptr(params);

    if (badgame)
    {
        // Not interested in this game
        // remove hooks
        ((u32*)sceUtilitySavedataGetStatus_ptr)[0] = sceUtilitySavedataGetStatus_j;
        ((u32*)sceUtilitySavedataGetStatus_ptr)[1] = sceUtilitySavedataGetStatus_bd;
        ((u32*)sceUtilitySavedataShutdownStart_ptr)[0] = sceUtilitySavedataShutdownStart_j;
        ((u32*)sceUtilitySavedataShutdownStart_ptr)[1] = sceUtilitySavedataShutdownStart_bd;
    }
    else
    {
        REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x50C4CD57), sceUtilitySavedataInitStart_patched);
    }

    
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
        int loadStatus = loadDecryptedSavedata(currentParams);
        if (loadStatus == FILE_NOT_FOUND)
        {
            // Get the encrypted data that was read from the memory stick and save it
            // with the decrypted data
            saveDecryptedSavedata(currentParams);
        }

        pspSdkSetK1(k1);
    }

    REDIRECT_FUNCTION(FindProc("sceUtility_Driver", "sceUtility", 0x8874DBE0), sceUtilitySavedataGetStatus_patched);
    ClearCaches();

    return ret;
}

// Main thread
// Tries to hook savedata functions, if fails, assumes they haven't been loaded yet
// and hooks sceKernelStartModule to try again when they are loaded
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
    printf("Starting\n");
    int main_thid = sceKernelCreateThread("myhook", threadMain, 6, 0x04000, 0, NULL);

    if(main_thid >= 0) sceKernelStartThread(main_thid, args, argp);

    return 0;

    sceKernelExitDeleteThread(0); // unreachable?
}

int module_stop(SceSize args, void *argp)
{
    return 0;
}

