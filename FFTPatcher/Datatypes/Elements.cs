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

using System.Collections.Generic;
using System;
namespace FFTPatcher.Datatypes
{
    public class Elements : IEquatable<Elements>, ISupportDigest
    {

        #region Static Fields (2)

        private static readonly string[] elementNames = new string[8] {
            "Fire", "Lightning", "Ice", "Wind", 
            "Earth", "Water", "Holy", "Dark" };

        #endregion Static Fields

        #region Properties (11)


        public bool Dark { get; set; }

        public Elements Default { get; set; }

        public IList<string> DigestableProperties
        {
            get { return elementNames; }
        }

        public bool Earth { get; set; }

        public bool Fire { get; set; }

        public bool HasChanged
        {
            get { return !Equals( Default ); }
        }

        public bool Holy { get; set; }

        public bool Ice { get; set; }

        public bool Lightning { get; set; }

        public bool Water { get; set; }

        public bool Wind { get; set; }


        #endregion Properties

        #region Constructors (1)

        public Elements( byte b )
        {
            bool[] flags = Utilities.BooleansFromByte( b );
            Fire = flags[7];
            Lightning = flags[6];
            Ice = flags[5];
            Wind = flags[4];
            Earth = flags[3];
            Water = flags[2];
            Holy = flags[1];
            Dark = flags[0];
        }

        #endregion Constructors

        #region Methods (6)


        public bool Equals( Elements other )
        {
            return
                other != null &&
                other.Fire == Fire &&
                other.Lightning == Lightning &&
                other.Ice == Ice &&
                other.Wind == Wind &&
                other.Earth == Earth &&
                other.Water == Water &&
                other.Holy == Holy &&
                other.Dark == Dark;
        }

        public bool[] ToBoolArray()
        {
            return new bool[8] {
                Fire, Lightning, Ice, Wind, Earth, Water, Holy, Dark };
        }

        public byte ToByte()
        {
            return Utilities.ByteFromBooleans( Fire, Lightning, Ice, Wind, Earth, Water, Holy, Dark );
        }



        public override bool Equals( object obj )
        {
            if( obj is Elements )
            {
                return Equals( obj as Elements );
            }
            else
            {
                return base.Equals( obj );
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            List<string> strings = new List<string>( 8 );
            foreach( string name in elementNames )
            {
                if( ReflectionHelpers.GetFieldOrProperty<bool>( this, name ) )
                {
                    strings.Add( name );
                }
            }
            return string.Join( " | ", strings.ToArray() );
        }


        #endregion Methods

    }
}
