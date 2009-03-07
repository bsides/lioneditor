using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using PatcherLib.Datatypes;

namespace FFTPatcher.SpriteEditor
{
    public class OPNBK
    {

		#region Fields (1) 

        private Image[] images;

		#endregion Fields 

		#region Constructors (1) 

        public OPNBK( IList<byte> bytes )
        {
            const int start0 = 0;
            const int end0 = 320 * 240 * 2 - 1;
            const int start1 = end0 + 1;
            const int end1 = start1 + 256 * 240 * 2 - 1;
            const int start2 = end1 + 1;
            const int end2 = start2 + 210 * 180 * 2 - 1;
            const int start3 = end2 + 1;
            const int end3 = start3 + 210 * 180 * 2 - 1;
            const int start4 = end3 + 1;
            const int end4 = start4 + 210 * 180 * 2 - 1;
            const int start5 = end4 + 1;
            const int end5 = start5 + 210 * 180 * 2 - 1;
            const int start6 = end5 + 1;
            const int end6 = start6 + 512 * 240 * 2 - 1;

            images = new Image[7];
            images[0] = Utilities.ReadBytesAsRawImage( bytes.Sub( start0, end0 ), 320, 240 ).ToImage();
            images[1] = Utilities.ReadBytesAsRawImage( bytes.Sub( start1, end1 ), 256, 240 ).ToImage();
            images[2] = Utilities.ReadBytesAsRawImage( bytes.Sub( start2, end2 ), 210, 180 ).ToImage();
            images[3] = Utilities.ReadBytesAsRawImage( bytes.Sub( start3, end3 ), 210, 180 ).ToImage();
            images[4] = Utilities.ReadBytesAsRawImage( bytes.Sub( start4, end4 ), 210, 180 ).ToImage();
            images[5] = Utilities.ReadBytesAsRawImage( bytes.Sub( start5, end5 ), 210, 180 ).ToImage();
            images[6] = Utilities.ReadBytesAsRawImage( bytes.Sub( start6, end6 ), 512, 240 ).ToImage();

        }

		#endregion Constructors 

		#region Properties (1) 

        public IList<Image> Images { get { return new ReadOnlyCollection<Image>( images ); } }

		#endregion Properties 

    }
}
