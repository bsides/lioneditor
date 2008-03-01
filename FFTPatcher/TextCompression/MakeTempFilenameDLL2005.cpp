// MakeTempFilenameDLL2005.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#define MIN(x,y) (((x)<(y))?(x):(y))
#define MAX(x,y) (((x)>(y))?(x):(y))

#ifdef _MANAGED
#pragma managed(push, off)
#endif

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
    return TRUE;
}

int CompressionJumps[3793];

void BuildCompressionJumps(void)
{
	if ((CompressionJumps[3792]==3820) && (CompressionJumps[2634]==2654)) return;
	for (int i = 0; i <= 3820; i++)
	{
		int x = i - (i/256) * 2;
		CompressionJumps[x] = i;
	}
}

TCHAR szReturn[MAX_PATH];

int FindByte(unsigned char byteToFind, unsigned char* whereToLook, int whereToStart, int whereToEnd)
{
	for (int i = whereToStart; i <= whereToEnd; i++)
	{
		if (whereToLook[i] == byteToFind) return i;
	}

	return -1;
}

BOOL PointersHaveSameData(unsigned char* one, unsigned char* two, int length)
{
	for (int i = 0; i < length; i++)
	{
		if (one[i] != two[i]) return FALSE;
	}

	return TRUE;
}

int FindSubArray(unsigned char* input, int inputLength, unsigned char* whatToFind, int subLength)
{
	for (int i = 0; i <= (inputLength-subLength); i++)
	{
		if (PointersHaveSameData(input+i, whatToFind, subLength))
		{
			return i;
		}
	}

	return -1;
}


BOOL GetPositionOfMaxSubArray(unsigned char* whereToLook, int whereToStopLooking, unsigned char* whatToFind, int inputLength, 
							  int* location, int* size)
{
    if (inputLength >= 4)
	{
		for (int i = inputLength; i >= 4; i--)
		{
			int loc = FindSubArray(whereToLook, whereToStopLooking, whatToFind, i);
			if (loc > -1)
			{
				*size = i;
				*location = whereToStopLooking - loc;
				return TRUE;
			}
		}
	}

	return FALSE;
}

void AddJump(unsigned char* destination, int jump, int length)
{
	int l = length - 4;
	int j = CompressionJumps[jump];

    destination[0] = 0xF0 | ((l&0x18) >> 3);
	destination[1] = (l&0x07) << 5;
	destination[1] |= (j&0x1F00) >> 8;
	destination[2] = j&0xFF;
}

__declspec(dllexport) void Compress(unsigned char* bytes, int inputLength, unsigned char* output, int* outputLength)
{
	BuildCompressionJumps();

    int outputPosition = 0;
	int loc = 0;
    int size = 0;
	for (int i = 0; i < inputLength; i++)
	{
		if (bytes[i] == 0xFE)
		{
			output[outputPosition++] = bytes[i];
			continue;
		}

		int fe = FindByte(0xFE, bytes, i, i+MIN(35, inputLength-i)-1);
		if (fe == -1) fe = i + 35;

		if (GetPositionOfMaxSubArray(
		   output+MAX(0,outputPosition-3792),
		   MIN(outputPosition, 3792),
		   bytes+i,
		   fe - i,
		   &loc,
		   &size))
		{
			AddJump(output+outputPosition, loc, size);
			outputPosition += 3;
			i += size -1;
		}
		else
		{
			output[outputPosition++] = bytes[i];
		}
	}

	*outputLength = outputPosition;
}

#ifdef _MANAGED
#pragma managed(pop)
#endif

