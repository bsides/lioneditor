using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FFTPatcher.SpriteEditor
{
    public abstract class PalettedImage : GraphicsFile
    {
        public int CurrentPalette { get; private set; }

        public IList<Palette> Palettes { get; set; }

        public int[,] Pixels { get; protected set; }

        public override Color this[int x, int y]
        {
            get
            {
                return Palettes[CurrentPalette].Colors[Pixels[x, y]];
            }
            set
            {
                Palette p = Palettes[CurrentPalette];
                int? found = null;
                for( int i = 0; i < p.Colors.Length; i++ )
                {
                    if( p.Colors[i].ToArgb() == value.ToArgb() )
                    {
                        found = i;
                        break;
                    }
                }

                if( found.HasValue )
                {
                    Pixels[x, y] = found.Value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException( "value", "Color not in current palette" );
                }
            }
        }
    }
}
