using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using PatcherLib.Utilities;
using PatcherLib.Datatypes;
namespace FFTPatcher.SpriteEditor
{
    public class StupidTM2Image : AbstractImage
    {
        protected PatcherLib.Iso.KnownPosition PalettePosition { get; private set; }
        protected IList<PatcherLib.Iso.KnownPosition> PixelPositions { get; private set; }

        protected virtual int NumColors { get { return 256; } }

        public StupidTM2Image( string name, int width, int height,
            PatcherLib.Iso.KnownPosition palettePosition,
            PatcherLib.Iso.KnownPosition firstPixelsPosition,
            params PatcherLib.Iso.KnownPosition[] otherPixelsPositions )
            : base( name, width, height )
        {
            PalettePosition = palettePosition;
            var pixelPositions = new List<PatcherLib.Iso.KnownPosition>( 1 + otherPixelsPositions.Length );
            pixelPositions.Add( firstPixelsPosition );
            pixelPositions.AddRange( otherPixelsPositions );

            int sum = 0;
            pixelPositions.ForEach( kp => sum += kp.Length );

            PixelPositions = pixelPositions.AsReadOnly();
        }

        protected IList<byte> GetAllPixelBytes(System.IO.Stream iso)
        {
            List<byte> pixels = new List<byte>();
            PixelPositions.ForEach( pp => pixels.AddRange( pp.ReadIso( iso ) ) );
            return pixels.AsReadOnly();
        }

        protected Palette GetPalette( System.IO.Stream iso )
        {
            return new Palette( PalettePosition.ReadIso( iso ) );
        }
        
        protected override System.Drawing.Bitmap GetImageFromIsoInner( System.IO.Stream iso )
        {
            var pixels = GetAllPixelBytes( iso );

            Palette palette = GetPalette( iso );

            Bitmap result = new Bitmap( Width, Height );

            for ( int i = 0; i < Width * Height; i++ )
            {
                result.SetPixel( i % Width, i / Width, palette[pixels[i]] );
            }

            return result;
        }

        protected override string SaveFileName
        {
            get { throw new NotImplementedException(); }
        }

        protected virtual IList<byte> PixelsToBytes( IList<byte> pixels )
        {
            return pixels;
        }

        protected override void WriteImageToIsoInner( System.IO.Stream iso, System.Drawing.Image image )
        {
            using ( System.Drawing.Bitmap sourceBitmap = new System.Drawing.Bitmap( image ) )
            {
                Set<System.Drawing.Color> colors = GetColors( sourceBitmap );
                if ( colors.Count > NumColors )
                {
                    ImageQuantization.OctreeQuantizer q = new ImageQuantization.OctreeQuantizer( NumColors, 8 );
                    using ( var newBmp = q.Quantize( sourceBitmap ) )
                    {
                        WriteImageToIsoInner( iso, newBmp );
                    }
                }
                else
                {
                    IList<byte> bytes = new List<byte>( Width * Height );
                    for ( int y = 0; y < Height; y++ )
                    {
                        for ( int x = 0; x < Width; x++ )
                        {
                            bytes.Add( (byte)colors.IndexOf( sourceBitmap.GetPixel( x, y ) ) );
                        }
                    }

                    bytes = PixelsToBytes( bytes );

                    int currentIndex = 0;
                    foreach ( var kp in PixelPositions )
                    {
                        kp.PatchIso( iso, bytes.Sub( currentIndex, currentIndex + kp.Length - 1 ) );
                        currentIndex += kp.Length;
                    }
                }
            }
        }
    }
}
