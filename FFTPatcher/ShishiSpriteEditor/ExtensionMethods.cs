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
using System.Collections.Generic;

namespace FFTPatcher.SpriteEditor
{
    /// <summary>
    /// Extension methods for various types.
    /// </summary>
    public static class ExtensionMethods
    {
        public static void DrawSprite( this Graphics g, Sprite s, Palette p )
        {
            using( Bitmap b = new Bitmap( 256, 488 ) )
            {
                for( int i = 0; i < s.Pixels.Length; i++ )
                {
                    b.SetPixel( i % 256, i / 256, p.Colors[s.Pixels[i]] );
                }

                g.DrawImage( b, 0, 0 );
            }
        }

        public static byte[] ToPALFile( this Palette[] palettes )
        {
            List<byte> result = new List<byte>( 0x418 );
            result.AddRange( new byte[] { 
                0x52, 0x49, 0x46, 0x46, // RIFF
                0x10, 0x04, 0x00, 0x00, // Filesize or sommat
                0x50, 0x41, 0x4C, 0x20, 0x64, 0x61, 0x74, 0x61,  // PAL data
                0x04, 0x04, 0x00, 0x00, 0x00, 0x03, 0x00, 0x01 } ); // filesize of sommat

            foreach( Palette p in palettes )
            {
                result.AddRange( p.ToPALByteArray() );
            }

            return result.ToArray();
        }

    }
}
