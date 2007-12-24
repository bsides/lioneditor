using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FFTPatcher.Datatypes.Job;
using FFTPatcher.Properties;

namespace FFTPatcher.Editors
{
    public partial class AllJobsEditor : UserControl
    {
        public AllJobs AllJobs { get; set; }

        public AllJobsEditor()
        {
            InitializeComponent();
            AllJobs = new AllJobs(new SubArray<byte>(new List<byte>(Resources.JobsBin), 0));
            jobsListBox.Items.AddRange(AllJobs.Jobs.ToArray());
            jobsListBox.SelectedIndexChanged += jobsListBox_SelectedIndexChanged;
            jobsListBox.SelectedIndex = 0;
        }

        private void jobsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Job j = jobsListBox.SelectedItem as Job;
            jobEditor.Job = j;
        }
    }
}
