using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FFTPatcher.Properties;

namespace FFTPatcher.Datatypes
{
    public class Job
    {
        public string Name { get; private set; }
        public byte Value { get; private set; }
        public SkillSet SkillSet { get; set; }
        public Ability InnateA { get; set; }
        public Ability InnateB { get; set; }
        public Ability InnateC { get; set; }
        public Ability InnateD { get; set; }

        public Equipment Equipment { get; private set; }

        public byte HPConstant { get; set; }
        public byte HPMultiplier { get; set; }
        public byte MPConstant { get; set; }
        public byte MPMultiplier { get; set; }
        public byte SpeedConstant { get; set; }
        public byte SpeedMultiplier { get; set; }
        public byte PAConstant { get; set; }
        public byte PAMultiplier { get; set; }
        public byte MAConstant { get; set; }
        public byte MAMultiplier { get; set; }
        public byte Move { get; set; }
        public byte Jump { get; set; }
        public byte CEvade { get; set; }

        public Statuses PermanentStatus { get; private set; }
        public Statuses StatusImmunity { get; private set; }
        public Statuses StartingStatus { get; private set; }

        public Elements AbsorbElement { get; private set; }
        public Elements CancelElement { get; private set; }
        public Elements HalfElement { get; private set; }
        public Elements WeakElement { get; private set; }

        public byte MPortrait { get; set; }
        public byte MPalette { get; set; }
        public byte MGraphic { get; set; }

        public Job( byte value, string name, SubArray<byte> bytes )
            : this( bytes )
        {
            Value = value;
            Name = name;
        }

        public Job( SubArray<byte> bytes )
        {
            SkillSet = SkillSet.DummySkillSets[bytes[0]];
            InnateA = AllAbilities.DummyAbilities[Utilities.BytesToUShort( bytes[1], bytes[2] )];
            InnateB = AllAbilities.DummyAbilities[Utilities.BytesToUShort( bytes[3], bytes[4] )];
            InnateC = AllAbilities.DummyAbilities[Utilities.BytesToUShort( bytes[5], bytes[6] )];
            InnateD = AllAbilities.DummyAbilities[Utilities.BytesToUShort( bytes[7], bytes[8] )];
            Equipment = new Equipment( new SubArray<byte>( bytes, 9, 13 ) );
            HPConstant = bytes[14];
            HPMultiplier = bytes[15];
            MPConstant = bytes[16];
            MPMultiplier = bytes[17];
            SpeedConstant = bytes[18];
            SpeedMultiplier = bytes[19];
            PAConstant = bytes[20];
            PAMultiplier = bytes[21];
            MAConstant = bytes[22];
            MAMultiplier = bytes[23];
            Move = bytes[24];
            Jump = bytes[25];
            CEvade = bytes[26];
            PermanentStatus = new Statuses( new SubArray<byte>( bytes, 27, 31 ) );
            StatusImmunity = new Statuses( new SubArray<byte>( bytes, 32, 36 ) );
            StartingStatus = new Statuses( new SubArray<byte>( bytes, 37, 41 ) );
            AbsorbElement = new Elements( bytes[42] );
            CancelElement = new Elements( bytes[43] );
            HalfElement = new Elements( bytes[44] );
            WeakElement = new Elements( bytes[45] );

            MPortrait = bytes[46];
            MPalette = bytes[47];
            MGraphic = bytes[48];
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 49 );
            result.Add( SkillSet.Value );
            result.AddRange( Utilities.UShortToBytes( InnateA.Offset ) );
            result.AddRange( Utilities.UShortToBytes( InnateB.Offset ) );
            result.AddRange( Utilities.UShortToBytes( InnateC.Offset ) );
            result.AddRange( Utilities.UShortToBytes( InnateD.Offset ) );
            result.AddRange( Equipment.ToByteArray() );
            result.Add( HPConstant );
            result.Add( HPMultiplier );
            result.Add( MPConstant );
            result.Add( MPMultiplier );
            result.Add( SpeedConstant );
            result.Add( SpeedMultiplier );
            result.Add( PAConstant );
            result.Add( PAMultiplier );
            result.Add( MAConstant );
            result.Add( MAMultiplier );
            result.Add( Move );
            result.Add( Jump );
            result.Add( CEvade );
            result.AddRange( PermanentStatus.ToByteArray() );
            result.AddRange( StatusImmunity.ToByteArray() );
            result.AddRange( StartingStatus.ToByteArray() );
            result.Add( AbsorbElement.ToByte() );
            result.Add( CancelElement.ToByte() );
            result.Add( HalfElement.ToByte() );
            result.Add( WeakElement.ToByte() );
            result.Add( MPortrait );
            result.Add( MPalette );
            result.Add( MGraphic );

            return result.ToArray();
        }

        public override string ToString()
        {
            return Value.ToString( "X2" ) + " " + Name;
        }
    }

    public class AllJobs
    {
        public static string[] Names { get; private set; }
        public Job[] Jobs { get; private set; }

        static AllJobs()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( Resources.Jobs );
            Names = new string[0xA9];
            for( int i = 0; i < 0xA9; i++ )
            {
                Names[i] = doc.SelectSingleNode( string.Format( "//Job[@offset='{0}']/@name", i.ToString( "X2" ) ) ).InnerText;
            }
        }

        public AllJobs( SubArray<byte> bytes )
        {
            Jobs = new Job[0xA9];
            for( int i = 0; i < 0xA9; i++ )
            {
                Jobs[i] = new Job( (byte)i, Names[i], new SubArray<byte>( bytes, 49 * i, 49 * i + 48 ) );
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 0x205C );
            foreach( Job j in Jobs )
            {
                result.AddRange( j.ToByteArray() );
            }
            result.Add( 0x00 );
            result.Add( 0x00 );
            result.Add( 0x00 );

            return result.ToArray();
        }

        public string GenerateCodes()
        {
            byte[] newBytes = this.ToByteArray();
            byte[] oldBytes = Resources.JobsBin;
            StringBuilder codeBuilder = new StringBuilder();
            for( int i = 0; i < newBytes.Length; i++ )
            {
                if( newBytes[i] != oldBytes[i] )
                {
                    UInt32 addy = (UInt32)(0x277988 + i);
                    string code = "_L 0x0" + addy.ToString( "X7" ) + " 0x000000" + newBytes[i].ToString( "X2" );
                    codeBuilder.AppendLine( code );
                }
            }

            return codeBuilder.ToString();
        }
    }
}
