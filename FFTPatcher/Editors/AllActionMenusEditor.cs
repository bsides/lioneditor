using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using FFTPatcher.Properties;

namespace FFTPatcher.Editors
{
    public partial class AllActionMenusEditor : UserControl
    {
        public AllActionMenus AllActionMenus { get; private set; }

        public AllActionMenusEditor()
        {
            InitializeComponent();

            AllActionMenus = new AllActionMenus( new SubArray<byte>( new List<byte>( Resources.ActionEventsBin ), 0 ) );
            for( int i = 0; i < ActionMenuEntry.AllActionMenuEntries.Count; i++ )
            {
                ActionColumn.Items.Add( ActionMenuEntry.AllActionMenuEntries[i] );
            }
            ActionColumn.ValueType = typeof( ActionMenuEntry );

            dataGridView.AutoSize = true;
            dataGridView.CellParsing += dataGridView_CellParsing;

            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = AllActionMenus.ActionMenus;
            dataGridView.EditingControlShowing += dataGridView_EditingControlShowing;
            dataGridView.DataError += new DataGridViewDataErrorEventHandler( dataGridView_DataError );
        }

        void dataGridView_DataError( object sender, DataGridViewDataErrorEventArgs e )
        {
            e.Cancel = true;
        }

        private void dataGridView_CellParsing( object sender, DataGridViewCellParsingEventArgs e )
        {
            DataGridViewComboBoxEditingControl c = dataGridView.EditingControl as DataGridViewComboBoxEditingControl;
            if( c != null )
            {
                e.Value = c.SelectedItem;
                e.ParsingApplied = true;
            }
        }

        private void dataGridView_EditingControlShowing( object sender, DataGridViewEditingControlShowingEventArgs e )
        {
            DataGridViewComboBoxEditingControl c = e.Control as DataGridViewComboBoxEditingControl;
            if( c != null )
            {
                c.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }
    }
}
