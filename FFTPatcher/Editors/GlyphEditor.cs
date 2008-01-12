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

        private Glyph glyph;
        public Glyph Glyph 
        {
            get { return glyph; }
            set
            {
                if( glyph != value )
                {
                    glyph = value;
                    Invalidate();
                }
            }
        }

        static GlyphEditor()
        {
            colors[FontColor.Black] = new SolidBrush( Color.Black );
            colors[FontColor.Dark] = new SolidBrush( Color.DarkGray );
            colors[FontColor.Light] = new SolidBrush( Color.LightGray );
            colors[FontColor.Transparent] = new SolidBrush( Color.White );
        }

        public GlyphEditor()
        {
            SetStyle( ControlStyles.AllPaintingInWmPaint, true );
            SetStyle( ControlStyles.UserPaint, true );
            SetStyle( ControlStyles.OptimizedDoubleBuffer, true );

            InitializeComponent();
        }

        private void RedrawPixel( int col, int row, FontColor color, Graphics g )
        {
            g.FillRectangle( colors[color], new Rectangle( col * 15, row * 15, 15, 15 ) );
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            if( Glyph != null )
            {
                if( e.ClipRectangle == e.Graphics.ClipBounds )
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
            }

            base.OnPaint( e );
        }

        protected override void OnMouseClick( MouseEventArgs e )
        {
            int column = e.X / 15;
            int row = e.Y / 15;
            FontColor oldValue = Glyph.Pixels[row * 10 + column];
            FontColor newValue = (FontColor)(((int)(oldValue + 1)) % 4);
            Glyph.Pixels[row * 10 + column] = newValue;
            Invalidate( new Rectangle( column * 15, row * 15, 15, 15 ) );

            base.OnMouseClick( e );
        }
    }
}
