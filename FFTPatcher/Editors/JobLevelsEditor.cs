using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using FFTPatcher.Properties;

namespace FFTPatcher.Editors
{
    public partial class JobLevelsEditor : UserControl
    {
        private bool ignoreChanges = false;

        public JobLevels JobLevels { get; set; }

        public JobLevelsEditor()
        {
            InitializeComponent();
            JobLevels = new JobLevels( new SubArray<byte>( new List<byte>( Resources.JobLevelsBin ), 0 ) );

            foreach( Control c in Controls )
            {
                if( c is NumericUpDown )
                {
                    NumericUpDown spinner = c as NumericUpDown;
                    spinner.Value = Utilities.GetFieldOrProperty<UInt16>( JobLevels, spinner.Tag.ToString() );
                    spinner.ValueChanged += spinner_ValueChanged;
                }
            }
            UpdateView();
        }

        private void spinner_ValueChanged( object sender, EventArgs e )
        {
            NumericUpDown spinner = sender as NumericUpDown;
            Utilities.SetFieldOrProperty( JobLevels, spinner.Tag.ToString(), (UInt16)spinner.Value );
        }

        private void UpdateView()
        {
            ignoreChanges = true;

            requirementsEditor1.Requirements = new List<Requirements>( new Requirements[] {
                JobLevels.Chemist, JobLevels.Knight, JobLevels.Archer, JobLevels.Monk,
                JobLevels.WhiteMage, JobLevels.BlackMage, JobLevels.TimeMage, JobLevels.Summoner,
                JobLevels.Thief, JobLevels.Orator, JobLevels.Mystic, JobLevels.Geomancer,
                JobLevels.Dragoon, JobLevels.Samurai, JobLevels.Ninja, JobLevels.Arithmetician,
                JobLevels.Bard, JobLevels.Dancer, JobLevels.Mime, JobLevels.DarkKnight,
                JobLevels.OnionKnight, JobLevels.Unknown } );

            ignoreChanges = false;
        }

    }
}
