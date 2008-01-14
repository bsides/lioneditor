namespace FFTPatcher.Editors
{
    partial class FontEditor
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.glyphEditor1 = new FFTPatcher.Editors.GlyphEditor();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Hexadecimal = true;
            this.numericUpDown1.Location = new System.Drawing.Point( 27, 14 );
            this.numericUpDown1.Maximum = new decimal( new int[] {
            2199,
            0,
            0,
            0} );
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size( 120, 20 );
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // glyphEditor2
            // 
            this.glyphEditor1.AutoSize = true;
            this.glyphEditor1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.glyphEditor1.Glyph = null;
            this.glyphEditor1.Location = new System.Drawing.Point( 27, 41 );
            this.glyphEditor1.Name = "glyphEditor2";
            this.glyphEditor1.Size = new System.Drawing.Size( 183, 216 );
            this.glyphEditor1.TabIndex = 4;
            // 
            // FontEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add( this.glyphEditor1 );
            this.Controls.Add( this.numericUpDown1 );
            this.Name = "FontEditor";
            this.Size = new System.Drawing.Size( 213, 260 );
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private GlyphEditor glyphEditor1;
    }
}
