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
            this.SuspendLayout();
            // 
            // sectionComboBox
            // 
            this.sectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sectionComboBox.FormattingEnabled = true;
            this.sectionComboBox.Location = new System.Drawing.Point( 3, 3 );
            this.sectionComboBox.Name = "sectionComboBox";
            this.sectionComboBox.Size = new System.Drawing.Size( 118, 21 );
            this.sectionComboBox.TabIndex = 0;
            // 
            // currentStringListBox
            // 
            this.currentStringListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.currentStringListBox.FormattingEnabled = true;
            this.currentStringListBox.Location = new System.Drawing.Point( 3, 30 );
            this.currentStringListBox.Name = "currentStringListBox";
            this.currentStringListBox.Size = new System.Drawing.Size( 120, 290 );
            this.currentStringListBox.TabIndex = 1;
            // 
            // currentString
            // 
            this.currentString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.currentString.Location = new System.Drawing.Point( 129, 3 );
            this.currentString.Multiline = true;
            this.currentString.Name = "currentString";
            this.currentString.Size = new System.Drawing.Size( 303, 338 );
            this.currentString.TabIndex = 2;
            // 
            // lengthLabel
            // 
            this.lengthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Location = new System.Drawing.Point( 435, 6 );
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size( 35, 13 );
            this.lengthLabel.TabIndex = 3;
            this.lengthLabel.Text = "label1";
            // 
            // maxLengthLabel
            // 
            this.maxLengthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.maxLengthLabel.AutoSize = true;
            this.maxLengthLabel.Location = new System.Drawing.Point( 435, 32 );
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
            this.errorLabel.Location = new System.Drawing.Point( 376, 322 );
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size( 46, 13 );
            this.errorLabel.TabIndex = 5;
            this.errorLabel.Text = "ERROR";
            // 
            // StringSectionedEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.errorLabel );
            this.Controls.Add( this.maxLengthLabel );
            this.Controls.Add( this.lengthLabel );
            this.Controls.Add( this.currentString );
            this.Controls.Add( this.currentStringListBox );
            this.Controls.Add( this.sectionComboBox );
            this.Name = "StringSectionedEditor";
            this.Size = new System.Drawing.Size( 556, 344 );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox sectionComboBox;
        private System.Windows.Forms.ListBox currentStringListBox;
        private System.Windows.Forms.TextBox currentString;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Label maxLengthLabel;
        private System.Windows.Forms.Label errorLabel;
    }
}
