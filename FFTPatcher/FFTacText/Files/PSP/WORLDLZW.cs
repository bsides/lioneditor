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

namespace FFTPatcher.TextEditor.Files.PSP
{
    public class WORLDLZW : BasePSPSectionedFile, IFFTPackFile
    {

		#region Static Fields (3) 

        private static string[][] entryNames;
        private static Dictionary<string, long> locations;
        private static string[] sectionNames;

		#endregion Static Fields 

		#region Fields (2) 

        private const int fftpackIndex = 44;
        private const string filename = "WORLD.LZW";
        private static readonly int[] exclusions;

        /// <summary>
        /// Gets the entries that are excluded from compression.
        /// </summary>
        /// <value></value>
        public override IList<int> ExcludedEntries
        {
            get { return exclusions; }
        }

		#endregion Fields 

		#region Properties (7) 


        /// <summary>
        /// Gets the index of this file in fftpack.bin
        /// </summary>
        public int Index
        {
            get { return fftpackIndex; }
        }



        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected override int NumberOfSections { get { return 32; } }

        /// <summary>
        /// Gets a collection of lists of strings, each string being a description of an entry in this file.
        /// </summary>
        /// <value></value>
        public override IList<IList<string>> EntryNames { get { return entryNames; } }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <value></value>
        public override string Filename { get { return filename; } }

        /// <summary>
        /// Gets the filenames and locations for this file.
        /// </summary>
        /// <value></value>
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

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        /// <value></value>
        public override int MaxLength
        {
            get { return 0x14000; }
        }

        /// <summary>
        /// Gets a collection of strings with a description of each section in this file.
        /// </summary>
        /// <value></value>
        public override IList<string> SectionNames { get { return sectionNames; } }


		#endregion Properties 

		#region Constructors (3) 

        static WORLDLZW()
        {
            sectionNames = new string[32] {
                "","","","","","","Job names","Item names",
                "Unit names", "Unit names", "Battle menus","Map help", "Errand names","","Ability names","Feat rewards",
                "Tutorial menus","","Location names","Empty","Map menu text","Event names","Skillset names","Tavern text",
                "Tutorial menus","Rumors/Multiplayer text", "Feats names", "Wonders names", "Artefacts names","Events names","Personae names","Feats descriptions" };
            int[] sectionLengths = new int[32] {
                1,1,1,1,1,1,169,316,
                1024,1024,11,2,96,1,512,77,
                76,1,44,24,1,115,227,374,
                32,185,96,16,47,78,1024,96 };
            entryNames = new string[32][];
            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[sectionLengths[i]];
            }

            entryNames[6] = FFTPatcher.Datatypes.AllJobs.PSPNames;
            entryNames[7] = FFTPatcher.Datatypes.Item.PSPNames.ToArray();
            entryNames[14] = FFTPatcher.Datatypes.AllAbilities.PSPNames;
            List<string> temp = new List<string>( FFTPatcher.Datatypes.SkillSet.PSPNames.Sub( 0, 175 ) );
            temp.AddRange( new string[48] );
            temp.AddRange( FFTPatcher.Datatypes.SkillSet.PSPNames.Sub( 224, 226 ) );
            entryNames[22] = temp.ToArray();

            exclusions = new int[3];
            exclusions[0] = 1 + 1 + 1 + 1 + 1 + 1 + 169 + 316 + 1024 + 1024;
            exclusions[1] = 1 + 1 + 1 + 1 + 1 + 1 + 169 + 316 + 1024 + 1024 + 10;
            exclusions[2] = 1 + 1 + 1 + 1 + 1 + 1 + 169 + 316 + 1024 + 1024 + 11 + 2 + 96 + 1 + 512 + 77 + 76 + 1 + 44 + 24;
        }

        private WORLDLZW()
        {
        }

        public WORLDLZW( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

    }
}
