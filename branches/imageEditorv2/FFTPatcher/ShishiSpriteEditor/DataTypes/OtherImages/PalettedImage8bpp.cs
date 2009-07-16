using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FFTPatcher.SpriteEditor
{
    class PalettedImage8bpp : AbstractImage
    {
        public PalettedImage8bpp( 
            string name, 
            int width, int height, 
            int numPalettes, Palette.ColorDepth depth,
            PatcherLib.Iso.KnownPosition imagePosition, 
            PatcherLib.Iso.KnownPosition palettePosition )
            : base( name, width, height, imagePosition )
        {
            this.palettePosition = palettePosition;
            this.depth = depth;
            System.Diagnostics.Debug.Assert( palettePosition.Length == 256 * (int)depth );
        }

        private FFTPatcher.SpriteEditor.Palette.ColorDepth depth;
        private PatcherLib.Iso.KnownPosition palettePosition;

        protected override System.Drawing.Bitmap GetImageFromIsoInner( System.IO.Stream iso )
        {
            Palette p = new Palette( palettePosition.ReadIso( iso ), depth );
            IList<byte> bytes = Position.ReadIso( iso );

            Bitmap result = new Bitmap( Width, Height );

            for (int i = 0; i < Width * Height; i++)
            {
                result.SetPixel( i % Width, i / Width, p[bytes[i]] );
            }

            return result;
        }

        protected override void WriteImageToIsoInner( System.IO.Stream iso, System.Drawing.Image image )
        {
            
        }
    }
}
