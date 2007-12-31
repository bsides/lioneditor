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
    public partial class AllJobsEditor : UserControl
    {
        public AllJobsEditor()
        {
            InitializeComponent();
            jobEditor.SkillSetClicked += jobEditor_SkillSetClicked;
        }

        public event EventHandler<LabelClickedEventArgs> SkillSetClicked;
        private void jobEditor_SkillSetClicked( object sender, LabelClickedEventArgs e )
        {
            if( SkillSetClicked != null )
            {
                SkillSetClicked( this, e );
            }
        }

        public void UpdateView( AllJobs jobs )
        {
            jobsListBox.SelectedIndexChanged -= jobsListBox_SelectedIndexChanged;
            jobsListBox.Items.Clear();
            jobsListBox.Items.AddRange( jobs.Jobs );
            jobsListBox.SelectedIndexChanged += jobsListBox_SelectedIndexChanged;
            jobsListBox.SelectedIndex = 0;
            jobEditor.Job = jobsListBox.SelectedItem as Job;
        }

        private void jobsListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            Job j = jobsListBox.SelectedItem as Job;
            jobEditor.Job = j;
        }
    }
}
