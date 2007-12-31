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
    public partial class AllSkillSetsEditor : UserControl
    {
        public int SelectedIndex { get { return skillSetListBox.SelectedIndex; } set { skillSetListBox.SelectedIndex = value; } }

        public AllSkillSetsEditor()
        {
            InitializeComponent();
        }

        public void UpdateView( AllSkillSets skills )
        {
            skillSetListBox.SelectedIndexChanged -= skillSetListBox_SelectedIndexChanged;
            skillSetListBox.Items.Clear();
            skillSetListBox.Items.AddRange( skills.SkillSets );
            skillSetListBox.SelectedIndexChanged += skillSetListBox_SelectedIndexChanged;
            skillSetListBox.SelectedIndex = 0;
            skillSetEditor.SkillSet = skillSetListBox.SelectedItem as SkillSet;
        }

        private void skillSetListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            SkillSet s = skillSetListBox.SelectedItem as SkillSet;
            skillSetEditor.SkillSet = s;
        }
    }
}
