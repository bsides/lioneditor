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

using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace FFTPatcher.SpriteEditor
{
    /// <summary>
    /// Extension methods for various types.
    /// </summary>
    public static partial class ExtensionMethods
    {

        #region Static Fields (1)

        private static readonly Rectangle portraitRectangle = new Rectangle( 80, 256, 48, 32 );

        #endregion Static Fields

        #region Methods (5)


        /// <summary>
        /// Copies the rectangle to point.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="sourceRectangle">The source rectangle.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="destinationPoint">The destination point.</param>
        /// <param name="flip">if set to <c>true</c> [flip].</param>
        public static void CopyRectangleToPoint( this Bitmap source, Rectangle sourceRectangle, Bitmap destination, Point destinationPoint, Palette palette, bool flip )
        {
            BitmapData bmdSource = source.LockBits( new Rectangle( 0, 0, source.Width, source.Height ), ImageLockMode.ReadOnly, source.PixelFormat );
            BitmapData bmdDest = destination.LockBits( new Rectangle( 0, 0, destination.Width, destination.Height ), ImageLockMode.WriteOnly, destination.PixelFormat );
            if( flip )
            {
                for( int col = 0; col < sourceRectangle.Width; col++ )
                {
                    for( int row = 0; row < sourceRectangle.Height; row++ )
                    {
                        int index = bmdSource.GetPixel( col + sourceRectangle.X, row + sourceRectangle.Y );
                        if( palette.Colors[index % 16].A != 0 )
                        {
                            bmdDest.SetPixel(
                                destinationPoint.X + (sourceRectangle.Width - col - 1), destinationPoint.Y + row,
                                index );
                        }
                    }
                }
            }
            else
            {
                for( int col = 0; col < sourceRectangle.Width; col++ )
                {
                    for( int row = 0; row < sourceRectangle.Height; row++ )
                    {
                        int index = bmdSource.GetPixel( col + sourceRectangle.X, row + sourceRectangle.Y );
                        if( palette.Colors[index % 16].A != 0 )
                        {
                            bmdDest.SetPixel(
                                destinationPoint.X + col, destinationPoint.Y + row,
                                index );
                        }
                    }
                }
            }

            source.UnlockBits( bmdSource );
            destination.UnlockBits( bmdDest );
        }

        /// <summary>
        /// Draws a sprite.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        /// <param name="s">The <see cref="Sprite"/> to draw.</param>
        /// <param name="p">The <see cref="Palette"/> to use to draw the sprite.</param>
        public static void DrawSprite( this Graphics g, Sprite s, Palette p, Palette portrait, bool proper )
        {
            using( Bitmap b = new Bitmap( 256, 488 ) )
            {
                b.DrawSprite( s, p, portrait, proper );
                g.DrawImage( b, 0, 0 );
            }
        }

        /// <summary>
        /// Draws a sprite.
        /// </summary>
        /// <param name="b">The <see cref="Bitmap"/> object to draw on.</param>
        /// <param name="s">The <see cref="Sprite"/> to draw.</param>
        /// <param name="p">The <see cref="Palette"/> to use to draw the sprite.</param>
        public static void DrawSprite( this Bitmap b, Sprite s, Palette p, Palette portrait, bool proper )
        {
            if( proper )
            {
                for( int i = 0; (i < s.Pixels.Length) && (i / 256 < 256); i++ )
                {
                    b.SetPixel( i % 256, i / 256, p.Colors[s.Pixels[i] % 16] );
                }
                for( int i = 288 * 256; (i < s.Pixels.Length) && (i / 256 < 488); i++ )
                {
                    b.SetPixel( i % 256, i / 256 - 32, p.Colors[s.Pixels[i] % 16] );
                }
                for( int i = 256 * 256; (i < s.Pixels.Length) && (i / 256 < 288); i++ )
                {
                    b.SetPixel( i % 256, i / 256 + 200, p.Colors[s.Pixels[i] % 16] );
                }

                Rectangle pRect = portraitRectangle;
                pRect.Offset( 0, 200 );

                for( int x = pRect.X; x < pRect.Right; x++ )
                {
                    for( int y = pRect.Y; y < pRect.Bottom && (x + y * 256 < s.Pixels.Length); y++ )
                    {
                        b.SetPixel( x, y, portrait.Colors[s.Pixels[x + (y - 200) * 256] % 16] );
                    }
                }
            }
            else
            {
                for( int i = 0; (i < s.Pixels.Length) && (i / 256 < b.Height); i++ )
                {
                    b.SetPixel( i % 256, i / 256, p.Colors[s.Pixels[i] % 16] );
                }

                for( int x = portraitRectangle.X; x < portraitRectangle.Right; x++ )
                {
                    for( int y = portraitRectangle.Y; y < portraitRectangle.Bottom && (x + y * 256 < s.Pixels.Length); y++ )
                    {
                        b.SetPixel( x, y, portrait.Colors[s.Pixels[x + y * 256] % 16] );
                    }
                }
            }
        }

        /// <summary>
        /// Sets a pixel in this bitmap to a specified value.
        /// </summary>
        /// <param name="bmd">The bitmap data.</param>
        /// <param name="x">The x position of the pixel.</param>
        /// <param name="y">The y position of the pixel.</param>
        /// <param name="index">The index in the palette to use to set the pixel to.</param>
        public static unsafe void SetPixel( this BitmapData bmd, int x, int y, int index )
        {
            byte* p = (byte*)bmd.Scan0.ToPointer();
            int offset = y * bmd.Stride + x;
            p[offset] = (byte)index;
        }

        /// <summary>
        /// Gets a pixel in this bitmap.
        /// </summary>
        /// <param name="bmd">The bitmap data.</param>
        /// <param name="x">The x position of the pixel.</param>
        /// <param name="y">The y position of the pixel.</param>
        /// <returns>The palette index of the pixel.</returns>
        public static unsafe int GetPixel( this BitmapData bmd, int x, int y )
        {
            byte* p = (byte*)bmd.Scan0.ToPointer();
            int offset = y * bmd.Stride + x;
            return p[offset];
        }


        public static Image ToImage( this IList<IList<Color>> colors )
        {
            Bitmap b = new Bitmap( colors.Count, colors[0].Count );
            for ( int x = 0; x < b.Width; x++ )
            {
                for ( int y = 0; y < b.Height; y++ )
                {
                    b.SetPixel( x, y, colors[x][y] );
                }
            }

            return b;
        }

        #endregion Methods

    }
}
