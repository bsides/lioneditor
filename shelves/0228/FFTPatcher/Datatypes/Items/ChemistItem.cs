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
    /// <summary>
    /// Represents a Chemist's item.
    /// </summary>
    public class ChemistItem : Item
    {
        public byte Formula { get; set; }
        public byte X { get; set; }
        public byte InflictStatus { get; set; }
        public ChemistItem ChemistItemDefault { get; private set; }

        public ChemistItem( UInt16 offset, SubArray<byte> itemBytes, SubArray<byte> chemistBytes ) :
            this( offset, itemBytes, chemistBytes, null )
        {
        }

        public ChemistItem( UInt16 offset, SubArray<byte> itemBytes, SubArray<byte> chemistBytes, ChemistItem defaults )
            : base( offset, itemBytes, defaults )
        {
            ChemistItemDefault = defaults;
            Formula = chemistBytes[0];
            X = chemistBytes[1];
            InflictStatus = chemistBytes[2];
        }

        public byte[] ToItemByteArray()
        {
            return base.ToByteArray().ToArray();
        }

        public byte[] ToChemistItemByteArray()
        {
            return new byte[3] { Formula, X, InflictStatus };
        }

        public override byte[] ToFirstByteArray()
        {
            return ToItemByteArray();
        }

        public override byte[] ToSecondByteArray()
        {
            return ToChemistItemByteArray();
        }
    }
}
