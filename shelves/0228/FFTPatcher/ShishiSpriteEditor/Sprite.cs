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
using System.Drawing;
using System.Drawing.Imaging;
using FFTPatcher.Datatypes;

namespace FFTPatcher.SpriteEditor
{
    public class Sprite
    {
        public Palette[] Palettes { get; set; }
        public byte[] Pixels { get; private set; }
        private long OriginalSize { get; set; }
        private bool Compressed { get; set; }

        public bool SP2 { get; private set; }
        public bool SPR { get; private set; }

        public void ImportBitmap( Bitmap bmp )
        {
            if( bmp.PixelFormat != PixelFormat.Format8bppIndexed )
            {
                throw new BadImageFormatException();
            }
            if( bmp.Width != 256 )
            {
                throw new BadImageFormatException();
            }

            Palettes = new Palette[16];
            for( int i = 0; i < 16; i++ )
            {
                Palettes[i] = new Palette( new SubArray<Color>( bmp.Palette.Entries, 16 * i, 16 * (i + 1) - 1 ) );
            }

            BitmapData bmd = bmp.LockBits( new Rectangle( 0, 0, bmp.Width, bmp.Height ), ImageLockMode.ReadWrite, bmp.PixelFormat );
            for( int i = 0; (i < Pixels.Length) && (i / 256 < bmp.Height); i++ )
            {
                Pixels[i] = (byte)bmp.GetPixel( bmd, i % 256, i / 256 );
            }

            bmp.UnlockBits( bmd );
        }

        private static Palette BuildGreyscalePalette()
        {
            Color[] colors = new Color[16];
            for( int i = 0; i < 16; i++ )
            {
                colors[i] = Color.FromArgb( (byte)(i << 4), (byte)(i << 4), (byte)(i << 4) );
            }

            return new Palette( colors );
        }

        private void FromSPR( IList<byte> bytes )
        {
            SPR = true;
            SP2 = false;
            Palettes = new Palette[16];
            for( int i = 0; i < 16; i++ )
            {
                Palettes[i] = new Palette( new SubArray<byte>( bytes, i * 32, (i + 1) * 32 - 1 ) );
            }

            if( bytes.Count < 0x9200 || ((bytes.Count > 0x9200) && (bytes[0x9200] == 0x00)) )
            {
                Pixels = BuildPixels( new SubArray<byte>( bytes, 16 * 32 ), new byte[0] );
                Compressed = false;
            }
            else
            {
                Pixels = BuildPixels( new SubArray<byte>( bytes, 16 * 32, 16 * 32 + 36864 - 1 ), new SubArray<byte>( bytes, 16 * 32 + 36864 ) );
                Compressed = true;
            }
        }

        private void FromSP2( IList<byte> bytes )
        {
            SP2 = true;
            SPR = false;
            Pixels = BuildPixels( bytes, new byte[0] );
            Compressed = false;
            Palettes = new Palette[16];
            for( int i = 0; i < 16; i++ )
                Palettes[i] = BuildGreyscalePalette();
        }

        public Sprite( IList<byte> bytes )
        {
            OriginalSize = bytes.Count;
            if( bytes.Count == 0x8000 )
            {
                FromSP2( bytes );
            }
            else
            {
                FromSPR( bytes );
            }
        }

        private static byte[] Recompress( IList<byte> bytes )
        {
            List<byte> realBytes = new List<byte>( bytes.Count );
            for( int i = 0; (i + 1) < bytes.Count; i += 2 )
            {
                realBytes.Add( bytes[i + 1] );
                realBytes.Add( bytes[i] );
            }

            List<byte> result = new List<byte>();
            int pos = 0;
            while( pos < realBytes.Count )
            {
                int z = NumberOfZeroes( new SubArray<byte>( realBytes, pos ) );
                z = Math.Min( z, 0xFFF );

                if( z == 0 )
                {
                    byte b = realBytes[pos];
                    result.Add( realBytes[pos] );
                    pos += 1;
                }
                else if( z < 16 )
                {
                    if( (z == 8) ||
                        (z == 7) )
                    {
                        result.Add( 0x00 );
                        result.Add( 0x00 );
                        result.Add( (byte)z );
                    }
                    else
                    {
                        result.Add( 0x00 );
                        result.Add( (byte)z );
                    }
                }
                else if( z < 256 )
                {
                    result.Add( 0x00 );
                    result.Add( 0x07 );
                    result.Add( ((byte)z).GetLowerNibble() );
                    result.Add( ((byte)z).GetUpperNibble() );
                }
                else if( z < 4096 )
                {
                    result.Add( 0x00 );
                    result.Add( 0x08 );
                    result.Add( ((byte)z).GetLowerNibble() );
                    result.Add( ((byte)z).GetUpperNibble() );
                    result.Add( (byte)((z & 0xF00) >> 8) );
                }

                pos += z;
            }

            return CompressNibbles( result );
        }

