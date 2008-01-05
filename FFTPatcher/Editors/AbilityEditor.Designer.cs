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
    partial class AbilityEditor
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
            this.itemUseComboBox = new System.Windows.Forms.ComboBox();
            this.itemUseLabel = new System.Windows.Forms.Label();
            this.throwingComboBox = new System.Windows.Forms.ComboBox();
            this.throwingLabel = new System.Windows.Forms.Label();
            this.horizontalSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.verticalSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.jumpingLabel = new System.Windows.Forms.Label();
            this.verticalLabel = new System.Windows.Forms.Label();
            this.horizontalLabel = new System.Windows.Forms.Label();
            this.ctLabel = new System.Windows.Forms.Label();
            this.powerLabel = new System.Windows.Forms.Label();
            this.chargingLabel = new System.Windows.Forms.Label();
            this.powerSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.ctSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.arithmeticksSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.arithmeticksLabel = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.idSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.abilityAttributesEditor = new FFTPatcher.Editors.AbilityAttributesEditor();
            this.commonAbilitiesEditor = new FFTPatcher.Editors.CommonAbilitiesEditor();
            this.hLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arithmeticksSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // itemUseComboBox
            // 
            this.itemUseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.itemUseComboBox.FormattingEnabled = true;
            this.itemUseComboBox.Location = new System.Drawing.Point(9, 298);
            this.itemUseComboBox.Name = "itemUseComboBox";
            this.itemUseComboBox.Size = new System.Drawing.Size(145, 21);
            this.itemUseComboBox.TabIndex = 8;
            // 
            // itemUseLabel
            // 
            this.itemUseLabel.AutoSize = true;
            this.itemUseLabel.Location = new System.Drawing.Point(9, 278);
            this.itemUseLabel.Name = "itemUseLabel";
            this.itemUseLabel.Size = new System.Drawing.Size(52, 13);
            this.itemUseLabel.TabIndex = 3;
            this.itemUseLabel.Text = "Item Use:";
            // 
            // throwingComboBox
            // 
            this.throwingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.throwingComboBox.FormattingEnabled = true;
            this.throwingComboBox.Location = new System.Drawing.Point(9, 298);
            this.throwingComboBox.Name = "throwingComboBox";
            this.throwingComboBox.Size = new System.Drawing.Size(121, 21);
            this.throwingComboBox.TabIndex = 9;
            // 
            // throwingLabel
            // 
            this.throwingLabel.AutoSize = true;
            this.throwingLabel.Location = new System.Drawing.Point(9, 280);
            this.throwingLabel.Name = "throwingLabel";
            this.throwingLabel.Size = new System.Drawing.Size(54, 13);
            this.throwingLabel.TabIndex = 5;
            this.throwingLabel.Text = "Throwing:";
            // 
            // horizontalSpinner
            // 
            this.horizontalSpinner.Location = new System.Drawing.Point(9, 312);
            this.horizontalSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.horizontalSpinner.Name = "horizontalSpinner";
            this.horizontalSpinner.Size = new System.Drawing.Size(43, 20);
            this.horizontalSpinner.TabIndex = 5;
            this.horizontalSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // verticalSpinner
            // 
            this.verticalSpinner.Location = new System.Drawing.Point(62, 312);
            this.verticalSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.verticalSpinner.Name = "verticalSpinner";
            this.verticalSpinner.Size = new System.Drawing.Size(43, 20);
            this.verticalSpinner.TabIndex = 6;
            this.verticalSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // jumpingLabel
            // 
            this.jumpingLabel.AutoSize = true;
            this.jumpingLabel.Location = new System.Drawing.Point(9, 280);
            this.jumpingLabel.Name = "jumpingLabel";
            this.jumpingLabel.Size = new System.Drawing.Size(49, 13);
            this.jumpingLabel.TabIndex = 8;
            this.jumpingLabel.Text = "Jumping:";
            // 
            // verticalLabel
            // 
            this.verticalLabel.AutoSize = true;
            this.verticalLabel.Location = new System.Drawing.Point(76, 296);
            this.verticalLabel.Name = "verticalLabel";
            this.verticalLabel.Size = new System.Drawing.Size(14, 13);
            this.verticalLabel.TabIndex = 9;
            this.verticalLabel.Text = "V";
            // 
            // horizontalLabel
            // 
            this.horizontalLabel.AutoSize = true;
            this.horizontalLabel.Location = new System.Drawing.Point(23, 296);
            this.horizontalLabel.Name = "horizontalLabel";
            this.horizontalLabel.Size = new System.Drawing.Size(15, 13);
            this.horizontalLabel.TabIndex = 10;
            this.horizontalLabel.Text = "H";
            // 
            // ctLabel
            // 
            this.ctLabel.AutoSize = true;
            this.ctLabel.Location = new System.Drawing.Point(20, 296);
            this.ctLabel.Name = "ctLabel";
            this.ctLabel.Size = new System.Drawing.Size(21, 13);
            this.ctLabel.TabIndex = 15;
            this.ctLabel.Text = "CT";
            // 
            // powerLabel
            // 
            this.powerLabel.AutoSize = true;
            this.powerLabel.Location = new System.Drawing.Point(65, 296);
            this.powerLabel.Name = "powerLabel";
            this.powerLabel.Size = new System.Drawing.Size(37, 13);
            this.powerLabel.TabIndex = 14;
            this.powerLabel.Text = "Power";
            // 
            // chargingLabel
            // 
            this.chargingLabel.AutoSize = true;
            this.chargingLabel.Location = new System.Drawing.Point(9, 280);
            this.chargingLabel.Name = "chargingLabel";
            this.chargingLabel.Size = new System.Drawing.Size(52, 13);
            this.chargingLabel.TabIndex = 13;
            this.chargingLabel.Text = "Charging:";
            // 
            // powerSpinner
            // 
            this.powerSpinner.Location = new System.Drawing.Point(62, 312);
            this.powerSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.powerSpinner.Name = "powerSpinner";
            this.powerSpinner.Size = new System.Drawing.Size(43, 20);
            this.powerSpinner.TabIndex = 4;
            this.powerSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ctSpinner
            // 
            this.ctSpinner.Location = new System.Drawing.Point(9, 312);
            this.ctSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctSpinner.Name = "ctSpinner";
            this.ctSpinner.Size = new System.Drawing.Size(43, 20);
            this.ctSpinner.TabIndex = 3;
            this.ctSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // arithmeticksSpinner
            // 
            this.arithmeticksSpinner.Hexadecimal = true;
            this.arithmeticksSpinner.Location = new System.Drawing.Point(9, 298);
            this.arithmeticksSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.arithmeticksSpinner.Name = "arithmeticksSpinner";
            this.arithmeticksSpinner.Size = new System.Drawing.Size(43, 20);
            this.arithmeticksSpinner.TabIndex = 2;
            this.arithmeticksSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // arithmeticksLabel
            // 
            this.arithmeticksLabel.AutoSize = true;
            this.arithmeticksLabel.Location = new System.Drawing.Point(9, 280);
            this.arithmeticksLabel.Name = "arithmeticksLabel";
            this.arithmeticksLabel.Size = new System.Drawing.Size(64, 13);
            this.arithmeticksLabel.TabIndex = 17;
            this.arithmeticksLabel.Text = "Arithmeticks";
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(9, 280);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(51, 13);
            this.idLabel.TabIndex = 19;
            this.idLabel.Text = "Ability ID:";
            // 
            // idSpinner
            // 
            this.idSpinner.Hexadecimal = true;
            this.idSpinner.Location = new System.Drawing.Point(9, 298);
            this.idSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.idSpinner.Name = "idSpinner";
            this.idSpinner.Size = new System.Drawing.Size(43, 20);
            this.idSpinner.TabIndex = 7;
            this.idSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // abilityAttributesEditor
            // 
            this.abilityAttributesEditor.Attributes = null;
            this.abilityAttributesEditor.AutoSize = true;
            this.abilityAttributesEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.abilityAttributesEditor.Location = new System.Drawing.Point(172, 175);
            this.abilityAttributesEditor.Name = "abilityAttributesEditor";
            this.abilityAttributesEditor.Size = new System.Drawing.Size(303, 517);
            this.abilityAttributesEditor.TabIndex = 2;
            // 
            // commonAbilitiesEditor
            // 
            this.commonAbilitiesEditor.Ability = null;
            this.commonAbilitiesEditor.AutoSize = true;
            this.commonAbilitiesEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.commonAbilitiesEditor.Location = new System.Drawing.Point(3, 3);
            this.commonAbilitiesEditor.Name = "commonAbilitiesEditor";
            this.commonAbilitiesEditor.Size = new System.Drawing.Size(557, 268);
            this.commonAbilitiesEditor.TabIndex = 1;
            // 
            // hLabel
            // 
            this.hLabel.AutoSize = true;
            this.hLabel.Location = new System.Drawing.Point(52, 301);
            this.hLabel.Name = "hLabel";
            this.hLabel.Size = new System.Drawing.Size(13, 13);
            this.hLabel.TabIndex = 20;
            this.hLabel.Text = "h";
            // 
            // AbilityEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.hLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.idSpinner);
            this.Controls.Add(this.arithmeticksLabel);
            this.Controls.Add(this.arithmeticksSpinner);
            this.Controls.Add(this.ctLabel);
            this.Controls.Add(this.powerLabel);
            this.Controls.Add(this.chargingLabel);
            this.Controls.Add(this.powerSpinner);
            this.Controls.Add(this.ctSpinner);
            this.Controls.Add(this.horizontalLabel);
            this.Controls.Add(this.verticalLabel);
            this.Controls.Add(this.jumpingLabel);
            this.Controls.Add(this.verticalSpinner);
            this.Controls.Add(this.horizontalSpinner);
            this.Controls.Add(this.throwingLabel);
            this.Controls.Add(this.throwingComboBox);
            this.Controls.Add(this.itemUseLabel);
            this.Controls.Add(this.itemUseComboBox);
            this.Controls.Add(this.abilityAttributesEditor);
            this.Controls.Add(this.commonAbilitiesEditor);
            this.Name = "AbilityEditor";
            this.Size = new System.Drawing.Size(563, 695);
            ((System.ComponentModel.ISupportInitialize)(this.horizontalSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arithmeticksSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idSpinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CommonAbilitiesEditor commonAbilitiesEditor;
        private AbilityAttributesEditor abilityAttributesEditor;
        private System.Windows.Forms.ComboBox itemUseComboBox;
        private System.Windows.Forms.Label itemUseLabel;
        private System.Windows.Forms.ComboBox throwingComboBox;
        private System.Windows.Forms.Label throwingLabel;
        private FFTPatcher.Controls.NumericUpDownWithDefault horizontalSpinner;
        private FFTPatcher.Controls.NumericUpDownWithDefault verticalSpinner;
        private System.Windows.Forms.Label jumpingLabel;
        private System.Windows.Forms.Label verticalLabel;
        private System.Windows.Forms.Label horizontalLabel;
        private System.Windows.Forms.Label ctLabel;
        private System.Windows.Forms.Label powerLabel;
        private System.Windows.Forms.Label chargingLabel;
        private FFTPatcher.Controls.NumericUpDownWithDefault powerSpinner;
        private FFTPatcher.Controls.NumericUpDownWithDefault ctSpinner;
        private FFTPatcher.Controls.NumericUpDownWithDefault arithmeticksSpinner;
        private System.Windows.Forms.Label arithmeticksLabel;
        private System.Windows.Forms.Label idLabel;
        private FFTPatcher.Controls.NumericUpDownWithDefault idSpinner;
        private System.Windows.Forms.Label hLabel;
    }
}
