using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    public abstract class AbstractImage
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        protected abstract System.Drawing.Bitmap GetImageFromIsoInner( System.IO.Stream iso );

        System.Drawing.Bitmap cachedImage;
        bool cachedImageDirty = true;
        public System.Drawing.Bitmap GetImageFromIso( System.IO.Stream iso )
        {
            if (cachedImageDirty && cachedImage != null)
            {
                cachedImage.Dispose();
                cachedImage = null;
            }

            if ( cachedImage == null )
            {
                cachedImage = GetImageFromIsoInner( iso );
                cachedImageDirty = false;
            }
            return cachedImage;
        }

        protected abstract void WriteImageToIsoInner( System.IO.Stream iso, System.Drawing.Image image );

        public void WriteImageToIso( System.IO.Stream iso, System.IO.Stream image )
        {
            using ( System.Drawing.Image i = System.Drawing.Image.FromStream( image ) )
            {
                WriteImageToIso( iso, i );
            }
        }

        public void WriteImageToIso( System.IO.Stream iso, string filename )
        {
            using ( System.IO.Stream s = System.IO.File.OpenRead( filename ) )
            {
                WriteImageToIso( iso, s );
            }
        }

        public void WriteImageToIso( System.IO.Stream iso, System.Drawing.Image image )
        {
            if ( image.Width != Width || image.Height != Height )
            {
                throw new FormatException( "image has wrong dimensions" );
            }

            WriteImageToIsoInner( iso, image );
            cachedImageDirty = true;
        }

        protected PatcherLib.Iso.KnownPosition Position { get; private set; }

        public string Name { get; private set; }

        public AbstractImage( string name, int width, int height, PatcherLib.Iso.KnownPosition position )
        {
            Name = name;
            Position = position;
            Width = width;
            Height = height;
        }
    }
}
