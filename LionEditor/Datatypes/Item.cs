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
    public class Item:IComparable, IEquatable<Item>
    {
        private ItemType m_Type;
        public ItemType Type
        {
        	get { return m_Type; }
        	set { m_Type = value; }
        }

        private ItemSubType m_SubType;
        public ItemSubType SubType
        {
        	get { return m_SubType; }
        	set { m_SubType = value; }
        }


        private UInt16 m_Offset;
        public UInt16 Offset
        {
        	get { return m_Offset; }
        	set { m_Offset = value; }
        }


        private string m_Name;
        public string Name
        {
        	get { return m_Name; }
        	set { m_Name = value; }
        }

        private uint m_Power;
        public uint Power
        {
        	get { return m_Power; }
        	set { m_Power = value; }
        }

        private uint m_BlockRate;
        public uint BlockRate
        {
        	get { return m_BlockRate; }
        	set { m_BlockRate = value; }
        }

        private uint m_MABonus;
        public uint MABonus
        {
        	get { return m_MABonus; }
        	set { m_MABonus = value; }
        }

        private uint m_PABonus;
        public uint PABonus
        {
        	get { return m_PABonus; }
        	set { m_PABonus = value; }
        }

        private uint m_SpeedBonus;
        public uint SpeedBonus
        {
        	get { return m_SpeedBonus; }
        	set { m_SpeedBonus = value; }
        }

        private uint m_JumpBonus;
        public uint JumpBonus
        {
        	get { return m_JumpBonus; }
        	set { m_JumpBonus = value; }
        }

        private uint m_MoveBonus;
        public uint MoveBonus
        {
        	get { return m_MoveBonus; }
        	set { m_MoveBonus = value; }
        }

        private uint m_PhysicalSEV;
        public uint PhysicalSEV
        {
        	get { return m_PhysicalSEV; }
        	set { m_PhysicalSEV = value; }
        }

        private uint m_MagicSEV;
        public uint MagicSEV
        {
        	get { return m_MagicSEV; }
        	set { m_MagicSEV = value; }
        }

        private uint m_HPBonus;
        public uint HPBonus
        {
        	get { return m_HPBonus; }
        	set { m_HPBonus = value; }
        }

        private uint m_MPBonus;
        public uint MPBonus
        {
        	get { return m_MPBonus; }
        	set { m_MPBonus = value; }
        }

        private uint m_PhysicalAEV;
        public uint PhysicalAEV
        {
        	get { return m_PhysicalAEV; }
        	set { m_PhysicalAEV = value; }
        }

        private uint m_MagicAEV;
        public uint MagicAEV
        {
        	get { return m_MagicAEV; }
        	set { m_MagicAEV = value; }
        }



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
            this.BlockRate = i.BlockRate;
            this.HPBonus = i.HPBonus;
            this.JumpBonus = i.JumpBonus;
            this.MABonus = i.MABonus;
            this.MagicSEV = i.MagicSEV;
            this.MoveBonus = i.MoveBonus;
            this.MPBonus = i.MPBonus;
            this.PABonus = i.PABonus;
            this.PhysicalSEV = i.PhysicalSEV;
            this.Power = i.Power;
            this.SpeedBonus = i.SpeedBonus;
            this.PhysicalAEV = i.PhysicalAEV;
            this.MagicAEV = i.MagicAEV;
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
                        newItem.Name = i.SelectSingleNode( "name" ).InnerText;
                        newItem.Offset = Convert.ToUInt16( i.Attributes["offset"].InnerText );
                        newItem.Type = (ItemType)Enum.Parse( typeof( ItemType ), i.Attributes["type"].InnerText );
                        newItem.SubType = (ItemSubType)Enum.Parse( typeof( ItemSubType ), i.Attributes["subtype"].InnerText );

                        XmlNode node = i.SelectSingleNode( "power" );
                        if( node != null ) { newItem.Power = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "blockRate" );
                        if( node != null ) { newItem.BlockRate = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "ma" );
                        if( node != null ) { newItem.MABonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "pa" );
                        if( node != null ) { newItem.PABonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "speed" );
                        if( node != null ) { newItem.SpeedBonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "jump" );
                        if( node != null ) { newItem.JumpBonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "move" );
                        if( node != null ) { newItem.MoveBonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "physicalSEV" );
                        if( node != null ) { newItem.PhysicalSEV = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "magicSEV" );
                        if( node != null ) { newItem.MagicSEV = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "hp" );
                        if( node != null ) { newItem.HPBonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "mp" );
                        if( node != null ) { newItem.MPBonus = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "magicAEV" );
                        if( node != null ) { newItem.MagicAEV = Convert.ToUInt32( node.InnerText ); }

                        node = i.SelectSingleNode( "physicalAEV" );
                        if( node != null ) { newItem.PhysicalAEV = Convert.ToUInt32( node.InnerText ); }

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
