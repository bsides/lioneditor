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

namespace FFTPatcher.TextEditor.Files
{
    public class FilePartition : IPartition
    {

		#region Fields (2) 

        private GenericCharMap charmap;
        private int length;

		#endregion Fields 

		#region Properties (4) 


        public IList<string> Entries { get; private set; }

        public IList<string> EntryNames { get; private set; }

        public int Length { get { return CalcLength(); } }

        public int MaxLength { get { return length; } }


		#endregion Properties 

		#region Constructors (3) 

        private FilePartition( IList<string> entryNames, TextUtilities.CharMapType charmap )
        {
            this.charmap = charmap == TextUtilities.CharMapType.PSP ?
                TextUtilities.PSPMap as GenericCharMap :
                TextUtilities.PSXMap as GenericCharMap;
            EntryNames = entryNames;
        }

        public FilePartition( IList<byte> bytes, IList<string> entryNames, TextUtilities.CharMapType charmap )
            : this( entryNames, charmap )
        {
            length = bytes.Count;
            Entries = TextUtilities.ProcessList( bytes.Sub( 0, bytes.LastIndexOf( (byte)0xFE ) ), charmap );
        }

        public FilePartition( IList<string> entries, int length, IList<string> entryNames, TextUtilities.CharMapType charmap )
            : this( entryNames, charmap )
        {
            this.length = length;
            Entries = entries;
        }

		#endregion Constructors 

		#region Methods (3) 


        private int CalcLength()
        {
            return ToUnpaddedBytes().Count;
        }

        private List<byte> ToUnpaddedBytes()
        {
            List<byte> result = new List<byte>( length );
            foreach( string entry in Entries )
            {
                result.AddRange( charmap.StringToByteArray( entry ) );
            }

            return result;
        }

        public byte[] ToByteArray()
        {
            List<byte> result = ToUnpaddedBytes();

            while( result.Count < length )
            {
                result.Add( 0 );
            }

            return result.ToArray();
        }


		#endregion Methods 

    }
}
