using System;
using System.Collections.Generic;
using System.Text;
using PatcherLib.Iso;
using System.IO;

namespace FFTPatcher.SpriteEditor
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

    internal class SpriteAttributes
    {
        public SpriteType SHP { get; private set;}
        public SpriteType SEQ { get; private set; }
        public bool Flying { get; private set; }
        public bool Flag1 { get { return flags[0]; } set { flags[0] = value; } }
        public bool Flag2 { get { return flags[1]; } set { flags[1] = value; } }
        public bool Flag3 { get { return flags[2]; } set { flags[2] = value; } }
        public bool Flag4 { get { return flags[3]; } set { flags[3] = value; } }
        public bool Flag5 { get { return flags[4]; } set { flags[4] = value; } }
        public bool Flag6 { get { return flags[5]; } set { flags[5] = value; } }
        public bool Flag7 { get { return flags[6]; } set { flags[6] = value; } }
        public bool Flag8 { get { return flags[7]; } set { flags[7] = value; } }

        private bool[] flags = new bool[8];
        internal void SetSHP(Stream iso, SpriteType shp)
        {
            SHP = shp;
            UpdateIso(iso);
        }

        internal void SetSEQ(Stream iso, SpriteType seq)
        {
            SEQ = seq;
            UpdateIso(iso);
        }

        internal void SetFlying(Stream iso, bool flying)
        {
            Flying = flying;
            UpdateIso(iso);
        }

        internal void SetFlag(Stream iso, int index, bool flag)
        {
            flags[index] = flag;
            UpdateIso(iso);
        }

        private void UpdateIso(Stream iso)
        {
            PsxIso.PatchPsxIso(iso, psxPos.GetPatchedByteArray(ToByteArray()));
        }

        private SpriteAttributes(PsxIso.KnownPosition pos, IList<byte> bytes)
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
            psxPos = pos;
        }

        private PsxIso.KnownPosition psxPos;

        public static SpriteAttributes BuildPsx(PsxIso.KnownPosition pos, IList<byte> bytes)
        {
            return new SpriteAttributes(pos, bytes);
        }

        private byte[] ToByteArray()
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
