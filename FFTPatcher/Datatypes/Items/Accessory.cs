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
    public class Accessory : Item
    {
        public byte PhysicalEvade { get; set; }
        public byte MagicEvade { get; set; }
        public Accessory AccessoryDefault { get; private set; }

        public Accessory( UInt16 offset, SubArray<byte> itemBytes, SubArray<byte> accessoryBytes, Accessory defaults )
            : base( offset, itemBytes, defaults )
        {
            AccessoryDefault = defaults;
            PhysicalEvade = accessoryBytes[0];
            MagicEvade = accessoryBytes[1];
        }

        public Accessory( UInt16 offset, SubArray<byte> itemBytes, SubArray<byte> accessoryBytes )
            : this( offset, itemBytes, accessoryBytes, null )
        {
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
