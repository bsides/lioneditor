using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace FFTPatcher.SpriteEditor
{
    public class WLDFACE
    {

		#region Properties (2) 


        public Palette[] Palettes { get; private set; }

        public byte[] Pixels { get; private set; }

        public Image[] Images { get; private set; }

		#endregion Properties 

		#region Constructors (1) 

        public WLDFACE( IList<byte> bytes )
        {
            Images = new Image[160];
            Palettes = new Palette[64*4];

            //byte[][] pixels = new byte[160][];
            //for ( int i = 0; i < 160; i++ )
            //{
            //    pixels[i] = new byte[32 * 48];
            //}

            //for ( int i = 0; i < bytes.Count; i++ )
            //{
            //    byte left = bytes[i].GetLowerNibble();
            //    byte right = bytes[i].GetUpperNibble();
            //}

            List<byte> truePixels = new List<byte>( portraitHeight * portraitWidth * portraitsPerPage * 4 );
            truePixels.AddRange( GetTruePixels( bytes.Sub( 0, portraitsPerPage * portraitWidth * portraitHeight / 2 - 1 ) ) );
            truePixels.AddRange( GetTruePixels( bytes.Sub( 32768, 32768 + portraitsPerPage * portraitWidth * portraitHeight / 2 - 1 ) ) );
            truePixels.AddRange( GetTruePixels( bytes.Sub( 65536, 65536 + portraitsPerPage * portraitWidth * portraitHeight / 2 - 1 ) ) );
            truePixels.AddRange( GetTruePixels( bytes.Sub( 98304, 98304 + portraitsPerPage * portraitWidth * portraitHeight / 2 - 1 ) ) );
            for ( int page = 0; page < 4; page++ )
            {
                int start = page * 32768 + portraitHeight * portraitWidth * portraitsPerPage / 2;
                for ( int pal = 0; pal < 64; pal++ )
                {
                    Palettes[pal + 64 * page] = new Palette( bytes.Sub( start + pal * 32, start + ( pal + 1 ) * 32 - 1 ) );
                }
            }

            Bitmap b = new Bitmap( portraitWidth * colsPerPage, portraitHeight * rowsPerPage * 4, PixelFormat.Format4bppIndexed );
            BitmapData bmd = b.LockBits( new Rectangle( Point.Empty, b.Size ), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed );
            for ( int i = 0; i < truePixels.Count; i++ )
            {
                bmd.SetPixel4bpp( i % ( colsPerPage * portraitWidth ), i / colsPerPage / portraitWidth, truePixels[i] );
            }
            b.UnlockBits( bmd );

            for ( int i = 0; i < Palettes.Length; i++ )
            {
                ColorPalette pal = b.Palette;
                Palette.FixupColorPalette( pal, Palettes, i, 0 );
                b.Palette = pal;
                b.Save( string.Format( "{0}.png", i ), ImageFormat.Png );
            }


            for ( int i = 0; i < 4; i++ )
            {
                ProcessPage( i, bytes.Sub( pageSize * i, pageSize * ( i + 1 ) - 1 ) );

                //for ( int row = 0; row < 5; row++ )
                //{
                //    for ( int col = 0; col < 8; col++ )
                //    {
                //        Palettes[i * row * col] = new Palette( bytes.Sub( ( i + 1 ) * 32 * 48 / 2 * 5 * 8 + ( row * 8 * col * 32 ), ( i + 1 ) * 32 * 48 / 2 * 5 * 8 + ( row * 8 * col * 32 ) + 32 - 1 ) );
                //        using ( Bitmap b = new Bitmap( 32, 48, System.Drawing.Imaging.PixelFormat.Format4bppIndexed ) )
                //        {
                //            BitmapData bmd = b.LockBits( new Rectangle( Point.Empty, b.Size ), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed );
                //            for ( int x = 0; x < 32; x++ )
                //            {
                //                for ( int y = 0; y < 48; y++ )
                //                {
                //                }
                //            }
                //            b.UnlockBits( bmd );
                //        }
                //    }
                //}
            }

            Pixels = BuildPixels( bytes.Sub( 0x0000, 0x77FF ) );

            Palettes = new Palette[64];
            for( int i = 0; i < 64; i++ )
            {
                Palettes[i] = new Palette( bytes.Sub( 0x7800 + 32 * i, 0x7800 + 32 * (i + 1) - 1 ) );
            }
        }

        const int portraitWidth = 32;
        const int portraitHeight = 48;
        const int rowsPerPage = 5;
        const int colsPerPage = 8;
        const int portraitsPerPage = rowsPerPage * colsPerPage;
        const int paletteSize = 16 * 2;
        const int pageSize = portraitWidth * portraitHeight * portraitsPerPage  / 2 + portraitsPerPage * paletteSize;

        private void ProcessPage( int page, IList<byte> bytes )
        {
            int startIndex = page * portraitsPerPage;
            IList<byte> truePixels = GetTruePixels( bytes.Sub( 0, portraitsPerPage * portraitWidth * portraitHeight / 2 ) );

            for ( int i = 0; i < portraitsPerPage; i++ )
            {
                Palettes[page * portraitsPerPage + i] = new Palette(
                    bytes.Sub(
                        i * paletteSize + portraitsPerPage * portraitWidth * portraitHeight / 2,
                        ( i + 1 ) * paletteSize + portraitsPerPage * portraitWidth * portraitHeight / 2 - 1 ) );
            }

            for ( int row = 0; row < rowsPerPage; row++ )
            {
                for ( int col = 0; col < colsPerPage; col++ )
                {
                    Bitmap b = new Bitmap( portraitWidth, portraitHeight, PixelFormat.Format4bppIndexed );
                    ColorPalette p = b.Palette;
                    Palette.FixupColorPalette( p, Palettes, startIndex + row * col, 0 );
                    b.Palette = p;
                    BitmapData bmd = b.LockBits( new Rectangle( Point.Empty, b.Size ), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed );

                    int xOffset = col * portraitWidth;
                    int yOffset = row * portraitHeight;

                    for ( int x = 0; x < portraitWidth; x++ )
                    {
                        for ( int y = 0; y < portraitHeight; y++ )
                        {
                            bmd.SetPixel4bpp( x, y, truePixels[xOffset + x + ( yOffset + y ) * portraitWidth * colsPerPage] );
                        }
                    }
                    b.UnlockBits( bmd );
                    Images[startIndex + row * colsPerPage + col] = b;
                    b.Save( string.Format( "{0}.png", startIndex + row * colsPerPage + col ), ImageFormat.Png );
                }
            }

        }

        private IList<byte> GetTruePixels( IList<byte> bytes )
        {
            List<byte> result = new List<byte>( bytes.Count * 2 );
            foreach ( byte b in bytes )
            {
                result.Add( b.GetLowerNibble() );
                result.Add( b.GetUpperNibble() );
            }
            return result.AsReadOnly();
        }

        #endregion Constructors

        #region Methods (3)


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


		#endregion Methods 

    }
}
