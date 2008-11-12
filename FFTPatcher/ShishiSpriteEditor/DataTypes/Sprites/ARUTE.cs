using System;
using System.Collections.Generic;
using System.Text;

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
            get { return new System.Drawing.Rectangle( 116, 68, 32, 48 ); }
        }

        public ARUTE( IList<byte> bytes )
            : base( "ARUTE", bytes )
        {
        }
    }
}
