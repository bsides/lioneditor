#define J_OPCODE	0x08000000
#define NOP	0x00000000
#define REDIRECT_FUNCTION(a, f) _sw(J_OPCODE | (((u32)(f) >> 2)  & 0x03ffffff), a);  _sw(NOP, a+4);

// hooked function, function pointer, and saved instructions for generic_sce_function_name
int generic_sce_function_name_Patched(SceUtilitySavedataParam* params); // name of your patched function
int (* generic_sce_function_name_k)(SceUtilitySavedataParam* params); // function pointer with the same data type/args
u32 generic_sce_function_name_j; // 'jump' call used to resolve the NID for the function
u32 generic_sce_function_name_bd; // code from the jump's branch delay slot


static u32 FindProc(const char* szMod, const char* szLib, u32 nid) 
{ 
    SceModule* modP = sceKernelFindModuleByName(szMod); 
    if (modP == NULL) 
    { 
        printf("Failed to find mod '%s'\n", szMod); 
        return 0; 
    } 
    SceLibraryEntryTable* entP = (SceLibraryEntryTable*)modP->ent_top; 
    while ((u32)entP < ((u32)modP->ent_top + modP->ent_size)) 
    { 
        if (entP->libname != NULL && strcmp(entP->libname, szLib) == 0) 
        { 
            // found lib 
            int i; 
            int count = entP->stubcount + entP->vstubcount; 
            u32* nidtable = (u32*)entP->entrytable; 
            for (i = 0; i < count; i++) 
            { 
                if (nidtable[i] == nid) 
                { 
                    u32 procAddr = nidtable[count+i]; 
                    // printf("entry found: '%s' '%s' = $%x\n", szMod, szLib, (int)procAddr); 
                    return procAddr; 
                } 
            } 
            printf("Found mod '%s' and lib '%s' but not nid=$%x\n", szMod, szLib, nid); 
            return 0; 
        } 
        entP++; 
    } 
    printf("Found mod '%s' but not lib '%s'\n", szMod, szLib); 
    return 0; 
}

void ClearCaches()
{
	sceKernelIcacheInvalidateAll();
	sceKernelDcacheWritebackInvalidateAll();
}

void set_hook()
{
   generic_sce_function_name_k = (void *)FindProc(......);
   generic_sce_function_name_j = ((u32*)generic_sce_function_name_k)[0];
   generic_sce_function_name_bd = ((u32*)generic_sce_function_name_k)[1];
   REDIRECT_FUNCTION(FindProc(......), generic_sce_function_name_Patched);
   ClearCaches();
}

int generic_sce_function_name_Patched(SceUtilitySavedataParam* params)
{
   // do stuff
......
   // un-set the hook by returning the original jump/branch code to the NID resolution address
   ((u32*)generic_sce_function_name_k)[0] = generic_sce_function_name_j;
   ((u32*)generic_sce_function_name_k)[1] = generic_sce_function_name_bd;
   ClearCaches();
   // get the return value
   <function type> ret = generic_sce_function_name_k(<function args>);
   // re-hook the function
   REDIRECT_FUNCTION(FindProc(......), generic_sce_function_name_Patched);
   ClearCaches();
   return ret;
}