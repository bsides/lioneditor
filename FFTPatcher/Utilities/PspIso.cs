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
using FFTPatcher.Datatypes;

namespace FFTPatcher
{
    public static class PspIso
    {

		#region Static Fields (3) 

        private static byte[] buffer = new byte[1024];
        private static byte[] euSizes = new byte[] { 0xA4, 0x84, 0x3A, 0x00, 0x00, 0x3A, 0x84, 0xA4 };
        private static byte[] jpSizes = new byte[] { 0xE4, 0xD9, 0x37, 0x00, 0x00, 0x37, 0xD9, 0xE4 };

		#endregion Static Fields 

		#region Fields (1) 

        private const int bufferSize = 1024;

		#endregion Fields 

		#region Methods (9) 


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

        /// <summary>
        /// Decrypts the ISO.
        /// </summary>
        /// <param name="filename">The filename of the ISO to decrypt.</param>
        public static void DecryptISO( string filename )
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream( filename, FileMode.Open );
                DecryptISO( stream );
            }
            catch( NotSupportedException )
            {
                throw;
            }
            finally
            {
                if( stream != null )
                {
                    stream.Flush();
                    stream.Close();
                    stream = null;
                }
            }
        }

        /// <summary>
        /// Decrypts the ISO.
        /// </summary>
        /// <param name="stream">The stream of the ISO to decrypt.</param>
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
                throw new NotSupportedException( "Unrecognized image." );
            }
        }

        /// <summary>
        /// Determines whether the specified stream is EU.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// 	<c>true</c> if the specified stream is EU; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEU( FileStream stream )
        {
            return CheckFile( stream, "ULES-00850", "ULES00850", new long[] { 0x8373, 0xE000 }, new long[] { 0x2C18128, 0x101EC3A8, 0x10232530 } );
        }

        /// <summary>
        /// Determines whether the specified stream is JP.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// 	<c>true</c> if the specified stream is JP; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsJP( FileStream stream )
        {
            return CheckFile( stream, "ULJM-05194", "ULJM05194", new long[] { 0x8373, 0xE000 }, new long[] { 0x2BF0128, 0xFD619FC, 0xFD97A5C } );
        }

        /// <summary>
        /// Determines whether the specified stream is US.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// 	<c>true</c> if the specified stream is US; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUS( FileStream stream )
        {
            return CheckFile( stream, "ULUS-10297", "ULUS10297", new long[] { 0x8373, 0xE000 }, new long[] { 0x2C18128, 0x101EC3A8, 0x10232530 } );
        }

        /// <summary>
        /// Patches the ISO.
        /// </summary>
        /// <param name="filename">The filename of the ISO to patch.</param>
        public static void PatchISO( string filename )
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream( filename, FileMode.Open );
                PatchISO( stream );
            }
            catch( NotSupportedException )
            {
                throw;
            }
            finally
            {
                if( stream != null )
                {
                    stream.Flush();
                    stream.Close();
                    stream = null;
                }
            }
        }

        /// <summary>
        /// Patches the ISO.
        /// </summary>
        /// <param name="stream">The stream of the ISO to patch.</param>
        public static void PatchISO( FileStream stream )
        {
            if( IsJP( stream ) )
            {
                throw new NotSupportedException( "Unrecognized image." );
            }

            DecryptISO( stream );

            stream.WriteArrayToPositions( FFTPatch.SkillSets.ToByteArray( Context.US_PSP ), 0x285A38, 0x1014DA38 );
            stream.WriteArrayToPositions( FFTPatch.MonsterSkills.ToByteArray( Context.US_PSP ), 0x286BB4, 0x2C7BC04, 0x1014EBB4 );

            stream.WriteArrayToPositions( FFTPatch.PoachProbabilities.ToByteArray( Context.US_PSP ), 0x287024, 0x2C7C0A4, 0x1014F024 );
            stream.WriteArrayToPositions( FFTPatch.Items.ToFirstByteArray(), 0x3352DC, 0x2C78EF8, 0x101FD2DC );
            stream.WriteArrayToPositions( FFTPatch.Items.ToSecondByteArray(), 0x266E00, 0x1012EE00 );
            stream.WriteArrayToPositions( FFTPatch.ItemAttributes.ToFirstByteArray(), 0x3366E8, 0x2C7A304, 0x101FE6E8 );
            stream.WriteArrayToPositions( FFTPatch.ItemAttributes.ToSecondByteArray(), 0x26720C, 0x1012F20C );

            stream.WriteArrayToPositions( FFTPatch.Abilities.ToByteArray( Context.US_PSP ), 0x281514, 0x10149514 );
            stream.WriteArrayToPositions( FFTPatch.Abilities.ToEffectsByteArray(), 0x3277B4, 0x101EF7B4 );

            stream.WriteArrayToPositions( FFTPatch.ActionMenus.ToByteArray( Context.US_PSP ), 0x286CA4, 0x2C7BCF4, 0x1014ECA4 );

            stream.WriteArrayToPositions( FFTPatch.Jobs.ToByteArray( Context.US_PSP ), 0x2839DC, 0x1014B9DC );
            stream.WriteArrayToPositions( FFTPatch.JobLevels.ToByteArray( Context.US_PSP ), 0x287084, 0x1014F084 );

            stream.WriteArrayToPositions( FFTPatch.InflictStatuses.ToByteArray(), 0x3363E8, 0x2C7A004, 0x101FE3E8 );
            stream.WriteArrayToPositions( FFTPatch.StatusAttributes.ToByteArray( Context.US_PSP ), 0x286DA4, 0x2C7BE24 );

            stream.WriteArrayToPositions( FFTPatch.Font.ToByteArray(), 0x28B80C, 0x3073B8, 0x1015380C, 0x101CF3B8 );
            stream.WriteArrayToPositions( FFTPatch.Font.ToWidthsByteArray(), 0x2A3F40, 0x31FAC0, 0x1016BF40, 0x101E7AC0 );

            stream.WriteArrayToPositions( FFTPatch.ENTDs.ENTDs[0].ToByteArray(), 0x44CA800 );
            stream.WriteArrayToPositions( FFTPatch.ENTDs.ENTDs[1].ToByteArray(), 0x44DE800 );
            stream.WriteArrayToPositions( FFTPatch.ENTDs.ENTDs[2].ToByteArray(), 0x44F2800 );
            stream.WriteArrayToPositions( FFTPatch.ENTDs.ENTDs[3].ToByteArray(), 0x4506800 );
            stream.WriteArrayToPositions( FFTPatch.ENTDs.PSPEventsToByteArray(), 0x7500800 );
        }


		#endregion Methods 

    }
}
