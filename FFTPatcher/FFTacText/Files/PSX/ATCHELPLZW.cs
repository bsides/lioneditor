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

using System;
using System.Collections.Generic;
using FFTPatcher.Datatypes;

namespace FFTPatcher.TextEditor.Files.PSX
{
    public class ATCHELPLZW : IStringSectioned
    {
        private static Dictionary<string, long> locations;
        public IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "EVENT/ATCHELP.LZW", 0x00 );
                }

                return locations;
            }
        }

        private const int dataStart = 0x80;

        private static ATCHELPLZW Instance { get; set; }
        private static string[] sectionNames = new string[21] {
            "", "", "", "", "", "", 
            "", "", "", "", "", "Help", 
            "Job descriptions", "Item descriptions", "", "Ability descriptions", "", "",
            "", "Skillset descriptions", "" };

        public static string[][] entryNames;

        public List<IList<string>> Sections { get; private set; }
        public IList<string> SectionNames { get { return sectionNames; } }
        public IList<IList<string>> EntryNames { get { return entryNames; } }
        public int MaxLength { get { return 0x0160D5; } }
        public int Length { get { return ToBytes().Count; } }

        static ATCHELPLZW()
        {
            Instance = new ATCHELPLZW( Properties.Resources.ATCHELP_LZW );
            entryNames = new string[21][];
            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[Instance.Sections[i].Count];
            }
            entryNames[11] = new string[40] {
                "Unit #", "Level", "HP", "MP", "CT", "AT", "Exp", "Name",
                "Brave", "Faith", "", "Move", "Jump", "", "", "",
                "Speed", "ATK", "Weapon ATK", "", "Eva%", "SEv%", "AEv%", "Phys land effect",
                "Magic land effect", "Estimated", "Hit rate", "Aries", "Taurus", "Gemini", "Cancer", "Leo", 
                "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces", "Serpentarius" };
            IList<string> temp = new List<string>( new SubArray<string>( FFTPatcher.Datatypes.AllJobs.PSXNames, 0, 154 ) );
            temp.AddRange( new string[Instance.Sections[12].Count - temp.Count] );
            entryNames[12] = temp.ToArray();
            entryNames[13] = FFTPatcher.Datatypes.Item.PSXNames.ToArray();
            temp = new List<string>( new string[265] );
            temp.AddRange( new SubArray<string>( FFTPatcher.Datatypes.AllAbilities.PSXNames, 265 ) );
            entryNames[15] = temp.ToArray();
            temp = new List<string>( new SubArray<string>( FFTPatcher.Datatypes.SkillSet.PSXNames, 0, 0xAF ) );
            temp.AddRange( new string[12] );
            entryNames[19] = temp.ToArray();
        }

        private ATCHELPLZW( IList<byte> bytes )
        {
            Sections = new List<IList<string>>( 21 );
            for( int i = 0; i < 21; i++ )
            {
                uint start = Utilities.BytesToUInt32( new SubArray<byte>( bytes, i * 4, i * 4 + 3 ) );
                uint stop = Utilities.BytesToUInt32( new SubArray<byte>( bytes, (i + 1) * 4, (i + 1) * 4 + 3 ) ) - 1;
                if( i == 20 )
                {
                    stop = (uint)bytes.Count - 1 - dataStart;
                }

                IList<byte> thisSection = new SubArray<byte>( bytes, (int)(start + dataStart), (int)(stop + dataStart) );
                Sections.Add( TextUtilities.ProcessList( thisSection, TextUtilities.CharMapType.PSX ) );
            }
        }

        public static ATCHELPLZW GetInstance()
        {
            return Instance;
        }

        private List<byte> ToBytes()
        {
            List<List<byte>> byteSections = new List<List<byte>>( 21 );
            foreach( List<string> section in Sections )
            {
                List<byte> sectionBytes = new List<byte>();
                foreach( string s in section )
                {
                    sectionBytes.AddRange( TextUtilities.PSXMap.StringToByteArray( s ) );
                }
                byteSections.Add( sectionBytes );
            }

            List<byte> result = new List<byte>();
            result.AddRange( new byte[] { 0x00, 0x00, 0x00, 0x00 } );
            int old = 0;
            for( int i = 0; i < 20; i++ )
            {
                result.AddRange( ((UInt32)(byteSections[i].Count + old)).ToBytes() );
                old += byteSections[i].Count;
            }
            result.AddRange( new byte[0x2C] );

            foreach( List<byte> bytes in byteSections )
            {
                result.AddRange( bytes );
            }

            return result;
        }

        public byte[] ToByteArray()
        {
            List<byte> result = ToBytes();

            if( result.Count < MaxLength )
            {
                result.AddRange( new byte[MaxLength - result.Count] );
            }

            return result.ToArray();
        }
    }
}
