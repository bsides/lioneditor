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
    along with FFTPatcher.  If not, see <http:,www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;

namespace FFTPatcher.TextEditor.Files.PSX
{
    /// <summary>
    /// Text for the WLDMES.BIN file.
    /// </summary>
    public class WLDMESBIN : BasePSXPartitionedFile
    {

        #region Static Fields (3)

        private static string[][] entryNames;
        private static Dictionary<Enum, long> locations;
        private static string[] sectionNames;

        #endregion Static Fields

        #region Fields (3)

        private const string filename = "WLDMES.BIN";
        private const int numberOfSections = 112;
        private const int sectionLength = 0xB000;

        #endregion Fields

        #region Properties (6)


        /// <summary>
        /// Gets a collection of lists of strings, each string being a description of an entry in this file.
        /// </summary>
        /// <value></value>
        public override IList<IList<string>> EntryNames { get { return entryNames; } }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <value></value>
        public override string Filename { get { return filename; } }

        /// <summary>
        /// Gets the filenames and locations for this file.
        /// </summary>
        /// <value></value>
        public override IDictionary<Enum, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<Enum, long>();
                    locations.Add( PsxIso.Sectors.WORLD_WLDMES_BIN, 0x00 );
                }

                return locations;
            }
        }

        /// <summary>
        /// Gets the number of sections in this file.
        /// </summary>
        /// <value></value>
        public override int NumberOfSections { get { return numberOfSections; } }

        /// <summary>
        /// Gets the length of every section in this file.
        /// </summary>
        /// <value></value>
        public override int SectionLength { get { return sectionLength; } }

        /// <summary>
        /// Gets a collection of strings with a description of each section in this file.
        /// </summary>
        /// <value></value>
        public override IList<string> SectionNames { get { return sectionNames; } }


        #endregion Properties

        #region Constructors (3)

        static WLDMESBIN()
        {

            int[] sectionLengths = new int[112] {
                64,96,16,47,64,41,256,256,
                256,256,96,24,1,1,1,1,
                168,168,168,168,168,168,168,168,
                168,168,168,168,168,168,168,168,
                168,168,168,168,168,168,168,168,
                168,168,168,168,168,168,168,168,
                168,168,168,168,168,168,162,168,
                168,168,168,169,168,168,168,168,
                168,168,168,168,168,168,168,168,
                168,168,168,168,168,168,168,168,
                168,168,168,168,168,162,168,168,
                168,168,168,168,168,168,169,168,
                168,162,168,168,168,168,168,168,
                168,168,168,168,168,168,168,168 };

            sectionNames = new string[numberOfSections];
            entryNames = new string[numberOfSections][];
            for( int i = 0; i < numberOfSections; i++ )
            {
                entryNames[i] = new string[sectionLengths[i]];
            }
        }

        private WLDMESBIN()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WLDMESBIN"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        public WLDMESBIN( IList<byte> bytes )
            : base( bytes )
        {
        }

        #endregion Constructors

    }
}
