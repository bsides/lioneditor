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
using PatcherLib.Iso;

namespace PatcherLib.Datatypes
{
    public class PatchedByteArray
    {
		#region Public Properties (4) 

        public byte[] Bytes { get; private set; }

        public long Offset { get; private set; }

        public int Sector { get; private set; }

        public Enum SectorEnum { get; private set; }

		#endregion Public Properties 

		#region Constructors (4) 

        public PatchedByteArray( PsxIso.Sectors sector, long offset, byte[] bytes )
            : this( (int)sector, offset, bytes )
        {
            SectorEnum = sector;
        }

        public PatchedByteArray( PspIso.Sectors sector, long offset, byte[] bytes )
            : this( (int)sector, offset, bytes )
        {
            SectorEnum = sector;
        }

        public PatchedByteArray( FFTPack.Files file, long offset, byte[] bytes )
            : this( (int)file, offset, bytes )
        {
            SectorEnum = file;
        }

        public PatchedByteArray(Enum fileOrSector, long offset, byte[] bytes)
            : this(-1, offset, bytes)
        {
            SectorEnum = fileOrSector;
            Type t = fileOrSector.GetType();
            if (t == typeof(PspIso.Sectors))
            {
                Sector = (int)((PspIso.Sectors)fileOrSector);
            }
            else if (t == typeof(FFTPack.Files))
            {
                Sector = (int)((FFTPack.Files)fileOrSector);
            }
            else if (t == typeof(PsxIso.Sectors))
            {
                Sector = (int)((PsxIso.Sectors)fileOrSector);
            }
            else
            {
                throw new ArgumentException("fileOrSector has incorrect type");
            }
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
