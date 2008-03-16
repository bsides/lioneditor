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
    public class OPENLZW : BasePSXSectionedFile
    {

		#region Static Fields (3) 

        private static string[][] entryNames;
        private static Dictionary<string, long> locations;
        private static string[] sectionNames = new string[32] {
            "", "", "", "", "", "", "", "", 
            "Unit names", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "Birthday",
            "Track names", "Composers comments", "", "", "", "", "", "" };

		#endregion Static Fields 

		#region Fields (1) 

        private const string filename = "OPEN.LZW";

		#endregion Fields 

		#region Properties (6) 


        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected override int NumberOfSections { get { return 32; } }

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
        public override IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "EVENT/OPEN.LZW", 0x00 );
                }

                return locations;
            }
        }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        /// <value></value>
        public override int MaxLength { get { return 0x5579; } }

        /// <summary>
        /// Gets a collection of strings with a description of each section in this file.
        /// </summary>
        /// <value></value>
        public override IList<string> SectionNames { get { return sectionNames; } }


		#endregion Properties 

		#region Constructors (3) 

        static OPENLZW()
        {
            entryNames = new string[32][];
            int[] sectionLengths = new int[32] {
                1, 1, 1, 1, 1, 1, 1, 1,
                1024, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 16,
                97, 96, 1, 1, 1, 1, 1, 1 };
            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[sectionLengths[i]];
            }
        }

        private OPENLZW()
        {
        }

        public OPENLZW( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

    }
}
