/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of LionEditor.

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
using System.Collections.Generic;
using System.Xml;
using LionEditor.Properties;

namespace LionEditor
{
    /// <summary>
    /// Represents a movement, reaction, or support ability
    /// </summary>
    public class Ability : IComparable, IEquatable<Ability>
    {
        #region Fields

        private UInt16 value;
        private string name;
        private uint jumpBonus = 0;
        private uint moveBonus = 0;
        private uint hpMultiplier = 0;

        #endregion

        #region Properties

        public UInt16 Value
        {
            get { return value; }
        }

        /// <summary>
        /// Gets the name of this ability
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the jump bonus provided by this ability, if any
        /// </summary>
        public uint JumpBonus
        {
            get { return jumpBonus; }
        }

        /// <summary>
        /// Gets the move bonus provided by this ability, if any
        /// </summary>
        public uint MoveBonus
        {
            get { return moveBonus; }
        }

        /// <summary>
        /// Gets the HP multiplier provided by this ability, if any
        /// </summary>
        public uint HPMultiplier
        {
            get { return hpMultiplier; }
        }

        public string String
        {
            get { return this.ToString(); }
        }

        #endregion

        #region Constructor

        private Ability()
        {
        }

        #endregion

        public static Ability GetAbilityFromOffset(UInt16 value)
        {
            Ability i = AbilityList.Find(
                delegate(Ability j)
                {
                    return j.Value == value;
                });

            return i;
        }

        public override string ToString()
        {
            return string.Format( "{0} ({1:X03})", this.Name, this.Value );
        }

        /// <summary>
        /// Converts this Ability into a series of bytes appropriate for putting into a character's struct
        /// </summary>
        /// <remarks>Returned byte[] is little-endian (least significant byte is in 0th index)</remarks>
        public byte[] ToByte()
        {
            return new byte[] { (byte)(Value & 0xFF), (byte)((Value & 0xFF00) >> 8) };
        }

        #region Static members

        private static List<Ability> abilityList;

        /// <summary>
        /// Gets a list of ALL abilities from the XML
        /// </summary>
        public static List<Ability> AbilityList
        {
            get
            {
                if( abilityList == null )
                {
                    XmlDocument d = new XmlDocument();
                    d.LoadXml( Resources.Abilities );

                    XmlNodeList abilities = d.SelectNodes( "//Ability" );

                    abilityList = new List<Ability>( abilities.Count );

                    foreach( XmlNode i in abilities )
                    {
                        Ability newAbility = new Ability();
                        newAbility.name = i.SelectSingleNode( "name" ).InnerText;
                        newAbility.value = Convert.ToUInt16( i.Attributes["value"].InnerText );
                        XmlNode moveJumpNode = i.SelectSingleNode( "move" );
                        if( moveJumpNode != null ) { newAbility.moveBonus = Convert.ToUInt32( moveJumpNode.InnerText ); }
                        moveJumpNode = i.SelectSingleNode( "jump" );
                        if( moveJumpNode != null ) { newAbility.jumpBonus = Convert.ToUInt32( moveJumpNode.InnerText ); }
                        moveJumpNode = i.SelectSingleNode( "hpMultiplier" );
                        if( moveJumpNode != null ) { newAbility.hpMultiplier = Convert.ToUInt32( moveJumpNode.InnerText ); }

                        abilityList.Add( newAbility );
                    }

                    abilityList.Sort();
                }


                return abilityList;
            }
        }

        #endregion

        #region IComparable Members

        public int CompareTo( object obj )
        {
            Ability a = obj as Ability;
            if( a != null )
            {
                return this.ToString().CompareTo( a.ToString() );
            }

            return -1;
        }

        #endregion

        #region IEquatable<Ability> Members

        public bool Equals( Ability other )
        {
            return (this.Value == other.Value);
        }

        #endregion
    }
}
