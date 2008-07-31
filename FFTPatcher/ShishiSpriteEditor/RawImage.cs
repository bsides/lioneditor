using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace FFTPatcher.SpriteEditor
{
    public abstract class RawImage : GraphicsFile
    {
        private Color[,] pixels;

        public override Color this[int x, int y]
        {
            get { return pixels[x, y]; }
            set { pixels[x, y] = value; }
        }

        protected RawImage( Size size, IList<byte> bytes )
        {
            if( size.Width * size.Height * 2 != bytes.Count )
            {
                throw new ArgumentOutOfRangeException( "size", "size does not correspond to byte array length" );
            }

            int w = size.Width;
            int h = size.Height;
            pixels = new Color[w, h];

            Size = size;
            for( int i = 0; i < bytes.Count; i++ )
            {
                pixels[(i / 2) % w, (i / 2) / w] = Palette.BytesToColor( bytes[i * 2], bytes[i * 2 + 1] );
            }
        }

        public override void Draw( Graphics graphics, int paletteIndex )
        {
            using( Image i = Export() )
            {
                graphics.DrawImage( i, new Point( 0, 0 ) );
            }
        }

        public override Image Export()
        {
            Bitmap result = new Bitmap( Width, Height );
            int w = Width;
            int h = Height;
            for( int x = 0; x < w; x++ )
            {
                for( int y = 0; y < h; y++ )
                {
                    result.SetPixel( x, y, pixels[x, y] );
                }
            }
            return result;
        }
    }
}
