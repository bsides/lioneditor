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
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class GlyphEditor : UserControl
    {
        private static Dictionary<FontColor, Brush> colors = new Dictionary<FontColor, Brush>();
        private bool ignoreChanges = false;
        private FontColor currentColor;

        private RadioButton[] radios = new RadioButton[4];

        private Glyph glyph;
        public Glyph Glyph 
        {
            get { return glyph; }
            set
            {
                if( glyph != value )
                {
                    glyph = value;
                    ignoreChanges = true;
                    widthSpinner.Value = glyph.Width;
                    ignoreChanges = false;
                    Invalidate( true );
                    glyphPanel.Invalidate();
                    thumbnailPanel.Invalidate();
                }
            }
        }

        static GlyphEditor()
        {
            colors[FontColor.Black] = new SolidBrush( Color.FromArgb( 70, 63, 51 ) );
            colors[FontColor.Dark] = new SolidBrush( Color.FromArgb( 107, 104, 85 ) );
            colors[FontColor.Light] = new SolidBrush( Color.FromArgb( 120, 112, 96 ) );
            colors[FontColor.Transparent] = new SolidBrush( Color.Transparent );
        }

        public GlyphEditor()
        {
            InitializeComponent();

            glyphPanel.MouseClick += glyphPanel_MouseClick;
            glyphPanel.Paint += glyphPanel_Paint;
            thumbnailPanel.Paint += thumbnailPanel_Paint;
            widthSpinner.ValueChanged += widthSpinner_ValueChanged;
            smallerThumbnailPanel.Paint += smallerThumbnailPanel_Paint;

            currentColor = FontColor.Black;

            blackRadioButton.Tag = FontColor.Black;
            darkRadioButton.Tag = FontColor.Dark;
            lightRadioButton.Tag = FontColor.Light;
            transparentRadioButton.Tag = FontColor.Transparent;

            radios[0] = blackRadioButton;
            radios[1] = darkRadioButton;
            radios[2] = lightRadioButton;
            radios[3] = transparentRadioButton;

            foreach( RadioButton r in radios )
            {
                r.CheckedChanged += radioButton_CheckedChanged;
            }
        }

        private void smallerThumbnailPanel_Paint( object sender, PaintEventArgs e )
        {
            if( Glyph != null )
            {
                for( int i = 0; i < Glyph.Pixels.Length; i++ )
                {
                    int col = i % 10;
                    int row = i / 10;
                    e.Graphics.FillRectangle( colors[Glyph.Pixels[row * 10 + col]], new Rectangle( col, row, 1, 1 ) );
                }
            }
        }

        private void radioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            RadioButton r = sender as RadioButton;
            currentColor = (FontColor)r.Tag;
        }

        private void widthSpinner_ValueChanged( object sender, System.EventArgs e )
        {
            if( !ignoreChanges && glyph != null )
            {
                glyph.Width = (byte)widthSpinner.Value;
                glyphPanel.Invalidate();
            }
        }

        private void thumbnailPanel_Paint( object sender, PaintEventArgs e )
        {
            if( Glyph != null )
            {
                if( e.ClipRectangle == e.Graphics.VisibleClipBounds )
                {
                    for( int i = 0; i < Glyph.Pixels.Length; i++ )
                    {
                        int col = i % 10;
                        int row = i / 10;
                        e.Graphics.FillRectangle( colors[Glyph.Pixels[row * 10 + col]], new Rectangle( col * 2, row * 2, 2, 2 ) );
                    }
                }
                else
                {
                    int col = e.ClipRectangle.Location.X / 2;
                    int row = e.ClipRectangle.Location.Y / 2;
                    e.Graphics.FillRectangle( colors[Glyph.Pixels[row * 10 + col]], new Rectangle( col * 2, row * 2, 2, 2 ) );
                }
            }
        }

        private void glyphPanel_Paint( object sender, PaintEventArgs e )
        {
            if( Glyph != null )
            {
                if( e.ClipRectangle == e.Graphics.VisibleClipBounds )
                {
                    for( int i = 0; i < Glyph.Pixels.Length; i++ )
                    {
                        int col = i % 10;
                        int row = i / 10;
                        RedrawPixel( col, row, Glyph.Pixels[i], e.Graphics );
                    }
                }
                else
                {
                    int col = e.ClipRectangle.Location.X / 15;
                    int row = e.ClipRectangle.Location.Y / 15;
                    RedrawPixel( col, row, Glyph.Pixels[row * 10 + col], e.Graphics );
                }

                using( Pen p = new Pen( Color.White ) )
                {
                    e.Graphics.DrawLine( p, 
                        new Point( (int)widthSpinner.Value * 15, 0 ), 
                        new Point( (int)widthSpinner.Value * 15, (int)e.Graphics.VisibleClipBounds.Bottom ) );
                }
            }
        }

        private void glyphPanel_MouseClick( object sender, MouseEventArgs e )
        {
            if( Glyph != null )
            {
                int column = e.X / 15;
                int row = e.Y / 15;
                FontColor newValue = currentColor;
                Glyph.Pixels[row * 10 + column] = newValue;
                glyphPanel.Invalidate( new Rectangle( column * 15, row * 15, 15, 15 ) );
                thumbnailPanel.Invalidate( new Rectangle( column * 2, row * 2, 2, 2 ) );
                smallerThumbnailPanel.Invalidate();
            }
        }

        private void RedrawPixel( int col, int row, FontColor color, Graphics g )
        {
            g.FillRectangle( colors[color], new Rectangle( col * 15, row * 15, 15, 15 ) );
        }
    }
}
