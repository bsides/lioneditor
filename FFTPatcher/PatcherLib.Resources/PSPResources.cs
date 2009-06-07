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

namespace PatcherLib
{
    using PatcherLib.Datatypes;
    using PatcherLib.Utilities;
    using Paths = Resources.Paths.PSP;

    public static class PSPResources
    {

        public static IList<string> CharacterSet { get; private set; }

        private static string[] abilityAI;
        private static string[] abilityAttributes;
        private static string[] abilityEffects;
        private static string[] abilityTypes;
        static Dictionary<string, object> dict = new Dictionary<string, object>();
        private static IDictionary<Shops, string> readOnlyStoreNames;
        private static ReadOnlyCollection<string> mapNamesReadOnly;
        private static string[] shopAvailabilities;
        private static string[] statuses;
        private static IList<string> spriteFiles = new List<string>
        {
            "Ramza Chapter 1",
            "Ramza Chapter 2/3",
            "Ramza Chapter 4",
            "Delita Chapter 1",
            "Delita Chapter 2/3",
            "Delita Chapter 4",
            "Argath",
            "Zalbaag",
            "Dycedarg",
            "Larg",
            "Goltanna",
            "Ovelia",
            "Orlandeau",
            "Funebris",
            "Reis (Human)",
            "Zalmour",
            "Gaffgarion (Enemy)",
            "Marach (Dead, Used once)",
            "Simon",
            "Alma (Battle)",
            "Orran",
            "Mustadio (Join)",
            "Gaffgarion (Guest)",
            "Delacroix",
            "Rapha (Guest)",
            "Marach (Enemy & Join)",
            "Elmdore",
            "Tietra",
            "Barrington",
            "Agrias (Join)",
            "Beowulf",
            "Wiegraf Chapter 1",
            "Valmafra",
            "Mustadio (Guest)",
            "Ludovich",
            "Folmarv",
            "Loffrey",
            "Isilud",
            "Cletienne",
            "Wiegraf Chapter 2/3",
            "Rapha (Join)",
            "Meliadoul (Join)",
            "Barich",
            "Alma (Dead)",
            "Celia",
            "Lettie",
            "Meliadoul (Enemy)",
            "Alma (Events)",
            "Ajora",
            "Cloud",
            "Zalbaag (Zombie)",
            "Agrias (Guest)",
            "Female Chemist",
            "Female White Mage",
            "Male Black Mage",
            "Male Mystic",
            "Male Squire",
            "Celia (Never Used)",
            "Lettie (Never Used)",
            "Belias",
            "Male Knight",
            "Zalera",
            "Male Archer",
            "Hashmal",
            "Altima (First Form)",
            "Male Black Mage",
            "Cuchulainn",
            "Female Time Mage",
            "Adrammelech",
            "Male Mystic",
            "Female Summoner",
            "Reis (Dragon Form)",
            "Altima (Second Form)",
            "10 Year Old Male",
            "10 Year Old Woman",
            "20 Year Old Male",
            "20 Year Old Woman",
            "40 Year Old Male",
            "40 Year Old Woman",
            "60 Year Old Male",
            "60 Year Old Woman",
            "Old Funeral Man",
            "Old Funeral Woman",
            "Funeral Man",
            "Funeral Woman",
            "Funeral Priest",
            "Male Squire",
            "Male Squire",
            "Male Squire",
            "Male Squire",
            "Male Squire",
            "Male Squire",
            "???",
            "???",
            "???",
            "Male Squire",
            "Female Squire",
            "Male Chemist",
            "Female Chemist",
            "Male Knight",
            "Female Knight",
            "Male Archer",
            "Female Archer",
            "Male Monk",
            "Female Monk",
            "Male White Mage",
            "Female White Mage",
            "Male Black Mage",
            "Female Black Mage",
            "Male Time Mage",
            "Female Time Mage",
            "Male Summoner",
            "Female Summoner",
            "Male Thief",
            "Female Thief",
            "Male Orator",
            "Female Orator",
            "Male Mystic",
            "Female Mystic",
            "Male Geomancer",
            "Female Geomancer",
            "Male Dragoon",
            "Female Dragoon",
            "Male Samurai",
            "Female Samurai",
            "Male Ninja",
            "Female Ninja",
            "Male Arithmetician",
            "Female Arithmetician",
            "Male Bard",
            "Female Dancer",
            "Male Mime",
            "Female Mime",
            "Chocobo",
            "Goblin",
            "Bomb",
            "Coeurl",
            "Squid",
            "Skeleton",
            "Ghost",
            "Ahriman",
            "Cockatrice",
            "Pig",
            "Treant",
            "Minotaur",
            "Malboro",
            "Behemoth",
            "Dragon",
            "Tiamat",
            "Apanda/Byblos",
            "Elidibus",
            "Dragon",
            "Demon",
            "Automaton",
            "Male Dark Knight",
            "Female Dark Knight",
            "Male Onion Knight",
            "Female Onion Knight",
            "Balthier",
            "Luso",
            "Argath (Death Knight)",
            "Aliste",
            "Bremondt",
            "Bremondt (Dark Dragon)",
            "???"
        }.AsReadOnly();


