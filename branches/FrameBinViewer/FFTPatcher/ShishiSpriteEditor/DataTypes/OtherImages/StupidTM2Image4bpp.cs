using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using PatcherLib.Utilities;
using PatcherLib.Datatypes;
namespace FFTPatcher.SpriteEditor
{
    public class StupidTM2Image4bpp : StupidTM2Image
    {

        protected override int NumColors
        {
            get
            {
                return 16;
            }
        }

        public StupidTM2Image4bpp( string name, int width, int height,
            PatcherLib.Iso.KnownPosition palettePosition,
            PatcherLib.Iso.KnownPosition firstPixelsPosition,
            params PatcherLib.Iso.KnownPosition[] otherPixelsPositions )
            : base( name, width, height, palettePosition, firstPixelsPosition, otherPixelsPositions)
        {
        }

        protected override IList<byte> PixelsToBytes( IList<byte> pixels )
        {
            List<byte> result = new List<byte>( pixels.Count / 2 );

            for ( int i = 0; i < pixels.Count; i += 2 )
            {
                result.Add( (byte)( pixels[i] & 0x0F | ( ( pixels[i + 1] & 0x0F ) << 4 ) ) );
            }

            return result.AsReadOnly();
        }

        protected override System.Drawing.Bitmap GetImageFromIsoInner( System.IO.Stream iso )
        {
            var pixels = GetAllPixelBytes( iso );
            var palette = GetPalette( iso );

            Bitmap result = new Bitmap( Width, Height );

            var mypixels = new List<byte>();
            foreach ( byte p in pixels )
            {
                mypixels.Add( p.GetLowerNibble() );
                mypixels.Add( p.GetUpperNibble() );
            }
            pixels = mypixels;

            for ( int i = 0; i < Width * Height; i++ )
            {
                result.SetPixel( i % Width, i / Width, palette[pixels[i]] );
            }

            return result;
        }
    }
}
