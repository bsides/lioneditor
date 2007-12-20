using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher
{
    public static class Utilities
    {
        public static byte ByteFromBooleans(bool msb, bool six, bool five, bool four, bool three, bool two, bool one, bool lsb)
        {
            bool[] flags = new bool[] { lsb, one, two, three, four, five, six, msb };
            byte result = 0;

            for (int i = 0; i < 8; i++)
            {
                if (flags[i])
                {
                    result |= (byte)(i << i);
                }
            }

            return result;
        }

        /// <summary>
        /// Creates an array of booleans from a byte. Index 0 in the array is the least significant bit.
        /// </summary>
        public static bool[] BooleansFromByte(byte b)
        {
            bool[] result = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = ((b >> i) & 0x01) > 0;
            }

            return result;
        }

        public static void CopyBoolArrayToBooleans(bool[] bools, 
            ref bool msb,
            ref bool six,
            ref bool five,
            ref bool four,
            ref bool three,
            ref bool two,
            ref bool one,
            ref bool lsb)
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

        public static void CopyByteToBooleans(byte b,
            ref bool msb,
            ref bool six,
            ref bool five,
            ref bool four,
            ref bool three,
            ref bool two,
            ref bool one,
            ref bool lsb)
        {
            CopyBoolArrayToBooleans(BooleansFromByte(b), ref msb, ref six, ref five, ref four, ref three, ref two, ref one, ref lsb);
        }

        public static UInt16 BytesToUShort(byte lsb, byte msb)
        {
            UInt16 result = 0;
            result += lsb;
            result += (UInt16)(msb << 8);
            return result;
        }
    }
}
