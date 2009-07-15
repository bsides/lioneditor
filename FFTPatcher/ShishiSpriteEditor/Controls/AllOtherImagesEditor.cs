using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FFTPatcher.SpriteEditor.Controls
{
    public partial class AllOtherImagesEditor : UserControl
    {
        public AllOtherImagesEditor()
        {
            InitializeComponent();
        }

        private bool ignoreChanges = false;
        private System.IO.Stream iso = null;

        public void BindTo( AllOtherImages images, System.IO.Stream iso )
        {
            ignoreChanges = true;
            comboBox1.SelectedIndex = -1;
            comboBox1.BeginUpdate();
            comboBox1.DataSource = new List<AbstractImage>( images );
            comboBox1.DisplayMember = "Name";
            comboBox1.EndUpdate();
            this.iso = iso;
            Enabled = true;
            ignoreChanges = false;

            comboBox1.SelectedIndex = 0;
            RefreshPictureBox();
        }

        public void SaveCurrentImage( string path )
        {
            AbstractImage im = comboBox1.SelectedItem as AbstractImage;
            im.GetImageFromIso( iso ).Save( path, System.Drawing.Imaging.ImageFormat.Png );
        }

        public void LoadToCurrentImage( string path )
        {
            using (Image im = Image.FromFile( path ))
            {
                AbstractImage abIm = comboBox1.SelectedItem as AbstractImage;
                abIm.WriteImageToIso( iso, im );
            }
            RefreshPictureBox();
        }

        private void DestroyPictureBoxImage()
        {
            //if ( pictureBox1.Image != null )
            //{
            //    var im = pictureBox1.Image;
            //    pictureBox1.Image = null;
            //    im.Dispose();
            //}
        }

        private void AssignNewPictureBoxImage(Image newImage)
        {
            DestroyPictureBoxImage();
            pictureBox1.Image = newImage;
        }

        private void RefreshPictureBox()
        {
            AbstractImage im = comboBox1.SelectedItem as AbstractImage;
            AssignNewPictureBoxImage( im.GetImageFromIso( iso ) );
        }

        private void comboBox1_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( !ignoreChanges )
            {
                RefreshPictureBox();
            }
        }
    }
}
