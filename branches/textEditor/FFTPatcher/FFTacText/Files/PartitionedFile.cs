using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.TextEditor
{
    class PartitionedFile : AbstractFile
    {
        public int PartitionSize { get; private set; }

        public PartitionedFile( GenericCharMap map, FFTTextFactory.FileInfo layout, IList<IList<string>> strings )
            : base( map, layout, strings, false )
        {
            PartitionSize = layout.Size / NumberOfSections;
        }

        public PartitionedFile( GenericCharMap map, FFTPatcher.TextEditor.FFTTextFactory.FileInfo layout, IList<byte> bytes )
            : base( map, layout, false )
        {
            PartitionSize = layout.Size / NumberOfSections;
            List<IList<string>> sections = new List<IList<string>>( NumberOfSections );
            for ( int i = 0; i < NumberOfSections; i++ )
            {
                sections.Add( TextUtilities.ProcessList( bytes.Sub( i * PartitionSize, ( i + 1 ) * PartitionSize - 1 ), map ) );
            }
            Sections = sections.AsReadOnly();
        }

        protected override IList<byte> ToByteArray()
        {
            List<byte> result = new List<byte>( Layout.Size );
            foreach ( IList<string> section in Sections )
            {
                List<byte> currentPart = new List<byte>( PartitionSize );
                section.ForEach( s => currentPart.AddRange( CharMap.StringToByteArray( s ) ) );
                currentPart.AddRange( new byte[Math.Max( PartitionSize - currentPart.Count, 0 )] );
                result.AddRange( currentPart.Sub( 0, PartitionSize - 1 ) );
            }

            return result.AsReadOnly();
        }

        protected override IList<byte> ToByteArray( IDictionary<string, byte> dteTable )
        {
            return ToByteArray();
        }
    }
}
