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
    partial class AllItemsEditor
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
            this.itemListBox = new System.Windows.Forms.ListBox();
            this.itemEditor = new FFTPatcher.Editors.ItemEditor();
            this.SuspendLayout();
            // 
            // itemListBox
            // 
            this.itemListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.itemListBox.FormattingEnabled = true;
            this.itemListBox.Location = new System.Drawing.Point( 0, 0 );
            this.itemListBox.Name = "itemListBox";
            this.itemListBox.Size = new System.Drawing.Size( 120, 433 );
            this.itemListBox.TabIndex = 0;
            // 
            // itemEditor
            // 
            this.itemEditor.AutoSize = true;
            this.itemEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.itemEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemEditor.Item = null;
            this.itemEditor.Location = new System.Drawing.Point( 120, 0 );
            this.itemEditor.Name = "itemEditor";
            this.itemEditor.Size = new System.Drawing.Size( 583, 445 );
            this.itemEditor.TabIndex = 1;
            // 
            // AllItemsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.itemEditor );
            this.Controls.Add( this.itemListBox );
            this.Name = "AllItemsEditor";
            this.Size = new System.Drawing.Size( 703, 445 );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox itemListBox;
        private ItemEditor itemEditor;
    }
}
