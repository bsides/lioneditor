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

namespace FFTPatcher.SpriteEditor
{
    partial class SpriteDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.paletteGroupBox = new System.Windows.Forms.GroupBox();
            this.portraitCheckbox = new System.Windows.Forms.CheckBox();
            this.paletteSelector = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.shapesListBox = new System.Windows.Forms.ListBox();
            this.properCheckbox = new System.Windows.Forms.CheckBox();
            this.spriteViewer1 = new FFTPatcher.SpriteEditor.SpriteViewer();
            this.paletteGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // paletteGroupBox
            // 
            this.paletteGroupBox.AutoSize = true;
            this.paletteGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.paletteGroupBox.Controls.Add( this.portraitCheckbox );
            this.paletteGroupBox.Controls.Add( this.paletteSelector );
            this.paletteGroupBox.Location = new System.Drawing.Point( 282, 3 );
            this.paletteGroupBox.Name = "paletteGroupBox";
            this.paletteGroupBox.Size = new System.Drawing.Size( 185, 106 );
            this.paletteGroupBox.TabIndex = 2;
            this.paletteGroupBox.TabStop = false;
            this.paletteGroupBox.Text = "Palette";
            // 
            // portraitCheckbox
            // 
            this.portraitCheckbox.Checked = true;
            this.portraitCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.portraitCheckbox.Location = new System.Drawing.Point( 6, 50 );
            this.portraitCheckbox.Name = "portraitCheckbox";
            this.portraitCheckbox.Size = new System.Drawing.Size( 153, 37 );
            this.portraitCheckbox.TabIndex = 3;
            this.portraitCheckbox.Text = "Always use corresponding palette for portrait";
            this.portraitCheckbox.UseVisualStyleBackColor = true;
            // 
            // paletteSelector
            // 
            this.paletteSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteSelector.FormattingEnabled = true;
            this.paletteSelector.Items.AddRange( new object[] {
            "Sprite #1",
            "Sprite #2",
            "Sprite #3",
            "Sprite #4",
            "Sprite #5",
            "Sprite #6",
            "Sprite #7",
            "Sprite #8",
            "Portrait #1",
            "Portrait #2",
            "Portrait #3",
            "Portrait #4",
            "Portrait #5",
            "Portrait #6",
            "Portrait #7",
            "Portrait #8"} );
            this.paletteSelector.Location = new System.Drawing.Point( 6, 19 );
            this.paletteSelector.Name = "paletteSelector";
            this.paletteSelector.Size = new System.Drawing.Size( 173, 21 );
            this.paletteSelector.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add( this.spriteViewer1 );
            this.panel1.Location = new System.Drawing.Point( 10, 3 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 266, 498 );
            this.panel1.TabIndex = 1;
            // 
            // shapesListBox
            // 
            this.shapesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shapesListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.shapesListBox.FormattingEnabled = true;
            this.shapesListBox.IntegralHeight = false;
            this.shapesListBox.ItemHeight = 180;
            this.shapesListBox.Location = new System.Drawing.Point( 283, 140 );
            this.shapesListBox.Name = "shapesListBox";
            this.shapesListBox.Size = new System.Drawing.Size( 245, 391 );
            this.shapesListBox.TabIndex = 3;
            // 
            // properCheckbox
            // 
            this.properCheckbox.AutoSize = true;
            this.properCheckbox.Checked = true;
            this.properCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.properCheckbox.Location = new System.Drawing.Point( 288, 117 );
            this.properCheckbox.Name = "properCheckbox";
            this.properCheckbox.Size = new System.Drawing.Size( 105, 17 );
            this.properCheckbox.TabIndex = 6;
            this.properCheckbox.Text = "Proper alignment";
            this.properCheckbox.UseVisualStyleBackColor = true;
            // 
            // spriteViewer1
            // 
            this.spriteViewer1.Location = new System.Drawing.Point( 3, 3 );
            this.spriteViewer1.Name = "spriteViewer1";
            this.spriteViewer1.Proper = true;
            this.spriteViewer1.Size = new System.Drawing.Size( 256, 488 );
            this.spriteViewer1.Sprite = null;
            this.spriteViewer1.TabIndex = 0;
            // 
            // SpriteDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 540, 595 );
            this.Controls.Add( this.properCheckbox );
            this.Controls.Add( this.shapesListBox );
            this.Controls.Add( this.paletteGroupBox );
            this.Controls.Add( this.panel1 );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size( 488, 560 );
            this.Name = "SpriteDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Shishi Sprite Manager";
            this.paletteGroupBox.ResumeLayout( false );
            this.panel1.ResumeLayout( false );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private SpriteViewer spriteViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox paletteSelector;
        private System.Windows.Forms.CheckBox portraitCheckbox;
        private System.Windows.Forms.ListBox shapesListBox;
        private System.Windows.Forms.CheckBox properCheckbox;
        private System.Windows.Forms.GroupBox paletteGroupBox;
    }
}

