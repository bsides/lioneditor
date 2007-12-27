using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public class Accessory : Item
    {
        public byte PhysicalEvade { get; set; }
        public byte MagicEvade { get; set; }

        public Accessory( UInt16 offset, SubArray<byte> itemBytes, SubArray<byte> accessoryBytes )
            : base( offset, itemBytes )
        {
            PhysicalEvade = accessoryBytes[0];
            MagicEvade = accessoryBytes[1];
        }

        public byte[] ToItemByteArray()
        {
            return base.ToByteArray().ToArray();
        }

        public byte[] ToAccessoryByteArray()
        {
            return new byte[2] { PhysicalEvade, MagicEvade };
        }

        public override byte[] ToFirstByteArray()
        {
            return ToItemByteArray();
        }

        public override byte[] ToSecondByteArray()
        {
            return ToAccessoryByteArray();
        }
    }
}
