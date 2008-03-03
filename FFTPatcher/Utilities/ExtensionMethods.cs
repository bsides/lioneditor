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
using System.IO;
using System.Text;
using FFTPatcher.Datatypes;

namespace System.Runtime.CompilerServices
{
    public class ExtensionAttribute : Attribute
    {
    }
}

namespace FFTPatcher
{
    /// <summary>
    /// Extension methods for various types.
    /// </summary>
    public static partial class ExtensionMethods
    {
        public static int LastIndexOf<T>( this IList<T> list, T value ) where T : IEquatable<T>
        {
            for( int i = list.Count - 1; i >= 0; i-- )
            {
                if( list[i].Equals( value ) )
                {
                    return i;
                }
            }

            return -1;
        }

        public static void AddRange<T>( this IList<T> list, IEnumerable<T> items )
        {
            foreach( T item in items )
            {
                list.Add( item );
            }
        }

        public static T[] ToArray<T>( this IList<T> list )
        {
            T[] result = new T[list.Count];
            list.CopyTo( result, 0 );
            return result;
        }

        public static void WriteArrayToPosition( this FileStream stream, byte[] array, long position )
        {
            stream.Seek( position, SeekOrigin.Begin );
            stream.Write( array, 0, array.Length );
        }

        public static void WriteArrayToPositions( this FileStream stream, byte[] array, params long[] positions )
        {
            foreach( long position in positions )
            {
                stream.WriteArrayToPosition( array, position );
            }
        }

        /// <summary>
        /// Converts this string to an array of bytes.
        /// Each character in the string should be a single byte ASCII character.
        /// </summary>
        public static byte[] ToByteArray( this string s )
        {
            byte[] result = new byte[s.Length];
            for( int i = 0; i < s.Length; i++ )
            {
                result[i] = (byte)s[i];
            }

            return result;
        }

        /// <summary>
        /// Adds <paramref name="lines"/> to the StringBuilder.
        /// </summary>
        public static void AppendLines( this StringBuilder sb, IEnumerable<string> lines )
        {
            foreach( string line in lines )
            {
                sb.Append( line + "\n" );
            }
        }

        /// <summary>
        /// Adds lines of text in groups of a specified size to the StringBuilder.
        /// </summary>
        /// <param name="groupSize">Number of strings in each group</param>
        /// <param name="groupName">What to name each group.</param>
        /// <param name="lines">Lines to add</param>
        public static void AddGroups( this StringBuilder sb, int groupSize, string groupName, List<string> lines )
        {
            if( lines.Count == 0 )
            {
                return;
            }
            else if( lines.Count <= groupSize )
            {
                if( groupName != string.Empty )
                    sb.Append( groupName + "\n" );
                sb.AppendLines( lines );
            }
            else
            {
                int i = 0;
                int j = 1;
                for( i = 0; (i + 1) * groupSize < lines.Count; i++ )
                {
                    if( groupName != string.Empty )
                        sb.Append( string.Format( "{0} (part {1})\n", groupName, j++ ) );
                    sb.AppendLines( lines.Sub( i * groupSize, (i + 1) * groupSize - 1 ) );
                }

                if( groupName != string.Empty )
                    sb.Append( string.Format( "{0} (part {1})\n", groupName, j++ ) );
                sb.AppendLines( lines.Sub( i * groupSize, lines.Count - 1 ) );
            }
        }

        /// <summary>
        /// Gets the upper nibble of this byte.
        /// </summary>
        public static byte GetUpperNibble( this byte b )
        {
            return (byte)((b & 0xF0) >> 4);
        }

        /// <summary>
        /// Gets the lower nibble of this byte.
        /// </summary>
        public static byte GetLowerNibble( this byte b )
        {
            return (byte)(b & 0x0F);
        }

        /// <summary>
        /// Converts this into a pair of big endian bytes.
        /// </summary>
        public static byte[] ToBytes( this UInt16 value )
        {
            byte[] result = new byte[2];
            result[0] = (byte)(value & 0xFF);
            result[1] = (byte)((value >> 8) & 0xFF);
            return result;
        }

        /// <summary>
        /// Converts this into a set of four big endian bytes.
        /// </summary>
        public static byte[] ToBytes( this UInt32 value )
        {
            byte[] result = new byte[4];
            result[0] = (byte)(value & 0xFF);
            result[1] = (byte)((value >> 8) & 0xFF);
            result[2] = (byte)((value >> 16) & 0xFF);
            result[3] = (byte)((value >> 24) & 0xFF);
            return result;
        }

        /// <summary>
        /// Converts this array of bytes into a UInt32.
        /// </summary>
        public static UInt32 ToUInt32( this byte[] bytes )
        {
            UInt32 result = 0;
            result += bytes[0];
            result += (UInt32)(bytes[1] << 8);
            result += (UInt32)(bytes[2] << 16);
            result += (UInt32)(bytes[3] << 24);

            return result;
        }

        /// <summary>
        /// Converts this to a string.
        /// </summary>
        public static string ToUTF8String( this byte[] bytes )
        {
            if( (bytes[0] == 0xef) && (bytes[1] == 0xbb) && (bytes[2] == 0xbf) )
            {
                return Encoding.UTF8.GetString( bytes, 3, bytes.Length - 3 );
            }
            else
            {
                return Encoding.UTF8.GetString( bytes );
            }
        }
    }
}
