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
        private IList<Bitmap> frames;

        protected bool FramesDirty { get; set; }

        public abstract Shape Shape { get; }

        public abstract int ThumbnailFrame { get; }

        public IList<Bitmap> Frames 
        {
            get
            {
                if ( FramesDirty )
                {
                    frames = Shape.GetFrames( this );
                    FramesDirty = false;
                }

                return new ReadOnlyCollection<Bitmap>( frames );
            }
        }

        public AbstractShapedSprite( string name, IList<byte> bytes, params IList<byte>[] otherBytes )
            : base( name, bytes, otherBytes )
        {
            FramesDirty = true;
        }

        public override Image GetThumbnail()
        {
            Bitmap result = new Bitmap( 64, 48, PixelFormat.Format8bppIndexed );

            ColorPalette palette = result.Palette;
            FixupColorPalette( palette );
            result.Palette = palette;

            Frames[ThumbnailFrame].CopyRectangleToPoint( ThumbnailRectangle, result, Point.Empty, Palettes[0], false );

            using ( Bitmap portrait = new Bitmap( 48, 32, PixelFormat.Format8bppIndexed ) )
            using ( Bitmap wholeImage = ToBitmap() )
            {
                ColorPalette palette2 = portrait.Palette;
                FixupColorPalette( palette2 );
                portrait.Palette = palette2;
                wholeImage.CopyRectangleToPoint( PortraitRectangle, portrait, Point.Empty, Palettes[8], false );
                portrait.RotateFlip( RotateFlipType.Rotate270FlipNone );
                portrait.CopyRectangleToPoint( new Rectangle( 0, 0, 32, 48 ), result, new Point( 32, 0 ), Palettes[8], false );
            }

            return result;
        }
    }
}
