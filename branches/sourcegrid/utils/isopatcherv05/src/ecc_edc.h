#ifndef _ECC_EDC_H
#define _ECC_EDC_H

/*******************************************************************
  eccedc_generate()
  recalculates ecc & edc fields in *ONE* iso sector.
  for more information, pls refer to ISO9660 standards.
  params:
    pSector  -  sector buffer(2352 bytes)
    iso_type -  in iso9660 type, see iso9660.h
********************************************************************/
void eccedc_generate(BYTE *pSector, int iso_type);


/*******************************************************************
  eccedc_init()
  init static data(LUTs) for calculating edc/ecc.
  must be called *once* before eccedc_generate()
  and once is enough.
********************************************************************/
void eccedc_init(void);
#endif 
