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
    partial class ChronicleEditor
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
            System.Windows.Forms.Label killsLabel;
            System.Windows.Forms.Label casualtiesLabel;
            System.Windows.Forms.Label dateLabel;
            System.Windows.Forms.Label warFundsLabel;
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.killsSpinner = new System.Windows.Forms.NumericUpDown();
            this.casualtiesSpinner = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.date = new LionEditor.Editors.Chronicle.StupidDateEditor();
            this.timerEditor1 = new LionEditor.Editors.Chronicle.TimerEditor();
            this.warFunds = new System.Windows.Forms.NumericUpDown();
            killsLabel = new System.Windows.Forms.Label();
            casualtiesLabel = new System.Windows.Forms.Label();
            dateLabel = new System.Windows.Forms.Label();
            warFundsLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.killsSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.casualtiesSpinner)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.warFunds)).BeginInit();
            this.SuspendLayout();
            // 
            // killsLabel
            // 
            killsLabel.AutoSize = true;
            killsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            killsLabel.Location = new System.Drawing.Point( 3, 0 );
            killsLabel.Name = "killsLabel";
            killsLabel.Size = new System.Drawing.Size( 58, 30 );
            killsLabel.TabIndex = 0;
            killsLabel.Text = "Kills:";
            killsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // casualtiesLabel
            // 
            casualtiesLabel.AutoSize = true;
            casualtiesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            casualtiesLabel.Location = new System.Drawing.Point( 3, 30 );
            casualtiesLabel.Name = "casualtiesLabel";
            casualtiesLabel.Size = new System.Drawing.Size( 58, 31 );
            casualtiesLabel.TabIndex = 1;
            casualtiesLabel.Text = "Casualties:";
            casualtiesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 10.76487F ) );
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 89.23513F ) );
            this.tableLayoutPanel1.Controls.Add( this.listBox1, 0, 0 );
            this.tableLayoutPanel1.Controls.Add( this.splitContainer1, 1, 0 );
            this.tableLayoutPanel1.Controls.Add( this.timerEditor1, 1, 2 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point( 0, 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 21.70543F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 78.29457F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 56F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size( 706, 439 );
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange( new object[] {
            "Events",
            "Personae",
            "Feats",
            "Wonders",
            "Artefacts"} );
            this.listBox1.Location = new System.Drawing.Point( 3, 3 );
            this.listBox1.Name = "listBox1";
            this.tableLayoutPanel1.SetRowSpan( this.listBox1, 2 );
            this.listBox1.Size = new System.Drawing.Size( 69, 238 );
            this.listBox1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point( 78, 3 );
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add( this.panel1 );
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add( this.groupBox1 );
            this.splitContainer1.Size = new System.Drawing.Size( 625, 77 );
            this.splitContainer1.SplitterDistance = 461;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.tableLayoutPanel2 );
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point( 0, 0 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding( 3, 0, 3, 3 );
            this.groupBox1.Size = new System.Drawing.Size( 160, 77 );
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 42.20779F ) );
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 57.79221F ) );
            this.tableLayoutPanel2.Controls.Add( killsLabel, 0, 0 );
            this.tableLayoutPanel2.Controls.Add( casualtiesLabel, 0, 1 );
            this.tableLayoutPanel2.Controls.Add( this.killsSpinner, 1, 0 );
            this.tableLayoutPanel2.Controls.Add( this.casualtiesSpinner, 1, 1 );
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point( 3, 13 );
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel2.Size = new System.Drawing.Size( 154, 61 );
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // killsSpinner
            // 
            this.killsSpinner.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.killsSpinner.Location = new System.Drawing.Point( 67, 7 );
            this.killsSpinner.Maximum = new decimal( new int[] {
            9999,
            0,
            0,
            0} );
            this.killsSpinner.Name = "killsSpinner";
            this.killsSpinner.Size = new System.Drawing.Size( 84, 20 );
            this.killsSpinner.TabIndex = 2;
            this.killsSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.killsSpinner.ThousandsSeparator = true;
            // 
            // casualtiesSpinner
            // 
            this.casualtiesSpinner.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.casualtiesSpinner.Location = new System.Drawing.Point( 67, 38 );
            this.casualtiesSpinner.Maximum = new decimal( new int[] {
            9999,
            0,
            0,
            0} );
            this.casualtiesSpinner.Name = "casualtiesSpinner";
            this.casualtiesSpinner.Size = new System.Drawing.Size( 84, 20 );
            this.casualtiesSpinner.TabIndex = 3;
            this.casualtiesSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.casualtiesSpinner.ThousandsSeparator = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add( warFundsLabel );
            this.panel1.Controls.Add( this.warFunds );
            this.panel1.Controls.Add( dateLabel );
            this.panel1.Controls.Add( this.date );
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point( 0, 0 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 461, 77 );
            this.panel1.TabIndex = 0;
            // 
            // dateLabel
            // 
            dateLabel.AutoSize = true;
            dateLabel.Location = new System.Drawing.Point( 236, 22 );
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new System.Drawing.Size( 33, 13 );
            dateLabel.TabIndex = 1;
            dateLabel.Text = "Date:";
            // 
            // date
            // 
            this.date.Location = new System.Drawing.Point( 275, 20 );
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size( 183, 29 );
            this.date.TabIndex = 0;
            // 
            // timerEditor1
            // 
            this.timerEditor1.Dock = System.Windows.Forms.DockStyle.Right;
            this.timerEditor1.Location = new System.Drawing.Point( 438, 385 );
            this.timerEditor1.Name = "timerEditor1";
            this.timerEditor1.Size = new System.Drawing.Size( 265, 51 );
            this.timerEditor1.TabIndex = 2;
            this.timerEditor1.Value = ((uint)(0u));
            // 
            // warFunds
            // 
            this.warFunds.Location = new System.Drawing.Point( 275, 54 );
            this.warFunds.Maximum = new decimal( new int[] {
            99999999,
            0,
            0,
            0} );
            this.warFunds.Name = "warFunds";
            this.warFunds.Size = new System.Drawing.Size( 173, 20 );
            this.warFunds.TabIndex = 2;
            this.warFunds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.warFunds.ThousandsSeparator = true;
            // 
            // warFundsLabel
            // 
            warFundsLabel.AutoSize = true;
            warFundsLabel.Location = new System.Drawing.Point( 207, 57 );
            warFundsLabel.Name = "warFundsLabel";
            warFundsLabel.Size = new System.Drawing.Size( 62, 13 );
            warFundsLabel.TabIndex = 3;
            warFundsLabel.Text = "War Funds:";
            // 
            // ChronicleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.tableLayoutPanel1 );
            this.Name = "ChronicleEditor";
            this.Size = new System.Drawing.Size( 706, 439 );
            this.tableLayoutPanel1.ResumeLayout( false );
            this.splitContainer1.Panel1.ResumeLayout( false );
            this.splitContainer1.Panel2.ResumeLayout( false );
            this.splitContainer1.ResumeLayout( false );
            this.groupBox1.ResumeLayout( false );
            this.tableLayoutPanel2.ResumeLayout( false );
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.killsSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.casualtiesSpinner)).EndInit();
            this.panel1.ResumeLayout( false );
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.warFunds)).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.NumericUpDown killsSpinner;
        private System.Windows.Forms.NumericUpDown casualtiesSpinner;
        private LionEditor.Editors.Chronicle.TimerEditor timerEditor1;
        private System.Windows.Forms.Panel panel1;
        private LionEditor.Editors.Chronicle.StupidDateEditor date;
        private System.Windows.Forms.NumericUpDown warFunds;
    }
}
