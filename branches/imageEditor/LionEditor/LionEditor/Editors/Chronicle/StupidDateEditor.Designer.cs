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
    partial class StupidDateEditor
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
            this.monthCombo = new System.Windows.Forms.ComboBox();
            this.daySpinner = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.daySpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // monthCombo
            // 
            this.monthCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthCombo.FormattingEnabled = true;
            this.monthCombo.Location = new System.Drawing.Point( 52, 0 );
            this.monthCombo.Name = "monthCombo";
            this.monthCombo.Size = new System.Drawing.Size( 121, 21 );
            this.monthCombo.TabIndex = 0;
            // 
            // daySpinner
            // 
            this.daySpinner.Location = new System.Drawing.Point( 0, 0 );
            this.daySpinner.Maximum = new decimal( new int[] {
            32,
            0,
            0,
            0} );
            this.daySpinner.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.daySpinner.Name = "daySpinner";
            this.daySpinner.Size = new System.Drawing.Size( 46, 20 );
            this.daySpinner.TabIndex = 1;
            this.daySpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.daySpinner.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            // 
            // StupidDateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.daySpinner );
            this.Controls.Add( this.monthCombo );
            this.Name = "StupidDateEditor";
            this.Size = new System.Drawing.Size( 183, 29 );
            ((System.ComponentModel.ISupportInitialize)(this.daySpinner)).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ComboBox monthCombo;
        private System.Windows.Forms.NumericUpDown daySpinner;
    }
}
