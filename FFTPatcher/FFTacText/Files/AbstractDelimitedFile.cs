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
using System;

namespace FFTPatcher.TextEditor.Files
{
    /// <summary>
    /// Represents a file that has no sections where each entry is delimited 
    /// by 0xFE bytes.
    /// </summary>
    public abstract class AbstractDelimitedFile : AbstractStringSectioned
    {

        #region Properties (1)


        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected override int NumberOfSections
        {
            get { return EntryNames.Count; }
        }


        #endregion Properties

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractDelimitedFile"/> class.
        /// </summary>
        protected AbstractDelimitedFile()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractDelimitedFile"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="sectionOffsets">The start position of each section.</param>
        protected AbstractDelimitedFile( IList<byte> bytes, params int[] sectionOffsets )
            : this()
        {
            if ( sectionOffsets.Length != NumberOfSections )
            {
                throw new ArgumentException( "incorrect number of section offsets", "sectionOffsets" );
            }

            List<int> offsets = new List<int>( sectionOffsets );
            offsets.Add( bytes.Count );
            for( int i = 0; i < NumberOfSections; i++ )
            {
                int sectionStart = offsets[i];
                int sectionEnd = offsets[i + 1] - 1;
                Sections.Add( TextUtilities.ProcessList( bytes.Sub( sectionStart, sectionEnd ), CharMap ) );
            }
        }

        #endregion Constructors

        #region Methods (2)


        /// <summary>
        /// Gets a list of bytes that represent this file in its on-disc form.
        /// </summary>
        /// <returns></returns>
        protected override IList<byte> ToFinalBytes()
        {
            List<byte> result = new List<byte>();
            foreach ( IList<string> section in Sections )
            {
                result.AddRange( CharMap.StringsToByteArray( section ) );
            }

            return result;
        }

        /// <summary>
        /// Creates a collection of bytes representing the uncompressed contents of this file.
        /// </summary>
        /// <returns></returns>
        public override IList<byte> ToUncompressedBytes()
        {
            return ToFinalBytes();
        }


        #endregion Methods

    }
}
