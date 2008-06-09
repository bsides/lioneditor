/*  Pcsx - Pc Psx Emulator
 *  Copyright (C) 1999-2003  Pcsx Team
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

#include "PsxCommon.h"
#include "agemo\agemodebug.h"

// Dma0/1 in Mdec.c
// Dma3   in CdRom.c

//in psxhw.c
extern int _agemo_gpu_upload_x, _agemo_gpu_upload_y, _agemo_gpu_upload_w, _agemo_gpu_upload_h;
extern int agemo_flag_gpu_upload, agemo_flag_gpu_chain;
extern int agemo_flag_spu_upload;

void psxDma4(u32 madr, u32 bcr, u32 chcr) { // SPU
	u16 *ptr;
	u32 size;

	switch (chcr) {
		case 0x01000201: //cpu to spu transfer
#ifdef PSXDMA_LOG
			PSXDMA_LOG("*** DMA4 SPU - mem2spu *** %lx addr = %lx size = %lx\n", chcr, madr, bcr);
#endif
			ptr = (u16 *)PSXM(madr);
			if (ptr == NULL) {
#ifdef CPU_LOG
				CPU_LOG("*** DMA4 SPU - mem2spu *** NULL Pointer!!!\n");
#endif
				break;
			}
			SPU_writeDMAMem(ptr, (bcr >> 16) * (bcr & 0xffff) * 2);
			if(agemo_flag_spu_upload)
			{
				AgemoTrace("SPU Upload from $%X , $%X ", madr, (bcr >> 16) * (bcr & 0xffff) * 2);
				__agemo_pause_cpu(1);
			}
			break;

		case 0x01000200: //spu to cpu transfer
#ifdef PSXDMA_LOG
			PSXDMA_LOG("*** DMA4 SPU - spu2mem *** %lx addr = %lx size = %lx\n", chcr, madr, bcr);
#endif
			ptr = (u16 *)PSXM(madr);
			if (ptr == NULL) {
#ifdef CPU_LOG
				CPU_LOG("*** DMA4 SPU - spu2mem *** NULL Pointer!!!\n");
#endif
				break;
			}
			size = (bcr >> 16) * (bcr & 0xffff) * 2;
    		SPU_readDMAMem(ptr, size);
			psxCpu->Clear(madr, size);
			break;

#ifdef PSXDMA_LOG
		default:
			PSXDMA_LOG("*** DMA4 SPU - unknown *** %lx addr = %lx size = %lx\n", chcr, madr, bcr);
			break;
#endif
	}

	HW_DMA4_CHCR &= ~0x01000000;
	DMA_INTERRUPT(4);
}

void psxDma2(u32 madr, u32 bcr, u32 chcr) { // GPU
	u32 *ptr;
	u32 size;

	switch(chcr) {
		case 0x01000200: // vram2mem
#ifdef PSXDMA_LOG
			PSXDMA_LOG("*** DMA2 GPU - vram2mem *** %lx addr = %lx size = %lx\n", chcr, madr, bcr);
#endif
			ptr = (u32 *)PSXM(madr);
			if (ptr == NULL) {
#ifdef CPU_LOG
				CPU_LOG("*** DMA2 GPU - vram2mem *** NULL Pointer!!!\n");
#endif
				break;
			}
			size = (bcr >> 16) * (bcr & 0xffff);
			GPU_readDataMem(ptr, size);
			psxCpu->Clear(madr, size);
			break;

		case 0x01000201: // mem2vram
#ifdef PSXDMA_LOG
			PSXDMA_LOG("*** DMA 2 - GPU mem2vram *** %lx addr = %lx size = %lx\n", chcr, madr, bcr);
#endif

			ptr = (u32 *)PSXM(madr);
			if (ptr == NULL) {
#ifdef CPU_LOG
				CPU_LOG("*** DMA2 GPU - mem2vram *** NULL Pointer!!!\n");
#endif
				break;
			}

			size = (bcr >> 16) * (bcr & 0xffff);
			GPU_writeDataMem(ptr, size);
			GPUDMA_INT((size / 4) / BIAS);

#ifdef PSXGPU_UPLOAD_LOG
			PSXGPU_UPLOAD_LOG("mem2vram chcr=%lx madr = %lx bcr = %lx\n", chcr, madr, bcr);
#endif

			//by Agemo 
			//AgemoTrace("%X , %X bytes", madr, size);
			//PSXDMA_LOG("%X , %X bytes\n", madr, size);
			if(agemo_flag_gpu_upload)
			{
				AgemoTrace("GPU Upload from $%X , $%X bytes", madr, size*4);
				AgemoTrace("area X/Y(%d, %d) W/H(%d,%d)", _agemo_gpu_upload_x, _agemo_gpu_upload_y, _agemo_gpu_upload_w, _agemo_gpu_upload_h);
				__agemo_pause_cpu(1);
			}

			return;
//			break;

		case 0x01000401: // dma chain
#ifdef PSXDMA_LOG
			PSXDMA_LOG("*** DMA 2 - GPU dma chain *** %lx addr = %lx size = %lx\n", chcr, madr, bcr);
#endif

#ifdef PSXGPU_UPLOAD_LOG	//by Agemo 
			//PSXGPU_UPLOAD_LOG("mem2vram chcr=%lx madr = %lx bcr = %lx\n", chcr, madr, bcr);
			//__agemo_pause_cpu(1);
#endif
			GPU_dmaChain((u32 *)psxM, madr & 0x1fffff);
			if (agemo_flag_gpu_chain)
			{
				__agemo_pause_cpu(1);
				AgemoTrace("dma chain madr = %lx", madr);
				Agemo_OnGpuDmaChain((u32 *)psxM, madr & 0x1fffff);
			}
			
			break;

#ifdef PSXDMA_LOG
		default:
			PSXDMA_LOG("*** DMA 2 - GPU unknown *** %lx addr = %lx size = %lx\n", chcr, madr, bcr);
			break;
#endif
	}

	HW_DMA2_CHCR &= ~0x01000000;
	DMA_INTERRUPT(2);
}

void gpuInterrupt() {
	HW_DMA2_CHCR &= ~0x01000000;
	DMA_INTERRUPT(2);
}

void psxDma6(u32 madr, u32 bcr, u32 chcr) {
	u32 *mem = (u32 *)PSXM(madr);

#ifdef PSXDMA_LOG
	PSXDMA_LOG("*** DMA6 OT *** %lx addr = %lx size = %lx\n", chcr, madr, bcr);
#endif

	if (chcr == 0x11000002) {
		if (mem == NULL) {
#ifdef CPU_LOG
			CPU_LOG("*** DMA6 OT *** NULL Pointer!!!\n");
#endif
			HW_DMA6_CHCR &= ~0x01000000;
			DMA_INTERRUPT(6);
			return;
		}

		while (bcr--) {
			*mem-- = (madr - 4) & 0xffffff;
			madr -= 4;
		}
		mem++; *mem = 0xffffff;
	}
#ifdef PSXDMA_LOG
	else {
		// Unknown option
		PSXDMA_LOG("*** DMA6 OT - unknown *** %lx addr = %lx size = %lx\n", chcr, madr, bcr);
	}
#endif

	HW_DMA6_CHCR &= ~0x01000000;
	DMA_INTERRUPT(6);
}

