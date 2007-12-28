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
    public partial class ItemAttributeEditor : UserControl
    {
        private bool ignoreChanges = false;
        private NumericUpDown[] spinners;
        private ItemAttributes attributes;
        public ItemAttributes ItemAttributes
        {
            get { return attributes; }
            set
            {
                if( value == null )
                {
                    this.Enabled = false;
                    this.attributes = null;
                }
                else if( value != attributes )
                {
                    attributes = value;
                    this.Enabled = true;
                    UpdateView();
                }
            }
        }

        private void UpdateView()
        {
            this.ignoreChanges = true;
            SuspendLayout();
            statusImmunityEditor.SuspendLayout();
            startingStatusesEditor.SuspendLayout();
            permanentStatusesEditor.SuspendLayout();
            strongElementsEditor.SuspendLayout();
            weakElementsEditor.SuspendLayout();
            halfElementsEditor.SuspendLayout();
            absorbElementsEditor.SuspendLayout();
            cancelElementsEditor.SuspendLayout();

            foreach( NumericUpDown spinner in spinners )
            {
                spinner.Value = Utilities.GetFieldOrProperty<byte>( attributes, spinner.Tag.ToString() );
            }
            statusImmunityEditor.Statuses = attributes.StatusImmunity;
            startingStatusesEditor.Statuses = attributes.StartingStatuses;
            permanentStatusesEditor.Statuses = attributes.PermanentStatuses;
            strongElementsEditor.Elements = attributes.Strong;
            weakElementsEditor.Elements = attributes.Weak;
            halfElementsEditor.Elements = attributes.Half;
            absorbElementsEditor.Elements = attributes.Absorb;
            cancelElementsEditor.Elements = attributes.Cancel;

            cancelElementsEditor.ResumeLayout();
            absorbElementsEditor.ResumeLayout();
            halfElementsEditor.ResumeLayout();
            weakElementsEditor.ResumeLayout();
            strongElementsEditor.ResumeLayout();
            permanentStatusesEditor.ResumeLayout();
            startingStatusesEditor.ResumeLayout();
            statusImmunityEditor.ResumeLayout();
            ResumeLayout();
            this.ignoreChanges = false;
        }

        public ItemAttributeEditor()
        {
            InitializeComponent();
            spinners = new NumericUpDown[] { maSpinner, paSpinner, speedSpinner, moveSpinner, jumpSpinner };
            foreach( NumericUpDown spinner in spinners )
            {
                spinner.ValueChanged += spinner_ValueChanged;
            }
        }

        private void spinner_ValueChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                NumericUpDown spinner = sender as NumericUpDown;
                Utilities.SetFieldOrProperty( attributes, spinner.Tag.ToString(), (byte)spinner.Value );
            }
        }
    }
}
