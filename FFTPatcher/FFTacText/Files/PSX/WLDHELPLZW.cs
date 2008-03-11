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
    public class WLDHELPLZW : BasePSXCompressedFile
    {

		#region Static Fields (3) 

        public static string[][] entryNames;
        private static Dictionary<string, long> locations;
        private static string[] sectionNames;

		#endregion Static Fields 

		#region Fields (2) 

        private const string filename = "WLDHELP.LZW";
        private const int numberOfSections = 21;

		#endregion Fields 

		#region Properties (6) 


        protected override int NumberOfSections { get { return numberOfSections; } }

        public override IList<IList<string>> EntryNames { get { return entryNames; } }

        public override string Filename { get { return filename; } }

        public override IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "EVENT/WLDHELP.LZW", 0x00 );
                    locations.Add( "WORLD/WORLD.BIN", 0x8EE68 );
                }

                return locations;
            }
        }

        public override int MaxLength { get { return 0x01ADE4; } }

        public override IList<string> SectionNames { get { return sectionNames; } }


		#endregion Properties 

		#region Constructors (3) 

        static WLDHELPLZW()
        {
            sectionNames = new string[21] {
                "","Help", "Menu/Options Help", "", "Error/Confirmation messages", "", "",
                "","","","","","Job descriptions", "Item descriptions",
                "","Ability descriptions","Location descriptions","Location descriptions","","Skillset descriptions","Unit quotes"};
            entryNames = new string[numberOfSections][];

            int[] sectionLengths = new int[numberOfSections] {
                1,40,155,1,31,1,1,
                1,1,1,1,1,160,256,
                1,512,44,116,1,188,768 };

            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[sectionLengths[i]];
            }

            entryNames[1] = new string[40] {
                "Unit #", "Level", "HP", "MP", "CT", "AT", "Exp", "Name",
                "Brave", "Faith", "", "Move", "Jump", "", "", "",
                "Speed", "ATK", "Weapon ATK", "", "Eva%", "SEv%", "AEv%", "Phys land effect",
                "Magic land effect", "Estimated", "Hit rate", "Aries", "Taurus", "Gemini", "Cancer", "Leo", 
                "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces", "Serpentarius" };
            entryNames[4] = new string[31] {
                "No items", "Item out of stock", "No more shields", "No unequipped items", "Set number to dispose", "Cancel equip?", "No ability learned", "Learned all abilities",
                "Is __ OK?", "Insufficient JP", "Remove __?", "Team not ready", "Team full", "Can't delete leader", "No memory card", "No saves",
                "No room on memory card", "Save?", "Can't remove unit", "Format memory card?", "Input birthday", "Input name", "Kanji search method", "First Kanji", 
                "Kanji strokes", "Input name", "Is __ OK?", "Destroy egg?", "Save error", "Load error", "Save?" };
            entryNames[12] = AllJobs.PSXNames;
            entryNames[13] = Item.PSXNames.ToArray();
            entryNames[15] = AllAbilities.PSXNames;
            entryNames[16] = new string[44] {
                "Lesalia Imperial Capital", "Riovanes Castle", "Igros Castle", "Lionel Castle", "Limberry Castle", "Zeltennia Castle", "Magic City Gariland",
                "Yardow Fort City", "Goland Coal City", "Dorter Trade City", "Zaland Fort City", "Goug Machine City", "Warjils Trade City", "Bervenia Free City",
                "Zarghidas Trade City", "Fort Zeakden", "Murond Holy Place", "Thieves Fort", "Orbonne Monastery", "Golgorand Execution Site", "Murond Death City",
                "Bethla Garrison", "Deep Dungeon", "Nelveska Temple", "Mandalia Plains", "Fovoham Plains", "Sweegy Woods", "Bervenia Volcano", 
                "Zeklaus Desert", "Lenalia Plateau", "Zigolis Swamp", "Yuguo Woods", "Araguay Woods", "Grog Hill", "Bed Desert", 
                "Zirekile Falls", "Dolbodar Swamp", "Bariaus Hill", "Doguola Pass", "Bariaus Valley", "Finath River", "Poeskas Lake",
                "Germinas Peak", "" };
            List<string> temp = new List<string>( 188 );
            temp.AddRange( SkillSet.PSXNames.Sub( 0, 175 ) );
            temp.AddRange( new string[12] );

            entryNames[19] = temp.ToArray();
        }

        private WLDHELPLZW()
        {
        }

        public WLDHELPLZW( IList<byte> bytes )
        {
            Sections = new List<IList<string>>( NumberOfSections );
            for( int i = 0; i < NumberOfSections; i++ )
            {
                uint start = Utilities.BytesToUInt32( bytes.Sub( i * 4, i * 4 + 3 ) );
                uint stop = Utilities.BytesToUInt32( bytes.Sub( (i + 1) * 4, (i + 1) * 4 + 3 ) ) - 1;
                if( i == NumberOfSections - 1 )
                {
                    stop = (uint)bytes.Count - 1 - dataStart;
                }

                IList<byte> thisSection = TextUtilities.Decompress(
                    bytes,
                    bytes.Sub( (int)(start + dataStart), (int)(stop + dataStart) ),
                    (int)(start + dataStart) );
                Sections.Add( TextUtilities.ProcessList( thisSection, CharMap ) );
            }
        }

		#endregion Constructors 

		#region Methods (1) 


        protected override IList<byte> ToFinalBytes()
        {
            return Compress();
        }


		#endregion Methods 

    }
}