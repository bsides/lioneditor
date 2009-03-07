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
using System.ComponentModel;
using System.Xml;
using LionEditor.Properties;

namespace LionEditor
{
    public class Item : IComparable, IEquatable<Item>
    {
        #region Fields

        private ItemType type;
        private ItemSubType subType;
        private UInt16 offset;
        private string name;
        private uint power;
        private uint blockRate;
        private uint maBonus;
        private uint paBonus;
        private uint speedBonus;
        private uint jumpBonus;
        private uint moveBonus;
        private uint physicalSEV;
        private uint magicSEV;
        private uint hpBonus;
        private uint mpBonus;
        private uint physicalAEV;
        private uint magicAEV;
        private static List<Item> itemList;

        #endregion

        #region Properties

        /// <summary>
        /// Gets this item's <see cref="ItemType"/>
        /// </summary>
        public ItemType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Gets this item's <see cref="ItemSubType"/>
        /// </summary>
        public ItemSubType SubType
        {
            get { return subType; }
        }

        /// <summary>
        /// Gets this item's offset in the inventory/list
        /// </summary>
        public UInt16 Offset
        {
            get { return offset; }
        }

        /// <summary>
        /// Gets the name of this item
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the power provided by this item, if any
        /// </summary>
        public uint Power
        {
            get { return power; }
        }

        /// <summary>
        /// Gets the block rate of this item, if any
        /// </summary>
        public uint BlockRate
        {
            get { return blockRate; }
        }

        /// <summary>
        /// Gets the magic attack bonus of this item, if any
        /// </summary>
        public uint MABonus
        {
            get { return maBonus; }
        }

        /// <summary>
        /// Gets the physical attack bonus of this item, if any
        /// </summary>
        public uint PABonus
        {
            get { return paBonus; }
        }

        /// <summary>
        /// Gets the speed bonus of this item, if any
        /// </summary>
        public uint SpeedBonus
        {
            get { return speedBonus; }
        }

        /// <summary>
        /// Gets the jump bonus of this item, if any
        /// </summary>
        public uint JumpBonus
        {
            get { return jumpBonus; }
        }

        /// <summary>
        /// Gets the move bonus of this item
        /// </summary>
        public uint MoveBonus
        {
            get { return moveBonus; }
        }

        /// <summary>
        /// Gets the Physical Shield Evade% of this item
        /// </summary>
        public uint PhysicalSEV
        {
            get { return physicalSEV; }
        }

        /// <summary>
        /// Gets the Magic Shield Evade% of this item
        /// </summary>
        public uint MagicSEV
        {
            get { return magicSEV; }
        }

        /// <summary>
        /// Gets the HP bonus provided by this item, if any
        /// </summary>
        public uint HPBonus
        {
            get { return hpBonus; }
        }

        /// <summary>
        /// Gets the MP bonus provided by this item, if any
        /// </summary>
        public uint MPBonus
        {
            get { return mpBonus; }
        }

        /// <summary>
        /// Gets the Physical Accessory Evade% of this item
        /// </summary>
        public uint PhysicalAEV
        {
            get { return physicalAEV; }
        }

        /// <summary>
        /// Gets the Magic Accessory Evade% of this item
        /// </summary>
        public uint MagicAEV
        {
            get { return magicAEV; }
        }

        /// <summary>
        /// Gets a string representing this item
        /// </summary>
        public string String
        {
            get { return this.ToString(); }
        }

        /// <summary>
        /// Gets a list of all items
        /// </summary>
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
                        newItem.name = i.SelectSingleNode( "name" ).InnerText;
                        newItem.offset = Convert.ToUInt16( i.Attributes["offset"].InnerText );
                        newItem.type = (ItemType)Enum.Parse( typeof( ItemType ), i.Attributes["type"].InnerText );
                        newItem.subType = (ItemSubType)Enum.Parse( typeof( ItemSubType ), i.Attributes["subtype"].InnerText );

