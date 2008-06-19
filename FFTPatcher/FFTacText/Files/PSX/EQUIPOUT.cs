using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.TextEditor.Files.PSX
{
    public class EQUIPOUT : BasePSXCompressedFile
    {
        private const int numberOfSections = 21;
        private const string filename = "EQUIP.OUT";
        private static Dictionary<string, long> locations;
        private static readonly string[][] entryNames;
        private static readonly string[] sectionNames;
        private const int maxLength = 0x8AB7;

        static EQUIPOUT()
        {
            sectionNames = new string[numberOfSections];
            entryNames = new string[numberOfSections][];
            for( int i = 0; i < numberOfSections; i++ )
            {
                entryNames[i] = new string[1024];
            }
        }

        protected override int NumberOfSections
        {
            get { return numberOfSections; }
        }

        public override IList<IList<string>> EntryNames
        {
            get { return entryNames; }
        }

        public override string Filename
        {
            get { return filename; }
        }

        public override IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "EQUIP.OUT", 0x10BD8 );
                }

                return locations;
            }
        }

        public override int MaxLength
        {
            get { return maxLength; }
        }

        public override IList<string> SectionNames
        {
            get { return sectionNames; }
        }

        public EQUIPOUT( IList<byte> bytes )
        {
            Sections = new List<IList<string>>( NumberOfSections );
            for( int i = 0; i < NumberOfSections; i++ )
            {
                uint start = Utilities.BytesToUInt32( bytes.Sub( i * 4, i * 4 + 3 ) );
                uint stop = Utilities.BytesToUInt32( bytes.Sub( (i + 1) * 4, (i + 1) * 4 + 3 ) ) - 1;
                if( i == NumberOfSections - 1 )
                {
                    stop = (uint)bytes.Count - 1 - dataStart;
                }

                IList<byte> thisSection = TextUtilities.Decompress(
                    bytes,
                    bytes.Sub( (int)(start + dataStart), (int)(stop + dataStart) ),
                    (int)(start + dataStart) );
                Sections.Add( TextUtilities.ProcessList( thisSection, CharMap ) );
            }
        }
    }
}
