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

namespace FFTPatcher
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
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.MenuItem fileMenuItem;
            System.Windows.Forms.MenuItem exportMenu;
            this.newPSXMenuItem = new System.Windows.Forms.MenuItem();
            this.newPSPMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.openMenuItem = new System.Windows.Forms.MenuItem();
            this.openModifiedMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.applySCUSMenuItem = new System.Windows.Forms.MenuItem();
            this.generateMenuItem = new System.Windows.Forms.MenuItem();
            this.mainMenu = new System.Windows.Forms.MainMenu( this.components );
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.applyPatchOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fftPatchEditor1 = new FFTPatcher.Editors.FFTPatchEditor();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.applyBattleBinMenuItem = new System.Windows.Forms.MenuItem();
            this.generateFontMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            fileMenuItem = new System.Windows.Forms.MenuItem();
            exportMenu = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // fileMenuItem
            // 
            fileMenuItem.Index = 0;
            fileMenuItem.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.newPSXMenuItem,
            this.newPSPMenuItem,
            this.menuItem4,
            this.openMenuItem,
            this.openModifiedMenuItem,
            this.saveMenuItem,
            this.menuItem7,
            this.exitMenuItem} );
            fileMenuItem.Text = "&File";
            // 
            // newPSXMenuItem
            // 
            this.newPSXMenuItem.Index = 0;
            this.newPSXMenuItem.Text = "New PS&X patch";
            // 
            // newPSPMenuItem
            // 
            this.newPSPMenuItem.Index = 1;
            this.newPSPMenuItem.Text = "New PS&P patch";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "-";
            // 
            // openMenuItem
            // 
            this.openMenuItem.Index = 3;
            this.openMenuItem.Text = "&Open patch...";
            // 
            // openModifiedMenuItem
            // 
            this.openModifiedMenuItem.Index = 4;
            this.openModifiedMenuItem.Text = "Open modified SCUS_942.21...";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Index = 5;
            this.saveMenuItem.Text = "&Save patch...";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 6;
            this.menuItem7.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 7;
            this.exitMenuItem.Text = "E&xit";
            // 
            // exportMenu
            // 
            exportMenu.Index = 1;
            exportMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.applySCUSMenuItem,
            this.menuItem3,
            this.generateFontMenuItem,
            this.applyBattleBinMenuItem,
            this.menuItem5,
            this.generateMenuItem} );
            exportMenu.Text = "&Export";
            // 
            // applySCUSMenuItem
            // 
            this.applySCUSMenuItem.Enabled = false;
            this.applySCUSMenuItem.Index = 0;
            this.applySCUSMenuItem.Text = "&Apply patch to SCUS_942.21...";
            // 
            // generateMenuItem
            // 
            this.generateMenuItem.Enabled = false;
            this.generateMenuItem.Index = 5;
            this.generateMenuItem.Text = "&Generate ENTD?.ENT files...";
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            fileMenuItem,
            exportMenu,
            this.aboutMenuItem} );
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 2;
            this.aboutMenuItem.Text = "About...";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "FFTPatcher files|*.fftpatch";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "FFTPatcher files (*.fftpatch)|*.fftpatch";
            // 
            // applyPatchOpenFileDialog
            // 
            this.applyPatchOpenFileDialog.Filter = "ISO files|*.iso";
            // 
            // fftPatchEditor1
            // 
            this.fftPatchEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fftPatchEditor1.Enabled = false;
            this.fftPatchEditor1.Location = new System.Drawing.Point( 0, 0 );
            this.fftPatchEditor1.Name = "fftPatchEditor1";
            this.fftPatchEditor1.Size = new System.Drawing.Size( 815, 671 );
            this.fftPatchEditor1.TabIndex = 0;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Where should the files be exported?\nAny files in the folder you choose with the n" +
                "ames ENTD1.ENT, ENTD2.ENT, ENTD3.ENT, ENTD4.ENT, or ENTD5.ENT will be overwritte" +
                "n.";
            // 
            // applyBattleBinMenuItem
            // 
            this.applyBattleBinMenuItem.Enabled = false;
            this.applyBattleBinMenuItem.Index = 3;
            this.applyBattleBinMenuItem.Text = "Apply font widths to BATTLE.BIN...";
            // 
            // generateFontMenuItem
            // 
            this.generateFontMenuItem.Enabled = false;
            this.generateFontMenuItem.Index = 2;
            this.generateFontMenuItem.Text = "Generate FONT.BIN...";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "-";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            this.menuItem5.Text = "-";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size( 815, 671 );
            this.Controls.Add( this.fftPatchEditor1 );
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "FFTPatcher";
            this.ResumeLayout( false );

        }

        #endregion

        private FFTPatcher.Editors.FFTPatchEditor fftPatchEditor1;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem newPSXMenuItem;
        private System.Windows.Forms.MenuItem newPSPMenuItem;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem openMenuItem;
        private System.Windows.Forms.MenuItem saveMenuItem;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem applySCUSMenuItem;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog applyPatchOpenFileDialog;
        private System.Windows.Forms.MenuItem openModifiedMenuItem;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.MenuItem generateMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem generateFontMenuItem;
        private System.Windows.Forms.MenuItem applyBattleBinMenuItem;
        private System.Windows.Forms.MenuItem menuItem5;

    }
}

