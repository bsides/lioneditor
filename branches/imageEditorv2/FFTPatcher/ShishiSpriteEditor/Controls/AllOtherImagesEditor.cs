using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FFTPatcher.SpriteEditor
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

            List<object> imageList = new List<object>();
            for ( int i = 0; i < images.ListCount; i++ )
            {
                var l = images[i];
                for ( int j = 0; j < l.Count; j++ )
                {
                    if ( j == l.Count - 1 && i != images.ListCount - 1 )
                    {
                        imageList.Add( new SeparatorComboBox.SeparatorItem( l[j] ) );
                    }
                    else
                    {
                        imageList.Add( l[j] );
                    }
                }
            }

            comboBox1.DataSource = imageList;
            comboBox1.EndUpdate();
            this.iso = iso;
            Enabled = true;
            ignoreChanges = false;

            comboBox1.SelectedIndex = 0;
            RefreshPictureBox();
        }

        public string GetCurrentImageFileFilter()
        {
            AbstractImage im = comboBox1.SelectedItem as AbstractImage;
            return im.FilenameFilter;
        }

        public void SaveCurrentImage( string path )
        {
            AbstractImage im = comboBox1.SelectedItem as AbstractImage;
            using ( System.IO.Stream s = System.IO.File.Open( path, System.IO.FileMode.Create ) )
            {
                im.SaveImage( iso, s );
            }
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
            panel1.SuspendLayout();
            panel1.BackColor = Color.Black;
            if ( pictureBox1.Width < panel1.ClientSize.Width && pictureBox1.Height < panel1.ClientSize.Height )
            {
                pictureBox1.BorderStyle = BorderStyle.Fixed3D;
                pictureBox1.Location = new Point( 
                    ( panel1.ClientRectangle.Width - pictureBox1.Width ) / 2 + panel1.ClientRectangle.X, 
                    ( panel1.ClientRectangle.Height - pictureBox1.Height ) / 2 + panel1.ClientRectangle.Y );
            }
            else
            {
                pictureBox1.BorderStyle = BorderStyle.None;
                pictureBox1.Location = panel1.ClientRectangle.Location;
            }
            panel1.ResumeLayout();
        }

        private void RefreshPictureBox()
        {
            object item = comboBox1.SelectedItem;
            if ( item is SeparatorComboBox.SeparatorItem )
            {
                item = ( item as SeparatorComboBox.SeparatorItem ).Data;
            }

            AbstractImage im = item as AbstractImage;
            AssignNewPictureBoxImage( im.GetImageFromIso( iso ) );
            imageSizeLabel.Text = string.Format( "Image dimensions: {0}x{1}", im.Width, im.Height );
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
