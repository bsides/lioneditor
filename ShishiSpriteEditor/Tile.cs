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
using System.Drawing;

namespace FFTPatcher.SpriteEditor
{
    [Serializable]
    public class Tile
    {

		#region Static Fields (1) 

        private static Dictionary<byte, Size> sizes;

		#endregion Static Fields 

		#region Properties (3) 


        public Point Location { get; private set; }

        public Rectangle Rectangle { get; private set; }

        public bool Reverse { get; private set; }


		#endregion Properties 

		#region Constructors (1) 

        static Tile()
        {
            sizes = new Dictionary<byte, Size>( 16 );
            sizes.Add( 0, new Size( 8, 8 ) );
            sizes.Add( 1, new Size( 16, 8 ) );
            sizes.Add( 2, new Size( 16, 16 ) );
            sizes.Add( 3, new Size( 16, 24 ) );
            sizes.Add( 4, new Size( 24, 8 ) );
            sizes.Add( 5, new Size( 24, 16 ) );
            sizes.Add( 6, new Size( 24, 24 ) );
            sizes.Add( 7, new Size( 32, 8 ) );
            sizes.Add( 8, new Size( 32, 16 ) );
            sizes.Add( 9, new Size( 32, 24 ) );
            sizes.Add( 10, new Size( 32, 32 ) );
            sizes.Add( 11, new Size( 32, 40 ) );
            sizes.Add( 12, new Size( 48, 16 ) );
            sizes.Add( 13, new Size( 40, 32 ) );
            sizes.Add( 14, new Size( 48, 48 ) );
            sizes.Add( 15, new Size( 56, 56 ) );
        }

		#endregion Constructors 

		#region Methods (1) 


        internal Tile( IList<byte> bytes, int yOffset )
        {
            int xByte = bytes[0];
            int yByte = bytes[1];
            byte x = (byte)Math.Abs( xByte - 129 );
            byte y = (byte)Math.Abs( yByte - 129 );
            ushort flags = (ushort)(bytes[2] + bytes[3] * 256);
            Reverse = (flags & 0x4000) == 0x4000;
            byte f = (byte)((flags >> 10) & 0x0F);
            int tileX = (flags & 0x1F) * 8;
            int tileY = ((flags >> 5) & 0x1F) * 8 + yOffset;
            Location = new Point( x, y );
            Rectangle = new Rectangle( new Point( tileX, tileY ), sizes[f] );
        }


		#endregion Methods 

    }
}
