using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.TextEditor
{
    class PartitionedFile : AbstractFile
    {
        public int PartitionSize { get; private set; }

        public PartitionedFile( GenericCharMap map, FFTPatcher.TextEditor.FFTTextFactory.FileInfo layout, IList<byte> bytes )
            : base( map, layout )
        {
            PartitionSize = layout.Size / NumberOfSections;
            List<IList<string>> sections = new List<IList<string>>( NumberOfSections );
            for ( int i = 0; i < NumberOfSections; i++ )
            {
                sections.Add( TextUtilities.ProcessList( bytes.Sub( i * PartitionSize, ( i + 1 ) * PartitionSize - 1 ), map ) );
            }
            Sections = sections.AsReadOnly();
        }
    }
}
