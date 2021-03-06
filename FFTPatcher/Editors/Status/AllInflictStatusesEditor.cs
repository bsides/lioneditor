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
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class AllInflictStatusesEditor : UserControl
    {
		#region Public Properties (1) 

        public int SelectedIndex { get { return offsetListBox.SelectedIndex; } set { offsetListBox.SelectedIndex = value; } }

		#endregion Public Properties 

		#region Constructors (1) 

        public AllInflictStatusesEditor()
        {
            InitializeComponent();
            inflictStatusEditor.DataChanged += new EventHandler( inflictStatusEditor_DataChanged );
        }

		#endregion Constructors 

		#region Public Methods (1) 

        public void UpdateView( AllInflictStatuses statuses )
        {
            offsetListBox.SelectedIndexChanged -= offsetListBox_SelectedIndexChanged;
            offsetListBox.DataSource = statuses.InflictStatuses;
            offsetListBox.SelectedIndexChanged += offsetListBox_SelectedIndexChanged;
            offsetListBox.SelectedIndex = 0;
            inflictStatusEditor.InflictStatus = offsetListBox.SelectedItem as InflictStatus;
        }

		#endregion Public Methods 

		#region Private Methods (2) 

        private void inflictStatusEditor_DataChanged( object sender, EventArgs e )
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[offsetListBox.DataSource];
            cm.Refresh();
        }

        private void offsetListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            inflictStatusEditor.InflictStatus = offsetListBox.SelectedItem as InflictStatus;
        }

		#endregion Private Methods 
    }
}
