namespace FFTPatcher.Editors
{
    partial class StatusesEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusGroupBox = new System.Windows.Forms.GroupBox();
            this.statusesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.statusGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusGroupBox
            // 
            this.statusGroupBox.AutoSize = true;
            this.statusGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statusGroupBox.Controls.Add(this.statusesCheckedListBox);
            this.statusGroupBox.Location = new System.Drawing.Point(3, 3);
            this.statusGroupBox.Name = "statusGroupBox";
            this.statusGroupBox.Size = new System.Drawing.Size(501, 192);
            this.statusGroupBox.TabIndex = 0;
            this.statusGroupBox.TabStop = false;
            this.statusGroupBox.Text = "Status";
            // 
            // statusesCheckedListBox
            // 
            this.statusesCheckedListBox.FormattingEnabled = true;
            this.statusesCheckedListBox.Items.AddRange(new object[] {
            "",
            "Crystal",
            "Dead",
            "Undead",
            "Charging",
            "Jump",
            "Defending",
            "Performing",
            "Petrify",
            "Invite",
            "Darkness",
            "Confusion",
            "Silence",
            "Blood Suck",
            "Dark/Evil Looking",
            "Treasure",
            "Oil",
            "Float",
            "Reraise",
            "Transparent",
            "Berserk",
            "Chicken",
            "Frog",
            "Critical",
            "Poison",
            "Regen",
            "Protect",
            "Shell",
            "Haste",
            "Slow",
            "Stop",
            "Wall",
            "Faith",
            "Innocent",
            "Charm",
            "Sleep",
            "Don\'t Move",
            "Don\'t Act",
            "Reflect",
            "Death Sentence"});
            this.statusesCheckedListBox.Location = new System.Drawing.Point(6, 19);
            this.statusesCheckedListBox.MultiColumn = true;
            this.statusesCheckedListBox.Name = "statusesCheckedListBox";
            this.statusesCheckedListBox.Size = new System.Drawing.Size(489, 154);
            this.statusesCheckedListBox.TabIndex = 0;
            // 
            // StatusesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.statusGroupBox);
            this.Name = "StatusesEditor";
            this.Size = new System.Drawing.Size(507, 198);
            this.statusGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox statusGroupBox;
        private System.Windows.Forms.CheckedListBox statusesCheckedListBox;
    }
}
