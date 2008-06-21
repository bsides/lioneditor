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

namespace FFTPatcher.TextEditor.Files.PSX
{
    public class BUNITOUT : BasePSXCompressedFile
    {
        private const int numberOfSections = 21;
        private const string filename = "BUNIT.OUT";
        private static Dictionary<string, long> locations;
        private static readonly string[][] entryNames;
        private static readonly string[] sectionNames;
        private const int maxLength = 0x13663;

        static BUNITOUT()
        {
            sectionNames = new string[numberOfSections];
            entryNames = new string[numberOfSections][];
            for( int i = 0; i < numberOfSections; i++ )
            {
                entryNames[i] = new string[1024];
            }
        }

        protected override int NumberOfSections
        {
            get { return numberOfSections; }
        }

        public override IList<IList<string>> EntryNames
        {
            get { return entryNames; }
        }

        public override string Filename
        {
            get { return filename; }
        }

        public override IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "EVENT/BUNIT.OUT", 0x10DF8 );
                }

                return locations;
            }
        }

        public override int MaxLength
        {
            get { return maxLength; }
        }

        public override IList<string> SectionNames
        {
            get { return sectionNames; }
        }

        private BUNITOUT()
        {
        }

        public BUNITOUT( IList<byte> bytes )
            : base( bytes )
        {
        }
    }
}
