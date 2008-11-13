using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    public class KANZEN : AbstractShapedSprite
    {
        public override Shape Shape
        {
            get { return Shape.KANZEN; }
        }

        public override int ThumbnailFrame
        {
            get { return 1; }
        }

        protected override System.Drawing.Rectangle ThumbnailRectangle
        {
            get { return new System.Drawing.Rectangle( 105, 10, 48, 48 ); }
        }

        public KANZEN( IList<byte> bytes )
            : base( "KANZEN", bytes )
        {
        }
    }
}
