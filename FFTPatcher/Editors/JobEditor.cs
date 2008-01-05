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
    public partial class JobEditor : UserControl
    {
        private Job job;
        private bool ignoreChanges;
        private NumericUpDownWithDefault[] spinners;
        private ComboBoxWithDefault[] comboBoxes;

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
            spinners = new NumericUpDownWithDefault[] {
                hpGrowthSpinner, hpMultiplierSpinner, mpGrowthSpinner, mpMultiplierSpinner,
                speedGrowthSpinner, speedMultiplierSpinner, paGrowthSpinner, paMultiplierSpinner,
                maGrowthSpinner, maMultiplierSpinner, moveSpinner, jumpSpinner,
                cevSpinner, mPortraitSpinner, mPaletteSpinner, mGraphicSpinner };
            comboBoxes = new ComboBoxWithDefault[] {
                skillsetComboBox, innateAComboBox, innateBComboBox, innateCComboBox, innateDComboBox };

            foreach( NumericUpDownWithDefault spinner in spinners )
            {
                spinner.ValueChanged += spinner_ValueChanged;
            }
            foreach( ComboBoxWithDefault comboBox in comboBoxes )
            {
                comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            }

            skillSetLabel.TabStop = false;
            skillSetLabel.Click += skillSetLabel_Click;
        }

        public event EventHandler<LabelClickedEventArgs> SkillSetClicked;
        private void skillSetLabel_Click( object sender, EventArgs e )
        {
            if( SkillSetClicked != null )
            {
                SkillSetClicked( this, new LabelClickedEventArgs( (byte)skillsetComboBox.SelectedIndex ) );
            }
        }

        private void comboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                ComboBoxWithDefault c = sender as ComboBoxWithDefault;
                Utilities.SetFieldOrProperty( job, c.Tag.ToString(), c.SelectedItem );
            }
        }

        private void spinner_ValueChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                NumericUpDownWithDefault spinner = sender as NumericUpDownWithDefault;
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
                foreach( ComboBoxWithDefault cb in new ComboBoxWithDefault[] { innateAComboBox, innateBComboBox, innateCComboBox, innateDComboBox } )
                {
                    cb.Items.Clear();
                    cb.Items.AddRange( AllAbilities.DummyAbilities );
                }
            }

            skillsetComboBox.SetValueAndDefault( job.SkillSet, job.Default.SkillSet );
            foreach( NumericUpDownWithDefault s in spinners )
            {
                // TODO Update Default
                s.SetValueAndDefault(
                    Utilities.GetFieldOrProperty<byte>( job, s.Tag.ToString() ),
                    Utilities.GetFieldOrProperty<byte>( job.Default, s.Tag.ToString() ) );
            }
            innateAComboBox.SetValueAndDefault( job.InnateA, job.Default.InnateA );
            innateBComboBox.SetValueAndDefault( job.InnateB, job.Default.InnateB );
            innateCComboBox.SetValueAndDefault( job.InnateC, job.Default.InnateC );
            innateDComboBox.SetValueAndDefault( job.InnateD, job.Default.InnateD );
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
