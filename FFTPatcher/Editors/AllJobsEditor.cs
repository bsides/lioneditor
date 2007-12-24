using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using FFTPatcher.Properties;

namespace FFTPatcher.Editors
{
    public partial class AllJobsEditor : UserControl
    {
        public AllJobs AllJobs { get; set; }

        public AllJobsEditor()
        {
            InitializeComponent();
            AllJobs = new AllJobs( new SubArray<byte>( new List<byte>( Resources.JobsBin ), 0 ) );
            jobsListBox.Items.AddRange( AllJobs.Jobs );
            jobsListBox.SelectedIndexChanged += jobsListBox_SelectedIndexChanged;
            jobsListBox.SelectedIndex = 0;
        }

        private void jobsListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            Job j = jobsListBox.SelectedItem as Job;
            jobEditor.Job = j;
        }
    }
}
