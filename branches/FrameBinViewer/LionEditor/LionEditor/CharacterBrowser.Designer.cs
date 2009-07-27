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
    partial class CharacterBrowser
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
            System.Windows.Forms.SplitContainer splitContainer;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterBrowser));
            this.gameSelector = new System.Windows.Forms.ComboBox();
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.openButton = new System.Windows.Forms.ToolBarButton();
            this.toolBarIcons = new System.Windows.Forms.ImageList(this.components);
            this.characterCollectionEditor = new LionEditor.CharacterCollectionEditor();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
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
            splitContainer.Panel2.Controls.Add(this.characterCollectionEditor);
            splitContainer.Size = new System.Drawing.Size(732, 455);
            splitContainer.SplitterDistance = 46;
            splitContainer.SplitterWidth = 1;
            splitContainer.TabIndex = 1;
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
            this.openButton});
            this.toolBar.ButtonSize = new System.Drawing.Size(32, 32);
            this.toolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.toolBarIcons;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(732, 44);
            this.toolBar.TabIndex = 0;
            this.toolBar.Wrappable = false;
            // 
            // openButton
            // 
            this.openButton.ImageIndex = 0;
            this.openButton.Name = "openButton";
            this.openButton.ToolTipText = "Open...";
            // 
            // toolBarIcons
            // 
            this.toolBarIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolBarIcons.ImageStream")));
            this.toolBarIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.toolBarIcons.Images.SetKeyName(0, "open-32x32.png");
            // 
            // characterCollectionEditor
            // 
            this.characterCollectionEditor.CharacterCollection = null;
            this.characterCollectionEditor.CharacterEditorEnabled = false;
            this.characterCollectionEditor.ContextMenuEnabled = true;
            this.characterCollectionEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.characterCollectionEditor.Location = new System.Drawing.Point(0, 0);
            this.characterCollectionEditor.Name = "characterCollectionEditor";
            this.characterCollectionEditor.Size = new System.Drawing.Size(732, 408);
            this.characterCollectionEditor.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "*.SYS;*.GME;*.bin";
            this.openFileDialog.Filter = "All Valid Files|lioneditor.bin;FFTA.SYS;*.gme|War of the Lions Files (lioneditor." +
                "bin; FFTA.SYS)|lioneditor.bin;FFTA.SYS|Dex Drive Files|*.gme";
            this.openFileDialog.ReadOnlyChecked = true;
            // 
            // CharacterBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 455);
            this.Controls.Add(splitContainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CharacterBrowser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "CharacterBrowser";
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel1.PerformLayout();
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox gameSelector;
        private System.Windows.Forms.ToolBar toolBar;
        private System.Windows.Forms.ToolBarButton openButton;
        private System.Windows.Forms.ImageList toolBarIcons;
        private CharacterCollectionEditor characterCollectionEditor;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}