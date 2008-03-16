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

namespace FFTPatcher.TextEditor.Files
{
    /// <summary>
    /// A part of a <see cref="IPartitionedFile"/>.
    /// </summary>
    public interface IPartition
    {
        /// <summary>
        /// Gets a collection of strings, each string being a description of an entry in this partition.
        /// </summary>
        IList<string> EntryNames { get; }

        /// <summary>
        /// Gets the entries.
        /// </summary>
        IList<string> Entries { get; }

        /// <summary>
        /// Gets the maximum length of this partition.
        /// </summary>
        int MaxLength { get; }

        /// <summary>
        /// Gets the current length of this partition.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Creates a byte array representing this partition.
        /// </summary>
        byte[] ToByteArray();
    }
}
