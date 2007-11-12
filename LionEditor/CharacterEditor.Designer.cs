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
    partial class CharacterEditor
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
            System.Windows.Forms.Label rightHandLabel;
            System.Windows.Forms.Label rightShieldLabel;
            System.Windows.Forms.Label leftHandLabel;
            System.Windows.Forms.Label leftShieldLabel;
            System.Windows.Forms.Label headLabel;
            System.Windows.Forms.Label bodyLabel;
            System.Windows.Forms.Label accessoryLabel;
            System.Windows.Forms.Label skillLabel;
            System.Windows.Forms.Label secondaryLabel;
            System.Windows.Forms.Label reactLabel;
            System.Windows.Forms.Label supportLabel;
            System.Windows.Forms.Label movementLabel;
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.equipAbilityGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.skillTextLabel = new System.Windows.Forms.Label();
            this.rightHandCombo = new System.Windows.Forms.ComboBox();
            this.rightShieldCombo = new System.Windows.Forms.ComboBox();
            this.leftHandCombo = new System.Windows.Forms.ComboBox();
            this.leftShieldCombo = new System.Windows.Forms.ComboBox();
            this.headCombo = new System.Windows.Forms.ComboBox();
            this.bodyCombo = new System.Windows.Forms.ComboBox();
            this.accessoryCombo = new System.Windows.Forms.ComboBox();
            this.secondaryCombo = new System.Windows.Forms.ComboBox();
            this.supportCombo = new System.Windows.Forms.ComboBox();
            this.reactionCombo = new System.Windows.Forms.ComboBox();
            this.movementCombo = new System.Windows.Forms.ComboBox();
            rightHandLabel = new System.Windows.Forms.Label();
            rightShieldLabel = new System.Windows.Forms.Label();
            leftHandLabel = new System.Windows.Forms.Label();
            leftShieldLabel = new System.Windows.Forms.Label();
            headLabel = new System.Windows.Forms.Label();
            bodyLabel = new System.Windows.Forms.Label();
            accessoryLabel = new System.Windows.Forms.Label();
            skillLabel = new System.Windows.Forms.Label();
            secondaryLabel = new System.Windows.Forms.Label();
            reactLabel = new System.Windows.Forms.Label();
            supportLabel = new System.Windows.Forms.Label();
            movementLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.equipAbilityGroupBox.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightHandLabel
            // 
            rightHandLabel.AutoSize = true;
            rightHandLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            rightHandLabel.Location = new System.Drawing.Point( 3, 0 );
            rightHandLabel.Name = "rightHandLabel";
            rightHandLabel.Size = new System.Drawing.Size( 98, 26 );
            rightHandLabel.TabIndex = 11;
            rightHandLabel.Text = "Right hand:";
            rightHandLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rightShieldLabel
            // 
            rightShieldLabel.AutoSize = true;
            rightShieldLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            rightShieldLabel.Location = new System.Drawing.Point( 3, 26 );
            rightShieldLabel.Name = "rightShieldLabel";
            rightShieldLabel.Size = new System.Drawing.Size( 98, 26 );
            rightShieldLabel.TabIndex = 12;
            rightShieldLabel.Text = "Right shield:";
            rightShieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // leftHandLabel
            // 
            leftHandLabel.AutoSize = true;
            leftHandLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            leftHandLabel.Location = new System.Drawing.Point( 3, 52 );
            leftHandLabel.Name = "leftHandLabel";
            leftHandLabel.Size = new System.Drawing.Size( 98, 26 );
            leftHandLabel.TabIndex = 13;
            leftHandLabel.Text = "Left hand:";
            leftHandLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // leftShieldLabel
            // 
            leftShieldLabel.AutoSize = true;
            leftShieldLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            leftShieldLabel.Location = new System.Drawing.Point( 3, 78 );
            leftShieldLabel.Name = "leftShieldLabel";
            leftShieldLabel.Size = new System.Drawing.Size( 98, 26 );
            leftShieldLabel.TabIndex = 14;
            leftShieldLabel.Text = "Left shield:";
            leftShieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // headLabel
            // 
            headLabel.AutoSize = true;
            headLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            headLabel.Location = new System.Drawing.Point( 3, 104 );
            headLabel.Name = "headLabel";
            headLabel.Size = new System.Drawing.Size( 98, 26 );
            headLabel.TabIndex = 15;
            headLabel.Text = "Head:";
            headLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bodyLabel
            // 
            bodyLabel.AutoSize = true;
            bodyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            bodyLabel.Location = new System.Drawing.Point( 3, 130 );
            bodyLabel.Name = "bodyLabel";
            bodyLabel.Size = new System.Drawing.Size( 98, 26 );
            bodyLabel.TabIndex = 16;
            bodyLabel.Text = "Body:";
            bodyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // accessoryLabel
            // 
            accessoryLabel.AutoSize = true;
            accessoryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            accessoryLabel.Location = new System.Drawing.Point( 3, 156 );
            accessoryLabel.Name = "accessoryLabel";
            accessoryLabel.Size = new System.Drawing.Size( 98, 29 );
            accessoryLabel.TabIndex = 17;
            accessoryLabel.Text = "Accessory:";
            accessoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // skillLabel
            // 
            skillLabel.AutoSize = true;
            skillLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            skillLabel.Location = new System.Drawing.Point( 308, 0 );
            skillLabel.Name = "skillLabel";
            skillLabel.Size = new System.Drawing.Size( 98, 26 );
            skillLabel.TabIndex = 18;
            skillLabel.Text = "Skill:";
            skillLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // secondaryLabel
            // 
            secondaryLabel.AutoSize = true;
            secondaryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            secondaryLabel.Location = new System.Drawing.Point( 308, 52 );
            secondaryLabel.Name = "secondaryLabel";
            secondaryLabel.Size = new System.Drawing.Size( 98, 26 );
            secondaryLabel.TabIndex = 20;
            secondaryLabel.Text = "Secondary skill:";
            secondaryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // reactLabel
            // 
            reactLabel.AutoSize = true;
            reactLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            reactLabel.Location = new System.Drawing.Point( 308, 104 );
            reactLabel.Name = "reactLabel";
            reactLabel.Size = new System.Drawing.Size( 98, 26 );
            reactLabel.TabIndex = 21;
            reactLabel.Text = "Reaction ability:";
            reactLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // supportLabel
            // 
            supportLabel.AutoSize = true;
            supportLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            supportLabel.Location = new System.Drawing.Point( 308, 130 );
            supportLabel.Name = "supportLabel";
            supportLabel.Size = new System.Drawing.Size( 98, 26 );
            supportLabel.TabIndex = 22;
            supportLabel.Text = "Support ability:";
            supportLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // movementLabel
            // 
            movementLabel.AutoSize = true;
            movementLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            movementLabel.Location = new System.Drawing.Point( 308, 156 );
            movementLabel.Name = "movementLabel";
            movementLabel.Size = new System.Drawing.Size( 98, 29 );
            movementLabel.TabIndex = 23;
            movementLabel.Text = "Movement ability:";
            movementLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.Controls.Add( this.equipAbilityGroupBox, 0, 2 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point( 0, 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 45.64103F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 54.35897F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 209F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size( 624, 405 );
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // equipAbilityGroupBox
            // 
            this.equipAbilityGroupBox.Controls.Add( this.tableLayoutPanel2 );
            this.equipAbilityGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.equipAbilityGroupBox.Location = new System.Drawing.Point( 3, 198 );
            this.equipAbilityGroupBox.Name = "equipAbilityGroupBox";
            this.equipAbilityGroupBox.Size = new System.Drawing.Size( 618, 204 );
            this.equipAbilityGroupBox.TabIndex = 1;
            this.equipAbilityGroupBox.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 17F ) );
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 33F ) );
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 17F ) );
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 33F ) );
            this.tableLayoutPanel2.Controls.Add( this.skillTextLabel, 3, 0 );
            this.tableLayoutPanel2.Controls.Add( skillLabel, 2, 0 );
            this.tableLayoutPanel2.Controls.Add( this.rightHandCombo, 1, 0 );
            this.tableLayoutPanel2.Controls.Add( this.rightShieldCombo, 1, 1 );
            this.tableLayoutPanel2.Controls.Add( this.leftHandCombo, 1, 2 );
            this.tableLayoutPanel2.Controls.Add( this.leftShieldCombo, 1, 3 );
            this.tableLayoutPanel2.Controls.Add( this.headCombo, 1, 4 );
            this.tableLayoutPanel2.Controls.Add( this.bodyCombo, 1, 5 );
            this.tableLayoutPanel2.Controls.Add( this.accessoryCombo, 1, 6 );
            this.tableLayoutPanel2.Controls.Add( this.secondaryCombo, 3, 2 );
            this.tableLayoutPanel2.Controls.Add( this.supportCombo, 3, 5 );
            this.tableLayoutPanel2.Controls.Add( this.reactionCombo, 3, 4 );
            this.tableLayoutPanel2.Controls.Add( this.movementCombo, 3, 6 );
            this.tableLayoutPanel2.Controls.Add( rightHandLabel, 0, 0 );
            this.tableLayoutPanel2.Controls.Add( leftHandLabel, 0, 2 );
            this.tableLayoutPanel2.Controls.Add( rightShieldLabel, 0, 1 );
            this.tableLayoutPanel2.Controls.Add( leftShieldLabel, 0, 3 );
            this.tableLayoutPanel2.Controls.Add( headLabel, 0, 4 );
            this.tableLayoutPanel2.Controls.Add( bodyLabel, 0, 5 );
            this.tableLayoutPanel2.Controls.Add( accessoryLabel, 0, 6 );
            this.tableLayoutPanel2.Controls.Add( secondaryLabel, 2, 2 );
            this.tableLayoutPanel2.Controls.Add( reactLabel, 2, 4 );
            this.tableLayoutPanel2.Controls.Add( supportLabel, 2, 5 );
            this.tableLayoutPanel2.Controls.Add( movementLabel, 2, 6 );
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point( 3, 16 );
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 14.28571F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 14.28571F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 14.28571F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 14.28571F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 14.28571F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 14.28571F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 14.28571F ) );
            this.tableLayoutPanel2.Size = new System.Drawing.Size( 612, 185 );
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // skillTextLabel
            // 
            this.skillTextLabel.AutoSize = true;
            this.skillTextLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillTextLabel.Location = new System.Drawing.Point( 412, 0 );
            this.skillTextLabel.Name = "skillTextLabel";
            this.skillTextLabel.Size = new System.Drawing.Size( 197, 26 );
            this.skillTextLabel.TabIndex = 19;
            this.skillTextLabel.Text = "label";
            this.skillTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rightHandCombo
            // 
            this.rightHandCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.rightHandCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.rightHandCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightHandCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rightHandCombo.FormattingEnabled = true;
            this.rightHandCombo.Location = new System.Drawing.Point( 107, 3 );
            this.rightHandCombo.Name = "rightHandCombo";
            this.rightHandCombo.Size = new System.Drawing.Size( 195, 21 );
            this.rightHandCombo.TabIndex = 0;
            // 
            // rightShieldCombo
            // 
            this.rightShieldCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.rightShieldCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.rightShieldCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightShieldCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rightShieldCombo.FormattingEnabled = true;
            this.rightShieldCombo.Location = new System.Drawing.Point( 107, 29 );
            this.rightShieldCombo.Name = "rightShieldCombo";
            this.rightShieldCombo.Size = new System.Drawing.Size( 195, 21 );
            this.rightShieldCombo.TabIndex = 1;
            // 
            // leftHandCombo
            // 
            this.leftHandCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.leftHandCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.leftHandCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftHandCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leftHandCombo.FormattingEnabled = true;
            this.leftHandCombo.Location = new System.Drawing.Point( 107, 55 );
            this.leftHandCombo.Name = "leftHandCombo";
            this.leftHandCombo.Size = new System.Drawing.Size( 195, 21 );
            this.leftHandCombo.TabIndex = 2;
            // 
            // leftShieldCombo
            // 
            this.leftShieldCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.leftShieldCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.leftShieldCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftShieldCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leftShieldCombo.FormattingEnabled = true;
            this.leftShieldCombo.Location = new System.Drawing.Point( 107, 81 );
            this.leftShieldCombo.Name = "leftShieldCombo";
            this.leftShieldCombo.Size = new System.Drawing.Size( 195, 21 );
            this.leftShieldCombo.TabIndex = 3;
            // 
            // headCombo
            // 
            this.headCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.headCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.headCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.headCombo.FormattingEnabled = true;
            this.headCombo.Location = new System.Drawing.Point( 107, 107 );
            this.headCombo.Name = "headCombo";
            this.headCombo.Size = new System.Drawing.Size( 195, 21 );
            this.headCombo.TabIndex = 4;
            // 
            // bodyCombo
            // 
            this.bodyCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.bodyCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.bodyCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bodyCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bodyCombo.FormattingEnabled = true;
            this.bodyCombo.Location = new System.Drawing.Point( 107, 133 );
            this.bodyCombo.Name = "bodyCombo";
            this.bodyCombo.Size = new System.Drawing.Size( 195, 21 );
            this.bodyCombo.TabIndex = 5;
            // 
            // accessoryCombo
            // 
            this.accessoryCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.accessoryCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.accessoryCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accessoryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.accessoryCombo.FormattingEnabled = true;
            this.accessoryCombo.Location = new System.Drawing.Point( 107, 159 );
            this.accessoryCombo.Name = "accessoryCombo";
            this.accessoryCombo.Size = new System.Drawing.Size( 195, 21 );
            this.accessoryCombo.TabIndex = 6;
            // 
            // secondaryCombo
            // 
            this.secondaryCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.secondaryCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.secondaryCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secondaryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secondaryCombo.FormattingEnabled = true;
            this.secondaryCombo.Location = new System.Drawing.Point( 412, 55 );
            this.secondaryCombo.Name = "secondaryCombo";
            this.secondaryCombo.Size = new System.Drawing.Size( 197, 21 );
            this.secondaryCombo.TabIndex = 7;
            // 
            // supportCombo
            // 
            this.supportCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.supportCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.supportCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.supportCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.supportCombo.FormattingEnabled = true;
            this.supportCombo.Location = new System.Drawing.Point( 412, 133 );
            this.supportCombo.Name = "supportCombo";
            this.supportCombo.Size = new System.Drawing.Size( 197, 21 );
            this.supportCombo.TabIndex = 9;
            // 
            // reactionCombo
            // 
            this.reactionCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.reactionCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.reactionCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reactionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.reactionCombo.FormattingEnabled = true;
            this.reactionCombo.Location = new System.Drawing.Point( 412, 107 );
            this.reactionCombo.Name = "reactionCombo";
            this.reactionCombo.Size = new System.Drawing.Size( 197, 21 );
            this.reactionCombo.TabIndex = 8;
            // 
            // movementCombo
            // 
            this.movementCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.movementCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.movementCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movementCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.movementCombo.FormattingEnabled = true;
            this.movementCombo.Location = new System.Drawing.Point( 412, 159 );
            this.movementCombo.Name = "movementCombo";
            this.movementCombo.Size = new System.Drawing.Size( 197, 21 );
            this.movementCombo.TabIndex = 10;
            // 
            // CharacterEditor
            // 
            this.Controls.Add( this.tableLayoutPanel1 );
            this.Name = "CharacterEditor";
            this.Size = new System.Drawing.Size( 624, 405 );
            this.tableLayoutPanel1.ResumeLayout( false );
            this.equipAbilityGroupBox.ResumeLayout( false );
            this.tableLayoutPanel2.ResumeLayout( false );
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox rightHandCombo;
        private System.Windows.Forms.ComboBox rightShieldCombo;
        private System.Windows.Forms.ComboBox leftHandCombo;
        private System.Windows.Forms.ComboBox leftShieldCombo;
        private System.Windows.Forms.ComboBox headCombo;
        private System.Windows.Forms.ComboBox bodyCombo;
        private System.Windows.Forms.ComboBox accessoryCombo;
        private System.Windows.Forms.ComboBox secondaryCombo;
        private System.Windows.Forms.ComboBox supportCombo;
        private System.Windows.Forms.ComboBox reactionCombo;
        private System.Windows.Forms.ComboBox movementCombo;
        private System.Windows.Forms.Label skillTextLabel;
        private System.Windows.Forms.GroupBox equipAbilityGroupBox;

    }
}
