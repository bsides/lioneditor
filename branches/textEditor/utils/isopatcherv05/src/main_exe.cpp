#include <stdio.h>
#include <stdlib.h>
#include <io.h>
#include <fcntl.h>
#define WIN32_LEAN_AND_MEAN
#include <windows.h>

#include "ecc_edc.h"
#include "tools.h"
#include "iso_patch.h"
#include "iso9660.h"


void main(int argc, char *argv[])
{
	int nRet;
	int iso_type = ISO9660_M2F1;
	int need_ecc_edc = 1;

	MyTrace("-----------------------------------------------------------------------------\n");
	MyTrace("iso patcher v0.5 - open source project 2002-2005, by xade, agemo.\n");
	MyTrace("-----------------------------------------------------------------------------\n");

	if (argc != 3 && argc != 4 && argc != 5 )
	{
		MyTrace("usage:\n");
		MyTrace("  %s list_file iso_file [/mode] [/e]\n\n", argv[0]);
		MyTrace("  /mode : iso mode, optional params.\n");
		MyTrace("          /M1   = mode1\n");
		MyTrace("          /M2F1 = mode2 form1 (default) (playstation)\n");
		MyTrace("          /M2F2 = mode2 form2\n");
		MyTrace("  /e: calculating ecc and edc OFF. optional params. (default ON)\n");
		MyTrace("\n");
		MyTrace("  all params is case insensitive.\n");
		return ;
	}

	if(argc >= 4)
	{
		if(lstrcmpi(argv[3], "/M1"  ) == 0)iso_type = ISO9660_M1;
		if(lstrcmpi(argv[3], "/M2F1") == 0)iso_type = ISO9660_M2F1;
		if(lstrcmpi(argv[3], "/M2F2") == 0)iso_type = ISO9660_M2F2;
	}

	if(argc >= 5)
	{
		if(lstrcmpi(argv[3], "/E"  ) == 0)need_ecc_edc = 0;
	}

	nRet = iso_patch_list(iso_type, argv[2], need_ecc_edc, argv[1]);
	if(nRet >= 0)
	{
		MyTrace("total patched %d bytes\n", nRet);
	}

}
