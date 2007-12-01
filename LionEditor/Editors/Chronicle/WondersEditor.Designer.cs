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
            this.wondersListBox = new System.Windows.Forms.CheckedListBox();
            this.stupidDateEditor = new LionEditor.StupidDateEditor();
            this.SuspendLayout();
            // 
            // wondersListBox
            // 
            this.wondersListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.wondersListBox.FormattingEnabled = true;
            this.wondersListBox.Location = new System.Drawing.Point( 0, 0 );
            this.wondersListBox.Name = "wondersListBox";
            this.wondersListBox.Size = new System.Drawing.Size( 117, 334 );
            this.wondersListBox.TabIndex = 0;
            // 
            // stupidDateEditor
            // 
            this.stupidDateEditor.Location = new System.Drawing.Point( 127, 4 );
            this.stupidDateEditor.Name = "stupidDateEditor";
            this.stupidDateEditor.Size = new System.Drawing.Size( 183, 29 );
            this.stupidDateEditor.TabIndex = 1;
            // 
            // WondersEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.stupidDateEditor );
            this.Controls.Add( this.wondersListBox );
            this.Name = "WondersEditor";
            this.Size = new System.Drawing.Size( 512, 343 );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.CheckedListBox wondersListBox;
        private StupidDateEditor stupidDateEditor;

    }
}
