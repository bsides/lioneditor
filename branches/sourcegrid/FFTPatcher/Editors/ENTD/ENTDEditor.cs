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

using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class ENTDEditor : UserControl
    {

		#region Constructors (1) 

        public ENTDEditor()
        {
            InitializeComponent();
        }

		#endregion Constructors 

		#region Methods (2) 


        private void eventListBox_SelectedIndexChanged( object sender, System.EventArgs e )
        {
            eventEditor1.Event = eventListBox.SelectedItem as Event;
        }

        public void UpdateView( AllENTDs entds )
        {
            eventListBox.SelectedIndexChanged -= eventListBox_SelectedIndexChanged;
            eventListBox.DataSource = entds.Events;
            eventListBox.SelectedIndex = 0;
            eventListBox.SelectedIndexChanged += eventListBox_SelectedIndexChanged;
            eventEditor1.Event = eventListBox.SelectedItem as Event;
        }


		#endregion Methods 

    }
}
