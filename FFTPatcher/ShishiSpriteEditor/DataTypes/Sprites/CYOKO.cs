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
            get { return new System.Drawing.Rectangle( 110, 92, 48, 48 ); }
            //get { return new System.Drawing.Rectangle( 10, 12, 48, 48 ); }
        }

        internal CYOKO( SerializedSprite sprite )
            : base( sprite )
        {
        }

        public CYOKO( IList<byte> bytes )
            : base( "CYOKO", new string[] { "CYOKO.SPR" }, bytes )
        {
        }
    }
}
