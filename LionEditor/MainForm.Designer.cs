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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
            this.mainMenu = new System.Windows.Forms.MainMenu( this.components );
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.openButton = new System.Windows.Forms.ToolBarButton();
            this.saveButton = new System.Windows.Forms.ToolBarButton();
            this.toolBarIcons = new System.Windows.Forms.ImageList( this.components );
            this.savegameEditor1 = new LionEditor.SavegameEditor();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3} );
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "File";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "Edit";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "Help";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point( 0, 0 );
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add( this.comboBox1 );
            this.splitContainer1.Panel1.Controls.Add( this.toolBar1 );
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add( this.savegameEditor1 );
            this.splitContainer1.Size = new System.Drawing.Size( 738, 527 );
            this.splitContainer1.SplitterDistance = 46;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point( 149, 13 );
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size( 303, 21 );
            this.comboBox1.TabIndex = 1;
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange( new System.Windows.Forms.ToolBarButton[] {
            this.openButton,
            this.saveButton} );
            this.toolBar1.ButtonSize = new System.Drawing.Size( 32, 32 );
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.toolBarIcons;
            this.toolBar1.Location = new System.Drawing.Point( 0, 0 );
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size( 738, 44 );
            this.toolBar1.TabIndex = 0;
            this.toolBar1.Wrappable = false;
            // 
            // openButton
            // 
            this.openButton.ImageIndex = 3;
            this.openButton.Name = "openButton";
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.ImageIndex = 0;
            this.saveButton.Name = "saveButton";
            // 
            // toolBarIcons
            // 
            this.toolBarIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject( "toolBarIcons.ImageStream" )));
            this.toolBarIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.toolBarIcons.Images.SetKeyName( 0, "save-32x32.png" );
            this.toolBarIcons.Images.SetKeyName( 1, "copy-32x32.png" );
            this.toolBarIcons.Images.SetKeyName( 2, "cut-32x32.png" );
            this.toolBarIcons.Images.SetKeyName( 3, "open-32x32.png" );
            // 
            // savegameEditor1
            // 
            this.savegameEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.savegameEditor1.Location = new System.Drawing.Point( 0, 0 );
            this.savegameEditor1.Name = "savegameEditor1";
            this.savegameEditor1.Size = new System.Drawing.Size( 738, 480 );
            this.savegameEditor1.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "*.SYS";
            this.openFileDialog.Filter = "FFTA.SYS|FFTA.SYS";
            this.openFileDialog.Title = "Open file...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 738, 527 );
            this.Controls.Add( this.splitContainer1 );
            this.MaximumSize = new System.Drawing.Size( 746, 554 );
            this.Menu = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size( 746, 554 );
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout( false );
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout( false );
            this.splitContainer1.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private SavegameEditor savegameEditor1;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ImageList toolBarIcons;
        private System.Windows.Forms.ToolBarButton saveButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolBarButton openButton;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

