namespace FFTPatcher.TextEditor.Editors
{
    partial class FileEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.stringListEditor1 = new FFTPatcher.TextEditor.StringListEditor();
            this.sectionComboBox = new System.Windows.Forms.ComboBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // stringListEditor1
            // 
            this.stringListEditor1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.stringListEditor1.Location = new System.Drawing.Point( 3, 31 );
            this.stringListEditor1.Name = "stringListEditor1";
            this.stringListEditor1.Size = new System.Drawing.Size( 844, 467 );
            this.stringListEditor1.TabIndex = 0;
            // 
            // sectionComboBox
            // 
            this.sectionComboBox.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.sectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sectionComboBox.FormattingEnabled = true;
            this.sectionComboBox.Location = new System.Drawing.Point( 4, 4 );
            this.sectionComboBox.Name = "sectionComboBox";
            this.sectionComboBox.Size = new System.Drawing.Size( 843, 21 );
            this.sectionComboBox.TabIndex = 1;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.errorLabel.Location = new System.Drawing.Point( 4, 505 );
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size( 843, 122 );
            this.errorLabel.TabIndex = 2;
            this.errorLabel.Visible = false;
            // 
            // FileEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.errorLabel );
            this.Controls.Add( this.sectionComboBox );
            this.Controls.Add( this.stringListEditor1 );
            this.Name = "FileEditor";
            this.Size = new System.Drawing.Size( 850, 627 );
            this.ResumeLayout( false );

        }

        #endregion

        private StringListEditor stringListEditor1;
        private System.Windows.Forms.ComboBox sectionComboBox;
        private System.Windows.Forms.Label errorLabel;
    }
}
