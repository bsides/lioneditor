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
using System.Reflection;
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
    public static class Utilities
    {
        public static byte ByteFromBooleans( bool msb, bool six, bool five, bool four, bool three, bool two, bool one, bool lsb )
        {
            bool[] flags = new bool[] { lsb, one, two, three, four, five, six, msb };
            byte result = 0;

            for( int i = 0; i < 8; i++ )
            {
                if( flags[i] )
                {
                    result |= (byte)(1 << i);
                }
            }

            return result;
        }

        public static byte UpperNibble( byte b )
        {
            return (byte)((b & 0xF0) >> 4);
        }

        public static byte LowerNibble( byte b )
        {
            return (byte)(b & 0x0F);
        }

        public static byte MoveToUpperAndLowerNibbles( int upper, int lower )
        {
            return (byte)(((upper & 0x0F) << 4) | (lower & 0x0F));
        }

        /// <summary>
        /// Creates an array of booleans from a byte. Index 0 in the array is the least significant bit.
        /// </summary>
        public static bool[] BooleansFromByte( byte b )
        {
            bool[] result = new bool[8];
            for( int i = 0; i < 8; i++ )
            {
                result[i] = ((b >> i) & 0x01) > 0;
            }

            return result;
        }

        /// <summary>
        /// Creates an array of booleans from a byte. Index 0 in the array is the most significant bit.
        /// </summary>
        public static bool[] BooleansFromByteMSB( byte b )
        {
            bool[] result = new bool[8];
            for( int i = 0; i < 8; i++ )
            {
                result[i] = ((b >> (7 - i)) & 0x01) > 0;
            }

            return result;
        }

        public static void CopyBoolArrayToBooleans( bool[] bools,
            ref bool msb,
            ref bool six,
            ref bool five,
            ref bool four,
            ref bool three,
            ref bool two,
            ref bool one,
            ref bool lsb )
        {
            lsb = bools[0];
            one = bools[1];
            two = bools[2];
            three = bools[3];
            four = bools[4];
            five = bools[5];
            six = bools[6];
            msb = bools[7];
        }

        public static void CopyByteToBooleans( byte b,
            ref bool msb,
            ref bool six,
            ref bool five,
            ref bool four,
            ref bool three,
            ref bool two,
            ref bool one,
            ref bool lsb )
        {
            CopyBoolArrayToBooleans( BooleansFromByte( b ), ref msb, ref six, ref five, ref four, ref three, ref two, ref one, ref lsb );
        }

        public static UInt16 BytesToUShort( byte lsb, byte msb )
        {
            UInt16 result = 0;
            result += lsb;
            result += (UInt16)(msb << 8);
            return result;
        }

        public static UInt32 BytesToUInt32( byte[] bytes )
        {
            UInt32 result = 0;
            result += bytes[0];
            result += (UInt32)(bytes[1] << 8);
            result += (UInt32)(bytes[2] << 16);
            result += (UInt32)(bytes[3] << 24);

            return result;
        }

        public static bool GetFlag( object o, string name )
        {
            return GetFieldOrProperty<bool>( o, name );
        }

        public static void SetFlag( object o, string name, bool newValue )
        {
            SetFieldOrProperty( o, name, newValue );
        }

        public static T GetFieldOrProperty<T>( object target, string name )
        {
            PropertyInfo pi = target.GetType().GetProperty( name );
            FieldInfo fi = target.GetType().GetField( name );

            if( pi != null )
            {
                return (T)pi.GetValue( target, null );
            }
            else if( fi != null )
            {
                return (T)fi.GetValue( target );
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static T[] GetFieldsOrProperties<T>( object target, string[] names )
        {
            T[] result = new T[names.Length];
            for( int i = 0; i < names.Length; i++ )
            {
                result[i] = GetFieldOrProperty<T>( target, names[i] );
            }

            return result;
        }

        public static void SetFieldOrProperty( object target, string name, object newValue )
        {
            PropertyInfo pi = target.GetType().GetProperty( name );
            FieldInfo fi = target.GetType().GetField( name );
            if( pi != null )
            {
                pi.SetValue( target, newValue, null );
            }
            else if( fi != null )
            {
                fi.SetValue( target, newValue );
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static byte[] UShortToBytes( UInt16 value )
        {
            byte[] result = new byte[2];
            result[0] = (byte)(value & 0xFF);
            result[1] = (byte)((value >> 8) & 0xFF);
            return result;
        }

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

        public static bool CompareArrays<T>( T[] one, T[] two ) where T : IComparable, IEquatable<T>
        {
            if( one.Length != two.Length )
                return false;
            for( long i = 0; i < one.Length; i++ )
            {
                if( !one[i].Equals( two[i] ) )
                    return false;
            }

            return true;
        }

        public static void AppendLines( this StringBuilder sb, IEnumerable<string> lines )
        {
            foreach( string line in lines )
            {
                sb.AppendLine( line );
            }
        }

        public static void AddGroups( this StringBuilder sb, int groupSize, string groupName, List<string> lines )
        {
            if( lines.Count <= groupSize )
            {
                if( groupName != string.Empty ) sb.AppendLine( groupName );
                sb.AppendLines( lines );
            }
            else
            {
                int i = 0;
                int j = 1;
                for( i = 0; (i + 1) * groupSize < lines.Count; i++ )
                {
                    if( groupName != string.Empty ) sb.AppendLine( string.Format( "{0} (part {1})", groupName, j++ ) );
                    sb.AppendLines( new SubArray<string>( lines, i * groupSize, (i + 1) * groupSize - 1 ) );
                }

                if( groupName != string.Empty ) sb.AppendLine( string.Format( "{0} (part {1})", groupName, j++ ) );
                sb.AppendLines( new SubArray<string>( lines, i * groupSize, lines.Count - 1 ) );
            }
        }
    }
}
