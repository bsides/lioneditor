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

using System;
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using FFTPatcher.Controls;

namespace FFTPatcher.Editors
{
    public partial class StatusAttributeEditor : UserControl
    {

        private static readonly string[] PropertyNames = new string[] {
            "FreezeCT", "Unknown1", "Unknown2", "Unknown3", "Unknown4", "Unknown5", "Unknown6", "KO",
            "CanReact", "Blank", "IgnoreAttack", "Unknown7", "Unknown8", "Unknown9", "Unknown10", "Unknown11" };

        private StatusAttribute statusAttribute;
        private bool ignoreChanges = false;
        private NumericUpDownWithDefault[] spinners;

        public StatusAttribute StatusAttribute
        {
            get { return statusAttribute; }
            set
            {
                if( value == null )
                {
                    statusAttribute = null;
                    this.Enabled = false;
                }
                else if( statusAttribute != value )
                {
                    statusAttribute = value;
                    this.Enabled = true;
                    UpdateView();
                }
            }
        }

        private void UpdateView()
        {
            this.ignoreChanges = true;
            
            foreach( NumericUpDownWithDefault spinner in spinners )
            {
                spinner.SetValueAndDefault(
                    Utilities.GetFieldOrProperty<byte>( statusAttribute, spinner.Tag.ToString() ),
                    Utilities.GetFieldOrProperty<byte>( statusAttribute.Default, spinner.Tag.ToString() ) );
            }

            if( statusAttribute.Default != null )
            {
                checkedListBox.Defaults = statusAttribute.Default.ToBoolArray();
            }

            for( int i = 0; i < checkedListBox.Items.Count; i++ )
            {
                checkedListBox.SetItemChecked( i, Utilities.GetFieldOrProperty<bool>( statusAttribute, PropertyNames[i] ) );
            }

            cantStackStatusesEditor.Statuses = statusAttribute.CantStackOn;
            cancelStatusesEditor.Statuses = statusAttribute.Cancels;

            this.ignoreChanges = false;
        }


        public StatusAttributeEditor()
        {
            InitializeComponent();
            spinners = new NumericUpDownWithDefault[4] { unknown1Spinner, unknown2Spinner, orderSpinner, ctSpinner };
            foreach( NumericUpDownWithDefault spinner in spinners )
            {
                spinner.ValueChanged += spinner_ValueChanged;
            }
            checkedListBox.ItemCheck += checkedListBox_ItemCheck;
        }

        private void checkedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                Utilities.SetFieldOrProperty( statusAttribute, PropertyNames[e.Index], e.NewValue == CheckState.Checked );
            }
        }

        private void spinner_ValueChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                NumericUpDownWithDefault spinner = sender as NumericUpDownWithDefault;
                Utilities.SetFieldOrProperty( statusAttribute, spinner.Tag.ToString(), (byte)spinner.Value );
            }
        }
    }
}
