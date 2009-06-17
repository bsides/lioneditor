using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PatcherLib
{
    using PatcherLib.Datatypes;
    using PatcherLib.Utilities;
    using Paths = Resources.Paths.PSP;

    public static partial class PSPResources
    {
        public static class Lists
        {

            private static ReadOnlyCollection<string> mapNamesReadOnly;
            private static IList<string> abilityAI;
            private static IList<string> abilityAttributes;
            private static IList<string> abilityEffects;
            private static IList<string> abilityTypes;
            private static IList<string> shopAvailabilities;
            private static IList<string> statuses;
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









            private static IList<string> abilityNames;
            public static IList<string> AbilityNames
            {
                get
                {
                    if ( abilityNames == null )
                    {
                        IList<string> temp = PatcherLib.Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            abilitiesDoc,
                            "/Abilities/Ability[@value='{0}']/@name",
                            512 );
                        abilityNames = temp.AsReadOnly();
                    }

                    return abilityNames;
                }
            }


            public static IList<string> SpriteFiles { get { return spriteFiles; } }

            public static IList<string> AbilityAI
            {
                get
                {
                    if ( abilityAI == null )
                    {
                        var temp =
                            Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                                PSPResources.abilitiesStringsDoc,
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
                        var temp  =
                            Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                                PSPResources.abilitiesStringsDoc,
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
                        var temp = Utilities.Utilities.GetStringsFromNumberedXmlNodes(
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
                            Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                                PSPResources.abilitiesStringsDoc,
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
                        var names = Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            PSPResources.mapNamesDoc,
                            "/MapNames/Map[@value='{0}']",
                            128 );
                        mapNamesReadOnly = new ReadOnlyCollection<string>( names );
                    }

                    return mapNamesReadOnly;
                }
            }

            private static IList<string> items;
            public static IList<string> Items
            {
                get
                {
                    if ( items == null )
                    {
                        var names = PatcherLib.Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            PSPResources.itemsDoc,
                            "/Items/Item[@offset='{0}']/@name",
                            316 );
                        items = names.AsReadOnly();
                    }

                    return items;
                }
            }

            private static IList<string> monsterNames;
            private static IList<string> jobNames;

            public static IList<string> MonsterNames
            {
                get
                {
                    if ( monsterNames == null )
                    {
                        var names = PatcherLib.Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            PSPResources.jobsDoc,
                            "/Jobs/Job[@offset='{0:X2}']/@name",
                            48,
                            0x5E );
                        monsterNames = names.AsReadOnly();
                    }

                    return monsterNames;
                }
            }
            public static IList<string> JobNames
            {
                get
                {
                    if ( jobNames == null )
                    {
                        var names = PatcherLib.Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            PSPResources.jobsDoc,
                            "/Jobs/Job[@offset='{0:X2}']/@name",
                            0xAA );
                        jobNames = names.AsReadOnly();
                    }

                    return jobNames;
                }
            }

            private static IList<string> skillSets;
            public static IList<string> SkillSets
            {
                get
                {
                    if ( skillSets == null )
                    {
                        var names = PatcherLib.Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            PSPResources.skillSetsDoc,
                            "/SkillSets/SkillSet[@byte='{0:X2}']/@name",
                            0xE3 );
                        skillSets = names.AsReadOnly();
                    }

                    return skillSets;
                }
            }
            private static IList<string> spriteSets;
            public static IList<string> SpriteSets
            {
                get
                {
                    if ( spriteSets == null )
                    {
                        var names = PatcherLib.Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            PSPResources.spriteSetsDoc,
                            "/Sprites/Sprite[@byte='{0:X2}']/@name",
                            256 );
                        spriteSets = names.AsReadOnly();
                    }

                    return spriteSets;
                }
            }

            private static IList<string> eventNames;
            public static IList<string> EventNames
            {
                get
                {
                    if ( eventNames == null )
                    {
                        var names = PatcherLib.Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            PSPResources.eventNamesDoc,
                            "/Events/Event[@value='{0:X3}']/@name",
                            0x200 + 77 );
                        eventNames = names.AsReadOnly();
                    }

                    return eventNames;
                }
            }

            private static IList<string> specialNames;
            public static IList<string> SpecialNames
            {
                get
                {
                    if ( specialNames == null )
                    {
                        var names = PatcherLib.Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            PSPResources.specialNamesDoc,
                            "/SpecialNames/SpecialName[@byte='{0:X2}']/@name",
                            256 );
                        specialNames = names.AsReadOnly();
                    }

                    return specialNames;
                }
            }

            public static IList<string> ShopAvailabilities
            {
                get
                {
                    if ( shopAvailabilities == null )
                    {
                        var temp = 
                            Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                                PSPResources.itemsStringsDoc,
                                "/ItemStrings/ShopAvailabilities/string[@value='{0}']/@name",
                                21 );
                        shopAvailabilities = temp.AsReadOnly();
                    }

                    return shopAvailabilities;
                }
            }

            public static IList<string> Statuses
            {
                get
                {
                    if ( statuses == null )
                    {
                        var temp = Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                            PSPResources.statusNamesDoc,
                            "/Statuses/Status[@offset='{0}']/@name",
                            40 );
                        statuses = temp.AsReadOnly();
                    }

                    return statuses;
                }
            }


        }

    }
}