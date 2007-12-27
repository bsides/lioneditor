using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public class Armor : Item
    {
        public byte HPBonus { get; set; }
        public byte MPBonus { get; set; }

        public Armor( UInt16 offset, SubArray<byte> itemBytes, SubArray<byte> armorBytes )
            : base( offset, itemBytes )
        {
            HPBonus = armorBytes[0];
            MPBonus = armorBytes[1];
        }

        public byte[] ToItemByteArray()
        {
            return base.ToByteArray().ToArray();
        }

        public byte[] ToArmorByteArray()
        {
            return new byte[2] { HPBonus, MPBonus };
        }

        public override byte[] ToFirstByteArray()
        {
            return ToItemByteArray();
        }

        public override byte[] ToSecondByteArray()
        {
            return ToArmorByteArray();
        }
    }
}
