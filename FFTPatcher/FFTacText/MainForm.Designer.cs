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
            System.Windows.Forms.MenuItem separator1;
            System.Windows.Forms.MenuItem separator2;
            this.mainMenu = new System.Windows.Forms.MainMenu( this.components );
            this.fileMenuItem = new System.Windows.Forms.MenuItem();
            this.newPsxMenuItem = new System.Windows.Forms.MenuItem();
            this.newPspMenuItem = new System.Windows.Forms.MenuItem();
            this.openMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.psxMenuItem = new System.Windows.Forms.MenuItem();
            this.pspMenuItem = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.stringSectionedEditor = new FFTPatcher.TextEditor.StringSectionedEditor();
            this.compressedStringSectionedEditor = new FFTPatcher.TextEditor.CompressedStringSectionedEditor();
            this.partitionEditor = new FFTPatcher.TextEditor.Editors.PartitionEditor();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            separator1 = new System.Windows.Forms.MenuItem();
            separator2 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // separator1
            // 
            separator1.Index = 2;
            separator1.Text = "-";
            // 
            // separator2
            // 
            separator2.Index = 5;
            separator2.Text = "-";
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.psxMenuItem,
            this.pspMenuItem,
            this.aboutMenuItem} );
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.Index = 0;
            this.fileMenuItem.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.newPsxMenuItem,
            this.newPspMenuItem,
            separator1,
            this.openMenuItem,
            this.saveMenuItem,
            separator2,
            this.exitMenuItem} );
            this.fileMenuItem.Text = "File";
            // 
            // newPsxMenuItem
            // 
            this.newPsxMenuItem.Index = 0;
            this.newPsxMenuItem.Text = "New PS&X text";
            // 
            // newPspMenuItem
            // 
            this.newPspMenuItem.Index = 1;
            this.newPspMenuItem.Text = "New PS&P text";
            // 
            // openMenuItem
            // 
            this.openMenuItem.Index = 3;
            this.openMenuItem.Text = "&Open .ffttext...";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Enabled = false;
            this.saveMenuItem.Index = 4;
            this.saveMenuItem.Text = "&Save .ffttext...";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 6;
            this.exitMenuItem.Text = "E&xit";
            // 
            // psxMenuItem
            // 
            this.psxMenuItem.Index = 1;
            this.psxMenuItem.Text = "PSX";
            this.psxMenuItem.Visible = false;
            // 
            // pspMenuItem
            // 
            this.pspMenuItem.Index = 2;
            this.pspMenuItem.Text = "PSP";
            this.pspMenuItem.Visible = false;
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 3;
            this.aboutMenuItem.Text = "About...";
            // 
            // stringSectionedEditor
            // 
            this.stringSectionedEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stringSectionedEditor.Location = new System.Drawing.Point( 0, 0 );
            this.stringSectionedEditor.Name = "stringSectionedEditor";
            this.stringSectionedEditor.Size = new System.Drawing.Size( 543, 416 );
            this.stringSectionedEditor.Strings = null;
            this.stringSectionedEditor.TabIndex = 0;
            // 
            // compressedStringSectionedEditor
            // 
            this.compressedStringSectionedEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compressedStringSectionedEditor.Location = new System.Drawing.Point( 0, 0 );
            this.compressedStringSectionedEditor.Name = "compressedStringSectionedEditor";
            this.compressedStringSectionedEditor.Size = new System.Drawing.Size( 543, 416 );
            this.compressedStringSectionedEditor.Strings = null;
            this.compressedStringSectionedEditor.TabIndex = 1;
            // 
            // partitionEditor
            // 
            this.partitionEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.partitionEditor.Location = new System.Drawing.Point( 0, 0 );
            this.partitionEditor.Name = "partitionEditor";
            this.partitionEditor.Size = new System.Drawing.Size( 543, 416 );
            this.partitionEditor.Strings = null;
            this.partitionEditor.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 543, 416 );
            this.Controls.Add( this.partitionEditor );
            this.Controls.Add( this.stringSectionedEditor );
            this.Controls.Add( this.compressedStringSectionedEditor );
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
        private StringSectionedEditor stringSectionedEditor;
        private CompressedStringSectionedEditor compressedStringSectionedEditor;
        private FFTPatcher.TextEditor.Editors.PartitionEditor partitionEditor;
        private System.Windows.Forms.MenuItem newPsxMenuItem;
        private System.Windows.Forms.MenuItem newPspMenuItem;
        private System.Windows.Forms.MenuItem openMenuItem;
        private System.Windows.Forms.MenuItem saveMenuItem;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;

    }
}

