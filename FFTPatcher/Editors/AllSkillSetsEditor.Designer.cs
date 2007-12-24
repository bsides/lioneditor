namespace FFTPatcher.Editors
{
    partial class AllSkillSetsEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.skillSetListBox = new System.Windows.Forms.ListBox();
            this.skillSetEditor = new FFTPatcher.Editors.SkillSetEditor();
            this.SuspendLayout();
            // 
            // skillSetListBox
            // 
            this.skillSetListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.skillSetListBox.FormattingEnabled = true;
            this.skillSetListBox.Location = new System.Drawing.Point(0, 0);
            this.skillSetListBox.Name = "skillSetListBox";
            this.skillSetListBox.Size = new System.Drawing.Size(192, 420);
            this.skillSetListBox.TabIndex = 0;
            // 
            // skillSetEditor
            // 
            this.skillSetEditor.AutoSize = true;
            this.skillSetEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.skillSetEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillSetEditor.Location = new System.Drawing.Point(192, 0);
            this.skillSetEditor.Name = "skillSetEditor";
            this.skillSetEditor.Size = new System.Drawing.Size(422, 426);
            this.skillSetEditor.SkillSet = null;
            this.skillSetEditor.TabIndex = 1;
            // 
            // AllSkillSetsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.skillSetEditor);
            this.Controls.Add(this.skillSetListBox);
            this.Name = "AllSkillSetsEditor";
            this.Size = new System.Drawing.Size(614, 426);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox skillSetListBox;
        private SkillSetEditor skillSetEditor;
    }
}
