TARGET = FFTSaveHook
OBJS = main.o 

BUILD_PRX=1
USE_KERNEL_LIBC=1
USE_KERNEL_LIBS=1
PSP_FW_VERSION=371

INCDIR = 
CFLAGS = -Os -G0 -Wall -fno-strict-aliasing -fno-builtin-printf -D_PSPSLIM
CXXFLAGS = $(CFLAGS) -fno-exceptions -fno-rtti
ASFLAGS = $(CFLAGS)

LIBDIR = 
LDFLAGS = -mno-crt0 -nostartfiles
LIBS = -lpspsdk -lpspkernel

PSPSDK=$(shell psp-config --pspsdk-path)
include $(PSPSDK)/lib/build_prx.mak
