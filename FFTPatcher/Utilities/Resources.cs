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
using System.Xml;
using System.Collections.ObjectModel;

namespace FFTPatcher
{
    public static class Resources
    {

		#region Static Fields (9) 

        private static string[] abilityAI;
        private static string[] abilityAttributes;
        private static string[] abilityEffects;
        private static Dictionary<byte, string> abilityFormulas;
        private static string[] abilityTypes;
        static Dictionary<string, object> dict = new Dictionary<string, object>();
        private static Dictionary<int, string> fftPackFiles;
        private static string[] shopAvailabilities;
        private static string[] statuses;

        private static ReadOnlyCollection<string> mapNamesReadOnly;
        private static string[] mapNames = new string[128] {
            "",
            "Eagrose Castle Gate",
            "Lesalia Castle Postern",
            "Mullonde Cathedral Nave",
            "Office of Lesalia Castle",
            "Riovanes Castle Roof",
            "Riovanes Castle Gate",
            "Riovanes Castle Keep",
            "Riovanes Castle",
            "Citadel of Igros Castle",
            "Eagrose Castle Keep",
            "Eagrose Castle Solar",
            "Lionel Castle Gate",
            "Lionel Castle Oratory",
            "Lionel Castle Parlor",
            "Limberry Castle Gate",
            "Limberry Castle Keep",
            "Limberry Castle Undercroft",
            "Limberry Castle Parlor",
            "Limberry Castle Gate",
            "Zeltennia Castle Keep",
            "Zeltennia Castle",
            "Gariland",
            "The Beoulve Manse",
            "The Royal Military Akademy At Gariland",
            "Yardrow",
            "Yardrow Armory",
            "Gollund",
            "Gollund Colliery Ridge",
            "Gollund Colliery Slope",
            "Gollund Colliery Floor",
            "Dorter",
            "Dorter Slums",
            "Hospital in Slums",
            "The Sand Rat's Sietch",
            "Zaland",
            "Outlying Church",
            "Ruins outside Zaland",
            "Goug",
            "Golland Coal Shaft",
            "Goug Lowtown",
            "Bunansa Residence",
            "Warjilis",
            "Warjilis Harbor",
            "Bervenia",
            "Zeltennia Castle Chapel Ruins",
            "The Tomb of Barbaneth Beoulve",
            "Sal Ghidos",
            "Sal Ghidos Slumtown",
            "Ziekden Fortress",
            "Mullonde Cathedral",
            "Mullonde Cathedral",
            "Mullonde Cathedral Sanctuary",
            "The Necrohol Gate",
            "Lost Halidom",
            "Airship Graveyard",
            "Orbonne Monastery",
            "Monastery Vaults: First Level",
            "Monastery Vaults: Second Level",
            "Monastery Vaults: Third Level",
            "Monastery Vaults: Fourth Level",
            "Monastery Vaults: Fifth Level",
            "Orbonne Monastery",
            "Golgollada Gallows",
            "Fort Besselat Sluice",
            "Fort Besselat Granary",
            "Fort Besselat: South Wall",
            "Fort Besselat: North Wall",
            "Fort Besselat",
            "The Necrohol of Mullonde",
            "Nelveska Temple",
            "Dorvauldar Marsh",
            "Fovoham Windflats",
            "Mill Interior",
            "The Siedge Weald",
            "Mount Bervenia",
            "Zeklaus Desert",
            "Lenalian Plateau",
            "Tchigolith Fenlands",
            "The Yuguewood",
            "Araguay Woods",
            "Grogh Heights",
            "Beddha Sandwaste",
            "Zeirchele Falls",
            "Balias Tor",
            "Mandalia Plain",
            "Dugeura Pass",
            "Balias Swale",
            "Finnath Creek",
            "Lake Poeskas",
            "Mount Germinas",
            "Brigands' Den",
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
            "Windflat Mill",
            "The Beoulve Manse: In the waning days of the Fifty Years' War",
            "The Stair",
            "The Hollow",
            "The Cravasse",
            "The Switchback",
            "The Crossing",
            "The Catacombs",
            "The Oubliette",
            "The Palings",
            "The Interstice",
            "Terminus",
            "Abandoned Watchtower",
            "(No name) -- Battle Arena",
            "(No name) -- Checkerboard Wall",
            "(No name) -- Checkerboard Wall ???",
            "(No name) -- Checkerboard Wall Waterland",
            "(Garbled name) -- Sloped Checkerboard",
            "",
            "",
            "",
            "",
            "",
            "Zeltennia Castle Postern",
            "Limberry Castle: Inner Court"
            };

		#endregion Static Fields 

		#region Static Properties (39) 

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

        public static Dictionary<byte, string> AbilityFormulas
        {
            get
            {
                if( abilityFormulas == null )
                {
                    abilityFormulas = new Dictionary<byte, string>();
                    string[] formulaNames = Utilities.GetStringsFromNumberedXmlNodes(
                        dict["AbilityFormulas"] as string,
                        "/AbilityFormulas/Ability[@value='{0:X2}']",
                        256 );
                    for( int i = 0; i < 256; i++ )
                    {
                        abilityFormulas.Add( (byte)i, formulaNames[i] );
                    }
                }

                return abilityFormulas;
            }
        }

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

        public static byte[] ENTD1 { get { return dict["ENTD1"] as byte[]; } }

        public static byte[] ENTD2 { get { return dict["ENTD2"] as byte[]; } }

        public static byte[] ENTD3 { get { return dict["ENTD3"] as byte[]; } }

        public static byte[] ENTD4 { get { return dict["ENTD4"] as byte[]; } }

        public static byte[] ENTD5 { get { return dict["ENTD5"] as byte[]; } }

        public static byte[] ICON0 { get { return Properties.Resources.ICON0; } }

