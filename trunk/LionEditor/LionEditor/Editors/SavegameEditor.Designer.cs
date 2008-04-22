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
            System.Windows.Forms.Label poachersDenLabel;
            System.Windows.Forms.Label inventoryLabel;
            System.Windows.Forms.TableLayoutPanel inventoryTable;
            this.inventoryEditor = new LionEditor.InventoryEditor();
            this.poachersDenEditor = new LionEditor.InventoryEditor();
            this.optionsTab = new System.Windows.Forms.TabPage();
            this.optionsEditor = new LionEditor.OptionsEditor();
            this.inventoryTab = new System.Windows.Forms.TabPage();
            this.chronicleTab = new System.Windows.Forms.TabPage();
            this.chronicleEditor = new LionEditor.ChronicleEditor();
            this.charactersTab = new System.Windows.Forms.TabPage();
            this.characterCollectionEditor = new LionEditor.CharacterCollectionEditor();
            this.tabControl = new System.Windows.Forms.TabControl();
            poachersDenLabel = new System.Windows.Forms.Label();
            inventoryLabel = new System.Windows.Forms.Label();
            inventoryTable = new System.Windows.Forms.TableLayoutPanel();
            inventoryTable.SuspendLayout();
            this.optionsTab.SuspendLayout();
            this.inventoryTab.SuspendLayout();
            this.chronicleTab.SuspendLayout();
            this.charactersTab.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // poachersDenLabel
            // 
            poachersDenLabel.AutoSize = true;
            poachersDenLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            poachersDenLabel.Location = new System.Drawing.Point(361, 0);
            poachersDenLabel.Name = "poachersDenLabel";
            poachersDenLabel.Size = new System.Drawing.Size(353, 24);
            poachersDenLabel.TabIndex = 3;
            poachersDenLabel.Text = "Poacher\'s Den";
            poachersDenLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // inventoryLabel
            // 
            inventoryLabel.AutoSize = true;
            inventoryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            inventoryLabel.Location = new System.Drawing.Point(3, 0);
            inventoryLabel.Name = "inventoryLabel";
            inventoryLabel.Size = new System.Drawing.Size(352, 24);
            inventoryLabel.TabIndex = 2;
            inventoryLabel.Text = "Inventory";
            inventoryLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // inventoryTable
            // 
            inventoryTable.ColumnCount = 2;
            inventoryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            inventoryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            inventoryTable.Controls.Add(this.inventoryEditor, 0, 1);
            inventoryTable.Controls.Add(this.poachersDenEditor, 1, 1);
            inventoryTable.Controls.Add(inventoryLabel, 0, 0);
            inventoryTable.Controls.Add(poachersDenLabel, 1, 0);
            inventoryTable.Dock = System.Windows.Forms.DockStyle.Fill;
            inventoryTable.Location = new System.Drawing.Point(0, 0);
            inventoryTable.Margin = new System.Windows.Forms.Padding(0);
            inventoryTable.Name = "inventoryTable";
            inventoryTable.RowCount = 2;
            inventoryTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.405406F));
            inventoryTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.5946F));
            inventoryTable.Size = new System.Drawing.Size(717, 450);
            inventoryTable.TabIndex = 1;
            // 
            // inventoryEditor
            // 
            this.inventoryEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inventoryEditor.Inventory = null;
            this.inventoryEditor.Location = new System.Drawing.Point(1, 25);
            this.inventoryEditor.Margin = new System.Windows.Forms.Padding(1);
            this.inventoryEditor.Name = "inventoryEditor";
            this.inventoryEditor.Size = new System.Drawing.Size(356, 424);
            this.inventoryEditor.TabIndex = 0;
            // 
            // poachersDenEditor
            // 
            this.poachersDenEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.poachersDenEditor.Inventory = null;
            this.poachersDenEditor.Location = new System.Drawing.Point(359, 25);
            this.poachersDenEditor.Margin = new System.Windows.Forms.Padding(1);
            this.poachersDenEditor.Name = "poachersDenEditor";
            this.poachersDenEditor.Size = new System.Drawing.Size(357, 424);
            this.poachersDenEditor.TabIndex = 1;
            // 
            // optionsTab
            // 
            this.optionsTab.Controls.Add(this.optionsEditor);
            this.optionsTab.Location = new System.Drawing.Point(4, 22);
            this.optionsTab.Name = "optionsTab";
            this.optionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.optionsTab.Size = new System.Drawing.Size(717, 450);
            this.optionsTab.TabIndex = 3;
            this.optionsTab.Text = "Options";
            this.optionsTab.UseVisualStyleBackColor = true;
            // 
            // optionsEditor
            // 
            this.optionsEditor.Dock = System.Windows.Forms.DockStyle.Left;
            this.optionsEditor.Location = new System.Drawing.Point(3, 3);
            this.optionsEditor.Name = "optionsEditor";
            this.optionsEditor.Options = null;
            this.optionsEditor.Size = new System.Drawing.Size(460, 444);
            this.optionsEditor.TabIndex = 0;
            // 
            // inventoryTab
            // 
            this.inventoryTab.Controls.Add(inventoryTable);
            this.inventoryTab.Location = new System.Drawing.Point(4, 22);
            this.inventoryTab.Name = "inventoryTab";
            this.inventoryTab.Size = new System.Drawing.Size(717, 450);
            this.inventoryTab.TabIndex = 2;
            this.inventoryTab.Text = "Inventory";
            this.inventoryTab.UseVisualStyleBackColor = true;
            // 
            // chronicleTab
            // 
            this.chronicleTab.Controls.Add(this.chronicleEditor);
            this.chronicleTab.Location = new System.Drawing.Point(4, 22);
            this.chronicleTab.Name = "chronicleTab";
            this.chronicleTab.Padding = new System.Windows.Forms.Padding(3);
            this.chronicleTab.Size = new System.Drawing.Size(717, 450);
            this.chronicleTab.TabIndex = 1;
            this.chronicleTab.Text = "Chronicle";
            this.chronicleTab.UseVisualStyleBackColor = true;
            // 
            // chronicleEditor
            // 
            this.chronicleEditor.Artefacts = null;
            this.chronicleEditor.Casualties = ((uint)(0u));
            this.chronicleEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chronicleEditor.Feats = null;
            this.chronicleEditor.Kills = ((uint)(0u));
            this.chronicleEditor.Location = new System.Drawing.Point(3, 3);
            this.chronicleEditor.Name = "chronicleEditor";
            this.chronicleEditor.Size = new System.Drawing.Size(711, 444);
            this.chronicleEditor.TabIndex = 0;
            this.chronicleEditor.Timer = ((uint)(0u));
            this.chronicleEditor.WarFunds = ((uint)(0u));
            this.chronicleEditor.Wonders = null;
            // 
            // charactersTab
            // 
            this.charactersTab.Controls.Add(this.characterCollectionEditor);
            this.charactersTab.Location = new System.Drawing.Point(4, 22);
            this.charactersTab.Name = "charactersTab";
            this.charactersTab.Padding = new System.Windows.Forms.Padding(3);
            this.charactersTab.Size = new System.Drawing.Size(717, 450);
            this.charactersTab.TabIndex = 0;
            this.charactersTab.Text = "Characters";
            this.charactersTab.UseVisualStyleBackColor = true;
            // 
            // characterCollectionEditor
            // 
            this.characterCollectionEditor.CharacterCollection = null;
            this.characterCollectionEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.characterCollectionEditor.Location = new System.Drawing.Point(3, 3);
            this.characterCollectionEditor.Name = "characterCollectionEditor";
            this.characterCollectionEditor.Size = new System.Drawing.Size(711, 444);
            this.characterCollectionEditor.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.charactersTab);
            this.tabControl.Controls.Add(this.chronicleTab);
            this.tabControl.Controls.Add(this.inventoryTab);
            this.tabControl.Controls.Add(this.optionsTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(725, 476);
            this.tabControl.TabIndex = 1;
            // 
            // SavegameEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "SavegameEditor";
            this.Size = new System.Drawing.Size(725, 476);
            inventoryTable.ResumeLayout(false);
            inventoryTable.PerformLayout();
            this.optionsTab.ResumeLayout(false);
            this.inventoryTab.ResumeLayout(false);
            this.chronicleTab.ResumeLayout(false);
            this.charactersTab.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage optionsTab;
        private LionEditor.OptionsEditor optionsEditor;
        private System.Windows.Forms.TabPage inventoryTab;
        private InventoryEditor inventoryEditor;
        private InventoryEditor poachersDenEditor;
        private System.Windows.Forms.TabPage chronicleTab;
        private ChronicleEditor chronicleEditor;
        private System.Windows.Forms.TabPage charactersTab;
        private System.Windows.Forms.TabControl tabControl;
        private LionEditor.CharacterCollectionEditor characterCollectionEditor;

    }
}
