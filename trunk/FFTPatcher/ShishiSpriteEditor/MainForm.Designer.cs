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
            System.Windows.Forms.MenuItem separator1;
            System.Windows.Forms.MenuItem separator3;
            System.Windows.Forms.MenuItem pspMenuItem;
            System.Windows.Forms.MenuItem psxMenuItem;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
            this.openMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.importPspMenuItem = new System.Windows.Forms.MenuItem();
            this.patchPspMenuItem = new System.Windows.Forms.MenuItem();
            this.importPsxMenuItem = new System.Windows.Forms.MenuItem();
            this.patchPsxMenuItem = new System.Windows.Forms.MenuItem();
            this.mainMenu = new System.Windows.Forms.MainMenu( this.components );
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.fullSpriteSetEditor1 = new FFTPatcher.SpriteEditor.FullSpriteSetEditor();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            fileMenu = new System.Windows.Forms.MenuItem();
            separator1 = new System.Windows.Forms.MenuItem();
            separator3 = new System.Windows.Forms.MenuItem();
            pspMenuItem = new System.Windows.Forms.MenuItem();
            psxMenuItem = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // fileMenu
            // 
            fileMenu.Index = 0;
            fileMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.openMenuItem,
            separator1,
            this.saveMenuItem,
            separator3,
            this.exitMenuItem} );
            fileMenu.Text = "&File";
            fileMenu.Popup += new System.EventHandler( this.fileMenu_Popup );
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
            // separator3
            // 
            separator3.Index = 3;
            separator3.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 4;
            this.exitMenuItem.Text = "E&xit";
            // 
            // pspMenuItem
            // 
            pspMenuItem.Index = 0;
            pspMenuItem.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.importPspMenuItem,
            this.patchPspMenuItem} );
            pspMenuItem.Text = "&PSP";
            pspMenuItem.Popup += new System.EventHandler( this.pspMenuItem_Popup );
            // 
            // importPspMenuItem
            // 
            this.importPspMenuItem.Index = 0;
            this.importPspMenuItem.Text = "&Import sprites from ISO...";
            // 
            // patchPspMenuItem
            // 
            this.patchPspMenuItem.Enabled = false;
            this.patchPspMenuItem.Index = 1;
            this.patchPspMenuItem.Text = "&Patch ISO with sprites...";
            this.patchPspMenuItem.Click += new System.EventHandler( this.patchPspMenuItem_Click );
            // 
            // psxMenuItem
            // 
            psxMenuItem.Index = 1;
            psxMenuItem.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.importPsxMenuItem,
            this.patchPsxMenuItem} );
            psxMenuItem.Text = "PS&X";
            psxMenuItem.Popup += new System.EventHandler( this.psxMenuItem_Popup );
            // 
            // importPsxMenuItem
            // 
            this.importPsxMenuItem.Index = 0;
            this.importPsxMenuItem.Text = "&Import sprites from ISO...";
            // 
            // patchPsxMenuItem
            // 
            this.patchPsxMenuItem.Enabled = false;
            this.patchPsxMenuItem.Index = 1;
            this.patchPsxMenuItem.Text = "&Patch ISO with sprites...";
            this.patchPsxMenuItem.Click += new System.EventHandler( this.patchPsxMenuItem_Click );
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            fileMenu,
            this.menuItem1,
            this.aboutMenuItem} );
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            pspMenuItem,
            psxMenuItem} );
            this.menuItem1.Text = "&ISO";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 2;
            this.aboutMenuItem.Text = "About...";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Sprite files (*.SPR)|*.SPR|Secondary sprite files (*.SP2)|*.SP2";
            // 
            // fullSpriteSetEditor1
            // 
            this.fullSpriteSetEditor1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.fullSpriteSetEditor1.Location = new System.Drawing.Point( 12, 12 );
            this.fullSpriteSetEditor1.Name = "fullSpriteSetEditor1";
            this.fullSpriteSetEditor1.Size = new System.Drawing.Size( 516, 500 );
            this.fullSpriteSetEditor1.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 540, 524 );
            this.Controls.Add( this.fullSpriteSetEditor1 );
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size( 488, 560 );
            this.Name = "MainForm";
            this.Text = "Shishi Sprite Manager";
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem openMenuItem;
        private System.Windows.Forms.MenuItem saveMenuItem;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem importPspMenuItem;
        private System.Windows.Forms.MenuItem patchPspMenuItem;
        private System.Windows.Forms.MenuItem importPsxMenuItem;
        private System.Windows.Forms.MenuItem patchPsxMenuItem;
        private FullSpriteSetEditor fullSpriteSetEditor1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

