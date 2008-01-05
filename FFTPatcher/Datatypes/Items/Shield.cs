/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;

namespace FFTPatcher.Datatypes
{
    public class Shield : Item
    {
        public byte PhysicalBlockRate { get; set; }
        public byte MagicBlockRate { get; set; }
        public Shield ShieldDefault { get; private set; }

        public Shield( UInt16 offset, SubArray<byte> itemBytes, SubArray<byte> shieldBytes )
            : this( offset, itemBytes, shieldBytes, null )
        {
        }

        public Shield( UInt16 offset, SubArray<byte> itemBytes, SubArray<byte> shieldBytes, Shield defaults )
            : base( offset, itemBytes, defaults )
        {
            ShieldDefault = defaults;
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
