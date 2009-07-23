using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FFTPatcher.SpriteEditor
{
    class Raw16BitImage : AbstractImage
    {
        private PatcherLib.Iso.KnownPosition position;

        public Raw16BitImage( string name, int width, int height, PatcherLib.Iso.KnownPosition position )
            : base( name, width, height )
        {
            this.position = position;
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

        protected override System.Drawing.Bitmap GetImageFromIsoInner( System.IO.Stream iso )
        {
            IList<byte> bytes = position.ReadIso( iso );
            IList<Color> pixels = new Color[bytes.Count / 2];
            for ( int i = 0; i < pixels.Count; i++ )
            {
                pixels[i] = Palette.BytesToColor( bytes[i * 2], bytes[i * 2 + 1] );
            }

            Bitmap result = new Bitmap( Width, Height );
            for ( int x = 0; x < Width; x++ )
            {
                for ( int y = 0; y < Height; y++ )
                {
                    result.SetPixel( x, y, pixels[y * Width + x] );
                }
            }
            return result;
        }

        protected override void WriteImageToIsoInner( System.IO.Stream iso, System.Drawing.Image image )
        {
            IList<Color> pixels = new Color[Width * Height];
            using ( Bitmap bmp = new Bitmap( image ) )
            {
                for ( int x = 0; x < Width; x++ )
                {
                    for ( int y = 0; y < Height; y++ )
                    {
                        pixels[y * Width + x] = bmp.GetPixel( x, y );
                    }
                }
            }

            byte[] result = new byte[pixels.Count * 2];
            for ( int i = 0; i < pixels.Count; i++ )
            {
                byte[] bytes = Palette.ColorToBytes( pixels[i] );
                result[i * 2] = bytes[0];
                result[i * 2 + 1] = bytes[1];
            }

            position.PatchIso( iso, result );
        }
    }
}
