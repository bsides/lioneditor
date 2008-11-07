namespace FFTPatcher.SpriteEditor
{
    partial class ImageListView
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
            System.Windows.Forms.TreeNode battleNode = new System.Windows.Forms.TreeNode( "BATTLE" );
            System.Windows.Forms.TreeNode evtFaceBinNode = new System.Windows.Forms.TreeNode( "EVTFACE.BIN" );
            System.Windows.Forms.TreeNode eventNode = new System.Windows.Forms.TreeNode( "EVENT", new System.Windows.Forms.TreeNode[] {
            evtFaceBinNode} );
            System.Windows.Forms.TreeNode menuNode = new System.Windows.Forms.TreeNode( "MENU" );
            System.Windows.Forms.TreeNode openNode = new System.Windows.Forms.TreeNode( "OPEN" );
            System.Windows.Forms.TreeNode worldNode = new System.Windows.Forms.TreeNode( "WORLD" );
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.treeView1.FullRowSelect = true;
            this.treeView1.Location = new System.Drawing.Point( 0, 0 );
            this.treeView1.Name = "treeView1";
            battleNode.Name = "battleNode";
            battleNode.Text = "BATTLE";
            evtFaceBinNode.Name = "evtFaceBinNode";
            evtFaceBinNode.Text = "EVTFACE.BIN";
            eventNode.Name = "eventNode";
            eventNode.Text = "EVENT";
            menuNode.Name = "menuNode";
            menuNode.Text = "MENU";
            openNode.Name = "openNode";
            openNode.Text = "OPEN";
            worldNode.Name = "worldNode";
            worldNode.Text = "WORLD";
            this.treeView1.Nodes.AddRange( new System.Windows.Forms.TreeNode[] {
            battleNode,
            eventNode,
            menuNode,
            openNode,
            worldNode} );
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.Size = new System.Drawing.Size( 202, 272 );
            this.treeView1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.pictureBox1.Location = new System.Drawing.Point( 208, 3 );
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size( 412, 266 );
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // ImageListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.pictureBox1 );
            this.Controls.Add( this.treeView1 );
            this.Name = "ImageListView";
            this.Size = new System.Drawing.Size( 623, 272 );
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
