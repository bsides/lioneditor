using System;
using System.Collections.Generic;
using System.Text;
using FFTPatcher.Properties;
using System.Xml;

namespace FFTPatcher.Datatypes
{
    public class AllAbilities
    {
        public static List<string> Names { get; private set; }

        static AllAbilities()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Resources.Abilities);

            Names = new List<string>(512);
            for (int i = 0; i < 512; i++)
            {
                Names.Add(doc.SelectSingleNode(string.Format("/Abilities/Ability[@value='{0}']/@name", i)).InnerXml);
            }
        }

        public List<Ability> Abilities { get; private set; }

        public AllAbilities(SubArray<byte> bytes)
        {
            Abilities = new List<Ability>(512);
            for (UInt16 i = 0; i < 512; i++)
            {
                Abilities.Add(
                    new Ability(Names[i], i,
                        new SubArray<byte>(bytes, i * 8, i * 8 + 7)));
            }
        }
    }

    public class AllAbilityAttributes
    {
        public List<AbilityAttributes> Abilities { get; private set; }
        public AllAbilityAttributes(SubArray<byte> bytes)
        {
            Abilities = new List<AbilityAttributes>(368);

            for (UInt16 i = 0; i < 368; i++)
            {
                Abilities.Add(
                    new AbilityAttributes(AllAbilities.Names[i], i, 
                        new SubArray<byte>(bytes, i * 14, i * 14 + 13)));
            }
        }
    }
}
