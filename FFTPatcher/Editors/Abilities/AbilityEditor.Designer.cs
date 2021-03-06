﻿/*
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
            System.Windows.Forms.Label hLabel2;
            System.Windows.Forms.Label hLabel;
            System.Windows.Forms.Label idLabel;
            System.Windows.Forms.Label arithmeticksLabel;
            System.Windows.Forms.Label ctLabel;
            System.Windows.Forms.Label powerLabel;
            System.Windows.Forms.Label chargingLabel;
            System.Windows.Forms.Label horizontalLabel;
            System.Windows.Forms.Label verticalLabel;
            System.Windows.Forms.Label jumpingLabel;
            System.Windows.Forms.Label throwingLabel;
            System.Windows.Forms.Label itemUseLabel;
            this.abilityIdPanel = new System.Windows.Forms.Panel();
            this.arithmeticksPanel = new System.Windows.Forms.Panel();
            this.chargingPanel = new System.Windows.Forms.Panel();
            this.jumpingPanel = new System.Windows.Forms.Panel();
            this.throwingPanel = new System.Windows.Forms.Panel();
            this.itemUsePanel = new System.Windows.Forms.Panel();
            this.effectLabel = new System.Windows.Forms.Label();
            this.effectComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            this.commonAbilitiesEditor = new FFTPatcher.Editors.CommonAbilitiesEditor();
            this.abilityAttributesEditor = new FFTPatcher.Editors.AbilityAttributesEditor();
            this.arithmeticksSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.itemUseComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            this.idSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.throwingComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            this.verticalSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.horizontalSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.powerSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.ctSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            hLabel2 = new System.Windows.Forms.Label();
            hLabel = new System.Windows.Forms.Label();
            idLabel = new System.Windows.Forms.Label();
            arithmeticksLabel = new System.Windows.Forms.Label();
            ctLabel = new System.Windows.Forms.Label();
            powerLabel = new System.Windows.Forms.Label();
            chargingLabel = new System.Windows.Forms.Label();
            horizontalLabel = new System.Windows.Forms.Label();
            verticalLabel = new System.Windows.Forms.Label();
            jumpingLabel = new System.Windows.Forms.Label();
            throwingLabel = new System.Windows.Forms.Label();
            itemUseLabel = new System.Windows.Forms.Label();
            this.abilityIdPanel.SuspendLayout();
            this.arithmeticksPanel.SuspendLayout();
            this.chargingPanel.SuspendLayout();
            this.jumpingPanel.SuspendLayout();
            this.throwingPanel.SuspendLayout();
            this.itemUsePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.arithmeticksSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // hLabel2
            // 
            hLabel2.AutoSize = true;
            hLabel2.Location = new System.Drawing.Point( 45, 23 );
            hLabel2.Name = "hLabel2";
            hLabel2.Size = new System.Drawing.Size( 13, 13 );
            hLabel2.TabIndex = 21;
            hLabel2.Text = "h";
            // 
            // hLabel
            // 
            hLabel.AutoSize = true;
            hLabel.Location = new System.Drawing.Point( 45, 23 );
            hLabel.Name = "hLabel";
            hLabel.Size = new System.Drawing.Size( 13, 13 );
            hLabel.TabIndex = 20;
            hLabel.Text = "h";
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new System.Drawing.Point( 3, 3 );
            idLabel.Name = "idLabel";
            idLabel.Size = new System.Drawing.Size( 51, 13 );
            idLabel.TabIndex = 21;
            idLabel.Text = "Ability ID:";
            // 
            // arithmeticksLabel
            // 
            arithmeticksLabel.AutoSize = true;
            arithmeticksLabel.Location = new System.Drawing.Point( 3, 3 );
            arithmeticksLabel.Name = "arithmeticksLabel";
            arithmeticksLabel.Size = new System.Drawing.Size( 64, 13 );
            arithmeticksLabel.TabIndex = 19;
            arithmeticksLabel.Text = "Arithmeticks";
            // 
            // ctLabel
            // 
            ctLabel.AutoSize = true;
            ctLabel.Location = new System.Drawing.Point( 14, 19 );
            ctLabel.Name = "ctLabel";
            ctLabel.Size = new System.Drawing.Size( 21, 13 );
            ctLabel.TabIndex = 20;
            ctLabel.Text = "CT";
            // 
            // powerLabel
            // 
            powerLabel.AutoSize = true;
            powerLabel.Location = new System.Drawing.Point( 59, 19 );
            powerLabel.Name = "powerLabel";
            powerLabel.Size = new System.Drawing.Size( 37, 13 );
            powerLabel.TabIndex = 19;
            powerLabel.Text = "Power";
            // 
            // chargingLabel
            // 
            chargingLabel.AutoSize = true;
            chargingLabel.Location = new System.Drawing.Point( 3, 3 );
            chargingLabel.Name = "chargingLabel";
            chargingLabel.Size = new System.Drawing.Size( 52, 13 );
            chargingLabel.TabIndex = 18;
            chargingLabel.Text = "Charging:";
            // 
            // horizontalLabel
            // 
            horizontalLabel.AutoSize = true;
            horizontalLabel.Location = new System.Drawing.Point( 17, 19 );
            horizontalLabel.Name = "horizontalLabel";
            horizontalLabel.Size = new System.Drawing.Size( 15, 13 );
            horizontalLabel.TabIndex = 15;
            horizontalLabel.Text = "H";
            // 
            // verticalLabel
            // 
            verticalLabel.AutoSize = true;
            verticalLabel.Location = new System.Drawing.Point( 70, 19 );
            verticalLabel.Name = "verticalLabel";
            verticalLabel.Size = new System.Drawing.Size( 14, 13 );
            verticalLabel.TabIndex = 14;
            verticalLabel.Text = "V";
            // 
            // jumpingLabel
            // 
            jumpingLabel.AutoSize = true;
            jumpingLabel.Location = new System.Drawing.Point( 3, 3 );
            jumpingLabel.Name = "jumpingLabel";
            jumpingLabel.Size = new System.Drawing.Size( 49, 13 );
            jumpingLabel.TabIndex = 13;
            jumpingLabel.Text = "Jumping:";
            // 
            // throwingLabel
            // 
            throwingLabel.AutoSize = true;
            throwingLabel.Location = new System.Drawing.Point( 3, 2 );
            throwingLabel.Name = "throwingLabel";
            throwingLabel.Size = new System.Drawing.Size( 54, 13 );
            throwingLabel.TabIndex = 10;
            throwingLabel.Text = "Throwing:";
            // 
            // itemUseLabel
            // 
            itemUseLabel.AutoSize = true;
            itemUseLabel.Location = new System.Drawing.Point( 3, 1 );
            itemUseLabel.Name = "itemUseLabel";
            itemUseLabel.Size = new System.Drawing.Size( 52, 13 );
            itemUseLabel.TabIndex = 9;
            itemUseLabel.Text = "Item Use:";
            // 
            // abilityIdPanel
            // 
            this.abilityIdPanel.Controls.Add( idLabel );
            this.abilityIdPanel.Controls.Add( this.idSpinner );
            this.abilityIdPanel.Controls.Add( hLabel );
            this.abilityIdPanel.Location = new System.Drawing.Point( 9, 277 );
            this.abilityIdPanel.Name = "abilityIdPanel";
            this.abilityIdPanel.Size = new System.Drawing.Size( 154, 65 );
            this.abilityIdPanel.TabIndex = 21;
            this.abilityIdPanel.Visible = false;
            // 
            // arithmeticksPanel
            // 
            this.arithmeticksPanel.Controls.Add( hLabel2 );
            this.arithmeticksPanel.Controls.Add( arithmeticksLabel );
            this.arithmeticksPanel.Controls.Add( this.arithmeticksSpinner );
            this.arithmeticksPanel.Location = new System.Drawing.Point( 9, 277 );
            this.arithmeticksPanel.Name = "arithmeticksPanel";
            this.arithmeticksPanel.Size = new System.Drawing.Size( 154, 65 );
            this.arithmeticksPanel.TabIndex = 22;
            this.arithmeticksPanel.Visible = false;
            // 
            // chargingPanel
            // 
            this.chargingPanel.Controls.Add( ctLabel );
            this.chargingPanel.Controls.Add( powerLabel );
            this.chargingPanel.Controls.Add( chargingLabel );
            this.chargingPanel.Controls.Add( this.powerSpinner );
            this.chargingPanel.Controls.Add( this.ctSpinner );
            this.chargingPanel.Location = new System.Drawing.Point( 9, 277 );
            this.chargingPanel.Name = "chargingPanel";
            this.chargingPanel.Size = new System.Drawing.Size( 154, 65 );
            this.chargingPanel.TabIndex = 23;
            this.chargingPanel.Visible = false;
            // 
            // jumpingPanel
            // 
            this.jumpingPanel.Controls.Add( horizontalLabel );
            this.jumpingPanel.Controls.Add( verticalLabel );
            this.jumpingPanel.Controls.Add( jumpingLabel );
            this.jumpingPanel.Controls.Add( this.verticalSpinner );
            this.jumpingPanel.Controls.Add( this.horizontalSpinner );
            this.jumpingPanel.Location = new System.Drawing.Point( 9, 277 );
            this.jumpingPanel.Name = "jumpingPanel";
            this.jumpingPanel.Size = new System.Drawing.Size( 154, 65 );
            this.jumpingPanel.TabIndex = 24;
            this.jumpingPanel.Visible = false;
            // 
            // throwingPanel
            // 
            this.throwingPanel.Controls.Add( throwingLabel );
            this.throwingPanel.Controls.Add( this.throwingComboBox );
            this.throwingPanel.Location = new System.Drawing.Point( 9, 277 );
            this.throwingPanel.Name = "throwingPanel";
            this.throwingPanel.Size = new System.Drawing.Size( 154, 65 );
            this.throwingPanel.TabIndex = 25;
            this.throwingPanel.Visible = false;
            // 
            // itemUsePanel
            // 
            this.itemUsePanel.Controls.Add( itemUseLabel );
            this.itemUsePanel.Controls.Add( this.itemUseComboBox );
            this.itemUsePanel.Location = new System.Drawing.Point( 9, 277 );
            this.itemUsePanel.Name = "itemUsePanel";
            this.itemUsePanel.Size = new System.Drawing.Size( 154, 65 );
            this.itemUsePanel.TabIndex = 26;
            this.itemUsePanel.Visible = false;
            // 
            // effectLabel
            // 
            this.effectLabel.AutoSize = true;
            this.effectLabel.Location = new System.Drawing.Point( 172, 189 );
            this.effectLabel.Name = "effectLabel";
            this.effectLabel.Size = new System.Drawing.Size( 38, 13 );
            this.effectLabel.TabIndex = 28;
            this.effectLabel.Text = "Effect:";
            // 
            // effectComboBox
            // 
            this.effectComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.effectComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.effectComboBox.FormattingEnabled = true;
            this.effectComboBox.Location = new System.Drawing.Point( 216, 186 );
            this.effectComboBox.Name = "effectComboBox";
            this.effectComboBox.Size = new System.Drawing.Size( 333, 21 );
            this.effectComboBox.TabIndex = 27;
            this.effectComboBox.Tag = "Effect";
            // 
            // commonAbilitiesEditor
            // 
            this.commonAbilitiesEditor.Ability = null;
            this.commonAbilitiesEditor.AutoSize = true;
            this.commonAbilitiesEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.commonAbilitiesEditor.Location = new System.Drawing.Point( 3, 3 );
            this.commonAbilitiesEditor.Name = "commonAbilitiesEditor";
            this.commonAbilitiesEditor.Size = new System.Drawing.Size( 558, 268 );
            this.commonAbilitiesEditor.TabIndex = 1;
            // 
            // abilityAttributesEditor
            // 
            this.abilityAttributesEditor.Attributes = null;
            this.abilityAttributesEditor.AutoSize = true;
            this.abilityAttributesEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.abilityAttributesEditor.Location = new System.Drawing.Point( 9, 277 );
            this.abilityAttributesEditor.Name = "abilityAttributesEditor";
            this.abilityAttributesEditor.Size = new System.Drawing.Size( 575, 401 );
            this.abilityAttributesEditor.TabIndex = 2;
            // 
            // arithmeticksSpinner
            // 
            this.arithmeticksSpinner.Hexadecimal = true;
            this.arithmeticksSpinner.Location = new System.Drawing.Point( 3, 21 );
            this.arithmeticksSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.arithmeticksSpinner.Name = "arithmeticksSpinner";
            this.arithmeticksSpinner.Size = new System.Drawing.Size( 43, 20 );
            this.arithmeticksSpinner.TabIndex = 18;
            this.arithmeticksSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // itemUseComboBox
            // 
            this.itemUseComboBox.FormattingEnabled = true;
            this.itemUseComboBox.Location = new System.Drawing.Point( 3, 21 );
            this.itemUseComboBox.Name = "itemUseComboBox";
            this.itemUseComboBox.Size = new System.Drawing.Size( 145, 21 );
            this.itemUseComboBox.TabIndex = 10;
            // 
            // idSpinner
            // 
            this.idSpinner.Hexadecimal = true;
            this.idSpinner.Location = new System.Drawing.Point( 3, 21 );
            this.idSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.idSpinner.Name = "idSpinner";
            this.idSpinner.Size = new System.Drawing.Size( 43, 20 );
            this.idSpinner.TabIndex = 20;
            this.idSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // throwingComboBox
            // 
            this.throwingComboBox.FormattingEnabled = true;
            this.throwingComboBox.Location = new System.Drawing.Point( 3, 20 );
            this.throwingComboBox.Name = "throwingComboBox";
            this.throwingComboBox.Size = new System.Drawing.Size( 121, 21 );
            this.throwingComboBox.TabIndex = 11;
            // 
            // verticalSpinner
            // 
            this.verticalSpinner.Location = new System.Drawing.Point( 56, 35 );
            this.verticalSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.verticalSpinner.Name = "verticalSpinner";
            this.verticalSpinner.Size = new System.Drawing.Size( 43, 20 );
            this.verticalSpinner.TabIndex = 12;
            this.verticalSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // horizontalSpinner
            // 
            this.horizontalSpinner.Location = new System.Drawing.Point( 3, 35 );
            this.horizontalSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.horizontalSpinner.Name = "horizontalSpinner";
            this.horizontalSpinner.Size = new System.Drawing.Size( 43, 20 );
            this.horizontalSpinner.TabIndex = 11;
            this.horizontalSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // powerSpinner
            // 
            this.powerSpinner.Location = new System.Drawing.Point( 56, 35 );
            this.powerSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.powerSpinner.Name = "powerSpinner";
            this.powerSpinner.Size = new System.Drawing.Size( 43, 20 );
            this.powerSpinner.TabIndex = 17;
            this.powerSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ctSpinner
            // 
            this.ctSpinner.Location = new System.Drawing.Point( 3, 35 );
            this.ctSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.ctSpinner.Name = "ctSpinner";
            this.ctSpinner.Size = new System.Drawing.Size( 43, 20 );
            this.ctSpinner.TabIndex = 16;
            this.ctSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // AbilityEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add( this.effectLabel );
            this.Controls.Add( this.effectComboBox );
            this.Controls.Add( this.commonAbilitiesEditor );
            this.Controls.Add( this.abilityAttributesEditor );
            this.Controls.Add( this.arithmeticksPanel );
            this.Controls.Add( this.itemUsePanel );
            this.Controls.Add( this.abilityIdPanel );
            this.Controls.Add( this.throwingPanel );
            this.Controls.Add( this.jumpingPanel );
            this.Controls.Add( this.chargingPanel );
            this.Name = "AbilityEditor";
            this.Size = new System.Drawing.Size( 587, 681 );
            this.abilityIdPanel.ResumeLayout( false );
            this.abilityIdPanel.PerformLayout();
            this.arithmeticksPanel.ResumeLayout( false );
            this.arithmeticksPanel.PerformLayout();
            this.chargingPanel.ResumeLayout( false );
            this.chargingPanel.PerformLayout();
            this.jumpingPanel.ResumeLayout( false );
            this.jumpingPanel.PerformLayout();
            this.throwingPanel.ResumeLayout( false );
            this.throwingPanel.PerformLayout();
            this.itemUsePanel.ResumeLayout( false );
            this.itemUsePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.arithmeticksSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctSpinner)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private CommonAbilitiesEditor commonAbilitiesEditor;
        private AbilityAttributesEditor abilityAttributesEditor;
        private System.Windows.Forms.Panel abilityIdPanel;
        private FFTPatcher.Controls.NumericUpDownWithDefault idSpinner;
        private System.Windows.Forms.Panel arithmeticksPanel;
        private FFTPatcher.Controls.NumericUpDownWithDefault arithmeticksSpinner;
        private System.Windows.Forms.Panel chargingPanel;
        private FFTPatcher.Controls.NumericUpDownWithDefault powerSpinner;
        private FFTPatcher.Controls.NumericUpDownWithDefault ctSpinner;
        private System.Windows.Forms.Panel jumpingPanel;
        private FFTPatcher.Controls.NumericUpDownWithDefault verticalSpinner;
        private FFTPatcher.Controls.NumericUpDownWithDefault horizontalSpinner;
        private System.Windows.Forms.Panel throwingPanel;
        private FFTPatcher.Controls.ComboBoxWithDefault throwingComboBox;
        private System.Windows.Forms.Panel itemUsePanel;
        private FFTPatcher.Controls.ComboBoxWithDefault itemUseComboBox;
        private FFTPatcher.Controls.ComboBoxWithDefault effectComboBox;
        private System.Windows.Forms.Label effectLabel;
    }
}
