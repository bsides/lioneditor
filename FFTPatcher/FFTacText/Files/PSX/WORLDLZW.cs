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

namespace FFTPatcher.TextEditor.Files.PSX
{
    public class WORLDLZW : BasePSXFile
    {
        private static Dictionary<string, long> locations;
        public override IDictionary<string, long> Locations
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

        protected override int NumberOfSections { get { return 32; } }

        private static WORLDLZW Instance { get; set; }
        private static string[] sectionNames;

        public static string[][] entryNames;

        public override IList<string> SectionNames { get { return sectionNames; } }
        public override IList<IList<string>> EntryNames { get { return entryNames; } }

        public override int MaxLength { get { return 0xE2DD; } }

        static WORLDLZW()
        {
            Instance = new WORLDLZW( PSXResources.WORLD_LZW );

            sectionNames = new string[32] {
                "","","","","","","Job names","Item names",
                "Character names","Character names","Battle menus","Help text","Errand names","","Ability names","Errand rewards",
                "??","","Location names","Blank","Map menu","Event names","Skillsets","Tavern menu",
                "Tutorial","Brave story","Errand names","Unexplored Land","Treasure","Record","Person","Errand objectives"};
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
            : base( bytes )
        {
        }

        public static WORLDLZW GetInstance()
        {
            return Instance;
        }

   }
}