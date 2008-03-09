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

using System.Collections.Generic;

namespace FFTPatcher.TextEditor.Files.PSX
{
    public class WLDMES : BasePSXPartitionedFile
    {

		#region Static Fields (3) 

        private static string[][] entryNames;
        private static Dictionary<string, long> locations;
        private static string[] sectionNames;

		#endregion Static Fields 

		#region Fields (3) 

        private const string filename = "WLDMES.BIN";
        private const int numberOfSections = 112;
        private const int sectionLength = 0xB000;

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
                    locations.Add( "WORLD/WLDMES.BIN", 0x00 );
                }

                return locations;
            }
        }

        public override int NumberOfSections { get { return numberOfSections; } }

        public override int SectionLength { get { return sectionLength; } }

        public override IList<string> SectionNames { get { return sectionNames; } }


		#endregion Properties 

		#region Constructors (2) 

        static WLDMES()
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

        public WLDMES( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

    }
}
