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
using System.Collections.Generic;

namespace FFTPatcher.Datatypes
{
    /// <summary>
    /// Represents a weapon.
    /// </summary>
    public class Weapon : Item
    {

		#region Fields (8) 

        public bool Arc;
        public bool Blank;
        public bool Direct;
        public bool Force2Hands;
        public bool Lunging;
        public bool Striking;
        public bool TwoHands;
        public bool TwoSwords;

		#endregion Fields 

		#region Properties (9) 


        public Elements Elements { get; private set; }

        public byte EvadePercentage { get; set; }

        public byte Formula { get; set; }

        public byte InflictStatus { get; set; }

        public byte Range { get; set; }

        public byte Unknown { get; set; }

        public Weapon WeaponDefault { get; private set; }

        public byte WeaponPower { get; set; }



        /// <summary>
        /// Gets a value indicating whether this instance has changed.
        /// </summary>
        /// <value></value>
        public override bool HasChanged
        {
            get
            {
                return base.HasChanged ||
                    (WeaponDefault != null &&
                    (Elements.ToByte() != WeaponDefault.Elements.ToByte() ||
                    EvadePercentage != WeaponDefault.EvadePercentage ||
                    Formula != WeaponDefault.Formula ||
                    InflictStatus != WeaponDefault.InflictStatus ||
                    Range != WeaponDefault.Range ||
                    Unknown != WeaponDefault.Unknown ||
                    WeaponPower != WeaponDefault.WeaponPower));
            }
        }


		#endregion Properties 

		#region Constructors (2) 

        public Weapon( UInt16 offset, IList<byte> itemBytes, IList<byte> weaponBytes )
            : this( offset, itemBytes, weaponBytes, null )
        {
        }

        public Weapon( UInt16 offset, IList<byte> itemBytes, IList<byte> weaponBytes, Weapon defaults )
            : base( offset, itemBytes, defaults )
        {
            WeaponDefault = defaults;
            Range = weaponBytes[0];
            Utilities.CopyByteToBooleans( weaponBytes[1], ref Striking, ref Lunging, ref Direct, ref Arc, ref TwoSwords, ref TwoHands, ref Blank, ref Force2Hands );
            Formula = weaponBytes[2];
            Unknown = weaponBytes[3];
            WeaponPower = weaponBytes[4];
            EvadePercentage = weaponBytes[5];
            Elements = new Elements( weaponBytes[6] );
            InflictStatus = weaponBytes[7];
        }

		#endregion Constructors 

		#region Methods (5) 


        public byte[] ToItemByteArray()
        {
            return base.ToByteArray().ToArray();
        }

        public bool[] ToWeaponBoolArray()
        {
            return new bool[8] {
                Striking, Lunging, Direct, Arc, TwoSwords, TwoHands, Blank, Force2Hands };
        }

        public byte[] ToWeaponByteArray()
        {
            byte[] result = new byte[8];
            result[0] = Range;
            result[1] = Utilities.ByteFromBooleans( Striking, Lunging, Direct, Arc, TwoSwords, TwoHands, Blank, Force2Hands );
            result[2] = Formula;
            result[3] = Unknown;
            result[4] = WeaponPower;
            result[5] = EvadePercentage;
            result[6] = Elements.ToByte();
            result[7] = InflictStatus;
            return result;
        }



        public override byte[] ToFirstByteArray()
        {
            return ToItemByteArray();
        }

        public override byte[] ToSecondByteArray()
        {
            return ToWeaponByteArray();
        }


		#endregion Methods 

    }
}
