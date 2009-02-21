/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace FFTPatcher.SpriteEditor
{
    public class SpriteViewer : UserControl
    {

		#region Fields (5) 

        private int palette = 0;
        private int portraitPalette = 8;
        private AbstractSprite sprite = null;
        private PictureBox pictureBox1;
        private IList<Tile> tiles;

		#endregion Fields 

		#region Properties (2) 


        public AbstractSprite Sprite
        {
            get { return sprite; }
            set
            {
                if( value != sprite )
                {
                    this.tiles = new Tile[0];
                    sprite = value;
                    UpdateImage();
                }
            }
        }


		#endregion Properties 

		#region Constructors (1) 

        public SpriteViewer()
        {
            pictureBox1 = new PictureBox();
            ( pictureBox1 as System.ComponentModel.ISupportInitialize ).BeginInit();
            SuspendLayout();
            pictureBox1.Location = new Point( 0, 0 );
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size( 256, 50 );
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabStop = false;
            AutoScroll = true;
            Controls.Add( pictureBox1 );
            Name = "SpriteViewer";
            Size = new Size( 256 + 10 + SystemInformation.VerticalScrollBarWidth, 50 );
            ( pictureBox1 as System.ComponentModel.ISupportInitialize ).EndInit();
            ResumeLayout( false );
            PerformLayout();
        }

		#endregion Constructors 

		#region Methods (4) 


        public void HighlightTiles( IList<Tile> tiles )
        {
            this.tiles = tiles;
            UpdateImage();
        }

        public void SetPalette( int paletteId )
        {
            SetPalette( paletteId, paletteId );
        }

        public void SetPalette( int paletteId, int portraitPaletteId )
        {
            if( palette != paletteId || portraitPalette != portraitPaletteId )
            {
                palette = paletteId;
                portraitPalette = portraitPaletteId;
                UpdateImage();
            }
        }

        private void UpdateImage()
        {
            if ( sprite != null )
            {
                Bitmap b = new Bitmap( sprite.Width, sprite.Height, PixelFormat.Format32bppArgb );
                BitmapData bmd = b.LockBits( new Rectangle( Point.Empty, b.Size ), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb );
                sprite.DrawSprite( bmd, palette, portraitPalette );
                b.UnlockBits( bmd );
                if ( tiles != null )
                {
                    using ( Pen p = new Pen( Color.Yellow ) )
                    using ( Graphics g = Graphics.FromImage( b ) )
                    {
                        foreach ( Tile t in tiles )
                            g.DrawRectangle( p, t.Rectangle );
                    }
                }

                SuspendLayout();
                pictureBox1.SuspendLayout();
                if ( pictureBox1.Image != null )
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                pictureBox1.Image = b;
                pictureBox1.ResumeLayout();
                ResumeLayout( false );
                PerformLayout();
            }           
        }



		#endregion Methods 

    }
}
