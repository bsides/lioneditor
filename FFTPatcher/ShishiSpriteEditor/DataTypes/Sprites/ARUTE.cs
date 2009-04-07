using System.Collections.Generic;

namespace FFTPatcher.SpriteEditor
{
    public class ARUTE : AbstractShapedSprite
    {
        public override Shape Shape
        {
            get { return Shape.ARUTE; }
        }

        public override int ThumbnailFrame
        {
            get { return 1; }
        }

        protected override System.Drawing.Rectangle ThumbnailRectangle
        {
            get { return new System.Drawing.Rectangle( 108, 68, 48, 48 ); }
        }

        internal ARUTE( SerializedSprite sprite )
            : base( sprite )
        {
        }

        public ARUTE( IList<byte> bytes )
            : base( bytes )
        {
        }
    }
}
