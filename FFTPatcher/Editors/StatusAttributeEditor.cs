using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class StatusAttributeEditor : UserControl
    {

        private static readonly string[] PropertyNames = new string[] {
            "FreezeCT", "Unknown1", "Unknown2", "Unknown3", "Unknown4", "Unknown5", "Unknown6", "KO",
            "CanReact", "Blank", "IgnoreAttack", "Unknown7", "Unknown8", "Unknown9", "Unknown10", "Unknown11" };

        private StatusAttribute statusAttribute;
        private bool ignoreChanges = false;
        private NumericUpDown[] spinners;

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

            foreach( NumericUpDown spinner in spinners )
            {
                spinner.Value = Utilities.GetFieldOrProperty<byte>( statusAttribute, spinner.Tag.ToString() );
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
            spinners = new NumericUpDown[4] { unknown1Spinner, unknown2Spinner, orderSpinner, ctSpinner };
            foreach( NumericUpDown spinner in spinners )
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
                NumericUpDown spinner = sender as NumericUpDown;
                Utilities.SetFieldOrProperty( statusAttribute, spinner.Tag.ToString(), (byte)spinner.Value );
            }
        }
    }
}
