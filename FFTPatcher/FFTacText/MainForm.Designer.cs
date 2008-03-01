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
            this.atchelpMenuItem = new System.Windows.Forms.MenuItem();
            this.attackoutMenuItem = new System.Windows.Forms.MenuItem();
            this.joinlzwMenuItem = new System.Windows.Forms.MenuItem();
            this.openlzwMenuItem = new System.Windows.Forms.MenuItem();
            this.samplelzwMenuItem = new System.Windows.Forms.MenuItem();
            this.wldhelplzwMenuItem = new System.Windows.Forms.MenuItem();
            this.worldlzwMenuItem = new System.Windows.Forms.MenuItem();
            this.pspMenuItem = new System.Windows.Forms.MenuItem();
            this.pspatchelpMenuItem = new System.Windows.Forms.MenuItem();
            this.pspopenlzwMenuItem = new System.Windows.Forms.MenuItem();
            this.stringSectionedEditor1 = new FFTPatcher.TextEditor.StringSectionedEditor();
            this.compressedStringSectionedEditor1 = new FFTPatcher.TextEditor.CompressedStringSectionedEditor();
            this.psp299024MenuItem = new System.Windows.Forms.MenuItem();
            this.psp29E334MenuItem = new System.Windows.Forms.MenuItem();
            this.psp2A1630MenuItem = new System.Windows.Forms.MenuItem();
            this.psp2EB4C0MenuItem = new System.Windows.Forms.MenuItem();
            this.psp32D368MenuItem = new System.Windows.Forms.MenuItem();
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
            this.psxMenuItem.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.atchelpMenuItem,
            this.attackoutMenuItem,
            this.joinlzwMenuItem,
            this.openlzwMenuItem,
            this.samplelzwMenuItem,
            this.wldhelplzwMenuItem,
            this.worldlzwMenuItem} );
            this.psxMenuItem.Text = "PSX";
            // 
            // atchelpMenuItem
            // 
            this.atchelpMenuItem.Checked = true;
            this.atchelpMenuItem.Index = 0;
            this.atchelpMenuItem.Tag = "ATCHELP.LZW";
            this.atchelpMenuItem.Text = "ATCHELP.LZW";
            // 
            // attackoutMenuItem
            // 
            this.attackoutMenuItem.Index = 1;
            this.attackoutMenuItem.Tag = "ATTACK.OUT";
            this.attackoutMenuItem.Text = "ATTACK.OUT";
            // 
            // joinlzwMenuItem
            // 
            this.joinlzwMenuItem.Index = 2;
            this.joinlzwMenuItem.Tag = "JOIN.LZW";
            this.joinlzwMenuItem.Text = "JOIN.LZW";
            // 
            // openlzwMenuItem
            // 
            this.openlzwMenuItem.Index = 3;
            this.openlzwMenuItem.Tag = "OPEN.LZW";
            this.openlzwMenuItem.Text = "OPEN.LZW";
            // 
            // samplelzwMenuItem
            // 
            this.samplelzwMenuItem.Index = 4;
            this.samplelzwMenuItem.Tag = "SAMPLE.LZW";
            this.samplelzwMenuItem.Text = "SAMPLE.LZW";
            // 
            // wldhelplzwMenuItem
            // 
            this.wldhelplzwMenuItem.Index = 5;
            this.wldhelplzwMenuItem.Tag = "WLDHELP.LZW";
            this.wldhelplzwMenuItem.Text = "WLDHELP.LZW";
            // 
            // worldlzwMenuItem
            // 
            this.worldlzwMenuItem.Index = 6;
            this.worldlzwMenuItem.Tag = "WORLD.LZW";
            this.worldlzwMenuItem.Text = "WORLD.LZW";
            // 
            // pspMenuItem
            // 
            this.pspMenuItem.Index = 2;
            this.pspMenuItem.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.pspatchelpMenuItem,
            this.pspopenlzwMenuItem,
            this.psp299024MenuItem,
            this.psp29E334MenuItem,
            this.psp2A1630MenuItem,
            this.psp2EB4C0MenuItem,
            this.psp32D368MenuItem} );
            this.pspMenuItem.Text = "PSP";
            // 
            // pspatchelpMenuItem
            // 
            this.pspatchelpMenuItem.Index = 0;
            this.pspatchelpMenuItem.Tag = "pspATCHELP.LZW";
            this.pspatchelpMenuItem.Text = "ATCHELP.LZW";
            // 
            // pspopenlzwMenuItem
            // 
            this.pspopenlzwMenuItem.Index = 1;
            this.pspopenlzwMenuItem.Tag = "pspOPEN.LZW";
            this.pspopenlzwMenuItem.Text = "OPEN.LZW";
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
            // psp299024MenuItem
            // 
            this.psp299024MenuItem.Index = 2;
            this.psp299024MenuItem.Tag = "psp299024";
            this.psp299024MenuItem.Text = "BOOT.BIN[0x299024]";
            // 
            // psp29E334MenuItem
            // 
            this.psp29E334MenuItem.Index = 3;
            this.psp29E334MenuItem.Tag = "psp29E334";
            this.psp29E334MenuItem.Text = "BOOT.BIN[0x29E334]";
            // 
            // psp2A1630MenuItem
            // 
            this.psp2A1630MenuItem.Index = 4;
            this.psp2A1630MenuItem.Tag = "psp2A1630";
            this.psp2A1630MenuItem.Text = "BOOT.BIN[0x2A1630]";
            // 
            // psp2EB4C0MenuItem
            // 
            this.psp2EB4C0MenuItem.Index = 5;
            this.psp2EB4C0MenuItem.Tag = "psp2EB4C0";
            this.psp2EB4C0MenuItem.Text = "BOOT.BIN[0x2EB4C0]";
            // 
            // psp32D368MenuItem
            // 
            this.psp32D368MenuItem.Index = 6;
            this.psp32D368MenuItem.Tag = "psp32D368";
            this.psp32D368MenuItem.Text = "BOOT.BIN[0x32D368]";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 543, 416 );
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
        private System.Windows.Forms.MenuItem atchelpMenuItem;
        private System.Windows.Forms.MenuItem attackoutMenuItem;
        private System.Windows.Forms.MenuItem joinlzwMenuItem;
        private System.Windows.Forms.MenuItem openlzwMenuItem;
        private System.Windows.Forms.MenuItem samplelzwMenuItem;
        private System.Windows.Forms.MenuItem wldhelplzwMenuItem;
        private System.Windows.Forms.MenuItem worldlzwMenuItem;
        private System.Windows.Forms.MenuItem pspMenuItem;
        private StringSectionedEditor stringSectionedEditor1;
        private CompressedStringSectionedEditor compressedStringSectionedEditor1;
        private System.Windows.Forms.MenuItem pspatchelpMenuItem;
        private System.Windows.Forms.MenuItem pspopenlzwMenuItem;
        private System.Windows.Forms.MenuItem psp299024MenuItem;
        private System.Windows.Forms.MenuItem psp29E334MenuItem;
        private System.Windows.Forms.MenuItem psp2A1630MenuItem;
        private System.Windows.Forms.MenuItem psp2EB4C0MenuItem;
        private System.Windows.Forms.MenuItem psp32D368MenuItem;

    }
}

