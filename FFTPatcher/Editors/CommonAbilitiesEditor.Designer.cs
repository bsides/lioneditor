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

namespace FFTPatcher.Editors
{
    partial class CommonAbilitiesEditor
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
            System.Windows.Forms.Label jpCostLabel;
            System.Windows.Forms.Label learnChanceLabel;
            System.Windows.Forms.Label percentLabel;
            System.Windows.Forms.GroupBox aiGroupBox;
            this.aiCheckedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.aiCheckedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.aiCheckedListBox3 = new System.Windows.Forms.CheckedListBox();
            this.jpCostSpinner = new System.Windows.Forms.NumericUpDown();
            this.chanceSpinner = new System.Windows.Forms.NumericUpDown();
            this.propertiesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.abilityTypeComboBox = new System.Windows.Forms.ComboBox();
            jpCostLabel = new System.Windows.Forms.Label();
            learnChanceLabel = new System.Windows.Forms.Label();
            percentLabel = new System.Windows.Forms.Label();
            aiGroupBox = new System.Windows.Forms.GroupBox();
            aiGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jpCostSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chanceSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // jpCostLabel
            // 
            jpCostLabel.AutoSize = true;
            jpCostLabel.Location = new System.Drawing.Point(3, 7);
            jpCostLabel.Name = "jpCostLabel";
            jpCostLabel.Size = new System.Drawing.Size(46, 13);
            jpCostLabel.TabIndex = 0;
            jpCostLabel.Text = "JP Cost:";
            // 
            // learnChanceLabel
            // 
            learnChanceLabel.AutoSize = true;
            learnChanceLabel.Location = new System.Drawing.Point(3, 31);
            learnChanceLabel.Name = "learnChanceLabel";
            learnChanceLabel.Size = new System.Drawing.Size(88, 13);
            learnChanceLabel.TabIndex = 2;
            learnChanceLabel.Text = "Chance to learn: ";
            // 
            // percentLabel
            // 
            percentLabel.AutoSize = true;
            percentLabel.Location = new System.Drawing.Point(148, 31);
            percentLabel.Name = "percentLabel";
            percentLabel.Size = new System.Drawing.Size(15, 13);
            percentLabel.TabIndex = 10;
            percentLabel.Text = "%";
            // 
            // aiGroupBox
            // 
            aiGroupBox.AutoSize = true;
            aiGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            aiGroupBox.Controls.Add(this.aiCheckedListBox1);
            aiGroupBox.Controls.Add(this.aiCheckedListBox2);
            aiGroupBox.Controls.Add(this.aiCheckedListBox3);
            aiGroupBox.Location = new System.Drawing.Point(169, 3);
            aiGroupBox.Name = "aiGroupBox";
            aiGroupBox.Size = new System.Drawing.Size(385, 162);
            aiGroupBox.TabIndex = 4;
            aiGroupBox.TabStop = false;
            aiGroupBox.Text = "AI Behavior";
            // 
            // aiCheckedListBox1
            // 
            this.aiCheckedListBox1.FormattingEnabled = true;
            this.aiCheckedListBox1.Items.AddRange(new object[] {
            "HP",
            "MP",
            "Cancel Status",
            "Add Status",
            "Stats",
            "Unequip",
            "Target Enemies",
            "Target Allies"});
            this.aiCheckedListBox1.Location = new System.Drawing.Point(6, 19);
            this.aiCheckedListBox1.Name = "aiCheckedListBox1";
            this.aiCheckedListBox1.Size = new System.Drawing.Size(120, 124);
            this.aiCheckedListBox1.TabIndex = 0;
            // 
            // aiCheckedListBox2
            // 
            this.aiCheckedListBox2.FormattingEnabled = true;
            this.aiCheckedListBox2.Items.AddRange(new object[] {
            "Ignore Range?",
            "Reflectable",
            "Undead Reverse",
            "Unknown",
            "Random Hits",
            "Unknown",
            "Unknown",
            "Silence"});
            this.aiCheckedListBox2.Location = new System.Drawing.Point(132, 19);
            this.aiCheckedListBox2.Name = "aiCheckedListBox2";
            this.aiCheckedListBox2.Size = new System.Drawing.Size(120, 124);
            this.aiCheckedListBox2.TabIndex = 1;
            // 
            // aiCheckedListBox3
            // 
            this.aiCheckedListBox3.FormattingEnabled = true;
            this.aiCheckedListBox3.Items.AddRange(new object[] {
            "",
            "Direct Attack",
            "Linear Attack",
            "Vertical Increase",
            "Triple Attack",
            "Triple Bracelet",
            "Magic Defense Up",
            "Defense Up"});
            this.aiCheckedListBox3.Location = new System.Drawing.Point(259, 19);
            this.aiCheckedListBox3.Name = "aiCheckedListBox3";
            this.aiCheckedListBox3.Size = new System.Drawing.Size(120, 124);
            this.aiCheckedListBox3.TabIndex = 2;
            // 
            // jpCostSpinner
            // 
            this.jpCostSpinner.AutoSize = true;
            this.jpCostSpinner.Location = new System.Drawing.Point(97, 4);
            this.jpCostSpinner.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.jpCostSpinner.MinimumSize = new System.Drawing.Size(51, 0);
            this.jpCostSpinner.Name = "jpCostSpinner";
            this.jpCostSpinner.Size = new System.Drawing.Size(51, 20);
            this.jpCostSpinner.TabIndex = 0;
            this.jpCostSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.jpCostSpinner.Value = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            // 
            // chanceSpinner
            // 
            this.chanceSpinner.AutoSize = true;
            this.chanceSpinner.Location = new System.Drawing.Point(97, 28);
            this.chanceSpinner.MinimumSize = new System.Drawing.Size(51, 0);
            this.chanceSpinner.Name = "chanceSpinner";
            this.chanceSpinner.Size = new System.Drawing.Size(51, 20);
            this.chanceSpinner.TabIndex = 1;
            this.chanceSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chanceSpinner.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // propertiesCheckedListBox
            // 
            this.propertiesCheckedListBox.FormattingEnabled = true;
            this.propertiesCheckedListBox.Items.AddRange(new object[] {
            "Learn with JP",
            "Action?",
            "Learn on Hit",
            "",
            "Unknown",
            "Unknown",
            "Unknown",
            "",
            "",
            "",
            "",
            "Unknown"});
            this.propertiesCheckedListBox.Location = new System.Drawing.Point(6, 81);
            this.propertiesCheckedListBox.Name = "propertiesCheckedListBox";
            this.propertiesCheckedListBox.Size = new System.Drawing.Size(142, 184);
            this.propertiesCheckedListBox.TabIndex = 3;
            // 
            // abilityTypeComboBox
            // 
            this.abilityTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.abilityTypeComboBox.FormattingEnabled = true;
            this.abilityTypeComboBox.Location = new System.Drawing.Point(6, 54);
            this.abilityTypeComboBox.Name = "abilityTypeComboBox";
            this.abilityTypeComboBox.Size = new System.Drawing.Size(142, 21);
            this.abilityTypeComboBox.TabIndex = 2;
            // 
            // CommonAbilitiesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(aiGroupBox);
            this.Controls.Add(percentLabel);
            this.Controls.Add(this.abilityTypeComboBox);
            this.Controls.Add(this.propertiesCheckedListBox);
            this.Controls.Add(this.chanceSpinner);
            this.Controls.Add(learnChanceLabel);
            this.Controls.Add(this.jpCostSpinner);
            this.Controls.Add(jpCostLabel);
            this.Name = "CommonAbilitiesEditor";
            this.Size = new System.Drawing.Size(557, 268);
            aiGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.jpCostSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chanceSpinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox aiCheckedListBox3;
        private System.Windows.Forms.CheckedListBox aiCheckedListBox2;
        private System.Windows.Forms.CheckedListBox aiCheckedListBox1;
        private System.Windows.Forms.ComboBox abilityTypeComboBox;
        private System.Windows.Forms.CheckedListBox propertiesCheckedListBox;
        private System.Windows.Forms.NumericUpDown chanceSpinner;
        private System.Windows.Forms.NumericUpDown jpCostSpinner;
    }
}
