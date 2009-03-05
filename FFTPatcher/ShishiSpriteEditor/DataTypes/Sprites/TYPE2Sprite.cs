using System.Collections.Generic;
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
            //get { return new System.Drawing.Rectangle( 1, 3, 48, 48 ); }
        }

        internal TYPE2Sprite( SerializedSprite sprite )
            : base( sprite )
        {
        }

        public TYPE2Sprite( string name, IList<byte> bytes )
            : base( name, new string[] { name + ".SPR" }, bytes )
        {
        }
    }
}
