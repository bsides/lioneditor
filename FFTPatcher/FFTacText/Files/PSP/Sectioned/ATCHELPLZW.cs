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

namespace FFTPatcher.TextEditor.Files.PSP
{
    /// <summary>
    /// Represents the text for the ATCHELP.LZW file.
    /// </summary>
    public class ATCHELPLZW : BasePSPSectionedFile, IFFTPackFile
    {

		#region Static Fields (2) 

        private static IList<IList<int>> disallowedEntries;
        private static Dictionary<Enum, long> locations;

		#endregion Static Fields 

		#region Fields (3) 

        private const FFTPack.Files fftpackIndex = FFTPack.EVENT.ATCHELP_LZW;
        private const string filename = "ATCHELP.LZW";
        private const int numberOfSections = 21;

		#endregion Fields 

		#region Properties (6) 


        /// <summary>
        /// Gets the index of this file in fftpack.bin
        /// </summary>
        public FFTPack.Files Index
        {
            get { return fftpackIndex; }
        }



        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected override int NumberOfSections
        {
            get { return numberOfSections; }
        }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        public override string Filename { get { return filename; } }

        /// <summary>
        /// Gets the filenames and locations for this file.
        /// </summary>
        public override IDictionary<Enum, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<Enum, long>();
                    locations.Add( Index, 0x00 );
                }
                return locations;
            }
        }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        public override int MaxLength
        {
            get { return 0x1F834; }
        }

        public override string this[int section, int entry]
        {
            get { return base[section, entry]; }
            set
            {
                if( !disallowedEntries[section].Contains( entry ) )
                {
                    base[section, entry] = value;
                }
            }
        }


		#endregion Properties 

		#region Constructors (3) 

        static ATCHELPLZW()
        {
            disallowedEntries = new int[numberOfSections][] {
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[261] { 
                     0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
                    20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
                    40, 41, 42, 43, 44,     46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59,
                    60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79,
                    80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99,
                    100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119,
                    120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139,
                    140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159,
                    160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179,
                    180, 181, 182, 183,      185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199,
                    200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218,
                         221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239,
                    240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259,
                    260, 261, 262, 263, 264 },
                new int[0],
                new int[0],
                new int[0],
                new int[0],
                new int[0] };
        }

        private ATCHELPLZW()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ATCHELPLZW"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        public ATCHELPLZW( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

		#region Methods (1) 


        /// <summary>
        /// Gets a list of indices for named sections.
        /// </summary>
        public override IList<NamedSection> GetNamedSections()
        {
            var result = base.GetNamedSections();
            result.Add( new NamedSection( this, SectionType.JobDescriptions, 12, true, 169 ) );
            result.Add( new NamedSection( this, SectionType.ItemDescriptions, 13, true, 316 ) );
            result.Add( new NamedSection( this, SectionType.AbilityDescriptions, 15 ) );
            result.Add( new NamedSection( this, SectionType.SkillsetDescriptions, 19, true, 227 ) );

            return result;
        }


		#endregion Methods 

    }
}
