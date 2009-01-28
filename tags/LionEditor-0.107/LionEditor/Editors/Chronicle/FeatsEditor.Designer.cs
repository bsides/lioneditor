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
            System.Windows.Forms.TableLayoutPanel table;
            this.stateComboBox = new System.Windows.Forms.ComboBox();
            this.stupidDateEditor = new LionEditor.StupidDateEditor();
            this.featListBox = new System.Windows.Forms.ListBox();
            nameLabel = new System.Windows.Forms.Label();
            stateLabel = new System.Windows.Forms.Label();
            dateLabel = new System.Windows.Forms.Label();
            table = new System.Windows.Forms.TableLayoutPanel();
            table.SuspendLayout();
            this.SuspendLayout();
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
            // stateComboBox
            // 
            this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stateComboBox.FormattingEnabled = true;
            this.stateComboBox.Location = new System.Drawing.Point( 225, 16 );
            this.stateComboBox.Name = "stateComboBox";
            this.stateComboBox.Size = new System.Drawing.Size( 82, 21 );
            this.stateComboBox.TabIndex = 2;
            // 
            // table
            // 
            table.ColumnCount = 3;
            table.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 222F ) );
            table.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 88F ) );
            table.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 180F ) );
            table.Controls.Add( this.stupidDateEditor, 2, 1 );
            table.Controls.Add( this.stateComboBox, 1, 1 );
            table.Controls.Add( nameLabel, 0, 0 );
            table.Controls.Add( stateLabel, 1, 0 );
            table.Controls.Add( dateLabel, 2, 0 );
            table.Controls.Add( this.featListBox, 0, 1 );
            table.Dock = System.Windows.Forms.DockStyle.Fill;
            table.Location = new System.Drawing.Point( 0, 0 );
            table.Name = "table";
            table.RowCount = 2;
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
            table.Size = new System.Drawing.Size( 490, 298 );
            table.TabIndex = 3;
            // 
            // stupidDateEditor
            // 
            this.stupidDateEditor.Location = new System.Drawing.Point( 313, 16 );
            this.stupidDateEditor.Name = "stupidDateEditor";
            this.stupidDateEditor.Size = new System.Drawing.Size( 174, 27 );
            this.stupidDateEditor.TabIndex = 0;
            // 
            // featListBox
            // 
            this.featListBox.FormattingEnabled = true;
            this.featListBox.Location = new System.Drawing.Point( 3, 16 );
            this.featListBox.Name = "featListBox";
            this.featListBox.Size = new System.Drawing.Size( 216, 251 );
            this.featListBox.TabIndex = 6;
            // 
            // FeatsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( table );
            this.Name = "FeatsEditor";
            this.Size = new System.Drawing.Size( 490, 298 );
            table.ResumeLayout( false );
            table.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private StupidDateEditor stupidDateEditor;
        private System.Windows.Forms.ComboBox stateComboBox;
        private System.Windows.Forms.ListBox featListBox;

    }
}
