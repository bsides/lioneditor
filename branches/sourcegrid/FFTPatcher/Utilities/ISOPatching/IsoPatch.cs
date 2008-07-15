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
using System.IO;
using System.Diagnostics;

namespace FFTPatcher
{
    public static class IsoPatch
    {
        public struct NewOldValue
        {
            public byte OldValue;
            public byte NewValue;

            public NewOldValue( byte newValue, byte oldValue )
            {
                OldValue = oldValue;
                NewValue = newValue;
            }
        }

		#region Static Fields (6) 

        private static readonly int[] dataSizes = new int[3] { 2048, 2048, 2324 };
        private static readonly int[] dataStarts = new int[3] { 0, 0x18, 0x18 };
        private static byte[] eccBLUT = new byte[256];
        private static byte[] eccFLUT = new byte[256];
        private static ulong[] edcLUT = new ulong[256];
        private static readonly int[] sectorSizes = new int[3] { 2048, fullSectorSize, fullSectorSize };

		#endregion Static Fields 

		#region Fields (1) 

        private const int fullSectorSize = 2352;

		#endregion Fields 

        public enum IsoType
        {
            Mode1 = 0,
            Mode2Form1 = 1,
            Mode2Form2 = 2
        }

		#region Constructors (1) 

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

		#endregion Constructors 

		#region Methods (11) 


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

        public static IList<byte> GeneratePpf( IDictionary<long, NewOldValue> dict )
        {
            IList<byte> result = InitializePpf();
            List<long> offsets = new List<long>( dict.Keys );
            offsets.Sort();

            List<IList<long>> offsetGroups = new List<IList<long>>();
            int start = 0;
            for ( int i = 0; i < offsets.Count - 1; i++ )
            {
                if ( ( offsets[i] != offsets[i + 1] + 1 ) || ( offsets[i] - start >= 255 ) )
                {
                    offsetGroups.Add( offsets.Sub( start, i ) );
                    start = i + 1;
                }
            }
            offsetGroups.Add( offsets.Sub( start, offsets.Count - 1 ) );
            foreach ( IList<long> group in offsetGroups )
            {
                Debug.Assert( group.Count <= 255 );
                result.AddRange( group[0].ToBytes() );
                result.Add( (byte)group.Count );
                group.ForEach( l => result.Add( dict[l].NewValue ) );
                group.ForEach( l => result.Add( dict[l].OldValue ) );
            }

            return result;
        }

        private static IList<byte> InitializePpf()
        {
            const string description = "FFTPatch generated file                           ";
            const string magicString = "PPF30";
            List<byte> result = new List<byte>();
            result.AddRange( magicString.ToByteArray() );
            result.Add( 0x02 ); // PPF 3.0
            result.AddRange( description.ToByteArray() );
            result.Add( 0x00 ); // binary file
            result.Add( 0x00 ); // disable blockcheck
            result.Add( 0x01 ); // enable undo data
            //result.Add( 0x00 ); // disable undo data
            result.Add( 0x01 ); // dummy
            return result;
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
                case IsoType.Mode2Form1:
                    ComputeEdcBlock( sector.Sub( 0x10 ), 0x808, sector.Sub( 0x818 ) );
                    GenerateEcc( sector, true );
                    break;
                case IsoType.Mode2Form2:
                    ComputeEdcBlock( sector.Sub( 0x10 ), 0x91C, sector.Sub( 0x92C ) );
                    break;
                case IsoType.Mode1:
                default:
                    throw new ArgumentException( "isotype" );
            }
        }

        public static void GeneratePpf( byte[] originalSector, byte[] sector, long offset, IDictionary<long, NewOldValue> ppfDictionary )
        {
            int sectorLength = sector.Length;
            for ( int i = 0; i < sectorLength; i++ )
            {
                if ( ppfDictionary.ContainsKey( offset + i ) )
                {
                    if ( sector[i] != ppfDictionary[offset + i].OldValue )
                    {
                        NewOldValue nov = ppfDictionary[offset + i];
                        nov.NewValue = sector[i];
                        ppfDictionary[offset + i] = nov;
                    }
                    else
                    {
                        ppfDictionary.Remove( offset + i );
                    }
                }
                else if ( sector[i] != originalSector[i] )
                {
                    ppfDictionary[offset + i] = new NewOldValue( sector[i], originalSector[i] );
                }
            }
        }

        /// <summary>
        /// Patches the bytes at a given offset.
        /// </summary>
        /// <param name="isoType">The type of ISO</param>
        /// <param name="isoFile">Path to the ISO image</param>
        /// <param name="patchEccEdc">Whether or not ECC/EDC blocks should be updated</param>
        /// <param name="offset">Where in the ISO to start writing</param>
        /// <param name="input">Bytes to write</param>
        public static void PatchFile( IsoType isoType, string isoFile, bool patchEccEdc, long offset, IList<byte> input, bool patchIso, bool generatePpf, IDictionary<long,NewOldValue> patch )
        {
            using( FileStream stream = new FileStream( isoFile, FileMode.Open ) )
            {
                PatchFile( isoType, stream, patchEccEdc, offset, input, patchIso, generatePpf, patch );
            }
        }

