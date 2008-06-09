using System.Drawing;
using FFTPatcher.SpriteEditor;
using NUnit.Framework;

namespace PatcherTests
{
    [TestFixture]
    public class WLDFACETests
    {
        [Test]
        public void ShouldExportBitmap()
        {
            byte[] bytes = Resources.WLDFACE4;
            WLDFACE face = new WLDFACE( bytes );
            for( int i = 0; i < 64; i++ )
            {
                using( Bitmap b = face.ToBitmap( i ) )
                {
                    b.Save( string.Format( "{0}.bmp", i ) );
                }
            }
        }
    }
}
