using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    public class ShortSprite : AbstractSprite
    {
        /*
         * 10M
         * 10W
         * 20M
         * 20W
         * 40M
         * 40W
         * 60M
         * 60W
         * CYOMON1
         * CYOMON2
         * CYOMON3
         * CYOMON4
         */

        public override int Height
        {
            get { return 288; }
        }

        public ShortSprite( IList<byte> bytes )
            : base( bytes )
        {
        }

        protected override IList<byte> BuildPixels( IList<byte> bytes, IList<byte>[] extraBytes )
        {
            byte[] result = new byte[36864 * 2];
            for( int i = 0; i < 36864; i++ )
            {
                result[i * 2] = bytes[i].GetLowerNibble();
                result[i * 2 + 1] = bytes[i].GetUpperNibble();
            }

            return result;
        }

        public override IList<byte[]> ToByteArrays()
        {
            byte[] ourResult = new byte[256 * 288 / 2];
            for( int i = 0; i < Pixels.Count / 2; i++ )
            {
                ourResult[i / 2] = (byte)((Pixels[2 * i + 1] << 4) | (Pixels[2 * i] & 0x0F));
            }
            return new byte[][] { ourResult };
        }
    }
}
