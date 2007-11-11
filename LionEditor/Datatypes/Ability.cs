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
    public class Ability
    {
        public UInt16 Value;
        public string Name;

        public Ability( UInt16 value )
        {
            Ability i = AbilityList.Find(
                delegate( Ability j )
                {
                    return j.Value == value;
                } );

            this.Value = i.Value;
            this.Name = i.Name;
        }

        private Ability()
        {
        }

        public override string ToString()
        {
            return this.Name;
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
                        newItem.Name = i.InnerText;
                        newItem.Value = Convert.ToUInt16( i.Attributes["value"].InnerText );

                        abilityList.Add( newItem );
                    }
                }

                return abilityList;
            }
        }

        #endregion
    }
}
