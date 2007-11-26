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
    partial class About
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
            System.Windows.Forms.Button closeButton;
            this.textBoxNoCaret1 = new LionEditor.TextBoxNoCaret();
            closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            closeButton.Location = new System.Drawing.Point( 289, 242 );
            closeButton.Name = "closeButton";
            closeButton.Size = new System.Drawing.Size( 75, 23 );
            closeButton.TabIndex = 1;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            // 
            // textBoxNoCaret1
            // 
            this.textBoxNoCaret1.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBoxNoCaret1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxNoCaret1.Location = new System.Drawing.Point( 0, 0 );
            this.textBoxNoCaret1.Multiline = true;
            this.textBoxNoCaret1.Name = "textBoxNoCaret1";
            this.textBoxNoCaret1.ReadOnly = true;
            this.textBoxNoCaret1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNoCaret1.ShortcutsEnabled = false;
            this.textBoxNoCaret1.Size = new System.Drawing.Size( 380, 229 );
            this.textBoxNoCaret1.TabIndex = 0;
            this.textBoxNoCaret1.TabStop = false;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 380, 277 );
            this.Controls.Add( closeButton );
            this.Controls.Add( this.textBoxNoCaret1 );
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size( 388, 304 );
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size( 388, 304 );
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private TextBoxNoCaret textBoxNoCaret1;




    }
}