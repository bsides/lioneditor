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
    partial class CharacterCollectionEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.SplitContainer splitContainer;
            this.characterSelector = new System.Windows.Forms.CheckedListBox();
            this.characterEditor = new LionEditor.CharacterEditor();
            this.characterRightClickMenu = new System.Windows.Forms.ContextMenu();
            this.characterCopyMenuItem = new System.Windows.Forms.MenuItem();
            this.characterPasteMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.characterMoveUpMenuItem = new System.Windows.Forms.MenuItem();
            this.characterMoveDownMenuItem = new System.Windows.Forms.MenuItem();
            splitContainer = new System.Windows.Forms.SplitContainer();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.IsSplitterFixed = true;
            splitContainer.Location = new System.Drawing.Point(0, 0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(this.characterSelector);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(this.characterEditor);
            splitContainer.Size = new System.Drawing.Size(782, 439);
            splitContainer.SplitterDistance = 142;
            splitContainer.TabIndex = 1;
            splitContainer.TabStop = false;
            // 
            // characterSelector
            // 
            this.characterSelector.AllowDrop = true;
            this.characterSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.characterSelector.FormattingEnabled = true;
            this.characterSelector.Location = new System.Drawing.Point(0, 0);
            this.characterSelector.Name = "characterSelector";
            this.characterSelector.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.characterSelector.Size = new System.Drawing.Size(142, 439);
            this.characterSelector.TabIndex = 0;
            // 
            // characterEditor
            // 
            this.characterEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.characterEditor.Location = new System.Drawing.Point(0, 0);
            this.characterEditor.Name = "characterEditor";
            this.characterEditor.Size = new System.Drawing.Size(636, 439);
            this.characterEditor.TabIndex = 0;
            // 
            // characterRightClickMenu
            // 
            this.characterRightClickMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.characterCopyMenuItem,
            this.characterPasteMenuItem,
            this.menuItem3,
            this.characterMoveUpMenuItem,
            this.characterMoveDownMenuItem});
            // 
            // characterCopyMenuItem
            // 
            this.characterCopyMenuItem.Index = 0;
            this.characterCopyMenuItem.Text = "&Copy";
            // 
            // characterPasteMenuItem
            // 
            this.characterPasteMenuItem.Enabled = false;
            this.characterPasteMenuItem.Index = 1;
            this.characterPasteMenuItem.Text = "P&aste";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "-";
            // 
            // characterMoveUpMenuItem
            // 
            this.characterMoveUpMenuItem.Index = 3;
            this.characterMoveUpMenuItem.Text = "Move &up";
            // 
            // characterMoveDownMenuItem
            // 
            this.characterMoveDownMenuItem.Index = 4;
            this.characterMoveDownMenuItem.Text = "Move &down";
            // 
            // CharacterCollectionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(splitContainer);
            this.Name = "CharacterCollectionEditor";
            this.Size = new System.Drawing.Size(782, 439);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox characterSelector;
        private CharacterEditor characterEditor;
        private System.Windows.Forms.ContextMenu characterRightClickMenu;
        private System.Windows.Forms.MenuItem characterCopyMenuItem;
        private System.Windows.Forms.MenuItem characterPasteMenuItem;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem characterMoveUpMenuItem;
        private System.Windows.Forms.MenuItem characterMoveDownMenuItem;
    }
}
