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
    partial class CompressedStringSectionedEditor
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
            this.compressButton = new System.Windows.Forms.Button();
            this.compressionProgressBar = new System.Windows.Forms.ProgressBar();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add( this.compressButton );
            this.splitContainer.Panel2.Controls.Add( this.compressionProgressBar );
            // 
            // compressButton
            // 
            this.compressButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.compressButton.Location = new System.Drawing.Point( 5, 329 );
            this.compressButton.MaximumSize = new System.Drawing.Size( 95, 47 );
            this.compressButton.MinimumSize = new System.Drawing.Size( 95, 47 );
            this.compressButton.Name = "compressButton";
            this.compressButton.Size = new System.Drawing.Size( 95, 47 );
            this.compressButton.TabIndex = 6;
            this.compressButton.Text = "Get Compressed Size";
            this.compressButton.UseVisualStyleBackColor = true;
            // 
            // compressionProgressBar
            // 
            this.compressionProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.compressionProgressBar.Location = new System.Drawing.Point( 5, 382 );
            this.compressionProgressBar.MaximumSize = new System.Drawing.Size( 95, 23 );
            this.compressionProgressBar.MinimumSize = new System.Drawing.Size( 95, 23 );
            this.compressionProgressBar.Name = "compressionProgressBar";
            this.compressionProgressBar.Size = new System.Drawing.Size( 95, 23 );
            this.compressionProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.compressionProgressBar.TabIndex = 7;
            this.compressionProgressBar.Visible = false;
            // 
            // CompressedStringSectionedEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CompressedStringSectionedEditor";
            this.splitContainer.Panel2.ResumeLayout( false );
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Button compressButton;
        private System.Windows.Forms.ProgressBar compressionProgressBar;
    }
}
