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

using System;
using System.IO;

namespace FFTPatcher
{
    public static class PspIso
    {
        private const int bufferSize = 1024;
        private static byte[] buffer = new byte[1024];
        private static byte[] jpSizes = new byte[] { 0xE4, 0xD9, 0x37, 0x00, 0x00, 0x37, 0xD9, 0xE4 };
        private static byte[] euSizes = new byte[] { 0xA4, 0x84, 0x3A, 0x00, 0x00, 0x3A, 0x84, 0xA4 };

        public static bool IsUS( FileStream stream )
        {
            return CheckFile( stream, "ULUS-10297", "ULUS10297", new long[] { 0x8373, 0xE000 }, new long[] { 0x2C18128, 0x101EC3A8, 0x10232530 } );
        }

        public static bool IsEU( FileStream stream )
        {
            return CheckFile( stream, "ULES-00850", "ULES00850", new long[] { 0x8373, 0xE000 }, new long[] { 0x2C18128, 0x101EC3A8, 0x10232530 } );
        }

        public static bool IsJP( FileStream stream )
        {
            return CheckFile( stream, "ULJM-05194", "ULJM05194", new long[] { 0x8373, 0xE000 }, new long[] { 0x2BF0128, 0xFD619FC, 0xFD97A5C } );
        }

        private static bool CheckFile( FileStream stream, string str1, string str2, long[] loc1, long[] loc2 )
        {
            byte[] str1bytes = str1.ToByteArray();
            foreach( long l in loc1 )
            {
                stream.Seek( l, SeekOrigin.Begin );
                stream.Read( buffer, 0, str1.Length );
                for( int i = 0; i < str1bytes.Length; i++ )
                {
                    if( buffer[i] != str1bytes[i] )
                        return false;
                }
            }

            byte[] str2bytes = str2.ToByteArray();
            foreach( long l in loc2 )
            {
                stream.Seek( l, SeekOrigin.Begin );
                stream.Read( buffer, 0, str2.Length );
                for( int i = 0; i < str2bytes.Length; i++ )
                {
                    if( buffer[i] != str2bytes[i] )
                        return false;
                }
            }

            return true;
        }

        public static void PatchISO( FileStream stream )
        {
            
        }

        public static void DecryptISO( FileStream stream )
        {
            if( IsJP( stream ) )
            {
                CopyBytes( stream, 0xFA68000, 0x37D9E4, 0x10000, 0x37DB40 );
                stream.Seek( 0xC0A2, SeekOrigin.Begin );
                stream.Write( jpSizes, 0, 8 );
            }
            else if( IsUS( stream ) || IsEU( stream ) )
            {
                CopyBytes( stream, 0xFED8000, 0x3A84A4, 0x10000, 0x3A8600 );
                stream.Seek( 0xC0A2, SeekOrigin.Begin );
                stream.Write( euSizes, 0, 8 );
            }
            else
            {
                throw new NotSupportedException( "Unrecognized file." );
            }
        }

        private static void CopyBytes( FileStream stream, long src, long srcSize, long dest, long destOldSize )
        {
            long bytesRead = 0;
            while( (bytesRead + bufferSize) < srcSize )
            {
                stream.Seek( src + bytesRead, SeekOrigin.Begin );
                stream.Read( buffer, 0, bufferSize );
                stream.Seek( dest + bytesRead, SeekOrigin.Begin );
                stream.Write( buffer, 0, bufferSize );
                bytesRead += bufferSize;
            }

            stream.Seek( src + bytesRead, SeekOrigin.Begin );
            stream.Read( buffer, 0, (int)(srcSize - bytesRead) );
            stream.Seek( dest + bytesRead, SeekOrigin.Begin );
            stream.Write( buffer, 0, (int)(srcSize - bytesRead) );

            if( destOldSize > srcSize )
            {
                buffer = new byte[bufferSize];
                stream.Seek( dest + srcSize, SeekOrigin.Begin );
                stream.Write( buffer, 0, (int)(destOldSize - srcSize) );
            }
        }
    }
}
