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
using FFTPatcher.Datatypes;

namespace FFTPatcher.TextEditor.Files.PSX
{
    public class OPENLZW : AbstractStringSectioned
    {
        protected override int NumberOfSections { get { return 32; } }

        private static Dictionary<string, long> locations;
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

        private static OPENLZW Instance { get; set; }
        private static string[] sectionNames = new string[32] {
            "", "", "", "", "", "", "", "", 
            "Unit names", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "Birthday",
            "Track names", "Composers comments", "", "", "", "", "", "" };

        private static string[][] entryNames;

        public override IList<string> SectionNames { get { return sectionNames; } }
        public override IList<IList<string>> EntryNames { get { return entryNames; } }
        public override int MaxLength { get { return 0x5579; } }

        static OPENLZW()
        {
            Instance = new OPENLZW( Properties.Resources.OPEN_LZW );

            entryNames = new string[32][];
            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[Instance.Sections[i].Count];
            }
        }

        private OPENLZW( IList<byte> bytes )
            : base( bytes )
        {
        }

        public static OPENLZW GetInstance()
        {
            return Instance;
        }
    }
}
