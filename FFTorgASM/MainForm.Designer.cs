namespace FFTorgASM
{
    partial class MainForm
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
            if ( disposing && ( components != null ) )
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.patchButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.reloadButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.variableSpinner = new System.Windows.Forms.NumericUpDown();
            this.variableComboBox = new System.Windows.Forms.ComboBox();
            ( (System.ComponentModel.ISupportInitialize)( this.variableSpinner ) ).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point( 12, 12 );
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size( 441, 289 );
            this.checkedListBox1.TabIndex = 0;
            // 
            // patchButton
            // 
            this.patchButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.patchButton.Location = new System.Drawing.Point( 378, 416 );
            this.patchButton.Name = "patchButton";
            this.patchButton.Size = new System.Drawing.Size( 75, 23 );
            this.patchButton.TabIndex = 1;
            this.patchButton.Text = "Patch ISO...";
            this.patchButton.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.CheckFileExists = true;
            this.saveFileDialog1.OverwritePrompt = false;
            // 
            // reloadButton
            // 
            this.reloadButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.reloadButton.Location = new System.Drawing.Point( 12, 416 );
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size( 75, 23 );
            this.reloadButton.TabIndex = 2;
            this.reloadButton.Text = "Reload";
            this.reloadButton.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.textBox1.Location = new System.Drawing.Point( 12, 307 );
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size( 441, 73 );
            this.textBox1.TabIndex = 3;
            // 
            // variableSpinner
            // 
            this.variableSpinner.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.variableSpinner.Hexadecimal = true;
            this.variableSpinner.Location = new System.Drawing.Point( 179, 388 );
            this.variableSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.variableSpinner.Name = "variableSpinner";
            this.variableSpinner.Size = new System.Drawing.Size( 54, 20 );
            this.variableSpinner.TabIndex = 5;
            this.variableSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.variableSpinner.Visible = false;
            // 
            // variableComboBox
            // 
            this.variableComboBox.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.variableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.variableComboBox.FormattingEnabled = true;
            this.variableComboBox.Location = new System.Drawing.Point( 12, 387 );
            this.variableComboBox.Name = "variableComboBox";
            this.variableComboBox.Size = new System.Drawing.Size( 161, 21 );
            this.variableComboBox.TabIndex = 6;
            this.variableComboBox.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 465, 451 );
            this.Controls.Add( this.variableComboBox );
            this.Controls.Add( this.variableSpinner );
            this.Controls.Add( this.textBox1 );
            this.Controls.Add( this.reloadButton );
            this.Controls.Add( this.patchButton );
            this.Controls.Add( this.checkedListBox1 );
            this.Name = "MainForm";
            this.Text = "FFTorgASM";
            ( (System.ComponentModel.ISupportInitialize)( this.variableSpinner ) ).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button patchButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button reloadButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown variableSpinner;
        private System.Windows.Forms.ComboBox variableComboBox;
    }
}