        public static string Abilities { get { return dict[Paths.AbilitiesNamesXML] as string; } }

        public static byte[] AbilitiesBin { get { return dict[Paths.Binaries.Abilities] as byte[]; } }

        public static byte[] AbilityAnimationsBin { get { return dict[Paths.Binaries.AbilityAnimations] as byte[]; } }

        public static string AbilitiesStrings { get { return dict[Paths.AbilitiesStringsXML] as string; } }

        public static IList<string> SpriteFiles { get { return spriteFiles; } }

        public static string[] AbilityAI
        {
            get
            {
                if (abilityAI == null)
                {
                    abilityAI =
                        Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            AbilitiesStrings,
                            "/AbilityStrings/AI/string[@value='{0}']/@name",
                            24);
                }
                return abilityAI;
            }
        }

        public static IDictionary<Shops, string> ShopNames
        {
            get
            {
                if (readOnlyStoreNames == null)
                {
                    Dictionary<Shops, string> storeNames = new Dictionary<Shops, string>();
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml((string)PSPResources.dict[Resources.Paths.PSP.ShopNamesXML]);

                    foreach (System.Xml.XmlNode node in doc.SelectNodes("/ShopNames/Shop"))
                    {
                        storeNames[(Shops)System.Enum.Parse(typeof(Shops), node.Attributes["value"].Value)] =
                            node.Attributes["name"].Value;
                    }

                    readOnlyStoreNames = new ReadOnlyDictionary<Shops, string>(storeNames);
                }

                return readOnlyStoreNames;
            }
        }

