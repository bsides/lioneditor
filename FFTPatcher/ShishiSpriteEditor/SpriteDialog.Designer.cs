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
    partial class SpriteDialog
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
            System.Windows.Forms.MenuItem palettesMenu;
            System.Windows.Forms.GroupBox paletteGroupBox;
            System.Windows.Forms.MainMenu mainMenu;
            System.Windows.Forms.MenuItem sprMenu;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( SpriteDialog ) );
            this.importPaletteMenuItem = new System.Windows.Forms.MenuItem();
            this.exportPaletteMenuItem = new System.Windows.Forms.MenuItem();
            this.portraitCheckbox = new System.Windows.Forms.CheckBox();
            this.paletteSelector = new System.Windows.Forms.ComboBox();
            this.fileMenu = new System.Windows.Forms.MenuItem();
            this.importBmpMenuItem = new System.Windows.Forms.MenuItem();
            this.exportBmpMenuItem = new System.Windows.Forms.MenuItem();
            this.closeMenuItem = new System.Windows.Forms.MenuItem();
            this.importSprMenuItem = new System.Windows.Forms.MenuItem();
            this.exportSprMenuItem = new System.Windows.Forms.MenuItem();
            this.sp2Menu = new System.Windows.Forms.MenuItem();
            this.importSp2MenuItem = new System.Windows.Forms.MenuItem();
            this.exportSp2MenuItem = new System.Windows.Forms.MenuItem();
            this.sp2Separator1 = new System.Windows.Forms.MenuItem();
            this.importSp2bMenuItem = new System.Windows.Forms.MenuItem();
            this.exportSp2bMenuItem = new System.Windows.Forms.MenuItem();
            this.sp2Separator2 = new System.Windows.Forms.MenuItem();
            this.importSp2cMenuItem = new System.Windows.Forms.MenuItem();
            this.exportSp2cMenuItem = new System.Windows.Forms.MenuItem();
            this.sp2Separator3 = new System.Windows.Forms.MenuItem();
            this.importSp2dMenuItem = new System.Windows.Forms.MenuItem();
            this.exportSp2dMenuItem = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.spriteViewer1 = new FFTPatcher.SpriteEditor.SpriteViewer();
            this.shapesListBox = new System.Windows.Forms.ListBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            separator1 = new System.Windows.Forms.MenuItem();
            palettesMenu = new System.Windows.Forms.MenuItem();
            paletteGroupBox = new System.Windows.Forms.GroupBox();
            mainMenu = new System.Windows.Forms.MainMenu( this.components );
            sprMenu = new System.Windows.Forms.MenuItem();
            paletteGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // separator1
            // 
            separator1.Index = 2;
            separator1.Text = "-";
            // 
            // palettesMenu
            // 
            palettesMenu.Index = 3;
            palettesMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.importPaletteMenuItem,
            this.exportPaletteMenuItem} );
            palettesMenu.Text = "&Palettes";
            // 
            // importPaletteMenuItem
            // 
            this.importPaletteMenuItem.Index = 0;
            this.importPaletteMenuItem.Text = "&Import...";
            this.importPaletteMenuItem.Click += new System.EventHandler( this.importPaletteMenuItem_Click );
            // 
            // exportPaletteMenuItem
            // 
            this.exportPaletteMenuItem.Index = 1;
            this.exportPaletteMenuItem.Text = "&Export...";
            this.exportPaletteMenuItem.Click += new System.EventHandler( this.exportPaletteMenuItem_Click );
            // 
            // paletteGroupBox
            // 
            paletteGroupBox.AutoSize = true;
            paletteGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            paletteGroupBox.Controls.Add( this.portraitCheckbox );
            paletteGroupBox.Controls.Add( this.paletteSelector );
            paletteGroupBox.Location = new System.Drawing.Point( 317, 3 );
            paletteGroupBox.Name = "paletteGroupBox";
            paletteGroupBox.Size = new System.Drawing.Size( 185, 106 );
            paletteGroupBox.TabIndex = 2;
            paletteGroupBox.TabStop = false;
            paletteGroupBox.Text = "Palette";
            // 
            // portraitCheckbox
            // 
            this.portraitCheckbox.Checked = true;
            this.portraitCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.portraitCheckbox.Location = new System.Drawing.Point( 6, 50 );
            this.portraitCheckbox.Name = "portraitCheckbox";
            this.portraitCheckbox.Size = new System.Drawing.Size( 153, 37 );
            this.portraitCheckbox.TabIndex = 3;
            this.portraitCheckbox.Text = "Always use corresponding palette for portrait";
            this.portraitCheckbox.UseVisualStyleBackColor = true;
            // 
            // paletteSelector
            // 
            this.paletteSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            // mainMenu
            // 
            mainMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.fileMenu,
            sprMenu,
            this.sp2Menu,
            palettesMenu} );
            // 
            // fileMenu
            // 
            this.fileMenu.Index = 0;
            this.fileMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.importBmpMenuItem,
            this.exportBmpMenuItem,
            separator1,
            this.closeMenuItem} );
            this.fileMenu.Text = "&File";
            // 
            // importBmpMenuItem
            // 
            this.importBmpMenuItem.Index = 0;
            this.importBmpMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.importBmpMenuItem.Text = "&Import BMP...";
            this.importBmpMenuItem.Click += new System.EventHandler( this.importBmpMenuItem_Click );
            // 
            // exportBmpMenuItem
            // 
            this.exportBmpMenuItem.Index = 1;
            this.exportBmpMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.exportBmpMenuItem.Text = "&Export BMP...";
            this.exportBmpMenuItem.Click += new System.EventHandler( this.exportBmpMenuItem_Click );
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Index = 3;
            this.closeMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlF4;
            this.closeMenuItem.Text = "&Close";
            this.closeMenuItem.Click += new System.EventHandler( this.closeMenuItem_Click );
            // 
            // sprMenu
            // 
            sprMenu.Index = 1;
            sprMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.importSprMenuItem,
            this.exportSprMenuItem} );
            sprMenu.Text = "&SPR";
            sprMenu.Popup += new System.EventHandler( this.sprMenu_Popup );
            // 
            // importSprMenuItem
            // 
            this.importSprMenuItem.Index = 0;
            this.importSprMenuItem.Text = "&Import...";
            this.importSprMenuItem.Click += new System.EventHandler( this.importSprMenuItem_Click );
            // 
            // exportSprMenuItem
            // 
            this.exportSprMenuItem.Index = 1;
            this.exportSprMenuItem.Text = "E&xport...";
            this.exportSprMenuItem.Click += new System.EventHandler( this.exportSprMenuItem_Click );
            // 
            // sp2Menu
            // 
            this.sp2Menu.Enabled = false;
            this.sp2Menu.Index = 2;
            this.sp2Menu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.importSp2MenuItem,
            this.exportSp2MenuItem,
            this.sp2Separator1,
            this.importSp2bMenuItem,
            this.exportSp2bMenuItem,
            this.sp2Separator2,
            this.importSp2cMenuItem,
            this.exportSp2cMenuItem,
            this.sp2Separator3,
            this.importSp2dMenuItem,
            this.exportSp2dMenuItem} );
            this.sp2Menu.Text = "SP&2";
            this.sp2Menu.Popup += new System.EventHandler( this.sp2Menu_Popup );
            // 
            // importSp2MenuItem
            // 
            this.importSp2MenuItem.Index = 0;
            this.importSp2MenuItem.Text = "Import...";
            this.importSp2MenuItem.Click += new System.EventHandler( this.importSp2MenuItem_Click );
            // 
            // exportSp2MenuItem
            // 
            this.exportSp2MenuItem.Index = 1;
            this.exportSp2MenuItem.Text = "Export...";
            this.exportSp2MenuItem.Click += new System.EventHandler( this.exportSp2MenuItem_Click );
            // 
            // sp2Separator1
            // 
            this.sp2Separator1.Index = 2;
            this.sp2Separator1.Text = "-";
            this.sp2Separator1.Visible = false;
            // 
            // importSp2bMenuItem
            // 
            this.importSp2bMenuItem.Index = 3;
            this.importSp2bMenuItem.Text = "Import 2...";
            this.importSp2bMenuItem.Visible = false;
            this.importSp2bMenuItem.Click += new System.EventHandler( this.importSp2bMenuItem_Click );
            // 
            // exportSp2bMenuItem
            // 
            this.exportSp2bMenuItem.Index = 4;
            this.exportSp2bMenuItem.Text = "Export 2...";
            this.exportSp2bMenuItem.Visible = false;
            this.exportSp2bMenuItem.Click += new System.EventHandler( this.exportSp2bMenuItem_Click );
            // 
            // sp2Separator2
            // 
            this.sp2Separator2.Index = 5;
            this.sp2Separator2.Text = "-";
            this.sp2Separator2.Visible = false;
            // 
            // importSp2cMenuItem
            // 
            this.importSp2cMenuItem.Index = 6;
            this.importSp2cMenuItem.Text = "Import 3...";
            this.importSp2cMenuItem.Visible = false;
            this.importSp2cMenuItem.Click += new System.EventHandler( this.importSp2cMenuItem_Click );
            // 
            // exportSp2cMenuItem
            // 
            this.exportSp2cMenuItem.Index = 7;
            this.exportSp2cMenuItem.Text = "Export 3...";
            this.exportSp2cMenuItem.Visible = false;
            this.exportSp2cMenuItem.Click += new System.EventHandler( this.exportSp2cMenuItem_Click );
            // 
            // sp2Separator3
            // 
            this.sp2Separator3.Index = 8;
            this.sp2Separator3.Text = "-";
            this.sp2Separator3.Visible = false;
            // 
            // importSp2dMenuItem
            // 
            this.importSp2dMenuItem.Index = 9;
            this.importSp2dMenuItem.Text = "Import 4...";
            this.importSp2dMenuItem.Visible = false;
            this.importSp2dMenuItem.Click += new System.EventHandler( this.importSp2dMenuItem_Click );
            // 
            // exportSp2dMenuItem
            // 
            this.exportSp2dMenuItem.Index = 10;
            this.exportSp2dMenuItem.Text = "Export 4...";
            this.exportSp2dMenuItem.Visible = false;
            this.exportSp2dMenuItem.Click += new System.EventHandler( this.exportSp2dMenuItem_Click );
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add( this.spriteViewer1 );
            this.panel1.Location = new System.Drawing.Point( 10, 3 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 300, 559 );
            this.panel1.TabIndex = 1;
            // 
            // spriteViewer1
            // 
            this.spriteViewer1.AutoScroll = true;
            this.spriteViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteViewer1.Location = new System.Drawing.Point( 0, 0 );
            this.spriteViewer1.Name = "spriteViewer1";
            this.spriteViewer1.Size = new System.Drawing.Size( 296, 555 );
            this.spriteViewer1.Sprite = null;
            this.spriteViewer1.TabIndex = 0;
            // 
            // shapesListBox
            // 
            this.shapesListBox.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.shapesListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.shapesListBox.FormattingEnabled = true;
            this.shapesListBox.IntegralHeight = false;
            this.shapesListBox.ItemHeight = 180;
            this.shapesListBox.Location = new System.Drawing.Point( 317, 140 );
            this.shapesListBox.Name = "shapesListBox";
            this.shapesListBox.Size = new System.Drawing.Size( 270, 367 );
            this.shapesListBox.TabIndex = 3;
            // 
            // SpriteDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 599, 571 );
            this.Controls.Add( this.shapesListBox );
            this.Controls.Add( paletteGroupBox );
            this.Controls.Add( this.panel1 );
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.MaximizeBox = false;
            this.Menu = mainMenu;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size( 488, 560 );
            this.Name = "SpriteDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Shishi Sprite Manager";
            paletteGroupBox.ResumeLayout( false );
            this.panel1.ResumeLayout( false );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private SpriteViewer spriteViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox paletteSelector;
        private System.Windows.Forms.CheckBox portraitCheckbox;
        private System.Windows.Forms.ListBox shapesListBox;
        private System.Windows.Forms.MenuItem fileMenu;
        private System.Windows.Forms.MenuItem importBmpMenuItem;
        private System.Windows.Forms.MenuItem exportBmpMenuItem;
        private System.Windows.Forms.MenuItem closeMenuItem;
        private System.Windows.Forms.MenuItem importPaletteMenuItem;
        private System.Windows.Forms.MenuItem exportPaletteMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.MenuItem sp2Menu;
        private System.Windows.Forms.MenuItem importSprMenuItem;
        private System.Windows.Forms.MenuItem exportSprMenuItem;
        private System.Windows.Forms.MenuItem importSp2MenuItem;
        private System.Windows.Forms.MenuItem exportSp2MenuItem;
        private System.Windows.Forms.MenuItem sp2Separator1;
        private System.Windows.Forms.MenuItem importSp2bMenuItem;
        private System.Windows.Forms.MenuItem exportSp2bMenuItem;
        private System.Windows.Forms.MenuItem sp2Separator2;
        private System.Windows.Forms.MenuItem importSp2cMenuItem;
        private System.Windows.Forms.MenuItem exportSp2cMenuItem;
        private System.Windows.Forms.MenuItem sp2Separator3;
        private System.Windows.Forms.MenuItem importSp2dMenuItem;
        private System.Windows.Forms.MenuItem exportSp2dMenuItem;
    }
}

