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
using FFTPatcher.Datatypes;
using System;

namespace FFTPatcher.TextEditor.Files.PSX
{
    public class WLDHELPLZW : AbstractStringSectioned
    {
        private static Dictionary<string, long> locations;
        public override IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "EVENT/WLDHELP.LZW", 0x00 );
                }

                return locations;
            }
        }

        private const int numberOfSections = 21;
        protected override int NumberOfSections { get { return numberOfSections; } }

        private static WLDHELPLZW Instance { get; set; }
        private static string[] sectionNames;

        public static string[][] entryNames;

        public override IList<string> SectionNames { get { return sectionNames; } }
        public override IList<IList<string>> EntryNames { get { return entryNames; } }

        public override int MaxLength { get { return 0x01ADE4; } }

        static WLDHELPLZW()
        {
            Instance = new WLDHELPLZW( Properties.Resources.WLDHELP_LZW );

            sectionNames = new string[numberOfSections];
            entryNames = new string[numberOfSections][];
            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[Instance.Sections[i].Count];
            }
        }

        private WLDHELPLZW( IList<byte> bytes )
        {
            Sections = new List<IList<string>>( NumberOfSections );
            for( int i = 0; i < NumberOfSections; i++ )
            {
                uint start = Utilities.BytesToUInt32( new SubArray<byte>( bytes, i * 4, i * 4 + 3 ) );
                uint stop = Utilities.BytesToUInt32( new SubArray<byte>( bytes, (i + 1) * 4, (i + 1) * 4 + 3 ) ) - 1;
                if( i == NumberOfSections - 1 )
                {
                    stop = (uint)bytes.Count - 1 - dataStart;
                }

                IList<byte> thisSection = TextUtilities.Decompress(
                    bytes,
                    new SubArray<byte>( bytes, (int)(start + dataStart), (int)(stop + dataStart) ),
                    (int)(start + dataStart) );
                Sections.Add( TextUtilities.ProcessList( thisSection, TextUtilities.CharMapType.PSX ) );
            }
        }

        public static WLDHELPLZW GetInstance()
        {
            return Instance;
        }

        protected override IList<byte> ToBytes()
        {
            List<byte> bytes = new List<byte>();
            foreach( List<string> section in Sections )
            {
                foreach( string s in section )
                {
                    bytes.AddRange( TextUtilities.PSXMap.StringToByteArray( s ) );
                }
            }

            return bytes;
        }

    }
}