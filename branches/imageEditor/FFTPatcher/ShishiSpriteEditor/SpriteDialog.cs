using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FFTPatcher.SpriteEditor
{
    public partial class SpriteDialog : Form
    {
        public SpriteDialog()
        {
            InitializeComponent();
        }

        public void ShowDialog( Image image )
        {
            pictureBox1.Image = image;
            ShowDialog();
        }
    }
}
