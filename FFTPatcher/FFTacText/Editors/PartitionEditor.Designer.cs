namespace FFTPatcher.TextEditor.Editors
{
    partial class PartitionEditor
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
            this.currentStringListBox = new System.Windows.Forms.ListBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.currentString = new System.Windows.Forms.TextBox();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.maxLengthLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.saveAllButton = new System.Windows.Forms.Button();
            this.filesListBox = new System.Windows.Forms.ListBox();
            this.saveThisButton = new System.Windows.Forms.Button();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentStringListBox
            // 
            this.currentStringListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentStringListBox.FormattingEnabled = true;
            this.currentStringListBox.Location = new System.Drawing.Point( 0, 0 );
            this.currentStringListBox.Name = "currentStringListBox";
            this.currentStringListBox.Size = new System.Drawing.Size( 149, 407 );
            this.currentStringListBox.TabIndex = 7;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point( 0, 0 );
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add( this.currentStringListBox );
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add( this.saveThisButton );
            this.splitContainer.Panel2.Controls.Add( this.currentString );
            this.splitContainer.Panel2.Controls.Add( this.filesListBox );
            this.splitContainer.Panel2.Controls.Add( this.lengthLabel );
            this.splitContainer.Panel2.Controls.Add( this.saveAllButton );
            this.splitContainer.Panel2.Controls.Add( this.maxLengthLabel );
            this.splitContainer.Size = new System.Drawing.Size( 509, 415 );
            this.splitContainer.SplitterDistance = 149;
            this.splitContainer.TabIndex = 15;
            // 
            // currentString
            // 
            this.currentString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.currentString.Location = new System.Drawing.Point( 4, 3 );
            this.currentString.Multiline = true;
            this.currentString.Name = "currentString";
            this.currentString.Size = new System.Drawing.Size( 352, 280 );
            this.currentString.TabIndex = 8;
            // 
            // lengthLabel
            // 
            this.lengthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Location = new System.Drawing.Point( 3, 292 );
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size( 35, 13 );
            this.lengthLabel.TabIndex = 9;
            this.lengthLabel.Text = "label1";
            // 
            // maxLengthLabel
            // 
            this.maxLengthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxLengthLabel.AutoSize = true;
            this.maxLengthLabel.Location = new System.Drawing.Point( 3, 318 );
            this.maxLengthLabel.Name = "maxLengthLabel";
            this.maxLengthLabel.Size = new System.Drawing.Size( 35, 13 );
            this.maxLengthLabel.TabIndex = 10;
            this.maxLengthLabel.Text = "label2";
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.SystemColors.Window;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point( 449, 260 );
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size( 46, 13 );
            this.errorLabel.TabIndex = 11;
            this.errorLabel.Text = "ERROR";
            // 
            // saveAllButton
            // 
            this.saveAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveAllButton.Enabled = false;
            this.saveAllButton.Location = new System.Drawing.Point( 163, 354 );
            this.saveAllButton.Name = "saveAllButton";
            this.saveAllButton.Size = new System.Drawing.Size( 188, 23 );
            this.saveAllButton.TabIndex = 12;
            this.saveAllButton.Text = "Save all sections to selected file";
            this.saveAllButton.UseVisualStyleBackColor = true;
            // 
            // filesListBox
            // 
            this.filesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.filesListBox.FormattingEnabled = true;
            this.filesListBox.Location = new System.Drawing.Point( 163, 292 );
            this.filesListBox.MultiColumn = true;
            this.filesListBox.Name = "filesListBox";
            this.filesListBox.Size = new System.Drawing.Size( 188, 56 );
            this.filesListBox.TabIndex = 13;
            // 
            // saveThisButton
            // 
            this.saveThisButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveThisButton.Enabled = false;
            this.saveThisButton.Location = new System.Drawing.Point( 163, 381 );
            this.saveThisButton.Name = "saveThisButton";
            this.saveThisButton.Size = new System.Drawing.Size( 188, 23 );
            this.saveThisButton.TabIndex = 14;
            this.saveThisButton.Text = "Save this section to selected file";
            this.saveThisButton.UseVisualStyleBackColor = true;
            // 
            // PartitionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.errorLabel );
            this.Controls.Add( this.splitContainer );
            this.Name = "PartitionEditor";
            this.Size = new System.Drawing.Size( 509, 415 );
            this.splitContainer.Panel1.ResumeLayout( false );
            this.splitContainer.Panel2.ResumeLayout( false );
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout( false );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox currentStringListBox;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button saveThisButton;
        private System.Windows.Forms.TextBox currentString;
        private System.Windows.Forms.ListBox filesListBox;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Button saveAllButton;
        private System.Windows.Forms.Label maxLengthLabel;
        private System.Windows.Forms.Label errorLabel;

    }
}
