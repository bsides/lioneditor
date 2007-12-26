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
    public partial class AllPoachProbabilitiesEditor : UserControl
    {
        public AllPoachProbabilities PoachProbabilities { get; private set; }

        public AllPoachProbabilitiesEditor()
        {
            InitializeComponent();
            PoachProbabilities = new AllPoachProbabilities( new SubArray<byte>( new List<byte>( Resources.PoachProbabilitiesBin ), 0 ) );
            foreach( Item i in Item.ItemList )
            {
                if( i.Offset <= 0xFF )
                {
                    CommonItem.Items.Add( i );
                    UncommonItem.Items.Add( i );
                }
            }

            CommonItem.ValueType = typeof( Item );
            UncommonItem.ValueType = typeof( Item );

            dataGridView.AutoSize = true;
            dataGridView.CellParsing += dataGridView_CellParsing;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = PoachProbabilities.PoachProbabilities;
            dataGridView.EditingControlShowing += dataGridView_EditingControlShowing;
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
