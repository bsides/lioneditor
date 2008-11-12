using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    public class TYPE2Sprite : AbstractCompressedSprite
    {
        public TYPE2Sprite( IList<byte> bytes )
            : base( bytes )
        {
        }
    }
}
