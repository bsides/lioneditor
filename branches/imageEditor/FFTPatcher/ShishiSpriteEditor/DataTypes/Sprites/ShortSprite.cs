using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FFTPatcher.SpriteEditor
{
    public class ShortSprite : AbstractSprite
    {
        /*
         * 10M
         * 10W
         * 20M
         * 20W
         * 40M
         * 40W
         * 60M
         * 60W
         * CYOMON1
         * CYOMON2
         * CYOMON3
         * CYOMON4
         * FURAIA
         */

        public override int Height
        {
            get { return 288; }
        }

        public ShortSprite( string name, IList<byte> bytes )
            : base( name, bytes )
        {
        }

        protected override Rectangle PortraitRectangle
        {
            get { return new Rectangle( 80, 256, 48, 42 ); }
        }

        protected override Rectangle ThumbnailRectangle
        {
            get { return new Rectangle( 39, 5, 26, 38 ); }
        }

        protected override void ToBitmapInner( System.Drawing.Bitmap bmp, System.Drawing.Imaging.BitmapData bmd )
        {
            throw new NotImplementedException();
        }

        protected override IList<byte> BuildPixels( IList<byte> bytes, IList<byte>[] extraBytes )
        {
            byte[] result = new byte[36864 * 2];
            for( int i = 0; i < 36864; i++ )
            {
                result[i * 2] = bytes[i].GetLowerNibble();
                result[i * 2 + 1] = bytes[i].GetUpperNibble();
            }

            return result;
        }

        public override IList<byte[]> ToByteArrays()
        {
            List<byte> ourResult = new List<byte>( 37377 );
            foreach ( Palette p in Palettes )
            {
                ourResult.AddRange( p.ToByteArray() );
            }

            for( int i = 0; i < Pixels.Count / 2; i++ )
            {
                ourResult.Add( (byte)( ( Pixels[2 * i + 1] << 4 ) | ( Pixels[2 * i] & 0x0F ) ) );
            }
            return new byte[][] { ourResult.ToArray() };
        }
    }
}
