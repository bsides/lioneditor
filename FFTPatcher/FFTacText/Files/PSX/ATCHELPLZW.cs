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
    public class ATCHELPLZW : BasePSXSectionedFile
    {
        protected override int NumberOfSections { get { return 21; } }

        private static Dictionary<string, long> locations;
        public override IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "EVENT/ATCHELP.LZW", 0x00 );
                }

                return locations;
            }
        }

        private static string[] sectionNames = new string[21] {
            "", "", "", "", "", "", 
            "", "", "", "", "", "Help", 
            "Job descriptions", "Item descriptions", "", "Ability descriptions", "", "",
            "", "Skillset descriptions", "" };

        private static string[][] entryNames;

        public override IList<string> SectionNames { get { return sectionNames; } }
        public override IList<IList<string>> EntryNames { get { return entryNames; } }
        public override int MaxLength { get { return 0x0160D5; } }

        static ATCHELPLZW()
        {
            entryNames = new string[21][];
            int[] sectionLengths = new int[] { 
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1,
                1, 40, 160, 256, 1, 
                512, 1, 1, 1, 188,
                1 };

            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[sectionLengths[i]];
            }
            entryNames[11] = new string[40] {
                "Unit #", "Level", "HP", "MP", "CT", "AT", "Exp", "Name",
                "Brave", "Faith", "", "Move", "Jump", "", "", "",
                "Speed", "ATK", "Weapon ATK", "", "Eva%", "SEv%", "AEv%", "Phys land effect",
                "Magic land effect", "Estimated", "Hit rate", "Aries", "Taurus", "Gemini", "Cancer", "Leo", 
                "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces", "Serpentarius" };
            IList<string> temp = new List<string>( FFTPatcher.Datatypes.AllJobs.PSXNames.Sub( 0, 154 ) );
            temp.AddRange( new string[sectionLengths[12] - temp.Count] );
            entryNames[12] = temp.ToArray();
            entryNames[13] = FFTPatcher.Datatypes.Item.PSXNames.ToArray();
            temp = new List<string>( new string[265] );
            temp.AddRange( FFTPatcher.Datatypes.AllAbilities.PSXNames.Sub( 265 ) );
            entryNames[15] = temp.ToArray();
            temp = new List<string>( FFTPatcher.Datatypes.SkillSet.PSXNames.Sub( 0, 0xAF ) );
            temp.AddRange( new string[12] );
            entryNames[19] = temp.ToArray();
        }

        public ATCHELPLZW( IList<byte> bytes )
            : base( bytes )
        {
        }
    }
}
