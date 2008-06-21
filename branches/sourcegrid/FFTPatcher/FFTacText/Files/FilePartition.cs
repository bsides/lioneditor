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
    /// <summary>
    /// A portion of a file that has a fixed length.
    /// </summary>
    public class FilePartition : IPartition
    {

		#region Fields (2) 

        private GenericCharMap charmap;
        private int length;

		#endregion Fields 

		#region Properties (5) 


        /// <summary>
        /// Gets the entries.
        /// </summary>
        public IList<string> Entries { get; private set; }

        /// <summary>
        /// Gets a collection of strings, each string being a description of an entry in this partition.
        /// </summary>
        public IList<string> EntryNames { get; private set; }

        /// <summary>
        /// Gets the current length of this partition.
        /// </summary>
        public int Length { get { return CalcLength(); } }

        /// <summary>
        /// Gets the maximum length of this partition.
        /// </summary>
        public int MaxLength { get { return length; } }

        /// <summary>
        /// Gets the owner of this instance.
        /// </summary>
        public IPartitionedFile Owner { get; private set; }


		#endregion Properties 

		#region Constructors (3) 

        private FilePartition( IPartitionedFile owner, IList<string> entryNames, int maxLength, GenericCharMap charmap )
        {
            length = maxLength;
            Owner = owner;
            this.charmap = charmap;
            EntryNames = entryNames;
        }

        public FilePartition( IPartitionedFile owner, IList<byte> bytes, IList<string> entryNames, int maxLength, GenericCharMap charmap )
            : this( owner, entryNames, maxLength, charmap )
        {
            Entries = TextUtilities.ProcessList( bytes.Sub( 0, bytes.LastIndexOf( (byte)0xFE ) ), charmap );
        }

        public FilePartition( IPartitionedFile owner, IList<string> entries, int maxLength, IList<string> entryNames, GenericCharMap charmap )
            : this( owner, entryNames, maxLength, charmap )
        {
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

        /// <summary>
        /// Creates a byte array representing this partition.
        /// </summary>
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
