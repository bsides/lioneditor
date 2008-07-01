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

namespace FFTPatcher.TextEditor.Files.PSP
{
    /// <summary>
    /// Text for the BOOT.BIN file at location 0x32D368.
    /// </summary>
    public class BOOT32D368 : BasePSPCompressedFile, IBootBin
    {

		#region Static Fields (1) 

        private static Dictionary<int, long> locations;

		#endregion Static Fields 

		#region Fields (1) 

        private const string filename = "BOOT.BIN[0x32D368]";

		#endregion Fields 

		#region Properties (5) 


        ICollection<long> IBootBin.Locations
        {
            get { return (this as AbstractStringSectioned).Locations.Values; }
        }



        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected override int NumberOfSections
        {
            get { return 21; }
        }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <value></value>
        public override string Filename { get { return filename; } }

        /// <summary>
        /// Gets the filenames and locations for this file.
        /// </summary>
        /// <value></value>
        public override IDictionary<int, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<int, long>();
                    locations.Add( 0, 0x32D368 );
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
            get { return 0x2CF5B; }
        }


		#endregion Properties 

		#region Constructors (2) 

        private BOOT32D368()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BOOT32D368"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        public BOOT32D368( IList<byte> bytes )
        {
            Sections = new List<IList<string>>( NumberOfSections );
            for( int i = 0; i < NumberOfSections; i++ )
            {
                uint start = Utilities.BytesToUInt32( bytes.Sub( i * 4, i * 4 + 3 ) );
                uint stop = Utilities.BytesToUInt32( bytes.Sub( (i + 1) * 4, (i + 1) * 4 + 3 ) ) - 1;
                if( i == NumberOfSections - 1 )
                {
                    stop = (uint)bytes.Count - 1 - dataStart;
                }

                IList<byte> thisSection = TextUtilities.Decompress(
                    bytes,
                    bytes.Sub( (int)(start + dataStart), (int)(stop + dataStart) ),
                    (int)(start + dataStart) );
                Sections.Add( TextUtilities.ProcessList( thisSection, CharMap ) );
            }
        }

		#endregion Constructors 

		#region Methods (1) 


        /// <summary>
        /// Gets a list of indices for named sections.
        /// </summary>
        public override IList<NamedSection> GetNamedSections()
        {
            var result = base.GetNamedSections();
            result.Add( new NamedSection( this, SectionType.JobDescriptions, 12 ) );
            result.Add( new NamedSection( this, SectionType.ItemDescriptions, 13 ) );
            result.Add( new NamedSection( this, SectionType.AbilityDescriptions, 15 ) );
            result.Add( new NamedSection( this, SectionType.SkillsetDescriptions, 19 ) );
            return result;
        }


		#endregion Methods 

    }
}
