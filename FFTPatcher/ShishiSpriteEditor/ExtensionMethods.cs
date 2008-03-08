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


        public static void DrawSprite( this Graphics g, Sprite s, Palette p )
        {
            using( Bitmap b = new Bitmap( 256, 488 ) )
            {
                b.DrawSprite( s, p );
                g.DrawImage( b, 0, 0 );
            }
        }

        public static void DrawSprite( this Bitmap b, Sprite s, Palette p )
        {
            for( int i = 0; (i < s.Pixels.Length) && (i / 256 < b.Height); i++ )
            {
                b.SetPixel( i % 256, i / 256, p.Colors[s.Pixels[i] % 16] );
            }
        }

        public static unsafe void SetPixel( this Bitmap b, BitmapData bmd, int x, int y, int index )
        {
            byte* p = (byte*)bmd.Scan0.ToPointer();
            int offset = y * bmd.Stride + x;
            p[offset] = (byte)index;
        }

        public static unsafe int GetPixel( this Bitmap b, BitmapData bmd, int x, int y )
        {
            byte* p = (byte*)bmd.Scan0.ToPointer();
            int offset = y * bmd.Stride + x;
            return p[offset];
        }

		#endregion Methods 

    }
}
