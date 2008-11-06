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
using System.Collections.ObjectModel;

namespace FFTPatcher
{
    using Paths = FFTPatcher.Resources.Paths.PSX;

    public static class PSXResources
    {


		#region Static Fields (7) 

        private static string[] abilityAI;
        private static string[] abilityAttributes;
        private static string[] abilityEffects;
        private static string[] abilityTypes;
        static Dictionary<string, object> dict = new Dictionary<string, object>();
        private static string[] shopAvailabilities;
        private static string[] statuses;

        private static ReadOnlyCollection<string> mapNamesReadOnly;
        private static string[] mapNames = new string[128] {
            "(No name)",
            "At main gate of Igros Castle",
            "Back gate of Lesalia Castle",
            "Hall of St. Murond Temple",
            "Office of Lesalia Castle",
            "Roof of Riovanes Castle",
            "At the gate of Riovanes Castle",
            "Inside of Riovanes Castle",
            "Riovanes Castle",
            "Citadel of Igros Castle",
            "Inside of Igros Castle",
            "Office of Igros Castle",
            "At the gate of Lionel Castle",
            "Inside of Lionel Castle",
            "Office of Lionel Castle",
            "At the gate of Limberry Castle",
            "Inside of Limberry Castle",
            "Underground cemetery of Limberry Castle",
            "Office of Limberry Castle",
            "At the gate of Limberry Castle",
            "Inside of Zeltennia Castle",
            "Zeltennia Castle",
            "Magic City Gariland",
            "Beoulve residence",
            "Military Academy's Auditorium",
            "Yardow Fort City",
            "Weapon storage of Yardow",
            "Goland Coal City",
            "Colliery underground First floor",
            "Colliery underground Second floor",
            "Colliery underground Third floor",
            "Dorter Trade City",
            "Slums in Dorter",
            "Hospital in slums",
            "Cellar of Sand Mouse",
            "Zaland Fort City",
            "Church outside the town",
            "Ruins outside Zaland",
            "Goug Machine City",
            "Underground passage in Goland",
            "Slums in Goug",
            "Besrodio's house",
            "Warjilis Trade City",
            "Port of Warjilis",
            "Bervenia Free City",
            "Ruins of Zeltennia Castle's church",
            "Cemetery of Heavenly Knight, Balbanes",
            "Zarghidas Trade City",
            "Slums of Zarghidas",
            "Fort Zeakden",
            "St. Murond Temple",
            "St. Murond Temple",
            "Chapel of St. Murond Temple",
            "Entrance to Death City",
            "Lost Sacred Precincts",
            "Graveyard of Airships",
            "Orbonne Monastery",
            "Underground Book Storage First Floor",
            "Underground Book Storage Second Floor",
            "Underground Book Storage Third Floor",
            "Underground Book Storage Fourth Floor",
            "Underground Book Storage Fifth Floor",
            "Chapel of Orbonne Monastery",
            "Golgorand Execution Site",
            "In front of Bethla Garrison's Sluice",
            "Granary of Bethla Garrison",
            "South Wall of Bethla Garrison",
            "North Wall of Bethla Garrison",
            "Bethla Garrison",
            "Murond Death City",
            "Nelveska Temple",
            "Dolbodar Swamp",
            "Fovoham Plains",
            "Inside of windmill Shed",
            "Sweegy Woods",
            "Bervenia Volcano",
            "Zeklaus Desert",
            "Lenalia Plateau",
            "Zigolis Swamp",
            "Yuguo Woods",
            "Araguay Woods",
            "Grog Hill",
            "Bed Desert",
            "Zirekile Falls",
            "Bariaus Hill",
            "Mandalia Plains",
            "Doguola Pass",
            "Bariaus Valley",
            "Finath River",
            "Poeskas Lake",
            "Germinas Peak",
            "Thieves Fort",
            "Igros·Beoulve residence",
            "Broke down shed·Wooden building",
            "Broke down shed·Stone building",
            "Church",
            "Pub",
            "Inside castle gate in Lesalia",
            "Outside castle gate in Lesalia",
            "Main street of Lesalia",
            "Public cemetary",
            "For tutorial 1",
            "For tutorial 2",
            "Windmill shed",
            "A room of Beoulve residence",
            "terminate",
            "delta",
            "nogias",
            "voyage",
            "bridge",
            "valkyries",
            "mlapan",
            "tiger",
            "horror",
            "end",
            "Banished fort",
            "(No name) -- Battle Arena",
            "(No name) -- Checkerboard Wall",
            "(No name) -- Checkerboard Wall ???",
            "(No name) -- Checkerboard Wall Waterland",
            "(Garbled name) -- Sloped Checkerboard",
            "","","","","","",""
            };
		#endregion Static Fields 

		#region Static Properties (31) 

