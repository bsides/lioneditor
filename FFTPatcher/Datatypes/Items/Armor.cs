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

namespace FFTPatcher.Datatypes
{
    public class Armor : Item
    {
        public byte HPBonus { get; set; }
        public byte MPBonus { get; set; }
        public Armor ArmorDefault { get; private set; }

        public Armor( UInt16 offset, SubArray<byte> itemBytes, SubArray<byte> armorBytes, Armor defaults )
            : base( offset, itemBytes, defaults )
        {
            ArmorDefault = defaults;
            HPBonus = armorBytes[0];
            MPBonus = armorBytes[1];
        }

        public Armor( UInt16 offset, SubArray<byte> itemBytes, SubArray<byte> armorBytes )
            : this( offset, itemBytes, armorBytes, null )
        {
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
