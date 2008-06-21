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
    public class HELPLZW : BasePSXCompressedFile
    {

		#region Static Fields (1) 

        private static Dictionary<string, long> locations;

		#endregion Static Fields 

		#region Fields (3) 

        private const string filename = "HELP.LZW";
        private const int maxLength = 0x169C0;
        private const int numberOfSections = 21;

		#endregion Fields 

		#region Properties (4) 


        protected override int NumberOfSections
        {
            get { return numberOfSections; }
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
                    locations.Add( "EVENT/HELP.LZW", 0x00 );
                }

                return locations;
            }
        }

        public override int MaxLength
        {
            get { return maxLength; }
        }


		#endregion Properties 

		#region Constructors (2) 

        private HELPLZW()
        {
        }

        public HELPLZW( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

    }
}
