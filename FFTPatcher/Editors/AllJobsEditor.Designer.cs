namespace FFTPatcher.Editors
{
    partial class AllJobsEditor
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
            this.jobsListBox = new System.Windows.Forms.ListBox();
            this.jobEditor = new FFTPatcher.Editors.JobEditor();
            this.SuspendLayout();
            // 
            // jobsListBox
            // 
            this.jobsListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.jobsListBox.FormattingEnabled = true;
            this.jobsListBox.Location = new System.Drawing.Point(0, 0);
            this.jobsListBox.Name = "jobsListBox";
            this.jobsListBox.Size = new System.Drawing.Size(143, 511);
            this.jobsListBox.TabIndex = 0;
            // 
            // jobEditor
            // 
            this.jobEditor.AutoScroll = true;
            this.jobEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobEditor.Job = null;
            this.jobEditor.Location = new System.Drawing.Point(143, 0);
            this.jobEditor.Name = "jobEditor";
            this.jobEditor.Size = new System.Drawing.Size(503, 520);
            this.jobEditor.TabIndex = 1;
            // 
            // AllJobsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.jobEditor);
            this.Controls.Add(this.jobsListBox);
            this.Name = "AllJobsEditor";
            this.Size = new System.Drawing.Size(646, 520);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox jobsListBox;
        private JobEditor jobEditor;
    }
}
