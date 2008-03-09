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

namespace FFTPatcher
{
    /// <summary>
    /// Utilities for decompressing GZip files.
    /// </summary>
    public static class GZip
    {

		#region Methods (1) 


        /// <summary>
        /// Decompresses a file.
        /// </summary>
        public static byte[] Decompress( IList<byte> bytes )
        {
            List<byte> result = new List<byte>( 1024 * 1024 );
            using( GZipStream stream = new GZipStream( new MemoryStream( bytes.ToArray() ), CompressionMode.Decompress, false ) )
            {
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
                    result.AddRange( buffer.Sub( 0, b - 1 ) );
                }
            }

            return result.ToArray();
        }

        public static IList<byte> Compress( byte[] bytes )
        {
            MemoryStream ms = new MemoryStream();
            using( GZipStream stream = new GZipStream( ms, CompressionMode.Compress, true ) )
            {
                stream.Write( bytes, 0, bytes.Length );
            }

            byte[] result = new byte[ms.Length];
            ms.Seek( 0, SeekOrigin.Begin );
            ms.Read( result, 0, result.Length );
            ms.Close();
            ms.Dispose();

            return result;
        }


		#endregion Methods 

    }
}
