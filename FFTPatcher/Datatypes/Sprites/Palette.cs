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

using System.Collections.Generic;
using System.Drawing;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Datatypes
{
    public class Palette
    {
        public static Color BytesToColor(byte first, byte second)
        {
            int b = (second & 0x7C) << 1;
            int g = (second & 0x03) << 6 | (first & 0xE0) >> 2;
            int r = (first & 0x1F) << 3;

            return Color.FromArgb( r, g, b );
        }

        public static byte[] ColorToBytes( Color c )
        {
            byte r = (byte)((c.R & 0xF8) >> 3);
            byte g = (byte)((c.G & 0xF8) >> 3);
            byte b = (byte)((c.B & 0xF8) >> 3);

            byte[] result = new byte[2];
            result[0] = (byte)(((g & 0x07) << 5) | r);
            result[1] = (byte)((b << 2) | ((g & 0x18) >> 3));

            return result;
        }

        public Color[] Colors { get; private set; }

        public Palette( SubArray<byte> bytes )
        {
            Colors = new Color[16];
            for( int i = 0; i < 16; i++ )
            {
                Colors[i] = BytesToColor( bytes[i * 2], bytes[i * 2 + 1] );
            }

            if( Colors[0].ToArgb() == Color.Black.ToArgb() )
            {
                Colors[0] = Color.Transparent;
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 16 * 2 );
            foreach( Color c in Colors )
            {
                result.AddRange( ColorToBytes( c ) );
            }

            if( Colors[0] == Color.Transparent )
            {
                result[0] = 0x00;
                result[1] = 0x00;
            }

            return result.ToArray();
        }
    }
}
