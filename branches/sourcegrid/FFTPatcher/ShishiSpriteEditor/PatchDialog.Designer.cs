﻿namespace FFTPatcher.SpriteEditor
{
    partial class PatchDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button checkAllButton;
            System.Windows.Forms.Button uncheckButton;
            System.Windows.Forms.Button toggleButton;
            System.Windows.Forms.Button cancelButton;
            System.Windows.Forms.Label isoLabel;
            System.Windows.Forms.Button isoBrowseButton;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( PatchDialog ) );
            this.listView1 = new FFTPatcher.SpriteEditor.FlickerFreeListView();
            this.okButton = new System.Windows.Forms.Button();
            this.isoPathTextBox = new System.Windows.Forms.TextBox();
            this.fileSaveDialog = new System.Windows.Forms.SaveFileDialog();
            checkAllButton = new System.Windows.Forms.Button();
            uncheckButton = new System.Windows.Forms.Button();
            toggleButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            isoLabel = new System.Windows.Forms.Label();
            isoBrowseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkAllButton
            // 
            checkAllButton.Location = new System.Drawing.Point( 12, 367 );
            checkAllButton.Name = "checkAllButton";
            checkAllButton.Size = new System.Drawing.Size( 75, 23 );
            checkAllButton.TabIndex = 1;
            checkAllButton.Text = "Check All";
            checkAllButton.UseVisualStyleBackColor = true;
            checkAllButton.Click += new System.EventHandler( this.checkAllButton_Click );
            // 
            // uncheckButton
            // 
            uncheckButton.Location = new System.Drawing.Point( 13, 397 );
            uncheckButton.Name = "uncheckButton";
            uncheckButton.Size = new System.Drawing.Size( 75, 23 );
            uncheckButton.TabIndex = 2;
            uncheckButton.Text = "Uncheck All";
            uncheckButton.UseVisualStyleBackColor = true;
            uncheckButton.Click += new System.EventHandler( this.uncheckButton_Click );
            // 
            // toggleButton
            // 
            toggleButton.Location = new System.Drawing.Point( 13, 427 );
            toggleButton.Name = "toggleButton";
            toggleButton.Size = new System.Drawing.Size( 75, 23 );
            toggleButton.TabIndex = 3;
            toggleButton.Text = "Toggle All";
            toggleButton.UseVisualStyleBackColor = true;
            toggleButton.Click += new System.EventHandler( this.toggleButton_Click );
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point( 358, 461 );
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size( 75, 23 );
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // isoLabel
            // 
            isoLabel.AutoSize = true;
            isoLabel.Location = new System.Drawing.Point( 128, 383 );
            isoLabel.Name = "isoLabel";
            isoLabel.Size = new System.Drawing.Size( 25, 13 );
            isoLabel.TabIndex = 16;
            isoLabel.Text = "ISO";
            // 
            // isoBrowseButton
            // 
            isoBrowseButton.AutoSize = true;
            isoBrowseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            isoBrowseButton.Location = new System.Drawing.Point( 407, 397 );
            isoBrowseButton.Name = "isoBrowseButton";
            isoBrowseButton.Size = new System.Drawing.Size( 26, 23 );
            isoBrowseButton.TabIndex = 15;
            isoBrowseButton.Text = "...";
            isoBrowseButton.UseVisualStyleBackColor = true;
            isoBrowseButton.Click += new System.EventHandler( this.isoBrowseButton_Click );
            // 
            // listView1
            // 
            this.listView1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.listView1.CheckBoxes = true;
            this.listView1.Location = new System.Drawing.Point( 12, 12 );
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size( 421, 348 );
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point( 277, 461 );
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size( 75, 23 );
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // isoPathTextBox
            // 
            this.isoPathTextBox.Location = new System.Drawing.Point( 128, 399 );
            this.isoPathTextBox.Name = "isoPathTextBox";
            this.isoPathTextBox.ReadOnly = true;
            this.isoPathTextBox.Size = new System.Drawing.Size( 273, 20 );
            this.isoPathTextBox.TabIndex = 14;
            // 
            // fileSaveDialog
            // 
            this.fileSaveDialog.Filter = "ISO files (*.iso; *.bin; *.img)|*.iso;*.bin;*.img";
            this.fileSaveDialog.OverwritePrompt = false;
            // 
            // PatchDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = cancelButton;
            this.ClientSize = new System.Drawing.Size( 445, 496 );
            this.Controls.Add( isoLabel );
            this.Controls.Add( isoBrowseButton );
            this.Controls.Add( this.isoPathTextBox );
            this.Controls.Add( this.okButton );
            this.Controls.Add( cancelButton );
            this.Controls.Add( toggleButton );
            this.Controls.Add( uncheckButton );
            this.Controls.Add( checkAllButton );
            this.Controls.Add( this.listView1 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.Name = "PatchDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Select sprites";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private FFTPatcher.SpriteEditor.FlickerFreeListView listView1;
        private System.Windows.Forms.TextBox isoPathTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.SaveFileDialog fileSaveDialog;
    }
}
