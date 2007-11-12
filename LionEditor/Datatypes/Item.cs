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
    public class Item:IComparable
    {
        public ItemType Type;
        public ItemSubType SubType;
        public UInt16 Offset;

        public string Name;

        public Item( UInt16 offset )
        {
            Item i = ItemList.Find(
                delegate( Item j )
                {
                    return j.Offset == offset;
                } );

            this.Type = i.Type;
            this.SubType = i.SubType;
            this.Name = i.Name;
            this.Offset = offset;
        }

        private Item()
        {
        }

        public string String
        {
            get { return this.ToString(); }
        }

        public override string ToString()
        {
            return string.Format( "{0} ({1:X03})", this.Name, this.Offset );
        }

        /// <summary>
        /// Converts this Item into a series of bytes appropriate for putting into a character's struct
        /// </summary>
        /// <remarks>Returned byte[] is little-endian (least significant byte is in 0th index)</remarks>
        public byte[] ToByte()
        {
            return new byte[] { (byte)(Offset & 0xFF), (byte)((Offset & 0xFF00) >> 8) };
        }

        #region Static members

        private static List<Item> itemList;

        public static List<Item> ItemList
        {
            get
            {
                if( itemList == null )
                {
                    XmlDocument d = new XmlDocument();
                    d.LoadXml( Resources.Items );

                    XmlNodeList items = d.SelectNodes( "//Item" );

                    itemList = new List<Item>( items.Count );

                    foreach( XmlNode i in items )
                    {
                        Item newItem = new Item();
                        newItem.Name = i.InnerText;
                        newItem.Offset = Convert.ToUInt16( i.Attributes["offset"].InnerText );
                        newItem.Type = (ItemType)Enum.Parse( typeof( ItemType ), i.Attributes["type"].InnerText );
                        newItem.SubType = (ItemSubType)Enum.Parse( typeof( ItemSubType ), i.Attributes["subtype"].InnerText );

                        itemList.Add( newItem );
                    }

                    itemList.Sort();
                }

                return new List<Item>(itemList);
            }
        }

        public static List<Item> GetAll( ItemType itemType )
        {
            List<Item> fullList = new List<Item>( ItemList );

            foreach (Item i in fullList)
            {
                if( i.Type != itemType )
                {
                    fullList.Remove( i );
                }
            }
            return fullList;
        }

        public static List<Item> GetAll( ItemSubType itemSubType )
        {
            List<Item> fullList = new List<Item>( ItemList );

            foreach( Item i in fullList )
            {
                if( i.SubType != itemSubType )
                {
                    fullList.Remove( i );
                }
            }

            return fullList;
        }


        #endregion

        #region IComparable Members

        public int CompareTo( object obj )
        {
            Item o = obj as Item;
            if( obj != null )
            {
                return (this.ToString().CompareTo( o.ToString() ));
            }

            return -1;
        }

        #endregion
    }

    public enum ItemType
    {
        [Description("Hand")]
        Hand,

        [Description("Item")]
        Item,

        [Description("Head")]
        Head,

        [Description("Body")]
        Body,

        [Description("Accessory")]
        Accessory,

        [Description("None")]
        None
    }

    public enum ItemSubType
    {
        [Description("Knife")]
        Knife,
        
        [Description("Ninja Blade")]
        NinjaBlade,
        
        [Description("Sword")]
        Sword,
        
        [Description("Knight's Sword")]
        KnightsSword,
        
        [Description("Katana")]
        Katana,
        
        [Description("Axe")]
        Axe,

        [Description("Rod")]
        Rod,
        
        [Description("Staff")]
        Staff,
        
        [Description("Flail")]
        Flail,
        
        [Description("Gun")]
        Gun,
        
        [Description("Crossbow")]
        Crossbow,
        
        [Description("Bow")]
        Bow,
        
        [Description("Instrument")]
        Instrument,
        
        [Description("Book")]
        Book,
        
        [Description("Polearm")]
        Polearm,
        
        [Description("Pole")]
        Pole,
        
        [Description("Bag")]
        Bag,
        
        [Description("Cloth")]
        Cloth,
        
        [Description("Throwing")]
        Throwing,
        
        [Description("Bomb")]
        Bomb,
        
        [Description("Shield")]
        Shield,
        
        [Description("Helmet")]
        Helmet,
        
        [Description("Hat")]
        Hat,
        
        [Description("Hair Adornment")]
        HairAdornment,
        
        [Description("Armor")]
        Armor,
        
        [Description("Clothing")]
        Clothing,
        
        [Description("Robe")]
        Robe,
        
        [Description("Shoes")]
        Shoes,
        
        [Description("Armguard")]
        Armguard,
        
        [Description("Ring")]
        Ring,
        
        [Description("Armlet")]
        Armlet,
        
        [Description("Cloak")]
        Cloak,
        
        [Description("Perfume")]
        Perfume,
        
        [Description("Fell Sword")]
        FellSword,
        
        [Description("Lip Rouge")]
        LipRouge,

        [Description("None")]
        None
    }

}
