using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Resources;

namespace LionEditor
{
    public struct Item
    {
        public ItemType Type;
        public ItemSubType SubType;
        public UInt16 Offset;

        public string Name;

        public Item( UInt16 offset )
        {
            Item? i = ItemList.Find(
                delegate( Item j )
                {
                    return j.Offset == offset;
                } );

            if( i.HasValue )
            {
                this.Type = i.Value.Type;
                this.SubType = i.Value.SubType;
                this.Name = i.Value.Name;
                this.Offset = offset;
            }
            else
            {
                this.Type = ItemType.Accessory;
                this.SubType = ItemSubType.Armguard;
                this.Name = string.Empty;
                this.Offset = 0;
            }
        }

        public override string ToString()
        {
            return this.Name;
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
                    System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                    d.Load( a.GetManifestResourceStream( "Items" ) );

                    XmlNodeList items = d.SelectNodes( "//Item" );

                    itemList = new List<Item>( items.Count );

                    foreach( XmlNode i in items )
                    {
                        Item newItem;
                        newItem.Name = i.InnerText;
                        newItem.Offset = Convert.ToUInt16( i.Attributes["offset"].InnerText );
                        newItem.Type = (ItemType)Enum.Parse( typeof( ItemType ), i.Attributes["type"].InnerText );
                        newItem.SubType = (ItemSubType)Enum.Parse( typeof( ItemSubType ), i.Attributes["subtype"].InnerText );

                        itemList.Add( newItem );
                    }
                }

                return itemList;
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