        public static string[] AbilityAttributes
        {
            get
            {
                if (abilityAttributes == null)
                {
                    abilityAttributes =
                        Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            AbilitiesStrings,
                            "/AbilityStrings/Attributes/string[@value='{0}']/@name",
                            32);
                }
                return abilityAttributes;
            }
        }

        public static string[] AbilityEffects
        {
            get
            {
                if (abilityEffects == null)
                {
                    abilityEffects = Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                        dict[Paths.AbilityEffectsXML] as string,
                        "/Effects/Effect[@value='{0:X3}']/@name",
                        512);
                }

                return abilityEffects;
            }
        }

        public static byte[] AbilityEffectsBin { get { return dict[Paths.Binaries.AbilityEffects] as byte[]; } }

        public static string[] AbilityTypes
        {
            get
            {
                if (abilityTypes == null)
                {
                    abilityTypes =
                        Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            AbilitiesStrings,
                            "/AbilityStrings/Types/string[@value='{0}']/@name",
                            16);
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

        public static string EventNames { get { return dict[Paths.EventNamesXML] as string; } }

        public static byte[] FontBin { get { return dict[Paths.Binaries.Font] as byte[]; } }

        public static byte[] FontWidthsBin { get { return dict[Paths.Binaries.FontWidths] as byte[]; } }

        public static byte[] ICON0 { get { return dict[Paths.Binaries.ICON0] as byte[]; } }

        public static System.Drawing.Image ICON0_PNG
        {
            get
            {
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream(ICON0, false))
                {
                    return System.Drawing.Image.FromStream(stream);
                }
            }
        }

        public static byte[] InflictStatusesBin { get { return dict[Paths.Binaries.InflictStatuses] as byte[]; } }

        public static string Items { get { return dict[Paths.ItemsXML] as string; } }

        public static string ItemsStrings { get { return dict[Paths.ItemsStringsXML] as string; } }

        public static byte[] JobLevelsBin { get { return dict[Paths.Binaries.JobLevels] as byte[]; } }

        public static string Jobs { get { return dict[Paths.JobsXML] as string; } }

        public static byte[] JobsBin { get { return dict[Paths.Binaries.Jobs] as byte[]; } }

        public static ReadOnlyCollection<string> MapNames
        {
            get
            {
                if (mapNamesReadOnly == null)
                {
                    var names = Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                        dict[Paths.MapNamesXML] as string,
                        "/MapNames/Map[@value='{0}']",
                        128);
                    mapNamesReadOnly = new ReadOnlyCollection<string>(names);
                }

                return mapNamesReadOnly;
            }
        }

        public static byte[] MonsterSkillsBin { get { return dict[Paths.Binaries.MonsterSkills] as byte[]; } }

        public static byte[] MoveFind { get { return dict[Paths.Binaries.MoveFind] as byte[]; } }

        public static byte[] NewItemAttributesBin { get { return dict[Paths.Binaries.NewItemAttributes] as byte[]; } }

        public static byte[] NewItemsBin { get { return dict[Paths.Binaries.NewItems] as byte[]; } }

        public static byte[] OldItemAttributesBin { get { return dict[Paths.Binaries.OldItemAttributes] as byte[]; } }

        public static byte[] OldItemsBin { get { return dict[Paths.Binaries.OldItems] as byte[]; } }

        public static byte[] PoachProbabilitiesBin { get { return dict[Paths.Binaries.PoachProbabilities] as byte[]; } }

        public static string[] ShopAvailabilities
        {
            get
            {
                if (shopAvailabilities == null)
                {
                    shopAvailabilities =
                        Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            ItemsStrings,
                            "/ItemStrings/ShopAvailabilities/string[@value='{0}']/@name",
                            21);
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
                if (statuses == null)
                {
                    statuses = Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                        StatusNames,
                        "/Statuses/Status[@offset='{0}']/@name",
                        40);
                }

                return statuses;
            }
        }

        public static string StatusNames { get { return dict[Paths.StatusNamesXML] as string; } }

        public static byte[] StoreInventoriesBin { get { return dict[Paths.Binaries.StoreInventories] as byte[]; } }


        static PSPResources()
        {
            dict[Resources.Paths.PSP.Binaries.StoreInventories] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.StoreInventories];
            dict[Resources.Paths.PSP.Binaries.ENTD1] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD1];
            dict[Resources.Paths.PSP.Binaries.ENTD2] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD2];
            dict[Resources.Paths.PSP.Binaries.ENTD3] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD3];
            dict[Resources.Paths.PSP.Binaries.ENTD4] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD4];
            dict[Resources.Paths.PSP.Binaries.ENTD5] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD5];
            dict[Resources.Paths.PSP.Binaries.MoveFind] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.MoveFind];
            dict[Resources.Paths.PSP.Binaries.Abilities] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.Abilities];
            dict[Resources.Paths.PSP.Binaries.AbilityAnimations] = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.AbilityAnimations];
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
            dict[Resources.Paths.PSP.ShopNamesXML] = Resources.ZipFileContents[Resources.Paths.PSP.ShopNamesXML].ToUTF8String();
            dict[Resources.Paths.PSP.MapNamesXML] = Resources.ZipFileContents[Paths.MapNamesXML].ToUTF8String();
            string[] characterSet = new string[2200];
            PSXResources.CharacterSet.CopyTo( characterSet, 0 );
            characterSet[0x95] = " ";
            characterSet[0x880] = "á";
            characterSet[0x881] = "à";
            characterSet[0x882] = "é";
            characterSet[0x883] = "è";
            characterSet[0x884] = "í";
            characterSet[0x885] = "ú";
            characterSet[0x886] = "ù";
            characterSet[0x887] = "-";
            characterSet[0x888] = "—";
            CharacterSet = characterSet.AsReadOnly();
        }

    }
}
