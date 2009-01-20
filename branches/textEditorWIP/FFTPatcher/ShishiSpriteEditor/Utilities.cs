using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace FFTPatcher.SpriteEditor
{
    class Utilities
    {
        public static IList<IList<Color>> ReadBytesAsRawImage( IList<byte> bytes, int width, int height )
        {
            Color[][] result = new Color[width][];

            for ( int i = 0; i < result.Length; i++ )
                result[i] = new Color[height];

            for ( int y = 0; y < height; y++ )
            {
                for ( int x = 0; x < width; x++ )
                {
                    byte one = bytes[( y * width + x ) * 2];
                    byte two = bytes[( y * width + x ) * 2 + 1];
                    result[x][y] = Palette.BytesToColor( one, two );
                }
            }

            return result;
        }
    }


}
