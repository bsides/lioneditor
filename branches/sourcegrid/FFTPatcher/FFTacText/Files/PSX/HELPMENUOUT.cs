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
    public class HELPMENUOUT : BasePSXCompressedFile
    {

        #region Static Fields (3)

        public static string[][] entryNames;
        private static Dictionary<string, long> locations;
        private static string[] sectionNames;

        #endregion Static Fields

        #region Fields (2)

        private const string filename = "HELPMENU.OUT";
        private const int numberOfSections = 21;

        #endregion Fields

        #region Properties (6)


        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected override int NumberOfSections { get { return numberOfSections; } }

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
                    locations.Add( "EVENT/HELPMENU.OUT", 0x1B30 );
                    locations.Add( "WORLD/WORLD.BIN", 0x777E0 );
                }

                return locations;
            }
        }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        /// <value></value>
        public override int MaxLength { get { return 0x169C0; } }

        /// <summary>
        /// Gets a collection of strings with a description of each section in this file.
        /// </summary>
        /// <value></value>
        public override IList<string> SectionNames { get { return sectionNames; } }


        #endregion Properties

        #region Constructors (3)

        private HELPMENUOUT()
        {
        }

        private static readonly int[] sectionLengths = new int[21] {
            1, 40, 155, 1, 1, 1, 1,
            1, 1, 1, 1, 64, 160, 256,
            1, 512, 1, 1, 1, 188, 40 };

        static HELPMENUOUT()
        {
            sectionNames = new string[21] {
                "","Help","Menu/Options Help","","","","",
                "","","","","Terrain descriptions","Job descriptions","Item descriptions",
                "","Ability descriptions","","","","Skillset descriptions","Status descriptions" };
            entryNames = new string[numberOfSections][];

            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[sectionLengths[i]];
            }

            entryNames[1] = new string[40] {
                "Unit #", "Level", "HP", "MP", "CT", "AT", "Exp", "Name",
                "Brave", "Faith", "", "Move", "Jump", "", "", "",
                "Speed", "ATK", "Weapon ATK", "", "Eva%", "SEv%", "AEv%", "Phys land effect",
                "Magic land effect", "Estimated", "Hit rate", "Aries", "Taurus", "Gemini", "Cancer", "Leo", 
                "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces", "Serpentarius" };
            entryNames[12] = FFTPatcher.Datatypes.AllJobs.PSXNames;
            entryNames[13] = FFTPatcher.Datatypes.Item.PSXNames.ToArray();
            entryNames[15] = FFTPatcher.Datatypes.AllAbilities.PSXNames;
            List<string> temp = new List<string>( FFTPatcher.Datatypes.SkillSet.PSXNames.Sub( 0, 175 ) );
            temp.AddRange( new string[13] );
            entryNames[19] = temp.ToArray();
            entryNames[20] = FFTPatcher.PSXResources.Statuses;
        }

        public HELPMENUOUT( IList<byte> bytes )
            : base( bytes )
        {
        }

        #endregion Constructors

        #region Methods (1)


        protected override IList<byte> ToFinalBytes()
        {
            return Compress();
        }


        #endregion Methods

    }
}