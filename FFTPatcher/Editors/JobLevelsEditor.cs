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
using System.Collections.Generic;
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
