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

namespace FFTPatcher.TextEditor
{
    /// <summary>
    /// Contains data for reporting the progress of a compression operation.
    /// </summary>
    public class CompressionEventArgs : EventArgs
    {

        #region Properties (2)


        /// <summary>
        /// Gets the progress so far.
        /// </summary>
        public int Progress { get; private set; }

        /// <summary>
        /// Gets the result of the compression.
        /// </summary>
        public IList<byte> Result { get; private set; }


        #endregion Properties

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionEventArgs"/> class.
        /// </summary>
        /// <param name="progress">The progress so far.</param>
        public CompressionEventArgs( int progress )
        {
            Progress = progress;
            Result = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionEventArgs"/> class.
        /// </summary>
        /// <param name="progress">The progress so far.</param>
        /// <param name="result">The result if the progress is finished.</param>
        public CompressionEventArgs( int progress, IList<byte> result )
            : this( progress )
        {
            Result = result;
        }

        #endregion Constructors

    }
}
