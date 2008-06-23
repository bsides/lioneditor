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
    /// <summary>
    /// Represents files in the Playstation version that are partitioned.
    /// </summary>
    public abstract class BasePSXPartitionedFile : AbstractPartitionedFile
    {

        #region Properties (1)


        /// <summary>
        /// Gets the character map used for this file.
        /// </summary>
        public override GenericCharMap CharMap { get { return TextUtilities.PSXMap; } }


        #endregion Properties

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePSXPartitionedFile"/> class.
        /// </summary>
        protected BasePSXPartitionedFile()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePSXPartitionedFile"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        protected BasePSXPartitionedFile( IList<byte> bytes )
            : base( bytes )
        {
        }

        #endregion Constructors

    }
}
