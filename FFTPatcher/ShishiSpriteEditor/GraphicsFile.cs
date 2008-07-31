using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace FFTPatcher.SpriteEditor
{
    public abstract class GraphicsFile
    {
        public Size Size { get; protected set; }

        public abstract Color this[int x, int y] { get; set; }

        public string Filename { get; set; }

        public int Width
        {
            get { return Size.Width; }
            protected set { Size = new Size( value, Size.Height ); }
        }

        public int Height
        {
            get { return Size.Height; }
            protected set { Size = new Size( Size.Width, value ); }
        }

        protected GraphicsFile()
        {
        }

        public abstract void Import( Image file );

        public abstract Image Export();

        public virtual void Export( string filename, ImageFormat format )
        {
            Image i = Export();
            i.Save( filename, format );
        }

        public abstract byte[] ToByteArray();

        public abstract void Draw( Graphics graphics, int paletteIndex );
    }
}
