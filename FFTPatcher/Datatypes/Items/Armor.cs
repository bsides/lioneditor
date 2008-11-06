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
    /// Represents a piece of armor.
    /// </summary>
    public class Armor : Item
    {

        #region Static Fields (1)

        private static readonly List<string> armorDigestableProperties;

        #endregion Static Fields

        #region Properties (5)


        public Armor ArmorDefault { get; private set; }

        public byte HPBonus { get; set; }

        public byte MPBonus { get; set; }



        public override IList<string> DigestableProperties
        {
            get { return armorDigestableProperties; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has changed.
        /// </summary>
        /// <value></value>
        public override bool HasChanged
        {
            get
            {
                return base.HasChanged ||
                    (ArmorDefault != null &&
                    (MPBonus != ArmorDefault.MPBonus ||
                    HPBonus != ArmorDefault.HPBonus));
            }
        }


        #endregion Properties

        #region Constructors (3)

        static Armor()
        {
            armorDigestableProperties = new List<string>( Item.digestableProperties );
            armorDigestableProperties.Add( "HPBonus" );
            armorDigestableProperties.Add( "MPBonus" );
        }

        public Armor( UInt16 offset, IList<byte> itemBytes, IList<byte> armorBytes )
            : this( offset, itemBytes, armorBytes, null )
        {
        }

        public Armor( UInt16 offset, IList<byte> itemBytes, IList<byte> armorBytes, Armor defaults )
            : base( offset, itemBytes, defaults )
        {
            ArmorDefault = defaults;
            HPBonus = armorBytes[0];
            MPBonus = armorBytes[1];
        }

        #endregion Constructors

        #region Methods (4)


        public byte[] ToArmorByteArray()
        {
            return new byte[2] { HPBonus, MPBonus };
        }

        public byte[] ToItemByteArray()
        {
            return base.ToByteArray().ToArray();
        }



        public override byte[] ToFirstByteArray()
        {
            return ToItemByteArray();
        }

        public override byte[] ToSecondByteArray()
        {
            return ToArmorByteArray();
        }
        public static void CopyArmor( Armor source, Armor destination )
        {
            destination.HPBonus = source.HPBonus;
            destination.MPBonus = source.MPBonus;
        }

        public void CopyArmorTo( Armor destination )
        {
            CopyArmor( this, destination );
        }

        public static void CopyAll( Armor source, Armor destination )
        {
            CopyArmor( source, destination );
            CopyCommon( source, destination );
        }

        public void CopyAllTo( Armor destination )
        {
            CopyAll( this, destination );
        }


        #endregion Methods

    }
}
