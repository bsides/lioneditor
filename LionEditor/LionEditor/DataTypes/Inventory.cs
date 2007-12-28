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

namespace LionEditor
{
    /// <summary>
    /// Represents a single item in the inventory
    /// </summary>
    public class InventoryEntry : IComparable<InventoryEntry>, IEquatable<InventoryEntry>
    {
        #region Fields

        private byte quantity;
        private byte equipped;
        private Item item;
        private static InventoryEntry dummyEntry;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the quantity of this item in the inventory
        /// </summary>
        public byte InventoryQuantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        /// <summary>
        /// Gets or sets the number of this item equipped by characters
        /// </summary>
        public byte Equipped
        {
            get { return equipped; }
            set { equipped = value; }
        }

        /// <summary>
        /// Gets the <see cref="Item"/> represented by this entry
        /// </summary>
        public Item Item
        {
            get { return item; }
        }

        /// <summary>
        /// Gets the type of item represented by this entry
        /// </summary>
        public string Type
        {
            get { return Item.Type.ToString(); }
        }

        /// <summary>
        /// Gets the sub-type of item represented by this entry
        /// </summary>
        public string SubType
        {
            get { return Item.SubType.ToString(); }
        }

        /// <summary>
        /// Gets the total number of this item in inventory+equipped
        /// </summary>
        public byte Total
        {
            get { return (byte)(quantity + equipped); }
        }

        /// <summary>
        /// A dummy entry for invalid items
        /// </summary>
        public static InventoryEntry DummyEntry
        {
            get
            {
                if( dummyEntry == null )
                {
                    dummyEntry = new InventoryEntry( Item.ItemList[0], 0x00 );
                }

                return dummyEntry;
            }
        }

        #endregion

        #region Constructors

        public InventoryEntry( Item item, byte quantity )
            : this( item, quantity, 0 )
        {
        }

        public InventoryEntry( Item item, byte quantity, byte equipped )
        {
            this.item = item;
            this.quantity = quantity;
            this.equipped = equipped;
        }

        #endregion

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

    /// <summary>
    /// Represents an Inventory in memory
    /// </summary>
    public class Inventory
    {
        #region Fields

        private Dictionary<ItemType, List<InventoryEntry>> filteredItems;
        private List<InventoryEntry> inventory;
        private int maxQuantity;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a dictionary of all <see cref="InventoryEntry"/>s, filtered by <see cref="ItemType"/>
        /// </summary>
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

        /// <summary>
        /// Gets an unfiltered list of all <see cref="InventoryEntry"/>s in this inventory
        /// </summary>
        public List<InventoryEntry> Items
        {
            get { return inventory; }
        }

        /// <summary>
        /// Gets the maximum quantity for items in this inventory
        /// </summary>
        public int MaxQuantity
        {
            get { return maxQuantity; }
        }

        #endregion

        #region Utilities

        public void UpdateEquippedQuantities(List<Character> characters)
        {
            foreach (InventoryEntry item in this.Items)
            {
                item.Equipped = 0;
            }

            foreach (Character c in characters)
            {
                foreach (Item i in new Item[] { c.RightShield, c.RightHand, c.LeftShield, c.LeftHand, c.Accessory, c.Head, c.Body })
                {
                    Items[i.Offset].Equipped += 1;
                }
            }
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

            result[0] = 0;
            result[254] = 0;
            result[255] = 0;

            return result;
        }

        #endregion

        public Inventory( byte[] bytes, int maxQuantity )
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

    }
}
