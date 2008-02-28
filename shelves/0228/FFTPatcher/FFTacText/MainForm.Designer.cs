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
    partial class MainForm
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
            this.stringSectionedEditor1 = new FFTPatcher.TextEditor.StringSectionedEditor();
            this.SuspendLayout();
            // 
            // stringSectionedEditor1
            // 
            this.stringSectionedEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stringSectionedEditor1.Location = new System.Drawing.Point( 0, 0 );
            this.stringSectionedEditor1.Name = "stringSectionedEditor1";
            this.stringSectionedEditor1.Size = new System.Drawing.Size( 459, 349 );
            this.stringSectionedEditor1.Strings = null;
            this.stringSectionedEditor1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 459, 349 );
            this.Controls.Add( this.stringSectionedEditor1 );
            this.Name = "MainForm";
            this.Text = "FFTacText Editor";
            this.ResumeLayout( false );

        }

        #endregion

        private StringSectionedEditor stringSectionedEditor1;
    }
}

