using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class StoreInventoryEditor : BaseEditor
    {
        private StoreInventory storeInventory;
        private List<ListBox> listBoxes;
        private Dictionary<Type, ListBox> fromListBoxes;
        private Dictionary<Type, ListBox> toListBoxes;

        public StoreInventoryEditor()
        {
            InitializeComponent();
            listBoxes = new List<ListBox>();
            listBoxes.AddRange( new ListBox[] {
                weaponsToListBox, weaponsFromListBox, itemsToListBox, itemsFromListBox,
                accessoriesToListBox, accessoriesFromListBox, armorFromListBox, armorToListBox,
                shieldsFromListBox, shieldsToListBox } );
            fromListBoxes = new Dictionary<Type, ListBox> { 
                { typeof(Weapon), weaponsFromListBox },
                { typeof(ChemistItem), itemsFromListBox },
                { typeof(Accessory), accessoriesFromListBox },
                { typeof(Armor), armorFromListBox },
                { typeof(Shield), shieldsFromListBox } };
            toListBoxes = new Dictionary<Type, ListBox> { 
                { typeof(Weapon), weaponsToListBox },
                { typeof(ChemistItem), itemsToListBox },
                { typeof(Accessory), accessoriesToListBox },
                { typeof(Armor), armorToListBox },
                { typeof(Shield), shieldsToListBox } };
        }

        public StoreInventory StoreInventory
        {
            get
            {
                return storeInventory;
            }
            set
            {
                if ( value == null )
                {
                    Enabled = false;
                    storeInventory = null;
                }
                else if (value != storeInventory)
                {
                    storeInventory = value;
                    Enabled = true;
                    UpdateView();
                }
            }
        }


        private void UpdateView()
        {
            foreach ( var listbox in listBoxes )
            {
                listbox.BeginUpdate();
                listbox.SuspendLayout();
                listbox.Items.Clear();
            }

            for ( int i = 0; i < 254; i++ )
            {
                if ( StoreInventory[Item.DummyItems[i]] )
                {
                    toListBoxes[FFTPatch.Items.Items[i].GetType()].Items.Add( Item.DummyItems[i] );
                }
                else
                {
                    fromListBoxes[FFTPatch.Items.Items[i].GetType()].Items.Add( Item.DummyItems[i] );
                }
            }

            for ( int i = 254; i < 256; i++ )
            {
                if ( StoreInventory[Item.DummyItems[i]] )
                {
                    toListBoxes[typeof( ChemistItem )].Items.Add( Item.DummyItems[i] );
                }
                else
                {
                    fromListBoxes[typeof( ChemistItem )].Items.Add( Item.DummyItems[i] );
                }
            }
            foreach ( var listbox in listBoxes )
            {
                listbox.ResumeLayout();
                listbox.EndUpdate();
            }
        }

        private void addDualList_AfterAction( object sender, FFTPatcher.Controls.DualListActionEventArgs e )
        {
            Item i = e.Item as Item;
            this.StoreInventory[i] = true;
            OnDataChanged( this, EventArgs.Empty );
        }

        private void removeDualList_AfterAction( object sender, FFTPatcher.Controls.DualListActionEventArgs e )
        {
            Item i = e.Item as Item;
            this.StoreInventory[i] = false;
            OnDataChanged( this, EventArgs.Empty );
        }
    }
}
