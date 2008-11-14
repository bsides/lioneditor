using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace FFTPatcher.SpriteEditor
{
    public abstract class AbstractCompressedSprite : AbstractSprite
    {

        public override System.Drawing.Image GetThumbnail()
        {
            Bitmap result = new Bitmap( 80, 48, PixelFormat.Format32bppRgb );

            using( Bitmap portrait = new Bitmap( 48, 32, PixelFormat.Format8bppIndexed ) )
            using( Bitmap wholeImage = ToBitmap() )
            {
                wholeImage.CopyRectangleToPointNonIndexed(
                    ThumbnailRectangle,
                    result,
                    new Point( ( 48 - ThumbnailRectangle.Width ) / 2, ( 48 - ThumbnailRectangle.Height ) / 2 ),
                    Palettes[0],
                    false );

                ColorPalette palette2 = portrait.Palette;
                FixupColorPalette( palette2 );
                portrait.Palette = palette2;
                wholeImage.CopyRectangleToPoint( PortraitRectangle, portrait, Point.Empty, Palettes[8], false );
                portrait.RotateFlip( RotateFlipType.Rotate270FlipNone );

                portrait.CopyRectangleToPointNonIndexed( new Rectangle( 0, 0, 32, 48 ), result, new Point( 48, 0 ), Palettes[8], false );
            }

            return result;
        }

        public override int Height
        {
            get { return 488; }
        }

        protected AbstractCompressedSprite( string name, IList<byte> bytes, params IList<byte>[] extraBytes )
            : base( name, bytes, extraBytes )
        {
        }

        protected override Rectangle PortraitRectangle
        {
            get { return new Rectangle( 80, 456, 48, 32 ); }
        }

        protected override IList<byte> BuildPixels( IList<byte> bytes, IList<byte>[] extraBytes )
        {
            List<byte> result = new List<byte>( 36864 * 2 );
            foreach( byte b in bytes.Sub( 0, 36864 - 1 ) )
            {
                result.Add( b.GetLowerNibble() );
                result.Add( b.GetUpperNibble() );
            }

            result.AddRange( Decompress( bytes.Sub( 36864 ) ) );

            return result.ToArray();
        }

        private static byte[] Recompress( IList<byte> bytes )
        {
            List<byte> realBytes = new List<byte>( bytes.Count );
            for( int i = 0; (i + 1) < bytes.Count; i += 2 )
            {
                realBytes.Add( bytes[i + 1] );
                realBytes.Add( bytes[i] );
            }

            List<byte> result = new List<byte>();
            int pos = 0;
            while( pos < realBytes.Count )
            {
                int z = NumberOfZeroes( realBytes.Sub( pos ) );
                z = Math.Min( z, 0xFFF );

                if( z == 0 )
                {
                    byte b = realBytes[pos];
                    result.Add( realBytes[pos] );
                    pos += 1;
                }
                else if( z < 16 )
                {
                    if( (z == 8) ||
                        (z == 7) )
                    {
                        result.Add( 0x00 );
                        result.Add( 0x00 );
                        result.Add( (byte)z );
                    }
                    else
                    {
                        result.Add( 0x00 );
                        result.Add( (byte)z );
                    }
                }
                else if( z < 256 )
                {
                    result.Add( 0x00 );
                    result.Add( 0x07 );
                    result.Add( ((byte)z).GetLowerNibble() );
                    result.Add( ((byte)z).GetUpperNibble() );
                }
                else if( z < 4096 )
                {
                    result.Add( 0x00 );
                    result.Add( 0x08 );
                    result.Add( ((byte)z).GetLowerNibble() );
                    result.Add( ((byte)z).GetUpperNibble() );
                    result.Add( (byte)((z & 0xF00) >> 8) );
                }

                pos += z;
            }

            return CompressNibbles( result );
        }

        private static byte[] CompressNibbles( IList<byte> bytes )
        {
            List<byte> result = new List<byte>( bytes.Count / 2 );
            for( int i = 0; i < bytes.Count; i += 2 )
            {
                if( (i + 1) < bytes.Count )
                {
                    result.Add( (byte)(((bytes[i] & 0x0F) << 4) + (bytes[i + 1] & 0x0F)) );
                }
                else
                {
                    result.Add( (byte)((bytes[i] & 0x0F) << 4) );
                }
            }
            return result.ToArray();
        }
        private static int NumberOfZeroes( IList<byte> bytes )
        {
            for( int i = 0; i < bytes.Count; i++ )
            {
                if( bytes[i] != 0 )
                    return i;
            }

            return bytes.Count;
        }

        public override IList<byte[]> ToByteArrays()
        {
            List<byte[]> result = new List<byte[]>();
            List<byte> ourResult = new List<byte>( 36864 );
            foreach( Palette p in Palettes )
            {
                ourResult.AddRange( p.ToByteArray() );
            }
            for( int i = 0; i < 36864; i++ )
            {
                ourResult.Add( (byte)((Pixels[2 * i + 1] << 4) | (Pixels[2 * i] & 0x0F)) );
            }

            ourResult.AddRange( Recompress( Pixels.Sub( 2 * 36864, 2 * 36864 + 200 * 256 - 1 ) ) );

            if( ourResult.Count < OriginalSize )
            {
                ourResult.AddRange( new byte[OriginalSize - ourResult.Count] );
            }

            result.Add( ourResult.ToArray() );

            return result;
        }

        public const int topHeight = 256;
        public const int portraintHeight = 32;
        public const int compressedHeight = 200;

        protected override void ToBitmapInner( System.Drawing.Bitmap bmp, System.Drawing.Imaging.BitmapData bmd )
        {
            // Top portrait
            for ( int i = 0; ( i < this.Pixels.Count ) && ( i / Width < topHeight ); i++ )
            {
                bmd.SetPixel( i % Width, i / Width, Pixels[i] );
            }

            // Compressed part
            for ( int i = ( topHeight + portraintHeight ) * Width; ( i < this.Pixels.Count ) && ( i / Width < Height ); i++ )
            {
                bmd.SetPixel( i % Width, i / Width - portraintHeight, Pixels[i] );
            }

            // Portrait part
            for ( int i = topHeight * Width; ( i < this.Pixels.Count ) && ( i / Width < ( topHeight + portraintHeight ) ); i++ )
            {
                bmd.SetPixel( i % Width, i / Width + compressedHeight, Pixels[i] );
            }
        }

        protected static IList<byte> Decompress( IList<byte> bytes )
        {
            byte[] compressed = new byte[bytes.Count * 2];
            for( int i = 0; i < bytes.Count; i++ )
            {
                compressed[i * 2] = bytes[i].GetUpperNibble();
                compressed[i * 2 + 1] = bytes[i].GetLowerNibble();
            }

            List<byte> result = new List<byte>();
            int j = 0;
            while( j < compressed.Length )
            {
                byte b = compressed[j];
                if( compressed[j] != 0 )
                {
                    result.Add( compressed[j] );
                }
                else if( (j + 1) < compressed.Length )
                {
                    byte s = compressed[j + 1];
                    int l = s;
                    if( (s == 7) && ((j + 3) < compressed.Length) )
                    {
                        l = compressed[j + 2] + (compressed[j + 3] << 4);
                        j += 2;
                    }
                    else if( (s == 8) && ((j + 4) < compressed.Length) )
                    {
                        l = compressed[j + 2] + (compressed[j + 3] << 4) + (compressed[j + 4] << 8);
                        j += 3;
                    }
                    else if( (s == 0) && ((j + 2) < compressed.Length) )
                    {
                        l = compressed[j + 2];
                        j++;
                    }
                    else
                    {
                        l = s;
                    }

                    j++;
                    for( int k = 0; k < l; k++ )
                        result.Add( 0x00 );
                }

                j++;
            }

            j = 0;
            while( (j + 1) < result.Count )
            {
                byte k = result[j];
                result[j] = result[j + 1];
                result[j + 1] = k;
                j += 2;
            }

            return result;
        }
    }
}
