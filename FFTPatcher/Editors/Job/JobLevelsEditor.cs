/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FFTPatcher.Controls;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class JobLevelsEditor : BaseEditor
    {
		#region Instance Variables (1) 

        private JobLevels levels;

		#endregion Instance Variables 

		#region Constructors (1) 

        public JobLevelsEditor()
        {
            InitializeComponent();
            requirementsEditor1.DataChanged += OnDataChanged;
        }

		#endregion Constructors 

		#region Public Methods (1) 

        public void UpdateView( JobLevels levels )
        {
            this.levels = levels;
            foreach( Control c in Controls )
            {
                if( c is NumericUpDownWithDefault )
                {
                    NumericUpDownWithDefault spinner = c as NumericUpDownWithDefault;
                    spinner.ValueChanged -= spinner_ValueChanged;
                    spinner.SetValueAndDefault(
                        ReflectionHelpers.GetFieldOrProperty<UInt16>( levels, spinner.Tag.ToString() ),
                        ReflectionHelpers.GetFieldOrProperty<UInt16>( levels.Default, spinner.Tag.ToString() ) );
                    spinner.ValueChanged += spinner_ValueChanged;
                }
            }

            List<Requirements> reqs = new List<Requirements>( new Requirements[] {
                levels.Chemist, levels.Knight, levels.Archer, levels.Monk,
                levels.WhiteMage, levels.BlackMage, levels.TimeMage, levels.Summoner,
                levels.Thief, levels.Orator, levels.Mystic, levels.Geomancer,
                levels.Dragoon, levels.Samurai, levels.Ninja, levels.Arithmetician,
                levels.Bard, levels.Dancer, levels.Mime } );
            if( FFTPatch.Context == Context.US_PSP )
            {
                reqs.Add( levels.DarkKnight );
                reqs.Add( levels.OnionKnight );
                reqs.Add( levels.Unknown );
            }
            darkKnightSideLabel.Visible = FFTPatch.Context == Context.US_PSP;
            darkKnightTopLabel.Visible = FFTPatch.Context == Context.US_PSP;
            unknown1TopLabel.Visible = FFTPatch.Context == Context.US_PSP;
            unknown2TopLabel.Visible = FFTPatch.Context == Context.US_PSP;
            unknownSideLabel.Visible = FFTPatch.Context == Context.US_PSP;
            onionKnightSideLabel.Visible = FFTPatch.Context == Context.US_PSP;
            onionKnightTopLabel.Visible = FFTPatch.Context == Context.US_PSP;

            requirementsEditor1.Requirements = reqs;
        }

		#endregion Public Methods 

		#region Private Methods (1) 

        private void spinner_ValueChanged( object sender, EventArgs e )
        {
            NumericUpDownWithDefault spinner = sender as NumericUpDownWithDefault;
            ReflectionHelpers.SetFieldOrProperty( levels, spinner.Tag.ToString(), (UInt16)spinner.Value );
            OnDataChanged( this, System.EventArgs.Empty );
        }

		#endregion Private Methods 
    }
}
