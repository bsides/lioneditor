namespace LionEditor
{
    partial class SavegameEditor
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
            System.Windows.Forms.TabControl tabControl1;
            System.Windows.Forms.SplitContainer splitContainer3;
            this.charactersTab = new System.Windows.Forms.TabPage();
            this.characterSelector = new System.Windows.Forms.CheckedListBox();
            this.characterEditor1 = new CharacterEditor();
            this.braveStoryTab = new System.Windows.Forms.TabPage();
            this.inventoryTab = new System.Windows.Forms.TabPage();
            tabControl1 = new System.Windows.Forms.TabControl();
            splitContainer3 = new System.Windows.Forms.SplitContainer();
            tabControl1.SuspendLayout();
            this.charactersTab.SuspendLayout();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add( this.charactersTab );
            tabControl1.Controls.Add( this.braveStoryTab );
            tabControl1.Controls.Add( this.inventoryTab );
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point( 0, 0 );
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size( 725, 476 );
            tabControl1.TabIndex = 1;
            // 
            // charactersTab
            // 
            this.charactersTab.Controls.Add( splitContainer3 );
            this.charactersTab.Location = new System.Drawing.Point( 4, 22 );
            this.charactersTab.Name = "charactersTab";
            this.charactersTab.Padding = new System.Windows.Forms.Padding( 3 );
            this.charactersTab.Size = new System.Drawing.Size( 717, 450 );
            this.charactersTab.TabIndex = 0;
            this.charactersTab.Text = "Characters";
            this.charactersTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer3.IsSplitterFixed = true;
            splitContainer3.Location = new System.Drawing.Point( 3, 3 );
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add( this.characterSelector );
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add( this.characterEditor1 );
            splitContainer3.Size = new System.Drawing.Size( 711, 444 );
            splitContainer3.SplitterDistance = 130;
            splitContainer3.TabIndex = 0;
            splitContainer3.TabStop = false;
            // 
            // characterSelector
            // 
            this.characterSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.characterSelector.FormattingEnabled = true;
            this.characterSelector.Location = new System.Drawing.Point( 0, 0 );
            this.characterSelector.Name = "characterSelector";
            this.characterSelector.Size = new System.Drawing.Size( 130, 439 );
            this.characterSelector.TabIndex = 0;
            // 
            // characterEditor1
            // 
            this.characterEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.characterEditor1.Location = new System.Drawing.Point( 0, 0 );
            this.characterEditor1.Name = "characterEditor1";
            this.characterEditor1.Size = new System.Drawing.Size( 577, 444 );
            this.characterEditor1.TabIndex = 0;
            // 
            // braveStoryTab
            // 
            this.braveStoryTab.Location = new System.Drawing.Point( 4, 22 );
            this.braveStoryTab.Name = "braveStoryTab";
            this.braveStoryTab.Padding = new System.Windows.Forms.Padding( 3 );
            this.braveStoryTab.Size = new System.Drawing.Size( 717, 450 );
            this.braveStoryTab.TabIndex = 1;
            this.braveStoryTab.Text = "Brave Story";
            this.braveStoryTab.UseVisualStyleBackColor = true;
            // 
            // inventoryTab
            // 
            this.inventoryTab.Location = new System.Drawing.Point( 4, 22 );
            this.inventoryTab.Name = "inventoryTab";
            this.inventoryTab.Padding = new System.Windows.Forms.Padding( 3 );
            this.inventoryTab.Size = new System.Drawing.Size( 717, 450 );
            this.inventoryTab.TabIndex = 2;
            this.inventoryTab.Text = "Inventory";
            this.inventoryTab.UseVisualStyleBackColor = true;
            // 
            // SavegameEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( tabControl1 );
            this.Name = "SavegameEditor";
            this.Size = new System.Drawing.Size( 725, 476 );
            tabControl1.ResumeLayout( false );
            this.charactersTab.ResumeLayout( false );
            splitContainer3.Panel1.ResumeLayout( false );
            splitContainer3.Panel2.ResumeLayout( false );
            splitContainer3.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TabPage charactersTab;
        private System.Windows.Forms.CheckedListBox characterSelector;
        private CharacterEditor characterEditor1;
        private System.Windows.Forms.TabPage braveStoryTab;
        private System.Windows.Forms.TabPage inventoryTab;
    }
}
