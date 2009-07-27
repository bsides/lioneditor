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
            if (disposing && (components != null))
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
            System.Windows.Forms.MenuItem separator6;
            System.Windows.Forms.MenuItem importSprMenuItem;
            System.Windows.Forms.MenuItem exportSprMenuItem;
            System.Windows.Forms.MenuItem separator2;
            System.Windows.Forms.MenuItem importBmpMenuItem;
            System.Windows.Forms.MenuItem exportBmpMenuItem;
            System.Windows.Forms.MenuItem separator3;
            System.Windows.Forms.MenuItem separator4;
            System.Windows.Forms.MenuItem separator5;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
            this.imageMenuItem = new System.Windows.Forms.MenuItem();
            this.importImageMenuItem = new System.Windows.Forms.MenuItem();
            this.exportImageMenuItem = new System.Windows.Forms.MenuItem();
            this.importAllImagesMenuItem = new System.Windows.Forms.MenuItem();
            this.dumpAllImagesMenuItem = new System.Windows.Forms.MenuItem();
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.spriteTabPage = new System.Windows.Forms.TabPage();
            this.allSpritesEditor1 = new FFTPatcher.SpriteEditor.AllSpritesEditor();
            this.otherTabPage = new System.Windows.Forms.TabPage();
            this.allOtherImagesEditor1 = new FFTPatcher.SpriteEditor.AllOtherImagesEditor();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.plus12Button = new System.Windows.Forms.Button();
            this.plus4Button = new System.Windows.Forms.Button();
            this.plus2Button = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.plus16Button = new System.Windows.Forms.Button();
            this.plus8Button = new System.Windows.Forms.Button();
            this.plusOneButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._8bitRadioButton = new System.Windows.Forms.RadioButton();
            this._16BitRadioButton = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.heightSpinner = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.widthSpinner = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.ySpinner = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.xSpinner = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            mainMenu = new System.Windows.Forms.MainMenu( this.components );
            fileMenu = new System.Windows.Forms.MenuItem();
            openIsoMenuItem = new System.Windows.Forms.MenuItem();
            separator1 = new System.Windows.Forms.MenuItem();
            exitMenuItem = new System.Windows.Forms.MenuItem();
            separator6 = new System.Windows.Forms.MenuItem();
            importSprMenuItem = new System.Windows.Forms.MenuItem();
            exportSprMenuItem = new System.Windows.Forms.MenuItem();
            separator2 = new System.Windows.Forms.MenuItem();
            importBmpMenuItem = new System.Windows.Forms.MenuItem();
            exportBmpMenuItem = new System.Windows.Forms.MenuItem();
            separator3 = new System.Windows.Forms.MenuItem();
            separator4 = new System.Windows.Forms.MenuItem();
            separator5 = new System.Windows.Forms.MenuItem();
            this.tabControl1.SuspendLayout();
            this.spriteTabPage.SuspendLayout();
            this.otherTabPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.numericUpDown2 ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.numericUpDown1 ) ).BeginInit();
            this.groupBox1.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox2 ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.heightSpinner ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.widthSpinner ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.ySpinner ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.xSpinner ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            mainMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            fileMenu,
            this.imageMenuItem,
            this.spriteMenuItem,
            this.sp2Menu} );
            // 
            // fileMenu
            // 
            fileMenu.Index = 0;
            fileMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            openIsoMenuItem,
            separator1,
            exitMenuItem} );
            fileMenu.Text = "&File";
            // 
            // openIsoMenuItem
            // 
            openIsoMenuItem.Index = 0;
            openIsoMenuItem.Text = "&Open ISO...";
            openIsoMenuItem.Click += new System.EventHandler( this.openIsoMenuItem_Click );
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
            exitMenuItem.Click += new System.EventHandler( this.exitMenuItem_Click );
            // 
            // imageMenuItem
            // 
            this.imageMenuItem.Index = 1;
            this.imageMenuItem.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.importImageMenuItem,
            this.exportImageMenuItem,
            separator6,
            this.importAllImagesMenuItem,
            this.dumpAllImagesMenuItem} );
            this.imageMenuItem.Text = "Image";
            this.imageMenuItem.Visible = false;
            // 
            // importImageMenuItem
            // 
            this.importImageMenuItem.Index = 0;
            this.importImageMenuItem.Text = "Import...";
            this.importImageMenuItem.Click += new System.EventHandler( this.importImageMenuItem_Click );
            // 
            // exportImageMenuItem
            // 
            this.exportImageMenuItem.Index = 1;
            this.exportImageMenuItem.Text = "Export...";
            this.exportImageMenuItem.Click += new System.EventHandler( this.exportImageMenuItem_Click );
            // 
            // separator6
            // 
            separator6.Index = 2;
            separator6.Text = "-";
            // 
            // importAllImagesMenuItem
            // 
            this.importAllImagesMenuItem.Index = 3;
            this.importAllImagesMenuItem.Text = "Import all images...";
            this.importAllImagesMenuItem.Click += new System.EventHandler( this.importAllImagesMenuItem_Click );
            // 
            // dumpAllImagesMenuItem
            // 
            this.dumpAllImagesMenuItem.Index = 4;
            this.dumpAllImagesMenuItem.Text = "Dump all images...";
            this.dumpAllImagesMenuItem.Click += new System.EventHandler( this.dumpAllImagesMenuItem_Click );
            // 
            // spriteMenuItem
            // 
            this.spriteMenuItem.Enabled = false;
            this.spriteMenuItem.Index = 2;
            this.spriteMenuItem.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            importSprMenuItem,
            exportSprMenuItem,
            separator2,
            importBmpMenuItem,
            exportBmpMenuItem} );
            this.spriteMenuItem.Text = "Sprite";
            // 
            // importSprMenuItem
            // 
            importSprMenuItem.Index = 0;
            importSprMenuItem.Text = "Import SPR...";
            importSprMenuItem.Click += new System.EventHandler( this.importSprMenuItem_Click );
            // 
            // exportSprMenuItem
            // 
            exportSprMenuItem.Index = 1;
            exportSprMenuItem.Text = "Export SPR...";
            exportSprMenuItem.Click += new System.EventHandler( this.exportSprMenuItem_Click );
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
            importBmpMenuItem.Click += new System.EventHandler( this.importBmpMenuItem_Click );
            // 
            // exportBmpMenuItem
            // 
            exportBmpMenuItem.Index = 4;
            exportBmpMenuItem.Text = "Export BMP...";
            exportBmpMenuItem.Click += new System.EventHandler( this.exportBmpMenuItem_Click );
            // 
            // sp2Menu
            // 
            this.sp2Menu.Enabled = false;
            this.sp2Menu.Index = 3;
            this.sp2Menu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
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
            this.exportFourthMenuItem} );
            this.sp2Menu.Text = "SP2";
            this.sp2Menu.Popup += new System.EventHandler( this.sp2Menu_Popup );
            // 
            // importFirstMenuItem
            // 
            this.importFirstMenuItem.Enabled = false;
            this.importFirstMenuItem.Index = 0;
            this.importFirstMenuItem.Tag = "1";
            this.importFirstMenuItem.Text = "Import first SP2...";
            this.importFirstMenuItem.Click += new System.EventHandler( this.importSp2MenuItem_Click );
            // 
            // exportFirstMenuItem
            // 
            this.exportFirstMenuItem.Enabled = false;
            this.exportFirstMenuItem.Index = 1;
            this.exportFirstMenuItem.Tag = "1";
            this.exportFirstMenuItem.Text = "Export first SP2...";
            this.exportFirstMenuItem.Click += new System.EventHandler( this.exportSp2MenuItem_Click );
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
            this.importSecondMenuItem.Click += new System.EventHandler( this.importSp2MenuItem_Click );
            // 
            // exportSecondMenuItem
            // 
            this.exportSecondMenuItem.Enabled = false;
            this.exportSecondMenuItem.Index = 4;
            this.exportSecondMenuItem.Tag = "2";
            this.exportSecondMenuItem.Text = "Export second SP2...";
            this.exportSecondMenuItem.Click += new System.EventHandler( this.exportSp2MenuItem_Click );
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
            this.importThirdMenuItem.Click += new System.EventHandler( this.importSp2MenuItem_Click );
            // 
            // exportThirdMenuItem
            // 
            this.exportThirdMenuItem.Enabled = false;
            this.exportThirdMenuItem.Index = 7;
            this.exportThirdMenuItem.Tag = "3";
            this.exportThirdMenuItem.Text = "Export third SP2...";
            this.exportThirdMenuItem.Click += new System.EventHandler( this.exportSp2MenuItem_Click );
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
            this.importFourthMenuItem.Click += new System.EventHandler( this.importSp2MenuItem_Click );
            // 
            // exportFourthMenuItem
            // 
            this.exportFourthMenuItem.Enabled = false;
            this.exportFourthMenuItem.Index = 10;
            this.exportFourthMenuItem.Tag = "4";
            this.exportFourthMenuItem.Text = "Export fourth SP2...";
            this.exportFourthMenuItem.Click += new System.EventHandler( this.exportSp2MenuItem_Click );
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.tabControl1.Controls.Add( this.spriteTabPage );
            this.tabControl1.Controls.Add( this.otherTabPage );
            this.tabControl1.Controls.Add( this.tabPage1 );
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point( 2, 4 );
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size( 738, 663 );
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler( this.tabControl1_SelectedIndexChanged );
            // 
            // spriteTabPage
            // 
            this.spriteTabPage.Controls.Add( this.allSpritesEditor1 );
            this.spriteTabPage.Location = new System.Drawing.Point( 4, 22 );
            this.spriteTabPage.Name = "spriteTabPage";
            this.spriteTabPage.Padding = new System.Windows.Forms.Padding( 3 );
            this.spriteTabPage.Size = new System.Drawing.Size( 730, 637 );
            this.spriteTabPage.TabIndex = 0;
            this.spriteTabPage.Text = "Sprites";
            this.spriteTabPage.UseVisualStyleBackColor = true;
            // 
            // allSpritesEditor1
            // 
            this.allSpritesEditor1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.allSpritesEditor1.Enabled = false;
            this.allSpritesEditor1.Location = new System.Drawing.Point( 0, 0 );
            this.allSpritesEditor1.Name = "allSpritesEditor1";
            this.allSpritesEditor1.Size = new System.Drawing.Size( 730, 634 );
            this.allSpritesEditor1.TabIndex = 0;
            // 
            // otherTabPage
            // 
            this.otherTabPage.Controls.Add( this.allOtherImagesEditor1 );
            this.otherTabPage.Location = new System.Drawing.Point( 4, 22 );
            this.otherTabPage.Name = "otherTabPage";
            this.otherTabPage.Padding = new System.Windows.Forms.Padding( 3 );
            this.otherTabPage.Size = new System.Drawing.Size( 634, 637 );
            this.otherTabPage.TabIndex = 1;
            this.otherTabPage.Text = "Other Images";
            this.otherTabPage.UseVisualStyleBackColor = true;
            // 
            // allOtherImagesEditor1
            // 
            this.allOtherImagesEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allOtherImagesEditor1.Enabled = false;
            this.allOtherImagesEditor1.Location = new System.Drawing.Point( 3, 3 );
            this.allOtherImagesEditor1.Name = "allOtherImagesEditor1";
            this.allOtherImagesEditor1.Size = new System.Drawing.Size( 628, 631 );
            this.allOtherImagesEditor1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add( this.numericUpDown2 );
            this.tabPage1.Controls.Add( this.textBox1 );
            this.tabPage1.Controls.Add( this.listBox1 );
            this.tabPage1.Controls.Add( this.plus12Button );
            this.tabPage1.Controls.Add( this.plus4Button );
            this.tabPage1.Controls.Add( this.plus2Button );
            this.tabPage1.Controls.Add( this.comboBox1 );
            this.tabPage1.Controls.Add( this.plus16Button );
            this.tabPage1.Controls.Add( this.plus8Button );
            this.tabPage1.Controls.Add( this.plusOneButton );
            this.tabPage1.Controls.Add( this.numericUpDown1 );
            this.tabPage1.Controls.Add( this.groupBox1 );
            this.tabPage1.Controls.Add( this.pictureBox2 );
            this.tabPage1.Controls.Add( this.heightSpinner );
            this.tabPage1.Controls.Add( this.label4 );
            this.tabPage1.Controls.Add( this.widthSpinner );
            this.tabPage1.Controls.Add( this.label3 );
            this.tabPage1.Controls.Add( this.ySpinner );
            this.tabPage1.Controls.Add( this.label2 );
            this.tabPage1.Controls.Add( this.xSpinner );
            this.tabPage1.Controls.Add( this.label1 );
            this.tabPage1.Controls.Add( this.pictureBox1 );
            this.tabPage1.Location = new System.Drawing.Point( 4, 22 );
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage1.Size = new System.Drawing.Size( 730, 637 );
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point( 559, 467 );
            this.numericUpDown2.Maximum = new decimal( new int[] {
            16,
            0,
            0,
            0} );
            this.numericUpDown2.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size( 69, 20 );
            this.numericUpDown2.TabIndex = 20;
            this.numericUpDown2.Value = new decimal( new int[] {
            16,
            0,
            0,
            0} );
            this.numericUpDown2.ValueChanged += new System.EventHandler( this.numericUpDown2_ValueChanged );
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font( "Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
            this.textBox1.Location = new System.Drawing.Point( 300, 491 );
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size( 424, 137 );
            this.textBox1.TabIndex = 19;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point( 6, 302 );
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size( 338, 121 );
            this.listBox1.TabIndex = 18;
            this.listBox1.SelectedIndexChanged += new System.EventHandler( this.listBox1_SelectedIndexChanged );
            // 
            // plus12Button
            // 
            this.plus12Button.Location = new System.Drawing.Point( 218, 517 );
            this.plus12Button.Name = "plus12Button";
            this.plus12Button.Size = new System.Drawing.Size( 76, 20 );
            this.plus12Button.TabIndex = 17;
            this.plus12Button.Text = "+0x0C";
            this.plus12Button.UseVisualStyleBackColor = true;
            this.plus12Button.Click += new System.EventHandler( this.plus12Button_Click );
            // 
            // plus4Button
            // 
            this.plus4Button.Location = new System.Drawing.Point( 136, 543 );
            this.plus4Button.Name = "plus4Button";
            this.plus4Button.Size = new System.Drawing.Size( 76, 20 );
            this.plus4Button.TabIndex = 16;
            this.plus4Button.Text = "+0x04";
            this.plus4Button.UseVisualStyleBackColor = true;
            this.plus4Button.Click += new System.EventHandler( this.plus4Button_Click );
            // 
            // plus2Button
            // 
            this.plus2Button.Location = new System.Drawing.Point( 137, 517 );
            this.plus2Button.Name = "plus2Button";
            this.plus2Button.Size = new System.Drawing.Size( 76, 20 );
            this.plus2Button.TabIndex = 15;
            this.plus2Button.Text = "+0x02";
            this.plus2Button.UseVisualStyleBackColor = true;
            this.plus2Button.Click += new System.EventHandler( this.plus2Button_Click );
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point( 12, 551 );
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size( 121, 21 );
            this.comboBox1.TabIndex = 14;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler( this.comboBox1_SelectedIndexChanged );
            // 
            // plus16Button
            // 
            this.plus16Button.Location = new System.Drawing.Point( 218, 543 );
            this.plus16Button.Name = "plus16Button";
            this.plus16Button.Size = new System.Drawing.Size( 76, 20 );
            this.plus16Button.TabIndex = 13;
            this.plus16Button.Text = "+0x10";
            this.plus16Button.UseVisualStyleBackColor = true;
            this.plus16Button.Click += new System.EventHandler( this.plus16Button_Click );
            // 
            // plus8Button
            // 
            this.plus8Button.Location = new System.Drawing.Point( 218, 491 );
            this.plus8Button.Name = "plus8Button";
            this.plus8Button.Size = new System.Drawing.Size( 76, 20 );
            this.plus8Button.TabIndex = 12;
            this.plus8Button.Text = "+0x08";
            this.plus8Button.UseVisualStyleBackColor = true;
            this.plus8Button.Click += new System.EventHandler( this.plus8Button_Click );
            // 
            // plusOneButton
            // 
            this.plusOneButton.Location = new System.Drawing.Point( 136, 491 );
            this.plusOneButton.Name = "plusOneButton";
            this.plusOneButton.Size = new System.Drawing.Size( 76, 20 );
            this.plusOneButton.TabIndex = 11;
            this.plusOneButton.Text = "+0x01";
            this.plusOneButton.UseVisualStyleBackColor = true;
            this.plusOneButton.Click += new System.EventHandler( this.plusOneButton_Click );
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Hexadecimal = true;
            this.numericUpDown1.Location = new System.Drawing.Point( 12, 525 );
            this.numericUpDown1.Maximum = new decimal( new int[] {
            -1,
            0,
            0,
            0} );
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size( 118, 20 );
            this.numericUpDown1.TabIndex = 10;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.ValueChanged += new System.EventHandler( this.numericUpDown1_ValueChanged );
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this._8bitRadioButton );
            this.groupBox1.Controls.Add( this._16BitRadioButton );
            this.groupBox1.Location = new System.Drawing.Point( 12, 439 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 75, 80 );
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // _8bitRadioButton
            // 
            this._8bitRadioButton.AutoSize = true;
            this._8bitRadioButton.Location = new System.Drawing.Point( 6, 42 );
            this._8bitRadioButton.Name = "_8bitRadioButton";
            this._8bitRadioButton.Size = new System.Drawing.Size( 45, 17 );
            this._8bitRadioButton.TabIndex = 11;
            this._8bitRadioButton.Text = "8-bit";
            this._8bitRadioButton.UseVisualStyleBackColor = true;
            this._8bitRadioButton.CheckedChanged += new System.EventHandler( this._8bitRadioButton_CheckedChanged );
            // 
            // _16BitRadioButton
            // 
            this._16BitRadioButton.AutoSize = true;
            this._16BitRadioButton.Checked = true;
            this._16BitRadioButton.Location = new System.Drawing.Point( 6, 19 );
            this._16BitRadioButton.Name = "_16BitRadioButton";
            this._16BitRadioButton.Size = new System.Drawing.Size( 51, 17 );
            this._16BitRadioButton.TabIndex = 10;
            this._16BitRadioButton.TabStop = true;
            this._16BitRadioButton.Text = "16-bit";
            this._16BitRadioButton.UseVisualStyleBackColor = true;
            this._16BitRadioButton.CheckedChanged += new System.EventHandler( this._16BitRadioButton_CheckedChanged );
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point( 6, 272 );
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size( 77, 24 );
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // heightSpinner
            // 
            this.heightSpinner.Hexadecimal = true;
            this.heightSpinner.Location = new System.Drawing.Point( 310, 465 );
            this.heightSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.heightSpinner.Name = "heightSpinner";
            this.heightSpinner.Size = new System.Drawing.Size( 120, 20 );
            this.heightSpinner.TabIndex = 3;
            this.heightSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.heightSpinner.ValueChanged += new System.EventHandler( this.spinner_ValueChanged );
            this.heightSpinner.Enter += new System.EventHandler( this.heightSpinner_Enter );
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point( 269, 467 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size( 38, 13 );
            this.label4.TabIndex = 7;
            this.label4.Text = "Height";
            // 
            // widthSpinner
            // 
            this.widthSpinner.Hexadecimal = true;
            this.widthSpinner.Location = new System.Drawing.Point( 310, 439 );
            this.widthSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.widthSpinner.Name = "widthSpinner";
            this.widthSpinner.Size = new System.Drawing.Size( 120, 20 );
            this.widthSpinner.TabIndex = 2;
            this.widthSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.widthSpinner.ValueChanged += new System.EventHandler( this.spinner_ValueChanged );
            this.widthSpinner.Enter += new System.EventHandler( this.heightSpinner_Enter );
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 269, 441 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 35, 13 );
            this.label3.TabIndex = 5;
            this.label3.Text = "Width";
            // 
            // ySpinner
            // 
            this.ySpinner.Hexadecimal = true;
            this.ySpinner.Location = new System.Drawing.Point( 137, 465 );
            this.ySpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.ySpinner.Name = "ySpinner";
            this.ySpinner.Size = new System.Drawing.Size( 120, 20 );
            this.ySpinner.TabIndex = 1;
            this.ySpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ySpinner.ValueChanged += new System.EventHandler( this.spinner_ValueChanged );
            this.ySpinner.Enter += new System.EventHandler( this.heightSpinner_Enter );
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 96, 467 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 14, 13 );
            this.label2.TabIndex = 3;
            this.label2.Text = "Y";
            // 
            // xSpinner
            // 
            this.xSpinner.Hexadecimal = true;
            this.xSpinner.Location = new System.Drawing.Point( 137, 439 );
            this.xSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.xSpinner.Name = "xSpinner";
            this.xSpinner.Size = new System.Drawing.Size( 120, 20 );
            this.xSpinner.TabIndex = 0;
            this.xSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.xSpinner.ValueChanged += new System.EventHandler( this.spinner_ValueChanged );
            this.xSpinner.Enter += new System.EventHandler( this.heightSpinner_Enter );
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 96, 441 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 14, 13 );
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point( 6, 6 );
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size( 338, 260 );
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point( 522, 98 );
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size( 70, 30 );
            this.progressBar1.TabIndex = 1;
            this.progressBar1.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 743, 687 );
            this.Controls.Add( this.progressBar1 );
            this.Controls.Add( this.tabControl1 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.MaximizeBox = false;
            this.Menu = mainMenu;
            this.MinimumSize = new System.Drawing.Size( 653, 713 );
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Shishi Sprite Manager";
            this.tabControl1.ResumeLayout( false );
            this.spriteTabPage.ResumeLayout( false );
            this.otherTabPage.ResumeLayout( false );
            this.tabPage1.ResumeLayout( false );
            this.tabPage1.PerformLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.numericUpDown2 ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.numericUpDown1 ) ).EndInit();
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox2 ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.heightSpinner ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.widthSpinner ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.ySpinner ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.xSpinner ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit();
            this.ResumeLayout( false );

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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage spriteTabPage;
        private System.Windows.Forms.TabPage otherTabPage;
        private FFTPatcher.SpriteEditor.AllOtherImagesEditor allOtherImagesEditor1;
        private System.Windows.Forms.MenuItem importImageMenuItem;
        private System.Windows.Forms.MenuItem exportImageMenuItem;
        private System.Windows.Forms.MenuItem importAllImagesMenuItem;
        private System.Windows.Forms.MenuItem dumpAllImagesMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.MenuItem imageMenuItem;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.NumericUpDown heightSpinner;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown widthSpinner;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown ySpinner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown xSpinner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button plus16Button;
        private System.Windows.Forms.Button plus8Button;
        private System.Windows.Forms.Button plusOneButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton _8bitRadioButton;
        private System.Windows.Forms.RadioButton _16BitRadioButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button plus12Button;
        private System.Windows.Forms.Button plus4Button;
        private System.Windows.Forms.Button plus2Button;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.TextBox textBox1;
    }
}

