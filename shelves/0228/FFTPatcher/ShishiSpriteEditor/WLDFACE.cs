using System.Collections.Generic;
using System.Drawing;
using FFTPatcher.Datatypes;

namespace FFTPatcher.SpriteEditor
{
    public class WLDFACE
    {
        public Palette[] Palettes { get; private set; }
        public byte[] Pixels { get; private set; }
        public WLDFACE( IList<byte> bytes )
        {
            Pixels = BuildPixels( new SubArray<byte>( bytes, 0x0000, 0x77FF ) );

            Palettes = new Palette[64];
            for( int i = 0; i < 64; i++ )
            {
                Palettes[i] = new Palette( new SubArray<byte>( bytes, 0x7800 + 32 * i, 0x7800 + 32 * (i + 1) - 1 ) );
            }
        }

        private static byte[] BuildPixels( IList<byte> bytes )
        {
            List<byte> result = new List<byte>( 32 * 48 * 5 * 8 );
            foreach( byte b in bytes )
            {
                result.Add( b.GetLowerNibble() );
                result.Add( b.GetUpperNibble() );
            }

            return result.ToArray();
        }

        public Bitmap ToBitmap( int palette )
        {
            Bitmap bmp = new Bitmap( 256, 240 );
            for( int i = 0; i < Pixels.Length; i++ )
            {
                bmp.SetPixel( i % 256, i / 256, Palettes[palette].Colors[Pixels[i]] );
            }

            return bmp;
        }

        public Bitmap ToBitmap()
        {
            return ToBitmap( 0 );
        }

    }
}
