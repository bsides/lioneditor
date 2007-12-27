using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public class Shield : Item
    {
        public byte PhysicalBlockRate { get; set; }
        public byte MagicBlockRate { get; set; }

        public Shield( UInt16 offset, SubArray<byte> itemBytes, SubArray<byte> shieldBytes )
            : base( offset, itemBytes )
        {
            PhysicalBlockRate = shieldBytes[0];
            MagicBlockRate = shieldBytes[1];
        }

        public byte[] ToItemByteArray()
        {
            return base.ToByteArray().ToArray();
        }

        public byte[] ToShieldByteArray()
        {
            return new byte[2] { PhysicalBlockRate, MagicBlockRate };
        }

        public override byte[] ToFirstByteArray()
        {
            return ToItemByteArray();
        }

        public override byte[] ToSecondByteArray()
        {
            return ToShieldByteArray();
        }
    }
}
