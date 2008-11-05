/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of LionEditor.

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

namespace LionEditor
{
    partial class JobsAndAbilitiesEditor
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
            System.Windows.Forms.SplitContainer splitContainer;
            this.jobSelector = new System.Windows.Forms.ListBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.jobEditor = new LionEditor.JobEditor();
            splitContainer = new System.Windows.Forms.SplitContainer();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.IsSplitterFixed = true;
            splitContainer.Location = new System.Drawing.Point( 0, 0 );
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add( this.jobSelector );
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add( this.closeButton );
            splitContainer.Panel2.Controls.Add( this.jobEditor );
            splitContainer.Size = new System.Drawing.Size( 384, 436 );
            splitContainer.SplitterDistance = 109;
            splitContainer.SplitterWidth = 1;
            splitContainer.TabIndex = 0;
            splitContainer.TabStop = false;
            // 
            // jobSelector
            // 
            this.jobSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobSelector.FormattingEnabled = true;
            this.jobSelector.Location = new System.Drawing.Point( 0, 0 );
            this.jobSelector.Name = "jobSelector";
            this.jobSelector.Size = new System.Drawing.Size( 109, 433 );
            this.jobSelector.TabIndex = 100;
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point( 187, 401 );
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size( 75, 23 );
            this.closeButton.TabIndex = 300;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // jobEditor
            // 
            this.jobEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobEditor.Info = null;
            this.jobEditor.Job = null;
            this.jobEditor.Location = new System.Drawing.Point( 0, 0 );
            this.jobEditor.Name = "jobEditor";
            this.jobEditor.Size = new System.Drawing.Size( 274, 436 );
            this.jobEditor.TabIndex = 200;
            // 
            // JobsAndAbilitiesEditor
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size( 384, 436 );
            this.Controls.Add( splitContainer );
            this.Location = new System.Drawing.Point( 392, 463 );
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size( 392, 463 );
            this.MinimizeBox = false;
            this.Name = "JobsAndAbilitiesEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            splitContainer.Panel1.ResumeLayout( false );
            splitContainer.Panel2.ResumeLayout( false );
            splitContainer.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ListBox jobSelector;
        private JobEditor jobEditor;
        private System.Windows.Forms.Button closeButton;
    }
}