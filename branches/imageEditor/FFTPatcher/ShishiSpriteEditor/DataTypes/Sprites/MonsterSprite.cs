using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    public class MonsterSprite : AbstractCompressedSprite
    {
        public override int Height
        {
            get
            {
                return 488 + sp2Count * 256;
            }
        }

        protected override System.Drawing.Rectangle ThumbnailRectangle
        {
            get { return new System.Drawing.Rectangle( 0, 8, 32, 48 ); }
        }

        private int sp2Count;

        public MonsterSprite( string name, IList<byte> bytes, params IList<byte>[] sp2Files )
            : base( name, bytes, sp2Files )
        {
            sp2Count = sp2Files.Length;
        }

        protected override IList<byte> BuildPixels( IList<byte> bytes, IList<byte>[] extraBytes )
        {
            List<byte> result = new List<byte>( 36864 * 2 );
            foreach ( byte b in bytes.Sub( 0, 36863 ) )
            {
                result.Add( b.GetLowerNibble() );
                result.Add( b.GetUpperNibble() );
            }

            result.AddRange( Decompress( bytes.Sub( 36864 ) ) );

            foreach( IList<byte> extra in extraBytes )
            {
                foreach( byte b in extra )
                {
                    result.Add( b.GetLowerNibble() );
                    result.Add( b.GetUpperNibble() );
                }
            }

            return result.ToArray();
        }

        public override IList<byte[]> ToByteArrays()
        {
            List<byte[]> result = new List<byte[]>( base.ToByteArrays() );
            for( int i = 0; i < sp2Count; i++ )
            {
                IList<byte> sp2 = Pixels.Sub( 256 * 488 + i * 256 * 256, 256 * 488 + ( i + 1 ) * 256 * 256 - 1 );
                byte[] sp2Array = new byte[32768];
                for( int j = 0; j < sp2.Count; j += 2 )
                {
                    sp2Array[j / 2] = (byte)((sp2[j + 1] << 4) | (sp2[j] & 0x0F));
                }
                result.Add( sp2Array );
            }

            return result;
        }

        protected override void ToBitmapInner( System.Drawing.Bitmap bmp, System.Drawing.Imaging.BitmapData bmd )
        {
            base.ToBitmapInner( bmp, bmd );
            for( int i = 256 * 488; (i < Pixels.Count) && (i / Width < Height); i++ )
            {
                bmd.SetPixel( i % Width, i / Width, Pixels[i] );
            }
        }
    }
}
