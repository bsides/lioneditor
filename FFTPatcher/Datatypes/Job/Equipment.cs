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

namespace FFTPatcher.Datatypes
{
    public class Equipment
    {
        public bool Unused;
        public bool Knife;
        public bool NinjaBlade;
        public bool Sword;
        public bool KnightsSword;
        public bool Katana;
        public bool Axe;
        public bool Rod;
        public bool Staff;
        public bool Flail;
        public bool Gun;
        public bool Crossbow;
        public bool Bow;
        public bool Instrument;
        public bool Book;
        public bool Polearm;
        public bool Pole;
        public bool Bag;
        public bool Cloth;
        public bool Shield;
        public bool Helmet;
        public bool Hat;
        public bool HairAdornment;
        public bool Armor;
        public bool Clothing;
        public bool Robe;
        public bool Shoes;
        public bool Armguard;
        public bool Ring;
        public bool Armlet;
        public bool Cloak;
        public bool Perfume;
        public bool Unknown1;
        public bool Unknown2;
        public bool Unknown3;
        public bool FellSword;
        public bool LipRouge;
        public bool Unknown6;
        public bool Unknown7;
        public bool Unknown8;

        public Equipment( SubArray<byte> bytes )
        {
            Utilities.CopyByteToBooleans( bytes[0], ref Unused, ref Knife, ref NinjaBlade, ref Sword, ref KnightsSword, ref Katana, ref Axe, ref Rod );
            Utilities.CopyByteToBooleans( bytes[1], ref Staff, ref Flail, ref Gun, ref Crossbow, ref Bow, ref Instrument, ref Book, ref Polearm );
            Utilities.CopyByteToBooleans( bytes[2], ref Pole, ref Bag, ref Cloth, ref Shield, ref Helmet, ref Hat, ref HairAdornment, ref Armor );
            Utilities.CopyByteToBooleans( bytes[3], ref Clothing, ref Robe, ref Shoes, ref Armguard, ref Ring, ref Armlet, ref Cloak, ref Perfume );
            if( bytes.Count == 5 )
            {
                Utilities.CopyByteToBooleans( bytes[4], ref Unknown1, ref Unknown2, ref Unknown3, ref FellSword, ref LipRouge, ref Unknown6, ref Unknown7, ref Unknown8 );
            }
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[5];
            result[0] = Utilities.ByteFromBooleans( Unused, Knife, NinjaBlade, Sword, KnightsSword, Katana, Axe, Rod );
            result[1] = Utilities.ByteFromBooleans( Staff, Flail, Gun, Crossbow, Bow, Instrument, Book, Polearm );
            result[2] = Utilities.ByteFromBooleans( Pole, Bag, Cloth, Shield, Helmet, Hat, HairAdornment, Armor );
            result[3] = Utilities.ByteFromBooleans( Clothing, Robe, Shoes, Armguard, Ring, Armlet, Cloak, Perfume );
            result[4] = Utilities.ByteFromBooleans( Unknown1, Unknown2, Unknown3, FellSword, LipRouge, Unknown6, Unknown7, Unknown8 );
            return result;
        }

        private byte[] ToByteArrayPSX()
        {
            byte[] result = new byte[4];
            result[0] = Utilities.ByteFromBooleans( Unused, Knife, NinjaBlade, Sword, KnightsSword, Katana, Axe, Rod );
            result[1] = Utilities.ByteFromBooleans( Staff, Flail, Gun, Crossbow, Bow, Instrument, Book, Polearm );
            result[2] = Utilities.ByteFromBooleans( Pole, Bag, Cloth, Shield, Helmet, Hat, HairAdornment, Armor );
            result[3] = Utilities.ByteFromBooleans( Clothing, Robe, Shoes, Armguard, Ring, Armlet, Cloak, Perfume );
            return result;
        }

        public byte[] ToByteArray( Context context )
        {
            switch( context )
            {
                case Context.US_PSX:
                    return ToByteArrayPSX();
                default:
                    return ToByteArray();
            }
        }
    }
}
