using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;

namespace FFTPatcher.SpriteEditor
{
    public abstract class AbstractShapedSprite : AbstractCompressedSprite
    {
        public abstract int ThumbnailFrame { get; }

        public IList<Bitmap> GetFrames()
        {
            return Shape.GetFrames( this );
        }

        internal AbstractShapedSprite( SerializedSprite sprite )
            : base( sprite )
        {
        }

        public AbstractShapedSprite( string name, IList<string> filenames, IList<byte> bytes, params IList<byte>[] otherBytes )
            : base( name, filenames, bytes, otherBytes )
        {
        }

        public override void Import( Image file )
        {
            base.Import( file );
        }

        public override void ImportBitmap( Bitmap bmp, out bool foundBadPixels )
        {
            base.ImportBitmap( bmp, out foundBadPixels );
        }

        protected override void ImportSPRInner( IList<byte> bytes )
        {
            base.ImportSPRInner( bytes );
        }

        protected override Image GetThumbnailInner()
        {
            Bitmap result = new Bitmap( 80, 48, PixelFormat.Format32bppArgb );

            Shape.Frames[ThumbnailFrame].GetFrame( this ).CopyRectangleToPointNonIndexed(
                ThumbnailRectangle,
                result,
                new Point( ( 48 - ThumbnailRectangle.Width ) / 2, ( 48 - ThumbnailRectangle.Height ) / 2 ),
                Palettes[0],
                false );

            using ( Bitmap portrait = new Bitmap( 48, 32, PixelFormat.Format8bppIndexed ) )
            {
                Bitmap wholeImage = ToBitmap();
                ColorPalette palette2 = portrait.Palette;
                Palette.FixupColorPalette( palette2, Palettes );
                portrait.Palette = palette2;

                wholeImage.CopyRectangleToPoint( PortraitRectangle, portrait, Point.Empty, Palettes[8], false );
                portrait.RotateFlip( RotateFlipType.Rotate270FlipNone );
                portrait.CopyRectangleToPointNonIndexed( new Rectangle( 0, 0, 32, 48 ), result, new Point( 48, 0 ), Palettes[8], false );
            }

            return result;
        }
    }
}
