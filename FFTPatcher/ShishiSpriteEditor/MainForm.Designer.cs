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
            System.Windows.Forms.MainMenu mainMenu;
            System.Windows.Forms.MenuItem fileMenu;
            System.Windows.Forms.MenuItem openIsoMenuItem;
            System.Windows.Forms.MenuItem separator1;
            System.Windows.Forms.MenuItem exitMenuItem;
            System.Windows.Forms.MenuItem importSprMenuItem;
            System.Windows.Forms.MenuItem exportSprMenuItem;
            System.Windows.Forms.MenuItem separator2;
            System.Windows.Forms.MenuItem importBmpMenuItem;
            System.Windows.Forms.MenuItem exportBmpMenuItem;
            System.Windows.Forms.MenuItem separator3;
            System.Windows.Forms.MenuItem separator4;
            System.Windows.Forms.MenuItem separator5;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.spriteMenuItem = new System.Windows.Forms.MenuItem();
            this.sp2Menu = new System.Windows.Forms.MenuItem();
            this.importFirstMenuItem = new System.Windows.Forms.MenuItem();
            this.exportFirstMenuItem = new System.Windows.Forms.MenuItem();
            this.importSecondMenuItem = new System.Windows.Forms.MenuItem();
            this.exportSecondMenuItem = new System.Windows.Forms.MenuItem();
            this.importThirdMenuItem = new System.Windows.Forms.MenuItem();
            this.exportThirdMenuItem = new System.Windows.Forms.MenuItem();
            this.importFourthMenuItem = new System.Windows.Forms.MenuItem();
            this.exportFourthMenuItem = new System.Windows.Forms.MenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.allSpritesEditor1 = new FFTPatcher.SpriteEditor.AllSpritesEditor();
            mainMenu = new System.Windows.Forms.MainMenu(this.components);
            fileMenu = new System.Windows.Forms.MenuItem();
            openIsoMenuItem = new System.Windows.Forms.MenuItem();
            separator1 = new System.Windows.Forms.MenuItem();
            exitMenuItem = new System.Windows.Forms.MenuItem();
            importSprMenuItem = new System.Windows.Forms.MenuItem();
            exportSprMenuItem = new System.Windows.Forms.MenuItem();
            separator2 = new System.Windows.Forms.MenuItem();
            importBmpMenuItem = new System.Windows.Forms.MenuItem();
            exportBmpMenuItem = new System.Windows.Forms.MenuItem();
            separator3 = new System.Windows.Forms.MenuItem();
            separator4 = new System.Windows.Forms.MenuItem();
            separator5 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            fileMenu,
            this.spriteMenuItem,
            this.sp2Menu});
            // 
            // fileMenu
            // 
            fileMenu.Index = 0;
            fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            openIsoMenuItem,
            separator1,
            exitMenuItem});
            fileMenu.Text = "&File";
            // 
            // openIsoMenuItem
            // 
            openIsoMenuItem.Index = 0;
            openIsoMenuItem.Text = "&Open ISO...";
            openIsoMenuItem.Click += new System.EventHandler(this.openIsoMenuItem_Click);
            // 
            // separator1
            // 
            separator1.Index = 1;
            separator1.Text = "-";
            // 
            // exitMenuItem
            // 
            exitMenuItem.Index = 2;
            exitMenuItem.Text = "E&xit";
            exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // spriteMenuItem
            // 
            this.spriteMenuItem.Enabled = false;
            this.spriteMenuItem.Index = 1;
            this.spriteMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            importSprMenuItem,
            exportSprMenuItem,
            separator2,
            importBmpMenuItem,
            exportBmpMenuItem});
            this.spriteMenuItem.Text = "Sprite";
            // 
            // importSprMenuItem
            // 
            importSprMenuItem.Index = 0;
            importSprMenuItem.Text = "Import SPR...";
            importSprMenuItem.Click += new System.EventHandler(this.importSprMenuItem_Click);
            // 
            // exportSprMenuItem
            // 
            exportSprMenuItem.Index = 1;
            exportSprMenuItem.Text = "Export SPR...";
            exportSprMenuItem.Click += new System.EventHandler(this.exportSprMenuItem_Click);
            // 
            // separator2
            // 
            separator2.Index = 2;
            separator2.Text = "-";
            // 
            // importBmpMenuItem
            // 
            importBmpMenuItem.Index = 3;
            importBmpMenuItem.Text = "Import BMP...";
            importBmpMenuItem.Click += new System.EventHandler(this.importBmpMenuItem_Click);
            // 
            // exportBmpMenuItem
            // 
            exportBmpMenuItem.Index = 4;
            exportBmpMenuItem.Text = "Export BMP...";
            exportBmpMenuItem.Click += new System.EventHandler(this.exportBmpMenuItem_Click);
            // 
            // sp2Menu
            // 
            this.sp2Menu.Enabled = false;
            this.sp2Menu.Index = 2;
            this.sp2Menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.importFirstMenuItem,
            this.exportFirstMenuItem,
            separator3,
            this.importSecondMenuItem,
            this.exportSecondMenuItem,
            separator4,
            this.importThirdMenuItem,
            this.exportThirdMenuItem,
            separator5,
            this.importFourthMenuItem,
            this.exportFourthMenuItem});
            this.sp2Menu.Text = "SP2";
            this.sp2Menu.Popup += new System.EventHandler(this.sp2Menu_Popup);
            // 
            // importFirstMenuItem
            // 
            this.importFirstMenuItem.Enabled = false;
            this.importFirstMenuItem.Index = 0;
            this.importFirstMenuItem.Tag = "1";
            this.importFirstMenuItem.Text = "Import first SP2...";
            this.importFirstMenuItem.Click += new System.EventHandler(this.importSp2MenuItem_Click);
            // 
            // exportFirstMenuItem
            // 
            this.exportFirstMenuItem.Enabled = false;
            this.exportFirstMenuItem.Index = 1;
            this.exportFirstMenuItem.Tag = "1";
            this.exportFirstMenuItem.Text = "Export first SP2...";
            this.exportFirstMenuItem.Click += new System.EventHandler(this.exportSp2MenuItem_Click);
            // 
            // separator3
            // 
            separator3.Index = 2;
            separator3.Text = "-";
            // 
            // importSecondMenuItem
            // 
            this.importSecondMenuItem.Enabled = false;
            this.importSecondMenuItem.Index = 3;
            this.importSecondMenuItem.Tag = "2";
            this.importSecondMenuItem.Text = "Import second SP2...";
            this.importSecondMenuItem.Click += new System.EventHandler(this.importSp2MenuItem_Click);
            // 
            // exportSecondMenuItem
            // 
            this.exportSecondMenuItem.Enabled = false;
            this.exportSecondMenuItem.Index = 4;
            this.exportSecondMenuItem.Tag = "2";
            this.exportSecondMenuItem.Text = "Export second SP2...";
            this.exportSecondMenuItem.Click += new System.EventHandler(this.exportSp2MenuItem_Click);
            // 
            // separator4
            // 
            separator4.Index = 5;
            separator4.Text = "-";
            // 
            // importThirdMenuItem
            // 
            this.importThirdMenuItem.Enabled = false;
            this.importThirdMenuItem.Index = 6;
            this.importThirdMenuItem.Tag = "3";
            this.importThirdMenuItem.Text = "Import third SP2...";
            this.importThirdMenuItem.Click += new System.EventHandler(this.importSp2MenuItem_Click);
            // 
            // exportThirdMenuItem
            // 
            this.exportThirdMenuItem.Enabled = false;
            this.exportThirdMenuItem.Index = 7;
            this.exportThirdMenuItem.Tag = "3";
            this.exportThirdMenuItem.Text = "Export third SP2...";
            this.exportThirdMenuItem.Click += new System.EventHandler(this.exportSp2MenuItem_Click);
            // 
            // separator5
            // 
            separator5.Index = 8;
            separator5.Text = "-";
            // 
            // importFourthMenuItem
            // 
            this.importFourthMenuItem.Enabled = false;
            this.importFourthMenuItem.Index = 9;
            this.importFourthMenuItem.Tag = "4";
            this.importFourthMenuItem.Text = "Import fourth SP2...";
            this.importFourthMenuItem.Click += new System.EventHandler(this.importSp2MenuItem_Click);
            // 
            // exportFourthMenuItem
            // 
            this.exportFourthMenuItem.Enabled = false;
            this.exportFourthMenuItem.Index = 10;
            this.exportFourthMenuItem.Tag = "4";
            this.exportFourthMenuItem.Text = "Export fourth SP2...";
            this.exportFourthMenuItem.Click += new System.EventHandler(this.exportSp2MenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Sprite files (*.SPR)|*.SPR|Secondary sprite files (*.SP2)|*.SP2";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            // 
            // allSpritesEditor1
            // 
            this.allSpritesEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.allSpritesEditor1.Enabled = false;
            this.allSpritesEditor1.Location = new System.Drawing.Point(12, 2);
            this.allSpritesEditor1.Name = "allSpritesEditor1";
            this.allSpritesEditor1.Size = new System.Drawing.Size(618, 593);
            this.allSpritesEditor1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 607);
            this.Controls.Add(this.allSpritesEditor1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = mainMenu;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(488, 560);
            this.Name = "MainForm";
            this.Text = "Shishi Sprite Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private AllSpritesEditor allSpritesEditor1;
        private System.Windows.Forms.MenuItem spriteMenuItem;
        private System.Windows.Forms.MenuItem sp2Menu;
        private System.Windows.Forms.MenuItem importFirstMenuItem;
        private System.Windows.Forms.MenuItem exportFirstMenuItem;
        private System.Windows.Forms.MenuItem importSecondMenuItem;
        private System.Windows.Forms.MenuItem exportSecondMenuItem;
        private System.Windows.Forms.MenuItem importThirdMenuItem;
        private System.Windows.Forms.MenuItem exportThirdMenuItem;
        private System.Windows.Forms.MenuItem importFourthMenuItem;
        private System.Windows.Forms.MenuItem exportFourthMenuItem;
    }
}

