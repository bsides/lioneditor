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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using SourceGrid.Cells;

namespace FFTPatcher.TextEditor
{
    public partial class StringListEditor : UserControl
    {


        #region Fields (2)

        private IList<string> names;
        private IList<string> values;

        #endregion Fields


        #region Constructors (1)

        public StringListEditor()
        {
            InitializeComponent();
            dataGridView.CellEndEdit += dataGridView_CellEndEdit;
            dataGridView.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler( dataGridView_EditingControlShowing );
            textColumn.DefaultCellStyle.Font = new Font( "Arial Unicode MS", 10 );
        }

        private void dataGridView_EditingControlShowing( object sender, DataGridViewEditingControlShowingEventArgs e )
        {
            if( dataGridView.CurrentCell.ColumnIndex == textColumn.Index && dataGridView.EditingControl is TextBox )
            {
                TextBox tb = dataGridView.EditingControl as TextBox;
                tb.Font = new Font( "Arial Unicode MS", 10 );
                tb.TextChanged += tb_TextChanged;
            }
        }

        public int CurrentRow
        {
            get { return dataGridView.CurrentRow.Index; }
        }

        private void dataGridView_CellValidating( object sender, DataGridViewCellValidatingEventArgs e )
        {
            throw new NotImplementedException();
        }

        private void dataGridView_CellEndEdit( object sender, DataGridViewCellEventArgs e )
        {
            if( dataGridView.EditingControl is TextBox )
            {
                (dataGridView.EditingControl).TextChanged -= tb_TextChanged;
            }
        }

        private void tb_TextChanged( object sender, EventArgs e )
        {
            if( TextBoxTextChanged != null )
            {
                TextBoxTextChanged( sender, e );
            }
        }

        #endregion Constructors


        #region Events (1)

        public event EventHandler TextBoxTextChanged;
        public event DataGridViewCellValidatingEventHandler CellValidating
        {
            add { dataGridView.CellValidating += value; }
            remove { dataGridView.CellValidating -= value; }
        }

        #endregion Events


        #region Methods (7)

        public void BindTo( IList<string> names, IList<string> values )
        {
            if( names.Count < values.Count )
            {
                throw new ArgumentException( "names and values must have same count" );
            }

            DataGridViewRow[] rows = new DataGridViewRow[values.Count];

            dataGridView.SuspendLayout();
            for( int i = 0; i < values.Count; i++ )
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells( dataGridView, i, names[i], values[i] );
                rows[i] = row;
            }
            dataGridView.Rows.Clear();
            dataGridView.Rows.AddRange( rows );
            dataGridView.ResumeLayout();
        }


        #endregion Methods


    }
}
