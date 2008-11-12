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

namespace FFTPatcher.SpriteEditor
{
    /// <summary>
    /// A FFT sprite.
    /// </summary>
    public abstract class AbstractSprite
    {

        #region Properties (6)

        public int OriginalSize { get; private set; }


        /// <summary>
        /// Gets or sets the palettes used to draw this sprite.
        /// </summary>
        public Palette[] Palettes { get; set; }

        /// <summary>
        /// Gets the pixels used to draw this sprite.
        /// </summary>
        public IList<byte> Pixels { get; private set; }

        public virtual int Width { get { return 256; } }
        public abstract int Height { get; }

        #endregion Properties

        #region Constructors (1)

        public AbstractSprite( IList<byte> bytes, params IList<byte>[] extraBytes )
        {
            OriginalSize = bytes.Count;
            Pixels = BuildPixels( bytes, extraBytes );
        }

        #endregion Constructors

        #region Methods (11)


        protected abstract IList<byte> BuildPixels( IList<byte> bytes, IList<byte>[] extraBytes );

        protected static Palette[] BuildPalettes( IList<byte> paletteBytes )
        {
            Palette[] result = new Palette[16];
            for( int i = 0; i < 16; i++ )
            {
                result[i] = new Palette( paletteBytes.Sub( i * 32, (i + 1) * 32 - 1 ) );
            }

            return result;
        }

        /// <summary>
        /// Imports a bitmap and tries to convert it to a FFT sprite.
        /// </summary>
        public void ImportBitmap( Bitmap bmp, out bool foundBadPixels )
        {
            foundBadPixels = false;

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
                Palettes[i] = new Palette( bmp.Palette.Entries.Sub( 16 * i, 16 * (i + 1) - 1 ) );
            }

            BitmapData bmd = bmp.LockBits( new Rectangle( 0, 0, bmp.Width, bmp.Height ), ImageLockMode.ReadWrite, bmp.PixelFormat );
            for( int i = 0; (i < Pixels.Count) && (i / 256 < bmp.Height); i++ )
            {
                Pixels[i] = (byte)bmd.GetPixel( i % 256, i / 256 );
                if( Pixels[i] >= 16 )
                {
                    foundBadPixels = true;
                }
            }

            bmp.UnlockBits( bmd );
        }

        public void Import( Image file )
        {
            if( file is Bitmap )
            {
                bool bad;
                ImportBitmap( file as Bitmap, out bad );
            }
            else
            {
                throw new ArgumentException( "file must be Bitmap", "file" );
            }
        }


        /// <summary>
        /// Converts this sprite to an indexed bitmap.
        /// </summary>
        public unsafe Bitmap ToBitmap()
        {
            return ToBitmap( false );
        }

        public unsafe Bitmap ToBitmap( bool proper )
        {
            Bitmap bmp = new Bitmap( 256, 488, PixelFormat.Format8bppIndexed );
            ColorPalette palette = bmp.Palette;

            int k = 0;
            for( int i = 0; i < Palettes.Length; i++ )
            {
                for( int j = 0; j < Palettes[i].Colors.Length; j++, k++ )
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
            if( proper )
            {
                for( int i = 0; (i < this.Pixels.Count) && (i / 256 < 256); i++ )
                {
                    bmd.SetPixel( i % 256, i / 256, Pixels[i] );
                }
                for( int i = 288 * 256; (i < this.Pixels.Count) && (i / 256 < 488); i++ )
                {
                    bmd.SetPixel( i % 256, i / 256 - 32, Pixels[i] );
                }
                for( int i = 256 * 256; (i < this.Pixels.Count) && (i / 256 < 288); i++ )
                {
                    bmd.SetPixel( i % 256, i / 256 + 200, Pixels[i] );
                }
            }
            else
            {
                for( int i = 0; (i < this.Pixels.Count) && (i / 256 < bmp.Height); i++ )
                {
                    bmd.SetPixel( i % 256, i / 256, Pixels[i] );
                }
            }
            bmp.UnlockBits( bmd );

            return bmp;
        }

        public Image Export()
        {
            return ToBitmap();
        }

        /// <summary>
        /// Converts this sprite to an array of bytes.
        /// </summary>
        public abstract IList<byte[]> ToByteArrays();
        //{
        //    List<byte> result = new List<byte>();
        //    if( SPR && !SP2 )
        //    {
        //        foreach( Palette p in Palettes )
        //        {
        //            result.AddRange( p.ToByteArray() );
        //        }
        //    }

        //    for(
        //        int i = 0;
        //        (Compressed && (i < 36864) && (2 * i + 1 < Pixels.Length)) ||
        //        (!Compressed && (2 * i + 1 < Pixels.Length));
        //        i++ )
        //    {
        //        result.Add( (byte)((Pixels[2 * i + 1] << 4) | (Pixels[2 * i] & 0x0F)) );
        //    }

        //    if( Pixels.Length > 2 * 36864 && Compressed )
        //    {
        //        result.AddRange( Recompress( Pixels.Sub( 2 * 36864 ) ) );
        //    }

        //    if( result.Count < OriginalSize )
        //    {
        //        result.AddRange( new byte[OriginalSize - result.Count] );
        //    }

        //    return result.ToArray();
        //}

        #endregion Methods

        public void Draw( Graphics graphics, int paletteIndex )
        {
            graphics.DrawSprite( this, Palettes[paletteIndex], Palettes[(paletteIndex + 8) % 8 + 8], true );
        }
    }
}
