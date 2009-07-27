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
#include "CompressionJumps.h"

#define MIN(x,y) (((x)<(y))?(x):(y))
#define MAX(x,y) (((x)>(y))?(x):(y))
#define MIN_NEEDLE_LENGTH 4

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
    if (needleLength >= MIN_NEEDLE_LENGTH)
    {
        for (int i = MIN(needleLength,haystackLength); i >= MIN_NEEDLE_LENGTH; i--)
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

__declspec(dllexport) void CompressSection(unsigned char* input, int inputLength, unsigned char* output, int* outputPosition)
{
    int loc = 0;
    int size = 0;

    for (int i = 0; i < inputLength; i++)
    {
        if (input[i] == 0xFE)
        {
			output[*outputPosition] = input[i];
			(*outputPosition) += 1;
            continue;
        }

        int fe = FindByte(0xFE, input, i, i+MIN(35, inputLength-i)-1);
        if (fe == -1) fe = i + 35;

        if (GetPositionOfMaxSubArray(
               output+MAX(0,*outputPosition-3792),
               MIN(*outputPosition, 3792),
               input+i,
               fe - i,
               &loc,
               &size))
        {
            AddJump(output+*outputPosition, loc, size);
            (*outputPosition) += 3;
            i += size -1;
        }
        else
        {
			output[*outputPosition] = input[i];
			(*outputPosition) += 1;
            continue;
        }
    }
}

#ifdef _MANAGED
#pragma managed(pop)
#endif

