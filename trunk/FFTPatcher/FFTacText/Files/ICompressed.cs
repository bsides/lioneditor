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
    public interface ICompressed
    {
        /// <summary>
        /// Gets the filename.
        /// </summary>
        string Filename { get; }

        /// <summary>
        /// Compresses this instance.
        /// </summary>
        IList<byte> Compress();

        /// <summary>
        /// Occurs when the progress of a compresison operation has changed.
        /// </summary>
        event EventHandler<CompressionEventArgs> ProgressChanged;

        /// <summary>
        /// Occurs when a compression operation has finished.
        /// </summary>
        event EventHandler<CompressionEventArgs> CompressionFinished;

        /// <summary>
        /// Creates an uncompressed byte array representing this file.
        /// </summary>
        IList<byte> ToUncompressedBytes();
    }
}
