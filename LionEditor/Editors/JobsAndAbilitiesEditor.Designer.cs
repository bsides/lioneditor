namespace LionEditor
{
    partial class JobsAndAbilitiesEditor
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.jobSelector = new System.Windows.Forms.ListBox();
            this.jobEditor1 = new LionEditor.JobEditor();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox16 = new System.Windows.Forms.CheckBox();
            this.checkBox15 = new System.Windows.Forms.CheckBox();
            this.checkBox14 = new System.Windows.Forms.CheckBox();
            this.checkBox13 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.action6 = new System.Windows.Forms.CheckBox();
            this.action5 = new System.Windows.Forms.CheckBox();
            this.action4 = new System.Windows.Forms.CheckBox();
            this.action3 = new System.Windows.Forms.CheckBox();
            this.action2 = new System.Windows.Forms.CheckBox();
            this.action1 = new System.Windows.Forms.CheckBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point( 0, 0 );
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add( this.jobSelector );
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add( this.jobEditor1 );
            this.splitContainer1.Size = new System.Drawing.Size( 498, 433 );
            this.splitContainer1.SplitterDistance = 109;
            this.splitContainer1.TabIndex = 0;
            // 
            // jobSelector
            // 
            this.jobSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobSelector.FormattingEnabled = true;
            this.jobSelector.Location = new System.Drawing.Point( 0, 0 );
            this.jobSelector.Name = "jobSelector";
            this.jobSelector.Size = new System.Drawing.Size( 109, 433 );
            this.jobSelector.TabIndex = 0;
            // 
            // jobEditor1
            // 
            this.jobEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobEditor1.Info = null;
            this.jobEditor1.Job = null;
            this.jobEditor1.Location = new System.Drawing.Point( 0, 0 );
            this.jobEditor1.Name = "jobEditor1";
            this.jobEditor1.Size = new System.Drawing.Size( 385, 433 );
            this.jobEditor1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point( 0, 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size( 200, 100 );
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
            this.tableLayoutPanel2.Controls.Add( this.checkBox16, 0, 15 );
            this.tableLayoutPanel2.Controls.Add( this.checkBox15, 0, 14 );
            this.tableLayoutPanel2.Controls.Add( this.checkBox14, 0, 13 );
            this.tableLayoutPanel2.Controls.Add( this.checkBox13, 0, 12 );
            this.tableLayoutPanel2.Controls.Add( this.checkBox12, 0, 11 );
            this.tableLayoutPanel2.Controls.Add( this.checkBox11, 0, 10 );
            this.tableLayoutPanel2.Controls.Add( this.checkBox10, 0, 9 );
            this.tableLayoutPanel2.Controls.Add( this.checkBox9, 0, 8 );
            this.tableLayoutPanel2.Controls.Add( this.checkBox8, 0, 7 );
            this.tableLayoutPanel2.Controls.Add( this.checkBox7, 0, 6 );
            this.tableLayoutPanel2.Controls.Add( this.action6, 0, 5 );
            this.tableLayoutPanel2.Controls.Add( this.action5, 0, 4 );
            this.tableLayoutPanel2.Controls.Add( this.action4, 0, 3 );
            this.tableLayoutPanel2.Controls.Add( this.action3, 0, 2 );
            this.tableLayoutPanel2.Controls.Add( this.action2, 0, 1 );
            this.tableLayoutPanel2.Controls.Add( this.action1, 0, 0 );
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point( 3, 3 );
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 16;
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel2.Size = new System.Drawing.Size( 94, 497 );
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // checkBox16
            // 
            this.checkBox16.AutoSize = true;
            this.checkBox16.Location = new System.Drawing.Point( 3, 468 );
            this.checkBox16.Name = "checkBox16";
            this.checkBox16.Size = new System.Drawing.Size( 86, 17 );
            this.checkBox16.TabIndex = 15;
            this.checkBox16.Text = "checkBox16";
            this.checkBox16.UseVisualStyleBackColor = true;
            // 
            // checkBox15
            // 
            this.checkBox15.AutoSize = true;
            this.checkBox15.Location = new System.Drawing.Point( 3, 437 );
            this.checkBox15.Name = "checkBox15";
            this.checkBox15.Size = new System.Drawing.Size( 86, 17 );
            this.checkBox15.TabIndex = 14;
            this.checkBox15.Text = "checkBox15";
            this.checkBox15.UseVisualStyleBackColor = true;
            // 
            // checkBox14
            // 
            this.checkBox14.AutoSize = true;
            this.checkBox14.Location = new System.Drawing.Point( 3, 406 );
            this.checkBox14.Name = "checkBox14";
            this.checkBox14.Size = new System.Drawing.Size( 86, 17 );
            this.checkBox14.TabIndex = 13;
            this.checkBox14.Text = "checkBox14";
            this.checkBox14.UseVisualStyleBackColor = true;
            // 
            // checkBox13
            // 
            this.checkBox13.AutoSize = true;
            this.checkBox13.Location = new System.Drawing.Point( 3, 375 );
            this.checkBox13.Name = "checkBox13";
            this.checkBox13.Size = new System.Drawing.Size( 86, 17 );
            this.checkBox13.TabIndex = 12;
            this.checkBox13.Text = "checkBox13";
            this.checkBox13.UseVisualStyleBackColor = true;
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Location = new System.Drawing.Point( 3, 344 );
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size( 86, 17 );
            this.checkBox12.TabIndex = 11;
            this.checkBox12.Text = "checkBox12";
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // checkBox11
            // 
            this.checkBox11.AutoSize = true;
            this.checkBox11.Location = new System.Drawing.Point( 3, 313 );
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size( 86, 17 );
            this.checkBox11.TabIndex = 10;
            this.checkBox11.Text = "checkBox11";
            this.checkBox11.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Location = new System.Drawing.Point( 3, 282 );
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size( 86, 17 );
            this.checkBox10.TabIndex = 9;
            this.checkBox10.Text = "checkBox10";
            this.checkBox10.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point( 3, 251 );
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size( 80, 17 );
            this.checkBox9.TabIndex = 8;
            this.checkBox9.Text = "checkBox9";
            this.checkBox9.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point( 3, 220 );
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size( 80, 17 );
            this.checkBox8.TabIndex = 7;
            this.checkBox8.Text = "checkBox8";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point( 3, 189 );
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size( 80, 17 );
            this.checkBox7.TabIndex = 6;
            this.checkBox7.Text = "checkBox7";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // action6
            // 
            this.action6.AutoSize = true;
            this.action6.Location = new System.Drawing.Point( 3, 158 );
            this.action6.Name = "action6";
            this.action6.Size = new System.Drawing.Size( 80, 17 );
            this.action6.TabIndex = 5;
            this.action6.Text = "checkBox6";
            this.action6.UseVisualStyleBackColor = true;
            // 
            // action5
            // 
            this.action5.AutoSize = true;
            this.action5.Location = new System.Drawing.Point( 3, 127 );
            this.action5.Name = "action5";
            this.action5.Size = new System.Drawing.Size( 80, 17 );
            this.action5.TabIndex = 4;
            this.action5.Text = "checkBox5";
            this.action5.UseVisualStyleBackColor = true;
            // 
            // action4
            // 
            this.action4.AutoSize = true;
            this.action4.Location = new System.Drawing.Point( 3, 96 );
            this.action4.Name = "action4";
            this.action4.Size = new System.Drawing.Size( 80, 17 );
            this.action4.TabIndex = 3;
            this.action4.Text = "checkBox4";
            this.action4.UseVisualStyleBackColor = true;
            // 
            // action3
            // 
            this.action3.AutoSize = true;
            this.action3.Location = new System.Drawing.Point( 3, 65 );
            this.action3.Name = "action3";
            this.action3.Size = new System.Drawing.Size( 80, 17 );
            this.action3.TabIndex = 2;
            this.action3.Text = "checkBox3";
            this.action3.UseVisualStyleBackColor = true;
            // 
            // action2
            // 
            this.action2.AutoSize = true;
            this.action2.Location = new System.Drawing.Point( 3, 34 );
            this.action2.Name = "action2";
            this.action2.Size = new System.Drawing.Size( 80, 17 );
            this.action2.TabIndex = 1;
            this.action2.Text = "checkBox2";
            this.action2.UseVisualStyleBackColor = true;
            // 
            // action1
            // 
            this.action1.AutoSize = true;
            this.action1.Location = new System.Drawing.Point( 3, 3 );
            this.action1.Name = "action1";
            this.action1.Size = new System.Drawing.Size( 61, 17 );
            this.action1.TabIndex = 0;
            this.action1.Text = "action1";
            this.action1.UseVisualStyleBackColor = true;
            // 
            // JobsAndAbilitiesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 498, 433 );
            this.Controls.Add( this.splitContainer1 );
            this.Name = "JobsAndAbilitiesEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "JobsAndAbilitiesEditor";
            this.splitContainer1.Panel1.ResumeLayout( false );
            this.splitContainer1.Panel2.ResumeLayout( false );
            this.splitContainer1.ResumeLayout( false );
            this.tableLayoutPanel2.ResumeLayout( false );
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox jobSelector;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBox16;
        private System.Windows.Forms.CheckBox checkBox15;
        private System.Windows.Forms.CheckBox checkBox14;
        private System.Windows.Forms.CheckBox checkBox13;
        private System.Windows.Forms.CheckBox checkBox12;
        private System.Windows.Forms.CheckBox checkBox11;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox action6;
        private System.Windows.Forms.CheckBox action5;
        private System.Windows.Forms.CheckBox action4;
        private System.Windows.Forms.CheckBox action3;
        private System.Windows.Forms.CheckBox action2;
        private System.Windows.Forms.CheckBox action1;
        private JobEditor jobEditor1;
    }
}