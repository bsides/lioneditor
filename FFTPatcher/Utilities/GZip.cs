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
using System.IO;
using System.IO.Compression;
using FFTPatcher.Datatypes;

namespace FFTPatcher
{
    public static class GZip
    {
        public static byte[] Decompress( byte[] bytes )
        {
            List<byte> result = new List<byte>( 1024 * 1024 );
            GZipStream stream = new GZipStream( new MemoryStream( bytes ), CompressionMode.Decompress, false );
            byte[] buffer = new byte[2048];

            int b = 0;
            while( true )
            {
                b = stream.Read( buffer, 0, 2048 );
                if( b < 2048 )
                    break;
                else
                    result.AddRange( buffer );
            }

            if( b > 0 )
            {
                result.AddRange( new SubArray<byte>( buffer, 0, b - 1 ) );
            }

            return result.ToArray();
        }
    }
}
