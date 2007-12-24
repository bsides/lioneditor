using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class StatusesEditor : UserControl
    {
        public static List<string> FieldNames = new List<string>(new string[]{
            "NoEffect","Crystal","Dead","Undead","Charging","Jump","Defending","Performing",
            "Petrify","Invite","Darkness","Confusion","Silence","BloodSuck","DarkEvilLooking","Treasure",
            "Oil","Float","Reraise","Transparent","Berserk","Chicken","Frog","Critical",
            "Poison","Regen","Protect","Shell","Haste","Slow","Stop","Wall",
            "Faith","Innocent","Charm","Sleep","DontMove","DontAct","Reflect","DeathSentence"});

        private Statuses statuses;
        private bool ignoreChanges = false;

        public string Status { get { return statusGroupBox.Text; } set { statusGroupBox.Text = value; } }

        public Statuses Statuses
        {
            get { return statuses; }
            set
            {
                if (value == null)
                {
                    this.Enabled = false;
                    statuses = null;
                }
                else if (statuses != value)
                {
                    this.Enabled = true;
                    statuses = value;
                    UpdateView();
                }
            }
        }

        public StatusesEditor()
        {
            InitializeComponent();
            statusesCheckedListBox.ItemCheck += statusesCheckedListBox_ItemCheck;
        }

        private void statusesCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!ignoreChanges)
            {
                Utilities.SetFlag(statuses, FieldNames[e.Index], e.NewValue == CheckState.Checked);
            }
        }

        private void UpdateView()
        {
            this.SuspendLayout();
            statusesCheckedListBox.SuspendLayout();

            ignoreChanges = true;
            for (int i = 0; i < statusesCheckedListBox.Items.Count; i++)
            {
                statusesCheckedListBox.SetItemChecked(i, Utilities.GetFlag(statuses, FieldNames[i]));
            }
            ignoreChanges = false;
            statusesCheckedListBox.ResumeLayout();
            this.ResumeLayout();
        }
    }
}
