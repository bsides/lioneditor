using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using PatcherLib.Datatypes;

namespace FFTPatcher.SpriteEditor
{
    class PalettedImage8bpp : AbstractImage
    {
        private PatcherLib.Iso.KnownPosition position;

        public PalettedImage8bpp( 
            string name, 
            int width, int height, 
            int numPalettes, Palette.ColorDepth depth,
            PatcherLib.Iso.KnownPosition imagePosition, 
            PatcherLib.Iso.KnownPosition palettePosition )
            : base( name, width, height )
        {
            this.position = imagePosition;
            this.palettePosition = palettePosition;
            this.depth = depth;
            System.Diagnostics.Debug.Assert( palettePosition.Length == 256 * (int)depth * numPalettes );
            if ( position is PatcherLib.Iso.PsxIso.KnownPosition )
            {
                var pos = position as PatcherLib.Iso.PsxIso.KnownPosition;
                saveFileName = string.Format( "{0}_{1}.png", pos.Sector, pos.StartLocation );
            }
            else if ( position is PatcherLib.Iso.PspIso.KnownPosition )
            {
                var pos = position as PatcherLib.Iso.PspIso.KnownPosition;
                saveFileName = string.Format( "{0}_{1}.png", pos.SectorEnum, pos.StartLocation );
            }
        }

        private string saveFileName;
        protected override string SaveFileName
        {
            get { return saveFileName; }
        }

        private FFTPatcher.SpriteEditor.Palette.ColorDepth depth;
        private PatcherLib.Iso.KnownPosition palettePosition;

        protected override System.Drawing.Bitmap GetImageFromIsoInner( System.IO.Stream iso )
        {
            Palette p = new Palette( palettePosition.ReadIso( iso ), depth );
            IList<byte> bytes = position.ReadIso( iso );

            Bitmap result = new Bitmap( Width, Height );

            for (int i = 0; i < Width * Height; i++)
            {
                result.SetPixel( i % Width, i / Width, p[bytes[i]] );
            }

            return result;
        }

        public override string FilenameFilter
        {
            get
            {
                return "GIF file (*.gif)|*.gif";
            }
        }

        public override void SaveImage( System.IO.Stream iso, System.IO.Stream output )
        {
            // Get colors
            Set<Color> colors = GetColors( iso );

            // Convert colors to indices
            Bitmap originalImage = GetImageFromIso(iso);

            using ( Bitmap bmp = new Bitmap( Width, Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed ) )
            {
                var pal = bmp.Palette;
                for ( int i = 0; i < colors.Count; i++ )
                {
                    pal.Entries[i] = colors[i];
                }
                bmp.Palette = pal;

                var bmd = bmp.LockBits( new Rectangle( 0, 0, Width, Height ), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed );
                for ( int x = 0; x < Width; x++ )
                {
                    for ( int y = 0; y < Height; y++ )
                    {
                        bmd.SetPixel8bpp( x, y, colors.IndexOf( originalImage.GetPixel( x, y ) ) );
                    }
                }
                bmp.UnlockBits( bmd );

                // Write that shit
                bmp.Save( output, System.Drawing.Imaging.ImageFormat.Gif );
            }
        }


        protected override void WriteImageToIsoInner( System.IO.Stream iso, System.Drawing.Image image )
        {
            using ( Bitmap bmp = new Bitmap( image ) )
            {
                Set<Color> colors = GetColors( bmp );
                if ( colors.Count > 256 )
                {
                    ImageQuantization.OctreeQuantizer q = new ImageQuantization.OctreeQuantizer( 256,  8 );
                    using ( var newBmp = q.Quantize( bmp ) )
                    {
                        WriteImageToIsoInner( iso, newBmp );
                    }
                }
                else
                {
                    var paletteBytes = GetPaletteBytes( colors, depth );
                    var imageBytes = GetImageBytes( bmp, colors );
                    position.PatchIso( iso, imageBytes );
                    palettePosition.PatchIso( iso, paletteBytes );
                }
            }
        }

        private IList<byte> GetImageBytes( Bitmap image, Set<Color> colors )
        {
            List<byte> result = new List<byte>( Width * Height );
            for ( int y = 0; y < Height; y++ )
            {
                for ( int x = 0; x < Width; x++ )
                {
                    result.Add( (byte)colors.IndexOf( image.GetPixel( x, y ) ) );
                }
            }
            return result;
        }

    }
}
