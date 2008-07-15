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

namespace FFTPatcher
{
    public class PatchedByteArray
    {

		#region Properties (3) 


        public byte[] Bytes { get; private set; }

        public long Offset { get; private set; }

        public int Sector { get; private set; }


		#endregion Properties 

		#region Constructors (1) 

        public PatchedByteArray( PsxIso.Sectors sector, long offset, byte[] bytes )
            : this( (int)sector, offset, bytes )
        {
        }

        public PatchedByteArray( int sector, long offset, byte[] bytes )
        {
            Sector = sector;
            Offset = offset;
            Bytes = bytes;
        }

		#endregion Constructors 

    }
}
