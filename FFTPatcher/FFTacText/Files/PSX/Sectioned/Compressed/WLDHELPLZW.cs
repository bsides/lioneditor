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

namespace FFTPatcher.TextEditor.Files.PSX
{
    /// <summary>
    /// REpresents the text in the WLDHELP.LZW file.
    /// </summary>
    public class WLDHELPLZW : BasePSXCompressedFile
    {

        #region Static Fields (1)

        private static Dictionary<string, long> locations;

        #endregion Static Fields

        #region Fields (2)

        private const string filename = "WLDHELP.LZW";
        private const int numberOfSections = 21;

        #endregion Fields

        #region Properties (4)


        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected override int NumberOfSections { get { return numberOfSections; } }

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
                    locations.Add( "EVENT/WLDHELP.LZW", 0x00 );
                    locations.Add( "WORLD/WORLD.BIN", 0x8EE68 );
                }

                return locations;
            }
        }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        /// <value></value>
        public override int MaxLength { get { return 0x01ADE4; } }


        #endregion Properties

        #region Constructors (2)

        private WLDHELPLZW()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WLDHELPLZW"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        public WLDHELPLZW( IList<byte> bytes )
            : base( bytes )
        {
        }

        #endregion Constructors

    }
}