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
    public class BOOT28E5EC : AbstractBootBinFile
    {

		#region Static Fields (4) 

        private static string[][] entryNames;
        private static readonly IDictionary<int, IList<int>> exclusions;
        private static Dictionary<string, long> locations;
        private static string[] sectionNames;

		#endregion Static Fields 

		#region Fields (1) 

        private const string filename = "BOOT.BIN[0x28E5EC]";

		#endregion Fields 

		#region Properties (6) 


        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected override int NumberOfSections
        {
            get { return 24; }
        }

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
                    locations.Add( "BOOT.BIN", 0x28E5EC );
                    locations.Add( "BOOT.BIN ", 0x30A198 );
                }
                return locations;
            }
        }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        /// <value></value>
        public override int MaxLength
        {
            get { return 0x580A; }
        }

        /// <summary>
        /// Gets a collection of strings with a description of each section in this file.
        /// </summary>
        /// <value></value>
        public override IList<string> SectionNames { get { return sectionNames; } }


		#endregion Properties 

		#region Constructors (3) 

        static BOOT28E5EC()
        {
            sectionNames = new string[24] {
                "", "", "Battle messages", "Battle messages", "", "",
                "Job names", "Item names", "Blank spaces", "Terrain names", "Battle menus", "Blank spaces",
                "", "", "Ability names", "", "Battle messages", "Status names", 
                "Misc. messages", "", "", "", "Skillset names", "Summon/Iaido" };
            entryNames = new string[24][];

            int[] sectionLengths = new int[24] {
                1,1,39,80,1,1,169,316,
                26,64,20,26,1,1,512,1,
                20,40,76,1,1,1,227,27 };
            for( int i = 0; i < 24; i++ )
            {
                entryNames[i] = new string[sectionLengths[i]];
            }
            entryNames[2] = new string[39] { 
                "","Don't move","Don't act","Underwater","Out of range","Unusable abilities",
                "No combos","Toad","Insufficient MP","Silence","No units in range","Out of stock",
                "Out of stock (Throw)","Out of stock (Draw out)","","Out of gun range","Can't target self ","Can't target ally",
                "Select target","Can't target panel","Can't target ally","Select target to protect","Can't target enemy","Learn ability?",
                "Inherit abilities?","Restore HP/MP?","Received item","Received item (full stock)","Discovered item","Discovered item (full stock)",
                "","Add party member?","Equipment sold","Party full","Select unit to dismiss","","Don't have Geomancy",
                "Can't move", "Arithmeticks not allowed" };
            entryNames[3] = new string[80] {
                "","Item broke","Stole item","Stole gil","Invited","Stole XP","Unit joined","WT=0",
                "Brave++","Brave--","Faith++","Faith--","Death sentence","Got gil","Berserk","Asleep",
                "Destroyed item","MP--","Speed--","ATK--","MAG--","ATK++","","Speed++",
                "","Max power","Sealed movement","Killed","","Barely survived","Sealed Act","Sealed Move",
                "Caused Silence","","","Vampire","Oil","","","Sealed",
                "Tamed","ATK++","Confused","Malboro","ATK++","MAG++","Speed++","Damage=gil",
                "Degenerator","Deathtrap","Sleeping gas","Steel needle","","","Found item","",
                "Successful poach","","Failed mimic","Failed teleport","Unit dropped treasure","Unit became crystal","Job level up","Level up",
                "","","","","","","","","","","","","","","","" };
            entryNames[6] = FFTPatcher.Datatypes.AllJobs.PSPNames;
            entryNames[7] = FFTPatcher.Datatypes.Item.PSPNames.ToArray();
            entryNames[14] = FFTPatcher.Datatypes.AllAbilities.PSPNames;
            entryNames[16] = new string[] {
                "","Charging","Performing","Move","Ride chocobo","Inherit crystal","Move confirmation",
                "Select combo","Combo target","Select magic","Insufficient MP","Select target","Select target","Confirm attack","Confirm protect",
                "Set direction","Select chargeup ability","Confirm chargeup ability","Confirm action","Silence" };
            entryNames[17] = FFTPatcher.Resources.Statuses;
            entryNames[22] = FFTPatcher.Datatypes.SkillSet.PSPNames;
            entryNames[23] = new string[27] {
                "Moogle","Shiva","Ramuh","Ifrit","Titan","Golem","Carbuncle","Bahamut",
                "Odin","Leviathan","Salamander","Sylph","Faerie","Lich","Cyclops","Zodiark",
                "Ashura","Kotetsu","Osafune","Murasame","Ama-no-Murakumo","Kiyomori","Muramasa","Kiku-ichimonji",
                "Masamune","Chirijiraden","" };

            exclusions = new Dictionary<int, IList<int>>();
            exclusions.Add( 10, new int[] { 0, 10 } );
        }

        private BOOT28E5EC()
        {
        }

        public BOOT28E5EC( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

    }
}
