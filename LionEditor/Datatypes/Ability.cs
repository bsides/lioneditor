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
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Resources;
using LionEditor.Properties;

namespace LionEditor
{
    public class Ability:IComparable, IEquatable<Ability>
    {
        private UInt16 m_Value;
        public UInt16 Value
        {
        	get { return m_Value; }
        	set { m_Value = value; }
        }

        private string m_Name;
        public string Name
        {
        	get { return m_Name; }
        	set { m_Name = value; }
        }

        private uint m_JumpBonus = 0;
        public uint JumpBonus
        {
        	get { return m_JumpBonus; }
        	set { m_JumpBonus = value; }
        }

        private uint m_MoveBonus = 0;
        public uint MoveBonus
        {
        	get { return m_MoveBonus; }
        	set { m_MoveBonus = value; }
        }

        private uint hpMultiplier = 0;
        public uint HPMultiplier
        {
            get { return hpMultiplier; }
            set { hpMultiplier = value; }
        }


        public Ability( UInt16 value )
        {
            Ability i = AbilityList.Find(
                delegate( Ability j )
                {
                    return j.Value == value;
                } );

            this.Value = i.Value;
            this.Name = i.Name;
            this.MoveBonus = i.MoveBonus;
            this.JumpBonus = i.JumpBonus;
        }

        private Ability()
        {
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

        public string String
        {
            get { return this.ToString(); }
        }



        #region Static members

        private static List<Ability> abilityList;

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
                        Ability newItem = new Ability();
                        newItem.Name = i.SelectSingleNode("name").InnerText;
                        newItem.Value = Convert.ToUInt16( i.Attributes["value"].InnerText );
                        XmlNode moveJumpNode = i.SelectSingleNode( "move" );
                        if( moveJumpNode != null ) { newItem.MoveBonus = Convert.ToUInt32( moveJumpNode.InnerText ); }
                        moveJumpNode = i.SelectSingleNode( "jump" );
                        if( moveJumpNode != null ) { newItem.JumpBonus = Convert.ToUInt32( moveJumpNode.InnerText ); }
                        moveJumpNode = i.SelectSingleNode( "hpMultiplier" );
                        if( moveJumpNode != null ) { newItem.HPMultiplier = Convert.ToUInt32( moveJumpNode.InnerText ); }

                        abilityList.Add( newItem );
                    }

                    abilityList.Sort();
                }


                return new List<Ability>(abilityList);
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
