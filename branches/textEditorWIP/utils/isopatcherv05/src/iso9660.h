#ifndef _ISO9660_H
#define _ISO9660_H

#define ISO9660_M1			0		//Mode 1
#define ISO9660_M2F1		1		//Mode 2 Form 2
#define ISO9660_M2F2		2		//Mode 2 Form 2

static const int ISO9660_DATA_START[] = {0x10, 0x18, 0x18};	//不同mode的数据部分在扇区开始偏移
static const int ISO9660_DATA_SIZE[]  = {2048, 2048, 2324};	//不同mode的数据部分在扇区的长度

#endif