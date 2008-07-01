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

namespace FFTPatcher.TextEditor.Files
{
    /// <summary>
    /// A file that can be edited.
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Gets the filenames and locations for this file.
        /// </summary>
        IDictionary<int, long> Locations { get; }

        /// <summary>
        /// Gets the charmap to use for this file.
        /// </summary>
        GenericCharMap CharMap { get; }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        int MaxLength { get; }

        /// <summary>
        /// Creates a byte array representing this file.
        /// </summary>
        byte[] ToByteArray();

        /// <summary>
        /// Gets other patches necessary to make modifications to this file functional.
        /// </summary>
        IList<PatchedByteArray> GetOtherPatches();
    }
}
