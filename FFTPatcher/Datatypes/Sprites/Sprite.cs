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

using FFTPatcher.Datatypes;
using System.Collections.Generic;

namespace FFTPatcher.Datatypes
{
    public class Sprite
    {
        public Palette[] Palettes { get; private set; }
        public byte[] Pixels { get; private set; }
        public byte[] AfterPixels { get; private set; }

        public Sprite( IList<byte> bytes )
        {
            Palettes = new Palette[16];
            for( int i = 0; i < 16; i++ )
            {
                Palettes[i] = new Palette( new SubArray<byte>( bytes, i * 32, (i + 1) * 32 - 1 ) );
            }

            Pixels = BuildPixels( new SubArray<byte>( bytes, 16 * 32 ) );
            //AfterPixels = new SubArray<byte>( bytes, 0x9200 ).ToArray();
        }

        private static byte[] BuildPixels( IList<byte> bytes )
        {
            List<byte> result = new List<byte>( 36864 * 2 );
            foreach( byte b in bytes )
            {
                result.Add( b.GetLowerNibble() );
                result.Add( b.GetUpperNibble() );
            }

            return result.ToArray();
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>();
            foreach( Palette p in Palettes )
            {
                result.AddRange( p.ToByteArray() );
            }

            for( int i = 0; i < 36864; i += 2 )
            {
                result.Add( (byte)((Pixels[i + 1] << 4) | (Pixels[i] & 0x0F)) );
            }

            result.AddRange( AfterPixels );
            return result.ToArray();
        }
    }
}
