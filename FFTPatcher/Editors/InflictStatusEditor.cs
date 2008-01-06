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
    public partial class InflictStatusEditor : UserControl
    {
        private readonly string[] flags = new string[] { 
            "AllOrNothing", "Random", "Separate", "Cancel", 
            "Blank1", "Blank2", "Blank3", "Blank4" };
        private bool ignoreChanges = false;
        private InflictStatus status;
        public InflictStatus InflictStatus
        {
            get { return status; }
            set
            {
                if( value == null )
                {
                    status = null;
                    this.Enabled = false;
                }
                else if( value != status )
                {
                    status = value;
                    this.Enabled = true;
                    UpdateView();
                }
            }
        }

        public InflictStatusEditor()
        {
            InitializeComponent();
            flagsCheckedListBox.ItemCheck += flagsCheckedListBox_ItemCheck;
        }

        private void flagsCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                Utilities.SetFlag( status, flags[e.Index], e.NewValue == CheckState.Checked );
            }
        }

        private void UpdateView()
        {
            ignoreChanges = true;
            SuspendLayout();
            flagsCheckedListBox.SuspendLayout();
            inflictStatusesEditor.SuspendLayout();

            if( status.Default != null )
            {
                flagsCheckedListBox.SetValuesAndDefaults( Utilities.GetFieldsOrProperties<bool>( status, flags ), status.Default.ToBoolArray() );
            }

            inflictStatusesEditor.Statuses = status.Statuses;

            inflictStatusesEditor.ResumeLayout();
            flagsCheckedListBox.ResumeLayout();
            ResumeLayout();
            ignoreChanges = false;
        }

    }
}
