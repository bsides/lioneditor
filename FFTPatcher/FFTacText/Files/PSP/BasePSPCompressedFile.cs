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

using FFTPatcher.Datatypes;

namespace FFTPatcher.TextEditor.Files.PSP
{
    /// <summary>
    /// Represents a file on the PSP that is compressed.
    /// </summary>
    public abstract class BasePSPCompressedFile : AbstractCompressedFile
    {

        #region Properties (1)


        /// <summary>
        /// Gets the character map that is used for this file.
        /// </summary>
        /// <value></value>
        public override GenericCharMap CharMap
        {
            get { return TextUtilities.PSPMap; }
        }


        #endregion Properties

        public override IList<PatchedByteArray> GetAllPatches()
        {
            var result = new List<PatchedByteArray>();
            byte[] bytes = ToByteArray();
            foreach( var kvp in Locations )
            {
                result.Add( new PatchedByteArray( (FFTPack.Files)kvp.Key, kvp.Value, bytes ) );
            }
            return result;
        }

        public override IList<PatchedByteArray> GetAllPatches( IDictionary<string, byte> dteTable )
        {
            var result = new List<PatchedByteArray>();
            byte[] bytes = ToByteArray( dteTable );
            Locations.ForEach( kvp => result.Add( new PatchedByteArray( (FFTPack.Files)kvp.Key, kvp.Value, bytes ) ) );
            return result;
        }
    }
}
