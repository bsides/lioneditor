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
    public class OPENLZW : BasePSPSectionedFile
    {

		#region Static Fields (3) 

        private static string[][] entryNames;
        private static Dictionary<string, long> locations;
        private static string[] sectionNames;

		#endregion Static Fields 

		#region Fields (1) 

        private const string filename = "OPEN.LZW";

		#endregion Fields 

		#region Properties (6) 


        protected override int NumberOfSections { get { return 32; } }

        public override IList<IList<string>> EntryNames { get { return entryNames; } }

        public override string Filename { get { return filename; } }

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

        public override int MaxLength
        {
            get { return 0x608D; }
        }

        public override IList<string> SectionNames { get { return sectionNames; } }


		#endregion Properties 

		#region Constructors (2) 

        static OPENLZW()
        {
            sectionNames = new string[32] {
                "", "", "", "", "", "", "", "", 
                "Unit names", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "Birthday",
                "Track names", "Composers comments", "", "", "", "", "", "" };            
            entryNames = new string[32][];

            int[] sectionLengths = new int[32] {
                1,1,1,1,1,1,1,1,
                1024,1,1,1,1,1,1,1,
                1,1,1,1,1,1,1,16,
                97,96,1,1,1,1,1,12};
            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[sectionLengths[i]];
            }
        }

        public OPENLZW( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

    }
}
