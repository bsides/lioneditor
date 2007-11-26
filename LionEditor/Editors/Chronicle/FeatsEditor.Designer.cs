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
    partial class FeatsEditor
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
            System.Windows.Forms.Label nameLabel;
            System.Windows.Forms.Label stateLabel;
            System.Windows.Forms.Label dateLabel;
            this.featComboBox = new System.Windows.Forms.ComboBox();
            this.stateComboBox = new System.Windows.Forms.ComboBox();
            this.stupidDateEditor1 = new LionEditor.Editors.Chronicle.StupidDateEditor();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            nameLabel = new System.Windows.Forms.Label();
            stateLabel = new System.Windows.Forms.Label();
            dateLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // featComboBox
            // 
            this.featComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.featComboBox.FormattingEnabled = true;
            this.featComboBox.Location = new System.Drawing.Point( 3, 16 );
            this.featComboBox.Name = "featComboBox";
            this.featComboBox.Size = new System.Drawing.Size( 216, 21 );
            this.featComboBox.TabIndex = 1;
            // 
            // stateComboBox
            // 
            this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stateComboBox.FormattingEnabled = true;
            this.stateComboBox.Location = new System.Drawing.Point( 225, 16 );
            this.stateComboBox.Name = "stateComboBox";
            this.stateComboBox.Size = new System.Drawing.Size( 82, 21 );
            this.stateComboBox.TabIndex = 2;
            // 
            // stupidDateEditor1
            // 
            this.stupidDateEditor1.Location = new System.Drawing.Point( 313, 16 );
            this.stupidDateEditor1.Name = "stupidDateEditor1";
            this.stupidDateEditor1.Size = new System.Drawing.Size( 174, 27 );
            this.stupidDateEditor1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 222F ) );
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 88F ) );
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 112F ) );
            this.tableLayoutPanel1.Controls.Add( this.featComboBox, 0, 1 );
            this.tableLayoutPanel1.Controls.Add( this.stupidDateEditor1, 2, 1 );
            this.tableLayoutPanel1.Controls.Add( this.stateComboBox, 1, 1 );
            this.tableLayoutPanel1.Controls.Add( nameLabel, 0, 0 );
            this.tableLayoutPanel1.Controls.Add( stateLabel, 1, 0 );
            this.tableLayoutPanel1.Controls.Add( dateLabel, 2, 0 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point( 0, 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            this.tableLayoutPanel1.Size = new System.Drawing.Size( 490, 53 );
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nameLabel.Location = new System.Drawing.Point( 3, 0 );
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size( 216, 13 );
            nameLabel.TabIndex = 3;
            nameLabel.Text = "Name";
            nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stateLabel
            // 
            stateLabel.AutoSize = true;
            stateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            stateLabel.Location = new System.Drawing.Point( 225, 0 );
            stateLabel.Name = "stateLabel";
            stateLabel.Size = new System.Drawing.Size( 82, 13 );
            stateLabel.TabIndex = 4;
            stateLabel.Text = "State";
            stateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateLabel
            // 
            dateLabel.AutoSize = true;
            dateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            dateLabel.Location = new System.Drawing.Point( 313, 0 );
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new System.Drawing.Size( 174, 13 );
            dateLabel.TabIndex = 5;
            dateLabel.Text = "Date";
            dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FeatsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.tableLayoutPanel1 );
            this.Name = "FeatsEditor";
            this.Size = new System.Drawing.Size( 490, 53 );
            this.tableLayoutPanel1.ResumeLayout( false );
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private LionEditor.Editors.Chronicle.StupidDateEditor stupidDateEditor1;
        private System.Windows.Forms.ComboBox featComboBox;
        private System.Windows.Forms.ComboBox stateComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}
