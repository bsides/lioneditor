namespace FFTPatcher.Editors
{
    partial class PropositionEditor
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
            if (disposing && (components != null))
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
            System.Windows.Forms.Label jobTypeLabel;
            System.Windows.Forms.Label preferredStatsLabel;
            System.Windows.Forms.Label priceLabel;
            System.Windows.Forms.Label plusLabel;
            System.Windows.Forms.Label daysLabel;
            System.Windows.Forms.Label toLabel;
            System.Windows.Forms.Label rewardLabel;
            System.Windows.Forms.Label townLabel;
            System.Windows.Forms.GroupBox prereqGroupBox;
            System.Windows.Forms.Label unlockedLabel;
            System.Windows.Forms.Label prereqTypeLabel;
            System.Windows.Forms.GroupBox unknownGroupBox;
            this.dateRangeLabel = new System.Windows.Forms.Label();
            this.unlockedComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            this.certainDateComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            this.certainDateLabel = new System.Windows.Forms.Label();
            this.completeJobComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            this.completeJobLabel = new System.Windows.Forms.Label();
            this.rawByteLabel = new System.Windows.Forms.Label();
            this.prereqByteSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.prereqTypeComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            this.unknown0x0BSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.unknown0x0CSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.unknown0x0FSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.unknown0x14Spinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.unknown0x09Spinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.jobTypeComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            this.preferredStatsComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            this.price1ComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            this.price2ComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            this.minDaysSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.maxDaysSpinner = new FFTPatcher.Controls.NumericUpDownWithDefault();
            this.rewardComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            this.townComboBox = new FFTPatcher.Controls.ComboBoxWithDefault();
            jobTypeLabel = new System.Windows.Forms.Label();
            preferredStatsLabel = new System.Windows.Forms.Label();
            priceLabel = new System.Windows.Forms.Label();
            plusLabel = new System.Windows.Forms.Label();
            daysLabel = new System.Windows.Forms.Label();
            toLabel = new System.Windows.Forms.Label();
            rewardLabel = new System.Windows.Forms.Label();
            townLabel = new System.Windows.Forms.Label();
            prereqGroupBox = new System.Windows.Forms.GroupBox();
            unlockedLabel = new System.Windows.Forms.Label();
            prereqTypeLabel = new System.Windows.Forms.Label();
            unknownGroupBox = new System.Windows.Forms.GroupBox();
            prereqGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prereqByteSpinner)).BeginInit();
            unknownGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unknown0x0BSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown0x0CSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown0x0FSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown0x14Spinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown0x09Spinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minDaysSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxDaysSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // jobTypeLabel
            // 
            jobTypeLabel.AutoSize = true;
            jobTypeLabel.Location = new System.Drawing.Point( 3, 6 );
            jobTypeLabel.Name = "jobTypeLabel";
            jobTypeLabel.Size = new System.Drawing.Size( 47, 13 );
            jobTypeLabel.TabIndex = 1;
            jobTypeLabel.Text = "Job type";
            // 
            // preferredStatsLabel
            // 
            preferredStatsLabel.AutoSize = true;
            preferredStatsLabel.Location = new System.Drawing.Point( 3, 33 );
            preferredStatsLabel.Name = "preferredStatsLabel";
            preferredStatsLabel.Size = new System.Drawing.Size( 75, 13 );
            preferredStatsLabel.TabIndex = 3;
            preferredStatsLabel.Text = "Preferred stats";
            // 
            // priceLabel
            // 
            priceLabel.AutoSize = true;
            priceLabel.Location = new System.Drawing.Point( 3, 61 );
            priceLabel.Name = "priceLabel";
            priceLabel.Size = new System.Drawing.Size( 31, 13 );
            priceLabel.TabIndex = 4;
            priceLabel.Text = "Price";
            // 
            // plusLabel
            // 
            plusLabel.AutoSize = true;
            plusLabel.Location = new System.Drawing.Point( 159, 61 );
            plusLabel.Name = "plusLabel";
            plusLabel.Size = new System.Drawing.Size( 13, 13 );
            plusLabel.TabIndex = 7;
            plusLabel.Text = "+";
            // 
            // daysLabel
            // 
            daysLabel.AutoSize = true;
            daysLabel.Location = new System.Drawing.Point( 3, 89 );
            daysLabel.Name = "daysLabel";
            daysLabel.Size = new System.Drawing.Size( 31, 13 );
            daysLabel.TabIndex = 8;
            daysLabel.Text = "Days";
            // 
            // toLabel
            // 
            toLabel.AutoSize = true;
            toLabel.Location = new System.Drawing.Point( 158, 89 );
            toLabel.Name = "toLabel";
            toLabel.Size = new System.Drawing.Size( 16, 13 );
            toLabel.TabIndex = 11;
            toLabel.Text = "to";
            // 
            // rewardLabel
            // 
            rewardLabel.AutoSize = true;
            rewardLabel.Location = new System.Drawing.Point( 3, 116 );
            rewardLabel.Name = "rewardLabel";
            rewardLabel.Size = new System.Drawing.Size( 44, 13 );
            rewardLabel.TabIndex = 13;
            rewardLabel.Text = "Reward";
            // 
            // townLabel
            // 
            townLabel.AutoSize = true;
            townLabel.Location = new System.Drawing.Point( 3, 143 );
            townLabel.Name = "townLabel";
            townLabel.Size = new System.Drawing.Size( 34, 13 );
            townLabel.TabIndex = 15;
            townLabel.Text = "Town";
            // 
            // prereqGroupBox
            // 
            prereqGroupBox.AutoSize = true;
            prereqGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            prereqGroupBox.Controls.Add( this.dateRangeLabel );
            prereqGroupBox.Controls.Add( this.unlockedComboBox );
            prereqGroupBox.Controls.Add( unlockedLabel );
            prereqGroupBox.Controls.Add( this.certainDateComboBox );
            prereqGroupBox.Controls.Add( this.certainDateLabel );
            prereqGroupBox.Controls.Add( this.completeJobComboBox );
            prereqGroupBox.Controls.Add( this.completeJobLabel );
            prereqGroupBox.Controls.Add( this.rawByteLabel );
            prereqGroupBox.Controls.Add( this.prereqByteSpinner );
            prereqGroupBox.Controls.Add( prereqTypeLabel );
            prereqGroupBox.Controls.Add( this.prereqTypeComboBox );
            prereqGroupBox.Location = new System.Drawing.Point( 253, 3 );
            prereqGroupBox.Name = "prereqGroupBox";
            prereqGroupBox.Size = new System.Drawing.Size( 283, 179 );
            prereqGroupBox.TabIndex = 9;
            prereqGroupBox.TabStop = false;
            prereqGroupBox.Text = "Prerequisites";
            // 
            // dateRangeLabel
            // 
            this.dateRangeLabel.AutoSize = true;
            this.dateRangeLabel.Location = new System.Drawing.Point( 102, 123 );
            this.dateRangeLabel.Name = "dateRangeLabel";
            this.dateRangeLabel.Size = new System.Drawing.Size( 0, 13 );
            this.dateRangeLabel.TabIndex = 24;
            // 
            // unlockedComboBox
            // 
            this.unlockedComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.unlockedComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.unlockedComboBox.FormattingEnabled = true;
            this.unlockedComboBox.Location = new System.Drawing.Point( 64, 139 );
            this.unlockedComboBox.Name = "unlockedComboBox";
            this.unlockedComboBox.Size = new System.Drawing.Size( 213, 21 );
            this.unlockedComboBox.TabIndex = 4;
            this.unlockedComboBox.Tag = "WhenUnlocked";
            this.unlockedComboBox.SelectedIndexChanged += new System.EventHandler( this.unlockedComboBox_SelectedIndexChanged );
            // 
            // unlockedLabel
            // 
            unlockedLabel.AutoSize = true;
            unlockedLabel.Location = new System.Drawing.Point( 6, 142 );
            unlockedLabel.Name = "unlockedLabel";
            unlockedLabel.Size = new System.Drawing.Size( 53, 13 );
            unlockedLabel.TabIndex = 23;
            unlockedLabel.Text = "Unlocked";
            // 
            // certainDateComboBox
            // 
            this.certainDateComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.certainDateComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.certainDateComboBox.FormattingEnabled = true;
            this.certainDateComboBox.Location = new System.Drawing.Point( 93, 99 );
            this.certainDateComboBox.Name = "certainDateComboBox";
            this.certainDateComboBox.Size = new System.Drawing.Size( 184, 21 );
            this.certainDateComboBox.TabIndex = 3;
            this.certainDateComboBox.Tag = "PrereqZodiac";
            this.certainDateComboBox.SelectedIndexChanged += new System.EventHandler( this.certainDateComboBox_SelectedIndexChanged );
            // 
            // certainDateLabel
            // 
            this.certainDateLabel.AutoSize = true;
            this.certainDateLabel.Location = new System.Drawing.Point( 19, 102 );
            this.certainDateLabel.Name = "certainDateLabel";
            this.certainDateLabel.Size = new System.Drawing.Size( 64, 13 );
            this.certainDateLabel.TabIndex = 21;
            this.certainDateLabel.Text = "Certain date";
            // 
            // completeJobComboBox
            // 
            this.completeJobComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.completeJobComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.completeJobComboBox.FormattingEnabled = true;
            this.completeJobComboBox.Location = new System.Drawing.Point( 93, 72 );
            this.completeJobComboBox.Name = "completeJobComboBox";
            this.completeJobComboBox.Size = new System.Drawing.Size( 184, 21 );
            this.completeJobComboBox.TabIndex = 2;
            this.completeJobComboBox.Tag = "PrereqProp";
            this.completeJobComboBox.SelectedIndexChanged += new System.EventHandler( this.completeJobComboBox_SelectedIndexChanged );
            // 
            // completeJobLabel
            // 
            this.completeJobLabel.AutoSize = true;
            this.completeJobLabel.Location = new System.Drawing.Point( 19, 75 );
            this.completeJobLabel.Name = "completeJobLabel";
            this.completeJobLabel.Size = new System.Drawing.Size( 68, 13 );
            this.completeJobLabel.TabIndex = 19;
            this.completeJobLabel.Text = "Complete job";
            // 
            // rawByteLabel
            // 
            this.rawByteLabel.AutoSize = true;
            this.rawByteLabel.Location = new System.Drawing.Point( 6, 48 );
            this.rawByteLabel.Name = "rawByteLabel";
            this.rawByteLabel.Size = new System.Drawing.Size( 52, 13 );
            this.rawByteLabel.TabIndex = 18;
            this.rawByteLabel.Text = "Raw byte";
            // 
            // prereqByteSpinner
            // 
            this.prereqByteSpinner.Hexadecimal = true;
            this.prereqByteSpinner.Location = new System.Drawing.Point( 64, 46 );
            this.prereqByteSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.prereqByteSpinner.Name = "prereqByteSpinner";
            this.prereqByteSpinner.Size = new System.Drawing.Size( 69, 20 );
            this.prereqByteSpinner.TabIndex = 1;
            this.prereqByteSpinner.Tag = "PrereqByte";
            this.prereqByteSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.prereqByteSpinner.ValueChanged += new System.EventHandler( this.prereqByteSpinner_ValueChanged );
            // 
            // prereqTypeLabel
            // 
            prereqTypeLabel.AutoSize = true;
            prereqTypeLabel.Location = new System.Drawing.Point( 6, 22 );
            prereqTypeLabel.Name = "prereqTypeLabel";
            prereqTypeLabel.Size = new System.Drawing.Size( 31, 13 );
            prereqTypeLabel.TabIndex = 17;
            prereqTypeLabel.Text = "Type";
            // 
            // prereqTypeComboBox
            // 
            this.prereqTypeComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.prereqTypeComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.prereqTypeComboBox.FormattingEnabled = true;
            this.prereqTypeComboBox.Location = new System.Drawing.Point( 64, 19 );
            this.prereqTypeComboBox.Name = "prereqTypeComboBox";
            this.prereqTypeComboBox.Size = new System.Drawing.Size( 213, 21 );
            this.prereqTypeComboBox.TabIndex = 0;
            this.prereqTypeComboBox.Tag = "PrereqType";
            this.prereqTypeComboBox.SelectedIndexChanged += new System.EventHandler( this.prereqTypeComboBox_SelectedIndexChanged );
            // 
            // unknownGroupBox
            // 
            unknownGroupBox.AutoSize = true;
            unknownGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            unknownGroupBox.Controls.Add( this.unknown0x0BSpinner );
            unknownGroupBox.Controls.Add( this.unknown0x0CSpinner );
            unknownGroupBox.Controls.Add( this.unknown0x0FSpinner );
            unknownGroupBox.Controls.Add( this.unknown0x14Spinner );
            unknownGroupBox.Controls.Add( this.unknown0x09Spinner );
            unknownGroupBox.Location = new System.Drawing.Point( 6, 167 );
            unknownGroupBox.Name = "unknownGroupBox";
            unknownGroupBox.Size = new System.Drawing.Size( 147, 84 );
            unknownGroupBox.TabIndex = 8;
            unknownGroupBox.TabStop = false;
            unknownGroupBox.Text = "Unknown";
            // 
            // unknown0x0BSpinner
            // 
            this.unknown0x0BSpinner.Hexadecimal = true;
            this.unknown0x0BSpinner.Location = new System.Drawing.Point( 53, 19 );
            this.unknown0x0BSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.unknown0x0BSpinner.Name = "unknown0x0BSpinner";
            this.unknown0x0BSpinner.Size = new System.Drawing.Size( 41, 20 );
            this.unknown0x0BSpinner.TabIndex = 1;
            this.unknown0x0BSpinner.Tag = "Unknown0x0B";
            this.unknown0x0BSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.unknown0x0BSpinner.ValueChanged += new System.EventHandler( this.spinner_ValueChanged );
            // 
            // unknown0x0CSpinner
            // 
            this.unknown0x0CSpinner.Hexadecimal = true;
            this.unknown0x0CSpinner.Location = new System.Drawing.Point( 100, 19 );
            this.unknown0x0CSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.unknown0x0CSpinner.Name = "unknown0x0CSpinner";
            this.unknown0x0CSpinner.Size = new System.Drawing.Size( 41, 20 );
            this.unknown0x0CSpinner.TabIndex = 2;
            this.unknown0x0CSpinner.Tag = "Unknown0x0C";
            this.unknown0x0CSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.unknown0x0CSpinner.ValueChanged += new System.EventHandler( this.spinner_ValueChanged );
            // 
            // unknown0x0FSpinner
            // 
            this.unknown0x0FSpinner.Hexadecimal = true;
            this.unknown0x0FSpinner.Location = new System.Drawing.Point( 6, 45 );
            this.unknown0x0FSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.unknown0x0FSpinner.Name = "unknown0x0FSpinner";
            this.unknown0x0FSpinner.Size = new System.Drawing.Size( 41, 20 );
            this.unknown0x0FSpinner.TabIndex = 3;
            this.unknown0x0FSpinner.Tag = "Unknown0x0F";
            this.unknown0x0FSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.unknown0x0FSpinner.ValueChanged += new System.EventHandler( this.spinner_ValueChanged );
            // 
            // unknown0x14Spinner
            // 
            this.unknown0x14Spinner.Hexadecimal = true;
            this.unknown0x14Spinner.Location = new System.Drawing.Point( 53, 45 );
            this.unknown0x14Spinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.unknown0x14Spinner.Name = "unknown0x14Spinner";
            this.unknown0x14Spinner.Size = new System.Drawing.Size( 41, 20 );
            this.unknown0x14Spinner.TabIndex = 4;
            this.unknown0x14Spinner.Tag = "Unknown0x14";
            this.unknown0x14Spinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.unknown0x14Spinner.ValueChanged += new System.EventHandler( this.spinner_ValueChanged );
            // 
            // unknown0x09Spinner
            // 
            this.unknown0x09Spinner.Hexadecimal = true;
            this.unknown0x09Spinner.Location = new System.Drawing.Point( 6, 19 );
            this.unknown0x09Spinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.unknown0x09Spinner.Name = "unknown0x09Spinner";
            this.unknown0x09Spinner.Size = new System.Drawing.Size( 41, 20 );
            this.unknown0x09Spinner.TabIndex = 0;
            this.unknown0x09Spinner.Tag = "Unknown0x09";
            this.unknown0x09Spinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.unknown0x09Spinner.ValueChanged += new System.EventHandler( this.spinner_ValueChanged );
            // 
            // jobTypeComboBox
            // 
            this.jobTypeComboBox.FormattingEnabled = true;
            this.jobTypeComboBox.Location = new System.Drawing.Point( 84, 3 );
            this.jobTypeComboBox.Name = "jobTypeComboBox";
            this.jobTypeComboBox.Size = new System.Drawing.Size( 121, 21 );
            this.jobTypeComboBox.TabIndex = 0;
            this.jobTypeComboBox.Tag = "Type";
            this.jobTypeComboBox.SelectedIndexChanged += new System.EventHandler( this.enumComboBox_SelectedIndexChanged );
            // 
            // preferredStatsComboBox
            // 
            this.preferredStatsComboBox.FormattingEnabled = true;
            this.preferredStatsComboBox.Location = new System.Drawing.Point( 84, 30 );
            this.preferredStatsComboBox.Name = "preferredStatsComboBox";
            this.preferredStatsComboBox.Size = new System.Drawing.Size( 121, 21 );
            this.preferredStatsComboBox.TabIndex = 1;
            this.preferredStatsComboBox.Tag = "BraveFaith";
            this.preferredStatsComboBox.SelectedIndexChanged += new System.EventHandler( this.enumComboBox_SelectedIndexChanged );
            // 
            // price1ComboBox
            // 
            this.price1ComboBox.FormattingEnabled = true;
            this.price1ComboBox.Location = new System.Drawing.Point( 84, 57 );
            this.price1ComboBox.Name = "price1ComboBox";
            this.price1ComboBox.Size = new System.Drawing.Size( 69, 21 );
            this.price1ComboBox.TabIndex = 2;
            this.price1ComboBox.Tag = "PriceIndex1";
            this.price1ComboBox.SelectedIndexChanged += new System.EventHandler( this.priceComboBox_SelectedIndexChanged );
            // 
            // price2ComboBox
            // 
            this.price2ComboBox.FormattingEnabled = true;
            this.price2ComboBox.Location = new System.Drawing.Point( 178, 57 );
            this.price2ComboBox.Name = "price2ComboBox";
            this.price2ComboBox.Size = new System.Drawing.Size( 69, 21 );
            this.price2ComboBox.TabIndex = 3;
            this.price2ComboBox.Tag = "PriceIndex2";
            this.price2ComboBox.SelectedIndexChanged += new System.EventHandler( this.priceComboBox_SelectedIndexChanged );
            // 
            // minDaysSpinner
            // 
            this.minDaysSpinner.Location = new System.Drawing.Point( 84, 87 );
            this.minDaysSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.minDaysSpinner.Name = "minDaysSpinner";
            this.minDaysSpinner.Size = new System.Drawing.Size( 69, 20 );
            this.minDaysSpinner.TabIndex = 4;
            this.minDaysSpinner.Tag = "MinDays";
            this.minDaysSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.minDaysSpinner.ValueChanged += new System.EventHandler( this.spinner_ValueChanged );
            // 
            // maxDaysSpinner
            // 
            this.maxDaysSpinner.Location = new System.Drawing.Point( 178, 87 );
            this.maxDaysSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.maxDaysSpinner.Name = "maxDaysSpinner";
            this.maxDaysSpinner.Size = new System.Drawing.Size( 69, 20 );
            this.maxDaysSpinner.TabIndex = 5;
            this.maxDaysSpinner.Tag = "MaxDays";
            this.maxDaysSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maxDaysSpinner.ValueChanged += new System.EventHandler( this.spinner_ValueChanged );
            // 
            // rewardComboBox
            // 
            this.rewardComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.rewardComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rewardComboBox.FormattingEnabled = true;
            this.rewardComboBox.Location = new System.Drawing.Point( 84, 113 );
            this.rewardComboBox.Name = "rewardComboBox";
            this.rewardComboBox.Size = new System.Drawing.Size( 121, 21 );
            this.rewardComboBox.TabIndex = 6;
            this.rewardComboBox.Tag = "Reward";
            this.rewardComboBox.SelectedIndexChanged += new System.EventHandler( this.enumComboBox_SelectedIndexChanged );
            // 
            // townComboBox
            // 
            this.townComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.townComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.townComboBox.FormattingEnabled = true;
            this.townComboBox.Location = new System.Drawing.Point( 84, 140 );
            this.townComboBox.Name = "townComboBox";
            this.townComboBox.Size = new System.Drawing.Size( 163, 21 );
            this.townComboBox.TabIndex = 7;
            this.townComboBox.Tag = "Town";
            this.townComboBox.SelectedIndexChanged += new System.EventHandler( this.enumComboBox_SelectedIndexChanged );
            // 
            // PropositionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add( unknownGroupBox );
            this.Controls.Add( prereqGroupBox );
            this.Controls.Add( townLabel );
            this.Controls.Add( this.townComboBox );
            this.Controls.Add( rewardLabel );
            this.Controls.Add( this.rewardComboBox );
            this.Controls.Add( toLabel );
            this.Controls.Add( this.maxDaysSpinner );
            this.Controls.Add( this.minDaysSpinner );
            this.Controls.Add( daysLabel );
            this.Controls.Add( plusLabel );
            this.Controls.Add( this.price2ComboBox );
            this.Controls.Add( this.price1ComboBox );
            this.Controls.Add( priceLabel );
            this.Controls.Add( preferredStatsLabel );
            this.Controls.Add( this.preferredStatsComboBox );
            this.Controls.Add( jobTypeLabel );
            this.Controls.Add( this.jobTypeComboBox );
            this.Name = "PropositionEditor";
            this.Size = new System.Drawing.Size( 539, 254 );
            prereqGroupBox.ResumeLayout( false );
            prereqGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prereqByteSpinner)).EndInit();
            unknownGroupBox.ResumeLayout( false );
            ((System.ComponentModel.ISupportInitialize)(this.unknown0x0BSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown0x0CSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown0x0FSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown0x14Spinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unknown0x09Spinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minDaysSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxDaysSpinner)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private FFTPatcher.Controls.ComboBoxWithDefault jobTypeComboBox;
        private FFTPatcher.Controls.ComboBoxWithDefault preferredStatsComboBox;
        private FFTPatcher.Controls.ComboBoxWithDefault price1ComboBox;
        private FFTPatcher.Controls.ComboBoxWithDefault price2ComboBox;
        private FFTPatcher.Controls.NumericUpDownWithDefault minDaysSpinner;
        private FFTPatcher.Controls.NumericUpDownWithDefault maxDaysSpinner;
        private FFTPatcher.Controls.ComboBoxWithDefault rewardComboBox;
        private FFTPatcher.Controls.ComboBoxWithDefault townComboBox;
        private FFTPatcher.Controls.NumericUpDownWithDefault prereqByteSpinner;
        private FFTPatcher.Controls.ComboBoxWithDefault prereqTypeComboBox;
        private FFTPatcher.Controls.ComboBoxWithDefault unlockedComboBox;
        private FFTPatcher.Controls.ComboBoxWithDefault certainDateComboBox;
        private System.Windows.Forms.Label certainDateLabel;
        private FFTPatcher.Controls.ComboBoxWithDefault completeJobComboBox;
        private System.Windows.Forms.Label completeJobLabel;
        private FFTPatcher.Controls.NumericUpDownWithDefault unknown0x0BSpinner;
        private FFTPatcher.Controls.NumericUpDownWithDefault unknown0x0CSpinner;
        private FFTPatcher.Controls.NumericUpDownWithDefault unknown0x0FSpinner;
        private FFTPatcher.Controls.NumericUpDownWithDefault unknown0x14Spinner;
        private FFTPatcher.Controls.NumericUpDownWithDefault unknown0x09Spinner;
        private System.Windows.Forms.Label dateRangeLabel;
        private System.Windows.Forms.Label rawByteLabel;
    }
}
