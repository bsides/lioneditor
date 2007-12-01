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
            System.Windows.Forms.SplitContainer topSplitContainer;
            System.Windows.Forms.Panel dateWarFundsPanel;
            System.Windows.Forms.GroupBox killGroupBox;
            System.Windows.Forms.TableLayoutPanel killsTables;
            this.entireTable = new System.Windows.Forms.TableLayoutPanel();
            this.pageSelector = new System.Windows.Forms.ListBox();
            this.warFunds = new System.Windows.Forms.NumericUpDown();
            this.killsSpinner = new System.Windows.Forms.NumericUpDown();
            this.casualtiesSpinner = new System.Windows.Forms.NumericUpDown();
            this.date = new LionEditor.StupidDateEditor();
            this.timerEditor = new LionEditor.TimerEditor();
            killsLabel = new System.Windows.Forms.Label();
            casualtiesLabel = new System.Windows.Forms.Label();
            dateLabel = new System.Windows.Forms.Label();
            warFundsLabel = new System.Windows.Forms.Label();
            topSplitContainer = new System.Windows.Forms.SplitContainer();
            dateWarFundsPanel = new System.Windows.Forms.Panel();
            killGroupBox = new System.Windows.Forms.GroupBox();
            killsTables = new System.Windows.Forms.TableLayoutPanel();
            this.entireTable.SuspendLayout();
            topSplitContainer.Panel1.SuspendLayout();
            topSplitContainer.Panel2.SuspendLayout();
            topSplitContainer.SuspendLayout();
            dateWarFundsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.warFunds)).BeginInit();
            killGroupBox.SuspendLayout();
            killsTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.killsSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.casualtiesSpinner)).BeginInit();
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
            // dateLabel
            // 
            dateLabel.AutoSize = true;
            dateLabel.Location = new System.Drawing.Point( 236, 22 );
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new System.Drawing.Size( 33, 13 );
            dateLabel.TabIndex = 1;
            dateLabel.Text = "Date:";
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
            // entireTable
            // 
            this.entireTable.ColumnCount = 2;
            this.entireTable.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 10.76487F ) );
            this.entireTable.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 89.23513F ) );
            this.entireTable.Controls.Add( this.pageSelector, 0, 0 );
            this.entireTable.Controls.Add( topSplitContainer, 1, 0 );
            this.entireTable.Controls.Add( this.timerEditor, 1, 2 );
            this.entireTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entireTable.Location = new System.Drawing.Point( 0, 0 );
            this.entireTable.Name = "entireTable";
            this.entireTable.RowCount = 3;
            this.entireTable.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 21.70543F ) );
            this.entireTable.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 78.29457F ) );
            this.entireTable.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 56F ) );
            this.entireTable.Size = new System.Drawing.Size( 706, 439 );
            this.entireTable.TabIndex = 0;
            // 
            // pageSelector
            // 
            this.pageSelector.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageSelector.FormattingEnabled = true;
            this.pageSelector.Items.AddRange( new object[] {
            "Events",
            "Personae",
            "Feats",
            "Wonders",
            "Artefacts"} );
            this.pageSelector.Location = new System.Drawing.Point( 3, 3 );
            this.pageSelector.Name = "pageSelector";
            this.entireTable.SetRowSpan( this.pageSelector, 2 );
            this.pageSelector.Size = new System.Drawing.Size( 69, 238 );
            this.pageSelector.TabIndex = 0;
            // 
            // topSplitContainer
            // 
            topSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            topSplitContainer.Location = new System.Drawing.Point( 78, 3 );
            topSplitContainer.Name = "topSplitContainer";
            // 
            // topSplitContainer.Panel1
            // 
            topSplitContainer.Panel1.Controls.Add( dateWarFundsPanel );
            // 
            // topSplitContainer.Panel2
            // 
            topSplitContainer.Panel2.Controls.Add( killGroupBox );
            topSplitContainer.Size = new System.Drawing.Size( 625, 77 );
            topSplitContainer.SplitterDistance = 461;
            topSplitContainer.TabIndex = 1;
            // 
            // dateWarFundsPanel
            // 
            dateWarFundsPanel.Controls.Add( warFundsLabel );
            dateWarFundsPanel.Controls.Add( this.warFunds );
            dateWarFundsPanel.Controls.Add( dateLabel );
            dateWarFundsPanel.Controls.Add( this.date );
            dateWarFundsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            dateWarFundsPanel.Location = new System.Drawing.Point( 0, 0 );
            dateWarFundsPanel.Name = "dateWarFundsPanel";
            dateWarFundsPanel.Size = new System.Drawing.Size( 461, 77 );
            dateWarFundsPanel.TabIndex = 0;
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
            // killGroupBox
            // 
            killGroupBox.Controls.Add( killsTables );
            killGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            killGroupBox.Location = new System.Drawing.Point( 0, 0 );
            killGroupBox.Name = "killGroupBox";
            killGroupBox.Padding = new System.Windows.Forms.Padding( 3, 0, 3, 3 );
            killGroupBox.Size = new System.Drawing.Size( 160, 77 );
            killGroupBox.TabIndex = 0;
            killGroupBox.TabStop = false;
            // 
            // killsTables
            // 
            killsTables.ColumnCount = 2;
            killsTables.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 42.20779F ) );
            killsTables.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 57.79221F ) );
            killsTables.Controls.Add( killsLabel, 0, 0 );
            killsTables.Controls.Add( casualtiesLabel, 0, 1 );
            killsTables.Controls.Add( this.killsSpinner, 1, 0 );
            killsTables.Controls.Add( this.casualtiesSpinner, 1, 1 );
            killsTables.Dock = System.Windows.Forms.DockStyle.Fill;
            killsTables.Location = new System.Drawing.Point( 3, 13 );
            killsTables.Name = "killsTables";
            killsTables.RowCount = 2;
            killsTables.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            killsTables.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            killsTables.Size = new System.Drawing.Size( 154, 61 );
            killsTables.TabIndex = 0;
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
            // date
            // 
            this.date.Location = new System.Drawing.Point( 275, 20 );
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size( 183, 29 );
            this.date.TabIndex = 0;
            // 
            // timerEditor
            // 
            this.timerEditor.Dock = System.Windows.Forms.DockStyle.Right;
            this.timerEditor.Location = new System.Drawing.Point( 438, 385 );
            this.timerEditor.Name = "timerEditor";
            this.timerEditor.Size = new System.Drawing.Size( 265, 51 );
            this.timerEditor.TabIndex = 2;
            this.timerEditor.Value = ((uint)(0u));
            // 
            // ChronicleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.entireTable );
            this.Name = "ChronicleEditor";
            this.Size = new System.Drawing.Size( 706, 439 );
            this.entireTable.ResumeLayout( false );
            topSplitContainer.Panel1.ResumeLayout( false );
            topSplitContainer.Panel2.ResumeLayout( false );
            topSplitContainer.ResumeLayout( false );
            dateWarFundsPanel.ResumeLayout( false );
            dateWarFundsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.warFunds)).EndInit();
            killGroupBox.ResumeLayout( false );
            killsTables.ResumeLayout( false );
            killsTables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.killsSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.casualtiesSpinner)).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ListBox pageSelector;
        private System.Windows.Forms.NumericUpDown killsSpinner;
        private System.Windows.Forms.NumericUpDown casualtiesSpinner;
        private TimerEditor timerEditor;
        private StupidDateEditor date;
        private System.Windows.Forms.NumericUpDown warFunds;
        private System.Windows.Forms.TableLayoutPanel entireTable;
    }
}
