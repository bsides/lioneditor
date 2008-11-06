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
    partial class ArtefactsEditor
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
            this.artefactsListBox = new System.Windows.Forms.CheckedListBox();
            this.stupidDateEditor = new LionEditor.StupidDateEditor();
            this.SuspendLayout();
            // 
            // artefactsListBox
            // 
            this.artefactsListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.artefactsListBox.FormattingEnabled = true;
            this.artefactsListBox.Location = new System.Drawing.Point( 0, 0 );
            this.artefactsListBox.Name = "artefactsListBox";
            this.artefactsListBox.Size = new System.Drawing.Size( 180, 334 );
            this.artefactsListBox.TabIndex = 2;
            // 
            // stupidDateEditor
            // 
            this.stupidDateEditor.Location = new System.Drawing.Point( 186, 3 );
            this.stupidDateEditor.Name = "stupidDateEditor";
            this.stupidDateEditor.Size = new System.Drawing.Size( 183, 29 );
            this.stupidDateEditor.TabIndex = 3;
            // 
            // ArtefactsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.stupidDateEditor );
            this.Controls.Add( this.artefactsListBox );
            this.Name = "ArtefactsEditor";
            this.Size = new System.Drawing.Size( 517, 342 );
            this.ResumeLayout( false );

        }

        #endregion

        private StupidDateEditor stupidDateEditor;
        private System.Windows.Forms.CheckedListBox artefactsListBox;

    }
}
