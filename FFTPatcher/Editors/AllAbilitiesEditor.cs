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
    public partial class AllAbilitiesEditor : UserControl
    {
        public AllAbilitiesEditor()
        {
            InitializeComponent();
            abilityEditor.InflictStatusLabelClicked += abilityEditor_InflictStatusLabelClicked;
        }

        public event EventHandler<LabelClickedEventArgs> InflictStatusClicked;

        private void abilityEditor_InflictStatusLabelClicked( object sender, LabelClickedEventArgs e )
        {
            if( InflictStatusClicked != null )
            {
                InflictStatusClicked( this, e );
            }
        }

        public void UpdateView( AllAbilities allAbilities )
        {
            abilitiesListBox.SelectedIndexChanged -= abilitiesListBox_SelectedIndexChanged;
            abilitiesListBox.Items.Clear();
            abilitiesListBox.Items.AddRange( allAbilities.Abilities );
            abilitiesListBox.SelectedIndexChanged += abilitiesListBox_SelectedIndexChanged;
            abilitiesListBox.SelectedIndex = 0;
            abilityEditor.Ability = abilitiesListBox.SelectedItem as Ability;
        }

        private void abilitiesListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            Ability a = abilitiesListBox.SelectedItem as Ability;
            abilityEditor.Ability = a;
        }
    }
}
