/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

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

using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class EventEditor : UserControl
    {
        private Event evt;
        public Event Event
        {
            get { return evt; }
            set
            {
                if( value == null )
                {
                    evt = null;
                    Enabled = false;
                }
                else if( value != evt )
                {
                    evt = value;
                    UpdateView();
                    Enabled = true;
                }
            }
        }

        public EventEditor()
        {
            InitializeComponent();
            unitSelectorListBox.SelectedIndexChanged += unitSelectorComboBox_SelectedIndexChanged;
            eventUnitEditor.DataChanged += eventUnitEditor_DataChanged;
        }

        private void eventUnitEditor_DataChanged( object sender, System.EventArgs e )
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[evt.Units];
            cm.Refresh();
        }

        private void unitSelectorComboBox_SelectedIndexChanged( object sender, System.EventArgs e )
        {
            eventUnitEditor.EventUnit = unitSelectorListBox.SelectedItem as EventUnit;
        }

        private void UpdateView()
        {
            eventUnitEditor.SuspendLayout();
            unitSelectorListBox.DataSource = evt.Units;
            unitSelectorListBox.SelectedIndex = 0;
            eventUnitEditor.EventUnit = unitSelectorListBox.SelectedItem as EventUnit;
            eventUnitEditor.ResumeLayout();
        }
    }
}
