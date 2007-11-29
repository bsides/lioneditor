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

namespace LionEditor
{
    public class InventoryEntry:IComparable<InventoryEntry>, IEquatable<InventoryEntry>
    {
        private byte m_Quantity;
        public byte InventoryQuantity
        {
        	get { return m_Quantity; }
        	set { m_Quantity = value; }
        }

        private byte m_Equipped;
        public byte Equipped
        {
        	get { return m_Equipped; }
        	set { m_Equipped = value; }
        }

        private Item m_Item;
        public Item Item
        {
        	get { return m_Item; }
        }

        public string Type
        {
            get { return Item.Type.ToString(); }
        }

        public string SubType
        {
            get { return Item.SubType.ToString(); }
        }


        public byte Total
        {
            get { return (byte)(m_Quantity + m_Equipped); }
        }

        private static InventoryEntry dummyEntry;
        public static InventoryEntry DummyEntry
        {
            get
            {
                if( dummyEntry == null )
                {
                    dummyEntry = new InventoryEntry( Item.ItemList[0], 0xFF );
                }

                return dummyEntry;
            }
        }

        public InventoryEntry( Item item, byte quantity )
            : this( item, quantity, 0 )
        {
        }

        public InventoryEntry( Item item, byte quantity, byte equipped )
        {
            m_Item = item;
            m_Quantity = quantity;
            m_Equipped = equipped;
        }


        #region IComparable<InventoryEntry> Members

        public int CompareTo( InventoryEntry other )
        {
            int typeComparison = this.Type.CompareTo( other.Type );
            int subTypeComparison = this.SubType.CompareTo( other.SubType );
            int nameComparison = this.Item.Name.CompareTo( other.Item.Name );
            return (typeComparison == 0) ? ((subTypeComparison == 0) ? nameComparison : subTypeComparison) : typeComparison;
        }

        #endregion

        #region IEquatable<InventoryEntry> Members

        public bool Equals( InventoryEntry other )
        {
            return this.Item.Name.Equals( other.Item.Name );
        }

        #endregion
    }

    public class Inventory
    {
        private Dictionary<ItemType, List<InventoryEntry>> filteredItems;
        public Dictionary<ItemType, List<InventoryEntry>> FilteredItems
        {
            get
            {
                if( filteredItems == null )
                {
                    filteredItems = new Dictionary<ItemType, List<InventoryEntry>>();
                    foreach( ItemType type in Enum.GetValues( typeof( ItemType ) ) )
                    {
                        filteredItems.Add( type, new List<InventoryEntry>() );
                    }

                    foreach( InventoryEntry i in Items )
                    {
                        if( i.Item.Type != ItemType.None )
                        {
                            filteredItems[i.Item.Type].Add( i );
                            filteredItems[ItemType.None].Add( i );
                        }
                    }

                    foreach( ItemType type in Enum.GetValues( typeof( ItemType ) ) )
                    {
                        filteredItems[type].Sort();
                    }

                }

                return filteredItems;
            }
        }


        public void UpdateEquippedQuantities( Character[][] characters )
        {
            foreach( InventoryEntry item in this.Items )
            {
                item.Equipped = 0;
            }

            foreach( Character[] arr in characters )
            {
                foreach( Character c in arr )
                {
                    foreach( Item i in new Item[] { c.RightShield, c.RightHand, c.LeftShield, c.LeftHand, c.Accessory, c.Head, c.Body } )
                    {
                        Items[i.Offset].Equipped += 1;
                    }
                }
            }
        }

        private List<InventoryEntry> inventory;

        public List<InventoryEntry> Items
        {
            get { return inventory; }
        }

        private int maxQuantity;
        public int MaxQuantity
        {
            get { return maxQuantity; }
        }

        public Inventory(byte[] bytes, int maxQuantity)
        {
            this.maxQuantity = maxQuantity;

            inventory = new List<InventoryEntry>( 316 );

            inventory.Add( InventoryEntry.DummyEntry );

            for( ushort i = 1; i < 254; i++ )
            {
                inventory.Add( new InventoryEntry( Item.ItemList.Find(
                    delegate( Item that )
                    {
                        return (that.Offset == i);
                    } ), bytes[i] ) );
            }
            inventory.Add( InventoryEntry.DummyEntry );
            inventory.Add( InventoryEntry.DummyEntry );

            for( ushort i = 256; i < 316; i++ )
            {
                inventory.Add( new InventoryEntry( Item.ItemList.Find(
                    delegate( Item that )
                    {
                        return (that.Offset == i);
                    } ), bytes[i] ) );
            }
        }

        public Inventory( byte[] bytes )
            : this( bytes, 99 )
        {
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[316];
            foreach( InventoryEntry item in inventory )
            {
                int count = 0;

                if( (item.Total > maxQuantity) || (item.InventoryQuantity > maxQuantity) )
                {
                    count = maxQuantity - item.Equipped;
                }
                else
                {
                    count = item.InventoryQuantity;
                }

                result[item.Item.Offset] = (byte)count;
            }

            return result;
        }
    }
}
