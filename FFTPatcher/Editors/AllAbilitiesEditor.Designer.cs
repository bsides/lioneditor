namespace FFTPatcher.Editors
{
    partial class AllAbilitiesEditor
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
            this.abilitiesListBox = new System.Windows.Forms.ListBox();
            this.abilityEditor = new FFTPatcher.Editors.AbilityEditor();
            this.SuspendLayout();
            // 
            // abilitiesListBox
            // 
            this.abilitiesListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.abilitiesListBox.FormattingEnabled = true;
            this.abilitiesListBox.Location = new System.Drawing.Point( 0, 0 );
            this.abilitiesListBox.Name = "abilitiesListBox";
            this.abilitiesListBox.Size = new System.Drawing.Size( 120, 693 );
            this.abilitiesListBox.TabIndex = 0;
            // 
            // abilityEditor
            // 
            this.abilityEditor.Ability = null;
            this.abilityEditor.AutoScroll = true;
            this.abilityEditor.AutoSize = true;
            this.abilityEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.abilityEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.abilityEditor.Location = new System.Drawing.Point( 120, 0 );
            this.abilityEditor.Name = "abilityEditor";
            this.abilityEditor.Size = new System.Drawing.Size( 563, 695 );
            this.abilityEditor.TabIndex = 1;
            // 
            // AllAbilitiesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add( this.abilityEditor );
            this.Controls.Add( this.abilitiesListBox );
            this.Name = "AllAbilitiesEditor";
            this.Size = new System.Drawing.Size( 683, 695 );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox abilitiesListBox;
        private AbilityEditor abilityEditor;
    }
}
