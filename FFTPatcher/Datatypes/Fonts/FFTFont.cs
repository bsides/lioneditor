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

namespace FFTPatcher.Datatypes
{
    public class FFTFont
    {
        public Glyph[] Glyphs { get; private set; }

        public FFTFont( SubArray<byte> bytes )
        {
            Glyphs = new Glyph[2200];
            for( int i = 0; i < 2200; i++ )
            {
                Glyphs[i] = new Glyph( new SubArray<byte>( bytes, i * 35, (i + 1) * 35 - 1 ) );
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 35 * 2200 );
            foreach( Glyph g in Glyphs )
            {
                result.AddRange( g.ToByteArray() );
            }
            if( FFTPatch.Context == Context.US_PSP )
            {
                result.AddRange( new byte[0x338] );
            }
            return result.ToArray();
        }
    }
}
