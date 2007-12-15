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
    partial class JobEditor
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
            System.Windows.Forms.TableLayoutPanel jobEditorTable;
            System.Windows.Forms.Label totalLabel;
            System.Windows.Forms.Label jpLabel;
            this.movementGroup = new System.Windows.Forms.GroupBox();
            this.supportGroup = new System.Windows.Forms.GroupBox();
            this.actionGroup = new System.Windows.Forms.GroupBox();
            this.reactionGroup = new System.Windows.Forms.GroupBox();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.levelLabel = new System.Windows.Forms.Label();
            this.masteredCheckbox = new System.Windows.Forms.CheckBox();
            this.totalSpinner = new System.Windows.Forms.NumericUpDown();
            this.jpSpinner = new System.Windows.Forms.NumericUpDown();
            this.action1 = new System.Windows.Forms.CheckBox();
            this.action2 = new System.Windows.Forms.CheckBox();
            this.action3 = new System.Windows.Forms.CheckBox();
            this.action4 = new System.Windows.Forms.CheckBox();
            this.action5 = new System.Windows.Forms.CheckBox();
            this.action6 = new System.Windows.Forms.CheckBox();
            this.action7 = new System.Windows.Forms.CheckBox();
            this.action8 = new System.Windows.Forms.CheckBox();
            this.action9 = new System.Windows.Forms.CheckBox();
            this.action10 = new System.Windows.Forms.CheckBox();
            this.action11 = new System.Windows.Forms.CheckBox();
            this.action12 = new System.Windows.Forms.CheckBox();
            this.action13 = new System.Windows.Forms.CheckBox();
            this.action14 = new System.Windows.Forms.CheckBox();
            this.action15 = new System.Windows.Forms.CheckBox();
            this.action16 = new System.Windows.Forms.CheckBox();
            this.reaction1 = new System.Windows.Forms.CheckBox();
            this.reaction2 = new System.Windows.Forms.CheckBox();
            this.reaction3 = new System.Windows.Forms.CheckBox();
            this.support1 = new System.Windows.Forms.CheckBox();
            this.support2 = new System.Windows.Forms.CheckBox();
            this.support3 = new System.Windows.Forms.CheckBox();
            this.support4 = new System.Windows.Forms.CheckBox();
            this.movement1 = new System.Windows.Forms.CheckBox();
            this.movement2 = new System.Windows.Forms.CheckBox();
            jobEditorTable = new System.Windows.Forms.TableLayoutPanel();
            totalLabel = new System.Windows.Forms.Label();
            jpLabel = new System.Windows.Forms.Label();
            jobEditorTable.SuspendLayout();
            this.movementGroup.SuspendLayout();
            this.supportGroup.SuspendLayout();
            this.actionGroup.SuspendLayout();
            this.reactionGroup.SuspendLayout();
            this.infoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jpSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // jobEditorTable
            // 
            jobEditorTable.ColumnCount = 2;
            jobEditorTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            jobEditorTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            jobEditorTable.Controls.Add(this.movementGroup, 1, 2);
            jobEditorTable.Controls.Add(this.supportGroup, 1, 1);
            jobEditorTable.Controls.Add(this.actionGroup, 0, 0);
            jobEditorTable.Controls.Add(this.reactionGroup, 1, 0);
            jobEditorTable.Controls.Add(this.infoPanel, 1, 3);
            jobEditorTable.Dock = System.Windows.Forms.DockStyle.Fill;
            jobEditorTable.Location = new System.Drawing.Point(0, 0);
            jobEditorTable.Name = "jobEditorTable";
            jobEditorTable.RowCount = 4;
            jobEditorTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.52941F));
            jobEditorTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.41176F));
            jobEditorTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.64706F));
            jobEditorTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.41176F));
            jobEditorTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            jobEditorTable.Size = new System.Drawing.Size(397, 432);
            jobEditorTable.TabIndex = 2;
            // 
            // movementGroup
            // 
            this.movementGroup.Controls.Add(this.movement1);
            this.movementGroup.Controls.Add(this.movement2);
            this.movementGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movementGroup.Location = new System.Drawing.Point(201, 231);
            this.movementGroup.Name = "movementGroup";
            this.movementGroup.Size = new System.Drawing.Size(193, 70);
            this.movementGroup.TabIndex = 5;
            this.movementGroup.TabStop = false;
            this.movementGroup.Text = "Movement";
            // 
            // supportGroup
            // 
            this.supportGroup.Controls.Add(this.support1);
            this.supportGroup.Controls.Add(this.support2);
            this.supportGroup.Controls.Add(this.support3);
            this.supportGroup.Controls.Add(this.support4);
            this.supportGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.supportGroup.Location = new System.Drawing.Point(201, 104);
            this.supportGroup.Name = "supportGroup";
            this.supportGroup.Size = new System.Drawing.Size(193, 121);
            this.supportGroup.TabIndex = 4;
            this.supportGroup.TabStop = false;
            this.supportGroup.Text = "Support";
            // 
            // actionGroup
            // 
            this.actionGroup.Controls.Add(this.action1);
            this.actionGroup.Controls.Add(this.action2);
            this.actionGroup.Controls.Add(this.action3);
            this.actionGroup.Controls.Add(this.action4);
            this.actionGroup.Controls.Add(this.action5);
            this.actionGroup.Controls.Add(this.action6);
            this.actionGroup.Controls.Add(this.action7);
            this.actionGroup.Controls.Add(this.action8);
            this.actionGroup.Controls.Add(this.action9);
            this.actionGroup.Controls.Add(this.action10);
            this.actionGroup.Controls.Add(this.action11);
            this.actionGroup.Controls.Add(this.action12);
            this.actionGroup.Controls.Add(this.action13);
            this.actionGroup.Controls.Add(this.action14);
            this.actionGroup.Controls.Add(this.action15);
            this.actionGroup.Controls.Add(this.action16);
            this.actionGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionGroup.Location = new System.Drawing.Point(3, 3);
            this.actionGroup.Name = "actionGroup";
            jobEditorTable.SetRowSpan(this.actionGroup, 4);
            this.actionGroup.Size = new System.Drawing.Size(192, 426);
            this.actionGroup.TabIndex = 1;
            this.actionGroup.TabStop = false;
            this.actionGroup.Text = "Action";
            // 
            // reactionGroup
            // 
            this.reactionGroup.Controls.Add(this.reaction1);
            this.reactionGroup.Controls.Add(this.reaction2);
            this.reactionGroup.Controls.Add(this.reaction3);
            this.reactionGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reactionGroup.Location = new System.Drawing.Point(201, 3);
            this.reactionGroup.Name = "reactionGroup";
            this.reactionGroup.Size = new System.Drawing.Size(193, 95);
            this.reactionGroup.TabIndex = 2;
            this.reactionGroup.TabStop = false;
            this.reactionGroup.Text = "Reaction";
            // 
            // infoPanel
            // 
            this.infoPanel.Controls.Add(this.levelLabel);
            this.infoPanel.Controls.Add(this.masteredCheckbox);
            this.infoPanel.Controls.Add(this.totalSpinner);
            this.infoPanel.Controls.Add(totalLabel);
            this.infoPanel.Controls.Add(this.jpSpinner);
            this.infoPanel.Controls.Add(jpLabel);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(199, 305);
            this.infoPanel.Margin = new System.Windows.Forms.Padding(1);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(197, 126);
            this.infoPanel.TabIndex = 8;
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(5, 1);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(53, 13);
            this.levelLabel.TabIndex = 201;
            this.levelLabel.Text = "Level: {0}";
            this.levelLabel.Visible = false;
            // 
            // masteredCheckbox
            // 
            this.masteredCheckbox.AutoSize = true;
            this.masteredCheckbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.masteredCheckbox.Enabled = false;
            this.masteredCheckbox.Location = new System.Drawing.Point(0, 0);
            this.masteredCheckbox.Name = "masteredCheckbox";
            this.masteredCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.masteredCheckbox.Size = new System.Drawing.Size(197, 17);
            this.masteredCheckbox.TabIndex = 8;
            this.masteredCheckbox.Text = "Mastered";
            this.masteredCheckbox.UseVisualStyleBackColor = true;
            // 
            // totalSpinner
            // 
            this.totalSpinner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.totalSpinner.Location = new System.Drawing.Point(127, 49);
            this.totalSpinner.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.totalSpinner.Name = "totalSpinner";
            this.totalSpinner.Size = new System.Drawing.Size(68, 20);
            this.totalSpinner.TabIndex = 200;
            this.totalSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // totalLabel
            // 
            totalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            totalLabel.AutoSize = true;
            totalLabel.Location = new System.Drawing.Point(87, 51);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new System.Drawing.Size(34, 13);
            totalLabel.TabIndex = 0;
            totalLabel.Text = "Total:";
            // 
            // jpSpinner
            // 
            this.jpSpinner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.jpSpinner.Location = new System.Drawing.Point(127, 22);
            this.jpSpinner.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.jpSpinner.Name = "jpSpinner";
            this.jpSpinner.Size = new System.Drawing.Size(68, 20);
            this.jpSpinner.TabIndex = 100;
            this.jpSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // jpLabel
            // 
            jpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            jpLabel.AutoSize = true;
            jpLabel.Location = new System.Drawing.Point(99, 24);
            jpLabel.Name = "jpLabel";
            jpLabel.Size = new System.Drawing.Size(22, 13);
            jpLabel.TabIndex = 0;
            jpLabel.Text = "JP:";
            // 
            // action1
            // 
            this.action1.AutoSize = true;
            this.action1.Location = new System.Drawing.Point(6, 19);
            this.action1.Name = "action1";
            this.action1.Size = new System.Drawing.Size(61, 17);
            this.action1.TabIndex = 1801;
            this.action1.Text = "action1";
            this.action1.UseVisualStyleBackColor = true;
            // 
            // action2
            // 
            this.action2.AutoSize = true;
            this.action2.Location = new System.Drawing.Point(6, 42);
            this.action2.Name = "action2";
            this.action2.Size = new System.Drawing.Size(61, 17);
            this.action2.TabIndex = 1802;
            this.action2.Text = "action2";
            this.action2.UseVisualStyleBackColor = true;
            // 
            // action3
            // 
            this.action3.AutoSize = true;
            this.action3.Location = new System.Drawing.Point(6, 65);
            this.action3.Name = "action3";
            this.action3.Size = new System.Drawing.Size(61, 17);
            this.action3.TabIndex = 1803;
            this.action3.Text = "action3";
            this.action3.UseVisualStyleBackColor = true;
            // 
            // action4
            // 
            this.action4.AutoSize = true;
            this.action4.Location = new System.Drawing.Point(6, 88);
            this.action4.Name = "action4";
            this.action4.Size = new System.Drawing.Size(61, 17);
            this.action4.TabIndex = 1804;
            this.action4.Text = "action4";
            this.action4.UseVisualStyleBackColor = true;
            // 
            // action5
            // 
            this.action5.AutoSize = true;
            this.action5.Location = new System.Drawing.Point(6, 111);
            this.action5.Name = "action5";
            this.action5.Size = new System.Drawing.Size(61, 17);
            this.action5.TabIndex = 1805;
            this.action5.Text = "action5";
            this.action5.UseVisualStyleBackColor = true;
            // 
            // action6
            // 
            this.action6.AutoSize = true;
            this.action6.Location = new System.Drawing.Point(6, 134);
            this.action6.Name = "action6";
            this.action6.Size = new System.Drawing.Size(61, 17);
            this.action6.TabIndex = 1806;
            this.action6.Text = "action6";
            this.action6.UseVisualStyleBackColor = true;
            // 
            // action7
            // 
            this.action7.AutoSize = true;
            this.action7.Location = new System.Drawing.Point(6, 157);
            this.action7.Name = "action7";
            this.action7.Size = new System.Drawing.Size(61, 17);
            this.action7.TabIndex = 1807;
            this.action7.Text = "action7";
            this.action7.UseVisualStyleBackColor = true;
            // 
            // action8
            // 
            this.action8.AutoSize = true;
            this.action8.Location = new System.Drawing.Point(6, 180);
            this.action8.Name = "action8";
            this.action8.Size = new System.Drawing.Size(61, 17);
            this.action8.TabIndex = 1808;
            this.action8.Text = "action8";
            this.action8.UseVisualStyleBackColor = true;
            // 
            // action9
            // 
            this.action9.AutoSize = true;
            this.action9.Location = new System.Drawing.Point(6, 203);
            this.action9.Name = "action9";
            this.action9.Size = new System.Drawing.Size(61, 17);
            this.action9.TabIndex = 1809;
            this.action9.Text = "action9";
            this.action9.UseVisualStyleBackColor = true;
            // 
            // action10
            // 
            this.action10.AutoSize = true;
            this.action10.Location = new System.Drawing.Point(6, 226);
            this.action10.Name = "action10";
            this.action10.Size = new System.Drawing.Size(67, 17);
            this.action10.TabIndex = 1810;
            this.action10.Text = "action10";
            this.action10.UseVisualStyleBackColor = true;
            // 
            // action11
            // 
            this.action11.AutoSize = true;
            this.action11.Location = new System.Drawing.Point(6, 249);
            this.action11.Name = "action11";
            this.action11.Size = new System.Drawing.Size(67, 17);
            this.action11.TabIndex = 1811;
            this.action11.Text = "action11";
            this.action11.UseVisualStyleBackColor = true;
            // 
            // action12
            // 
            this.action12.AutoSize = true;
            this.action12.Location = new System.Drawing.Point(6, 272);
            this.action12.Name = "action12";
            this.action12.Size = new System.Drawing.Size(67, 17);
            this.action12.TabIndex = 1812;
            this.action12.Text = "action12";
            this.action12.UseVisualStyleBackColor = true;
            // 
            // action13
            // 
            this.action13.AutoSize = true;
            this.action13.Location = new System.Drawing.Point(6, 295);
            this.action13.Name = "action13";
            this.action13.Size = new System.Drawing.Size(67, 17);
            this.action13.TabIndex = 1813;
            this.action13.Text = "action13";
            this.action13.UseVisualStyleBackColor = true;
            // 
            // action14
            // 
            this.action14.AutoSize = true;
            this.action14.Location = new System.Drawing.Point(6, 318);
            this.action14.Name = "action14";
            this.action14.Size = new System.Drawing.Size(67, 17);
            this.action14.TabIndex = 1814;
            this.action14.Text = "action14";
            this.action14.UseVisualStyleBackColor = true;
            // 
            // action15
            // 
            this.action15.AutoSize = true;
            this.action15.Location = new System.Drawing.Point(6, 341);
            this.action15.Name = "action15";
            this.action15.Size = new System.Drawing.Size(67, 17);
            this.action15.TabIndex = 1815;
            this.action15.Text = "action15";
            this.action15.UseVisualStyleBackColor = true;
            // 
            // action16
            // 
            this.action16.AutoSize = true;
            this.action16.Location = new System.Drawing.Point(6, 364);
            this.action16.Name = "action16";
            this.action16.Size = new System.Drawing.Size(67, 17);
            this.action16.TabIndex = 1816;
            this.action16.Text = "action16";
            this.action16.UseVisualStyleBackColor = true;
            // 
            // reaction1
            // 
            this.reaction1.AutoSize = true;
            this.reaction1.Location = new System.Drawing.Point(6, 19);
            this.reaction1.Name = "reaction1";
            this.reaction1.Size = new System.Drawing.Size(70, 17);
            this.reaction1.TabIndex = 2101;
            this.reaction1.Text = "reaction1";
            this.reaction1.UseVisualStyleBackColor = true;
            // 
            // reaction2
            // 
            this.reaction2.AutoSize = true;
            this.reaction2.Location = new System.Drawing.Point(6, 42);
            this.reaction2.Name = "reaction2";
            this.reaction2.Size = new System.Drawing.Size(70, 17);
            this.reaction2.TabIndex = 2102;
            this.reaction2.Text = "reaction2";
            this.reaction2.UseVisualStyleBackColor = true;
            // 
            // reaction3
            // 
            this.reaction3.AutoSize = true;
            this.reaction3.Location = new System.Drawing.Point(6, 65);
            this.reaction3.Name = "reaction3";
            this.reaction3.Size = new System.Drawing.Size(70, 17);
            this.reaction3.TabIndex = 2103;
            this.reaction3.Text = "reaction3";
            this.reaction3.UseVisualStyleBackColor = true;
            // 
            // support1
            // 
            this.support1.AutoSize = true;
            this.support1.Location = new System.Drawing.Point(6, 19);
            this.support1.Name = "support1";
            this.support1.Size = new System.Drawing.Size(67, 17);
            this.support1.TabIndex = 2501;
            this.support1.Text = "support1";
            this.support1.UseVisualStyleBackColor = true;
            // 
            // support2
            // 
            this.support2.AutoSize = true;
            this.support2.Location = new System.Drawing.Point(6, 42);
            this.support2.Name = "support2";
            this.support2.Size = new System.Drawing.Size(67, 17);
            this.support2.TabIndex = 2502;
            this.support2.Text = "support2";
            this.support2.UseVisualStyleBackColor = true;
            // 
            // support3
            // 
            this.support3.AutoSize = true;
            this.support3.Location = new System.Drawing.Point(6, 65);
            this.support3.Name = "support3";
            this.support3.Size = new System.Drawing.Size(67, 17);
            this.support3.TabIndex = 2503;
            this.support3.Text = "support3";
            this.support3.UseVisualStyleBackColor = true;
            // 
            // support4
            // 
            this.support4.AutoSize = true;
            this.support4.Location = new System.Drawing.Point(6, 88);
            this.support4.Name = "support4";
            this.support4.Size = new System.Drawing.Size(67, 17);
            this.support4.TabIndex = 2504;
            this.support4.Text = "support4";
            this.support4.UseVisualStyleBackColor = true;
            // 
            // movement1
            // 
            this.movement1.AutoSize = true;
            this.movement1.Location = new System.Drawing.Point(6, 19);
            this.movement1.Name = "movement1";
            this.movement1.Size = new System.Drawing.Size(81, 17);
            this.movement1.TabIndex = 2701;
            this.movement1.Text = "movement1";
            this.movement1.UseVisualStyleBackColor = true;
            // 
            // movement2
            // 
            this.movement2.AutoSize = true;
            this.movement2.Location = new System.Drawing.Point(6, 42);
            this.movement2.Name = "movement2";
            this.movement2.Size = new System.Drawing.Size(81, 17);
            this.movement2.TabIndex = 2702;
            this.movement2.Text = "movement2";
            this.movement2.UseVisualStyleBackColor = true;
            // 
            // JobEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(jobEditorTable);
            this.Name = "JobEditor";
            this.Size = new System.Drawing.Size(397, 432);
            jobEditorTable.ResumeLayout(false);
            this.movementGroup.ResumeLayout(false);
            this.movementGroup.PerformLayout();
            this.supportGroup.ResumeLayout(false);
            this.supportGroup.PerformLayout();
            this.actionGroup.ResumeLayout(false);
            this.actionGroup.PerformLayout();
            this.reactionGroup.ResumeLayout(false);
            this.reactionGroup.PerformLayout();
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jpSpinner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown jpSpinner;
        private System.Windows.Forms.NumericUpDown totalSpinner;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.CheckBox masteredCheckbox;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.CheckBox movement1;
        private System.Windows.Forms.CheckBox movement2;
        private System.Windows.Forms.CheckBox support1;
        private System.Windows.Forms.CheckBox support2;
        private System.Windows.Forms.CheckBox support3;
        private System.Windows.Forms.CheckBox support4;
        private System.Windows.Forms.CheckBox action1;
        private System.Windows.Forms.CheckBox action2;
        private System.Windows.Forms.CheckBox action3;
        private System.Windows.Forms.CheckBox action4;
        private System.Windows.Forms.CheckBox action5;
        private System.Windows.Forms.CheckBox action6;
        private System.Windows.Forms.CheckBox action7;
        private System.Windows.Forms.CheckBox action8;
        private System.Windows.Forms.CheckBox action9;
        private System.Windows.Forms.CheckBox action10;
        private System.Windows.Forms.CheckBox action11;
        private System.Windows.Forms.CheckBox action12;
        private System.Windows.Forms.CheckBox action13;
        private System.Windows.Forms.CheckBox action14;
        private System.Windows.Forms.CheckBox action15;
        private System.Windows.Forms.CheckBox action16;
        private System.Windows.Forms.CheckBox reaction1;
        private System.Windows.Forms.CheckBox reaction2;
        private System.Windows.Forms.CheckBox reaction3;
        private System.Windows.Forms.GroupBox movementGroup;
        private System.Windows.Forms.GroupBox supportGroup;
        private System.Windows.Forms.GroupBox actionGroup;
        private System.Windows.Forms.GroupBox reactionGroup;
    }
}
