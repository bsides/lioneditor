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

        private Palette[] palettes;

        /// <summary>
        /// Gets or sets the palettes used to draw this sprite.
        /// </summary>
        public Palette[] Palettes 
        {
            get { return palettes; }
            set
            {
                palettes = value;
                ThumbnailDirty = true;
                BitmapDirty = true;
             }
         }
 
        public int CurrentSize { get; protected set; }

        public int MaximumSize
        {
            get
            {
                if( OriginalSize % 2048 == 0 )
                {
                    return OriginalSize;
                }
                else
                {
                    return OriginalSize / 2048 + 2048;
                }
            }
        }

         /// <summary>
         /// Gets the pixels used to draw this sprite.
         /// </summary>
        public IList<byte> Pixels { get; private set; }

        public virtual int Width { get { return 256; } }
        public abstract int Height { get; }
        public string Name { get; private set; }

        protected abstract Rectangle PortraitRectangle { get; }
        protected abstract Rectangle ThumbnailRectangle { get; }

        public virtual Shape Shape { get { return null; } }

        protected bool ThumbnailDirty { get; set; }
        protected Image CachedThumbnail { get; set; }
        protected bool BitmapDirty { get; set; }
        protected Bitmap CachedBitmap { get; set; }

        public void DrawSprite( Bitmap b, int palette, int portraitPalette )
        {
            DrawSpriteInternal( palette, portraitPalette, ( x, y, z ) => b.SetPixel( x, y, z ) );
        }

        public void DrawSprite( BitmapData bmd, int palette, int portraitPalette )
        {
            DrawSpriteInternal( palette, portraitPalette, ( x, y, z ) => bmd.SetPixel32bpp( x, y, z ) );
        }

        protected abstract void DrawSpriteInternal( int palette, int portraitPalette, SetPixel setPixel );

        protected delegate void SetPixel( int x, int y, Color color );

        public IList<string> Filenames { get; private set; }

        #endregion Properties

        #region Constructors (1)

        internal AbstractSprite( SerializedSprite sprite )
            : this( sprite.Name, sprite.Filenames )
        {
            OriginalSize = sprite.OriginalSize;
            CurrentSize = OriginalSize;
            Palettes = BuildPalettes( sprite.Palettes );
            Pixels = new byte[sprite.Pixels.Count];
            sprite.Pixels.CopyTo( Pixels, 0 );
        }

        private AbstractSprite( string name, IList<string> filenames )
        {
            Name = name;
            Filenames = filenames.ToArray();
            ThumbnailDirty = true;
            BitmapDirty = true;
        }

        protected AbstractSprite( string name, IList<string> filenames, IList<byte> bytes, params IList<byte>[] extraBytes )
            : this( name, filenames )
        {
            OriginalSize = bytes.Count;
            CurrentSize = OriginalSize;
            Palettes = BuildPalettes( bytes.Sub( 0, 16 * 32 - 1 ) );
            Pixels = BuildPixels( bytes.Sub( 16 * 32 ), extraBytes );
        }

        #endregion Constructors

        #region Methods (11)

        protected abstract Image GetThumbnailInner();

        public Image GetThumbnail()
        {
            if ( ThumbnailDirty )
            {
                CachedThumbnail = GetThumbnailInner();
                ThumbnailDirty = false;
            }

            return CachedThumbnail;
        }

        public event EventHandler PixelsChanged;

        protected void FirePixelsChanged()
        {
            if ( PixelsChanged != null )
            {
                PixelsChanged( this, EventArgs.Empty );
            }
        }

        public void ImportSPR( IList<byte> bytes )
        {
            BitmapDirty = true;
            ThumbnailDirty = true;
            Palettes = BuildPalettes( bytes.Sub( 0, 16 * 32 - 1 ) );
            ImportSPRInner( bytes.Sub( 16 * 32 ) );
            FirePixelsChanged();
        }

        protected abstract IList<byte> BuildPixels( IList<byte> bytes, IList<byte>[] extraBytes );

        protected abstract void ImportSPRInner( IList<byte> bytes );

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
        public virtual void ImportBitmap( Bitmap bmp, out bool foundBadPixels )
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

            Pixels.InitializeElements();

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

            ThumbnailDirty = true;
            BitmapDirty = true;

            FirePixelsChanged();
        }

        public virtual void Import( Image file )
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

            ThumbnailDirty = true;
            BitmapDirty = true;
        }

        protected void FixupColorPalette( ColorPalette palette )
        {
            int k = 0;
            for ( int i = 0; i < Palettes.Length; i++ )
            {
                for ( int j = 0; j < Palettes[i].Colors.Length; j++, k++ )
                {
                    if ( Palettes[i].Colors[j].ToArgb() == Color.Transparent.ToArgb() )
                    {
                        palette.Entries[k] = Color.Black;
                    }
                    else
                    {
                        palette.Entries[k] = Palettes[i].Colors[j];
                    }
                }
            }
        }

        /// <summary>
        /// Converts this sprite to an indexed bitmap.
        /// </summary>
        public unsafe Bitmap ToBitmap()
        {
            if ( BitmapDirty )
            {

                Bitmap bmp = new Bitmap( Width, Height, PixelFormat.Format8bppIndexed );
                ColorPalette palette = bmp.Palette;
                FixupColorPalette( palette );
                bmp.Palette = palette;


                BitmapData bmd = bmp.LockBits( new Rectangle( 0, 0, bmp.Width, bmp.Height ), ImageLockMode.ReadWrite, bmp.PixelFormat );
                ToBitmapInner( bmp, bmd );
                bmp.UnlockBits( bmd );

                CachedBitmap = bmp;
                BitmapDirty = false;
            }
            return CachedBitmap;
        }

        protected abstract void ToBitmapInner( Bitmap bmp, BitmapData bmd );

        public virtual Image Export()
        {
            return ToBitmap();
        }

        /// <summary>
        /// Converts this sprite to an array of bytes.
        /// </summary>
        public abstract IList<byte[]> ToByteArrays();

        #endregion Methods

    }
}