        public static System.Drawing.Image ICON0_PNG 
        { 
            get 
            {
                using( System.IO.MemoryStream stream = new System.IO.MemoryStream( ICON0, false ) )
                {
                    return System.Drawing.Image.FromStream( stream );
                }
            }
        }

        public static byte[] MoveFind { get { return Properties.Resources.MoveFind; } }

        public static string EventNames { get { return dict["EventNames"] as string; } }

        public static Dictionary<int, string> FFTPackFiles
        {
            get
            {
                if( fftPackFiles == null )
                {
                    fftPackFiles = new Dictionary<int, string>();

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml( dict["FFTPackFiles"] as string );

                    XmlNodeList nodes = doc.SelectNodes( "/files/file" );
                    foreach( XmlNode node in nodes )
                    {
                        fftPackFiles.Add( Convert.ToInt32( node.Attributes["entry"].InnerText ), node.Attributes["name"].InnerText );
                    }
                }

                return fftPackFiles;
            }
        }

        public static byte[] FontBin { get { return dict["FontBin"] as byte[]; } }

        public static byte[] FontWidthsBin { get { return dict["FontWidthsBin"] as byte[]; } }

        public static byte[] InflictStatusesBin { get { return dict["InflictStatusesBin"] as byte[]; } }

        public static string Items { get { return dict["Items"] as string; } }

        public static string ItemsStrings { get { return dict["ItemsStrings"] as string; } }

        public static byte[] JobLevelsBin { get { return dict["JobLevelsBin"] as byte[]; } }

        public static string Jobs { get { return dict["Jobs"] as string; } }

        public static byte[] JobsBin { get { return dict["JobsBin"] as byte[]; } }

        public static byte[] MonsterSkillsBin { get { return dict["MonsterSkillsBin"] as byte[]; } }

        public static byte[] NewItemAttributesBin { get { return dict["NewItemAttributesBin"] as byte[]; } }

        public static byte[] NewItemsBin { get { return dict["NewItemsBin"] as byte[]; } }

        public static byte[] OldItemAttributesBin { get { return dict["OldItemAttributesBin"] as byte[]; } }

        public static byte[] OldItemsBin { get { return dict["OldItemsBin"] as byte[]; } }

        public static byte[] PoachProbabilitiesBin { get { return dict["PoachProbabilitiesBin"] as byte[]; } }

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


		#endregion Static Properties 

		#region Constructors (1) 

        static Resources()
        {
            dict["AbilitiesBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.AbilitiesBin );
            dict["ActionEventsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.ActionEventsBin );
            dict["ENTD1"] = GZip.Decompress( FFTPatcher.Properties.Resources.ENTD1 );
            dict["ENTD2"] = GZip.Decompress( FFTPatcher.Properties.Resources.ENTD2 );
            dict["ENTD3"] = GZip.Decompress( FFTPatcher.Properties.Resources.ENTD3 );
            dict["ENTD4"] = GZip.Decompress( FFTPatcher.Properties.Resources.ENTD4 );
            dict["ENTD5"] = GZip.Decompress( FFTPatcher.Properties.Resources.ENTD5 );
            dict["FontBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.FontBin );
            dict["FontWidthsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.FontWidthsBin );
            dict["InflictStatusesBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.InflictStatusesBin );
            dict["JobLevelsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.JobLevelsBin );
            dict["JobsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.JobsBin );
            dict["MonsterSkillsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.MonsterSkillsBin );
            dict["NewItemAttributesBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.NewItemAttributesBin );
            dict["NewItemsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.NewItemsBin );
            dict["StatusAttributesBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.StatusAttributesBin );
            dict["OldItemAttributesBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.OldItemAttributesBin );
            dict["OldItemsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.OldItemsBin );
            dict["SkillSetsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.SkillSetsBin );
            dict["PoachProbabilitiesBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.PoachProbabilitiesBin );

            dict["SkillSets"] = GZip.Decompress( FFTPatcher.Properties.Resources.SkillSets ).ToUTF8String();
            dict["Abilities"] = GZip.Decompress( FFTPatcher.Properties.Resources.Abilities ).ToUTF8String();
            dict["AbilitiesStrings"] = GZip.Decompress( FFTPatcher.Properties.Resources.AbilitiesStrings ).ToUTF8String();
            dict["EventNames"] = GZip.Decompress( FFTPatcher.Properties.Resources.EventNames ).ToUTF8String();
            dict["Items"] = GZip.Decompress( FFTPatcher.Properties.Resources.Items ).ToUTF8String();
            dict["ItemsStrings"] = GZip.Decompress( FFTPatcher.Properties.Resources.ItemsStrings ).ToUTF8String();
            dict["Jobs"] = GZip.Decompress( FFTPatcher.Properties.Resources.Jobs ).ToUTF8String();
            dict["SpecialNames"] = GZip.Decompress( FFTPatcher.Properties.Resources.SpecialNames ).ToUTF8String();
            dict["SpriteSets"] = GZip.Decompress( FFTPatcher.Properties.Resources.SpriteSets ).ToUTF8String();
            dict["StatusNames"] = GZip.Decompress( FFTPatcher.Properties.Resources.StatusNames ).ToUTF8String();

            dict["FFTPackFiles"] = GZip.Decompress( FFTPatcher.Properties.Resources.FFTPackFiles ).ToUTF8String();

            dict["AbilitiesEffects"] = GZip.Decompress( FFTPatcher.Properties.Resources.AbilitiesEffects ).ToUTF8String();
            dict["AbilitiesEffectsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.AbilityEffectsBin );

            dict["AbilityFormulas"] = GZip.Decompress( FFTPatcher.Properties.Resources.AbilityFormulas ).ToUTF8String();
        }

		#endregion Constructors 

    }
}