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
    public class BOOT2A1630 : BasePSPSectionedFile
    {

		#region Static Fields (3) 

        private static string[][] entryNames;
        private static Dictionary<string, long> locations;
        private static string[] sectionNames;

		#endregion Static Fields 

		#region Fields (1) 

        private const string filename = "BOOT.BIN[0x2A1630]";

		#endregion Fields 

		#region Properties (6) 


        protected override int NumberOfSections
        {
            get { return 21; }
        }

        public override IList<IList<string>> EntryNames { get { return entryNames; } }

        public override string Filename { get { return filename; } }

        public override IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "BOOT.BIN", 0x2A1630 );
                }
                return locations;
            }
        }

        public override int MaxLength
        {
            get { return 0x35C68; }
        }

        public override IList<string> SectionNames { get { return sectionNames; } }


		#endregion Properties 

		#region Constructors (3) 

        static BOOT2A1630()
        {
            sectionNames = new string[21] {
                "", "Help", "Menu/Options help", "", "", "", "", "", "", "",
                "", "", "Job descriptions", "Item descriptions", "", "Ability descriptions", "Location descriptions", "Location descriptions", "", "Skillset descriptions",
                "Unit quotes" };

            entryNames = new string[21][];

            int[] sectionLengths = new int[21] {
                1,50,181,2,31,1,1,
                1,1,1,1,1,169,316,
                1,512,45,116,1,227,768 };
            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[sectionLengths[i]];
            }
            entryNames[1] = new string[50] {
                "Unit #", "Level", "HP", "MP", "CT", "AT", "Exp", "Name",
                "Brave", "Faith", "", "Move", "Jump", "", "", "",
                "Speed", "ATK", "Weapon ATK", "", "Eva%", "SEv%", "AEv%", "Phys land effect",
                "Magic land effect", "Estimated", "Hit rate", "Aries", "Taurus", "Gemini", "Cancer", "Leo", 
                "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces", "Serpentarius",
                "Add", "Don't add", "Details", "Dismiss", "Cancel dismiss", "Move", "Action", "End turn", "View status","AI" };
            entryNames[12] = FFTPatcher.Datatypes.AllJobs.PSPNames.Sub(0, 168).ToArray();
            entryNames[13] = FFTPatcher.Datatypes.Item.PSPNames.ToArray();
            entryNames[15] = FFTPatcher.Datatypes.AllAbilities.PSPNames.ToArray();
            List<string> temp = new List<string>( FFTPatcher.Datatypes.SkillSet.PSPNames.Sub( 0, 0xAF ) );
            temp.AddRange( new string[48] );
            temp.AddRange( FFTPatcher.Datatypes.SkillSet.PSPNames.Sub( 0xE0, 0xE2 ) );
            entryNames[19] = temp.ToArray();
        }

        private BOOT2A1630()
        {
        }

        public BOOT2A1630( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

    }
}
