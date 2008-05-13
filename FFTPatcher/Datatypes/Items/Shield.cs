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
    /// Represents a shield.
    /// </summary>
    public class Shield : Item
    {

        #region Static Fields (1)

        private static readonly List<string> shieldDigestableProperties;

        #endregion Static Fields

        #region Properties (5)


        public byte MagicBlockRate { get; set; }

        public byte PhysicalBlockRate { get; set; }

        public Shield ShieldDefault { get; private set; }



        public override IList<string> DigestableProperties
        {
            get { return shieldDigestableProperties; }
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
                    (ShieldDefault != null &&
                    (MagicBlockRate != ShieldDefault.MagicBlockRate ||
                    PhysicalBlockRate != ShieldDefault.PhysicalBlockRate));
            }
        }


        #endregion Properties

        #region Constructors (3)

        static Shield()
        {
            shieldDigestableProperties = new List<string>( Item.digestableProperties );
            shieldDigestableProperties.Add( "PhysicalBlockRate" );
            shieldDigestableProperties.Add( "MagicBlockRate" );
        }

        public Shield( UInt16 offset, IList<byte> itemBytes, IList<byte> shieldBytes )
            : this( offset, itemBytes, shieldBytes, null )
        {
        }

        public Shield( UInt16 offset, IList<byte> itemBytes, IList<byte> shieldBytes, Shield defaults )
            : base( offset, itemBytes, defaults )
        {
            ShieldDefault = defaults;
            PhysicalBlockRate = shieldBytes[0];
            MagicBlockRate = shieldBytes[1];
        }

        #endregion Constructors

        #region Methods (4)


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


        #endregion Methods

    }
}
