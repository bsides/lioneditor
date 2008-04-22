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
    public abstract class BasePSXSectionedFile : AbstractStringSectioned
    {

		#region Properties (1) 


        public override GenericCharMap CharMap { get { return TextUtilities.PSXMap; } }


		#endregion Properties 

		#region Constructors (2) 

        protected BasePSXSectionedFile()
        {
        }

        protected BasePSXSectionedFile( IList<byte> bytes )
            : base( bytes )
        {
        }

		#endregion Constructors 

    }
}
