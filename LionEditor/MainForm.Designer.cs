/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of LionEditor.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace LionEditor
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
            System.Windows.Forms.SplitContainer splitContainer;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gameSelector = new System.Windows.Forms.ComboBox();
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.openButton = new System.Windows.Forms.ToolBarButton();
            this.saveButton = new System.Windows.Forms.ToolBarButton();
            this.toolBarIcons = new System.Windows.Forms.ImageList(this.components);
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.fileMenu = new System.Windows.Forms.MenuItem();
            this.openMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.editMenu = new System.Windows.Forms.MenuItem();
            this.importCharactersMenuItem = new System.Windows.Forms.MenuItem();
            this.helpMenu = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.savegameEditor = new LionEditor.SavegameEditor();
            splitContainer = new System.Windows.Forms.SplitContainer();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer.IsSplitterFixed = true;
            splitContainer.Location = new System.Drawing.Point(0, 0);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(this.gameSelector);
            splitContainer.Panel1.Controls.Add(this.toolBar);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(this.savegameEditor);
            splitContainer.Size = new System.Drawing.Size(738, 527);
            splitContainer.SplitterDistance = 46;
            splitContainer.SplitterWidth = 1;
            splitContainer.TabIndex = 0;
            splitContainer.TabStop = false;
            // 
            // gameSelector
            // 
            this.gameSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameSelector.Enabled = false;
            this.gameSelector.FormattingEnabled = true;
            this.gameSelector.Location = new System.Drawing.Point(149, 13);
            this.gameSelector.Name = "gameSelector";
            this.gameSelector.Size = new System.Drawing.Size(303, 21);
            this.gameSelector.TabIndex = 1;
            // 
            // toolBar
            // 
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.openButton,
            this.saveButton});
            this.toolBar.ButtonSize = new System.Drawing.Size(32, 32);
            this.toolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.toolBarIcons;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(738, 44);
            this.toolBar.TabIndex = 0;
            this.toolBar.Wrappable = false;
            // 
            // openButton
            // 
            this.openButton.ImageIndex = 3;
            this.openButton.Name = "openButton";
            this.openButton.ToolTipText = "Open...";
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.ImageIndex = 0;
            this.saveButton.Name = "saveButton";
            this.saveButton.ToolTipText = "Save";
            // 
            // toolBarIcons
            // 
            this.toolBarIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolBarIcons.ImageStream")));
            this.toolBarIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.toolBarIcons.Images.SetKeyName(0, "save-32x32.png");
            this.toolBarIcons.Images.SetKeyName(1, "copy-32x32.png");
            this.toolBarIcons.Images.SetKeyName(2, "cut-32x32.png");
            this.toolBarIcons.Images.SetKeyName(3, "open-32x32.png");
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenu,
            this.editMenu,
            this.helpMenu});
            // 
            // fileMenu
            // 
            this.fileMenu.Index = 0;
            this.fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.openMenuItem,
            this.saveMenuItem,
            this.saveAsMenuItem,
            this.menuItem4,
            this.exitMenuItem});
            this.fileMenu.Text = "&File";
            // 
            // openMenuItem
            // 
            this.openMenuItem.Index = 0;
            this.openMenuItem.Text = "&Open...";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Enabled = false;
            this.saveMenuItem.Index = 1;
            this.saveMenuItem.Text = "&Save";
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Enabled = false;
            this.saveAsMenuItem.Index = 2;
            this.saveAsMenuItem.Text = "Save &As...";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 4;
            this.exitMenuItem.Text = "E&xit";
            // 
            // editMenu
            // 
            this.editMenu.Index = 1;
            this.editMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.importCharactersMenuItem});
            this.editMenu.Text = "Edit";
            // 
            // importCharactersMenuItem
            // 
            this.importCharactersMenuItem.Index = 0;
            this.importCharactersMenuItem.Text = "Import characters...";
            // 
            // helpMenu
            // 
            this.helpMenu.Index = 2;
            this.helpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.aboutMenuItem});
            this.helpMenu.Text = "&Help";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 0;
            this.aboutMenuItem.Text = "&About...";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "*.SYS";
            this.openFileDialog.Filter = "War of the Lions Files|FFTA.SYS";
            this.openFileDialog.Title = "Open file...";
            // 
            // savegameEditor
            // 
            this.savegameEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.savegameEditor.Location = new System.Drawing.Point(0, 0);
            this.savegameEditor.Name = "savegameEditor";
            this.savegameEditor.Size = new System.Drawing.Size(738, 480);
            this.savegameEditor.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 527);
            this.Controls.Add(splitContainer);
            this.MaximumSize = new System.Drawing.Size(746, 554);
            this.Menu = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(746, 554);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Lion Editor";
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel1.PerformLayout();
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem fileMenu;
        private System.Windows.Forms.MenuItem editMenu;
        private System.Windows.Forms.MenuItem helpMenu;
        private SavegameEditor savegameEditor;
        private System.Windows.Forms.ToolBar toolBar;
        private System.Windows.Forms.ImageList toolBarIcons;
        private System.Windows.Forms.ToolBarButton saveButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolBarButton openButton;
        private System.Windows.Forms.ComboBox gameSelector;
        private System.Windows.Forms.MenuItem openMenuItem;
        private System.Windows.Forms.MenuItem saveMenuItem;
        private System.Windows.Forms.MenuItem saveAsMenuItem;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.MenuItem importCharactersMenuItem;
    }
}

