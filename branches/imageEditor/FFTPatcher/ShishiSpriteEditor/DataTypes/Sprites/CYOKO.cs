using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    public class CYOKO : AbstractShapedSprite
    {
        public override Shape Shape
        {
            get { return Shape.CYOKO; }
        }

        public override int ThumbnailFrame
        {
            get { return 2; }
        }

        protected override System.Drawing.Rectangle ThumbnailRectangle
        {
            get { return new System.Drawing.Rectangle( 112, 92, 32, 48 ); }
        }

        public CYOKO( IList<byte> bytes )
            : base( "CYOKO", bytes )
        {
        }
    }
}
