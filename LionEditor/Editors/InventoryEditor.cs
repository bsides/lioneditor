using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LionEditor.Editors
{
    public partial class InventoryEditor : UserControl
    {
        ItemType filter = ItemType.None;

        private void Filter( ItemType type )
        {
            filter = type;
            SetDataSource();
        }

        private bool addingRows = false;

        public void SetDataSource()
        {
            addingRows = true;
            if( inventory != null )
            {
                dataGridView.DataSource = inventory.FilteredItems[filter];
            }
            addingRows = false;
        }

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
            //dataGridView.CellValidating += new DataGridViewCellValidatingEventHandler( dataGridView_CellValidating );
        }

        void dataGridView_CellValueChanged( object sender, DataGridViewCellEventArgs e )
        {
            if( !addingRows )
            {
                FireDataChangedEvent();
            }
        }

        void dataGridView_CellValidating( object sender, DataGridViewCellValidatingEventArgs e )
        {
            if( e.ColumnIndex == dataGridView.Columns["Item"].Index )
            {
                int value;
                if( !Int32.TryParse( dataGridView[e.ColumnIndex, e.RowIndex].Value.ToString(), out value ) || (value < 0) || (value > this.inventory.MaxQuantity) )
                {
                    e.Cancel = true;
                }
            }
        }

        void filterComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            Filter( (ItemType)filterComboBox.SelectedItem );
        }

        private Inventory inventory;

        public Inventory Inventory
        {
            get
            {
                return inventory;
            }
            set
            {
                inventory = value;
                if( value != null )
                {
                    SetDataSource();
                }
            }
        }

        public event EventHandler DataChangedEvent;

        private void FireDataChangedEvent()
        {
            if( DataChangedEvent != null )
            {
                DataChangedEvent( this, EventArgs.Empty );
            }
        }

    }
}