        private static byte[] CompressNibbles( IList<byte> bytes )
        {
            List<byte> result = new List<byte>( bytes.Count / 2 );
            for( int i = 0; i < bytes.Count; i += 2 )
            {
                if( (i + 1) < bytes.Count )
                {
                    result.Add( (byte)(((bytes[i] & 0x0F) << 4) + (bytes[i + 1] & 0x0F)) );
                }
                else
                {
                    result.Add( (byte)((bytes[i] & 0x0F) << 4) );
                }
            }
            return result.ToArray();
        }

        private static int NumberOfZeroes( IList<byte> bytes )
        {
            for( int i = 0; i < bytes.Count; i++ )
            {
                if( bytes[i] != 0 )
                    return i;
            }

            return bytes.Count;
        }

        private static byte[] Decompress( IList<byte> bytes )
        {
            byte[] compressed = new byte[bytes.Count * 2];
            for( int i = 0; i < bytes.Count; i++ )
            {
                compressed[i * 2] = bytes[i].GetUpperNibble();
                compressed[i * 2 + 1] = bytes[i].GetLowerNibble();
            }

            List<byte> result = new List<byte>();
            int j = 0;
            while( j < compressed.Length )
            {
                byte b = compressed[j];
                if( compressed[j] != 0 )
                {
                    result.Add( compressed[j] );
                }
                else if( (j + 1) < compressed.Length )
                {
                    byte s = compressed[j + 1];
                    int l = s;
                    if( (s == 7) && ((j + 3) < compressed.Length) )
                    {
                        l = compressed[j + 2] + (compressed[j + 3] << 4);
                        j += 2;
                    }
                    else if( (s == 8) && ((j + 4) < compressed.Length) )
                    {
                        l = compressed[j + 2] + (compressed[j + 3] << 4) + (compressed[j + 4] << 8);
                        j += 3;
                    }
                    else if( (s == 0) && ((j + 2) < compressed.Length) )
                    {
                        l = compressed[j + 2];
                        j++;
                    }
                    else
                    {
                        l = s;
                    }

                    j++;
                    for( int k = 0; k < l; k++ )
                        result.Add( 0x00 );
                }

                j++;
            }

            j = 0;
            while( (j + 1) < result.Count )
            {
                byte k = result[j];
                result[j] = result[j + 1];
                result[j + 1] = k;
                j += 2;
            }

            return result.ToArray();
        }

        private static byte[] BuildPixels( IList<byte> bytes, IList<byte> compressedBytes )
        {
            List<byte> result = new List<byte>( 36864 * 2 );
            foreach( byte b in bytes )
            {
                result.Add( b.GetLowerNibble() );
                result.Add( b.GetUpperNibble() );
            }

            if( compressedBytes.Count > 0 )
            {
                result.AddRange( Decompress( compressedBytes ) );
            }

            return result.ToArray();
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>();
            if( SPR && !SP2 )
            {
                foreach( Palette p in Palettes )
                {
                    result.AddRange( p.ToByteArray() );
                }
            }

            for(
                int i = 0;
                (Compressed && (i < 36864) && (2 * i + 1 < Pixels.Length)) ||
                (!Compressed && (2 * i + 1 < Pixels.Length));
                i++ )
            {
                result.Add( (byte)((Pixels[2 * i + 1] << 4) | (Pixels[2 * i] & 0x0F)) );
            }

            if( Pixels.Length > 2 * 36864 && Compressed )
            {
                result.AddRange( Recompress( new SubArray<byte>( Pixels, 2 * 36864 ) ) );
            }

            if( result.Count < OriginalSize )
            {
                result.AddRange( new byte[OriginalSize - result.Count] );
            }

            return result.ToArray();
        }

        public unsafe Bitmap ToBitmap()
        {
            Bitmap bmp = new Bitmap( 256, Math.Min( 488, Pixels.Length / 256 ), PixelFormat.Format8bppIndexed );
            ColorPalette palette = bmp.Palette;

            int k = 0;
            for( int i = 0; i < Palettes.Length; i++ )
            {
                for( int j = 0; j < Palettes[i].Colors.Length; j++, k++)
                {
                    if( Palettes[i].Colors[j].ToArgb() == Color.Transparent.ToArgb() )
                    {
                        palette.Entries[k] = Color.Black;
                    }
                    else
                    {
                        palette.Entries[k] = Palettes[i].Colors[j];
                    }
                }
            }
            bmp.Palette = palette;

            BitmapData bmd = bmp.LockBits( new Rectangle( 0, 0, bmp.Width, bmp.Height ), ImageLockMode.ReadWrite, bmp.PixelFormat );
            for( int i = 0; (i < this.Pixels.Length) && (i / 256 < bmp.Height); i++ )
            {
                bmp.SetPixel( bmd, i % 256, i / 256, Pixels[i] );
            }
            bmp.UnlockBits( bmd );

            return bmp;
        }
    }
}
