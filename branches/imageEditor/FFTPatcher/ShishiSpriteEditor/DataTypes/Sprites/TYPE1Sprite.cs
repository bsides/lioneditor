using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    public class TYPE1Sprite : AbstractShapedSprite
    {
        public override Shape Shape
        {
            get { return Shape.TYPE1; }
        }

        public override int ThumbnailFrame
        {
            get { return 2; }
        }

        protected override System.Drawing.Rectangle ThumbnailRectangle
        {
            get { return new System.Drawing.Rectangle( 116, 95, 48, 32 ); }
        }

        public TYPE1Sprite( string name, IList<byte> bytes )
            : base( name, bytes )
        {
        }
    }
}
