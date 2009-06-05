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
using System.IO;

namespace PatcherLib.Datatypes
{
    public class STRPatchedByteArray : LazyLoadedPatchedByteArray 
    {
        public override byte[] GetBytes()
        {
            return base.GetBytes();
        }

        public STRPatchedByteArray(PsxIso.Sectors sector, string filename)
            : base(sector, 0, filename)
        {
        }
    }

    public class LazyLoadedPatchedByteArray : PatchedByteArray
    {
        protected string Filename { get; private set; }

        public override byte[] GetBytes()
        {
            return File.ReadAllBytes(Filename);
        }

        public LazyLoadedPatchedByteArray(PsxIso.Sectors sector, long offset, string filename)
            : this((int)sector, offset, filename)
        {
            SectorEnum = sector;
        }

        public LazyLoadedPatchedByteArray(PspIso.Sectors sector, long offset, string filename)
            : this((int)sector, offset, filename)
        {
            SectorEnum = sector;
        }

        public LazyLoadedPatchedByteArray(FFTPack.Files file, long offset, string filename)
            : this((int)file, offset, filename)
        {
            SectorEnum = file;
        }

        public LazyLoadedPatchedByteArray(Enum fileOrSector, long offset, string filename)
            : this(-1, offset, filename)
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

        public LazyLoadedPatchedByteArray(int sector, long offset, string filename)
            : base(sector, offset)
        {
            this.Filename = Path.GetFullPath(filename);
        }

    }

    public class PatchedByteArray
    {
		#region Public Properties (4) 

        public virtual byte[] GetBytes()
        {
            return bytes;
        }

        private byte[] bytes;

        public long Offset { get; protected set; }

        public int Sector { get; protected set; }

        public Enum SectorEnum { get; protected set; }

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

        public PatchedByteArray(int sector, long offset, byte[] bytes)
            : this(sector, offset)
        {
            this.bytes = bytes;
        }

        protected PatchedByteArray(int sector, long offset)
        {
            Sector = sector;
            Offset = offset;
        }

		#endregion Constructors 
    }
}
