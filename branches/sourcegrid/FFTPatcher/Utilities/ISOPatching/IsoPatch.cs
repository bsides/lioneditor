/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.
    ISO patching/ECC/EDC generation lifted from Agemo's isopatcherv05

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
using System.Text;
using System.IO;

namespace FFTPatcher
{
    static class IsoPatch
    {
        public enum IsoType
        {
            Mode1 = 0,
            Mode2Form1 = 1,
            Mode2Form2 = 2
        }

        private static readonly int[] DataStart = new int[3] { 0x10, 0x18, 0x18 };
        private static readonly int[] DataSize = new int[3] { 2048, 2048, 2324 };
        private static readonly int[] SectorSize = new int[3] { 2048, 2352, 2352 };
        private static byte[] eccFLUT = new byte[256];
        private static byte[] eccBLUT = new byte[256];
        private static ulong[] edcLUT = new ulong[256];

        static IsoPatch()
        {
            uint j = 0;
            ulong edc = 0;
            for( uint i = 0; i < 256; i++ )
            {
                j = (uint)((i << 1) ^ ((i & 0x80) == 0x80 ? 0x11D : 0));
                eccFLUT[i] = (byte)j;
                eccBLUT[i ^ j] = (byte)i;
                edc = i;
                for( j = 0; j < 8; j++ )
                {
                    edc = (edc >> 1) ^ ((edc & 1) == 1 ? 0xD8018001 : 0);
                }
                edcLUT[i] = edc;
            }
        }

        #region ECC/EDC

        private static void ComputeEdcBlock( IList<byte> source, int size, IList<byte> destination )
        {
            ulong edc = 0;
            for( int i = 0; i < size; i++ )
            {
                edc = (edc >> 8) ^ edcLUT[(edc ^ source[i]) & 0xFF];
            }
            destination[0] = (byte)((edc >> 0) & 0xFF);
            destination[1] = (byte)((edc >> 8) & 0xFF);
            destination[2] = (byte)((edc >> 16) & 0xFF);
            destination[3] = (byte)((edc >> 24) & 0xFF);
        }

        private static void ComputeEccBlock( IList<byte> source, uint majorCount, uint minorCount, uint majorMult, uint minorIncrement, IList<byte> destination )
        {
            ulong size = majorCount * minorCount;
            uint major = 0;
            uint minor = 0;
            for( major = 0; major < majorCount; major++ )
            {
                ulong i = (major >> 1) * majorMult + (major & 1);
                byte eccA = 0;
                byte eccB = 0;
                for( minor = 0; minor < minorCount; minor++ )
                {
                    byte t = source[(int)i];
                    i += minorIncrement;
                    if( i >= size ) i -= size;
                    eccA ^= t;
                    eccB ^= t;
                    eccA = eccFLUT[eccA];
                }

                eccA = eccBLUT[eccFLUT[eccA] ^ eccB];
                destination[(int)major] = eccA;
                destination[(int)(major + majorCount)] = (byte)(eccA ^ eccB);
            }
        }

        private static void GenerateEcc( IList<byte> sector, bool zeroAddress )
        {
            byte[] address = new byte[4];
            byte i = 0;
            if( zeroAddress )
            {
                for( i = 0; i < 4; i++ )
                {
                    address[i] = sector[12 + i];
                    sector[12 + i] = 0;
                }
            }

            ComputeEccBlock( sector.Sub( 0x0C ), 86, 24, 2, 86, sector.Sub( 0x81C ) );
            ComputeEccBlock( sector.Sub( 0x0C ), 52, 43, 86, 88, sector.Sub( 0x8C8 ) );
            if( zeroAddress )
            {
                for( i = 0; i < 4; i++ )
                {
                    sector[12 + i] = address[i];
                }
            }
        }

