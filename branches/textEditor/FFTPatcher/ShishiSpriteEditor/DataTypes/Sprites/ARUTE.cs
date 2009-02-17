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
            //get { return new System.Drawing.Rectangle( 33, 33, 48, 48 ); }
        }

        internal ARUTE( SerializedSprite sprite )
            : base( sprite )
        {
        }

        public ARUTE( string name, IList<byte> bytes )
            : base( name, new string[] { name + ".SPR" }, bytes )
        {
        }

        public ARUTE( IList<byte> bytes )
            : this( "ARUTE", bytes )
        {
        }
    }
}