                        XmlNode node = i.SelectSingleNode( "power" );
                        if( node != null ) { newItem.power = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "blockRate" );
                        if( node != null ) { newItem.blockRate = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "ma" );
                        if( node != null ) { newItem.maBonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "pa" );
                        if( node != null ) { newItem.paBonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "speed" );
                        if( node != null ) { newItem.speedBonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "jump" );
                        if( node != null ) { newItem.jumpBonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "move" );
                        if( node != null ) { newItem.moveBonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "physicalSEV" );
                        if( node != null ) { newItem.physicalSEV = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "magicSEV" );
                        if( node != null ) { newItem.magicSEV = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "hp" );
                        if( node != null ) { newItem.hpBonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "mp" );
                        if( node != null ) { newItem.mpBonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "magicAEV" );
                        if( node != null ) { newItem.magicAEV = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "physicalAEV" );
                        if( node != null ) { newItem.physicalAEV = Convert.ToUInt32( node.InnerText ); }

                        itemList.Add( newItem );
                    }

                    itemList.Sort();
                }

                return itemList;
            }
        }

        #endregion

        #region Constructors

        private Item()
        {
        }

        #endregion

        #region Utilities

        public static Item GetItemAtOffset(int offset)
        {
            Item i = ItemList.Find(
                delegate(Item j)
                {
                    return j.Offset == offset;
                });
            return i;
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

        public static List<Item> GetAll( ItemType itemType )
        {
            List<Item> fullList = new List<Item>( ItemList );

            foreach( Item i in fullList )
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

        #region IComparable Members

        public int CompareTo( object obj )
        {
            Item o = obj as Item;
            if( o != null )
            {
                return (this.ToString().CompareTo( o.ToString() ));
            }

            return -1;
        }

        #endregion

        #region IEquatable<Item> Members

        public bool Equals( Item other )
        {
            return (this.Offset == other.Offset);
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Represents types of items
    /// </summary>
    public enum ItemType
    {
        [Description( "Hand" )]
        Hand,

        [Description( "Item" )]
        Item,

        [Description( "Head" )]
        Head,

        [Description( "Body" )]
        Body,

        [Description( "Accessory" )]
        Accessory,

        [Description( "None" )]
        None
    }

    /// <summary>
    /// Represents subtypes of items
    /// </summary>
    public enum ItemSubType
    {
        [Description( "Knife" )]
        Knife,

        [Description( "Ninja Blade" )]
        NinjaBlade,

        [Description( "Sword" )]
        Sword,

        [Description( "Knight's Sword" )]
        KnightsSword,

        [Description( "Katana" )]
        Katana,

        [Description( "Axe" )]
        Axe,

        [Description( "Rod" )]
        Rod,

        [Description( "Staff" )]
        Staff,

        [Description( "Flail" )]
        Flail,

        [Description( "Gun" )]
        Gun,

        [Description( "Crossbow" )]
        Crossbow,

        [Description( "Bow" )]
        Bow,

        [Description( "Instrument" )]
        Instrument,

        [Description( "Book" )]
        Book,

        [Description( "Polearm" )]
        Polearm,

        [Description( "Pole" )]
        Pole,

        [Description( "Bag" )]
        Bag,

        [Description( "Cloth" )]
        Cloth,

        [Description( "Throwing" )]
        Throwing,

        [Description( "Bomb" )]
        Bomb,

        [Description( "Shield" )]
        Shield,

        [Description( "Helmet" )]
        Helmet,

        [Description( "Hat" )]
        Hat,

        [Description( "Hair Adornment" )]
        HairAdornment,

        [Description( "Armor" )]
        Armor,

        [Description( "Clothing" )]
        Clothing,

        [Description( "Robe" )]
        Robe,

        [Description( "Shoes" )]
        Shoes,

        [Description( "Armguard" )]
        Armguard,

        [Description( "Ring" )]
        Ring,

        [Description( "Armlet" )]
        Armlet,

        [Description( "Cloak" )]
        Cloak,

        [Description( "Perfume" )]
        Perfume,

        [Description( "Fell Sword" )]
        FellSword,

        [Description( "Lip Rouge" )]
        LipRouge,

        [Description( "None" )]
        None
    }
}
