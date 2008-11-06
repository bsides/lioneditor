using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FFTPatcher.SpriteEditor.Files
{
    internal class BaseFile
    {
        public static IList<byte> Bytes { get; private set; }

        public BaseFile( IList<byte> bytes )
        {
            Bytes = new ReadOnlyCollection<byte>( bytes );
        }
    }
}
