using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FFTPatcher.SpriteEditor
{
    public class TYPE2Sprite : AbstractShapedSprite
    {
        public override Shape Shape
        {
            get { return Shape.TYPE2; }
        }

        public override int ThumbnailFrame
        {
            get { return 2; }
        }

        protected override System.Drawing.Rectangle ThumbnailRectangle
        {
            get { return new Rectangle( 107, 88, 48, 48 ); }
        }

        public TYPE2Sprite( string name, IList<byte> bytes )
            : base( name, bytes )
        {
        }
    }
}
