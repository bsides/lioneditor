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


namespace FFTPatcher.TextEditor.Files.PSX
{
    public abstract class BasePSXCompressedFile : AbstractCompressedFile
    {

		#region Properties (2) 


        public override TextUtilities.CharMapType CharMap { get { return TextUtilities.CharMapType.PSX; } }

        public override int EstimatedLength
        {
            get { return (int)(base.EstimatedLength * 0.65346430772862594919277); }
        }


		#endregion Properties 

    }
}
