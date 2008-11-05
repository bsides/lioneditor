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

        public static string Abilities { get { return dict["Abilities"] as string; } }

        public static byte[] AbilitiesBin { get { return dict["AbilitiesBin"] as byte[]; } }

        public static string AbilitiesStrings { get { return dict["AbilitiesStrings"] as string; } }

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
                        dict["AbilitiesEffects"] as string,
                        "/Effects/Effect[@value='{0:X3}']/@name",
                        512 );
                }

                return abilityEffects;
            }
        }

        public static byte[] AbilityEffectsBin { get { return dict["AbilitiesEffectsBin"] as byte[]; } }

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

        public static byte[] ActionEventsBin { get { return dict["ActionEventsBin"] as byte[]; } }

        public static string EventNames { get { return dict["EventNames"] as string; } }

        public static byte[] FontBin { get { return dict["FontBin"] as byte[]; } }

        public static byte[] FontWidthsBin { get { return dict["FontWidthsBin"] as byte[]; } }

        public static byte[] InflictStatusesBin { get { return dict["InflictStatusesBin"] as byte[]; } }

        public static string Items { get { return dict["Items"] as string; } }

        public static string ItemsStrings { get { return dict["ItemsStrings"] as string; } }

        public static byte[] JobLevelsBin { get { return dict["JobLevelsBin"] as byte[]; } }

        public static string Jobs { get { return dict["Jobs"] as string; } }

        public static byte[] JobsBin { get { return dict["JobsBin"] as byte[]; } }

        public static byte[] MonsterSkillsBin { get { return dict["MonsterSkillsBin"] as byte[]; } }

        public static byte[] OldItemAttributesBin { get { return dict["OldItemAttributesBin"] as byte[]; } }

        public static byte[] OldItemsBin { get { return dict["OldItemsBin"] as byte[]; } }

        public static byte[] PoachProbabilitiesBin { get { return dict["PoachProbabilitiesBin"] as byte[]; } }

        public static byte[] SCEAPDAT { get { return dict["SCEAP"] as byte[]; } }

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

        public static string SkillSets { get { return dict["SkillSets"] as string; } }

        public static byte[] SkillSetsBin { get { return dict["SkillSetsBin"] as byte[]; } }

        public static string SpecialNames { get { return dict["SpecialNames"] as string; } }

        public static string SpriteSets { get { return dict["SpriteSets"] as string; } }

        public static byte[] StatusAttributesBin { get { return dict["StatusAttributesBin"] as byte[]; } }

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

        public static string StatusNames { get { return dict["StatusNames"] as string; } }

        public static byte[] MoveFind { get { return Properties.Resources.MoveFind; } }

		#endregion Static Properties 

		#region Constructors (1) 

        static PSXResources()
        {
            dict["AbilitiesBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.AbilitiesBin );
            dict["ActionEventsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.ActionEventsBin );
            dict["FontBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.FontBin );
            dict["FontWidthsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.FontWidthsBin );
            dict["InflictStatusesBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.InflictStatusesBin );
            dict["JobLevelsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.JobLevelsBin );
            dict["JobsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.JobsBin );
            dict["MonsterSkillsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.MonsterSkillsBin );
            dict["StatusAttributesBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.StatusAttributesBin );
            dict["OldItemAttributesBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.OldItemAttributesBin );
            dict["OldItemsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.OldItemsBin );
            dict["SkillSetsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.SkillSetsBin );
            dict["PoachProbabilitiesBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.PoachProbabilitiesBin );

            dict["SkillSets"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.SkillSets ).ToUTF8String();
            dict["Abilities"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.Abilities ).ToUTF8String();
            dict["AbilitiesStrings"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.AbilitiesStrings ).ToUTF8String();
            dict["EventNames"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.EventNames ).ToUTF8String();
            dict["Items"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.Items ).ToUTF8String();
            dict["ItemsStrings"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.ItemsStrings ).ToUTF8String();
            dict["Jobs"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.Jobs ).ToUTF8String();
            dict["SpecialNames"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.SpecialNames ).ToUTF8String();
            dict["SpriteSets"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.SpriteSets ).ToUTF8String();
            dict["StatusNames"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.StatusNames ).ToUTF8String();

            dict["AbilitiesEffects"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.AbilitiesEffects ).ToUTF8String();
            dict["AbilitiesEffectsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.AbilityEffectsBin );

            dict["SCEAP"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.SCEAP );
        }

		#endregion Constructors 

    }
}