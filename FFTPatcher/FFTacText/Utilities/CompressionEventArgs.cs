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
    public class CompressionEventArgs : EventArgs
    {

		#region Properties (2) 


        public int Progress { get; private set; }

        public IList<byte> Result { get; private set; }


		#endregion Properties 

		#region Constructors (2) 

        public CompressionEventArgs( int progress )
        {
            Progress = progress;
            Result = null;
        }

        public CompressionEventArgs( int progress, IList<byte> result )
            : this( progress )
        {
            Result = result;
        }

		#endregion Constructors 

    }
}
