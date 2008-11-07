using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FFTPatcher.SpriteEditor.Files.EVENT;

namespace FFTPatcher.SpriteEditor
{
    public partial class ImageListView : UserControl
    {
        public ImageListView()
        {
            InitializeComponent();
            treeView1.ExpandAll();
            treeView1.AfterSelect += new TreeViewEventHandler( treeView1_AfterSelect );
            treeView1.BeforeSelect += new TreeViewCancelEventHandler( treeView1_BeforeSelect );
        }

        void treeView1_BeforeSelect( object sender, TreeViewCancelEventArgs e )
        {
        }

        void treeView1_AfterSelect( object sender, TreeViewEventArgs e )
        {
            switch ( e.Node.Name )
            {
                case "evtFaceBinNode":
                    pictureBox1.Image = new EVTFACEbin().ToImage();
                    break;
            }
        }
    }
}
