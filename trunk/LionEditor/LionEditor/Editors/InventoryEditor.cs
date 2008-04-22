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
using System.Windows.Forms;

namespace LionEditor
{
    public partial class InventoryEditor : UserControl
    {
        #region Fields

        private ItemType filter = ItemType.None;
        private bool addingRows = false;
        private Inventory inventory;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the inventory being edited
        /// </summary>
        public Inventory Inventory
        {
            get { return inventory; }
            set
            {
                inventory = value;
                if( value != null )
                {
                    SetDataSource();
                }
            }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Filters the data grid view by item type
        /// </summary>
        private void Filter( ItemType type )
        {
            filter = type;
            SetDataSource();
        }

        public void SetDataSource()
        {
            addingRows = true;
            if( inventory != null )
            {
                dataGridView.DataSource = inventory.FilteredItems[filter];
            }
            addingRows = false;
        }

        #endregion

        #region Events

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if( !addingRows )
            {
                FireDataChangedEvent();
            }
        }

        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter( (ItemType)filterComboBox.SelectedItem );
        }

        public event EventHandler DataChangedEvent;

        private void FireDataChangedEvent()
        {
            if( DataChangedEvent != null )
            {
                DataChangedEvent( this, EventArgs.Empty );
            }
        }

        #endregion

        public InventoryEditor()
        {
            InitializeComponent();
            dataGridView.AutoGenerateColumns = false;

            foreach( ItemType type in Enum.GetValues( typeof( ItemType ) ) )
            {
                filterComboBox.Items.Add( type );
            }

            filterComboBox.SelectedIndexChanged += new EventHandler( filterComboBox_SelectedIndexChanged );
            filterComboBox.SelectedItem = ItemType.None;
            dataGridView.CellValueChanged += new DataGridViewCellEventHandler( dataGridView_CellValueChanged );
        }
    }
}
