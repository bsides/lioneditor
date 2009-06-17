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

    public static partial class PSPResources
    {
        private static string eventNamesDoc;

        private static string abilityEffectsDoc;
        private static string itemsDoc;
        private static string jobsDoc;
        private static string skillSetsDoc;
        private static string specialNamesDoc;
        private static string spriteSetsDoc;
        private static string statusNamesDoc;

        private static string itemsStringsDoc;
        private static string mapNamesDoc;
        private static string abilitiesDoc;

        private static string abilitiesStringsDoc;

        private static string shopNamesDoc;

        public static IList<string> CharacterSet { get; private set; }

        //static Dictionary<string, object> dict = new Dictionary<string, object>();
        private static IDictionary<Shops, string> readOnlyStoreNames;

        public static IDictionary<Shops, string> ShopNames
        {
            get
            {
                if (readOnlyStoreNames == null)
                {
                    Dictionary<Shops, string> storeNames = new Dictionary<Shops, string>();
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(shopNamesDoc);

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

        public static System.Drawing.Image ICON0_PNG
        {
            get
            {
                byte[] mem = Binaries.ICON0.ToArray();
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream(mem, false))
                {
                    return System.Drawing.Image.FromStream(stream);
                }
            }
        }

        static PSPResources()
        {
            Binaries.StoreInventories = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.StoreInventories].AsReadOnly();
            Binaries.ENTD1 = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD1].AsReadOnly();
            Binaries.ENTD2 = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD2].AsReadOnly();
            Binaries.ENTD3 = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD3].AsReadOnly();
            Binaries.ENTD4 = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD4].AsReadOnly();
            Binaries.ENTD5 = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ENTD5].AsReadOnly();
            Binaries.MoveFind = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.MoveFind].AsReadOnly();
            Binaries.Abilities = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.Abilities].AsReadOnly();
            Binaries.AbilityAnimations = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.AbilityAnimations].AsReadOnly();
            Binaries.AbilityEffects = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.AbilityEffects].AsReadOnly();
            Binaries.ActionEvents = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ActionEvents].AsReadOnly();
            Binaries.Font = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.Font].AsReadOnly();
            Binaries.FontWidths = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.FontWidths].AsReadOnly();
            Binaries.ICON0 = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.ICON0].AsReadOnly();
            Binaries.InflictStatuses = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.InflictStatuses].AsReadOnly();
            Binaries.JobLevels = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.JobLevels].AsReadOnly();
            Binaries.Jobs = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.Jobs].AsReadOnly();
            Binaries.MonsterSkills = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.MonsterSkills].AsReadOnly();
            Binaries.NewItemAttributes = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.NewItemAttributes].AsReadOnly();
            Binaries.NewItems = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.NewItems].AsReadOnly();
            Binaries.OldItemAttributes = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.OldItemAttributes].AsReadOnly();
            Binaries.OldItems = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.OldItems].AsReadOnly();
            Binaries.PoachProbabilities = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.PoachProbabilities].AsReadOnly();
            Binaries.SkillSets = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.SkillSets].AsReadOnly();
            Binaries.StatusAttributes = Resources.ZipFileContents[Resources.Paths.PSP.Binaries.StatusAttributes].AsReadOnly();


            eventNamesDoc = Resources.ZipFileContents[Resources.Paths.PSP.EventNamesXML].ToUTF8String();
            jobsDoc = Resources.ZipFileContents[Resources.Paths.PSP.JobsXML].ToUTF8String();
            skillSetsDoc = Resources.ZipFileContents[Resources.Paths.PSP.SkillSetsXML].ToUTF8String();
            specialNamesDoc = Resources.ZipFileContents[Resources.Paths.PSP.SpecialNamesXML].ToUTF8String();
            spriteSetsDoc = Resources.ZipFileContents[Resources.Paths.PSP.SpriteSetsXML].ToUTF8String();
            statusNamesDoc = Resources.ZipFileContents[Resources.Paths.PSP.StatusNamesXML].ToUTF8String();
            abilitiesStringsDoc = Resources.ZipFileContents[Resources.Paths.PSP.AbilitiesStringsXML].ToUTF8String();
            abilityEffectsDoc = Resources.ZipFileContents[Resources.Paths.PSP.AbilityEffectsXML].ToUTF8String();
            //dict[Resources.Paths.PSP.ItemAttributesXML] = Resources.ZipFileContents[Resources.Paths.PSP.ItemAttributesXML].ToUTF8String();
            itemsDoc = Resources.ZipFileContents[Resources.Paths.PSP.ItemsXML].ToUTF8String();
            itemsStringsDoc = Resources.ZipFileContents[Resources.Paths.PSP.ItemsStringsXML].ToUTF8String();
            shopNamesDoc = Resources.ZipFileContents[Resources.Paths.PSP.ShopNamesXML].ToUTF8String();
            mapNamesDoc = Resources.ZipFileContents[Paths.MapNamesXML].ToUTF8String();

            abilitiesDoc = Resources.ZipFileContents[Resources.Paths.PSP.AbilitiesNamesXML].ToUTF8String();

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
