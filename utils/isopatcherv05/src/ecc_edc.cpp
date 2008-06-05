/* LUTs used for computing ECC/EDC */
#include <windows.h>
#include "ecc_edc.h"
#include "iso9660.h"

static BYTE ecc_f_lut[256];
static BYTE ecc_b_lut[256];
static DWORD edc_lut[256];

/* Init routine */
void eccedc_init(void)
{
	DWORD i, j, edc;
	for(i = 0; i < 256; i++)
	{
		j = (i << 1) ^ (i & 0x80 ? 0x11D : 0);
		ecc_f_lut[i] = (BYTE)j;
		ecc_b_lut[i ^ j] = (BYTE)i;
		edc = i;
		for(j = 0; j < 8; j++) edc = (edc >> 1) ^ (edc & 1 ? 0xD8018001 : 0);
		edc_lut[i] = edc;
	}
}
/***************************************************************************/
// Compute EDC for a block
void edc_computeblock( BYTE *src, WORD size, BYTE *dest )
{
	DWORD edc=0x00000000;
	while(size--) edc = (edc >> 8) ^ edc_lut[(edc ^ (*src++)) & 0xFF];
	dest[0] = (BYTE)((edc >>  0) & 0xFF);
	dest[1] = (BYTE)((edc >>  8) & 0xFF);
	dest[2] = (BYTE)((edc >> 16) & 0xFF);
	dest[3] = (BYTE)((edc >> 24) & 0xFF);
}

/***************************************************************************/
// Compute ECC for a block (can do either P or Q)
static void ecc_computeblock( BYTE *src, DWORD major_count, DWORD minor_count, DWORD major_mult, DWORD minor_inc, BYTE *dest)
{
	DWORD size = major_count * minor_count;
	DWORD major, minor;
	for(major = 0; major < major_count; major++)
	{
		DWORD index = (major >> 1) * major_mult + (major & 1);
		BYTE ecc_a = 0;
		BYTE ecc_b = 0;
		for(minor = 0; minor < minor_count; minor++)
		{
			BYTE temp = src[index];
			index += minor_inc;
			if(index >= size) index -= size;
			ecc_a ^= temp;
			ecc_b ^= temp;
			ecc_a = ecc_f_lut[ecc_a];
		}
		ecc_a = ecc_b_lut[ecc_f_lut[ecc_a] ^ ecc_b];
		dest[major              ] = ecc_a;
		dest[major + major_count] = ecc_a ^ ecc_b;
	}
}

// Generate ECC P and Q codes for a block
static void ecc_generate( BYTE *pSector, int zeroaddress)
{
	BYTE address[4], i;
	/* Save the address and zero it out */
	if(zeroaddress) for(i = 0; i < 4; i++)
	{
		address[i] = pSector[12 + i];
		pSector[12 + i] = 0;
	}
	/* Compute ECC P code */
	ecc_computeblock(pSector + 0xC, 86, 24,  2, 86, pSector + 0x81C);
	/* Compute ECC Q code */
	ecc_computeblock(pSector + 0xC, 52, 43, 86, 88, pSector + 0x8C8);
	/* Restore the address */
	if(zeroaddress) for(i = 0; i < 4; i++) pSector[12 + i] = address[i];
}

/***************************************************************************/
// Generate ECC/EDC information for a sector (must be 2352 = 0x930 bytes) 
void eccedc_generate( BYTE *pSector, int iso_type)
{
	DWORD i;
	switch(iso_type) {
	case ISO9660_M1: /* Mode 1 */
		edc_computeblock(pSector + 0x00, 0x810, pSector + 0x810);
		/* Write out zero bytes - unused field*/
		for(i = 0; i < 8; i++) pSector[0x814 + i] = 0;
		ecc_generate(pSector, 0);
		break;
	case ISO9660_M2F1: /* Mode 2 form 1 */
		edc_computeblock(pSector + 0x10, 0x808, pSector + 0x818);
		ecc_generate(pSector, 1);
		break;
	case ISO9660_M2F2: /* Mode 2 form 2 */
		edc_computeblock(pSector + 0x10, 0x91C, pSector + 0x92C);
		break;
  }
}
