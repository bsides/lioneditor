using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using PatcherLib.Datatypes;

namespace FFTPatcher.SpriteEditor
{
    public class Greyscale4bppImage : PalettedImage4bpp
    {
        static Color[] colors = new Color[] {
            Color.FromArgb(0,0,0), Color.FromArgb(0x10, 0x10, 0x10), Color.FromArgb(0x20,0x20,0x20), Color.FromArgb(0x30,0x30,0x30),
            Color.FromArgb(0x40,0x40,0x40), Color.FromArgb(0x50, 0x50, 0x50), Color.FromArgb(0x60,0x60,0x60), Color.FromArgb(0x70,0x70,0x70),
            Color.FromArgb(0x80,0x80,0x80), Color.FromArgb(0x90, 0x90, 0x90), Color.FromArgb(0xa0,0xa0,0xa0), Color.FromArgb(0xb0,0xb0,0xb0),
            Color.FromArgb(0xC0,0xC0,0xC0), Color.FromArgb(0xD0, 0xd0, 0xd0), Color.FromArgb(0xe0,0xe0,0xe0), Color.FromArgb(0xf0,0xf0,0xf0),
        };

        public Greyscale4bppImage( string name, int width, int height, PatcherLib.Iso.KnownPosition imagePosition )
            : base( name, width, height, 1, imagePosition, new FakeGreyscalePalettePosition() )
        {
        }

        protected override void WriteImageToIsoInner( System.IO.Stream iso, Image image )
        {
            var q = new ImageQuantization.PaletteQuantizer( colors );
            using (var newImage = q.Quantize( image ))
            {
                var imageBytes = GetImageBytes( newImage, new Set<Color>( colors ) );
                position.PatchIso( iso, imageBytes );
            }
        }

        private class FakeGreyscalePalettePosition : PatcherLib.Iso.KnownPosition
        {
            public override void PatchIso( System.IO.Stream iso, IList<byte> bytes )
            {
            }

            public override IList<byte> ReadIso( System.IO.Stream iso )
            {
                return new Palette( colors ).ToByteArray();
            }

            public override PatcherLib.Datatypes.PatchedByteArray GetPatchedByteArray( byte[] bytes )
            {
                return new PatcherLib.Datatypes.PatchedByteArray( 0, 0, new byte[0] );
            }

            public override int Length
            {
                get { return 16 * 2; }
            }
        }
    }
}