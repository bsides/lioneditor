/*
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
    partial class GlyphEditor
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
            System.Windows.Forms.Panel thumbnailPanelPanel;
            System.Windows.Forms.Label widthLabel;
            this.thumbnailPanel = new System.Windows.Forms.Panel();
            this.glyphPanel = new System.Windows.Forms.Panel();
            this.glyphPanelPanel = new System.Windows.Forms.Panel();
            this.widthSpinner = new System.Windows.Forms.NumericUpDown();
            this.blackRadioButton = new System.Windows.Forms.RadioButton();
            this.darkRadioButton = new System.Windows.Forms.RadioButton();
            this.lightRadioButton = new System.Windows.Forms.RadioButton();
            this.transparentRadioButton = new System.Windows.Forms.RadioButton();
            thumbnailPanelPanel = new System.Windows.Forms.Panel();
            widthLabel = new System.Windows.Forms.Label();
            thumbnailPanelPanel.SuspendLayout();
            this.glyphPanelPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widthSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // thumbnailPanelPanel
            // 
            thumbnailPanelPanel.AutoSize = true;
            thumbnailPanelPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            thumbnailPanelPanel.BackgroundImage = global::FFTPatcher.Properties.Resources.bg;
            thumbnailPanelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            thumbnailPanelPanel.Controls.Add( this.thumbnailPanel );
            thumbnailPanelPanel.Location = new System.Drawing.Point( 164, 3 );
            thumbnailPanelPanel.Name = "thumbnailPanelPanel";
            thumbnailPanelPanel.Size = new System.Drawing.Size( 28, 36 );
            thumbnailPanelPanel.TabIndex = 2;
            // 
            // thumbnailPanel
            // 
            this.thumbnailPanel.BackColor = System.Drawing.Color.Transparent;
            this.thumbnailPanel.Location = new System.Drawing.Point( 3, 3 );
            this.thumbnailPanel.Name = "thumbnailPanel";
            this.thumbnailPanel.Size = new System.Drawing.Size( 20, 28 );
            this.thumbnailPanel.TabIndex = 1;
            // 
            // glyphPanel
            // 
            this.glyphPanel.BackColor = System.Drawing.Color.Transparent;
            this.glyphPanel.Location = new System.Drawing.Point( 3, 5 );
            this.glyphPanel.MaximumSize = new System.Drawing.Size( 150, 210 );
            this.glyphPanel.MinimumSize = new System.Drawing.Size( 150, 210 );
            this.glyphPanel.Name = "glyphPanel";
            this.glyphPanel.Size = new System.Drawing.Size( 150, 210 );
            this.glyphPanel.TabIndex = 0;
            // 
            // glyphPanelPanel
            // 
            this.glyphPanelPanel.AutoSize = true;
            this.glyphPanelPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.glyphPanelPanel.BackgroundImage = global::FFTPatcher.Properties.Resources.bg;
            this.glyphPanelPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glyphPanelPanel.Controls.Add( this.glyphPanel );
            this.glyphPanelPanel.Location = new System.Drawing.Point( 3, 3 );
            this.glyphPanelPanel.Name = "glyphPanelPanel";
            this.glyphPanelPanel.Size = new System.Drawing.Size( 160, 222 );
            this.glyphPanelPanel.TabIndex = 3;
            // 
            // widthSpinner
            // 
            this.widthSpinner.Location = new System.Drawing.Point( 165, 151 );
            this.widthSpinner.Maximum = new decimal( new int[] {
            10,
            0,
            0,
            0} );
            this.widthSpinner.Name = "widthSpinner";
            this.widthSpinner.Size = new System.Drawing.Size( 42, 20 );
            this.widthSpinner.TabIndex = 4;
            this.widthSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.widthSpinner.Value = new decimal( new int[] {
            10,
            0,
            0,
            0} );
            // 
            // widthLabel
            // 
            widthLabel.AutoSize = true;
            widthLabel.Location = new System.Drawing.Point( 165, 132 );
            widthLabel.Name = "widthLabel";
            widthLabel.Size = new System.Drawing.Size( 35, 13 );
            widthLabel.TabIndex = 5;
            widthLabel.Text = "Width";
            // 
            // blackRadioButton
            // 
            this.blackRadioButton.AutoSize = true;
            this.blackRadioButton.Checked = true;
            this.blackRadioButton.Location = new System.Drawing.Point( 3, 226 );
            this.blackRadioButton.Name = "blackRadioButton";
            this.blackRadioButton.Size = new System.Drawing.Size( 52, 17 );
            this.blackRadioButton.TabIndex = 6;
            this.blackRadioButton.TabStop = true;
            this.blackRadioButton.Text = "Black";
            this.blackRadioButton.UseVisualStyleBackColor = true;
            // 
            // darkRadioButton
            // 
            this.darkRadioButton.AutoSize = true;
            this.darkRadioButton.Location = new System.Drawing.Point( 95, 226 );
            this.darkRadioButton.Name = "darkRadioButton";
            this.darkRadioButton.Size = new System.Drawing.Size( 48, 17 );
            this.darkRadioButton.TabIndex = 7;
            this.darkRadioButton.TabStop = true;
            this.darkRadioButton.Text = "Dark";
            this.darkRadioButton.UseVisualStyleBackColor = true;
            // 
            // lightRadioButton
            // 
            this.lightRadioButton.AutoSize = true;
            this.lightRadioButton.Location = new System.Drawing.Point( 3, 249 );
            this.lightRadioButton.Name = "lightRadioButton";
            this.lightRadioButton.Size = new System.Drawing.Size( 48, 17 );
            this.lightRadioButton.TabIndex = 8;
            this.lightRadioButton.TabStop = true;
            this.lightRadioButton.Text = "Light";
            this.lightRadioButton.UseVisualStyleBackColor = true;
            // 
            // transparentRadioButton
            // 
            this.transparentRadioButton.AutoSize = true;
            this.transparentRadioButton.Location = new System.Drawing.Point( 95, 249 );
            this.transparentRadioButton.Name = "transparentRadioButton";
            this.transparentRadioButton.Size = new System.Drawing.Size( 82, 17 );
            this.transparentRadioButton.TabIndex = 9;
            this.transparentRadioButton.TabStop = true;
            this.transparentRadioButton.Text = "Transparent";
            this.transparentRadioButton.UseVisualStyleBackColor = true;
            // 
            // GlyphEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add( this.transparentRadioButton );
            this.Controls.Add( this.lightRadioButton );
            this.Controls.Add( this.darkRadioButton );
            this.Controls.Add( this.blackRadioButton );
            this.Controls.Add( widthLabel );
            this.Controls.Add( this.widthSpinner );
            this.Controls.Add( this.glyphPanelPanel );
            this.Controls.Add( thumbnailPanelPanel );
            this.Name = "GlyphEditor";
            this.Size = new System.Drawing.Size( 210, 269 );
            thumbnailPanelPanel.ResumeLayout( false );
            this.glyphPanelPanel.ResumeLayout( false );
            ((System.ComponentModel.ISupportInitialize)(this.widthSpinner)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel glyphPanel;
        private System.Windows.Forms.Panel thumbnailPanel;
        private System.Windows.Forms.Panel glyphPanelPanel;
        private System.Windows.Forms.NumericUpDown widthSpinner;
        private System.Windows.Forms.RadioButton blackRadioButton;
        private System.Windows.Forms.RadioButton darkRadioButton;
        private System.Windows.Forms.RadioButton lightRadioButton;
        private System.Windows.Forms.RadioButton transparentRadioButton;

    }
}
