namespace FFTPatcher.Editors
{
    partial class AbilityAttributesEditor
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
            System.Windows.Forms.GroupBox attributesGroupBox;
            System.Windows.Forms.Label mpLabel;
            System.Windows.Forms.Label ctLabel;
            System.Windows.Forms.Label statusLabel;
            System.Windows.Forms.Label yLabel;
            System.Windows.Forms.Label xLabel;
            System.Windows.Forms.Label formulaLabel;
            System.Windows.Forms.Label verticalLabel;
            System.Windows.Forms.Label effectLabel;
            System.Windows.Forms.Label rangeLabel;
            System.Windows.Forms.Label hLabel1;
            System.Windows.Forms.Label hLabel2;
            this.flagsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.mpSpinner = new System.Windows.Forms.NumericUpDown();
            this.ctSpinner = new System.Windows.Forms.NumericUpDown();
            this.statusSpinner = new System.Windows.Forms.NumericUpDown();
            this.ySpinner = new System.Windows.Forms.NumericUpDown();
            this.xSpinner = new System.Windows.Forms.NumericUpDown();
            this.formulaSpinner = new System.Windows.Forms.NumericUpDown();
            this.verticalSpinner = new System.Windows.Forms.NumericUpDown();
            this.effectSpinner = new System.Windows.Forms.NumericUpDown();
            this.rangeSpinner = new System.Windows.Forms.NumericUpDown();
            this.elementsEditor = new FFTPatcher.Editors.ElementsEditor();
            attributesGroupBox = new System.Windows.Forms.GroupBox();
            mpLabel = new System.Windows.Forms.Label();
            ctLabel = new System.Windows.Forms.Label();
            statusLabel = new System.Windows.Forms.Label();
            yLabel = new System.Windows.Forms.Label();
            xLabel = new System.Windows.Forms.Label();
            formulaLabel = new System.Windows.Forms.Label();
            verticalLabel = new System.Windows.Forms.Label();
            effectLabel = new System.Windows.Forms.Label();
            rangeLabel = new System.Windows.Forms.Label();
            hLabel1 = new System.Windows.Forms.Label();
            hLabel2 = new System.Windows.Forms.Label();
            attributesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mpSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ySpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.formulaSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // attributesGroupBox
            // 
            attributesGroupBox.AutoSize = true;
            attributesGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            attributesGroupBox.Controls.Add(hLabel2);
            attributesGroupBox.Controls.Add(hLabel1);
            attributesGroupBox.Controls.Add(this.flagsCheckedListBox);
            attributesGroupBox.Controls.Add(this.mpSpinner);
            attributesGroupBox.Controls.Add(this.ctSpinner);
            attributesGroupBox.Controls.Add(this.statusSpinner);
            attributesGroupBox.Controls.Add(this.ySpinner);
            attributesGroupBox.Controls.Add(this.xSpinner);
            attributesGroupBox.Controls.Add(this.formulaSpinner);
            attributesGroupBox.Controls.Add(this.verticalSpinner);
            attributesGroupBox.Controls.Add(this.effectSpinner);
            attributesGroupBox.Controls.Add(this.rangeSpinner);
            attributesGroupBox.Controls.Add(mpLabel);
            attributesGroupBox.Controls.Add(ctLabel);
            attributesGroupBox.Controls.Add(statusLabel);
            attributesGroupBox.Controls.Add(yLabel);
            attributesGroupBox.Controls.Add(xLabel);
            attributesGroupBox.Controls.Add(formulaLabel);
            attributesGroupBox.Controls.Add(verticalLabel);
            attributesGroupBox.Controls.Add(effectLabel);
            attributesGroupBox.Controls.Add(rangeLabel);
            attributesGroupBox.Controls.Add(this.elementsEditor);
            attributesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            attributesGroupBox.Location = new System.Drawing.Point(0, 0);
            attributesGroupBox.Name = "attributesGroupBox";
            attributesGroupBox.Size = new System.Drawing.Size(314, 517);
            attributesGroupBox.TabIndex = 0;
            attributesGroupBox.TabStop = false;
            attributesGroupBox.Text = "Attributes";
            // 
            // flagsCheckedListBox
            // 
            this.flagsCheckedListBox.FormattingEnabled = true;
            this.flagsCheckedListBox.Items.AddRange(new object[] {
            "",
            "",
            "Ranged Weapon",
            "Vertical Fixed",
            "Vertical Tolerance",
            "Weapon Strike",
            "Auto",
            "Target Self",
            "Hit Enemies",
            "Hit Allies",
            "",
            "Follow Target",
            "Random Fire",
            "Linear Attack",
            "3 Directions",
            "Hit Caster",
            "Reflect",
            "Arithmeticks",
            "Silence",
            "Mimic",
            "Normal Attack?",
            "Perservere",
            "Quote",
            "Unknown",
            "Counter Flood",
            "Counter Magic",
            "Direct",
            "Shirahadori",
            "Requires Sword",
            "Requires Materia Blade",
            "Evadeable",
            "Targeting"});
            this.flagsCheckedListBox.Location = new System.Drawing.Point(164, 14);
            this.flagsCheckedListBox.Name = "flagsCheckedListBox";
            this.flagsCheckedListBox.Size = new System.Drawing.Size(144, 484);
            this.flagsCheckedListBox.TabIndex = 9;
            // 
            // mpSpinner
            // 
            this.mpSpinner.AutoSize = true;
            this.mpSpinner.Location = new System.Drawing.Point(102, 198);
            this.mpSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.mpSpinner.Name = "mpSpinner";
            this.mpSpinner.Size = new System.Drawing.Size(45, 20);
            this.mpSpinner.TabIndex = 8;
            this.mpSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mpSpinner.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // ctSpinner
            // 
            this.ctSpinner.AutoSize = true;
            this.ctSpinner.Location = new System.Drawing.Point(102, 175);
            this.ctSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ctSpinner.Name = "ctSpinner";
            this.ctSpinner.Size = new System.Drawing.Size(45, 20);
            this.ctSpinner.TabIndex = 7;
            this.ctSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ctSpinner.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // statusSpinner
            // 
            this.statusSpinner.AutoSize = true;
            this.statusSpinner.Hexadecimal = true;
            this.statusSpinner.Location = new System.Drawing.Point(102, 152);
            this.statusSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.statusSpinner.Name = "statusSpinner";
            this.statusSpinner.Size = new System.Drawing.Size(45, 20);
            this.statusSpinner.TabIndex = 6;
            this.statusSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.statusSpinner.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // ySpinner
            // 
            this.ySpinner.AutoSize = true;
            this.ySpinner.Location = new System.Drawing.Point(102, 129);
            this.ySpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ySpinner.Name = "ySpinner";
            this.ySpinner.Size = new System.Drawing.Size(45, 20);
            this.ySpinner.TabIndex = 5;
            this.ySpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ySpinner.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // xSpinner
            // 
            this.xSpinner.AutoSize = true;
            this.xSpinner.Location = new System.Drawing.Point(102, 106);
            this.xSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.xSpinner.Name = "xSpinner";
            this.xSpinner.Size = new System.Drawing.Size(45, 20);
            this.xSpinner.TabIndex = 4;
            this.xSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.xSpinner.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // formulaSpinner
            // 
            this.formulaSpinner.Hexadecimal = true;
            this.formulaSpinner.Location = new System.Drawing.Point(102, 83);
            this.formulaSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.formulaSpinner.Name = "formulaSpinner";
            this.formulaSpinner.Size = new System.Drawing.Size(45, 20);
            this.formulaSpinner.TabIndex = 3;
            this.formulaSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.formulaSpinner.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // verticalSpinner
            // 
            this.verticalSpinner.AutoSize = true;
            this.verticalSpinner.Location = new System.Drawing.Point(102, 60);
            this.verticalSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.verticalSpinner.Name = "verticalSpinner";
            this.verticalSpinner.Size = new System.Drawing.Size(45, 20);
            this.verticalSpinner.TabIndex = 2;
            this.verticalSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.verticalSpinner.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // effectSpinner
            // 
            this.effectSpinner.AutoSize = true;
            this.effectSpinner.Location = new System.Drawing.Point(102, 37);
            this.effectSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.effectSpinner.Name = "effectSpinner";
            this.effectSpinner.Size = new System.Drawing.Size(45, 20);
            this.effectSpinner.TabIndex = 1;
            this.effectSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.effectSpinner.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // rangeSpinner
            // 
            this.rangeSpinner.AutoSize = true;
            this.rangeSpinner.Location = new System.Drawing.Point(102, 14);
            this.rangeSpinner.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.rangeSpinner.Name = "rangeSpinner";
            this.rangeSpinner.Size = new System.Drawing.Size(45, 20);
            this.rangeSpinner.TabIndex = 0;
            this.rangeSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.rangeSpinner.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // mpLabel
            // 
            mpLabel.AutoSize = true;
            mpLabel.Location = new System.Drawing.Point(6, 200);
            mpLabel.Name = "mpLabel";
            mpLabel.Size = new System.Drawing.Size(50, 13);
            mpLabel.TabIndex = 9;
            mpLabel.Text = "MP Cost:";
            // 
            // ctLabel
            // 
            ctLabel.AutoSize = true;
            ctLabel.Location = new System.Drawing.Point(6, 177);
            ctLabel.Name = "ctLabel";
            ctLabel.Size = new System.Drawing.Size(24, 13);
            ctLabel.TabIndex = 8;
            ctLabel.Text = "CT:";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new System.Drawing.Point(6, 154);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(74, 13);
            statusLabel.TabIndex = 7;
            statusLabel.Text = "Inflict Status:";
            // 
            // yLabel
            // 
            yLabel.AutoSize = true;
            yLabel.Location = new System.Drawing.Point(16, 131);
            yLabel.Name = "yLabel";
            yLabel.Size = new System.Drawing.Size(14, 13);
            yLabel.TabIndex = 6;
            yLabel.Text = "Y";
            // 
            // xLabel
            // 
            xLabel.AutoSize = true;
            xLabel.Location = new System.Drawing.Point(16, 108);
            xLabel.Name = "xLabel";
            xLabel.Size = new System.Drawing.Size(14, 13);
            xLabel.TabIndex = 5;
            xLabel.Text = "X";
            // 
            // formulaLabel
            // 
            formulaLabel.AutoSize = true;
            formulaLabel.Location = new System.Drawing.Point(6, 85);
            formulaLabel.Name = "formulaLabel";
            formulaLabel.Size = new System.Drawing.Size(47, 13);
            formulaLabel.TabIndex = 4;
            formulaLabel.Text = "Formula:";
            // 
            // verticalLabel
            // 
            verticalLabel.AutoSize = true;
            verticalLabel.Location = new System.Drawing.Point(6, 62);
            verticalLabel.Name = "verticalLabel";
            verticalLabel.Size = new System.Drawing.Size(45, 13);
            verticalLabel.TabIndex = 3;
            verticalLabel.Text = "Vertical:";
            // 
            // effectLabel
            // 
            effectLabel.AutoSize = true;
            effectLabel.Location = new System.Drawing.Point(6, 39);
            effectLabel.Name = "effectLabel";
            effectLabel.Size = new System.Drawing.Size(38, 13);
            effectLabel.TabIndex = 2;
            effectLabel.Text = "Effect:";
            // 
            // rangeLabel
            // 
            rangeLabel.AutoSize = true;
            rangeLabel.Location = new System.Drawing.Point(6, 16);
            rangeLabel.Name = "rangeLabel";
            rangeLabel.Size = new System.Drawing.Size(42, 13);
            rangeLabel.TabIndex = 1;
            rangeLabel.Text = "Range:";
            // 
            // elementsEditor
            // 
            this.elementsEditor.AutoSize = true;
            this.elementsEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.elementsEditor.GroupBoxText = "Elements";
            this.elementsEditor.Location = new System.Drawing.Point(9, 224);
            this.elementsEditor.Name = "elementsEditor";
            this.elementsEditor.Size = new System.Drawing.Size(94, 162);
            this.elementsEditor.TabIndex = 0;
            this.elementsEditor.TabStop = false;
            // 
            // hLabel1
            // 
            hLabel1.AutoSize = true;
            hLabel1.Location = new System.Drawing.Point(148, 85);
            hLabel1.Name = "hLabel1";
            hLabel1.Size = new System.Drawing.Size(13, 13);
            hLabel1.TabIndex = 10;
            hLabel1.Text = "h";
            // 
            // hLabel2
            // 
            hLabel2.AutoSize = true;
            hLabel2.Location = new System.Drawing.Point(148, 154);
            hLabel2.Name = "hLabel2";
            hLabel2.Size = new System.Drawing.Size(13, 13);
            hLabel2.TabIndex = 11;
            hLabel2.Text = "h";
            // 
            // AbilityAttributesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(attributesGroupBox);
            this.Name = "AbilityAttributesEditor";
            this.Size = new System.Drawing.Size(314, 517);
            attributesGroupBox.ResumeLayout(false);
            attributesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mpSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ySpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.formulaSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.effectSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeSpinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ElementsEditor elementsEditor;
        private System.Windows.Forms.NumericUpDown mpSpinner;
        private System.Windows.Forms.NumericUpDown ctSpinner;
        private System.Windows.Forms.NumericUpDown statusSpinner;
        private System.Windows.Forms.NumericUpDown ySpinner;
        private System.Windows.Forms.NumericUpDown xSpinner;
        private System.Windows.Forms.NumericUpDown formulaSpinner;
        private System.Windows.Forms.NumericUpDown verticalSpinner;
        private System.Windows.Forms.NumericUpDown effectSpinner;
        private System.Windows.Forms.NumericUpDown rangeSpinner;
        private System.Windows.Forms.CheckedListBox flagsCheckedListBox;
    }
}
