/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

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

namespace FFTPatcher.Grids
{
    partial class AbilitiesDataGridView
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JPCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LearnRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LearnWithJP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LearnOnHit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Blank1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AbilityType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.aiHP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiMP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiCancelStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiAddStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiStats = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiUnequip = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiTargetEnemies = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiTargetAllies = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiIgnoreRange = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiReflectable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiUndeadReverse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiUnknown1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiRandomHits = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiUnknown2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiUnknown3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiSilence = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiBlank = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiDirectAttack = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiLineAttack = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiVerticalIncrease = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiTripleAttack = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiTripleBracelet = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiMagicDefenseUp = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.aiDefenseUp = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Unknown1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Unknown2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Unknown3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Blank2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Blank3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Blank4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Blank5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Unknown4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.NameColumn,
            this.JPCost,
            this.LearnRate,
            this.LearnWithJP,
            this.Action,
            this.LearnOnHit,
            this.Blank1,
            this.AbilityType,
            this.aiHP,
            this.aiMP,
            this.aiCancelStatus,
            this.aiAddStatus,
            this.aiStats,
            this.aiUnequip,
            this.aiTargetEnemies,
            this.aiTargetAllies,
            this.aiIgnoreRange,
            this.aiReflectable,
            this.aiUndeadReverse,
            this.aiUnknown1,
            this.aiRandomHits,
            this.aiUnknown2,
            this.aiUnknown3,
            this.aiSilence,
            this.aiBlank,
            this.aiDirectAttack,
            this.aiLineAttack,
            this.aiVerticalIncrease,
            this.aiTripleAttack,
            this.aiTripleBracelet,
            this.aiMagicDefenseUp,
            this.aiDefenseUp,
            this.Unknown1,
            this.Unknown2,
            this.Unknown3,
            this.Blank2,
            this.Blank3,
            this.Blank4,
            this.Blank5,
            this.Unknown4});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(675, 488);
            this.dataGridView1.TabIndex = 3;
            // 
            // Index
            // 
            this.Index.DataPropertyName = "Index";
            this.Index.Frozen = true;
            this.Index.HeaderText = "";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.Width = 5;
            // 
            // Name
            // 
            this.NameColumn.DataPropertyName = "Name";
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.Name = "Name";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Width = 5;
            // 
            // JPCost
            // 
            this.JPCost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.JPCost.DataPropertyName = "JPCost";
            this.JPCost.HeaderText = "JP Cost";
            this.JPCost.Name = "JPCost";
            this.JPCost.Width = 5;
            // 
            // LearnRate
            // 
            this.LearnRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.LearnRate.DataPropertyName = "LearnRate";
            this.LearnRate.HeaderText = "Learn Rate";
            this.LearnRate.Name = "LearnRate";
            this.LearnRate.Width = 5;
            // 
            // LearnWithJP
            // 
            this.LearnWithJP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.LearnWithJP.DataPropertyName = "LearnWithJP";
            this.LearnWithJP.HeaderText = "Learn with JP";
            this.LearnWithJP.Name = "LearnWithJP";
            this.LearnWithJP.Width = 5;
            // 
            // Action
            // 
            this.Action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Action.DataPropertyName = "Action";
            this.Action.HeaderText = "Action";
            this.Action.Name = "Action";
            this.Action.Width = 5;
            // 
            // LearnOnHit
            // 
            this.LearnOnHit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.LearnOnHit.DataPropertyName = "LearnOnHit";
            this.LearnOnHit.HeaderText = "Learn on Hit";
            this.LearnOnHit.Name = "LearnOnHit";
            this.LearnOnHit.Width = 5;
            // 
            // Blank1
            // 
            this.Blank1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Blank1.DataPropertyName = "Blank1";
            this.Blank1.HeaderText = "Blank";
            this.Blank1.Name = "Blank1";
            this.Blank1.Width = 5;
            // 
            // AbilityType
            // 
            this.AbilityType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.AbilityType.DataPropertyName = "AbilityType";
            this.AbilityType.HeaderText = "Ability Type";
            this.AbilityType.Name = "AbilityType";
            this.AbilityType.Width = 5;
            // 
            // aiHP
            // 
            this.aiHP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiHP.DataPropertyName = "AIHP";
            this.aiHP.HeaderText = "AI: HP";
            this.aiHP.Name = "aiHP";
            this.aiHP.Width = 5;
            // 
            // aiMP
            // 
            this.aiMP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiMP.DataPropertyName = "AIMP";
            this.aiMP.HeaderText = "AI: MP";
            this.aiMP.Name = "aiMP";
            this.aiMP.Width = 5;
            // 
            // aiCancelStatus
            // 
            this.aiCancelStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiCancelStatus.DataPropertyName = "AICancelStatus";
            this.aiCancelStatus.HeaderText = "AI: CancelStatus";
            this.aiCancelStatus.Name = "aiCancelStatus";
            this.aiCancelStatus.Width = 5;
            // 
            // aiAddStatus
            // 
            this.aiAddStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiAddStatus.DataPropertyName = "AIAddStatus";
            this.aiAddStatus.HeaderText = "AI: Add Status";
            this.aiAddStatus.Name = "aiAddStatus";
            this.aiAddStatus.Width = 5;
            // 
            // aiStats
            // 
            this.aiStats.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiStats.DataPropertyName = "AIStats";
            this.aiStats.HeaderText = "AI: Stats";
            this.aiStats.Name = "aiStats";
            this.aiStats.Width = 5;
            // 
            // aiUnequip
            // 
            this.aiUnequip.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiUnequip.DataPropertyName = "AIUnequip";
            this.aiUnequip.HeaderText = "AI: Unequip";
            this.aiUnequip.Name = "aiUnequip";
            this.aiUnequip.Width = 5;
            // 
            // aiTargetEnemies
            // 
            this.aiTargetEnemies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiTargetEnemies.DataPropertyName = "AITargetEnemies";
            this.aiTargetEnemies.HeaderText = "AI: Target Enemies";
            this.aiTargetEnemies.Name = "aiTargetEnemies";
            this.aiTargetEnemies.Width = 5;
            // 
            // aiTargetAllies
            // 
            this.aiTargetAllies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiTargetAllies.DataPropertyName = "AITargetAllies";
            this.aiTargetAllies.HeaderText = "AI: Target Allies";
            this.aiTargetAllies.Name = "aiTargetAllies";
            this.aiTargetAllies.Width = 5;
            // 
            // aiIgnoreRange
            // 
            this.aiIgnoreRange.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiIgnoreRange.DataPropertyName = "AIIgnoreRange";
            this.aiIgnoreRange.HeaderText = "AI: Ignore Range";
            this.aiIgnoreRange.Name = "aiIgnoreRange";
            this.aiIgnoreRange.Width = 5;
            // 
            // aiReflectable
            // 
            this.aiReflectable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiReflectable.DataPropertyName = "AIReflectable";
            this.aiReflectable.HeaderText = "AI: Reflectable";
            this.aiReflectable.Name = "aiReflectable";
            this.aiReflectable.Width = 5;
            // 
            // aiUndeadReverse
            // 
            this.aiUndeadReverse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiUndeadReverse.DataPropertyName = "AIUndeadReverse";
            this.aiUndeadReverse.HeaderText = "AI: Undead Reverse";
            this.aiUndeadReverse.Name = "aiUndeadReverse";
            this.aiUndeadReverse.Width = 5;
            // 
            // aiUnknown1
            // 
            this.aiUnknown1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiUnknown1.DataPropertyName = "AIUnknown1";
            this.aiUnknown1.HeaderText = "AI: Unknown 1";
            this.aiUnknown1.Name = "aiUnknown1";
            this.aiUnknown1.Width = 5;
            // 
            // aiRandomHits
            // 
            this.aiRandomHits.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiRandomHits.DataPropertyName = "AIRandomHits";
            this.aiRandomHits.HeaderText = "AI: Random Hits";
            this.aiRandomHits.Name = "aiRandomHits";
            this.aiRandomHits.Width = 5;
            // 
            // aiUnknown2
            // 
            this.aiUnknown2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiUnknown2.DataPropertyName = "AIUnknown2";
            this.aiUnknown2.HeaderText = "AI: Unknown 2";
            this.aiUnknown2.Name = "aiUnknown2";
            this.aiUnknown2.Width = 5;
            // 
            // aiUnknown3
            // 
            this.aiUnknown3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiUnknown3.DataPropertyName = "AIUnknown3";
            this.aiUnknown3.HeaderText = "AI: Unknown 3";
            this.aiUnknown3.Name = "aiUnknown3";
            this.aiUnknown3.Width = 5;
            // 
            // aiSilence
            // 
            this.aiSilence.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiSilence.DataPropertyName = "AISilence";
            this.aiSilence.HeaderText = "AI: Silence";
            this.aiSilence.Name = "aiSilence";
            this.aiSilence.Width = 5;
            // 
            // aiBlank
            // 
            this.aiBlank.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiBlank.DataPropertyName = "AIBlank";
            this.aiBlank.HeaderText = "AI: Blank";
            this.aiBlank.Name = "aiBlank";
            this.aiBlank.Width = 5;
            // 
            // aiDirectAttack
            // 
            this.aiDirectAttack.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiDirectAttack.DataPropertyName = "AIDirectAttack";
            this.aiDirectAttack.HeaderText = "AI: Direct Attack";
            this.aiDirectAttack.Name = "aiDirectAttack";
            this.aiDirectAttack.Width = 5;
            // 
            // aiLineAttack
            // 
            this.aiLineAttack.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiLineAttack.DataPropertyName = "AILineAttack";
            this.aiLineAttack.HeaderText = "AI: Line Attack";
            this.aiLineAttack.Name = "aiLineAttack";
            this.aiLineAttack.Width = 5;
            // 
            // aiVerticalIncrease
            // 
            this.aiVerticalIncrease.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiVerticalIncrease.DataPropertyName = "AIVerticalIncrease";
            this.aiVerticalIncrease.HeaderText = "AI: Vertical Increase";
            this.aiVerticalIncrease.Name = "aiVerticalIncrease";
            this.aiVerticalIncrease.Width = 5;
            // 
            // aiTripleAttack
            // 
            this.aiTripleAttack.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiTripleAttack.DataPropertyName = "AITripleAttack";
            this.aiTripleAttack.HeaderText = "AI: Triple Attack";
            this.aiTripleAttack.Name = "aiTripleAttack";
            this.aiTripleAttack.Width = 5;
            // 
            // aiTripleBracelet
            // 
            this.aiTripleBracelet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiTripleBracelet.DataPropertyName = "AITripleBracelet";
            this.aiTripleBracelet.HeaderText = "AI: Triple Bracelet";
            this.aiTripleBracelet.Name = "aiTripleBracelet";
            this.aiTripleBracelet.Width = 5;
            // 
            // aiMagicDefenseUp
            // 
            this.aiMagicDefenseUp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiMagicDefenseUp.DataPropertyName = "AIMagicDefenseUp";
            this.aiMagicDefenseUp.HeaderText = "AI: Magic Defense Up";
            this.aiMagicDefenseUp.Name = "aiMagicDefenseUp";
            this.aiMagicDefenseUp.Width = 5;
            // 
            // aiDefenseUp
            // 
            this.aiDefenseUp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.aiDefenseUp.DataPropertyName = "AIDefenseUp";
            this.aiDefenseUp.HeaderText = "AI: Defense Up";
            this.aiDefenseUp.Name = "aiDefenseUp";
            this.aiDefenseUp.Width = 5;
            // 
            // Unknown1
            // 
            this.Unknown1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Unknown1.DataPropertyName = "Unknown1";
            this.Unknown1.HeaderText = "Unknown";
            this.Unknown1.Name = "Unknown1";
            this.Unknown1.Width = 5;
            // 
            // Unknown2
            // 
            this.Unknown2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Unknown2.DataPropertyName = "Unknown2";
            this.Unknown2.HeaderText = "Unknown";
            this.Unknown2.Name = "Unknown2";
            this.Unknown2.Width = 5;
            // 
            // Unknown3
            // 
            this.Unknown3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Unknown3.DataPropertyName = "Unknown3";
            this.Unknown3.HeaderText = "Unknown";
            this.Unknown3.Name = "Unknown3";
            this.Unknown3.Width = 5;
            // 
            // Blank2
            // 
            this.Blank2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Blank2.DataPropertyName = "Blank2";
            this.Blank2.HeaderText = "Blank";
            this.Blank2.Name = "Blank2";
            this.Blank2.Width = 5;
            // 
            // Blank3
            // 
            this.Blank3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Blank3.DataPropertyName = "Blank3";
            this.Blank3.HeaderText = "Blank";
            this.Blank3.Name = "Blank3";
            this.Blank3.Width = 5;
            // 
            // Blank4
            // 
            this.Blank4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Blank4.DataPropertyName = "Blank4";
            this.Blank4.HeaderText = "Blank";
            this.Blank4.Name = "Blank4";
            this.Blank4.Width = 5;
            // 
            // Blank5
            // 
            this.Blank5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Blank5.DataPropertyName = "Blank5";
            this.Blank5.HeaderText = "Blank";
            this.Blank5.Name = "Blank5";
            this.Blank5.Width = 5;
            // 
            // Unknown4
            // 
            this.Unknown4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Unknown4.DataPropertyName = "Unknown4";
            this.Unknown4.HeaderText = "Unknown";
            this.Unknown4.Name = "Unknown4";
            this.Unknown4.Width = 5;
            // 
            // AbilitiesDataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.dataGridView1);
            this.Name = "AbilitiesDataGridView";
            this.Size = new System.Drawing.Size(675, 488);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn JPCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn LearnRate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn LearnWithJP;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Action;
        private System.Windows.Forms.DataGridViewCheckBoxColumn LearnOnHit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Blank1;
        private System.Windows.Forms.DataGridViewComboBoxColumn AbilityType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiHP;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiMP;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiCancelStatus;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiAddStatus;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiStats;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiUnequip;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiTargetEnemies;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiTargetAllies;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiIgnoreRange;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiReflectable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiUndeadReverse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiUnknown1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiRandomHits;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiUnknown2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiUnknown3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiSilence;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiBlank;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiDirectAttack;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiLineAttack;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiVerticalIncrease;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiTripleAttack;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiTripleBracelet;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiMagicDefenseUp;
        private System.Windows.Forms.DataGridViewCheckBoxColumn aiDefenseUp;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Unknown1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Unknown2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Unknown3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Blank2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Blank3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Blank4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Blank5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Unknown4;

    }
}
