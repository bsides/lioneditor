﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FFTPatcher
{
    using Paths = FFTPatcher.Resources.Paths.PSP;
    using System.Xml;

    static class PSPResources
    {
        private static string[] abilityAI;
        private static string[] abilityAttributes;
        private static string[] abilityEffects;
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


        public static ReadOnlyCollection<string> MapNames
        {
            get
            {
                if( mapNamesReadOnly == null )
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

        public static byte[] ENTD5 { get { return dict[Paths.Binaries.ENTD5] as byte[]; } }

        public static byte[] ICON0 { get { return dict[Paths.Binaries.ICON0] as byte[]; } }

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

        public static byte[] MoveFind { get { return dict[Paths.Binaries.MoveFind] as byte[]; } }

        public static string EventNames { get { return dict[Paths.EventNamesXML] as string; } }

        public static Dictionary<int, string> FFTPackFiles
        {
            get
            {
                if( fftPackFiles == null )
                {
                    fftPackFiles = new Dictionary<int, string>();

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml( dict[Paths.FFTPackFilesXML] as string );

                    XmlNodeList nodes = doc.SelectNodes( "/files/file" );
                    foreach( XmlNode node in nodes )
                    {
                        fftPackFiles.Add( Convert.ToInt32( node.Attributes["entry"].InnerText ), node.Attributes["name"].InnerText );
                    }
                }

                return fftPackFiles;
            }
        }

        public static byte[] FontBin { get { return dict[Paths.Binaries.Font] as byte[]; } }

        public static byte[] FontWidthsBin { get { return dict[Paths.Binaries.FontWidths] as byte[]; } }

        public static byte[] InflictStatusesBin { get { return dict[Paths.Binaries.InflictStatuses] as byte[]; } }

        public static string Items { get { return dict[Paths.ItemsXML] as string; } }

        public static string ItemsStrings { get { return dict[Paths.ItemsStringsXML] as string; } }

        public static byte[] JobLevelsBin { get { return dict[Paths.Binaries.JobLevels] as byte[]; } }

        public static string Jobs { get { return dict[Paths.JobsXML] as string; } }

        public static byte[] JobsBin { get { return dict[Paths.Binaries.Jobs] as byte[]; } }

        public static byte[] MonsterSkillsBin { get { return dict[Paths.Binaries.MonsterSkills] as byte[]; } }

        public static byte[] NewItemAttributesBin { get { return dict[Paths.Binaries.NewItemAttributes] as byte[]; } }

        public static byte[] NewItemsBin { get { return dict[Paths.Binaries.NewItems] as byte[]; } }

        public static byte[] OldItemAttributesBin { get { return dict[Paths.Binaries.OldItemAttributes] as byte[]; } }

        public static byte[] OldItemsBin { get { return dict[Paths.Binaries.OldItems] as byte[]; } }

        public static byte[] PoachProbabilitiesBin { get { return dict[Paths.Binaries.PoachProbabilities] as byte[]; } }

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

        static PSPResources()
        {
            dict[Resources.Paths.PSP.Binaries.ENTD1] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD1];
            dict[Resources.Paths.PSP.Binaries.ENTD2] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD2];
            dict[Resources.Paths.PSP.Binaries.ENTD3] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD3];
            dict[Resources.Paths.PSP.Binaries.ENTD4] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD4];
            dict[Resources.Paths.PSP.Binaries.ENTD5] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD5];
            dict[Resources.Paths.PSP.Binaries.MoveFind] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.MoveFind];
            dict[Resources.Paths.PSP.Binaries.Abilities] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.Abilities];
            dict[Resources.Paths.PSP.Binaries.AbilityEffects] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.AbilityEffects];
            dict[Resources.Paths.PSP.Binaries.ActionEvents] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ActionEvents];
            dict[Resources.Paths.PSP.Binaries.Font] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.Font];
            dict[Resources.Paths.PSP.Binaries.FontWidths] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.FontWidths];
            dict[Resources.Paths.PSP.Binaries.ICON0] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ICON0];
            dict[Resources.Paths.PSP.Binaries.InflictStatuses] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.InflictStatuses];
            dict[Resources.Paths.PSP.Binaries.JobLevels] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.JobLevels];
            dict[Resources.Paths.PSP.Binaries.Jobs] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.Jobs];
            dict[Resources.Paths.PSP.Binaries.MonsterSkills] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.MonsterSkills];
            dict[Resources.Paths.PSP.Binaries.NewItemAttributes] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.NewItemAttributes];
            dict[Resources.Paths.PSP.Binaries.NewItems] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.NewItems];
            dict[Resources.Paths.PSP.Binaries.OldItemAttributes] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.OldItemAttributes];
            dict[Resources.Paths.PSP.Binaries.OldItems] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.OldItems];
            dict[Resources.Paths.PSP.Binaries.PoachProbabilities] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.PoachProbabilities];
            dict[Resources.Paths.PSP.Binaries.SkillSets] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.SkillSets];
            dict[Resources.Paths.PSP.Binaries.StatusAttributes] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.StatusAttributes];
            dict[Resources.Paths.PSP.EventNamesXML] = Resources.ZipFileContents[Resources.Paths.PSP.EventNamesXML].ToUTF8String();
            dict[Resources.Paths.PSP.FFTPackFilesXML] = Resources.ZipFileContents[Resources.Paths.PSP.FFTPackFilesXML].ToUTF8String();
            dict[Resources.Paths.PSP.JobsXML] = Resources.ZipFileContents[Resources.Paths.PSP.JobsXML].ToUTF8String();
            dict[Resources.Paths.PSP.SkillSetsXML] = Resources.ZipFileContents[Resources.Paths.PSP.SkillSetsXML].ToUTF8String();
            dict[Resources.Paths.PSP.SpecialNamesXML] = Resources.ZipFileContents[Resources.Paths.PSP.SpecialNamesXML].ToUTF8String();
            dict[Resources.Paths.PSP.SpriteSetsXML] = Resources.ZipFileContents[Resources.Paths.PSP.SpriteSetsXML].ToUTF8String();
            dict[Resources.Paths.PSP.StatusNamesXML] = Resources.ZipFileContents[Resources.Paths.PSP.StatusNamesXML].ToUTF8String();
            dict[Resources.Paths.PSP.AbilitiesNamesXML] = Resources.ZipFileContents[Resources.Paths.PSP.AbilitiesNamesXML].ToUTF8String();
            dict[Resources.Paths.PSP.AbilitiesStringsXML] = Resources.ZipFileContents[Resources.Paths.PSP.AbilitiesStringsXML].ToUTF8String();
            dict[Resources.Paths.PSP.AbilityEffectsXML] = Resources.ZipFileContents[Resources.Paths.PSP.AbilityEffectsXML].ToUTF8String();
            dict[Resources.Paths.PSP.ItemAttributesXML] = Resources.ZipFileContents[Resources.Paths.PSP.ItemAttributesXML].ToUTF8String();
            dict[Resources.Paths.PSP.ItemsXML] = Resources.ZipFileContents[Resources.Paths.PSP.ItemsXML].ToUTF8String();
            dict[Resources.Paths.PSP.ItemsStringsXML] = Resources.ZipFileContents[Resources.Paths.PSP.ItemsStringsXML].ToUTF8String();
         }
    }
}
