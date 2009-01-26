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
        IDictionary<Enum, long> Locations { get; }

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
        /// Creates a byte array representing this file with DTE substitutions performed as specified.
        /// </summary>
        byte[] ToByteArray( IDictionary<string, byte> dteTable );

        /// <summary>
        /// Gets all patches that this file needs to apply to the ISO for full functionality.
        /// </summary>
        IList<PatchedByteArray> GetAllPatches();

        IList<PatchedByteArray> GetAllPatches( IDictionary<string, byte> dteTable );

        /// <summary>
        /// Determines how many bytes would be saved if the specified string could be replaced with a single byte.
        /// </summary>
        IDictionary<string, int> CalculateBytesSaved( Set<string> replacements );


        /// <summary>
        /// Determines if this file will require DTE in order to fit on disc.
        /// </summary>
        bool IsDTENeeded();

        /// <summary>
        /// Gets the DTE pairs that this file needs in order to fit on disc, given a set of possible DTE pairs
        /// and a set of the pairs that are already required.
        /// </summary>
        Set<KeyValuePair<string, byte>> GetPreferredDTEPairs( Set<string> replacements, Set<KeyValuePair<string, byte>> currentPairs, Stack<byte> dteBytes );

    }
}
