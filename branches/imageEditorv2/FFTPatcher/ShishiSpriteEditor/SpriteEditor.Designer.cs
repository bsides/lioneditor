namespace FFTPatcher.SpriteEditor
{
    partial class SpriteEditor
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
            System.Windows.Forms.GroupBox paletteGroupBox;
            this.portraitCheckbox = new System.Windows.Forms.CheckBox();
            this.paletteSelector = new System.Windows.Forms.ComboBox();
            this.shapesListBox = new System.Windows.Forms.ListBox();
            this.shpComboBox = new System.Windows.Forms.ComboBox();
            this.seqComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.spriteViewer1 = new FFTPatcher.SpriteEditor.SpriteViewer();
            this.flyingCheckbox = new System.Windows.Forms.CheckBox();
            this.flagsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            paletteGroupBox = new System.Windows.Forms.GroupBox();
            paletteGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // paletteGroupBox
            // 
            paletteGroupBox.AutoSize = true;
            paletteGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            paletteGroupBox.Controls.Add(this.portraitCheckbox);
            paletteGroupBox.Controls.Add(this.paletteSelector);
            paletteGroupBox.Location = new System.Drawing.Point(323, 3);
            paletteGroupBox.Name = "paletteGroupBox";
            paletteGroupBox.Size = new System.Drawing.Size(185, 106);
            paletteGroupBox.TabIndex = 4;
            paletteGroupBox.TabStop = false;
            paletteGroupBox.Text = "Palette";
            // 
            // portraitCheckbox
            // 
            this.portraitCheckbox.Checked = true;
            this.portraitCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.portraitCheckbox.Location = new System.Drawing.Point(6, 50);
            this.portraitCheckbox.Name = "portraitCheckbox";
            this.portraitCheckbox.Size = new System.Drawing.Size(153, 37);
            this.portraitCheckbox.TabIndex = 3;
            this.portraitCheckbox.Text = "Always use corresponding palette for portrait";
            this.portraitCheckbox.UseVisualStyleBackColor = true;
            this.portraitCheckbox.CheckedChanged += new System.EventHandler(this.PaletteChanged);
            // 
            // paletteSelector
            // 
            this.paletteSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteSelector.FormattingEnabled = true;
            this.paletteSelector.Items.AddRange(new object[] {
            "Sprite #1",
            "Sprite #2",
            "Sprite #3",
            "Sprite #4",
            "Sprite #5",
            "Sprite #6",
            "Sprite #7",
            "Sprite #8",
            "Portrait #1",
            "Portrait #2",
            "Portrait #3",
            "Portrait #4",
            "Portrait #5",
            "Portrait #6",
            "Portrait #7",
            "Portrait #8"});
            this.paletteSelector.Location = new System.Drawing.Point(6, 19);
            this.paletteSelector.Name = "paletteSelector";
            this.paletteSelector.Size = new System.Drawing.Size(173, 21);
            this.paletteSelector.TabIndex = 0;
            this.paletteSelector.SelectedIndexChanged += new System.EventHandler(this.PaletteChanged);
            // 
            // shapesListBox
            // 
            this.shapesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shapesListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.shapesListBox.FormattingEnabled = true;
            this.shapesListBox.IntegralHeight = false;
            this.shapesListBox.ItemHeight = 180;
            this.shapesListBox.Location = new System.Drawing.Point(323, 115);
            this.shapesListBox.Name = "shapesListBox";
            this.shapesListBox.Size = new System.Drawing.Size(297, 368);
            this.shapesListBox.TabIndex = 5;
            // 
            // shpComboBox
            // 
            this.shpComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.shpComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shpComboBox.FormattingEnabled = true;
            this.shpComboBox.Location = new System.Drawing.Point(361, 489);
            this.shpComboBox.Name = "shpComboBox";
            this.shpComboBox.Size = new System.Drawing.Size(121, 21);
            this.shpComboBox.TabIndex = 6;
            this.shpComboBox.SelectedIndexChanged += new System.EventHandler(this.shpComboBox_SelectedIndexChanged);
            // 
            // seqComboBox
            // 
            this.seqComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.seqComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seqComboBox.FormattingEnabled = true;
            this.seqComboBox.Location = new System.Drawing.Point(361, 516);
            this.seqComboBox.Name = "seqComboBox";
            this.seqComboBox.Size = new System.Drawing.Size(121, 21);
            this.seqComboBox.TabIndex = 7;
            this.seqComboBox.SelectedIndexChanged += new System.EventHandler(this.seqComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(323, 492);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "SHP:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(323, 519);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "SEQ:";
            // 
            // spriteViewer1
            // 
            this.spriteViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.spriteViewer1.AutoScroll = true;
            this.spriteViewer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spriteViewer1.Location = new System.Drawing.Point(3, 3);
            this.spriteViewer1.Name = "spriteViewer1";
            this.spriteViewer1.Size = new System.Drawing.Size(314, 625);
            this.spriteViewer1.Sprite = null;
            this.spriteViewer1.TabIndex = 0;
            // 
            // flyingCheckbox
            // 
            this.flyingCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flyingCheckbox.AutoSize = true;
            this.flyingCheckbox.Location = new System.Drawing.Point(323, 543);
            this.flyingCheckbox.Name = "flyingCheckbox";
            this.flyingCheckbox.Size = new System.Drawing.Size(59, 17);
            this.flyingCheckbox.TabIndex = 10;
            this.flyingCheckbox.Text = "Flying?";
            this.flyingCheckbox.UseVisualStyleBackColor = true;
            this.flyingCheckbox.CheckedChanged += new System.EventHandler(this.flyingCheckbox_CheckedChanged);
            // 
            // flagsCheckedListBox
            // 
            this.flagsCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flagsCheckedListBox.FormattingEnabled = true;
            this.flagsCheckedListBox.Items.AddRange(new object[] {
            "Flag 1",
            "Flag 2",
            "Flag 3",
            "Flag 4",
            "Flag 5",
            "Flag 6",
            "Flag 7",
            "Flag 8"});
            this.flagsCheckedListBox.Location = new System.Drawing.Point(488, 489);
            this.flagsCheckedListBox.Name = "flagsCheckedListBox";
            this.flagsCheckedListBox.Size = new System.Drawing.Size(120, 124);
            this.flagsCheckedListBox.TabIndex = 11;
            this.flagsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.flagsCheckedListBox_ItemCheck);
            // 
            // SpriteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flagsCheckedListBox);
            this.Controls.Add(this.flyingCheckbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.seqComboBox);
            this.Controls.Add(this.shpComboBox);
            this.Controls.Add(this.shapesListBox);
            this.Controls.Add(paletteGroupBox);
            this.Controls.Add(this.spriteViewer1);
            this.Name = "SpriteEditor";
            this.Size = new System.Drawing.Size(623, 631);
            paletteGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SpriteViewer spriteViewer1;
        private System.Windows.Forms.CheckBox portraitCheckbox;
        private System.Windows.Forms.ListBox shapesListBox;
        private System.Windows.Forms.ComboBox paletteSelector;
        private System.Windows.Forms.ComboBox shpComboBox;
        private System.Windows.Forms.ComboBox seqComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox flyingCheckbox;
        private System.Windows.Forms.CheckedListBox flagsCheckedListBox;
    }
}
