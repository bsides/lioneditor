/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

#include "stdafx.h"
#include "memmem.h"
#include <stdlib.h>
#include <assert.h>
#include <set>

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

BOOL GetPositionOfMaxSubArray(unsigned char* haystack, int haystackLength, unsigned char* needle, int needleLength, 
                              int* location, int* size)
{
    if (needleLength >= 4)
    {
        for (int i = MIN(needleLength,haystackLength); i >= 4; i--)
        {
            unsigned char* loc = (unsigned char*)memmem(haystack, haystackLength, needle, i);
            if (loc != NULL)
            {
                *size = i;
                *location = haystackLength - (loc-haystack);
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

typedef void (__stdcall *callback_t)(int);

__declspec(dllexport) void CompressWithCallbackExcept(unsigned char* bytes, int inputLength, unsigned char* output, int* outputLength, int* exceptions, int numberOfExceptions, callback_t progressCallback)
{
	std::set<int> exceptionsSet;

	for (int i = 0; i < numberOfExceptions; i++) exceptionsSet.insert(exceptions[i]);

    BuildCompressionJumps();

    int outputPosition = 0;
    int loc = 0;
    int size = 0;
    int currentEntry = 0;

    for (int i = 0; i < inputLength; i++)
    {
        if (bytes[i] == 0xFE)
        {
            if (progressCallback != NULL)
            {
                progressCallback(i*100/inputLength);
            }

			currentEntry++;

			if (exceptionsSet.find(currentEntry) != exceptionsSet.end())
			{
				output[outputPosition++] = bytes[i]; // Copy the 0xFE

				while (((i+1) < inputLength) && (bytes[i+1] != 0xFE))
				{
					output[outputPosition++] = bytes[++i];
				}
			}
			else
			{
				output[outputPosition++] = bytes[i];
			}

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

__declspec(dllexport) void CompressWithCallback(unsigned char* bytes, int inputLength, unsigned char* output, int* outputLength, callback_t progressCallback)
{
	CompressWithCallbackExcept(bytes, inputLength, output, outputLength, NULL, 0, progressCallback);
}

__declspec(dllexport) void Compress(unsigned char* bytes, int inputLength, unsigned char* output, int* outputLength)
{
    CompressWithCallback(bytes, inputLength, output, outputLength, NULL);
}

__declspec(dllexport) void CompressExcept(unsigned char* bytes, int inputLength, unsigned char* output, int* outputLength, int* exceptions, int numberOfExceptions)
{
	CompressWithCallbackExcept(bytes, inputLength, output, outputLength, exceptions, numberOfExceptions, NULL);
}

#ifdef _MANAGED
#pragma managed(pop)
#endif

