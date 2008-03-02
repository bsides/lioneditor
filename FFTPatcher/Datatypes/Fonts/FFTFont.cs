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
    /// <summary>
    /// Represents a font used in FFT, which is an array of 2200 bitmaps.
    /// </summary>
    public class FFTFont
    {
        public Glyph[] Glyphs { get; private set; }

        public FFTFont( IList<byte> bytes, IList<byte> widthBytes )
        {
            Glyphs = new Glyph[2200];
            for( int i = 0; i < 2200; i++ )
            {
                Glyphs[i] = new Glyph( widthBytes[i], bytes.Sub( i * 35, (i + 1) * 35 - 1 ) );
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 35 * 2200 );
            foreach( Glyph g in Glyphs )
            {
                result.AddRange( g.ToByteArray() );
            }
            return result.ToArray();
        }

        public byte[] ToWidthsByteArray()
        {
            byte[] result = new byte[2200];
            for( int i = 0; i < 2200; i++ )
            {
                result[i] = Glyphs[i].Width;
            }

            return result;
        }

        public List<string> GenerateCodes()
        {
            List<string> strings = new List<string>();

            if( FFTPatch.Context == Context.US_PSP )
            {
                strings.AddRange( Codes.GenerateCodes( Context.US_PSP, Resources.FontBin, this.ToByteArray(), 0x27F7B8 ) );
                strings.AddRange( Codes.GenerateCodes( Context.US_PSP, Resources.FontWidthsBin, this.ToWidthsByteArray(), 0x297EEC ) );
                strings.AddRange( Codes.GenerateCodes( Context.US_PSP, Resources.FontBin, this.ToByteArray(), 0x2FB364 ) );
                strings.AddRange( Codes.GenerateCodes( Context.US_PSP, Resources.FontWidthsBin, this.ToWidthsByteArray(), 0x313A6C ) );
            }
            else
            {
                strings.AddRange( Codes.GenerateCodes( Context.US_PSX, PSXResources.FontBin, this.ToByteArray(), 0x13B8F8 ) );
                strings.AddRange( Codes.GenerateCodes( Context.US_PSX, PSXResources.FontWidthsBin, this.ToWidthsByteArray(), 0x1533E0 ) );
            }
            return strings;
        }
    }
}
