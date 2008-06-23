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

namespace FFTPatcher.TextEditor.Files.PSP
{
    /// <summary>
    /// Text for the BOOT.BIN file at location 0x28E5EC.
    /// </summary>
    public class BOOT28E5EC : AbstractBootBinFile
    {

		#region Fields (2) 

        private const string filename = "BOOT.BIN[0x28E5EC]";
        private static Dictionary<string, long> locations;

		#endregion Fields 

		#region Constructors (2) 

        /// <summary>
        /// Initializes a new instance of the <see cref="BOOT28E5EC"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        public BOOT28E5EC( IList<byte> bytes )
            : base( bytes )
        {
        }

        private BOOT28E5EC()
        {
        }

		#endregion Constructors 

		#region Properties (4) 

        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <value></value>
        public override string Filename { get { return filename; } }

        /// <summary>
        /// Gets the filenames and locations for this file.
        /// </summary>
        /// <value></value>
        public override IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "BOOT.BIN", 0x28E5EC );
                    locations.Add( "BOOT.BIN ", 0x30A198 );
                }
                return locations;
            }
        }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        /// <value></value>
        public override int MaxLength
        {
            get { return 0x580A; }
        }

        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected override int NumberOfSections
        {
            get { return 24; }
        }

		#endregion Properties 

    }
}
