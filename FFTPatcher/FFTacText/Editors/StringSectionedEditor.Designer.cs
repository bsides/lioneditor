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

namespace FFTPatcher.TextEditor
{
    partial class StringSectionedEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sectionComboBox = new System.Windows.Forms.ComboBox();
            this.currentStringListBox = new System.Windows.Forms.ListBox();
            this.currentString = new System.Windows.Forms.TextBox();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.maxLengthLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.filesListBox = new System.Windows.Forms.ListBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // sectionComboBox
            // 
            this.sectionComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.sectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sectionComboBox.FormattingEnabled = true;
            this.sectionComboBox.Location = new System.Drawing.Point( 0, 0 );
            this.sectionComboBox.Name = "sectionComboBox";
            this.sectionComboBox.Size = new System.Drawing.Size( 131, 21 );
            this.sectionComboBox.TabIndex = 0;
            // 
            // currentStringListBox
            // 
            this.currentStringListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.currentStringListBox.FormattingEnabled = true;
            this.currentStringListBox.Location = new System.Drawing.Point( 3, 27 );
            this.currentStringListBox.Name = "currentStringListBox";
            this.currentStringListBox.Size = new System.Drawing.Size( 126, 368 );
            this.currentStringListBox.TabIndex = 1;
            // 
            // currentString
            // 
            this.currentString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.currentString.Location = new System.Drawing.Point( 3, 3 );
            this.currentString.Multiline = true;
            this.currentString.Name = "currentString";
            this.currentString.Size = new System.Drawing.Size( 334, 275 );
            this.currentString.TabIndex = 2;
            // 
            // lengthLabel
            // 
            this.lengthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Location = new System.Drawing.Point( 2, 287 );
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size( 35, 13 );
            this.lengthLabel.TabIndex = 3;
            this.lengthLabel.Text = "label1";
            // 
            // maxLengthLabel
            // 
            this.maxLengthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxLengthLabel.AutoSize = true;
            this.maxLengthLabel.Location = new System.Drawing.Point( 2, 313 );
            this.maxLengthLabel.Name = "maxLengthLabel";
            this.maxLengthLabel.Size = new System.Drawing.Size( 35, 13 );
            this.maxLengthLabel.TabIndex = 4;
            this.maxLengthLabel.Text = "label2";
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.SystemColors.Window;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point( 286, 255 );
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size( 46, 13 );
            this.errorLabel.TabIndex = 5;
            this.errorLabel.Text = "ERROR";
            // 
            // filesListBox
            // 
            this.filesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.filesListBox.FormattingEnabled = true;
            this.filesListBox.Location = new System.Drawing.Point( 149, 284 );
            this.filesListBox.MultiColumn = true;
            this.filesListBox.Name = "filesListBox";
            this.filesListBox.Size = new System.Drawing.Size( 188, 82 );
            this.filesListBox.TabIndex = 6;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point( 149, 373 );
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size( 188, 23 );
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save to selected file";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point( 0, 0 );
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add( this.sectionComboBox );
            this.splitContainer.Panel1.Controls.Add( this.currentStringListBox );
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add( this.errorLabel );
            this.splitContainer.Panel2.Controls.Add( this.saveButton );
            this.splitContainer.Panel2.Controls.Add( this.currentString );
            this.splitContainer.Panel2.Controls.Add( this.filesListBox );
            this.splitContainer.Panel2.Controls.Add( this.lengthLabel );
            this.splitContainer.Panel2.Controls.Add( this.maxLengthLabel );
            this.splitContainer.Size = new System.Drawing.Size( 475, 410 );
            this.splitContainer.SplitterDistance = 131;
            this.splitContainer.TabIndex = 8;
            // 
            // StringSectionedEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.splitContainer );
            this.Name = "StringSectionedEditor";
            this.Size = new System.Drawing.Size( 475, 410 );
            this.splitContainer.Panel1.ResumeLayout( false );
            this.splitContainer.Panel2.ResumeLayout( false );
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ComboBox sectionComboBox;
        private System.Windows.Forms.ListBox currentStringListBox;
        private System.Windows.Forms.TextBox currentString;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Label maxLengthLabel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.ListBox filesListBox;
        private System.Windows.Forms.Button saveButton;
        protected System.Windows.Forms.SplitContainer splitContainer;
    }
}
