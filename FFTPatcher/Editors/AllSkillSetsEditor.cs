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
    public partial class AllSkillSetsEditor : UserControl
    {
        public AllSkillSets AllSkillSets { get; set; }

        public AllSkillSetsEditor()
        {
            InitializeComponent();
            AllSkillSets = new AllSkillSets(new SubArray<byte>(new List<byte>(Resources.SkillSetsBin), 0));
            skillSetListBox.Items.AddRange(AllSkillSets.SkillSets.ToArray());
            skillSetListBox.SelectedIndexChanged += skillSetListBox_SelectedIndexChanged;
            skillSetListBox.SelectedIndex = 0;
        }

        private void skillSetListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SkillSet s = skillSetListBox.SelectedItem as SkillSet;
            skillSetEditor.SkillSet = s;
        }
    }
}
