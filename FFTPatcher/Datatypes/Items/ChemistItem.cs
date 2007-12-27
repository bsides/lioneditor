using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public class ChemistItem : Item
    {
        public byte Formula { get; set; }
        public byte X { get; set; }
        public byte SpellStatus { get; set; }

        public ChemistItem( UInt16 offset, SubArray<byte> itemBytes, SubArray<byte> chemistBytes )
            : base( offset, itemBytes )
        {
            Formula = chemistBytes[0];
            X = chemistBytes[1];
            SpellStatus = chemistBytes[2];
        }

        public byte[] ToItemByteArray()
        {
            return base.ToByteArray().ToArray();
        }

        public byte[] ToChemistItemByteArray()
        {
            return new byte[3] { Formula, X, SpellStatus };
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
