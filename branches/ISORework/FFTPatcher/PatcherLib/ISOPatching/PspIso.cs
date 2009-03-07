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
using System.Collections.Generic;
using System.IO;
using PatcherLib.Datatypes;
using PatcherLib.Utilities;

namespace PatcherLib.Iso
{
    public static class PspIso
    {
        public class PspIsoInfo
        {
            private delegate void MyFunc( string path, Sectors sector );
            public IDictionary<Sectors, long> FileToSectorMap { get; private set; }
            private PspIsoInfo() { }
            ImageMaster.IsoReader reader;

            public static PspIsoInfo GetPspIsoInfo( Stream iso )
            {
                return GetPspIsoInfo( new ImageMaster.IsoReader( iso ) );
            }

            private static PspIsoInfo GetPspIsoInfo( ImageMaster.IsoReader reader )
            {
                if ( !reader.Open() )
                {
                    throw new IOException( "couldn't open ISO" );
                }
                ImageMaster.ImageRecord record = null;
                Dictionary<Sectors, long> myDict = new Dictionary<Sectors, long>();
                MyFunc func =
                    delegate( string path, Sectors sector )
                    {
                        record = reader.GetItemPath( path );
                        if ( record != null )
                        {
                            myDict[sector] = record.Location;
                        }
                        else
                        {
                            throw new FileNotFoundException( "couldn't find file in ISO", path );
                        }
                    };
                func( "PSP_GAME/ICON0.PNG", Sectors.PSP_GAME_ICON0_PNG );
                func( "PSP_GAME/PARAM.SFO", Sectors.PSP_GAME_PARAM_SFO );
                func( "PSP_GAME/PIC0.PNG", Sectors.PSP_GAME_PIC0_PNG );
                func( "PSP_GAME/PIC1.PNG", Sectors.PSP_GAME_PIC1_PNG );
                func( "PSP_GAME/SYSDIR/BOOT.BIN", Sectors.PSP_GAME_SYSDIR_BOOT_BIN );
                func( "PSP_GAME/SYSDIR/EBOOT.BIN", Sectors.PSP_GAME_SYSDIR_EBOOT_BIN );
                func( "PSP_GAME/SYSDIR/UPDATE/DATA.BIN", Sectors.PSP_GAME_SYSDIR_UPDATE_DATA_BIN );
                func( "PSP_GAME/SYSDIR/UPDATE/EBOOT.BIN", Sectors.PSP_GAME_SYSDIR_UPDATE_EBOOT_BIN );
                func( "PSP_GAME/SYSDIR/UPDATE/PARAM.SFO", Sectors.PSP_GAME_SYSDIR_UPDATE_PARAM_SFO );
                func( "PSP_GAME/USRDIR/fftpack.bin", Sectors.PSP_GAME_USRDIR_fftpack_bin );
                func( "PSP_GAME/USRDIR/movie/001_HolyStone.pmf", Sectors.PSP_GAME_USRDIR_movie_001_HolyStone_pmf );
                func( "PSP_GAME/USRDIR/movie/002_Opening.pmf", Sectors.PSP_GAME_USRDIR_movie_002_Opening_pmf );
                func( "PSP_GAME/USRDIR/movie/003_Abduction.pmf", Sectors.PSP_GAME_USRDIR_movie_003_Abduction_pmf );
                func( "PSP_GAME/USRDIR/movie/004_Kusabue.pmf", Sectors.PSP_GAME_USRDIR_movie_004_Kusabue_pmf );
                func( "PSP_GAME/USRDIR/movie/005_Get_away.pmf", Sectors.PSP_GAME_USRDIR_movie_005_Get_away_pmf );
                func( "PSP_GAME/USRDIR/movie/006_Reassume_Dilita.pmf", Sectors.PSP_GAME_USRDIR_movie_006_Reassume_Dilita_pmf );
                func( "PSP_GAME/USRDIR/movie/007_Dilita_Advice.pmf", Sectors.PSP_GAME_USRDIR_movie_007_Dilita_Advice_pmf );
                func( "PSP_GAME/USRDIR/movie/008_Ovelia_and_Dilita.pmf", Sectors.PSP_GAME_USRDIR_movie_008_Ovelia_and_Dilita_pmf );
                func( "PSP_GAME/USRDIR/movie/009_Dilita_Musing.pmf", Sectors.PSP_GAME_USRDIR_movie_009_Dilita_Musing_pmf );
                func( "PSP_GAME/USRDIR/movie/010_Ending.pmf", Sectors.PSP_GAME_USRDIR_movie_010_Ending_pmf );
                func( "PSP_GAME/USRDIR/movie/011_Russo.pmf", Sectors.PSP_GAME_USRDIR_movie_011_Russo_pmf );
                func( "PSP_GAME/USRDIR/movie/012_Valuhurea.pmf", Sectors.PSP_GAME_USRDIR_movie_012_Valuhurea_pmf );
                func( "PSP_GAME/USRDIR/movie/013_StaffRoll.pmf", Sectors.PSP_GAME_USRDIR_movie_013_StaffRoll_pmf );
                func( "UMD_DATA.BIN", Sectors.UMD_DATA_BIN );
                PspIsoInfo result = new PspIsoInfo();
                result.FileToSectorMap = new ReadOnlyDictionary<Sectors, long>( myDict );
                result.reader = reader;
                return result;
            }
            

