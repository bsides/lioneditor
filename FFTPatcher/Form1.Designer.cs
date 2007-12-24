namespace FFTPatcher
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.abilitiesPage = new System.Windows.Forms.TabPage();
            this.jobsPage = new System.Windows.Forms.TabPage();
            this.skillSetsPage = new System.Windows.Forms.TabPage();
            this.codesTab = new System.Windows.Forms.TabPage();
            this.allAbilitiesEditor1 = new FFTPatcher.Editors.AllAbilitiesEditor();
            this.allJobsEditor1 = new FFTPatcher.Editors.AllJobsEditor();
            this.allSkillSetsEditor1 = new FFTPatcher.Editors.AllSkillSetsEditor();
            this.codeCreator1 = new FFTPatcher.Editors.CodeCreator();
            this.tabControl1.SuspendLayout();
            this.abilitiesPage.SuspendLayout();
            this.jobsPage.SuspendLayout();
            this.skillSetsPage.SuspendLayout();
            this.codesTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.abilitiesPage);
            this.tabControl1.Controls.Add(this.jobsPage);
            this.tabControl1.Controls.Add(this.skillSetsPage);
            this.tabControl1.Controls.Add(this.codesTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(691, 712);
            this.tabControl1.TabIndex = 1;
            // 
            // abilitiesPage
            // 
            this.abilitiesPage.Controls.Add(this.allAbilitiesEditor1);
            this.abilitiesPage.Location = new System.Drawing.Point(4, 22);
            this.abilitiesPage.Name = "abilitiesPage";
            this.abilitiesPage.Padding = new System.Windows.Forms.Padding(3);
            this.abilitiesPage.Size = new System.Drawing.Size(683, 686);
            this.abilitiesPage.TabIndex = 0;
            this.abilitiesPage.Text = "Abilities";
            this.abilitiesPage.UseVisualStyleBackColor = true;
            // 
            // jobsPage
            // 
            this.jobsPage.Controls.Add(this.allJobsEditor1);
            this.jobsPage.Location = new System.Drawing.Point(4, 22);
            this.jobsPage.Name = "jobsPage";
            this.jobsPage.Padding = new System.Windows.Forms.Padding(3);
            this.jobsPage.Size = new System.Drawing.Size(683, 686);
            this.jobsPage.TabIndex = 1;
            this.jobsPage.Text = "Jobs";
            this.jobsPage.UseVisualStyleBackColor = true;
            // 
            // skillSetsPage
            // 
            this.skillSetsPage.Controls.Add(this.allSkillSetsEditor1);
            this.skillSetsPage.Location = new System.Drawing.Point(4, 22);
            this.skillSetsPage.Name = "skillSetsPage";
            this.skillSetsPage.Size = new System.Drawing.Size(683, 686);
            this.skillSetsPage.TabIndex = 2;
            this.skillSetsPage.Text = "Skill Sets";
            this.skillSetsPage.UseVisualStyleBackColor = true;
            // 
            // codesTab
            // 
            this.codesTab.Controls.Add(this.codeCreator1);
            this.codesTab.Location = new System.Drawing.Point(4, 22);
            this.codesTab.Name = "codesTab";
            this.codesTab.Padding = new System.Windows.Forms.Padding(3);
            this.codesTab.Size = new System.Drawing.Size(683, 686);
            this.codesTab.TabIndex = 3;
            this.codesTab.Text = "CWCheat";
            this.codesTab.UseVisualStyleBackColor = true;
            // 
            // allAbilitiesEditor1
            // 
            this.allAbilitiesEditor1.AutoSize = true;
            this.allAbilitiesEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allAbilitiesEditor1.Location = new System.Drawing.Point(3, 3);
            this.allAbilitiesEditor1.Name = "allAbilitiesEditor1";
            this.allAbilitiesEditor1.Size = new System.Drawing.Size(677, 680);
            this.allAbilitiesEditor1.TabIndex = 0;
            // 
            // allJobsEditor1
            // 
            this.allJobsEditor1.AutoScroll = true;
            this.allJobsEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allJobsEditor1.Location = new System.Drawing.Point(3, 3);
            this.allJobsEditor1.Name = "allJobsEditor1";
            this.allJobsEditor1.Size = new System.Drawing.Size(677, 680);
            this.allJobsEditor1.TabIndex = 0;
            // 
            // allSkillSetsEditor1
            // 
            this.allSkillSetsEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allSkillSetsEditor1.Location = new System.Drawing.Point(0, 0);
            this.allSkillSetsEditor1.Name = "allSkillSetsEditor1";
            this.allSkillSetsEditor1.Size = new System.Drawing.Size(683, 686);
            this.allSkillSetsEditor1.TabIndex = 0;
            // 
            // codeCreator1
            // 
            this.codeCreator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeCreator1.Location = new System.Drawing.Point(3, 3);
            this.codeCreator1.Name = "codeCreator1";
            this.codeCreator1.Size = new System.Drawing.Size(677, 680);
            this.codeCreator1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(691, 712);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.abilitiesPage.ResumeLayout(false);
            this.abilitiesPage.PerformLayout();
            this.jobsPage.ResumeLayout(false);
            this.skillSetsPage.ResumeLayout(false);
            this.codesTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FFTPatcher.Editors.AllAbilitiesEditor allAbilitiesEditor1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage abilitiesPage;
        private System.Windows.Forms.TabPage jobsPage;
        private FFTPatcher.Editors.AllJobsEditor allJobsEditor1;
        private System.Windows.Forms.TabPage skillSetsPage;
        private FFTPatcher.Editors.AllSkillSetsEditor allSkillSetsEditor1;
        private System.Windows.Forms.TabPage codesTab;
        private FFTPatcher.Editors.CodeCreator codeCreator1;
    }
}

