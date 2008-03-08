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
    public class WORLDLZW : BasePSXSectionedFile
    {

		#region Fields (3) 

        public static string[][] entryNames;
        private static Dictionary<string, long> locations;
        private static string[] sectionNames;

		#endregion Fields 

		#region Properties (5) 


        protected override int NumberOfSections { get { return 32; } }

        public override IList<IList<string>> EntryNames { get { return entryNames; } }

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

        public override int MaxLength { get { return 0xE2DD; } }

        public override IList<string> SectionNames { get { return sectionNames; } }


		#endregion Properties 

		#region Constructors (2) 

        static WORLDLZW()
        {
            sectionNames = new string[32] {
                "","","","","","","Job names","Item names",
                "Character names","Character names","Battle menus","Help text","Errand names","","Ability names","Errand rewards",
                "??","","Location names","Blank","Map menu","Event names","Skillsets","Tavern menu",
                "Tutorial","Brave story","Errand names","Unexplored Land","Treasure","Record","Person","Errand objectives"};
            entryNames = new string[32][];

            int[] sectionLengths = new int[32] {
                1,1,1,1,1,1,155,257,
                1024,1024,11,2,96,1,513,77,
                41,1,44,24,1,115,189,272,
                24,64,96,16,47,64,1024,96 };
            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[sectionLengths[i]];
            }
            entryNames[6] = FFTPatcher.Datatypes.AllJobs.PSXNames.Sub( 0, 154 ).ToArray();
            IList<string> temp = new List<string>( FFTPatcher.Datatypes.Item.PSXNames.Sub( 0 ).ToArray() );
            temp.AddRange( new string[sectionLengths[7] - temp.Count] );
            entryNames[7] = temp.ToArray();
            temp = new List<string>( AllAbilities.PSXNames );
            temp.AddRange( new string[sectionLengths[14] - temp.Count] );
            entryNames[14] = temp.ToArray();
            temp = new List<string>( SkillSet.PSXNames.Sub( 0, 175 ) );
            temp.AddRange( new string[sectionLengths[22] - temp.Count] );
            entryNames[22] = temp.ToArray();
        }

        public WORLDLZW( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

    }
}