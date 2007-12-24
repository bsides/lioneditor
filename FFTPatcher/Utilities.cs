using System;
using System.Reflection;

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
    }
}
