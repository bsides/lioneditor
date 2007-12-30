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

namespace FFTPatcher.Editors
{
    public partial class JobEditor : UserControl
    {
        private Job job;
        private bool ignoreChanges;
        private NumericUpDown[] spinners;
        private ComboBox[] comboBoxes;

        public Job Job
        {
            get { return job; }
            set
            {
                if( value == null )
                {
                    this.Enabled = false;
                    job = null;
                }
                else if( job != value )
                {
                    job = value;
                    this.Enabled = true;
                    UpdateView();
                }
            }
        }


        public JobEditor()
        {
            InitializeComponent();
            spinners = new NumericUpDown[] {
                hpGrowthSpinner, hpMultiplierSpinner, mpGrowthSpinner, mpMultiplierSpinner,
                speedGrowthSpinner, speedMultiplierSpinner, paGrowthSpinner, paMultiplierSpinner,
                maGrowthSpinner, maMultiplierSpinner, moveSpinner, jumpSpinner,
                cevSpinner, mPortraitSpinner, mPaletteSpinner, mGraphicSpinner };
            comboBoxes = new ComboBox[] {
                skillsetComboBox, innateAComboBox, innateBComboBox, innateCComboBox, innateDComboBox };

            foreach( NumericUpDown spinner in spinners )
            {
                spinner.ValueChanged += spinner_ValueChanged;
            }
            foreach( ComboBox comboBox in comboBoxes )
            {
                comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            }
        }

        private void comboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                ComboBox c = sender as ComboBox;
                Utilities.SetFieldOrProperty( job, c.Tag.ToString(), c.SelectedItem );
            }
        }

        private void spinner_ValueChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                NumericUpDown spinner = sender as NumericUpDown;
                Utilities.SetFieldOrProperty( job, spinner.Tag.ToString(), (byte)spinner.Value );
            }
        }
        private Context ourContext = Context.Default;
        private void UpdateView()
        {
            ignoreChanges = true;
            this.SuspendLayout();
            absorbElementsEditor.SuspendLayout();
            cancelElementsEditor.SuspendLayout();
            halfElementsEditor.SuspendLayout();
            weakElementsEditor.SuspendLayout();
            equipmentEditor.SuspendLayout();
            innateStatusesEditor.SuspendLayout();
            statusImmunityEditor.SuspendLayout();
            startingStatusesEditor.SuspendLayout();

            if( ourContext != FFTPatch.Context )
            {
                ourContext = FFTPatch.Context;
                skillsetComboBox.Items.Clear();
                skillsetComboBox.Items.AddRange( SkillSet.DummySkillSets );
                foreach( ComboBox cb in new ComboBox[] { innateAComboBox, innateBComboBox, innateCComboBox, innateDComboBox } )
                {
                    cb.Items.Clear();
                    cb.Items.AddRange( AllAbilities.DummyAbilities );
                }
            }

            skillsetComboBox.SelectedItem = job.SkillSet;
            foreach( NumericUpDown s in spinners )
            {
                s.Value = Utilities.GetFieldOrProperty<byte>( job, s.Tag.ToString() );
            }
            innateAComboBox.SelectedItem = job.InnateA;
            innateBComboBox.SelectedItem = job.InnateB;
            innateCComboBox.SelectedItem = job.InnateC;
            innateDComboBox.SelectedItem = job.InnateD;
            absorbElementsEditor.Elements = job.AbsorbElement;
            halfElementsEditor.Elements = job.HalfElement;
            cancelElementsEditor.Elements = job.CancelElement;
            weakElementsEditor.Elements = job.WeakElement;
            equipmentEditor.Equipment = job.Equipment;
            innateStatusesEditor.Statuses = job.PermanentStatus;
            statusImmunityEditor.Statuses = job.StatusImmunity;
            startingStatusesEditor.Statuses = job.StartingStatus;

            ignoreChanges = false;
            absorbElementsEditor.ResumeLayout();
            cancelElementsEditor.ResumeLayout();
            halfElementsEditor.ResumeLayout();
            weakElementsEditor.ResumeLayout();
            equipmentEditor.ResumeLayout();
            innateStatusesEditor.ResumeLayout();
            statusImmunityEditor.ResumeLayout();
            startingStatusesEditor.ResumeLayout();
            this.ResumeLayout();
        }

    }
}
