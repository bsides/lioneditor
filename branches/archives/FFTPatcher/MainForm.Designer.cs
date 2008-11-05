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
            System.Windows.Forms.MenuItem separator1;
            System.Windows.Forms.MenuItem separator2;
            System.Windows.Forms.MenuItem psxMenu;
            System.Windows.Forms.MenuItem separator3;
            System.Windows.Forms.MenuItem pspMenu;
            System.Windows.Forms.MenuItem separator5;
            System.Windows.Forms.MenuItem utilitiesMenuItem;
            System.Windows.Forms.MenuItem separator6;
            this.newPSXMenuItem = new System.Windows.Forms.MenuItem();
            this.newPSPMenuItem = new System.Windows.Forms.MenuItem();
            this.openMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.saveAsPspMenuItem = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.patchPsxIsoMenuItem = new System.Windows.Forms.MenuItem();
            this.patchPspIsoMenuItem = new System.Windows.Forms.MenuItem();
            this.cheatdbMenuItem = new System.Windows.Forms.MenuItem();
            this.extractFFTPackMenuItem = new System.Windows.Forms.MenuItem();
            this.rebuildFFTPackMenuItem = new System.Windows.Forms.MenuItem();
            this.decryptMenuItem = new System.Windows.Forms.MenuItem();
            this.mainMenu = new System.Windows.Forms.MainMenu( this.components );
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.applyPatchOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.fftPatchEditor1 = new FFTPatcher.Editors.FFTPatchEditor();
            this.patchPsxBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            fileMenuItem = new System.Windows.Forms.MenuItem();
            separator1 = new System.Windows.Forms.MenuItem();
            separator2 = new System.Windows.Forms.MenuItem();
            psxMenu = new System.Windows.Forms.MenuItem();
            separator3 = new System.Windows.Forms.MenuItem();
            pspMenu = new System.Windows.Forms.MenuItem();
            separator5 = new System.Windows.Forms.MenuItem();
            utilitiesMenuItem = new System.Windows.Forms.MenuItem();
            separator6 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // fileMenuItem
            // 
            fileMenuItem.Index = 0;
            fileMenuItem.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.newPSXMenuItem,
            this.newPSPMenuItem,
            separator1,
            this.openMenuItem,
            this.saveMenuItem,
            this.saveAsPspMenuItem,
            separator2,
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
            // separator1
            // 
            separator1.Index = 2;
            separator1.Text = "-";
            // 
            // openMenuItem
            // 
            this.openMenuItem.Index = 3;
            this.openMenuItem.Text = "&Open patch...";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Enabled = false;
            this.saveMenuItem.Index = 4;
            this.saveMenuItem.Text = "&Save patch...";
            // 
            // saveAsPspMenuItem
            // 
            this.saveAsPspMenuItem.Enabled = false;
            this.saveAsPspMenuItem.Index = 5;
            this.saveAsPspMenuItem.Text = "Save &as PSP patch...";
            // 
            // separator2
            // 
            separator2.Index = 6;
            separator2.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 7;
            this.exitMenuItem.Text = "E&xit";
            // 
            // psxMenu
            // 
            psxMenu.Index = 1;
            psxMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.patchPsxIsoMenuItem} );
            psxMenu.Text = "PS&X";
            // 
            // patchPsxIsoMenuItem
            // 
            this.patchPsxIsoMenuItem.Enabled = false;
            this.patchPsxIsoMenuItem.Index = 0;
            this.patchPsxIsoMenuItem.Text = "Patch &ISO...";
            // 
            // pspMenu
            // 
            pspMenu.Index = 2;
            pspMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.patchPspIsoMenuItem,
            this.cheatdbMenuItem,
            separator5,
            utilitiesMenuItem} );
            pspMenu.Text = "&PSP";
            // 
            // patchPspIsoMenuItem
            // 
            this.patchPspIsoMenuItem.Enabled = false;
            this.patchPspIsoMenuItem.Index = 0;
            this.patchPspIsoMenuItem.Text = "&Patch War of the Lions ISO...";
            // 
            // cheatdbMenuItem
            // 
            this.cheatdbMenuItem.Enabled = false;
            this.cheatdbMenuItem.Index = 1;
            this.cheatdbMenuItem.Text = "&Generate cheat.db...";
            // 
            // separator5
            // 
            separator5.Index = 2;
            separator5.Text = "-";
            // 
            // utilitiesMenuItem
            // 
            utilitiesMenuItem.Index = 3;
            utilitiesMenuItem.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.extractFFTPackMenuItem,
            this.rebuildFFTPackMenuItem,
            separator6,
            this.decryptMenuItem} );
            utilitiesMenuItem.Text = "&Utilities";
            // 
            // extractFFTPackMenuItem
            // 
            this.extractFFTPackMenuItem.Index = 0;
            this.extractFFTPackMenuItem.Text = "E&xtract fftpack.bin...";
            // 
            // rebuildFFTPackMenuItem
            // 
            this.rebuildFFTPackMenuItem.Index = 1;
            this.rebuildFFTPackMenuItem.Text = "&Rebuild fftpack.bin...";
            // 
            // separator6
            // 
            separator6.Index = 2;
            separator6.Text = "-";
            // 
            // decryptMenuItem
            // 
            this.decryptMenuItem.Index = 3;
            this.decryptMenuItem.Text = "&Decrypt War of the Lions ISO...";
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            fileMenuItem,
            psxMenu,
            pspMenu,
            this.aboutMenuItem} );
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 3;
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
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point( 87, 299 );
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size( 641, 23 );
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // fftPatchEditor1
            // 
            this.fftPatchEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fftPatchEditor1.Enabled = false;
            this.fftPatchEditor1.Location = new System.Drawing.Point( 0, 0 );
            this.fftPatchEditor1.Name = "fftPatchEditor1";
            this.fftPatchEditor1.Size = new System.Drawing.Size( 815, 599 );
            this.fftPatchEditor1.TabIndex = 0;
            // 
            // patchPsxBackgroundWorker
            // 
            this.patchPsxBackgroundWorker.WorkerReportsProgress = true;
            this.patchPsxBackgroundWorker.WorkerSupportsCancellation = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size( 815, 599 );
            this.Controls.Add( this.progressBar );
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
        private System.Windows.Forms.MenuItem openMenuItem;
        private System.Windows.Forms.MenuItem saveMenuItem;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog applyPatchOpenFileDialog;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.MenuItem patchPspIsoMenuItem;
        private System.Windows.Forms.MenuItem cheatdbMenuItem;
        private System.Windows.Forms.MenuItem extractFFTPackMenuItem;
        private System.Windows.Forms.MenuItem rebuildFFTPackMenuItem;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.MenuItem decryptMenuItem;
        private System.Windows.Forms.MenuItem patchPsxIsoMenuItem;
        private System.Windows.Forms.MenuItem saveAsPspMenuItem;
        private System.ComponentModel.BackgroundWorker patchPsxBackgroundWorker;

    }
}