            public static PspIsoInfo GetPspIsoInfo( string iso )
            {
                ImageMaster.IsoReader reader = null;
                try
                {
                    reader = new ImageMaster.IsoReader( iso );
                    var result = GetPspIsoInfo( reader );
                    result.reader = null;
                    return result;
                }
                finally
                {
                    if ( reader != null )
                    {
                        reader.Close();
                    }
                }
            }
        }
        

		#region Instance Variables (6) 

        private static readonly long[] bootBinLocations = { 0x10000, 0x0FED8000 };
        private static byte[] buffer = new byte[1024];
        private const int bufferSize = 1024;
        private static byte[] euSizes = new byte[] { 0xA4, 0x84, 0x3A, 0x00, 0x00, 0x3A, 0x84, 0xA4 };
        //public const long FFTPackLocation = 0x02C20000;
        private static byte[] jpSizes = new byte[] { 0xE4, 0xD9, 0x37, 0x00, 0x00, 0x37, 0xD9, 0xE4 };

		#endregion Instance Variables 

		#region Public Methods (10) 

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
                PspIsoInfo info = PspIsoInfo.GetPspIsoInfo( stream );
                DecryptISO( stream, info );
            }
            catch ( NotSupportedException )
            {
                throw;
            }
            finally
            {
                if ( stream != null )
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
        public static void DecryptISO( Stream stream, PspIsoInfo info )
        {
            if ( IsJP( stream, info ) )
            {
                CopyBytes( stream, info.FileToSectorMap[Sectors.PSP_GAME_SYSDIR_BOOT_BIN] * 2048, 0x37D9E4, info.FileToSectorMap[Sectors.PSP_GAME_SYSDIR_EBOOT_BIN] * 2048, 0x37DB40 );
            }
            else if ( IsUS( stream, info ) || IsEU( stream, info ) )
            {
                CopyBytes( stream, info.FileToSectorMap[Sectors.PSP_GAME_SYSDIR_BOOT_BIN] * 2048, 0x3A84A4, info.FileToSectorMap[Sectors.PSP_GAME_SYSDIR_EBOOT_BIN] * 2048, 0x3A8600 );
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
        public static bool IsEU( Stream stream, PspIsoInfo info )
        {
            return
                CheckString( stream, info.FileToSectorMap[Sectors.UMD_DATA_BIN] * 2048 + 0, "ULES-00850" ) &&
                CheckString( stream, info.FileToSectorMap[Sectors.PSP_GAME_PARAM_SFO] * 2048 + 0x128, "ULES00850" ) &&
                CheckString( stream, info.FileToSectorMap[Sectors.PSP_GAME_SYSDIR_BOOT_BIN] * 2048 + 0x3143A8, "ULES00850" ) &&
                CheckString( stream, info.FileToSectorMap[Sectors.PSP_GAME_SYSDIR_BOOT_BIN] * 2048 + 0x35A530, "ULES00850" );
        }

        /// <summary>
        /// Determines whether the specified stream is JP.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// 	<c>true</c> if the specified stream is JP; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsJP( Stream stream, PspIsoInfo info )
        {
            //return CheckFile( stream, "ULJM-05194", "ULJM05194", new long[] { 0x8373, 0xE000 }, new long[] { 0x2BF0128, 0xFD619FC, 0xFD97A5C } );
            return
                CheckString( stream, info.FileToSectorMap[Sectors.UMD_DATA_BIN] * 2048 + 0, "ULJM-05194" );
        }

        /// <summary>
        /// Determines whether the specified stream is US.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// 	<c>true</c> if the specified stream is US; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUS( Stream stream, PspIsoInfo info )
        {
            return
                CheckString( stream, info.FileToSectorMap[Sectors.UMD_DATA_BIN] * 2048 + 0, "ULUS-10297" ) &&
                CheckString( stream, info.FileToSectorMap[Sectors.PSP_GAME_PARAM_SFO] * 2048 + 0x128, "ULUS10297" ) &&
                CheckString( stream, info.FileToSectorMap[Sectors.PSP_GAME_SYSDIR_BOOT_BIN] * 2048 + 0x3143A8, "ULUS10297" ) &&
                CheckString( stream, info.FileToSectorMap[Sectors.PSP_GAME_SYSDIR_BOOT_BIN] * 2048 + 0x35A530, "ULUS10297" );
        }

        private static bool CheckString( Stream stream, long loc, string expectedString )
        {
            stream.Seek( loc, SeekOrigin.Begin );
            byte[] buffer = new byte[expectedString.Length];
            stream.Read( buffer, 0, buffer.Length );
            return buffer.ToUTF8String() == expectedString;
        }

        public static void PatchISO( Stream file, IEnumerable<PatcherLib.Datatypes.PatchedByteArray> patches )
        {
            PspIsoInfo info = PspIsoInfo.GetPspIsoInfo( file );
            DecryptISO( file, info );
            patches.ForEach( p => ApplyPatch( file, info, p ) );
        }

        /// <summary>
        /// Updates the BOOT.BIN file in a War of the Lions ISO image.
        /// </summary>
        /// <param name="stream">The stream that represents a War of the Lions ISO image.</param>
        /// <param name="location">The location in BOOT.BIN to update.</param>
        /// <param name="bytes">The bytes to update BOOT.BIN with.</param>
        //public static void UpdateBootBin( Stream stream, long location, byte[] bytes )
        //{
        //    DecryptISO( stream );

        //    foreach( long loc in bootBinLocations )
        //    {
        //        stream.WriteArrayToPosition( bytes, loc + location );
        //    }
        //}

        /// <summary>
        /// Updates the specified fftpack.bin file with new data.
        /// </summary>
        /// <param name="stream">The stream that represents a War of the Lions ISO image.</param>
        /// <param name="index">The index of the file in fftpack.bin to update.</param>
        /// <param name="bytes">The bytes to update the file with.</param>
        //public static void UpdateFFTPack( Stream stream, int index, byte[] bytes )
        //{
        //    FFTPack.PatchFile( stream, index, 0, bytes );
        //}

		#endregion Public Methods 

		#region Private Methods (3) 

        public static void ApplyPatch( Stream stream, PspIsoInfo info, PatcherLib.Datatypes.PatchedByteArray patch )
        {
            if ( patch.SectorEnum != null )
            {
                if ( patch.SectorEnum.GetType() == typeof( PspIso.Sectors ) )
                {
                    stream.WriteArrayToPosition( patch.Bytes, (int)( info.FileToSectorMap[(PspIso.Sectors)patch.SectorEnum] * 2048 ) + patch.Offset );
                }
                else if ( patch.SectorEnum.GetType() == typeof( FFTPack.Files ) )
                {
                    FFTPack.PatchFile( stream, info, (int)( (FFTPack.Files)patch.SectorEnum ), (int)patch.Offset, patch.Bytes );
                }
                else
                {
                    throw new ArgumentException( "invalid type" );
                }
            }
        }

        //private static bool CheckFile( Stream stream, string str1, string str2, long[] loc1, long[] loc2 )
        //{
        //    byte[] str1bytes = str1.ToByteArray();
        //    foreach( long l in loc1 )
        //    {
        //        stream.Seek( l, SeekOrigin.Begin );
        //        stream.Read( buffer, 0, str1.Length );
        //        for( int i = 0; i < str1bytes.Length; i++ )
        //        {
        //            if( buffer[i] != str1bytes[i] )
        //                return false;
        //        }
        //    }

        //    byte[] str2bytes = str2.ToByteArray();
        //    foreach( long l in loc2 )
        //    {
        //        stream.Seek( l, SeekOrigin.Begin );
        //        stream.Read( buffer, 0, str2.Length );
        //        for( int i = 0; i < str2bytes.Length; i++ )
        //        {
        //            if( buffer[i] != str2bytes[i] )
        //                return false;
        //        }
        //    }

        //    return true;
        //}

        public static IList<byte> GetFile( Stream stream, PspIsoInfo info, PspIso.Sectors sector, int start, int length )
        {
            byte[] result = new byte[length];
            stream.Seek( info.FileToSectorMap[sector] * 2048 + start, SeekOrigin.Begin );
            stream.Read( result, 0, length );
            return result;
        }

        public static IList<byte> GetFile( Stream stream, PspIsoInfo info, FFTPack.Files file, int start, int length )
        {
            byte[] result = FFTPack.GetFileFromIso( stream, info, file );
            return result.Sub( start, start + length - 1 );
        }

        private static void CopyBytes( Stream stream, long src, long srcSize, long dest, long destOldSize )
        {
            long bytesRead = 0;
            while ( ( bytesRead + bufferSize ) < srcSize )
            {
                stream.Seek( src + bytesRead, SeekOrigin.Begin );
                stream.Read( buffer, 0, bufferSize );
                stream.Seek( dest + bytesRead, SeekOrigin.Begin );
                stream.Write( buffer, 0, bufferSize );
                bytesRead += bufferSize;
            }

            stream.Seek( src + bytesRead, SeekOrigin.Begin );
            stream.Read( buffer, 0, (int)( srcSize - bytesRead ) );
            stream.Seek( dest + bytesRead, SeekOrigin.Begin );
            stream.Write( buffer, 0, (int)( srcSize - bytesRead ) );

            if ( destOldSize > srcSize )
            {
                buffer = new byte[bufferSize];
                stream.Seek( dest + srcSize, SeekOrigin.Begin );
                stream.Write( buffer, 0, (int)( destOldSize - srcSize ) );
            }
        }

		#endregion Private Methods 

        public enum Sectors
        {
            PSP_GAME_ICON0_PNG = 22560,
            PSP_GAME_PARAM_SFO = 22576,
            PSP_GAME_PIC0_PNG = 22416,
            PSP_GAME_PIC1_PNG = 22432,
            PSP_GAME_SYSDIR_BOOT_BIN = 130480,
            PSP_GAME_SYSDIR_EBOOT_BIN = 32,
            PSP_GAME_SYSDIR_UPDATE_DATA_BIN = 6032,
            PSP_GAME_SYSDIR_UPDATE_EBOOT_BIN = 1936,
            PSP_GAME_SYSDIR_UPDATE_PARAM_SFO = 1920,
            PSP_GAME_USRDIR_fftpack_bin = 22592,
            PSP_GAME_USRDIR_movie_001_HolyStone_pmf = 132368,
            PSP_GAME_USRDIR_movie_002_Opening_pmf = 190832,
            PSP_GAME_USRDIR_movie_003_Abduction_pmf = 198112,
            PSP_GAME_USRDIR_movie_004_Kusabue_pmf = 135360,
            PSP_GAME_USRDIR_movie_005_Get_away_pmf = 140288,
            PSP_GAME_USRDIR_movie_006_Reassume_Dilita_pmf = 144352,
            PSP_GAME_USRDIR_movie_007_Dilita_Advice_pmf = 150224,
            PSP_GAME_USRDIR_movie_008_Ovelia_and_Dilita_pmf = 156000,
            PSP_GAME_USRDIR_movie_009_Dilita_Musing_pmf = 166192,
            PSP_GAME_USRDIR_movie_010_Ending_pmf = 179264,
            PSP_GAME_USRDIR_movie_011_Russo_pmf = 183360,
            PSP_GAME_USRDIR_movie_012_Valuhurea_pmf = 186304,
            PSP_GAME_USRDIR_movie_013_StaffRoll_pmf = 202128,
            UMD_DATA_BIN = 28,
        }
    }
}
