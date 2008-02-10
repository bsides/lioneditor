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

namespace FFTPatcher.SpriteEditor
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
            System.Windows.Forms.MenuItem fileMenu;
            System.Windows.Forms.MenuItem paletteMenu;
            System.Windows.Forms.MenuItem separator1;
            System.Windows.Forms.MenuItem separator2;
            System.Windows.Forms.MenuItem separator3;
            System.Windows.Forms.GroupBox paletteGroupBox;
            this.spriteViewer1 = new FFTPatcher.SpriteEditor.SpriteViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainMenu = new System.Windows.Forms.MainMenu( this.components );
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.openMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.MenuItem();
            this.importMenuItem = new System.Windows.Forms.MenuItem();
            this.exportMenuItem = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.paletteSaveMenuItem = new System.Windows.Forms.MenuItem();
            this.paletteOpenMenuItem = new System.Windows.Forms.MenuItem();
            this.paletteSelector = new System.Windows.Forms.ComboBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            fileMenu = new System.Windows.Forms.MenuItem();
            paletteMenu = new System.Windows.Forms.MenuItem();
            separator1 = new System.Windows.Forms.MenuItem();
            separator2 = new System.Windows.Forms.MenuItem();
            separator3 = new System.Windows.Forms.MenuItem();
            paletteGroupBox = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            paletteGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // spriteViewer1
            // 
            this.spriteViewer1.Location = new System.Drawing.Point( 3, 3 );
            this.spriteViewer1.Name = "spriteViewer1";
            this.spriteViewer1.Palette = 0;
            this.spriteViewer1.Size = new System.Drawing.Size( 256, 488 );
            this.spriteViewer1.Sprite = null;
            this.spriteViewer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add( this.spriteViewer1 );
            this.panel1.Location = new System.Drawing.Point( 10, 3 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 266, 498 );
            this.panel1.TabIndex = 1;
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            fileMenu,
            paletteMenu,
            this.aboutMenuItem} );
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 2;
            this.aboutMenuItem.Text = "About...";
            // 
            // fileMenu
            // 
            fileMenu.Index = 0;
            fileMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.openMenuItem,
            separator1,
            this.saveMenuItem,
            this.saveAsMenuItem,
            separator2,
            this.importMenuItem,
            this.exportMenuItem,
            separator3,
            this.exitMenuItem} );
            fileMenu.Text = "&File";
            // 
            // paletteMenu
            // 
            paletteMenu.Index = 1;
            paletteMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.paletteSaveMenuItem,
            this.paletteOpenMenuItem} );
            paletteMenu.Text = "&Palette";
            // 
            // openMenuItem
            // 
            this.openMenuItem.Index = 0;
            this.openMenuItem.Text = "&Open...";
            // 
            // separator1
            // 
            separator1.Index = 1;
            separator1.Text = "-";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Enabled = false;
            this.saveMenuItem.Index = 2;
            this.saveMenuItem.Text = "&Save";
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Enabled = false;
            this.saveAsMenuItem.Index = 3;
            this.saveAsMenuItem.Text = "Save &As...";
            // 
            // separator2
            // 
            separator2.Index = 4;
            separator2.Text = "-";
            // 
            // importMenuItem
            // 
            this.importMenuItem.Enabled = false;
            this.importMenuItem.Index = 5;
            this.importMenuItem.Text = "&Import...";
            // 
            // exportMenuItem
            // 
            this.exportMenuItem.Enabled = false;
            this.exportMenuItem.Index = 6;
            this.exportMenuItem.Text = "&Export...";
            // 
            // separator3
            // 
            separator3.Index = 7;
            separator3.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 8;
            this.exitMenuItem.Text = "E&xit";
            // 
            // paletteSaveMenuItem
            // 
            this.paletteSaveMenuItem.Enabled = false;
            this.paletteSaveMenuItem.Index = 0;
            this.paletteSaveMenuItem.Text = "&Save...";
            // 
            // paletteOpenMenuItem
            // 
            this.paletteOpenMenuItem.Enabled = false;
            this.paletteOpenMenuItem.Index = 1;
            this.paletteOpenMenuItem.Text = "&Open...";
            // 
            // paletteGroupBox
            // 
            paletteGroupBox.AutoSize = true;
            paletteGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            paletteGroupBox.Controls.Add( this.paletteSelector );
            paletteGroupBox.Location = new System.Drawing.Point( 282, 3 );
            paletteGroupBox.Name = "paletteGroupBox";
            paletteGroupBox.Size = new System.Drawing.Size( 185, 59 );
            paletteGroupBox.TabIndex = 2;
            paletteGroupBox.TabStop = false;
            paletteGroupBox.Text = "Palette";
            // 
            // paletteSelector
            // 
            this.paletteSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteSelector.Enabled = false;
            this.paletteSelector.FormattingEnabled = true;
            this.paletteSelector.Items.AddRange( new object[] {
            "Sprite #1",
            "Sprite #2",
            "Sprite #3",
            "Sprite #4",
            "Sprite #5",
            "Sprite #6",
            "Sprite #7",
            "Sprite #8",
            "Portrait #1",
            "Portrait #2",
            "Portrait #3",
            "Portrait #4",
            "Portrait #5",
            "Portrait #6",
            "Portrait #7",
            "Portrait #8"} );
            this.paletteSelector.Location = new System.Drawing.Point( 6, 19 );
            this.paletteSelector.Name = "paletteSelector";
            this.paletteSelector.Size = new System.Drawing.Size( 173, 21 );
            this.paletteSelector.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Sprite files (*.SPR)|*.SPR|Secondary sprite files (*.SP2)|*.SP2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 480, 512 );
            this.Controls.Add( paletteGroupBox );
            this.Controls.Add( this.panel1 );
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size( 488, 560 );
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size( 488, 560 );
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Shishi Sprite Manager";
            this.panel1.ResumeLayout( false );
            paletteGroupBox.ResumeLayout( false );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private SpriteViewer spriteViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem openMenuItem;
        private System.Windows.Forms.MenuItem saveMenuItem;
        private System.Windows.Forms.MenuItem saveAsMenuItem;
        private System.Windows.Forms.MenuItem importMenuItem;
        private System.Windows.Forms.MenuItem exportMenuItem;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem paletteSaveMenuItem;
        private System.Windows.Forms.MenuItem paletteOpenMenuItem;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.ComboBox paletteSelector;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

