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

namespace FFTPatcher.Datatypes
{
    /// <summary>
    /// Represents the set of <see cref="Ability"/> a <see cref="Job"/> can use.
    /// </summary>
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

        private static SortedDictionary<byte, SkillSet> pspEventSkills;
        private static SortedDictionary<byte, SkillSet> psxEventSkills;
        public static SortedDictionary<byte, SkillSet> EventSkillSets
        {
            get { return FFTPatch.Context == Context.US_PSP ? pspEventSkills : psxEventSkills; }
        }

        static SkillSet()
        {
            pspSkills = new SkillSet[0xE3];
            pspEventSkills = new SortedDictionary<byte, SkillSet>();

            string[] pspNames = Utilities.GetStringsFromNumberedXmlNodes(
                Resources.SkillSets,
                "//SkillSet[@byte='{0:X2}']",
                0xE3 );
            string[] psxNames = Utilities.GetStringsFromNumberedXmlNodes(
                PSXResources.SkillSets,
                "//SkillSet[@byte='{0:X2}']",
                0xE0 );
            for( int i = 0; i < 0xE3; i++ )
            {
                string n = pspNames[i];
                pspSkills[i] = new SkillSet( n, (byte)(i & 0xFF) );
                pspEventSkills.Add( (byte)i, pspSkills[i] );
            }

            SkillSet random = new SkillSet( "<Random>", 0xFE );
            SkillSet equal = new SkillSet( "<Job's>", 0xFF );
            pspEventSkills.Add( 0xFE, random );
            pspEventSkills.Add( 0xFF, equal );

            psxSkills = new SkillSet[0xE0];
            psxEventSkills = new SortedDictionary<byte, SkillSet>();
            for( int i = 0; i < 0xE0; i++ )
            {
                string n = psxNames[i];
                psxSkills[i] = new SkillSet( n, (byte)(i & 0xFF) );
                psxEventSkills.Add( (byte)i, psxSkills[i] );
            }
            psxEventSkills.Add( 0xFE, random );
            psxEventSkills.Add( 0xFF, equal );
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

        public SkillSet Default { get; private set; }

        public SkillSet( byte value, SubArray<byte> bytes, SkillSet defaults )
            : this( value, bytes )
        {
            Default = defaults;
        }

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
            : this( Context.US_PSP, bytes, new SubArray<byte>( Resources.SkillSetsBin ) )
        {
        }

        public AllSkillSets( Context context, SubArray<byte> bytes )
            : this( context, bytes, null )
        {
        }

        public AllSkillSets( Context context, SubArray<byte> bytes, SubArray<byte> defaultBytes )
        {
            List<SkillSet> tempSkills = new List<SkillSet>( 179 );
            for( int i = 0; i < 176; i++ )
            {
                tempSkills.Add( new SkillSet( (byte)i, new SubArray<byte>( bytes, 25 * i, 25 * i + 24 ),
                    new SkillSet( (byte)i, new SubArray<byte>( defaultBytes, 25 * i, 25 * i + 24 ) ) ) );
            }

            if( context == Context.US_PSP )
            {
                for( int i = 0xE0; i <= 0xE2; i++ )
                {
                    tempSkills.Add( new SkillSet( (byte)i, new SubArray<byte>( bytes, 25 * (i - 0xE0 + 176), 25 * (i - 0xE0 + 176) + 24 ),
                        new SkillSet( (byte)i, new SubArray<byte>( defaultBytes, 25 * (i - 0xE0 + 176), 25 * (i - 0xE0 + 176) + 24 ) ) ) );
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
                return Codes.GenerateCodes( Context.US_PSP, Resources.SkillSetsBin, this.ToByteArray(), 0x2799E4 );
            }
            else
            {
                return Codes.GenerateCodes( Context.US_PSX, PSXResources.SkillSetsBin, this.ToByteArray( Context.US_PSX ), 0x064A94 );
            }
        }
    }
}
