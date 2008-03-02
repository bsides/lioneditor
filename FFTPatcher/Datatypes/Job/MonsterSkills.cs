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
    /// Represents the <see cref="Ability"/>s a monster can use.
    /// </summary>
    public class MonsterSkill
    {
        public Ability Ability1 { get; set; }
        public Ability Ability2 { get; set; }
        public Ability Ability3 { get; set; }
        public Ability Beastmaster { get; set; }
        public byte Value { get; private set; }
        public string Name { get; private set; }
        public MonsterSkill Default { get; private set; }

        public MonsterSkill( IList<byte> bytes )
            : this( 0, "", bytes, null )
        {
        }

        public MonsterSkill( byte value, string name, IList<byte> bytes, MonsterSkill defaults )
        {
            Default = defaults;
            Name = name;
            Value = value;
            bool[] flags = Utilities.BooleansFromByteMSB( bytes[0] );
            Ability1 = AllAbilities.DummyAbilities[flags[0] ? (bytes[1] + 0x100) : bytes[1]];
            Ability2 = AllAbilities.DummyAbilities[flags[1] ? (bytes[2] + 0x100) : bytes[2]];
            Ability3 = AllAbilities.DummyAbilities[flags[2] ? (bytes[3] + 0x100) : bytes[3]];
            Beastmaster = AllAbilities.DummyAbilities[flags[3] ? (bytes[4] + 0x100) : bytes[4]];
        }

        public MonsterSkill( byte value, string name, IList<byte> bytes )
            : this( value, name, bytes, null )
        {
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

        public byte[] ToByteArray( Context context )
        {
            return ToByteArray();
        }
    }

    public class AllMonsterSkills
    {
        public MonsterSkill[] MonsterSkills { get; private set; }
        public static string[] Names { get { return FFTPatch.Context == Context.US_PSP ? PSPNames : PSXNames; } }
        public static string[] PSXNames { get; private set; }
        public static string[] PSPNames { get; private set; }

        static AllMonsterSkills()
        {
            PSPNames = Utilities.GetStringsFromNumberedXmlNodes(
                Resources.Jobs,
                "/Jobs/Job[@offset='{0:X2}']/@name",
                48,
                0x5E );
            PSXNames = Utilities.GetStringsFromNumberedXmlNodes(
                PSXResources.Jobs,
                "/Jobs/Job[@offset='{0:X2}']/@name",
                48,
                0x5E );
        }

        public AllMonsterSkills( IList<byte> bytes )
        {
            byte[] defaultBytes = FFTPatch.Context == Context.US_PSP ? Resources.MonsterSkillsBin : PSXResources.MonsterSkillsBin;

            MonsterSkills = new MonsterSkill[48];
            for( int i = 0; i < 48; i++ )
            {
                MonsterSkills[i] = new MonsterSkill( (byte)(i + 0xB0), Names[i], bytes.Sub( 5 * i, 5 * i + 4 ),
                    new MonsterSkill( (byte)(i + 0xB0), Names[i], defaultBytes.Sub( 5 * i, 5 * i + 4 ) ) );
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

        public byte[] ToByteArray( Context context )
        {
            return ToByteArray();
        }

        public List<string> GenerateCodes()
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                return Codes.GenerateCodes( Context.US_PSP, Resources.MonsterSkillsBin, this.ToByteArray(), 0x27AB60 );
            }
            else
            {
                return Codes.GenerateCodes( Context.US_PSX, PSXResources.MonsterSkillsBin, this.ToByteArray(), 0x065BC4 );
            }
        }
    }
}
