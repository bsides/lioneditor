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
    public class SAMPLELZW : AbstractStringSectioned
    {
        private static Dictionary<string, long> locations;
        public override IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "EVENT/SAMPLE.LZW", 0x00 );
                    locations.Add( "BATTLE.BIN", 0x0FA2DC );
                    locations.Add( "WORLD/WORLD.BIN", 0x06E5C0 );
                }

                return locations;
            }
        }

        protected override int NumberOfSections { get { return 24; } }

        private static SAMPLELZW Instance { get; set; }
        private static string[] sectionNames = new string[24] { 
            "Empty", "Empty", "Error/Battle Messages", "Battle messages",
            "Empty", "Empty", "Job names", "Item names",
            "Empty (spaces)", "Japanese text", "Battle menus", "Empty (spaces)",
            "Empty", "Empty", "Ability names", "Empty", 
            "Battle messages", "Status effects", "Misc. messages", "Empty",
            "Empty", "Empty", "Skillset names", "Summons/Draw Out"};

        public static string[][] entryNames;

        public override IList<string> SectionNames { get { return sectionNames; } }
        public override IList<IList<string>> EntryNames { get { return entryNames; } }

        public override int MaxLength { get { return 0x4B88; } }

        static SAMPLELZW()
        {
            Instance = new SAMPLELZW( Properties.Resources.SAMPLE_LZW );
            entryNames = new string[24][];
            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[Instance.Sections[i].Count];
            }

            entryNames[0] = new string[] { "" };
            entryNames[1] = new string[] { "" };
            entryNames[2] = new string[38] { 
                "","Don't move","Don't act","Underwater","Out of range","Unusable abilities",
                "No combos","Frog","Insufficient MP","Silence","No units in range","Out of stock",
                "Out of stock (Throw)","Out of stock (Draw out)","","Out of gun range","Can't target self ","Can't target ally",
                "Select target","Can't target panel","Can't target ally","Select target to protect","Can't target enemy","Learn ability?",
                "Inherit abilities?","Restore HP/MP?","Received item","Received item (full stock)","Discovered item","Discovered item (full stock)",
                "","Add party member?","Equipment sold","Party full","Select unit to dismiss","","Don't have Geomancy",
                "Can't move" };
            entryNames[3] = new string[80] {
                "","Item broke","Stole gil","Stole item","Invited","Stole XP","Unit joined","WT=0",
                "Brave++","Brave--","Faith++","Faith--","Death sentence","Got gil","Berserk","Asleep",
                "Destroyed item","MP--","Speed--","ATK--","MAG--","ATK++","","Speed++",
                "","Max power","Sealed movement","Killed","","Barely survived","Sealed Act","Sealed Move",
                "Caused Silence","","","Vampire","Oil","","","Sealed",
                "Tamed","ATK++","Confused","Malboro","ATK++","MAG++","Speed++","Damage=gil",
                "Degenerator","Deathtrap","Sleeping gas","Steel needle","","","Found item","",
                "Successful poach","","Failed mimic","Failed teleport","Unit dropped treasure","Unit became crystal","Job level up","Level up",
                "","","","","","","","","","","","","","","","" };
            entryNames[4] = new string[] { "" };
            entryNames[5] = new string[] { "" };
            entryNames[6] = new SubArray<string>( FFTPatcher.Datatypes.AllJobs.PSXNames, 0, 154 ).ToArray();
            IList<string> temp = new List<string>( new SubArray<string>( FFTPatcher.Datatypes.Item.PSXNames, 0 ).ToArray() );
            temp.AddRange( new string[Instance.Sections[7].Count - temp.Count] );
            entryNames[7] = temp.ToArray();
            temp = new List<string>( AllAbilities.PSXNames );
            temp.AddRange( new string[Instance.Sections[14].Count - temp.Count] );
            entryNames[14] = temp.ToArray();
            entryNames[16] = new string[] {
                "","Charging","Performing","Move","Ride chocobo","Inherit crystal","Move confirmation",
                "Select combo","Combo target","Select magic","Insufficient MP","Select target","Select target","Confirm attack","Confirm protect",
                "Set direction","Select chargeup ability","Confirm chargeup ability","Confirm action","Silence" };
            entryNames[17] = PSXResources.Statuses;
            entryNames[18] = new string[31] {
                "No items", "Item out of stock", "No more shields", "No unequipped items", "Set number to dispose", "Cancel equip?", "No ability learned", "Learned all abilities",
                "Is __ OK?", "Insufficient JP", "Remove __?", "Team not ready", "Team full", "Can't delete leader", "No memory card", "No saves",
                "No room on memory card", "Save?", "Can't remove unit", "Format memory card?", "Input birthday", "Input name", "Kanji search method", "First Kanji", 
                "Kanji strokes", "Input name", "Is __ OK?", "Destroy egg?", "Save error", "Load error", "Save?" };
            temp = new List<string>( new SubArray<string>( SkillSet.PSXNames, 0, 175 ) );
            temp.AddRange( new string[Instance.Sections[22].Count - temp.Count] );
            entryNames[22] = temp.ToArray();

            entryNames[23] = new string[27] {
                "Mogri","Shiva","Ramuh","Ifrit","Titan","Golem","Carbunkle","Bahamut",
                "Odin","Leviathan","Salamander","Silf","Fairy","Rich","Clops","Zodiac",
                "Asura","Koutetsu","Bizen Boat","Murasame","Heaven's Cloud","Kiyomori","Muramasa","Kikuichimoji",
                "Masamune","Chirijiraden","" };
        }

        private SAMPLELZW( IList<byte> bytes )
            : base( bytes )
        {
        }

        public static SAMPLELZW GetInstance()
        {
            return Instance;
        }
    }
}
