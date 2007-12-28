namespace FFTPatcher.Editors
{
    partial class AllInflictStatusesEditor
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
            this.offsetListBox = new System.Windows.Forms.ListBox();
            this.inflictStatusEditor = new FFTPatcher.Editors.InflictStatusEditor();
            this.SuspendLayout();
            // 
            // offsetListBox
            // 
            this.offsetListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.offsetListBox.FormattingEnabled = true;
            this.offsetListBox.Location = new System.Drawing.Point( 0, 0 );
            this.offsetListBox.Name = "offsetListBox";
            this.offsetListBox.Size = new System.Drawing.Size( 46, 433 );
            this.offsetListBox.TabIndex = 1;
            // 
            // inflictStatusEditor
            // 
            this.inflictStatusEditor.AutoSize = true;
            this.inflictStatusEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.inflictStatusEditor.InflictStatus = null;
            this.inflictStatusEditor.Location = new System.Drawing.Point( 52, 0 );
            this.inflictStatusEditor.Name = "inflictStatusEditor";
            this.inflictStatusEditor.Size = new System.Drawing.Size( 611, 204 );
            this.inflictStatusEditor.TabIndex = 0;
            // 
            // AllInflictStatusesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.offsetListBox );
            this.Controls.Add( this.inflictStatusEditor );
            this.Name = "AllInflictStatusesEditor";
            this.Size = new System.Drawing.Size( 738, 437 );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private InflictStatusEditor inflictStatusEditor;
        private System.Windows.Forms.ListBox offsetListBox;
    }
}
