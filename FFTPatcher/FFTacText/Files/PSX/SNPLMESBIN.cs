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
    public class SNPLMESBIN : BasePSXPartitionedFile
    {

		#region Static Fields (3) 

        private static string[][] entryNames;
        private static Dictionary<string, long> locations;
        private static string[] sectionNames;

		#endregion Static Fields 

		#region Fields (3) 

        private const string filename = "SNPLMES.BIN";
        private const int numberOfSections = 6;
        private const int sectionLength = 0xA000;

		#endregion Fields 

		#region Properties (6) 


        public override IList<IList<string>> EntryNames { get { return entryNames; } }

        public override string Filename { get { return filename; } }

        public override IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "WORLD/SNPLMES.BIN", 0x00 );
                }

                return locations;
            }
        }

        public override int NumberOfSections { get { return numberOfSections; } }

        public override int SectionLength { get { return sectionLength; } }

        public override IList<string> SectionNames { get { return sectionNames; } }


		#endregion Properties 

		#region Constructors (3) 

        static SNPLMESBIN()
        {
            sectionNames = new string[numberOfSections];
            entryNames = new string[numberOfSections][];
            entryNames[0] = new string[204];
            entryNames[1] = new string[168];
            entryNames[2] = new string[186];
            entryNames[3] = new string[217];
            entryNames[4] = new string[39];
            entryNames[5] = new string[14];
        }

        private SNPLMESBIN()
        {
        }

        public SNPLMESBIN( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

    }
}
