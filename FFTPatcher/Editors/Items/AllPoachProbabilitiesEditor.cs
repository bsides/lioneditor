/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Drawing;
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class AllPoachProbabilitiesEditor : UserControl
    {
        public AllPoachProbabilitiesEditor()
        {
            InitializeComponent();

            CommonItem.ValueType = typeof( Item );
            UncommonItem.ValueType = typeof( Item );

            dataGridView.AutoSize = true;
            dataGridView.CellParsing += dataGridView_CellParsing;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.EditingControlShowing += dataGridView_EditingControlShowing;
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            dataGridView.CellToolTipTextNeeded += dataGridView_CellToolTipTextNeeded;
        }

        public void UpdateView( AllPoachProbabilities probs )
        {
            dataGridView.DataSource = null;
            CommonItem.Items.Clear();
            UncommonItem.Items.Clear();
            foreach( Item i in Item.DummyItems )
            {
                if( i.Offset <= 0xFF )
                {
                    CommonItem.Items.Add( i );
                    UncommonItem.Items.Add( i );
                }
            }
            dataGridView.DataSource = probs.PoachProbabilities;
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

        private void dataGridView_CellToolTipTextNeeded( object sender, DataGridViewCellToolTipTextNeededEventArgs e )
        {
            if( (e.RowIndex >= 0) && (e.ColumnIndex >= 0) &&
                (dataGridView[e.ColumnIndex, e.RowIndex] is DataGridViewComboBoxCell) &&
                (dataGridView.Rows[e.RowIndex].DataBoundItem is PoachProbability) )
            {
                PoachProbability poach = dataGridView.Rows[e.RowIndex].DataBoundItem as PoachProbability;
                if( poach.Default != null )
                {
                    Item i = ReflectionHelpers.GetFieldOrProperty<Item>( poach.Default, dataGridView.Columns[e.ColumnIndex].DataPropertyName );
                    e.ToolTipText = "Default: " + i.Name;
                }
            }
        }

        private void dataGridView_CellFormatting( object sender, DataGridViewCellFormattingEventArgs e )
        {
            if( (e.ColumnIndex == CommonItem.Index) ||
                (e.ColumnIndex == UncommonItem.Index) )
            {
                if( (e.RowIndex >= 0) && (e.ColumnIndex >= 0) &&
                    (dataGridView[e.ColumnIndex, e.RowIndex] is DataGridViewComboBoxCell) &&
                    (dataGridView.Rows[e.RowIndex].DataBoundItem is PoachProbability) )
                {
                    PoachProbability poach = dataGridView.Rows[e.RowIndex].DataBoundItem as PoachProbability;
                    if( poach.Default != null )
                    {
                        Item i = ReflectionHelpers.GetFieldOrProperty<Item>( poach.Default, dataGridView.Columns[e.ColumnIndex].DataPropertyName );
                        if( i != (e.Value as Item) )
                        {
                            e.CellStyle.BackColor = Color.Blue;
                            e.CellStyle.ForeColor = Color.White;
                        }
                    }
                }
            }
        }
    }
}
