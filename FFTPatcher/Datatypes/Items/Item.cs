/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

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
using FFTPatcher.Properties;

namespace FFTPatcher.Datatypes
{
    public class Item
    {
        public static List<string> PSXNames { get; private set; }
        public static List<string> PSPNames { get; private set; }
        public static List<string> ItemNames
        {
            get
            {
                return FFTPatch.Context == Context.US_PSP ? PSPNames : PSXNames;
            }
        }

        public static List<Item> PSPDummies { get; private set; }
        public static List<Item> PSXDummies { get; private set; }
        public static List<Item> DummyItems
        {
            get
            {
                return FFTPatch.Context == Context.US_PSP ? PSPDummies : PSXDummies;
            }
        }

        static Item()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( Resources.Items );
            PSPNames = new List<string>( 316 );
            PSPDummies = new List<Item>( 316 );
            for( int i = 0; i < 316; i++ )
            {
                PSPNames.Add( doc.SelectSingleNode( string.Format( "//Item[@offset='{0}']/name", i ) ).InnerText );
                Item item = new Item();
                item.Offset = (UInt16)i;
                item.Name = PSPNames[i];
                PSPDummies.Add( item );
            }

            doc = new XmlDocument();
            doc.LoadXml( PSXResources.Items );
            PSXNames = new List<string>( 256 );
            PSXDummies = new List<Item>( 256 );
            for( int i = 0; i < 256; i++ )
            {
                PSXNames.Add( doc.SelectSingleNode( string.Format( "//Item[@offset='{0}']/name", i ) ).InnerText );
                Item item = new Item();
                item.Offset = (UInt16)i;
                item.Name = PSXNames[i];
                PSXDummies.Add( item );
            }
        }

        public static Item GetItemAtOffset( UInt16 offset )
        {
            return DummyItems.Find(
                delegate( Item i )
                {
                    return i.Offset == offset;
                } );
        }

        public UInt16 Offset { get; private set; }
        public string Name { get; private set; }
        public byte Palette { get; set; }
        public byte Graphic { get; set; }
        public byte EnemyLevel { get; set; }

        private bool weapon;
        private bool shield;
        private bool head;
        private bool body;
        private bool accessory;
        private bool blank1;
        private bool rare;
        private bool blank2;
        public bool Weapon { get { return weapon; } set { weapon = value; } }
        public bool Shield { get { return shield; } set { shield = value; } }
        public bool Head { get { return head; } set { head = value; } }
        public bool Body { get { return body; } set { body = value; } }
        public bool Accessory { get { return accessory; } set { accessory = value; } }
        public bool Blank1 { get { return blank1; } set { blank1 = value; } }
        public bool Rare { get { return rare; } set { rare = value; } }
        public bool Blank2 { get { return blank2; } set { blank2 = value; } }

        public byte SecondTableId { get; set; }
        public ItemSubType ItemType { get; set; }
        public byte Unknown1 { get; set; }
        public byte SIA { get; set; }
        public UInt16 Price { get; set; }
        public ShopAvailability ShopAvailability { get; set; }
        public byte Unknown2 { get; set; }

        private Item()
        {
        }

        protected Item( UInt16 offset, SubArray<byte> bytes )
        {
            Name = ItemNames[offset];
            Offset = offset;
            Palette = bytes[0];
            Graphic = bytes[1];
            EnemyLevel = bytes[2];
            Utilities.CopyByteToBooleans( bytes[3], ref weapon, ref shield, ref head, ref body, ref accessory, ref blank1, ref rare, ref blank2 );
            SecondTableId = bytes[4];
            ItemType = (ItemSubType)bytes[5];
            Unknown1 = bytes[6];
            SIA = bytes[7];
            Price = Utilities.BytesToUShort( bytes[8], bytes[9] );
            ShopAvailability = ShopAvailability.AllAvailabilities[bytes[10]];
            Unknown2 = bytes[11];
        }

        protected List<byte> ToByteArray()
        {
            List<byte> result = new List<byte>( 12 );
            result.Add( Palette );
            result.Add( Graphic );
            result.Add( EnemyLevel );
            result.Add( Utilities.ByteFromBooleans( weapon, shield, head, body, accessory, blank1, rare, blank2 ) );
            result.Add( SecondTableId );
            result.Add( (byte)ItemType );
            result.Add( Unknown1 );
            result.Add( SIA );
            result.AddRange( Utilities.UShortToBytes( Price ) );
            result.Add( ShopAvailability.ToByte() );
            result.Add( Unknown2 );
            return result;
        }

        public virtual byte[] ToFirstByteArray()
        {
            return new byte[0];
        }

        public virtual byte[] ToSecondByteArray()
        {
            return new byte[0];
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ShopAvailability
    {
        private static readonly string[] events = new string[] {
            "<Blank>",
            "Chapter 1 - Start",
            "Chapter 1 - Enter Eagrose",
            "Chapter 1 - Save Elmdore",
            "Chapter 1 - Kill Milleuda",
            "Chapter 2 - Start",
            "Chapter 2 - Save Ovelia",
            "Chapter 2 - Meet Dracleau",
            "Chapter 2 - Save Agrias",
            "Chapter 3 - Start",
            "Chapter 3 - Zalmour",
            "Chapter 3 - Meet Belias",
            "Chapter 3 - Save Rapha",
            "Chapter 4 - Start",
            "Chapter 4 - Bethla",
            "Chapter 4 - Kill Elmdore",
            "Chapter 4 - Kill Zalbaag",
            "<Blank>",
            "<Blank>",
            "<Blank>",
            "<Blank>" };

        private string name;
        private static List<ShopAvailability> all;
        public static List<ShopAvailability> AllAvailabilities
        {
            get
            {
                if( all == null )
                {
                    all = new List<ShopAvailability>( 256 );
                    for( byte i = 0; i < events.Length; i++ )
                    {
                        ShopAvailability a = new ShopAvailability();
                        a.b = i;
                        a.name = events[i];
                        all.Add( a );
                    }
                    for( int i = events.Length; i <= 0xFF; i++ )
                    {
                        ShopAvailability a = new ShopAvailability();
                        a.b = (byte)i;
                        a.name = string.Format( "Unknown ({0})", i );
                        all.Add( a );
                    }
                }

                return all;
            }
        }

        private byte b;
        private ShopAvailability()
        {
        }

        public override string ToString()
        {
            return name;
        }

        public byte ToByte()
        {
            return b;
        }
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
        Nothing,

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

        [Description( "Throwing" )]
        Throwing,

        [Description( "Bomb" )]
        Bomb,

        [Description( "Chemist Item" )]
        None,

        [Description( "Fell Sword" )]
        FellSword,

        [Description( "Lip Rouge" )]
        LipRouge
    }
}
