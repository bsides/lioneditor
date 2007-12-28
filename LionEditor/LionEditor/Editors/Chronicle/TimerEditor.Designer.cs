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
    partial class TimerEditor
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
            System.Windows.Forms.TableLayoutPanel table;
            System.Windows.Forms.Label colon1;
            System.Windows.Forms.Label colon2;
            System.Windows.Forms.Label hoursLabel;
            System.Windows.Forms.Label minutesLabel;
            System.Windows.Forms.Label secondsLabel;
            this.hoursSpinner = new System.Windows.Forms.NumericUpDown();
            this.minutesSpinner = new System.Windows.Forms.NumericUpDown();
            this.secondsSpinner = new System.Windows.Forms.NumericUpDown();
            table = new System.Windows.Forms.TableLayoutPanel();
            colon1 = new System.Windows.Forms.Label();
            colon2 = new System.Windows.Forms.Label();
            hoursLabel = new System.Windows.Forms.Label();
            minutesLabel = new System.Windows.Forms.Label();
            secondsLabel = new System.Windows.Forms.Label();
            table.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hoursSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutesSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondsSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // table
            // 
            table.ColumnCount = 5;
            table.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 108F ) );
            table.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            table.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 60F ) );
            table.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            table.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 65F ) );
            table.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
            table.Controls.Add( colon1, 1, 1 );
            table.Controls.Add( colon2, 3, 1 );
            table.Controls.Add( this.hoursSpinner, 0, 1 );
            table.Controls.Add( this.minutesSpinner, 2, 1 );
            table.Controls.Add( this.secondsSpinner, 4, 1 );
            table.Controls.Add( hoursLabel, 0, 0 );
            table.Controls.Add( minutesLabel, 2, 0 );
            table.Controls.Add( secondsLabel, 4, 0 );
            table.Location = new System.Drawing.Point( 0, 0 );
            table.Name = "table";
            table.RowCount = 2;
            table.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            table.Size = new System.Drawing.Size( 265, 48 );
            table.TabIndex = 0;
            // 
            // colon1
            // 
            colon1.AutoSize = true;
            colon1.Dock = System.Windows.Forms.DockStyle.Fill;
            colon1.Location = new System.Drawing.Point( 111, 20 );
            colon1.Name = "colon1";
            colon1.Size = new System.Drawing.Size( 10, 28 );
            colon1.TabIndex = 0;
            colon1.Text = ":";
            colon1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // colon2
            // 
            colon2.AutoSize = true;
            colon2.Dock = System.Windows.Forms.DockStyle.Fill;
            colon2.Location = new System.Drawing.Point( 187, 20 );
            colon2.Name = "colon2";
            colon2.Size = new System.Drawing.Size( 10, 28 );
            colon2.TabIndex = 1;
            colon2.Text = ":";
            colon2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hoursSpinner
            // 
            this.hoursSpinner.Location = new System.Drawing.Point( 3, 23 );
            this.hoursSpinner.Maximum = new decimal( new int[] {
            1193046,
            0,
            0,
            0} );
            this.hoursSpinner.Name = "hoursSpinner";
            this.hoursSpinner.Size = new System.Drawing.Size( 102, 20 );
            this.hoursSpinner.TabIndex = 2;
            this.hoursSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // minutesSpinner
            // 
            this.minutesSpinner.Location = new System.Drawing.Point( 127, 23 );
            this.minutesSpinner.Maximum = new decimal( new int[] {
            59,
            0,
            0,
            0} );
            this.minutesSpinner.Name = "minutesSpinner";
            this.minutesSpinner.Size = new System.Drawing.Size( 54, 20 );
            this.minutesSpinner.TabIndex = 3;
            this.minutesSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // secondsSpinner
            // 
            this.secondsSpinner.Location = new System.Drawing.Point( 203, 23 );
            this.secondsSpinner.Maximum = new decimal( new int[] {
            59,
            0,
            0,
            0} );
            this.secondsSpinner.Name = "secondsSpinner";
            this.secondsSpinner.Size = new System.Drawing.Size( 59, 20 );
            this.secondsSpinner.TabIndex = 4;
            this.secondsSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // hoursLabel
            // 
            hoursLabel.AutoSize = true;
            hoursLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            hoursLabel.Location = new System.Drawing.Point( 3, 0 );
            hoursLabel.Name = "hoursLabel";
            hoursLabel.Size = new System.Drawing.Size( 102, 20 );
            hoursLabel.TabIndex = 5;
            hoursLabel.Text = "Hours";
            hoursLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // minutesLabel
            // 
            minutesLabel.AutoSize = true;
            minutesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            minutesLabel.Location = new System.Drawing.Point( 127, 0 );
            minutesLabel.Name = "minutesLabel";
            minutesLabel.Size = new System.Drawing.Size( 54, 20 );
            minutesLabel.TabIndex = 6;
            minutesLabel.Text = "Minutes";
            minutesLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // secondsLabel
            // 
            secondsLabel.AutoSize = true;
            secondsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            secondsLabel.Location = new System.Drawing.Point( 203, 0 );
            secondsLabel.Name = "secondsLabel";
            secondsLabel.Size = new System.Drawing.Size( 59, 20 );
            secondsLabel.TabIndex = 7;
            secondsLabel.Text = "Seconds";
            secondsLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // TimerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( table );
            this.Name = "TimerEditor";
            this.Size = new System.Drawing.Size( 265, 48 );
            table.ResumeLayout( false );
            table.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hoursSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutesSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondsSpinner)).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.NumericUpDown hoursSpinner;
        private System.Windows.Forms.NumericUpDown minutesSpinner;
        private System.Windows.Forms.NumericUpDown secondsSpinner;
    }
}