        /// <summary>
        /// Patches the bytes at a given offset.
        /// </summary>
        /// <param name="isoType">The type of ISO</param>
        /// <param name="iso">Stream that contains the ISO</param>
        /// <param name="patchEccEdc">Whether or not ECC/EDC blocks should be updated</param>
        /// <param name="offset">Where in the ISO to start writing</param>
        /// <param name="input">Bytes to write</param>
        public static void PatchFile( IsoType isoType, Stream iso, bool patchEccEdc, long offset, IList<byte> input, bool patchIso, bool generatePpf, IDictionary<long,NewOldValue> patch )
        {
            using ( MemoryStream inputStream = new MemoryStream( input.ToArray() ) )
            {
                PatchFile( isoType, iso, patchEccEdc, offset, inputStream, patchIso, generatePpf, patch );
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
        public static void PatchFile( IsoType isoType, Stream iso, bool patchEccEdc, long offset, Stream input, bool patchIso, bool generatePpf, IDictionary<long, NewOldValue> ppfDictionary )
        {
            int type = (int)isoType;
            int sectorSize = sectorSizes[type];
            byte[] sector = new byte[sectorSize];

            long sectorStart = offset % sectorSize;
            if( sectorStart < dataStarts[type] ||
                sectorStart >= (dataStarts[type] + dataSizes[type]) )
            {
                throw new ArgumentException( "start offset is incorrect", "offset" );
            }
            if( patchEccEdc && isoType == IsoType.Mode1 )
            {
                throw new ArgumentException( "Mode1 does not support ECC/EDC", "patchEccEdc" );
            }

            long sectorLength = dataSizes[type] + dataStarts[type] - sectorStart;
            int totalPatchedBytes = 0;
            int sizeRead = 0;
            long temp = offset - (offset % sectorSize);
            iso.Seek( temp, SeekOrigin.Begin );

            input.Seek( 0, SeekOrigin.Begin );
            while( input.Position < input.Length )
            {
                iso.Read( sector, 0, sectorSize );
                byte[] originalSector = sector.ToArray();

                sizeRead = input.Read( sector, (int)sectorStart, (int)sectorLength );

                if( isoType != IsoType.Mode1 && (sector[0x12] & 8) == 0 )
                {
                    sector[0x12] = 8;
                    sector[0x16] = 8;
                }

                if( patchEccEdc )
                {
                    GenerateEccEdc( sector, isoType );
                }

                iso.Seek( -sectorSize, SeekOrigin.Current );

                if( generatePpf )
                {
                    GeneratePpf( originalSector, sector, iso.Position, ppfDictionary );
                }

                if( patchIso )
                {
                    iso.Write( sector, 0, sectorSize );
                }
                else
                {
                    iso.Seek( sectorSize, SeekOrigin.Current );
                }

                totalPatchedBytes += sizeRead;
                sectorStart = dataStarts[type];
                sectorLength = dataSizes[type];
            }
        }

        /// <summary>
        /// Patches the file at a given sector in an ISO.
        /// </summary>
        /// <param name="isoType">The type of ISO</param>
        /// <param name="isoFile">Path to the ISO image</param>
        /// <param name="patchEccEdc">Whether or not ECC/EDC blocks should be updated</param>
        /// <param name="sectorNumber">The sector number where the file begins</param>
        /// <param name="input">Bytes to write</param>
        public static void PatchFileAtSector( IsoType isoType, string isoFile, bool patchEccEdc, int sectorNumber, IList<byte> input, bool patchIso, bool generatePpf, IDictionary<long, NewOldValue> ppfDictionary )
        {
            PatchFileAtSector( isoType, isoFile, patchEccEdc, sectorNumber, 0, input, patchIso, generatePpf, ppfDictionary );
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
        public static void PatchFileAtSector( IsoType isoType, string isoFile, bool patchEccEdc, int sectorNumber, long offset, IList<byte> input, bool patchIso, bool generatePpf, IDictionary<long, NewOldValue> ppfDictionary )
        {
            using ( FileStream stream = new FileStream( isoFile, FileMode.Open ) )
            {
                PatchFileAtSector( isoType, stream, patchEccEdc, sectorNumber, offset, input, patchIso, generatePpf, ppfDictionary );
            }
        }

        /// <summary>
        /// Patches the file at a given sector in an ISO.
        /// </summary>
        /// <param name="isoType">The type of ISO</param>
        /// <param name="iso">Stream that contains the ISO</param>
        /// <param name="patchEccEdc">Whether or not ECC/EDC blocks should be updated</param>
        /// <param name="sectorNumber">The sector number where the file begins</param>
        /// <param name="offset">Where in the file to start writing</param>
        /// <param name="input">Bytes to write</param>
        public static void PatchFileAtSector( IsoType isoType, Stream iso, bool patchEccEdc, int sectorNumber, long offset, IList<byte> input, bool patchIso, bool generatePpf, IDictionary<long, NewOldValue> ppfDictionary )
        {
            int dataSize = dataSizes[(int)isoType];
            int dataStart = dataStarts[(int)isoType];
            int sectorSize = sectorSizes[(int)isoType];

            long sectorsToAdvance = offset / dataSize;
            long newOffset = offset % dataSize;

            long realOffset = (sectorNumber + sectorsToAdvance) * sectorSize + dataStart + newOffset;

            PatchFile( isoType, iso, patchEccEdc, realOffset, input, patchIso, generatePpf, ppfDictionary );
        }


		#endregion Methods 

    }
}
