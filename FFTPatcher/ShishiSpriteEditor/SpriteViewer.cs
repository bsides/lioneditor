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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FFTPatcher.SpriteEditor
{
    public class SpriteViewer : UserControl
    {
        private Sprite sprite = null;

        public Sprite Sprite
        {
            get { return sprite; }
            set
            {
                if( value != sprite )
                {
                    sprite = value;
                    Invalidate();
                }
            }
        }

        private int palette = 0;
        public int Palette
        {
            get { return palette; }
            set
            {
                if( value != palette )
                {
                    palette = value;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            base.OnPaint( e );
            if( sprite != null )
            {
                using( Bitmap bmp = new Bitmap( 256, 488 ) )
                {
                    for( long i = 0; i < sprite.Pixels.LongLength; i++ )
                    {
                        bmp.SetPixel( (int)(i % 256), (int)(i / 256), sprite.Palettes[palette].Colors[sprite.Pixels[i]] );
                    }

                    e.Graphics.DrawImage( bmp, 0, 0 );
                }
            }
        }
    }
}
