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
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MainMenu( this.components );
            this.fileMenuItem = new System.Windows.Forms.MenuItem();
            this.psxMenuItem = new System.Windows.Forms.MenuItem();
            this.pspMenuItem = new System.Windows.Forms.MenuItem();
            this.stringSectionedEditor1 = new FFTPatcher.TextEditor.StringSectionedEditor();
            this.compressedStringSectionedEditor1 = new FFTPatcher.TextEditor.CompressedStringSectionedEditor();
            this.partitionEditor1 = new FFTPatcher.TextEditor.Editors.PartitionEditor();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.psxMenuItem,
            this.pspMenuItem} );
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.Index = 0;
            this.fileMenuItem.Text = "File";
            // 
            // psxMenuItem
            // 
            this.psxMenuItem.Index = 1;
            this.psxMenuItem.Text = "PSX";
            //
            // pspMenuItem
            //
            this.pspMenuItem.Index = 2;
            this.pspMenuItem.Text = "PSP";
            // 
            // stringSectionedEditor1
            // 
            this.stringSectionedEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stringSectionedEditor1.Location = new System.Drawing.Point( 0, 0 );
            this.stringSectionedEditor1.Name = "stringSectionedEditor1";
            this.stringSectionedEditor1.Size = new System.Drawing.Size( 543, 416 );
            this.stringSectionedEditor1.Strings = null;
            this.stringSectionedEditor1.TabIndex = 0;
            // 
            // compressedStringSectionedEditor1
            // 
            this.compressedStringSectionedEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compressedStringSectionedEditor1.Location = new System.Drawing.Point( 0, 0 );
            this.compressedStringSectionedEditor1.Name = "compressedStringSectionedEditor1";
            this.compressedStringSectionedEditor1.Size = new System.Drawing.Size( 543, 416 );
            this.compressedStringSectionedEditor1.Strings = null;
            this.compressedStringSectionedEditor1.TabIndex = 1;
            // 
            // partitionEditor1
            // 
            this.partitionEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.partitionEditor1.Location = new System.Drawing.Point( 0, 0 );
            this.partitionEditor1.Name = "partitionEditor1";
            this.partitionEditor1.Size = new System.Drawing.Size( 543, 416 );
            this.partitionEditor1.Strings = null;
            this.partitionEditor1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 543, 416 );
            this.Controls.Add( this.partitionEditor1 );
            this.Controls.Add( this.stringSectionedEditor1 );
            this.Controls.Add( this.compressedStringSectionedEditor1 );
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "FFTacText Editor";
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem fileMenuItem;
        private System.Windows.Forms.MenuItem psxMenuItem;
        private System.Windows.Forms.MenuItem pspMenuItem;
        private StringSectionedEditor stringSectionedEditor1;
        private CompressedStringSectionedEditor compressedStringSectionedEditor1;
        private FFTPatcher.TextEditor.Editors.PartitionEditor partitionEditor1;

    }
}

