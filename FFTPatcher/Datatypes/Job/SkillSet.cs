/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Collections.Generic;
using System.Xml;
using FFTPatcher.Properties;

namespace FFTPatcher.Datatypes
{
    public class SkillSet
    {
        private static SkillSet[] pspSkills;
        private static SkillSet[] psxSkills;
        public static SkillSet[] DummySkillSets
        {
            get
            {
                return FFTPatch.Context == Context.US_PSP ? pspSkills : psxSkills;
            }
        }

        static SkillSet()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( Resources.SkillSets );
            pspSkills = new SkillSet[0xE3];
            for( int i = 0; i < 0xE3; i++ )
            {
                string n = doc.SelectSingleNode( string.Format( "//SkillSet[@byte='{0}']", i.ToString( "X2" ) ) ).InnerText;
                pspSkills[i] = new SkillSet( n, (byte)(i & 0xFF) );
            }

            doc = new XmlDocument();
            doc.LoadXml( PSXResources.SkillSets );
            psxSkills = new SkillSet[0xE0];
            for( int i = 0; i < 0xE0; i++ )
            {
                string n = doc.SelectSingleNode( string.Format( "//SkillSet[@byte='{0}']", i.ToString( "X2" ) ) ).InnerText;
                psxSkills[i] = new SkillSet( n, (byte)(i & 0xFF) );
            }
        }

        public byte Value { get; private set; }
        public string Name { get; private set; }

        private SkillSet( string name, byte value )
        {
            Name = name;
            Value = value;
        }

        public Ability[] Actions { get; private set; }
        public Ability[] TheRest { get; private set; }

        public SkillSet( byte value, SubArray<byte> bytes )
            : this( DummySkillSets[value].Name, value )
        {
            List<bool> actions = new List<bool>( 16 );
            actions.AddRange( Utilities.BooleansFromByteMSB( bytes[0] ) );
            actions.AddRange( Utilities.BooleansFromByteMSB( bytes[1] ) );
            List<bool> theRest = new List<bool>( Utilities.BooleansFromByteMSB( bytes[2] ) );

            Actions = new Ability[16];
            TheRest = new Ability[6];

            for( int i = 0; i < 16; i++ )
            {
                Actions[i] = AllAbilities.DummyAbilities[(actions[i] ? (bytes[3 + i] + 0x100) : (bytes[3 + i]))];
            }
            for( int i = 0; i < 6; i++ )
            {
                TheRest[i] = AllAbilities.DummyAbilities[(theRest[i] ? (bytes[19 + i] + 0x100) : (bytes[19 + i]))];
            }
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[25];
            result[0] = Utilities.ByteFromBooleans(
                Actions[0].Offset > 0xFF,
                Actions[1].Offset > 0xFF,
                Actions[2].Offset > 0xFF,
                Actions[3].Offset > 0xFF,
                Actions[4].Offset > 0xFF,
                Actions[5].Offset > 0xFF,
                Actions[6].Offset > 0xFF,
                Actions[7].Offset > 0xFF );
            result[1] = Utilities.ByteFromBooleans(
                Actions[8].Offset > 0xFF,
                Actions[9].Offset > 0xFF,
                Actions[10].Offset > 0xFF,
                Actions[11].Offset > 0xFF,
                Actions[12].Offset > 0xFF,
                Actions[13].Offset > 0xFF,
                Actions[14].Offset > 0xFF,
                Actions[15].Offset > 0xFF );
            result[2] = Utilities.ByteFromBooleans(
                TheRest[0].Offset > 0xFF,
                TheRest[1].Offset > 0xFF,
                TheRest[2].Offset > 0xFF,
                TheRest[3].Offset > 0xFF,
                TheRest[4].Offset > 0xFF,
                TheRest[5].Offset > 0xFF,
                false,
                false );
            for( int i = 0; i < 16; i++ )
            {
                result[3 + i] = (byte)(Actions[i].Offset & 0xFF);
            }
            for( int i = 0; i < 6; i++ )
            {
                result[19 + i] = (byte)(TheRest[i].Offset & 0xFF);
            }

            return result;
        }

        public override string ToString()
        {
            return Value.ToString( "X2" ) + " " + Name;
        }
    }

    public class AllSkillSets
    {
        public SkillSet[] SkillSets { get; private set; }

        public AllSkillSets( SubArray<byte> bytes )
            : this( Context.US_PSP, bytes )
        {
        }

        public AllSkillSets( Context context, SubArray<byte> bytes )
        {
            List<SkillSet> tempSkills = new List<SkillSet>( 179 );
            for( int i = 0; i < 176; i++ )
            {
                tempSkills.Add( new SkillSet( (byte)i, new SubArray<byte>( bytes, 25 * i, 25 * i + 24 ) ) );
            }

            if( context == Context.US_PSP )
            {
                for( int i = 0xE0; i <= 0xE2; i++ )
                {
                    tempSkills.Add( new SkillSet( (byte)i, new SubArray<byte>( bytes, 25 * (i - 0xE0 + 176), 25 * (i - 0xE0 + 176) + 24 ) ) );
                }
            }

            SkillSets = tempSkills.ToArray();
        }

        public byte[] ToByteArray()
        {
            return ToByteArray( Context.US_PSP );
        }

        public byte[] ToByteArray( Context context )
        {
            List<byte> result = new List<byte>( 0x117B );
            foreach( SkillSet s in SkillSets )
            {
                result.AddRange( s.ToByteArray() );
            }

            while( (context == Context.US_PSP) && (result.Count < 0x117B) )
            {
                result.Add( 0x00 );
            }

            return result.ToArray();
        }

        public List<string> GenerateCodes()
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                return Utilities.GenerateCodes( Context.US_PSP, Resources.SkillSetsBin, this.ToByteArray(), 0x2799E4 );
            }
            else
            {
                return Utilities.GenerateCodes( Context.US_PSX, PSXResources.SkillSetsBin, this.ToByteArray( Context.US_PSX ), 0x064A94 );
            }
        }

    }
}
