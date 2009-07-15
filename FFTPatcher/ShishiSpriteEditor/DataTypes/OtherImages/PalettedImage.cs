using PatcherLib.Datatypes;
using PatcherLib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace FFTPatcher.SpriteEditor
{
    public class PalettedImage : AbstractImage
    {
        public PalettedImage( 
            string name, 
            int width, int height, 
            int numPalettes,
            PatcherLib.Iso.KnownPosition imagePosition, 
            PatcherLib.Iso.KnownPosition palettePosition )
            : base( name, width, height, imagePosition )
        {
            this.palettePosition = palettePosition;
        }

        private PatcherLib.Iso.KnownPosition palettePosition;

        protected override System.Drawing.Bitmap GetImageFromIsoInner( System.IO.Stream iso )
        {
            Palette p = new Palette( palettePosition.ReadIso( iso ) );
            IList<byte> bytes = Position.ReadIso( iso );
            IList<byte> splitBytes = new List<byte>( bytes.Count * 2 );
            foreach (byte b in bytes.Sub( 0, Height * Width / 2 ))
            {
                splitBytes.Add( b.GetLowerNibble() );
                splitBytes.Add( b.GetUpperNibble() );
            }

            Bitmap result = new Bitmap( Width, Height, System.Drawing.Imaging.PixelFormat.Format4bppIndexed );
            ColorPalette palette = result.Palette;
            Palette.FixupColorPalette( palette, new Palette[] { p } );
            result.Palette = palette;

            BitmapData bmd = result.LockBits( new Rectangle( 0, 0, Width, Height ), ImageLockMode.WriteOnly, result.PixelFormat );

            for (int i = 0; i < Width * Height; i++)
            {
                bmd.SetPixel8bpp( i % Width, i / Width, splitBytes[i] );
            }
            result.UnlockBits( bmd );

            return result;
        }

        protected override void WriteImageToIsoInner( System.IO.Stream iso, System.Drawing.Image image )
        {
            
        }
    }
}