        private static void GenerateEccEdc( IList<byte> sector, IsoType isoType )
        {
            switch( isoType )
            {
                case IsoType.Mode1:
                    ComputeEdcBlock( sector, 0x810, sector.Sub( 0x810 ) );
                    for( int i = 0; i < 8; i++ )
                    {
                        sector[0x814 + i] = 0;
                    }
                    GenerateEcc( sector, false );
                    break;
                case IsoType.Mode2Form1:
                    ComputeEdcBlock( sector.Sub( 0x10 ), 0x808, sector.Sub( 0x818 ) );
                    GenerateEcc( sector, true );
                    break;
                case IsoType.Mode2Form2:
                    ComputeEdcBlock( sector.Sub( 0x10 ), 0x91C, sector.Sub( 0x92C ) );
                    break;
                default:
                    throw new ArgumentException( "isotype" );
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Patches the file at a given sector in an ISO.
        /// </summary>
        /// <param name="isoType">The type of ISO</param>
        /// <param name="isoFile">Path to the ISO image</param>
        /// <param name="patchEccEdc">Whether or not ECC/EDC blocks should be updated</param>
        /// <param name="sectorNumber">The sector number where the file begins</param>
        /// <param name="input">Bytes to write</param>
        public static void PatchFileAtSector( IsoType isoType, string isoFile, bool patchEccEdc, int sectorNumber, IList<byte> input )
        {
            PatchFileAtSector( isoType, isoFile, patchEccEdc, sectorNumber, 0, input );
        }

        /// <summary>
        /// Patches the file at a given sector in an ISO.
        /// </summary>
        /// <param name="isoType">The type of ISO</param>
        /// <param name="isoFile">Path to the ISO image</param>
        /// <param name="patchEccEdc">Whether or not ECC/EDC blocks should be updated</param>
        /// <param name="sectorNumber">The sector number where the file begins</param>
        /// <param name="offset">Where in the file to start writing</param>
        /// <param name="input">Bytes to write</param>
        public static void PatchFileAtSector( IsoType isoType, string isoFile, bool patchEccEdc, int sectorNumber, int offset, IList<byte> input )
        {
            int dataSize = DataSize[(int)isoType];
            int dataStart = DataStart[(int)isoType];
            int sectorSize = SectorSize[(int)isoType];

            int sectorsToAdvance = offset / dataSize;
            int newOffset = offset % dataSize;

            int realOffset = (sectorNumber + sectorsToAdvance) * sectorSize + dataStart + newOffset;

            PatchFile( isoType, isoFile, patchEccEdc, realOffset, input );
        }

        /// <summary>
        /// Patches the bytes at a given offset.
        /// </summary>
        /// <param name="isoType">The type of ISO</param>
        /// <param name="isoFile">Path to the ISO image</param>
        /// <param name="patchEccEdc">Whether or not ECC/EDC blocks should be updated</param>
        /// <param name="offset">Where in the ISO to start writing</param>
        /// <param name="input">Bytes to write</param>
        public static void PatchFile( IsoType isoType, string isoFile, bool patchEccEdc, int offset, IList<byte> input )
        {
            using( FileStream stream = new FileStream( isoFile, FileMode.Open ) )
            using( MemoryStream inputStream = new MemoryStream( input.ToArray() ) )
            {
                PatchFile( isoType, stream, patchEccEdc, offset, inputStream );
            }
        }

        /// <summary>
        /// Patches the bytes at a given offset.
        /// </summary>
        /// <param name="isoType">The type of ISO</param>
        /// <param name="iso">Stream that contains the ISO</param>
        /// <param name="patchEccEdc">Whether or not ECC/EDC blocks should be updated</param>
        /// <param name="offset">Where in the ISO to start writing</param>
        /// <param name="input">Stream that contains the bytes to write</param>
        public static void PatchFile( IsoType isoType, Stream iso, bool patchEccEdc, int offset, Stream input )
        {
            byte[] sector = new byte[2352];
            int type = (int)isoType;

            int sectorStart = offset % 2352;
            if( sectorStart < DataStart[type] ||
                sectorStart >= (DataStart[type] + DataSize[type]) )
            {
                throw new ArgumentException( "start offset is incorrect", "offset" );
            }

            int sectorLength = DataSize[type] + DataStart[type] - sectorStart;
            int totalPatchedBytes = 0;
            int sizeRead = 0;
            int temp = offset - (offset % 2352);
            iso.Seek( temp, SeekOrigin.Begin );
            
            input.Seek( 0, SeekOrigin.Begin );
            while( input.Position < input.Length )
            {
                iso.Read( sector, 0, 2352 );
                sizeRead = input.Read( sector, sectorStart, sectorLength );
                if( (sector[0x12] & 8) == 0 )
                {
                    sector[0x12] = 8;
                    sector[0x16] = 8;
                }

                if( patchEccEdc )
                {
                    GenerateEccEdc( sector, isoType );
                }

                iso.Seek( -2352, SeekOrigin.Current );
                iso.Write( sector, 0, 2352 );
                totalPatchedBytes += sizeRead;
                sectorStart = DataStart[type];
                sectorLength = DataSize[type];
            }
        }

        #endregion
    }
}
