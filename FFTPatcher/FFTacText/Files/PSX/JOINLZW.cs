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
    public class JOINLZW : BasePSXSectionedFile
    {

		#region Static Fields (3) 

        private static string[][] entryNames;
        private static Dictionary<string, long> locations;
        private static string[] sectionNames = new string[5];

		#endregion Static Fields 

		#region Fields (1) 

        private const string filename = "JOIN.LZW";

		#endregion Fields 

		#region Properties (6) 


        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected override int NumberOfSections { get { return 5; } }

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
                    locations.Add( "EVENT/JOIN.LZW", 0x00 );
                    locations.Add( "EVENT/REQUIRE.OUT", 0xCE70 );
                }

                return locations;
            }
        }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        /// <value></value>
        public override int MaxLength { get { return 0x41F6; } }

        /// <summary>
        /// Gets a collection of strings with a description of each section in this file.
        /// </summary>
        /// <value></value>
        public override IList<string> SectionNames { get { return sectionNames; } }


		#endregion Properties 

		#region Constructors (3) 

        static JOINLZW()
        {
            entryNames = new string[5][];
            entryNames[0] = new string[1];
            entryNames[1] = new string[172];
            entryNames[2] = new string[172];
            entryNames[3] = new string[172];
            entryNames[4] = new string[155];
        }

        private JOINLZW()
        {
        }

        public JOINLZW( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

    }
}