        public static ReadOnlyCollection<string> MapNames
        {
            get
            {
                if ( mapNamesReadOnly == null )
                {
                    mapNamesReadOnly = new ReadOnlyCollection<string>( mapNames );
                }

                return mapNamesReadOnly;
            }
        }

        public static string Abilities { get { return dict[Paths.AbilitiesNamesXML] as string; } }

        public static byte[] AbilitiesBin { get { return dict[Paths.Binaries.Abilities] as byte[]; } }

        public static string AbilitiesStrings { get { return dict[Paths.AbilitiesStringsXML] as string; } }

        public static string[] AbilityAI
        {
            get
            {
                if( abilityAI == null )
                {
                    abilityAI =
                        Utilities.GetStringsFromNumberedXmlNodes(
                            AbilitiesStrings,
                            "/AbilityStrings/AI/string[@value='{0}']/@name",
                            24 );
                }
                return abilityAI;
            }
        }

        public static string[] AbilityAttributes
        {
            get
            {
                if( abilityAttributes == null )
                {
                    abilityAttributes =
                        Utilities.GetStringsFromNumberedXmlNodes(
                            AbilitiesStrings, 
                            "/AbilityStrings/Attributes/string[@value='{0}']/@name", 
                            32 );
                }
                return abilityAttributes;
            }
        }

        public static string[] AbilityEffects
        {
            get
            {
                if( abilityEffects == null )
                {
                    abilityEffects = Utilities.GetStringsFromNumberedXmlNodes(
                        dict[Paths.AbilityEffectsXML] as string,
                        "/Effects/Effect[@value='{0:X3}']/@name",
                        512 );
                }

                return abilityEffects;
            }
        }

        public static byte[] AbilityEffectsBin { get { return dict[Paths.Binaries.AbilityEffects] as byte[]; } }

        public static string[] AbilityTypes
        {
            get
            {
                if( abilityTypes == null )
                {
                    abilityTypes =
                        Utilities.GetStringsFromNumberedXmlNodes(
                            AbilitiesStrings,
                            "/AbilityStrings/Types/string[@value='{0}']/@name",
                            16 );
                }
                return abilityTypes;
            }
        }

        public static byte[] ActionEventsBin { get { return dict[Paths.Binaries.ActionEvents] as byte[]; } }

        public static byte[] ENTD1 { get { return dict[Paths.Binaries.ENTD1] as byte[]; } }

        public static byte[] ENTD2 { get { return dict[Paths.Binaries.ENTD2] as byte[]; } }

        public static byte[] ENTD3 { get { return dict[Paths.Binaries.ENTD3] as byte[]; } }

        public static byte[] ENTD4 { get { return dict[Paths.Binaries.ENTD4] as byte[]; } }

        public static byte[] MoveFind { get { return dict[Paths.Binaries.MoveFind] as byte[]; } }

        public static string EventNames { get { return dict[Paths.EventNamesXML] as string; } }

        public static byte[] FontBin { get { return dict[Paths.Binaries.Font] as byte[]; } }

        public static byte[] FontWidthsBin { get { return dict[Paths.Binaries.FontWidths] as byte[]; } }

        public static byte[] InflictStatusesBin { get { return dict[Paths.Binaries.InflictStatuses] as byte[]; } }

        public static string Items { get { return dict[Paths.ItemsXML] as string; } }

        public static string ItemsStrings { get { return dict[Paths.ItemsStringsXML] as string; } }

        public static byte[] JobLevelsBin { get { return dict[Paths.Binaries.JobLevels] as byte[]; } }

        public static string Jobs { get { return dict[Paths.JobsXML] as string; } }

        public static byte[] JobsBin { get { return dict[Paths.Binaries.Jobs] as byte[]; } }

        public static byte[] MonsterSkillsBin { get { return dict[Paths.Binaries.MonsterSkills] as byte[]; } }

        public static byte[] OldItemAttributesBin { get { return dict[Paths.Binaries.OldItemAttributes] as byte[]; } }

        public static byte[] OldItemsBin { get { return dict[Paths.Binaries.OldItems] as byte[]; } }

        public static byte[] PoachProbabilitiesBin { get { return dict[Paths.Binaries.PoachProbabilities] as byte[]; } }

        public static byte[] SCEAPDAT { get { return dict[Paths.Binaries.SCEAP] as byte[]; } }

        public static string[] ShopAvailabilities
        {
            get
            {
                if( shopAvailabilities == null )
                {
                    shopAvailabilities =
                        Utilities.GetStringsFromNumberedXmlNodes(
                            ItemsStrings,
                            "/ItemStrings/ShopAvailabilities/string[@value='{0}']/@name",
                            21 );
                }

                return shopAvailabilities;
            }
        }

        public static string SkillSets { get { return dict[Paths.SkillSetsXML] as string; } }

        public static byte[] SkillSetsBin { get { return dict[Paths.Binaries.SkillSets] as byte[]; } }

