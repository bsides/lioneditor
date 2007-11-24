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
            System.Windows.Forms.Panel panel2;
            System.Windows.Forms.Label totalLabel;
            System.Windows.Forms.GroupBox movementGroup;
            System.Windows.Forms.TableLayoutPanel movementTable;
            System.Windows.Forms.GroupBox supportGroup;
            System.Windows.Forms.TableLayoutPanel supportTable;
            System.Windows.Forms.GroupBox actionGroup;
            System.Windows.Forms.TableLayoutPanel actionTable;
            System.Windows.Forms.GroupBox reactionGroup;
            System.Windows.Forms.TableLayoutPanel reactionTable;
            System.Windows.Forms.Panel panel1;
            System.Windows.Forms.Label jpLabel;
            this.totalSpinner = new System.Windows.Forms.NumericUpDown();
            this.movement2 = new System.Windows.Forms.CheckBox();
            this.movement1 = new System.Windows.Forms.CheckBox();
            this.support1 = new System.Windows.Forms.CheckBox();
            this.support2 = new System.Windows.Forms.CheckBox();
            this.support3 = new System.Windows.Forms.CheckBox();
            this.support4 = new System.Windows.Forms.CheckBox();
            this.action13 = new System.Windows.Forms.CheckBox();
            this.action12 = new System.Windows.Forms.CheckBox();
            this.action11 = new System.Windows.Forms.CheckBox();
            this.action10 = new System.Windows.Forms.CheckBox();
            this.action9 = new System.Windows.Forms.CheckBox();
            this.action8 = new System.Windows.Forms.CheckBox();
            this.action7 = new System.Windows.Forms.CheckBox();
            this.action6 = new System.Windows.Forms.CheckBox();
            this.action5 = new System.Windows.Forms.CheckBox();
            this.action4 = new System.Windows.Forms.CheckBox();
            this.action3 = new System.Windows.Forms.CheckBox();
            this.action2 = new System.Windows.Forms.CheckBox();
            this.action1 = new System.Windows.Forms.CheckBox();
            this.action16 = new System.Windows.Forms.CheckBox();
            this.action15 = new System.Windows.Forms.CheckBox();
            this.action14 = new System.Windows.Forms.CheckBox();
            this.reaction1 = new System.Windows.Forms.CheckBox();
            this.reaction2 = new System.Windows.Forms.CheckBox();
            this.reaction3 = new System.Windows.Forms.CheckBox();
            this.jpSpinner = new System.Windows.Forms.NumericUpDown();
            jobEditorTable = new System.Windows.Forms.TableLayoutPanel();
            panel2 = new System.Windows.Forms.Panel();
            totalLabel = new System.Windows.Forms.Label();
            movementGroup = new System.Windows.Forms.GroupBox();
            movementTable = new System.Windows.Forms.TableLayoutPanel();
            supportGroup = new System.Windows.Forms.GroupBox();
            supportTable = new System.Windows.Forms.TableLayoutPanel();
            actionGroup = new System.Windows.Forms.GroupBox();
            actionTable = new System.Windows.Forms.TableLayoutPanel();
            reactionGroup = new System.Windows.Forms.GroupBox();
            reactionTable = new System.Windows.Forms.TableLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
            jpLabel = new System.Windows.Forms.Label();
            jobEditorTable.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalSpinner)).BeginInit();
            movementGroup.SuspendLayout();
            movementTable.SuspendLayout();
            supportGroup.SuspendLayout();
            supportTable.SuspendLayout();
            actionGroup.SuspendLayout();
            actionTable.SuspendLayout();
            reactionGroup.SuspendLayout();
            reactionTable.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jpSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // jobEditorTable
            // 
            jobEditorTable.ColumnCount = 2;
            jobEditorTable.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            jobEditorTable.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            jobEditorTable.Controls.Add( panel2, 1, 0 );
            jobEditorTable.Controls.Add( movementGroup, 1, 3 );
            jobEditorTable.Controls.Add( supportGroup, 1, 2 );
            jobEditorTable.Controls.Add( actionGroup, 0, 1 );
            jobEditorTable.Controls.Add( reactionGroup, 1, 1 );
            jobEditorTable.Controls.Add( panel1, 0, 0 );
            jobEditorTable.Dock = System.Windows.Forms.DockStyle.Fill;
            jobEditorTable.Location = new System.Drawing.Point( 0, 0 );
            jobEditorTable.Name = "jobEditorTable";
            jobEditorTable.RowCount = 5;
            jobEditorTable.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 40F ) );
            jobEditorTable.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 23.52941F ) );
            jobEditorTable.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 29.41176F ) );
            jobEditorTable.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 17.64706F ) );
            jobEditorTable.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 29.41176F ) );
            jobEditorTable.Size = new System.Drawing.Size( 625, 518 );
            jobEditorTable.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add( this.totalSpinner );
            panel2.Controls.Add( totalLabel );
            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Location = new System.Drawing.Point( 315, 3 );
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size( 307, 34 );
            panel2.TabIndex = 7;
            // 
            // totalSpinner
            // 
            this.totalSpinner.Location = new System.Drawing.Point( 63, 5 );
            this.totalSpinner.Maximum = new decimal( new int[] {
            9999,
            0,
            0,
            0} );
            this.totalSpinner.Name = "totalSpinner";
            this.totalSpinner.Size = new System.Drawing.Size( 68, 20 );
            this.totalSpinner.TabIndex = 1;
            // 
            // totalLabel
            // 
            totalLabel.AutoSize = true;
            totalLabel.Location = new System.Drawing.Point( 7, 9 );
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new System.Drawing.Size( 34, 13 );
            totalLabel.TabIndex = 0;
            totalLabel.Text = "Total:";
            // 
            // movementGroup
            // 
            movementGroup.Controls.Add( movementTable );
            movementGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            movementGroup.Location = new System.Drawing.Point( 315, 295 );
            movementGroup.Name = "movementGroup";
            movementGroup.Size = new System.Drawing.Size( 307, 78 );
            movementGroup.TabIndex = 5;
            movementGroup.TabStop = false;
            movementGroup.Text = "Movement";
            // 
            // movementTable
            // 
            movementTable.ColumnCount = 1;
            movementTable.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            movementTable.Controls.Add( this.movement2, 0, 1 );
            movementTable.Controls.Add( this.movement1, 0, 0 );
            movementTable.Dock = System.Windows.Forms.DockStyle.Fill;
            movementTable.Location = new System.Drawing.Point( 3, 16 );
            movementTable.Name = "movementTable";
            movementTable.RowCount = 2;
            movementTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            movementTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            movementTable.Size = new System.Drawing.Size( 301, 59 );
            movementTable.TabIndex = 0;
            // 
            // movement2
            // 
            this.movement2.AutoSize = true;
            this.movement2.Location = new System.Drawing.Point( 3, 26 );
            this.movement2.Name = "movement2";
            this.movement2.Size = new System.Drawing.Size( 81, 17 );
            this.movement2.TabIndex = 2700;
            this.movement2.Text = "movement2";
            this.movement2.UseVisualStyleBackColor = true;
            // 
            // movement1
            // 
            this.movement1.AutoSize = true;
            this.movement1.Location = new System.Drawing.Point( 3, 3 );
            this.movement1.Name = "movement1";
            this.movement1.Size = new System.Drawing.Size( 81, 17 );
            this.movement1.TabIndex = 2600;
            this.movement1.Text = "movement1";
            this.movement1.UseVisualStyleBackColor = true;
            // 
            // supportGroup
            // 
            supportGroup.Controls.Add( supportTable );
            supportGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            supportGroup.Location = new System.Drawing.Point( 315, 155 );
            supportGroup.Name = "supportGroup";
            supportGroup.Size = new System.Drawing.Size( 307, 134 );
            supportGroup.TabIndex = 4;
            supportGroup.TabStop = false;
            supportGroup.Text = "Support";
            // 
            // supportTable
            // 
            supportTable.ColumnCount = 1;
            supportTable.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            supportTable.Controls.Add( this.support1, 0, 0 );
            supportTable.Controls.Add( this.support2, 0, 1 );
            supportTable.Controls.Add( this.support3, 0, 2 );
            supportTable.Controls.Add( this.support4, 0, 3 );
            supportTable.Dock = System.Windows.Forms.DockStyle.Fill;
            supportTable.Location = new System.Drawing.Point( 3, 16 );
            supportTable.Name = "supportTable";
            supportTable.RowCount = 4;
            supportTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            supportTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            supportTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            supportTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            supportTable.Size = new System.Drawing.Size( 301, 115 );
            supportTable.TabIndex = 0;
            // 
            // support1
            // 
            this.support1.AutoSize = true;
            this.support1.Location = new System.Drawing.Point( 3, 3 );
            this.support1.Name = "support1";
            this.support1.Size = new System.Drawing.Size( 67, 17 );
            this.support1.TabIndex = 2200;
            this.support1.Text = "support1";
            this.support1.UseVisualStyleBackColor = true;
            // 
            // support2
            // 
            this.support2.AutoSize = true;
            this.support2.Location = new System.Drawing.Point( 3, 26 );
            this.support2.Name = "support2";
            this.support2.Size = new System.Drawing.Size( 67, 17 );
            this.support2.TabIndex = 2300;
            this.support2.Text = "support2";
            this.support2.UseVisualStyleBackColor = true;
            // 
            // support3
            // 
            this.support3.AutoSize = true;
            this.support3.Location = new System.Drawing.Point( 3, 49 );
            this.support3.Name = "support3";
            this.support3.Size = new System.Drawing.Size( 67, 17 );
            this.support3.TabIndex = 2400;
            this.support3.Text = "support3";
            this.support3.UseVisualStyleBackColor = true;
            // 
            // support4
            // 
            this.support4.AutoSize = true;
            this.support4.Location = new System.Drawing.Point( 3, 72 );
            this.support4.Name = "support4";
            this.support4.Size = new System.Drawing.Size( 67, 17 );
            this.support4.TabIndex = 2500;
            this.support4.Text = "support4";
            this.support4.UseVisualStyleBackColor = true;
            // 
            // actionGroup
            // 
            actionGroup.Controls.Add( actionTable );
            actionGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            actionGroup.Location = new System.Drawing.Point( 3, 43 );
            actionGroup.Name = "actionGroup";
            jobEditorTable.SetRowSpan( actionGroup, 4 );
            actionGroup.Size = new System.Drawing.Size( 306, 472 );
            actionGroup.TabIndex = 1;
            actionGroup.TabStop = false;
            actionGroup.Text = "Action";
            // 
            // actionTable
            // 
            actionTable.ColumnCount = 1;
            actionTable.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            actionTable.Controls.Add( this.action13, 0, 12 );
            actionTable.Controls.Add( this.action12, 0, 11 );
            actionTable.Controls.Add( this.action11, 0, 10 );
            actionTable.Controls.Add( this.action10, 0, 9 );
            actionTable.Controls.Add( this.action9, 0, 8 );
            actionTable.Controls.Add( this.action8, 0, 7 );
            actionTable.Controls.Add( this.action7, 0, 6 );
            actionTable.Controls.Add( this.action6, 0, 5 );
            actionTable.Controls.Add( this.action5, 0, 4 );
            actionTable.Controls.Add( this.action4, 0, 3 );
            actionTable.Controls.Add( this.action3, 0, 2 );
            actionTable.Controls.Add( this.action2, 0, 1 );
            actionTable.Controls.Add( this.action1, 0, 0 );
            actionTable.Controls.Add( this.action16, 0, 15 );
            actionTable.Controls.Add( this.action15, 0, 14 );
            actionTable.Controls.Add( this.action14, 0, 13 );
            actionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            actionTable.Location = new System.Drawing.Point( 3, 16 );
            actionTable.Name = "actionTable";
            actionTable.RowCount = 16;
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            actionTable.Size = new System.Drawing.Size( 300, 453 );
            actionTable.TabIndex = 1;
            // 
            // action13
            // 
            this.action13.AutoSize = true;
            this.action13.Location = new System.Drawing.Point( 3, 279 );
            this.action13.Name = "action13";
            this.action13.Size = new System.Drawing.Size( 67, 17 );
            this.action13.TabIndex = 1500;
            this.action13.Text = "action13";
            this.action13.UseVisualStyleBackColor = true;
            // 
            // action12
            // 
            this.action12.AutoSize = true;
            this.action12.Location = new System.Drawing.Point( 3, 256 );
            this.action12.Name = "action12";
            this.action12.Size = new System.Drawing.Size( 67, 17 );
            this.action12.TabIndex = 1400;
            this.action12.Text = "action12";
            this.action12.UseVisualStyleBackColor = true;
            // 
            // action11
            // 
            this.action11.AutoSize = true;
            this.action11.Location = new System.Drawing.Point( 3, 233 );
            this.action11.Name = "action11";
            this.action11.Size = new System.Drawing.Size( 67, 17 );
            this.action11.TabIndex = 1300;
            this.action11.Text = "action11";
            this.action11.UseVisualStyleBackColor = true;
            // 
            // action10
            // 
            this.action10.AutoSize = true;
            this.action10.Location = new System.Drawing.Point( 3, 210 );
            this.action10.Name = "action10";
            this.action10.Size = new System.Drawing.Size( 67, 17 );
            this.action10.TabIndex = 1200;
            this.action10.Text = "action10";
            this.action10.UseVisualStyleBackColor = true;
            // 
            // action9
            // 
            this.action9.AutoSize = true;
            this.action9.Location = new System.Drawing.Point( 3, 187 );
            this.action9.Name = "action9";
            this.action9.Size = new System.Drawing.Size( 61, 17 );
            this.action9.TabIndex = 1100;
            this.action9.Text = "action9";
            this.action9.UseVisualStyleBackColor = true;
            // 
            // action8
            // 
            this.action8.AutoSize = true;
            this.action8.Location = new System.Drawing.Point( 3, 164 );
            this.action8.Name = "action8";
            this.action8.Size = new System.Drawing.Size( 61, 17 );
            this.action8.TabIndex = 1000;
            this.action8.Text = "action8";
            this.action8.UseVisualStyleBackColor = true;
            // 
            // action7
            // 
            this.action7.AutoSize = true;
            this.action7.Location = new System.Drawing.Point( 3, 141 );
            this.action7.Name = "action7";
            this.action7.Size = new System.Drawing.Size( 61, 17 );
            this.action7.TabIndex = 900;
            this.action7.Text = "action7";
            this.action7.UseVisualStyleBackColor = true;
            // 
            // action6
            // 
            this.action6.AutoSize = true;
            this.action6.Location = new System.Drawing.Point( 3, 118 );
            this.action6.Name = "action6";
            this.action6.Size = new System.Drawing.Size( 61, 17 );
            this.action6.TabIndex = 800;
            this.action6.Text = "action6";
            this.action6.UseVisualStyleBackColor = true;
            // 
            // action5
            // 
            this.action5.AutoSize = true;
            this.action5.Location = new System.Drawing.Point( 3, 95 );
            this.action5.Name = "action5";
            this.action5.Size = new System.Drawing.Size( 61, 17 );
            this.action5.TabIndex = 700;
            this.action5.Text = "action5";
            this.action5.UseVisualStyleBackColor = true;
            // 
            // action4
            // 
            this.action4.AutoSize = true;
            this.action4.Location = new System.Drawing.Point( 3, 72 );
            this.action4.Name = "action4";
            this.action4.Size = new System.Drawing.Size( 61, 17 );
            this.action4.TabIndex = 600;
            this.action4.Text = "action4";
            this.action4.UseVisualStyleBackColor = true;
            // 
            // action3
            // 
            this.action3.AutoSize = true;
            this.action3.Location = new System.Drawing.Point( 3, 49 );
            this.action3.Name = "action3";
            this.action3.Size = new System.Drawing.Size( 61, 17 );
            this.action3.TabIndex = 500;
            this.action3.Text = "action3";
            this.action3.UseVisualStyleBackColor = true;
            // 
            // action2
            // 
            this.action2.AutoSize = true;
            this.action2.Location = new System.Drawing.Point( 3, 26 );
            this.action2.Name = "action2";
            this.action2.Size = new System.Drawing.Size( 61, 17 );
            this.action2.TabIndex = 400;
            this.action2.Text = "action2";
            this.action2.UseVisualStyleBackColor = true;
            // 
            // action1
            // 
            this.action1.AutoSize = true;
            this.action1.Location = new System.Drawing.Point( 3, 3 );
            this.action1.Name = "action1";
            this.action1.Size = new System.Drawing.Size( 61, 17 );
            this.action1.TabIndex = 300;
            this.action1.Text = "action1";
            this.action1.UseVisualStyleBackColor = true;
            // 
            // action16
            // 
            this.action16.AutoSize = true;
            this.action16.Location = new System.Drawing.Point( 3, 348 );
            this.action16.Name = "action16";
            this.action16.Size = new System.Drawing.Size( 67, 17 );
            this.action16.TabIndex = 1800;
            this.action16.Text = "action16";
            this.action16.UseVisualStyleBackColor = true;
            // 
            // action15
            // 
            this.action15.AutoSize = true;
            this.action15.Location = new System.Drawing.Point( 3, 325 );
            this.action15.Name = "action15";
            this.action15.Size = new System.Drawing.Size( 67, 17 );
            this.action15.TabIndex = 1700;
            this.action15.Text = "action15";
            this.action15.UseVisualStyleBackColor = true;
            // 
            // action14
            // 
            this.action14.AutoSize = true;
            this.action14.Location = new System.Drawing.Point( 3, 302 );
            this.action14.Name = "action14";
            this.action14.Size = new System.Drawing.Size( 67, 17 );
            this.action14.TabIndex = 1600;
            this.action14.Text = "action14";
            this.action14.UseVisualStyleBackColor = true;
            // 
            // reactionGroup
            // 
            reactionGroup.Controls.Add( reactionTable );
            reactionGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            reactionGroup.Location = new System.Drawing.Point( 315, 43 );
            reactionGroup.Name = "reactionGroup";
            reactionGroup.Size = new System.Drawing.Size( 307, 106 );
            reactionGroup.TabIndex = 2;
            reactionGroup.TabStop = false;
            reactionGroup.Text = "Reaction";
            // 
            // reactionTable
            // 
            reactionTable.ColumnCount = 1;
            reactionTable.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            reactionTable.Controls.Add( this.reaction1, 0, 0 );
            reactionTable.Controls.Add( this.reaction2, 0, 1 );
            reactionTable.Controls.Add( this.reaction3, 0, 2 );
            reactionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            reactionTable.Location = new System.Drawing.Point( 3, 16 );
            reactionTable.Name = "reactionTable";
            reactionTable.RowCount = 3;
            reactionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            reactionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            reactionTable.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            reactionTable.Size = new System.Drawing.Size( 301, 87 );
            reactionTable.TabIndex = 0;
            // 
            // reaction1
            // 
            this.reaction1.AutoSize = true;
            this.reaction1.Location = new System.Drawing.Point( 3, 3 );
            this.reaction1.Name = "reaction1";
            this.reaction1.Size = new System.Drawing.Size( 70, 17 );
            this.reaction1.TabIndex = 1900;
            this.reaction1.Text = "reaction1";
            this.reaction1.UseVisualStyleBackColor = true;
            // 
            // reaction2
            // 
            this.reaction2.AutoSize = true;
            this.reaction2.Location = new System.Drawing.Point( 3, 26 );
            this.reaction2.Name = "reaction2";
            this.reaction2.Size = new System.Drawing.Size( 70, 17 );
            this.reaction2.TabIndex = 2000;
            this.reaction2.Text = "reaction2";
            this.reaction2.UseVisualStyleBackColor = true;
            // 
            // reaction3
            // 
            this.reaction3.AutoSize = true;
            this.reaction3.Location = new System.Drawing.Point( 3, 49 );
            this.reaction3.Name = "reaction3";
            this.reaction3.Size = new System.Drawing.Size( 70, 17 );
            this.reaction3.TabIndex = 2100;
            this.reaction3.Text = "reaction3";
            this.reaction3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add( this.jpSpinner );
            panel1.Controls.Add( jpLabel );
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point( 3, 3 );
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size( 306, 34 );
            panel1.TabIndex = 6;
            // 
            // jpSpinner
            // 
            this.jpSpinner.Location = new System.Drawing.Point( 63, 5 );
            this.jpSpinner.Maximum = new decimal( new int[] {
            9999,
            0,
            0,
            0} );
            this.jpSpinner.Name = "jpSpinner";
            this.jpSpinner.Size = new System.Drawing.Size( 67, 20 );
            this.jpSpinner.TabIndex = 1;
            // 
            // jpLabel
            // 
            jpLabel.AutoSize = true;
            jpLabel.Location = new System.Drawing.Point( 7, 9 );
            jpLabel.Name = "jpLabel";
            jpLabel.Size = new System.Drawing.Size( 22, 13 );
            jpLabel.TabIndex = 0;
            jpLabel.Text = "JP:";
            // 
            // JobEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( jobEditorTable );
            this.Name = "JobEditor";
            this.Size = new System.Drawing.Size( 625, 518 );
            jobEditorTable.ResumeLayout( false );
            panel2.ResumeLayout( false );
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalSpinner)).EndInit();
            movementGroup.ResumeLayout( false );
            movementTable.ResumeLayout( false );
            movementTable.PerformLayout();
            supportGroup.ResumeLayout( false );
            supportTable.ResumeLayout( false );
            supportTable.PerformLayout();
            actionGroup.ResumeLayout( false );
            actionTable.ResumeLayout( false );
            actionTable.PerformLayout();
            reactionGroup.ResumeLayout( false );
            reactionTable.ResumeLayout( false );
            reactionTable.PerformLayout();
            panel1.ResumeLayout( false );
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jpSpinner)).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.CheckBox action13;
        private System.Windows.Forms.CheckBox action16;
        private System.Windows.Forms.CheckBox action15;
        private System.Windows.Forms.CheckBox action14;
        private System.Windows.Forms.CheckBox action12;
        private System.Windows.Forms.CheckBox action11;
        private System.Windows.Forms.CheckBox action10;
        private System.Windows.Forms.CheckBox action9;
        private System.Windows.Forms.CheckBox action8;
        private System.Windows.Forms.CheckBox action7;
        private System.Windows.Forms.CheckBox action6;
        private System.Windows.Forms.CheckBox action5;
        private System.Windows.Forms.CheckBox action4;
        private System.Windows.Forms.CheckBox action3;
        private System.Windows.Forms.CheckBox action2;
        private System.Windows.Forms.CheckBox action1;
        private System.Windows.Forms.CheckBox movement2;
        private System.Windows.Forms.CheckBox movement1;
        private System.Windows.Forms.CheckBox support1;
        private System.Windows.Forms.CheckBox support2;
        private System.Windows.Forms.CheckBox support3;
        private System.Windows.Forms.CheckBox support4;
        private System.Windows.Forms.CheckBox reaction1;
        private System.Windows.Forms.CheckBox reaction2;
        private System.Windows.Forms.CheckBox reaction3;
        private System.Windows.Forms.NumericUpDown jpSpinner;
        private System.Windows.Forms.NumericUpDown totalSpinner;
    }
}
