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
            this.lengthLabel = new System.Windows.Forms.Label();
            this.maxLengthLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.filesListBox = new System.Windows.Forms.ListBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.stringListEditor1 = new FFTPatcher.TextEditor.StringListEditor();
            this.SuspendLayout();
            //
            // sectionComboBox
            //
            this.sectionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sectionComboBox.FormattingEnabled = true;
            this.sectionComboBox.Location = new System.Drawing.Point( 0, 0 );
            this.sectionComboBox.Name = "sectionComboBox";
            this.sectionComboBox.Size = new System.Drawing.Size( 335, 21 );
            this.sectionComboBox.TabIndex = 0;
            //
            // lengthLabel
            //
            this.lengthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Location = new System.Drawing.Point( 3, 231 );
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size( 35, 13 );
            this.lengthLabel.TabIndex = 3;
            this.lengthLabel.Text = "label1";
            //
            // maxLengthLabel
            //
            this.maxLengthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxLengthLabel.AutoSize = true;
            this.maxLengthLabel.Location = new System.Drawing.Point( 3, 261 );
            this.maxLengthLabel.Name = "maxLengthLabel";
            this.maxLengthLabel.Size = new System.Drawing.Size( 35, 13 );
            this.maxLengthLabel.TabIndex = 4;
            this.maxLengthLabel.Text = "label2";
            //
            // errorLabel
            //
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.SystemColors.Window;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point( 341, 5 );
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size( 46, 13 );
            this.errorLabel.TabIndex = 5;
            this.errorLabel.Text = "ERROR";
            //
            // filesListBox
            //
            this.filesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.filesListBox.FormattingEnabled = true;
            this.filesListBox.Location = new System.Drawing.Point( 199, 231 );
            this.filesListBox.MultiColumn = true;
            this.filesListBox.Name = "filesListBox";
            this.filesListBox.Size = new System.Drawing.Size( 188, 82 );
            this.filesListBox.TabIndex = 6;
            //
            // saveButton
            //
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point( 199, 331 );
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size( 188, 23 );
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save to selected file";
            this.saveButton.UseVisualStyleBackColor = true;
            //
            // stringListEditor1
            //
            this.stringListEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stringListEditor1.Location = new System.Drawing.Point( 3, 27 );
            this.stringListEditor1.Name = "stringListEditor1";
            this.stringListEditor1.Size = new System.Drawing.Size( 384, 198 );
            this.stringListEditor1.TabIndex = 8;
            //
            // StringSectionedEditor
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.sectionComboBox );
            this.Controls.Add( this.errorLabel );
            this.Controls.Add( this.saveButton );
            this.Controls.Add( this.filesListBox );
            this.Controls.Add( this.lengthLabel );
            this.Controls.Add( this.maxLengthLabel );
            this.Controls.Add( this.stringListEditor1 );
            this.Name = "StringSectionedEditor";
            this.Size = new System.Drawing.Size( 390, 357 );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox sectionComboBox;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Label maxLengthLabel;
        /// <summary>
        /// Label for showing &quot;ERROR&quot;
        /// </summary>
        protected System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.ListBox filesListBox;
        private System.Windows.Forms.Button saveButton;
        private StringListEditor stringListEditor1;
    }
}