        public static string SpecialNames { get { return dict[Paths.SpecialNamesXML] as string; } }

        public static string SpriteSets { get { return dict[Paths.SpriteSetsXML] as string; } }

        public static byte[] StatusAttributesBin { get { return dict[Paths.Binaries.StatusAttributes] as byte[]; } }

        public static string[] Statuses
        {
            get
            {
                if( statuses == null )
                {
                    statuses = Utilities.GetStringsFromNumberedXmlNodes(
                        StatusNames,
                        "/Statuses/Status[@offset='{0}']/@name",
                        40 );
                }

                return statuses;
            }
        }

        public static string StatusNames { get { return dict[Paths.StatusNamesXML] as string; } }

		#endregion Static Properties 

		#region Constructors (1) 

        static PSXResources()
        {
            dict[Resources.Paths.PSX.Binaries.ENTD1] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.ENTD1];
            dict[Resources.Paths.PSX.Binaries.ENTD2] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.ENTD2];
            dict[Resources.Paths.PSX.Binaries.ENTD3] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.ENTD3];
            dict[Resources.Paths.PSX.Binaries.ENTD4] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.ENTD4];
            dict[Resources.Paths.PSX.Binaries.MoveFind] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.MoveFind];
            dict[Resources.Paths.PSX.Binaries.Abilities] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.Abilities];
            dict[Resources.Paths.PSX.Binaries.AbilityEffects] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.AbilityEffects];
            dict[Resources.Paths.PSX.Binaries.ActionEvents] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.ActionEvents];
            dict[Resources.Paths.PSX.Binaries.Font] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.Font];
            dict[Resources.Paths.PSX.Binaries.FontWidths] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.FontWidths];
            dict[Resources.Paths.PSX.Binaries.InflictStatuses] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.InflictStatuses];
            dict[Resources.Paths.PSX.Binaries.JobLevels] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.JobLevels];
            dict[Resources.Paths.PSX.Binaries.Jobs] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.Jobs];
            dict[Resources.Paths.PSX.Binaries.MonsterSkills] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.MonsterSkills];
            dict[Resources.Paths.PSX.Binaries.OldItemAttributes] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.OldItemAttributes];
            dict[Resources.Paths.PSX.Binaries.OldItems] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.OldItems];
            dict[Resources.Paths.PSX.Binaries.PoachProbabilities] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.PoachProbabilities];
            dict[Resources.Paths.PSX.Binaries.SkillSets] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.SkillSets];
            dict[Resources.Paths.PSX.Binaries.StatusAttributes] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.StatusAttributes];
            dict[Resources.Paths.PSX.EventNamesXML] = Resources.ZipFileContents[Resources.Paths.PSX.EventNamesXML].ToUTF8String();
            dict[Resources.Paths.PSX.JobsXML] = Resources.ZipFileContents[Resources.Paths.PSX.JobsXML].ToUTF8String();
            dict[Resources.Paths.PSX.SkillSetsXML] = Resources.ZipFileContents[Resources.Paths.PSX.SkillSetsXML].ToUTF8String();
            dict[Resources.Paths.PSX.SpecialNamesXML] = Resources.ZipFileContents[Resources.Paths.PSX.SpecialNamesXML].ToUTF8String();
            dict[Resources.Paths.PSX.SpriteSetsXML] = Resources.ZipFileContents[Resources.Paths.PSX.SpriteSetsXML].ToUTF8String();
            dict[Resources.Paths.PSX.StatusNamesXML] = Resources.ZipFileContents[Resources.Paths.PSX.StatusNamesXML].ToUTF8String();
            dict[Resources.Paths.PSX.AbilitiesNamesXML] = Resources.ZipFileContents[Resources.Paths.PSX.AbilitiesNamesXML].ToUTF8String();
            dict[Resources.Paths.PSX.AbilitiesStringsXML] = Resources.ZipFileContents[Resources.Paths.PSX.AbilitiesStringsXML].ToUTF8String();
            dict[Resources.Paths.PSX.AbilityEffectsXML] = Resources.ZipFileContents[Resources.Paths.PSX.AbilityEffectsXML].ToUTF8String();
            dict[Resources.Paths.PSX.ItemAttributesXML] = Resources.ZipFileContents[Resources.Paths.PSX.ItemAttributesXML].ToUTF8String();
            dict[Resources.Paths.PSX.ItemsXML] = Resources.ZipFileContents[Resources.Paths.PSX.ItemsXML].ToUTF8String();
            dict[Resources.Paths.PSX.ItemsStringsXML] = Resources.ZipFileContents[Resources.Paths.PSX.ItemsStringsXML].ToUTF8String();
            dict[Resources.Paths.PSX.Binaries.SCEAP] = Resources.ZipFileContents[Resources.Paths.PSX.Binaries.SCEAP];
        }

		#endregion Constructors 

    }
}