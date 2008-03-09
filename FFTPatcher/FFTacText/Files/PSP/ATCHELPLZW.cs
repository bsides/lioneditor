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

using System;
using System.Collections.Generic;

namespace FFTPatcher.TextEditor.Files.PSP
{
    public class ATCHELPLZW : BasePSPSectionedFile
    {

		#region Static Fields (3) 

        private static string[][] entryNames;
        private static Dictionary<string, long> locations;
        private static string[] sectionNames = new string[21] {
            "", "", "", "", "", "", 
            "", "", "", "", "", "Help", 
            "Job descriptions", "Item descriptions", "", "Ability descriptions", "", "",
            "", "Skillset descriptions", "", };

		#endregion Static Fields 

		#region Fields (1) 

        private const string filename = "ATCHELP.LZW";

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
                    locations.Add( "EVENT/ATCHELP.LZW", 0x00 );
                }
                return locations;
            }
        }

        public override int MaxLength
        {
            get { return 0x1F834; }
        }

        public override IList<string> SectionNames { get { return sectionNames; } }


		#endregion Properties 

		#region Constructors (3) 

        static ATCHELPLZW()
        {
            entryNames = new string[21][];

            int[] sectionLengths = new int[21] {
                1,1,1,1,1,1,1,
                1,1,1,1,40,169,316,
                1,512,1,1,1,227,1};

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
            IList<string> temp = new List<string>( FFTPatcher.Datatypes.AllJobs.PSPNames.Sub( 0, 168 ) );
            temp.AddRange( new string[sectionLengths[12] - temp.Count] );
            entryNames[12] = temp.ToArray();
            entryNames[13] = FFTPatcher.Datatypes.Item.PSPNames.ToArray();
            temp = new List<string>();
            temp.AddRange( new string[45] );
            temp.Add( FFTPatcher.Datatypes.AllAbilities.PSPNames[45] );
            temp.AddRange( new string[184 - 46] );
            temp.Add( FFTPatcher.Datatypes.AllAbilities.PSPNames[184] );
            temp.AddRange( new string[219 - 185] );
            temp.Add( FFTPatcher.Datatypes.AllAbilities.PSPNames[219] );
            temp.Add( FFTPatcher.Datatypes.AllAbilities.PSPNames[220] );
            temp.AddRange( new string[265 - 221] );
            temp.AddRange( FFTPatcher.Datatypes.AllAbilities.PSPNames.Sub( 265 ) );
            entryNames[15] = temp.ToArray();
            temp = new List<string>( FFTPatcher.Datatypes.SkillSet.PSPNames.Sub( 0, 0xAF ) );
            temp.AddRange( new string[48] );
            temp.AddRange( FFTPatcher.Datatypes.SkillSet.PSPNames.Sub( 0xE0, 0xE2 ) );
            entryNames[19] = temp.ToArray();
        }

        private ATCHELPLZW()
        {
        }

        public ATCHELPLZW( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

    }
}
