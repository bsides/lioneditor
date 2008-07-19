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
    public class FFTFont : PatchableFile
    {

		#region Properties (2) 


        public Glyph[] Glyphs { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance has changed.
        /// </summary>
        /// <value></value>
        public override bool HasChanged
        {
            get { return true; }
        }


		#endregion Properties 

		#region Constructors (1) 

        public FFTFont( IList<byte> bytes, IList<byte> widthBytes )
        {
            Glyphs = new Glyph[2200];
            for( int i = 0; i < 2200; i++ )
            {
                Glyphs[i] = new Glyph( widthBytes[i], bytes.Sub( i * 35, (i + 1) * 35 - 1 ) );
            }
        }

		#endregion Constructors 

		#region Methods (3) 


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


		#endregion Methods 

        public override IList<PatchedByteArray> GetPatches( Context context )
        {
            var result = new List<PatchedByteArray>( 5 );

            var font = ToByteArray();
            var width = ToWidthsByteArray();

            if ( context == Context.US_PSX )
            {
                result.Add( new PatchedByteArray( PsxIso.Sectors.EVENT_FONT_BIN, 0, font ) );
                result.Add( new PatchedByteArray( PsxIso.Sectors.BATTLE_BIN, 0xFF0FC, width ) );
            }
            else if ( context == Context.US_PSP )
            {
                result.Add( new PatchedByteArray( PspIso.Sectors.PSP_GAME_SYSDIR_BOOT_BIN, 0x27B80C, font ) );
                result.Add( new PatchedByteArray( PspIso.Sectors.PSP_GAME_SYSDIR_BOOT_BIN, 0x293F40, width ) );
                result.Add( new PatchedByteArray( PspIso.Sectors.PSP_GAME_SYSDIR_EBOOT_BIN, 0x27B80C, font ) );
                result.Add( new PatchedByteArray( PspIso.Sectors.PSP_GAME_SYSDIR_EBOOT_BIN, 0x293F40, width ) );
            }

            return result;
        }
    }
}
