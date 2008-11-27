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
            //get { return new System.Drawing.Rectangle( 30, 5, 48, 48 ); }
        }

        internal KANZEN( SerializedSprite sprite )
            : base( sprite )
        {
        }

        public KANZEN( string name, IList<byte> bytes )
            : base( name, new string[] { name + ".SPR" }, bytes )
        {
        }

        public KANZEN( IList<byte> bytes )
            : this( "KANZEN", bytes )
        {
        }
    }
}
