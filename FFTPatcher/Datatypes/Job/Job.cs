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

        public Job( Context context, SubArray<byte> bytes )
        {
            int equipEnd = context == Context.US_PSP ? 13 : 12;
            SkillSet = SkillSet.DummySkillSets[bytes[0]];
            InnateA = AllAbilities.DummyAbilities[Utilities.BytesToUShort( bytes[1], bytes[2] )];
            InnateB = AllAbilities.DummyAbilities[Utilities.BytesToUShort( bytes[3], bytes[4] )];
            InnateC = AllAbilities.DummyAbilities[Utilities.BytesToUShort( bytes[5], bytes[6] )];
            InnateD = AllAbilities.DummyAbilities[Utilities.BytesToUShort( bytes[7], bytes[8] )];
            Equipment = new Equipment( new SubArray<byte>( bytes, 9, equipEnd ) );
            HPConstant = bytes[equipEnd + 1];
            HPMultiplier = bytes[equipEnd + 2];
            MPConstant = bytes[equipEnd + 3];
            MPMultiplier = bytes[equipEnd + 4];
            SpeedConstant = bytes[equipEnd + 5];
            SpeedMultiplier = bytes[equipEnd + 6];
            PAConstant = bytes[equipEnd + 7];
            PAMultiplier = bytes[equipEnd + 8];
            MAConstant = bytes[equipEnd + 9];
            MAMultiplier = bytes[equipEnd + 10];
            Move = bytes[equipEnd + 11];
            Jump = bytes[equipEnd + 12];
            CEvade = bytes[equipEnd + 13];
            PermanentStatus = new Statuses( new SubArray<byte>( bytes, equipEnd + 14, equipEnd + 18 ) );
            StatusImmunity = new Statuses( new SubArray<byte>( bytes, equipEnd + 19, equipEnd + 23 ) );
            StartingStatus = new Statuses( new SubArray<byte>( bytes, equipEnd + 24, equipEnd + 28 ) );
            AbsorbElement = new Elements( bytes[equipEnd + 29] );
            CancelElement = new Elements( bytes[equipEnd + 30] );
            HalfElement = new Elements( bytes[equipEnd + 31] );
            WeakElement = new Elements( bytes[equipEnd + 32] );

            MPortrait = bytes[equipEnd + 33];
            MPalette = bytes[equipEnd + 34];
            MGraphic = bytes[equipEnd + 35];
        }

        public Job( SubArray<byte> bytes )
            : this( Context.US_PSP, bytes )
        {
        }

        public byte[] ToByteArray()
        {
            return ToByteArray( Context.US_PSP );
        }

        public byte[] ToByteArray( Context context )
        {
            List<byte> result = new List<byte>( 49 );
            result.Add( SkillSet.Value );
            result.AddRange( Utilities.UShortToBytes( InnateA.Offset ) );
            result.AddRange( Utilities.UShortToBytes( InnateB.Offset ) );
            result.AddRange( Utilities.UShortToBytes( InnateC.Offset ) );
            result.AddRange( Utilities.UShortToBytes( InnateD.Offset ) );
            result.AddRange( Equipment.ToByteArray( context ) );
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
            : this( Context.US_PSP, bytes )
        {
        }

        public AllJobs( Context context, SubArray<byte> bytes )
        {
            int numJobs = context == Context.US_PSP ? 0xA9 : 0xA0;
            int jobLength = context == Context.US_PSP ? 49 : 48;

            Jobs = new Job[numJobs];
            for( int i = 0; i < numJobs; i++ )
            {
                Jobs[i] = new Job( (byte)i, Names[i], new SubArray<byte>( bytes, i * jobLength, (i + 1) * jobLength - 1 ) );
            }
        }

        public byte[] ToByteArray()
        {
            return ToByteArray( Context.US_PSP );
        }

        public byte[] ToByteArray( Context context )
        {
            List<byte> result = new List<byte>( 0x205C );
            foreach( Job j in Jobs )
            {
                result.AddRange( j.ToByteArray( context ) );
            }
            result.Add( 0x00 );
            result.Add( 0x00 );
            result.Add( 0x00 );

            return result.ToArray();
        }

        public string GenerateCodes()
        {
            return Utilities.GenerateCodes( Context.US_PSP, Resources.JobsBin, this.ToByteArray(), 0x277988 );
        }
    }
}
