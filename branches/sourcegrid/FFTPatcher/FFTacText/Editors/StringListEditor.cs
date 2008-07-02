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

namespace FFTPatcher.TextEditor
{
    /// <summary>
    /// An editor that edits lists of strings.
    /// </summary>
    public partial class StringListEditor : UserControl
    {

		#region Properties (1) 


        /// <summary>
        /// Gets the current row.
        /// </summary>
        /// <value>The current row.</value>
        public int CurrentRow
        {
            get { return (int)dataGridView.CurrentRow.Cells[numberColumn.Name].Value; }
        }


		#endregion Properties 

		#region Constructors (1) 

        /// <summary>
        /// Initializes a new instance of the <see cref="StringListEditor"/> class.
        /// </summary>
        public StringListEditor()
        {
            InitializeComponent();
            dataGridView.CellEndEdit += dataGridView_CellEndEdit;
            dataGridView.EditingControlShowing += dataGridView_EditingControlShowing;
            textColumn.DefaultCellStyle.Font = new Font( "Arial Unicode MS", 9 );
        }

		#endregion Constructors 

		#region Events (2) 

        /// <summary>
        /// Occurs when a cell is validating.
        /// </summary>
        public event DataGridViewCellValidatingEventHandler CellValidating
        {
            add { dataGridView.CellValidating += value; }
            remove { dataGridView.CellValidating -= value; }
        }

        /// <summary>
        /// Occurs when text in a textbox has changed.
        /// </summary>
        public event EventHandler TextBoxTextChanged;

		#endregion Events 

		#region Methods (4) 


        private void dataGridView_CellEndEdit( object sender, DataGridViewCellEventArgs e )
        {
            if( dataGridView.EditingControl is TextBox )
            {
                dataGridView.EditingControl.TextChanged -= tb_TextChanged;
            }
        }

        private void dataGridView_EditingControlShowing( object sender, DataGridViewEditingControlShowingEventArgs e )
        {
            if( dataGridView.CurrentCell.ColumnIndex == textColumn.Index && dataGridView.EditingControl is TextBox )
            {
                TextBox tb = dataGridView.EditingControl as TextBox;
                tb.Font = new Font( "Arial Unicode MS", 9 );
                tb.TextChanged += tb_TextChanged;
            }
        }

        private void tb_TextChanged( object sender, EventArgs e )
        {
            if( TextBoxTextChanged != null )
            {
                TextBoxTextChanged( sender, e );
            }
        }

        /// <summary>
        /// Binds this editor to a list of strings.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <param name="values">The values.</param>
        public void BindTo( IList<string> names, IList<string> values )
        {
            List<string> ourNames = new List<string>( names );
            for ( int i = names.Count; i < values.Count; i++ )
            {
                ourNames.Add( string.Empty );
            }

            DataGridViewRow[] rows = new DataGridViewRow[values.Count];
            dataGridView.SuspendLayout();
            for( int i = 0; i < values.Count; i++ )
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells( dataGridView, i, ourNames[i], values[i] );
                rows[i] = row;
            }
            dataGridView.Rows.Clear();
            dataGridView.Rows.AddRange( rows );
            dataGridView.ResumeLayout();
        }


		#endregion Methods 

    }
}
