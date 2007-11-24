namespace LionEditor
{
    partial class JobEditor
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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.totalSpinner = new System.Windows.Forms.NumericUpDown();
            this.totalLabel = new System.Windows.Forms.Label();
            this.movementGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.movement2 = new System.Windows.Forms.CheckBox();
            this.movement1 = new System.Windows.Forms.CheckBox();
            this.supportGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.support1 = new System.Windows.Forms.CheckBox();
            this.support2 = new System.Windows.Forms.CheckBox();
            this.support3 = new System.Windows.Forms.CheckBox();
            this.support4 = new System.Windows.Forms.CheckBox();
            this.actionGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.action13 = new System.Windows.Forms.CheckBox();
            this.action12 = new System.Windows.Forms.CheckBox();
            this.action11 = new System.Windows.Forms.CheckBox();
            this.action10 = new System.Windows.Forms.CheckBox();
            this.action9 = new System.Windows.Forms.CheckBox();
            this.action8 = new System.Windows.Forms.CheckBox();
            this.action7 = new System.Windows.Forms.CheckBox();
            this.action6 = new System.Windows.Forms.CheckBox();
            this.action5 = new System.Windows.Forms.CheckBox();
            this.action4 = new System.Windows.Forms.CheckBox();
            this.action3 = new System.Windows.Forms.CheckBox();
            this.action2 = new System.Windows.Forms.CheckBox();
            this.action1 = new System.Windows.Forms.CheckBox();
            this.action16 = new System.Windows.Forms.CheckBox();
            this.action15 = new System.Windows.Forms.CheckBox();
            this.action14 = new System.Windows.Forms.CheckBox();
            this.reactionGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.reaction1 = new System.Windows.Forms.CheckBox();
            this.reaction2 = new System.Windows.Forms.CheckBox();
            this.reaction3 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.jpSpinner = new System.Windows.Forms.NumericUpDown();
            this.jpLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalSpinner)).BeginInit();
            this.movementGroup.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.supportGroup.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.actionGroup.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.reactionGroup.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jpSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel3.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel3.Controls.Add( this.panel2, 1, 0 );
            this.tableLayoutPanel3.Controls.Add( this.movementGroup, 1, 3 );
            this.tableLayoutPanel3.Controls.Add( this.supportGroup, 1, 2 );
            this.tableLayoutPanel3.Controls.Add( this.actionGroup, 0, 1 );
            this.tableLayoutPanel3.Controls.Add( this.reactionGroup, 1, 1 );
            this.tableLayoutPanel3.Controls.Add( this.panel1, 0, 0 );
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point( 0, 0 );
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute, 40F ) );
            this.tableLayoutPanel3.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 33.33333F ) );
            this.tableLayoutPanel3.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 33.33333F ) );
            this.tableLayoutPanel3.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 33.33333F ) );
            this.tableLayoutPanel3.Size = new System.Drawing.Size( 625, 620 );
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add( this.totalSpinner );
            this.panel2.Controls.Add( this.totalLabel );
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point( 315, 3 );
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size( 307, 34 );
            this.panel2.TabIndex = 7;
            // 
            // totalSpinner
            // 
            this.totalSpinner.Location = new System.Drawing.Point( 63, 5 );
            this.totalSpinner.Maximum = new decimal( new int[] {
            9999,
            0,
            0,
            0} );
            this.totalSpinner.Name = "totalSpinner";
            this.totalSpinner.Size = new System.Drawing.Size( 120, 20 );
            this.totalSpinner.TabIndex = 1;
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.Location = new System.Drawing.Point( 7, 9 );
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size( 34, 13 );
            this.totalLabel.TabIndex = 0;
            this.totalLabel.Text = "Total:";
            // 
            // movementGroup
            // 
            this.movementGroup.Controls.Add( this.tableLayoutPanel1 );
            this.movementGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movementGroup.Location = new System.Drawing.Point( 315, 429 );
            this.movementGroup.Name = "movementGroup";
            this.movementGroup.Size = new System.Drawing.Size( 307, 188 );
            this.movementGroup.TabIndex = 5;
            this.movementGroup.TabStop = false;
            this.movementGroup.Text = "Movement";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.Controls.Add( this.movement2, 0, 1 );
            this.tableLayoutPanel1.Controls.Add( this.movement1, 0, 0 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point( 3, 16 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size( 301, 169 );
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // movement2
            // 
            this.movement2.AutoSize = true;
            this.movement2.Location = new System.Drawing.Point( 3, 87 );
            this.movement2.Name = "movement2";
            this.movement2.Size = new System.Drawing.Size( 81, 17 );
            this.movement2.TabIndex = 2;
            this.movement2.Text = "movement2";
            this.movement2.UseVisualStyleBackColor = true;
            // 
            // movement1
            // 
            this.movement1.AutoSize = true;
            this.movement1.Location = new System.Drawing.Point( 3, 3 );
            this.movement1.Name = "movement1";
            this.movement1.Size = new System.Drawing.Size( 81, 17 );
            this.movement1.TabIndex = 1;
            this.movement1.Text = "movement1";
            this.movement1.UseVisualStyleBackColor = true;
            // 
            // supportGroup
            // 
            this.supportGroup.Controls.Add( this.tableLayoutPanel2 );
            this.supportGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.supportGroup.Location = new System.Drawing.Point( 315, 236 );
            this.supportGroup.Name = "supportGroup";
            this.supportGroup.Size = new System.Drawing.Size( 307, 187 );
            this.supportGroup.TabIndex = 4;
            this.supportGroup.TabStop = false;
            this.supportGroup.Text = "Support";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel2.Controls.Add( this.support1, 0, 0 );
            this.tableLayoutPanel2.Controls.Add( this.support2, 0, 1 );
            this.tableLayoutPanel2.Controls.Add( this.support3, 0, 2 );
            this.tableLayoutPanel2.Controls.Add( this.support4, 0, 3 );
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point( 3, 16 );
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 25F ) );
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 25F ) );
            this.tableLayoutPanel2.Size = new System.Drawing.Size( 301, 168 );
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // support1
            // 
            this.support1.AutoSize = true;
            this.support1.Location = new System.Drawing.Point( 3, 3 );
            this.support1.Name = "support1";
            this.support1.Size = new System.Drawing.Size( 67, 17 );
            this.support1.TabIndex = 2;
            this.support1.Text = "support1";
            this.support1.UseVisualStyleBackColor = true;
            // 
            // support2
            // 
            this.support2.AutoSize = true;
            this.support2.Location = new System.Drawing.Point( 3, 45 );
            this.support2.Name = "support2";
            this.support2.Size = new System.Drawing.Size( 67, 17 );
            this.support2.TabIndex = 3;
            this.support2.Text = "support2";
            this.support2.UseVisualStyleBackColor = true;
            // 
            // support3
            // 
            this.support3.AutoSize = true;
            this.support3.Location = new System.Drawing.Point( 3, 87 );
            this.support3.Name = "support3";
            this.support3.Size = new System.Drawing.Size( 67, 17 );
            this.support3.TabIndex = 4;
            this.support3.Text = "support3";
            this.support3.UseVisualStyleBackColor = true;
            // 
            // support4
            // 
            this.support4.AutoSize = true;
            this.support4.Location = new System.Drawing.Point( 3, 129 );
            this.support4.Name = "support4";
            this.support4.Size = new System.Drawing.Size( 67, 17 );
            this.support4.TabIndex = 5;
            this.support4.Text = "support4";
            this.support4.UseVisualStyleBackColor = true;
            // 
            // actionGroup
            // 
            this.actionGroup.Controls.Add( this.tableLayoutPanel4 );
            this.actionGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionGroup.Location = new System.Drawing.Point( 3, 43 );
            this.actionGroup.Name = "actionGroup";
            this.tableLayoutPanel3.SetRowSpan( this.actionGroup, 3 );
            this.actionGroup.Size = new System.Drawing.Size( 306, 574 );
            this.actionGroup.TabIndex = 1;
            this.actionGroup.TabStop = false;
            this.actionGroup.Text = "Action";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel4.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute, 20F ) );
            this.tableLayoutPanel4.Controls.Add( this.action13, 0, 12 );
            this.tableLayoutPanel4.Controls.Add( this.action12, 0, 11 );
            this.tableLayoutPanel4.Controls.Add( this.action11, 0, 10 );
            this.tableLayoutPanel4.Controls.Add( this.action10, 0, 9 );
            this.tableLayoutPanel4.Controls.Add( this.action9, 0, 8 );
            this.tableLayoutPanel4.Controls.Add( this.action8, 0, 7 );
            this.tableLayoutPanel4.Controls.Add( this.action7, 0, 6 );
            this.tableLayoutPanel4.Controls.Add( this.action6, 0, 5 );
            this.tableLayoutPanel4.Controls.Add( this.action5, 0, 4 );
            this.tableLayoutPanel4.Controls.Add( this.action4, 0, 3 );
            this.tableLayoutPanel4.Controls.Add( this.action3, 0, 2 );
            this.tableLayoutPanel4.Controls.Add( this.action2, 0, 1 );
            this.tableLayoutPanel4.Controls.Add( this.action1, 0, 0 );
            this.tableLayoutPanel4.Controls.Add( this.action16, 0, 15 );
            this.tableLayoutPanel4.Controls.Add( this.action15, 0, 14 );
            this.tableLayoutPanel4.Controls.Add( this.action14, 0, 13 );
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point( 3, 16 );
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 16;
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 6.25F ) );
            this.tableLayoutPanel4.Size = new System.Drawing.Size( 300, 555 );
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // action13
            // 
            this.action13.AutoSize = true;
            this.action13.Location = new System.Drawing.Point( 3, 411 );
            this.action13.Name = "action13";
            this.action13.Size = new System.Drawing.Size( 67, 17 );
            this.action13.TabIndex = 15;
            this.action13.Text = "action13";
            this.action13.UseVisualStyleBackColor = true;
            // 
            // action12
            // 
            this.action12.AutoSize = true;
            this.action12.Location = new System.Drawing.Point( 3, 377 );
            this.action12.Name = "action12";
            this.action12.Size = new System.Drawing.Size( 67, 17 );
            this.action12.TabIndex = 11;
            this.action12.Text = "action12";
            this.action12.UseVisualStyleBackColor = true;
            // 
            // action11
            // 
            this.action11.AutoSize = true;
            this.action11.Location = new System.Drawing.Point( 3, 343 );
            this.action11.Name = "action11";
            this.action11.Size = new System.Drawing.Size( 67, 17 );
            this.action11.TabIndex = 10;
            this.action11.Text = "action11";
            this.action11.UseVisualStyleBackColor = true;
            // 
            // action10
            // 
            this.action10.AutoSize = true;
            this.action10.Location = new System.Drawing.Point( 3, 309 );
            this.action10.Name = "action10";
            this.action10.Size = new System.Drawing.Size( 67, 17 );
            this.action10.TabIndex = 9;
            this.action10.Text = "action10";
            this.action10.UseVisualStyleBackColor = true;
            // 
            // action9
            // 
            this.action9.AutoSize = true;
            this.action9.Location = new System.Drawing.Point( 3, 275 );
            this.action9.Name = "action9";
            this.action9.Size = new System.Drawing.Size( 61, 17 );
            this.action9.TabIndex = 8;
            this.action9.Text = "action9";
            this.action9.UseVisualStyleBackColor = true;
            // 
            // action8
            // 
            this.action8.AutoSize = true;
            this.action8.Location = new System.Drawing.Point( 3, 241 );
            this.action8.Name = "action8";
            this.action8.Size = new System.Drawing.Size( 61, 17 );
            this.action8.TabIndex = 7;
            this.action8.Text = "action8";
            this.action8.UseVisualStyleBackColor = true;
            // 
            // action7
            // 
            this.action7.AutoSize = true;
            this.action7.Location = new System.Drawing.Point( 3, 207 );
            this.action7.Name = "action7";
            this.action7.Size = new System.Drawing.Size( 61, 17 );
            this.action7.TabIndex = 6;
            this.action7.Text = "action7";
            this.action7.UseVisualStyleBackColor = true;
            // 
            // action6
            // 
            this.action6.AutoSize = true;
            this.action6.Location = new System.Drawing.Point( 3, 173 );
            this.action6.Name = "action6";
            this.action6.Size = new System.Drawing.Size( 61, 17 );
            this.action6.TabIndex = 5;
            this.action6.Text = "action6";
            this.action6.UseVisualStyleBackColor = true;
            // 
            // action5
            // 
            this.action5.AutoSize = true;
            this.action5.Location = new System.Drawing.Point( 3, 139 );
            this.action5.Name = "action5";
            this.action5.Size = new System.Drawing.Size( 61, 17 );
            this.action5.TabIndex = 4;
            this.action5.Text = "action5";
            this.action5.UseVisualStyleBackColor = true;
            // 
            // action4
            // 
            this.action4.AutoSize = true;
            this.action4.Location = new System.Drawing.Point( 3, 105 );
            this.action4.Name = "action4";
            this.action4.Size = new System.Drawing.Size( 61, 17 );
            this.action4.TabIndex = 3;
            this.action4.Text = "action4";
            this.action4.UseVisualStyleBackColor = true;
            // 
            // action3
            // 
            this.action3.AutoSize = true;
            this.action3.Location = new System.Drawing.Point( 3, 71 );
            this.action3.Name = "action3";
            this.action3.Size = new System.Drawing.Size( 61, 17 );
            this.action3.TabIndex = 2;
            this.action3.Text = "action3";
            this.action3.UseVisualStyleBackColor = true;
            // 
            // action2
            // 
            this.action2.AutoSize = true;
            this.action2.Location = new System.Drawing.Point( 3, 37 );
            this.action2.Name = "action2";
            this.action2.Size = new System.Drawing.Size( 61, 17 );
            this.action2.TabIndex = 1;
            this.action2.Text = "action2";
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
            // action16
            // 
            this.action16.AutoSize = true;
            this.action16.Location = new System.Drawing.Point( 3, 513 );
            this.action16.Name = "action16";
            this.action16.Size = new System.Drawing.Size( 67, 17 );
            this.action16.TabIndex = 14;
            this.action16.Text = "action16";
            this.action16.UseVisualStyleBackColor = true;
            // 
            // action15
            // 
            this.action15.AutoSize = true;
            this.action15.Location = new System.Drawing.Point( 3, 479 );
            this.action15.Name = "action15";
            this.action15.Size = new System.Drawing.Size( 67, 17 );
            this.action15.TabIndex = 13;
            this.action15.Text = "action15";
            this.action15.UseVisualStyleBackColor = true;
            // 
            // action14
            // 
            this.action14.AutoSize = true;
            this.action14.Location = new System.Drawing.Point( 3, 445 );
            this.action14.Name = "action14";
            this.action14.Size = new System.Drawing.Size( 67, 17 );
            this.action14.TabIndex = 12;
            this.action14.Text = "action14";
            this.action14.UseVisualStyleBackColor = true;
            // 
            // reactionGroup
            // 
            this.reactionGroup.Controls.Add( this.tableLayoutPanel5 );
            this.reactionGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reactionGroup.Location = new System.Drawing.Point( 315, 43 );
            this.reactionGroup.Name = "reactionGroup";
            this.reactionGroup.Size = new System.Drawing.Size( 307, 187 );
            this.reactionGroup.TabIndex = 2;
            this.reactionGroup.TabStop = false;
            this.reactionGroup.Text = "Reaction";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel5.Controls.Add( this.reaction1, 0, 0 );
            this.tableLayoutPanel5.Controls.Add( this.reaction2, 0, 1 );
            this.tableLayoutPanel5.Controls.Add( this.reaction3, 0, 2 );
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point( 3, 16 );
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 33.33333F ) );
            this.tableLayoutPanel5.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 33.33333F ) );
            this.tableLayoutPanel5.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 33.33333F ) );
            this.tableLayoutPanel5.Size = new System.Drawing.Size( 301, 168 );
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // reaction1
            // 
            this.reaction1.AutoSize = true;
            this.reaction1.Location = new System.Drawing.Point( 3, 3 );
            this.reaction1.Name = "reaction1";
            this.reaction1.Size = new System.Drawing.Size( 70, 17 );
            this.reaction1.TabIndex = 0;
            this.reaction1.Text = "reaction1";
            this.reaction1.UseVisualStyleBackColor = true;
            // 
            // reaction2
            // 
            this.reaction2.AutoSize = true;
            this.reaction2.Location = new System.Drawing.Point( 3, 59 );
            this.reaction2.Name = "reaction2";
            this.reaction2.Size = new System.Drawing.Size( 70, 17 );
            this.reaction2.TabIndex = 1;
            this.reaction2.Text = "reaction2";
            this.reaction2.UseVisualStyleBackColor = true;
            // 
            // reaction3
            // 
            this.reaction3.AutoSize = true;
            this.reaction3.Location = new System.Drawing.Point( 3, 115 );
            this.reaction3.Name = "reaction3";
            this.reaction3.Size = new System.Drawing.Size( 70, 17 );
            this.reaction3.TabIndex = 2;
            this.reaction3.Text = "reaction3";
            this.reaction3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add( this.jpSpinner );
            this.panel1.Controls.Add( this.jpLabel );
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point( 3, 3 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 306, 34 );
            this.panel1.TabIndex = 6;
            // 
            // jpSpinner
            // 
            this.jpSpinner.Location = new System.Drawing.Point( 63, 5 );
            this.jpSpinner.Maximum = new decimal( new int[] {
            9999,
            0,
            0,
            0} );
            this.jpSpinner.Name = "jpSpinner";
            this.jpSpinner.Size = new System.Drawing.Size( 120, 20 );
            this.jpSpinner.TabIndex = 1;
            // 
            // jpLabel
            // 
            this.jpLabel.AutoSize = true;
            this.jpLabel.Location = new System.Drawing.Point( 7, 9 );
            this.jpLabel.Name = "jpLabel";
            this.jpLabel.Size = new System.Drawing.Size( 22, 13 );
            this.jpLabel.TabIndex = 0;
            this.jpLabel.Text = "JP:";
            // 
            // JobEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.tableLayoutPanel3 );
            this.Name = "JobEditor";
            this.Size = new System.Drawing.Size( 625, 620 );
            this.tableLayoutPanel3.ResumeLayout( false );
            this.panel2.ResumeLayout( false );
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalSpinner)).EndInit();
            this.movementGroup.ResumeLayout( false );
            this.tableLayoutPanel1.ResumeLayout( false );
            this.tableLayoutPanel1.PerformLayout();
            this.supportGroup.ResumeLayout( false );
            this.tableLayoutPanel2.ResumeLayout( false );
            this.tableLayoutPanel2.PerformLayout();
            this.actionGroup.ResumeLayout( false );
            this.tableLayoutPanel4.ResumeLayout( false );
            this.tableLayoutPanel4.PerformLayout();
            this.reactionGroup.ResumeLayout( false );
            this.tableLayoutPanel5.ResumeLayout( false );
            this.tableLayoutPanel5.PerformLayout();
            this.panel1.ResumeLayout( false );
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jpSpinner)).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox movementGroup;
        private System.Windows.Forms.GroupBox supportGroup;
        private System.Windows.Forms.GroupBox actionGroup;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox action13;
        private System.Windows.Forms.CheckBox action16;
        private System.Windows.Forms.CheckBox action15;
        private System.Windows.Forms.CheckBox action14;
        private System.Windows.Forms.CheckBox action12;
        private System.Windows.Forms.CheckBox action11;
        private System.Windows.Forms.CheckBox action10;
        private System.Windows.Forms.CheckBox action9;
        private System.Windows.Forms.CheckBox action8;
        private System.Windows.Forms.CheckBox action7;
        private System.Windows.Forms.CheckBox action6;
        private System.Windows.Forms.CheckBox action5;
        private System.Windows.Forms.CheckBox action4;
        private System.Windows.Forms.CheckBox action3;
        private System.Windows.Forms.CheckBox action2;
        private System.Windows.Forms.CheckBox action1;
        private System.Windows.Forms.GroupBox reactionGroup;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox movement2;
        private System.Windows.Forms.CheckBox movement1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox support1;
        private System.Windows.Forms.CheckBox support2;
        private System.Windows.Forms.CheckBox support3;
        private System.Windows.Forms.CheckBox support4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.CheckBox reaction1;
        private System.Windows.Forms.CheckBox reaction2;
        private System.Windows.Forms.CheckBox reaction3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown jpSpinner;
        private System.Windows.Forms.Label jpLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown totalSpinner;
        private System.Windows.Forms.Label totalLabel;
    }
}
