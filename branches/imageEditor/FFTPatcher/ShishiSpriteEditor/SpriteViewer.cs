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

namespace FFTPatcher.SpriteEditor
{
    public class SpriteViewer : UserControl
    {

		#region Fields (5) 

        private int palette = 0;
        private int portraitPalette = 8;
        private bool proper = true;
        private AbstractSprite sprite = null;
        private IList<Tile> tiles;

		#endregion Fields 

		#region Properties (2) 


        public bool Proper
        {
            get { return proper; }
            set
            {
                if( proper ^ value )
                {
                    proper = value;
                    Invalidate();
                }
            }
        }

        public AbstractSprite Sprite
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


		#endregion Properties 

		#region Constructors (1) 

        public SpriteViewer()
        {
            SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true );
        }

		#endregion Constructors 

		#region Methods (4) 


        public void HighlightTiles( IList<Tile> tiles )
        {
            this.tiles = tiles;
            Invalidate();
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
                Invalidate();
            }
        }



        protected override void OnPaint( PaintEventArgs e )
        {
            base.OnPaint( e );
            if( sprite != null )
            {
                e.Graphics.DrawSprite( sprite, sprite.Palettes[palette], sprite.Palettes[portraitPalette], proper );
                if( tiles != null )
                    using( Pen p = new Pen( Color.Yellow ) )
                        foreach( Tile t in tiles )
                            e.Graphics.DrawRectangle( p, t.Rectangle );
            }
        }


		#endregion Methods 

    }
}
