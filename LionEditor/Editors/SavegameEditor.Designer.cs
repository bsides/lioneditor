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
    partial class SavegameEditor
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.charactersTab = new System.Windows.Forms.TabPage();
            this.chronicleTab = new System.Windows.Forms.TabPage();
            this.chronicleEditor1 = new LionEditor.ChronicleEditor();
            this.inventoryTab = new System.Windows.Forms.TabPage();
            this.optionsTab = new System.Windows.Forms.TabPage();
            this.optionsEditor1 = new LionEditor.Editors.OptionsEditor();
            splitContainer = new System.Windows.Forms.SplitContainer();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.charactersTab.SuspendLayout();
            this.chronicleTab.SuspendLayout();
            this.optionsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.IsSplitterFixed = true;
            splitContainer.Location = new System.Drawing.Point( 3, 3 );
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add( this.characterSelector );
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add( this.characterEditor );
            splitContainer.Size = new System.Drawing.Size( 711, 444 );
            splitContainer.SplitterDistance = 130;
            splitContainer.TabIndex = 0;
            splitContainer.TabStop = false;
            // 
            // characterSelector
            // 
            this.characterSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.characterSelector.FormattingEnabled = true;
            this.characterSelector.Location = new System.Drawing.Point( 0, 0 );
            this.characterSelector.Name = "characterSelector";
            this.characterSelector.Size = new System.Drawing.Size( 130, 439 );
            this.characterSelector.TabIndex = 0;
            // 
            // characterEditor
            // 
            this.characterEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.characterEditor.Location = new System.Drawing.Point( 0, 0 );
            this.characterEditor.Name = "characterEditor";
            this.characterEditor.Size = new System.Drawing.Size( 577, 444 );
            this.characterEditor.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add( this.charactersTab );
            this.tabControl.Controls.Add( this.chronicleTab );
            this.tabControl.Controls.Add( this.inventoryTab );
            this.tabControl.Controls.Add( this.optionsTab );
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point( 0, 0 );
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size( 725, 476 );
            this.tabControl.TabIndex = 1;
            // 
            // charactersTab
            // 
            this.charactersTab.Controls.Add( splitContainer );
            this.charactersTab.Location = new System.Drawing.Point( 4, 22 );
            this.charactersTab.Name = "charactersTab";
            this.charactersTab.Padding = new System.Windows.Forms.Padding( 3 );
            this.charactersTab.Size = new System.Drawing.Size( 717, 450 );
            this.charactersTab.TabIndex = 0;
            this.charactersTab.Text = "Characters";
            this.charactersTab.UseVisualStyleBackColor = true;
            // 
            // chronicleTab
            // 
            this.chronicleTab.Controls.Add( this.chronicleEditor1 );
            this.chronicleTab.Location = new System.Drawing.Point( 4, 22 );
            this.chronicleTab.Name = "chronicleTab";
            this.chronicleTab.Padding = new System.Windows.Forms.Padding( 3 );
            this.chronicleTab.Size = new System.Drawing.Size( 717, 450 );
            this.chronicleTab.TabIndex = 1;
            this.chronicleTab.Text = "Chronicle";
            this.chronicleTab.UseVisualStyleBackColor = true;
            // 
            // chronicleEditor1
            // 
            this.chronicleEditor1.Artefacts = null;
            this.chronicleEditor1.Casualties = ((uint)(0u));
            this.chronicleEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chronicleEditor1.Feats = null;
            this.chronicleEditor1.Kills = ((uint)(0u));
            this.chronicleEditor1.Location = new System.Drawing.Point( 3, 3 );
            this.chronicleEditor1.Name = "chronicleEditor1";
            this.chronicleEditor1.Size = new System.Drawing.Size( 711, 444 );
            this.chronicleEditor1.TabIndex = 0;
            this.chronicleEditor1.Timer = ((uint)(0u));
            this.chronicleEditor1.Wonders = null;
            // 
            // inventoryTab
            // 
            this.inventoryTab.Location = new System.Drawing.Point( 4, 22 );
            this.inventoryTab.Name = "inventoryTab";
            this.inventoryTab.Padding = new System.Windows.Forms.Padding( 3 );
            this.inventoryTab.Size = new System.Drawing.Size( 717, 450 );
            this.inventoryTab.TabIndex = 2;
            this.inventoryTab.Text = "Inventory";
            this.inventoryTab.UseVisualStyleBackColor = true;
            // 
            // optionsTab
            // 
            this.optionsTab.Controls.Add( this.optionsEditor1 );
            this.optionsTab.Location = new System.Drawing.Point( 4, 22 );
            this.optionsTab.Name = "optionsTab";
            this.optionsTab.Padding = new System.Windows.Forms.Padding( 3 );
            this.optionsTab.Size = new System.Drawing.Size( 717, 450 );
            this.optionsTab.TabIndex = 3;
            this.optionsTab.Text = "Options";
            this.optionsTab.UseVisualStyleBackColor = true;
            // 
            // optionsEditor1
            // 
            this.optionsEditor1.Dock = System.Windows.Forms.DockStyle.Left;
            this.optionsEditor1.Location = new System.Drawing.Point( 3, 3 );
            this.optionsEditor1.Name = "optionsEditor1";
            this.optionsEditor1.Options = null;
            this.optionsEditor1.Size = new System.Drawing.Size( 460, 444 );
            this.optionsEditor1.TabIndex = 0;
            // 
            // SavegameEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.tabControl );
            this.Name = "SavegameEditor";
            this.Size = new System.Drawing.Size( 725, 476 );
            splitContainer.Panel1.ResumeLayout( false );
            splitContainer.Panel2.ResumeLayout( false );
            splitContainer.ResumeLayout( false );
            this.tabControl.ResumeLayout( false );
            this.charactersTab.ResumeLayout( false );
            this.chronicleTab.ResumeLayout( false );
            this.optionsTab.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TabPage charactersTab;
        private System.Windows.Forms.CheckedListBox characterSelector;
        private CharacterEditor characterEditor;
        private System.Windows.Forms.TabPage chronicleTab;
        private System.Windows.Forms.TabPage inventoryTab;
        private System.Windows.Forms.TabControl tabControl;
        private LionEditor.ChronicleEditor chronicleEditor1;
        private System.Windows.Forms.TabPage optionsTab;
        private LionEditor.Editors.OptionsEditor optionsEditor1;
    }
}
