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
    partial class WondersEditor
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.stupidDateEditor1 = new LionEditor.Editors.Chronicle.StupidDateEditor();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point( 0, 0 );
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size( 117, 334 );
            this.checkedListBox1.TabIndex = 0;
            // 
            // stupidDateEditor1
            // 
            this.stupidDateEditor1.Location = new System.Drawing.Point( 127, 4 );
            this.stupidDateEditor1.Name = "stupidDateEditor1";
            this.stupidDateEditor1.Size = new System.Drawing.Size( 183, 29 );
            this.stupidDateEditor1.TabIndex = 1;
            // 
            // WondersEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.stupidDateEditor1 );
            this.Controls.Add( this.checkedListBox1 );
            this.Name = "WondersEditor";
            this.Size = new System.Drawing.Size( 512, 343 );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private LionEditor.Editors.Chronicle.StupidDateEditor stupidDateEditor1;

    }
}
