﻿/*
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

namespace FFTPatcher.TextEditor.Files.PSX
{
    public class ATTACKOUT : BasePSXFile
    {
        protected override int NumberOfSections { get { return 3; } }

        private static Dictionary<string, long> locations;
        public override IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "EVENT/ATTACK.OUT", 0xE2B0 );
                    locations.Add( "EVENT/SMALL.OUT", 0x6C );
                }

                return locations;
            }
        }

        private static ATTACKOUT Instance { get; set; }
        private static string[] sectionNames = new string[3] { "", "Unit names", "Job names" };

        private static string[][] entryNames;

        public override IList<string> SectionNames { get { return sectionNames; } }
        public override IList<IList<string>> EntryNames { get { return entryNames; } }
        public override int MaxLength { get { return 0x1E67; } }

        static ATTACKOUT()
        {
            Instance = new ATTACKOUT( PSXResources.ATTACK_OUT_partial );

            entryNames = new string[3][];
            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[Instance.Sections[i].Count];
            }

            entryNames[2] = AllJobs.PSXNames;
        }

        private ATTACKOUT( IList<byte> bytes )
            : base( bytes )
        {
        }

        public static ATTACKOUT GetInstance()
        {
            return Instance;
        }
    }
}
