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

            var position = firstPixelsPosition;
            if (position is PatcherLib.Iso.PsxIso.KnownPosition)
            {
                var pos = position as PatcherLib.Iso.PsxIso.KnownPosition;
                saveFileName = string.Format( "{0}_{1}.png", pos.Sector, pos.StartLocation );
            }
            else if (position is PatcherLib.Iso.PspIso.KnownPosition)
            {
                var pos = position as PatcherLib.Iso.PspIso.KnownPosition;
                saveFileName = string.Format( "{0}_{1}.png", pos.SectorEnum, pos.StartLocation );
            }
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

        protected virtual IList<byte> GetPaletteBytes( Set<Color> colors )
        {
            List<byte> result = new List<byte>( colors.Count * 2 );
            List<Color> colorList = new List<Color>( colors );
            int transparentIndex = colorList.FindIndex( c => c.A == 0 );
            if (transparentIndex != -1)
            {
                Color trans = colorList[transparentIndex];
                trans = Color.FromArgb( 255, trans.R, trans.G, trans.B );
                colorList.RemoveAt( transparentIndex );
                colorList.Insert( 0, trans );
            }

            foreach (Color c in colorList)
            {
                result.AddRange( Palette.ColorToBytes( c ) );
            }

            return result.AsReadOnly();
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
        private string saveFileName;

        protected override string SaveFileName
        {
            get { return saveFileName; }
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
                        var bb = bytes.Sub( currentIndex, currentIndex + kp.Length - 1 );
                        kp.PatchIso( iso, bb );
                        currentIndex += kp.Length;
#if DEBUG
                        Console.Out.WriteLine( "<Location file=\"WORLD_WLDTEX_TM2\" offset=\"{0:X}\">", (kp as PatcherLib.Iso.PsxIso.KnownPosition).StartLocation );
                        foreach (byte b in bb)
                        {
                            Console.Out.Write( "{0:X2}", b );
                        }
                        Console.Out.WriteLine();
                        Console.Out.WriteLine( "</Location>" );
#endif
                    }

                    PalettePosition.PatchIso( iso, GetPaletteBytes( colors ) );
                }
            }
        }
    }
}
