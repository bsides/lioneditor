using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.TextEditor
{
    class SectionedFile : AbstractFile
    {
        const int dataStart = 0x80;

        public SectionedFile( GenericCharMap map, FFTPatcher.TextEditor.FFTTextFactory.FileInfo layout, IList<byte> bytes )
            : base( map, layout )
        {
            List<IList<string>> sections = new List<IList<string>>( NumberOfSections );

            for ( int i = 0; i < NumberOfSections; i++ )
            {
                uint start = Utilities.BytesToUInt32( bytes.Sub( i * 4, ( i + 1 ) * 4 - 1 ) );
                uint stop = Utilities.BytesToUInt32( bytes.Sub( ( i + 1 ) * 4, ( i + 2 ) * 4 - 1 ) ) - 1;
                if ( i == NumberOfSections - 1 )
                {
                    stop = (uint)bytes.Count - 1 - dataStart;
                }
                IList<byte> thisSection = bytes.Sub( (int)( start + dataStart ), (int)( stop + dataStart ) );
                thisSection = TextUtilities.Decompress( bytes, thisSection, (int)( start - dataStart ) );
                sections.Add( TextUtilities.ProcessList( thisSection, CharMap ) );
            }
            Sections = sections.AsReadOnly();
        }

        public override byte[] ToByteArray()
        {
            return base.ToByteArray();
        }

        //public IList<IList<byte>> GetSectionByteArraysUncompressed
    }
}
