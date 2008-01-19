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
using System.Xml;


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

        public Job Default { get; private set; }

        public Job( Context context, SubArray<byte> bytes )
            : this( context, 0, "", bytes )
        {
        }

        public Job( Context context, byte value, string name, SubArray<byte> bytes )
            : this( context, value, name, bytes, null )
        {
        }

        public Job( Context context, byte value, string name, SubArray<byte> bytes, Job defaults )
        {
            Default = defaults;
            Value = value;
            Name = name;
            int equipEnd = context == Context.US_PSP ? 13 : 12;
            SkillSet = SkillSet.DummySkillSets[bytes[0]];
            InnateA = AllAbilities.DummyAbilities[Utilities.BytesToUShort( bytes[1], bytes[2] )];
            InnateB = AllAbilities.DummyAbilities[Utilities.BytesToUShort( bytes[3], bytes[4] )];
            InnateC = AllAbilities.DummyAbilities[Utilities.BytesToUShort( bytes[5], bytes[6] )];
            InnateD = AllAbilities.DummyAbilities[Utilities.BytesToUShort( bytes[7], bytes[8] )];
            Equipment = new Equipment( new SubArray<byte>( bytes, 9, equipEnd ), defaults == null ? null : defaults.Equipment );
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
            PermanentStatus = new Statuses( new SubArray<byte>( bytes, equipEnd + 14, equipEnd + 18 ), defaults == null ? null : defaults.PermanentStatus );
            StatusImmunity = new Statuses( new SubArray<byte>( bytes, equipEnd + 19, equipEnd + 23 ), defaults == null ? null : defaults.StatusImmunity );
            StartingStatus = new Statuses( new SubArray<byte>( bytes, equipEnd + 24, equipEnd + 28 ), defaults == null ? null : defaults.StartingStatus );
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

        public Job( byte value, string name )
        {
            Value = value;
            Name = name;
        }

        public byte[] ToByteArray()
        {
            return ToByteArray( Context.US_PSP );
        }

        public byte[] ToByteArray( Context context )
        {
            List<byte> result = new List<byte>( 49 );
            result.Add( SkillSet.Value );
            result.AddRange( InnateA.Offset.ToBytes() );
            result.AddRange( InnateB.Offset.ToBytes() );
            result.AddRange( InnateC.Offset.ToBytes() );
            result.AddRange( InnateD.Offset.ToBytes() );
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
        private static string[] psxNames;
        private static string[] pspNames;
        private static Job[] psxJobs;
        private static Job[] pspJobs;

        public static Job[] DummyJobs
        {
            get { return FFTPatch.Context == Context.US_PSP ? pspJobs : psxJobs; }
        }

        public static string[] Names
        {
            get { return FFTPatch.Context == Context.US_PSP ? pspNames : psxNames; }
        }

        public Job[] Jobs { get; private set; }

        static AllJobs()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( Resources.Jobs );
            pspNames = new string[0xAA];
            pspJobs = new Job[0xAA];
            for( int i = 0; i < 0xA9; i++ )
            {
                pspNames[i] = doc.SelectSingleNode( string.Format( "//Job[@offset='{0}']/@name", i.ToString( "X2" ) ) ).InnerText;
                pspJobs[i] = new Job( (byte)i, pspNames[i] );
            }
            pspJobs[0xA9] = new Job( 0xA9, "???" );
            pspNames[0xA9] = "???";

            doc = new XmlDocument();
            doc.LoadXml( PSXResources.Jobs );
            psxNames = new string[0xA0];
            psxJobs = new Job[0xA0];
            for( int i = 0; i < 0xA0; i++ )
            {
                psxNames[i] = doc.SelectSingleNode( string.Format( "//Job[@offset='{0}']/@name", i.ToString( "X2" ) ) ).InnerText;
                psxJobs[i] = new Job( (byte)i, psxNames[i] );
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
            byte[] defaultBytes = context == Context.US_PSP ? Resources.JobsBin : PSXResources.JobsBin;
            Jobs = new Job[numJobs];
            for( int i = 0; i < numJobs; i++ )
            {
                Jobs[i] = new Job( context, (byte)i, Names[i], new SubArray<byte>( bytes, i * jobLength, (i + 1) * jobLength - 1 ),
                    new Job( context, (byte)i, Names[i], new SubArray<byte>( defaultBytes, i * jobLength, (i + 1) * jobLength - 1 ) ) );
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

            return result.ToArray();
        }

        public List<string> GenerateCodes()
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                return Codes.GenerateCodes( Context.US_PSP, Resources.JobsBin, this.ToByteArray(), 0x277988 );
            }
            else
            {
                return Codes.GenerateCodes( Context.US_PSX, PSXResources.JobsBin, this.ToByteArray( Context.US_PSX ), 0x0610B8 );
            }
        }
    }
}
