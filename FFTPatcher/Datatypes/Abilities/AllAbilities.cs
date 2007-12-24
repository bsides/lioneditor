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

        public static List<Ability> DummyAbilities { get; private set; }

        static AllAbilities()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Resources.Abilities);

            Names = new List<string>(512);
            DummyAbilities = new List<Ability>(512);
            for (int i = 0; i < 512; i++)
            {
                Names.Add(doc.SelectSingleNode(string.Format("/Abilities/Ability[@value='{0}']/@name", i)).InnerText);
                DummyAbilities.Add(new Ability(Names[i], (UInt16)i));
            }
        }

        public List<Ability> Abilities { get; private set; }

        public AllAbilities(SubArray<byte> bytes)
        {
            Abilities = new List<Ability>(512);
            for (UInt16 i = 0; i < 512; i++)
            {
                SubArray<byte> first = new SubArray<byte>(bytes, i * 8, i * 8 + 7);
                SubArray<byte> second;
                if (i <= 0x16F)
                {
                    second = new SubArray<byte>(bytes, 0x1000 + 14 * i, 0x1000 + 14 * i + 13);
                }
                else if (i <= 0x17D)
                {
                    second = new SubArray<byte>(bytes, 0x2420 + i - 0x170, 0x2420 + i - 0x170);
                }
                else if (i <= 0x189)
                {
                    second = new SubArray<byte>(bytes, 0x2430 + i - 0x17E, 0x2430 + i - 0x17E);
                }
                else if (i <= 0x195)
                {
                    second = new SubArray<byte>(bytes, 0x243C + (i - 0x18A) * 2, 0x243C + (i - 0x18A) * 2 + 1);
                }
                else if (i <= 0x19D)
                {
                    second = new SubArray<byte>(bytes, 0x2454 + (i - 0x196) * 2, 0x2454 + (i - 0x196) * 2 + 1);
                }
                else if (i <= 0x1A5)
                {
                    second = new SubArray<byte>(bytes, 0x2464 + i - 0x19E, 0x2464 + i - 0x19E);
                }
                else
                {
                    second = new SubArray<byte>(bytes, 0x246C + i - 0x1A6, 0x246C + i - 0x1A6);
                }

                Abilities.Add(new Ability(Names[i], i, first, second));
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> bytes = new List<byte>();
            for (UInt16 i = 0; i < 512; i++)
            {
                bytes.AddRange(Abilities[i].ToByteArray());
            }
            for (UInt16 i = 0; i < 512; i++)
            {
                bytes.AddRange(Abilities[i].ToSecondByteArray());
            }

            bytes.Insert(0x242E, 0x00);
            bytes.Insert(0x242E, 0x00);
            return bytes.ToArray();
        }

        public string GenerateCodes()
        {
            byte[] newBytes = this.ToByteArray();
            byte[] oldBytes = Resources.AbilitiesBin;
            StringBuilder codeBuilder = new StringBuilder();
            for (int i = 0; i < newBytes.Length; i++)
            {
                if (newBytes[i] != oldBytes[i])
                {
                    UInt32 addy = (UInt32)(0x2754C0 + i);
                    string code = "0x0" + addy.ToString("X7") + " 0x000000" + newBytes[i].ToString("X2");
                    codeBuilder.AppendLine(code);
                }
            }

            return codeBuilder.ToString();
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
