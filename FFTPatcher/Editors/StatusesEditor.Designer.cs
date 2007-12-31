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
            this.statusesCheckedListBox = new FFTPatcher.Controls.CheckedListBoxNoHighlight();
            this.statusGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusGroupBox
            // 
            this.statusGroupBox.Controls.Add( this.statusesCheckedListBox );
            this.statusGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusGroupBox.Location = new System.Drawing.Point( 0, 0 );
            this.statusGroupBox.Name = "statusGroupBox";
            this.statusGroupBox.Size = new System.Drawing.Size( 513, 186 );
            this.statusGroupBox.TabIndex = 0;
            this.statusGroupBox.TabStop = false;
            this.statusGroupBox.Text = "Status";
            // 
            // statusesCheckedListBox
            // 
            this.statusesCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusesCheckedListBox.FormattingEnabled = true;
            this.statusesCheckedListBox.Items.AddRange( new object[] {
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
            "Death Sentence"} );
            this.statusesCheckedListBox.Location = new System.Drawing.Point( 3, 16 );
            this.statusesCheckedListBox.MultiColumn = true;
            this.statusesCheckedListBox.Name = "statusesCheckedListBox";
            this.statusesCheckedListBox.Size = new System.Drawing.Size( 507, 154 );
            this.statusesCheckedListBox.TabIndex = 0;
            // 
            // StatusesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.statusGroupBox );
            this.Name = "StatusesEditor";
            this.Size = new System.Drawing.Size( 513, 186 );
            this.statusGroupBox.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.GroupBox statusGroupBox;
        private FFTPatcher.Controls.CheckedListBoxNoHighlight statusesCheckedListBox;
    }
}
