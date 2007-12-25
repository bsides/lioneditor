using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FFTPatcher.Properties;

namespace FFTPatcher.Datatypes
{
    public class MonsterSkill
    {
        public Ability Ability1 { get; set; }
        public Ability Ability2 { get; set; }
        public Ability Ability3 { get; set; }
        public Ability Beastmaster { get; set; }
        public byte Value { get; private set; }
        public string Name { get; private set; }

        public MonsterSkill( SubArray<byte> bytes )
        {
            bool[] flags = Utilities.BooleansFromByteMSB( bytes[0] );
            Ability1 = AllAbilities.DummyAbilities[flags[0] ? (bytes[1] + 0x100) : bytes[1]];
            Ability2 = AllAbilities.DummyAbilities[flags[1] ? (bytes[2] + 0x100) : bytes[2]];
            Ability3 = AllAbilities.DummyAbilities[flags[2] ? (bytes[3] + 0x100) : bytes[3]];
            Beastmaster = AllAbilities.DummyAbilities[flags[3] ? (bytes[4] + 0x100) : bytes[4]];
        }

        public MonsterSkill( byte value, string name, SubArray<byte> bytes )
            : this( bytes )
        {
            Name = name;
            Value = value;
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[5];
            result[0] = Utilities.ByteFromBooleans(
                Ability1.Offset > 0xFF,
                Ability2.Offset > 0xFF,
                Ability3.Offset > 0xFF,
                Beastmaster.Offset > 0xFF,
                false, false, false, false );
            result[1] = (byte)(Ability1.Offset & 0xFF);
            result[2] = (byte)(Ability2.Offset & 0xFF);
            result[3] = (byte)(Ability3.Offset & 0xFF);
            result[4] = (byte)(Beastmaster.Offset & 0xFF);

            return result;
        }
    }

    public class AllMonsterSkills
    {
        public MonsterSkill[] MonsterSkills { get; private set; }
        public static string[] Names { get; private set; }

        static AllMonsterSkills()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( Resources.Jobs );
            Names = new string[48];
            for( int i = 0; i < 48; i++ )
            {
                int j = i + 0x5E;
                Names[i] = doc.SelectSingleNode( string.Format( "//Job[@offset='{0}']/@name", j.ToString( "X2" ) ) ).InnerText;
            }
        }

        public AllMonsterSkills( SubArray<byte> bytes )
        {
            MonsterSkills = new MonsterSkill[48];
            for( int i = 0; i < 48; i++ )
            {
                MonsterSkills[i] = new MonsterSkill( (byte)(i + 0xB0), Names[i], new SubArray<byte>( bytes, 5 * i, 5 * i + 4 ) );
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 5 * MonsterSkills.Length );
            foreach( MonsterSkill s in MonsterSkills )
            {
                result.AddRange( s.ToByteArray() );
            }

            return result.ToArray();
        }

        public string GenerateCodes()
        {
            byte[] newBytes = this.ToByteArray();
            byte[] oldBytes = Resources.MonsterSkillsBin;
            StringBuilder codeBuilder = new StringBuilder();
            for( int i = 0; i < newBytes.Length; i++ )
            {
                if( newBytes[i] != oldBytes[i] )
                {
                    UInt32 addy = (UInt32)(0x27AB60 + i);
                    string code = "_L 0x0" + addy.ToString( "X7" ) + " 0x000000" + newBytes[i].ToString( "X2" );
                    codeBuilder.AppendLine( code );
                }
            }

            return codeBuilder.ToString();
        }
    }
}
