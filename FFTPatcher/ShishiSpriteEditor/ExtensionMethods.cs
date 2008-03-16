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

namespace FFTPatcher.SpriteEditor
{
    /// <summary>
    /// Extension methods for various types.
    /// </summary>
    public static partial class ExtensionMethods
    {

		#region Methods (4) 


        /// <summary>
        /// Draws a sprite.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        /// <param name="s">The <see cref="Sprite"/> to draw.</param>
        /// <param name="p">The <see cref="Palette"/> to use to draw the sprite.</param>
        public static void DrawSprite( this Graphics g, Sprite s, Palette p )
        {
            using( Bitmap b = new Bitmap( 256, 488 ) )
            {
                b.DrawSprite( s, p );
                g.DrawImage( b, 0, 0 );
            }
        }

        /// <summary>
        /// Draws a sprite.
        /// </summary>
        /// <param name="b">The <see cref="Bitmap"/> object to draw on.</param>
        /// <param name="s">The <see cref="Sprite"/> to draw.</param>
        /// <param name="p">The <see cref="Palette"/> to use to draw the sprite.</param>
        public static void DrawSprite( this Bitmap b, Sprite s, Palette p )
        {
            for( int i = 0; (i < s.Pixels.Length) && (i / 256 < b.Height); i++ )
            {
                b.SetPixel( i % 256, i / 256, p.Colors[s.Pixels[i] % 16] );
            }
        }

        /// <summary>
        /// Sets a pixel in this bitmap to a specified value.
        /// </summary>
        /// <param name="bmd">The bitmap data.</param>
        /// <param name="x">The x position of the pixel.</param>
        /// <param name="y">The y position of the pixel.</param>
        /// <param name="index">The index in the palette to use to set the pixel to.</param>
        public static unsafe void SetPixel( this Bitmap b, BitmapData bmd, int x, int y, int index )
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
        public static unsafe int GetPixel( this Bitmap b, BitmapData bmd, int x, int y )
        {
            byte* p = (byte*)bmd.Scan0.ToPointer();
            int offset = y * bmd.Stride + x;
            return p[offset];
        }

		#endregion Methods 

    }
}
