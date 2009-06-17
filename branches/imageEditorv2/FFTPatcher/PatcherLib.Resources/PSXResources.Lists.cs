using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PatcherLib
{
    using PatcherLib.Datatypes;
    using PatcherLib.Utilities;
    using Paths = Resources.Paths.PSX;

    public static partial class PSXResources
    {
        public static class Lists
        {
            private static IList<string> abilityAI;
            private static IList<string> abilityAttributes;
            private static IList<string> abilityEffects;
            private static IList<string> abilityTypes;
            private static ReadOnlyCollection<string> mapNamesReadOnly;
            private static IList<string> shopAvailabilities;
            private static IList<string> statuses;
            private static IList<string> abilityNames;

            private static IList<string> monsterNames;
            private static IList<string> jobNames;

            public static IList<string> MonsterNames
            {
                get
                {
                    if ( monsterNames == null )
                    {
                        var temp = Resources.GetStringsFromNumberedXmlNodes(
                            PSXResources.jobsDoc,
                            "/Jobs/Job[@offset='{0:X2}']/@name",
                            48,
                            0x5E );
                        monsterNames = temp.AsReadOnly();
                    }
                    return monsterNames;
                }
            }
            
            private static IList<string> skillsetNames;

            public static IList<string> SkillSets
            {
                get
                {
                    if ( skillsetNames == null )
                    {
                        var temp = Resources.GetStringsFromNumberedXmlNodes(
                            PSXResources.skillSetsDoc,
                            "/SkillSets/SkillSet[@byte='{0:X2}']/@name",
                            0xE0 );
                        skillsetNames = temp.AsReadOnly();
                    }
                    return skillsetNames;
                }
            }

            public static IList<string> JobNames
            {
                get
                {
                    if ( jobNames == null )
                    {
                        var temp = Resources.GetStringsFromNumberedXmlNodes(
                            PSXResources.jobsDoc,
                            "/Jobs/Job[@offset='{0:X2}']/@name",
                            0xA0 );
                        jobNames = temp.AsReadOnly();
                    }
                    return jobNames;
                }
            }

            private static IList<string> eventNames;
            public static IList<string> EventNames
            {
                get
                {
                    if ( eventNames == null )
                    {
                        var temp = Resources.GetStringsFromNumberedXmlNodes(
                            PSXResources.eventNamesDoc,
                            "/Events/Event[@value='{0:X3}']/@name",
                            0x200 );
                        eventNames = temp.AsReadOnly();
                    }

                    return eventNames;
                }
            }

            private static IList<string> items;
            public static IList<string> Items
            {
                get
                {
                    if ( items == null )
                    {
                        var temp = Resources.GetStringsFromNumberedXmlNodes(
                            PSXResources.itemsDoc,
                            "/Items/Item[@offset='{0}']/@name",
                            256 );
                        items = temp.AsReadOnly();
                    }
                    return items;
                }
            }

            public static IList<string> AbilityNames
            {
                get
                {
                    if ( abilityNames == null )
                    {
                        var tempNames = Resources.GetStringsFromNumberedXmlNodes(
                            abilitiesDoc,
                            "/Abilities/Ability[@value='{0}']/@name",
                            512 );
                        abilityNames = tempNames.AsReadOnly();
                    }

                    return abilityNames;
                }
            }

            public static IList<string> AbilityAI
            {
                get
                {
                    if ( abilityAI == null )
                    {
                        var temp =
                            Resources.GetStringsFromNumberedXmlNodes(
                                abilitiesStringsDoc,
                                "/AbilityStrings/AI/string[@value='{0}']/@name",
                                24 );
                        abilityAI = temp.AsReadOnly();
                    }
                    return abilityAI;
                }
            }

            public static IList<string> AbilityAttributes
            {
                get
                {
                    if ( abilityAttributes == null )
                    {
                        var temp =
                            Resources.GetStringsFromNumberedXmlNodes(
                                abilitiesStringsDoc,
                                "/AbilityStrings/Attributes/string[@value='{0}']/@name",
                                32 );
                        abilityAttributes = temp.AsReadOnly();
                    }
                    return abilityAttributes;
                }
            }

            public static IList<string> AbilityEffects
            {
                get
                {
                    if ( abilityEffects == null )
                    {
                        var temp = Resources.GetStringsFromNumberedXmlNodes(
                            abilityEffectsDoc,
                            "/Effects/Effect[@value='{0:X3}']/@name",
                            512 );
                        abilityEffects = temp.AsReadOnly();
                    }

                    return abilityEffects;
                }
            }

            public static IList<string> AbilityTypes
            {
                get
                {
                    if ( abilityTypes == null )
                    {
                        var temp =
                            Resources.GetStringsFromNumberedXmlNodes(
                                abilitiesStringsDoc,
                                "/AbilityStrings/Types/string[@value='{0}']/@name",
                                16 );
                        abilityTypes = temp.AsReadOnly();
                    }
                    return abilityTypes;
                }
            }

            public static IList<string> MapNames
            {
                get
                {
                    if ( mapNamesReadOnly == null )
                    {
                        var names = Resources.GetStringsFromNumberedXmlNodes(
                            PSXResources.mapNamesDoc,
                            "/MapNames/Map[@value='{0}']",
                            128 );
                        mapNamesReadOnly = new ReadOnlyCollection<string>( names );
                    }

                    return mapNamesReadOnly;
                }
            }
            public static IList<string> ShopAvailabilities
            {
                get
                {
                    if ( shopAvailabilities == null )
                    {
                        var temp =
                            Resources.GetStringsFromNumberedXmlNodes(
                                PSXResources.itemsStringsDoc,
                                "/ItemStrings/ShopAvailabilities/string[@value='{0}']/@name",
                                21 );
                        shopAvailabilities = temp.AsReadOnly();
                    }

                    return shopAvailabilities;
                }
            }

            public static IList<string> SpriteFiles { get { return spriteFiles; } }

            public static IList<string> StatusNames
            {
                get
                {
                    if ( statuses == null )
                    {
                        var temp = Resources.GetStringsFromNumberedXmlNodes(
                            statusNamesDoc,
                            "/Statuses/Status[@offset='{0}']/@name",
                            40 );
                        statuses = temp.AsReadOnly();
                    }

                    return statuses;
                }
            }

            public static IDictionary<Shops, string> ShopNames
            {
                get
                {
                    if ( readOnlyStoreNames == null )
                    {
                        Dictionary<Shops, string> storeNames = new Dictionary<Shops, string>();
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        doc.LoadXml( PSXResources.shopNamesDoc );

                        foreach ( System.Xml.XmlNode node in doc.SelectNodes( "/ShopNames/Shop" ) )
                        {
                            storeNames[(Shops)System.Enum.Parse( typeof( Shops ), node.Attributes["value"].Value )] =
                                node.Attributes["name"].Value;
                        }
                        readOnlyStoreNames = new ReadOnlyDictionary<Shops, string>( storeNames );
                    }

                    return readOnlyStoreNames;
                }
            }

            private static IList<string> specialNames;
            public static IList<string> SpecialNames
            {
                get
                {
                    if ( specialNames == null )
                    {
                        var temp = Resources.GetStringsFromNumberedXmlNodes(
                            PSXResources.specialNamesDoc,
                            "/SpecialNames/SpecialName[@byte='{0:X2}']/@name",
                            256 );
                        specialNames = temp.AsReadOnly();
                    }
                    return specialNames;
                }
            }
            
            private static IList<string> spriteSets;
            public static IList<string> SpriteSets
            {
                get
                {
                    if ( spriteSets == null )
                    {
                        var temp = Resources.GetStringsFromNumberedXmlNodes(
                            PSXResources.spriteSetsDoc,
                            "/Sprites/Sprite[@byte='{0:X2}']/@name",
                            256 );
                        spriteSets = temp.AsReadOnly();
                    }
                    return spriteSets;
                }
            }
            private static IList<string> spriteFiles = new List<string>
            {
                "Ramza Chapter 1",
                "Ramza Chapter 2/3",
                "Ramza Chapter 4",
                "Delita Chapter 1",
                "Delita Chapter 2/3",
                "Delita Chapter 4",
                "Algus",
                "Zalbag",
                "Dycedarg",
                "Larg",
                "Goltana",
                "Ovelia",
                "Orlandu",
                "Funeral",
                "Reis (Human)",
                "Zalmo",
                "Gafgarion (Enemy)",
                "Malak (Dead, Used once)",
                "Simon",
                "Alma (Battle)",
                "Olan",
                "Mustadio (Join)",
                "Gafgarion (Guest)",
                "Draclau",
                "Rafa (Guest)",
                "Malak (Enemy & Join)",
                "Elmdor",
                "Teta",
                "Barinten",
                "Agrias (Join)",
                "Beowulf",
                "Wiegraf Chapter 1",
                "Balmafula",
                "Mustadio (Guest)",
                "Rudvich",
                "Vormav",
                "Rofel",
                "Izlude",
                "Kletian",
                "Wiegraf Chapter 2/3",
                "Rafa (Join)",
                "Meliadoul (Join)",
                "Balk",
                "Alma (Dead)",
                "Celia",
                "Lede",
                "Meliadoul (Enemy)",
                "Alma (Events)",
                "Ajora",
                "Cloud",
                "Zalbag (Zombie)",
                "Agrias (Guest)",
                "Female Chemist",
                "Female Priest",
                "Male Wizard",
                "Male Oracle",
                "Male Squire",
                "Celia (Never Used)",
                "Lede (Never Used)",
                "Velius",
                "Male Knight",
                "Zalera",
                "Male Archer",
                "Hashmalum",
                "Altima (First Form)",
                "Male Wizard",
                "Queklain",
                "Female Time Mage",
                "Adramelk",
                "Male Oracle",
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
                "Male Priest",
                "Female Priest",
                "Male Wizard",
                "Female Wizard",
                "Male Time Mage",
                "Female Time Mage",
                "Male Summoner",
                "Female Summoner",
                "Male Thief",
                "Female Thief",
                "Male Mediator",
                "Female Mediator",
                "Male Oracle",
                "Female Oracle",
                "Male Geomancer",
                "Female Geomancer",
                "Male Lancer",
                "Female Lancer",
                "Male Samurai",
                "Female Samurai",
                "Male Ninja",
                "Female Ninja",
                "Male Calculator",
                "Female Calculator",
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
                "Uribo",
                "Treant",
                "Minotaur",
                "Malboro",
                "Behemoth",
                "Dragon",
                "Tiamat",
                "Apanda/Byblos",
                "Elidibs",
                "Dragon",
                "Demon",
                "Steel Giant"
            }.AsReadOnly();



        }

    }
}
