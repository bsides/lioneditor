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

namespace FFTPatcher.Datatypes
{
    public enum FontColor
    {
        Transparent = 0,
        Black = 1,
        Dark = 2,
        Light = 3
    }

    public class Glyph
    {
        public FontColor[] Pixels { get; private set; }
        public byte Width { get; set; }

        public Glyph( byte width, SubArray<byte> bytes )
        {
            Width = width;
            Pixels = new FontColor[14 * 10];
            for( int i = 0; i < bytes.Count; i++ )
            {
                CopyByteToPixels( bytes[i], Pixels, i * 4 );
            }
        }

        private void CopyByteToPixels( byte b, FontColor[] destination, int index )
        {
            destination[index] = (FontColor)((b & 0xC0) >> 6);
            destination[index + 1] = (FontColor)((b & 0x30) >> 4);
            destination[index + 2] = (FontColor)((b & 0x0C) >> 2);
            destination[index + 3] = (FontColor)(b & 0x03);
        }

        private byte CopyPixelsToByte( FontColor[] source, int index )
        {
            byte result = 0;
            result |= (byte)(((int)source[index]) << 6);
            result |= (byte)(((int)source[index+1]) << 4);
            result |= (byte)(((int)source[index+2]) << 2);
            result |= (byte)((int)source[index+3]);
            return result;
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[35];
            for( int i = 0; i < 35; i++ )
            {
                result[i] = CopyPixelsToByte( Pixels, i * 4 );
            }

            return result;
        }
    }
}
