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
            get { return new System.Drawing.Rectangle( 107, 88, 48, 48 ); }
            //get { return new System.Drawing.Rectangle( 1, 3, 48, 48 ); }
        }

        internal TYPE1Sprite( SerializedSprite sprite )
            : base( sprite )
        {
        }

        public TYPE1Sprite( string name, IList<byte> bytes )
            : base( name, new string[] { name + ".SPR" }, bytes )
        {
        }
    }
}
