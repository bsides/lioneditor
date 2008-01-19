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
using FFTPatcher.Datatypes;

namespace FFTPatcher
{
    /// <summary>
    /// Utilities for generating cheat codes.
    /// </summary>
    public static class Codes
    {
        private static List<string> GeneratePSPCodes( byte[] oldBytes, byte[] newBytes, UInt32 offset )
        {
            List<string> codes = new List<string>();
            bool[] patched = new bool[newBytes.Length];

            uint i = 0;
            if( offset % 4 > 0 )
            {
                i = 4 - (offset % 4);
            }

            for( ; i < newBytes.Length; i += 4 )
            {
                if( ((i + 3) < newBytes.Length) &&
                    ((newBytes[i] != oldBytes[i]) &&
                    (newBytes[i + 1] != oldBytes[i + 1]) &&
                    (newBytes[i + 2] != oldBytes[i + 2]) &&
                    (newBytes[i + 3] != oldBytes[i + 3])) &&
                    (!patched[i]) &&
                    (!patched[i + 1]) &&
                    (!patched[i + 2]) &&
                    (!patched[i + 3]) )
                {
                    UInt32 addy = (UInt32)(offset + i);
                    string code = string.Format( "_L 0x2{0:X7} 0x{4:X2}{3:X2}{2:X2}{1:X2}",
                        addy, newBytes[i], newBytes[i + 1], newBytes[i + 2], newBytes[i + 3] );
                    codes.Add( code );
                    patched[i] = true;
                    patched[i + 1] = true;
                    patched[i + 2] = true;
                    patched[i + 3] = true;
                }
            }

            for( i = offset % 2; i < newBytes.Length; i += 2 )
            {
                if( ((i + 1) < newBytes.Length) &&
                    ((newBytes[i] != oldBytes[i]) &&
                    (newBytes[i + 1] != oldBytes[i + 1])) &&
                    (!patched[i]) && (!patched[i + 1]) )
                {
                    UInt32 addy = (UInt32)(offset + i);
                    string code = string.Format( "_L 0x1{0:X7} 0x0000{2:X2}{1:X2}",
                        addy, newBytes[i], newBytes[i + 1] );
                    codes.Add( code );
                    patched[i] = true;
                    patched[i + 1] = true;
                }
            }

            for( i = 0; i < newBytes.Length; i++ )
            {
                if( (newBytes[i] != oldBytes[i]) && (!patched[i]) )
                {
                    UInt32 addy = (UInt32)(offset + i);
                    string code = string.Format( "_L 0x0{0:X7} 0x000000{1:X2}",
                        addy, newBytes[i] );
                    codes.Add( code );
                    patched[i] = true;
                }
            }

            codes.Sort( new Comparison<string>(
                delegate( string s, string t )
                {
                    return s.Substring( 6 ).CompareTo( t.Substring( 6 ) );
                } ) );

            return codes;
        }

        private static List<string> GeneratePSXCodes( byte[] oldBytes, byte[] newBytes, UInt32 offset )
        {
            List<string> codes = new List<string>();
            bool[] patched = new bool[newBytes.Length];
            for( uint i = offset % 2; i < newBytes.Length; i += 2 )
            {
                if( ((i + 1) < newBytes.Length) &&
                    ((newBytes[i] != oldBytes[i]) &&
                    (newBytes[i + 1] != oldBytes[i + 1])) &&
                    (!patched[i]) && (!patched[i + 1]) )
                {
                    UInt32 addy = (UInt32)(offset + i);
                    string code = string.Format( "80{0:X6} {2:X2}{1:X2}",
                        addy, newBytes[i], newBytes[i + 1] );
                    codes.Add( code );
                    patched[i] = true;
                    patched[i + 1] = true;
                }
            }
            for( int i = 0; i < newBytes.Length; i++ )
            {
                if( (newBytes[i] != oldBytes[i]) && (!patched[i]) )
                {
                    UInt32 addy = (UInt32)(offset + i);
                    string code = string.Format( "30{0:X6} 00{1:X2}",
                        addy, newBytes[i] );
                    codes.Add( code );
                    patched[i] = true;
                }
            }

            codes.Sort( new Comparison<string>(
                delegate( string s, string t )
                {
                    return s.Substring( 2 ).CompareTo( t.Substring( 2 ) );
                } ) );

            return codes;
        }

        /// <summary>
        /// Generates codes based on context.
        /// </summary>
        public static List<string> GenerateCodes( Context context, byte[] oldBytes, byte[] newBytes, UInt32 offset )
        {
            switch( context )
            {
                case Context.US_PSP:
                    return GeneratePSPCodes( oldBytes, newBytes, offset );
                case Context.US_PSX:
                    return GeneratePSXCodes( oldBytes, newBytes, offset );
            }

            return new List<string>();
        }
    }
}
