using System;
using System.Reflection;
using FFTPatcher.Datatypes;
using System.Text;
using System.Collections.Generic;

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

        public static string GenerateCodes( Context context, byte[] oldBytes, byte[] newBytes, UInt32 offset )
        {
            StringBuilder codeBuilder = new StringBuilder();
            List<string> codes = new List<string>();
            bool[] patched = new bool[newBytes.Length];

            for( int i = 0; i < newBytes.Length; i++ )
            {
                if( ((i + 3) < newBytes.Length) &&
                    (newBytes[i] != oldBytes[i]) && (!patched[i]) &&
                    (newBytes[i + 1] != oldBytes[i + 1]) && (!patched[i + 1]) &&
                    (newBytes[i + 2] != oldBytes[i + 2]) && (!patched[i + 2]) &&
                    (newBytes[i + 3] != oldBytes[i + 3]) && (!patched[i + 3]) )
                {
                    UInt32 addy = (UInt32)(offset + i);
                    string code = string.Format( "_L 0x2{0:X7} 0x{1:X2}{2:X2}{3:X2}{4:X2}",
                        addy, newBytes[i], newBytes[i + 1], newBytes[i + 2], newBytes[i + 3] );
                    codes.Add( code );
                    patched[i] = true;
                    patched[i + 1] = true;
                    patched[i + 2] = true;
                    patched[i + 3] = true;
                    i += 3;
                }
            }

            for( int i = 0; i < newBytes.Length; i++ )
            {
                if( ((i + 1) < newBytes.Length) &&
                    (newBytes[i] != oldBytes[i]) && (!patched[i]) &&
                    (newBytes[i + 1] != oldBytes[i + 1]) && (!patched[i + 1]) )
                {
                    UInt32 addy = (UInt32)(offset + i);
                    string code = string.Format( "_L 0x1{0:X7} 0x0000{1:X2}{2:X2}",
                        addy, newBytes[i], newBytes[i + 1] );
                    codes.Add( code );
                    patched[i] = true;
                    patched[i + 1] = true;
                    i++;
                }
            }

            for( int i = 0; i < newBytes.Length; i++ )
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
            foreach( string s in codes )
            {
                codeBuilder.AppendLine( s );
            }

            return codeBuilder.ToString();
        }
    }
}
