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
    public class WORLDLZW : IStringSectioned
    {
        private static Dictionary<string, long> locations;
        public IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "EVENT/WORLD.LZW", 0x00 );
                }

                return locations;
            }
        }

        private const int dataStart = 0x80;

        private static WORLDLZW Instance { get; set; }
        private static string[] sectionNames;

        public static string[][] entryNames;

        public List<IList<string>> Sections { get; private set; }
        public IList<string> SectionNames { get { return sectionNames; } }
        public IList<IList<string>> EntryNames { get { return entryNames; } }
        public int MaxLength { get { return 0xE2DD; } }
        public int Length { get { return ToBytes().Count; } }

        static WORLDLZW()
        {
            sectionNames = new string[32] {
                "","","","","","","Job names","Item names",
                "Character names","Character names","Battle menus","Help text","Errand names","","Ability names","Errand rewards",
                "??","","Location names","Blank","Map menu","Event names","Skillsets","Tavern menu",
                "Tutorial","Brave story","Errand names","Unexplored Land","Treasure","Record","Person","Errand objectives"};
            Instance = new WORLDLZW( Properties.Resources.WORLD_LZW );
            entryNames = new string[32][];
            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[Instance.Sections[i].Count];
            }
            entryNames[6] = new SubArray<string>( FFTPatcher.Datatypes.AllJobs.PSXNames, 0, 154 ).ToArray();
            IList<string> temp = new List<string>( new SubArray<string>( FFTPatcher.Datatypes.Item.PSXNames, 0 ).ToArray() );
            temp.AddRange( new string[Instance.Sections[7].Count - temp.Count] );
            entryNames[7] = temp.ToArray();
            temp = new List<string>( AllAbilities.PSXNames );
            temp.AddRange( new string[Instance.Sections[14].Count - temp.Count] );
            entryNames[14] = temp.ToArray();
            temp = new List<string>( new SubArray<string>( SkillSet.PSXNames, 0, 175 ) );
            temp.AddRange( new string[Instance.Sections[22].Count - temp.Count] );
            entryNames[22] = temp.ToArray();
        }

        private WORLDLZW( IList<byte> bytes )
        {
            Sections = new List<IList<string>>( 32 );
            for( int i = 0; i < 32; i++ )
            {
                uint start = Utilities.BytesToUInt32( new SubArray<byte>( bytes, i * 4, i * 4 + 3 ) );
                uint stop = Utilities.BytesToUInt32( new SubArray<byte>( bytes, (i + 1) * 4, (i + 1) * 4 + 3 ) ) - 1;
                if( i == 31 )
                {
                    stop = (uint)bytes.Count - 1 - dataStart;
                }

                IList<byte> thisSection = new SubArray<byte>( bytes, (int)(start + dataStart), (int)(stop + dataStart) );
                Sections.Add( TextUtilities.ProcessList( thisSection, TextUtilities.CharMapType.PSX ) );
            }
        }

        public static WORLDLZW GetInstance()
        {
            return Instance;
        }

        private List<byte> ToBytes()
        {
            List<List<byte>> byteSections = new List<List<byte>>( 32 );
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
            for( int i = 0; i < 31; i++ )
            {
                result.AddRange( ((UInt32)(byteSections[i].Count + old)).ToBytes() );
                old += byteSections[i].Count;
            }

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