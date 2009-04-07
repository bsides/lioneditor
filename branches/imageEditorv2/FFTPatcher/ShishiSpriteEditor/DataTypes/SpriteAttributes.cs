using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    public class SpriteAttributes
    {
        public enum SpriteType
        {
            TYPE1 = 0,
            TYPE2 = 1,
            CYOKO = 2,
            MON = 3,
            ARUTE = 6,
            RUKA = 5,
            KANZEN = 7,
        }

        public SpriteType SHP { get; set; }
        public SpriteType SEQ { get; set; }
        public bool Flying { get; set; }
        public bool Flag1 { get; set; }
        public bool Flag2 { get; set; }
        public bool Flag3 { get; set; }
        public bool Flag4 { get; set; }
        public bool Flag5 { get; set; }
        public bool Flag6 { get; set; }
        public bool Flag7 { get; set; }
        public bool Flag8 { get; set; }

        public SpriteAttributes(IList<byte> bytes)
        {
            System.Diagnostics.Debug.Assert(bytes.Count == 4);
            SHP = (SpriteType)bytes[0];
            SEQ = (SpriteType)bytes[1];
            Flying = bytes[2] != 0;
            bool[] bools = PatcherLib.Utilities.Utilities.BooleansFromByte(bytes[3]);
            Flag1 = bools[0];
            Flag2 = bools[1];
            Flag3 = bools[2];
            Flag4 = bools[3];
            Flag5 = bools[4];
            Flag6 = bools[5];
            Flag7 = bools[6];
            Flag8 = bools[7];
        }

        public IList<byte> ToByteArray()
        {
            byte[] result = new byte[4];
            result[0] = (byte)SHP;
            result[1] = (byte)SEQ;
            result[2] = Flying ? (byte)1 : (byte)0;
            result[3] = PatcherLib.Utilities.Utilities.ByteFromBooleans(Flag8, Flag7, Flag6, Flag5, Flag4, Flag3, Flag2, Flag1);
            return result;
        }
    }
}
