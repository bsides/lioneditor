using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FFTPatcher.Datatypes.Job;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class JobEditor : UserControl
    {
        private Job job;
        private bool ignoreChanges;
        private List<NumericUpDown> spinners;
        private List<ComboBox> comboBoxes;

        public Job Job
        {
            get { return job; }
            set
            {
                if (value == null)
                {
                    this.Enabled = false;
                    job = null;
                }
                else if (job != value)
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
            spinners = new List<NumericUpDown>(new NumericUpDown[] {
                hpGrowthSpinner, hpMultiplierSpinner, mpGrowthSpinner, mpMultiplierSpinner,
                speedGrowthSpinner, speedMultiplierSpinner, paGrowthSpinner, paMultiplierSpinner,
                maGrowthSpinner, maMultiplierSpinner, moveSpinner, jumpSpinner,
                cevSpinner, mPortraitSpinner, mPaletteSpinner, mGraphicSpinner});
            comboBoxes = new List<ComboBox>(new ComboBox[] {
                skillsetComboBox, innateAComboBox, innateBComboBox, innateCComboBox, innateDComboBox});

            skillsetComboBox.DataSource = SkillSet.DummySkillSets;
            innateAComboBox.BindingContext = new BindingContext();
            innateAComboBox.DataSource = AllAbilities.DummyAbilities;
            innateBComboBox.BindingContext = new BindingContext();
            innateBComboBox.DataSource = AllAbilities.DummyAbilities;
            innateCComboBox.BindingContext = new BindingContext();
            innateCComboBox.DataSource = AllAbilities.DummyAbilities;
            innateDComboBox.BindingContext = new BindingContext();
            innateDComboBox.DataSource = AllAbilities.DummyAbilities;

            foreach (NumericUpDown spinner in spinners)
            {
                spinner.ValueChanged += spinner_ValueChanged;
            }
            foreach (ComboBox comboBox in comboBoxes)
            {
                comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ignoreChanges)
            {
                ComboBox c = sender as ComboBox;
                Utilities.SetFieldOrProperty(job, c.Tag.ToString(), c.SelectedItem);
            }
        }
        
        private void spinner_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreChanges)
            {
                NumericUpDown spinner = sender as NumericUpDown;
                Utilities.SetFieldOrProperty(job, spinner.Tag.ToString(), (byte)spinner.Value);
            }
        }

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

            skillsetComboBox.SelectedItem = job.SkillSet;
            foreach (NumericUpDown s in spinners)
            {
                s.Value = Utilities.GetFieldOrProperty<byte>(job, s.Tag.ToString());
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
