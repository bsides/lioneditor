using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FFTPatcher.Datatypes;
using System.IO;
using FFTPatcher.TextEditor.Files;

namespace FFTPatcher.TextEditor
{
    enum FileType
    {
        SectionedFile,
        CompressedFile,
        PartitionedFile,
        OneShotFile
    }

    public enum SectorType
    {
        Sector,
        BootBin,
        FFTPack
    }

    static class FFTTextFactory
    {
        public struct FileInfo
        {
            public Context Context { get; set; }
            public string DisplayName { get; set; }
            public Guid Guid { get; set; }
            public int Size { get; set; }
            public FileType FileType { get; set; }
            public IList<int> SectionLengths { get; set; }
            public IDictionary<SectorType, IList<KeyValuePair<Enum, int>>> Sectors { get; set; }
            public IList<string> SectionNames { get; set; }
            public IList<IList<string>> EntryNames { get; set; }
            public IList<IList<int>> DisallowedEntries { get; set; }
        }

        delegate IList<byte> BytesFromIso( Stream iso, Enum file, int offset, int size );

        private static IList<byte> BytesFromPspIso( Stream iso, Enum file, int offset, int size )
        {
            if ( file.GetType() == typeof( FFTPack.Files ) )
            {
                return FFTPack.GetFileFromIso( iso, (FFTPack.Files)file ).Sub( offset, offset + size - 1 );
            }
            else if ( file.GetType() == typeof( PspIso.Sectors ) )
            {
                return PspIso.GetFile( iso, (PspIso.Sectors)file, offset, size );
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private static IList<byte> BytesFromPsxIso( Stream iso, Enum file, int offset, int size )
        {
            if ( file.GetType() == typeof( PsxIso.Sectors ) )
            {
                return PsxIso.ReadFile( iso, (PsxIso.Sectors)file, offset, size );
            }
            else
            {
                throw new ArgumentException();
            }
        }
        private static IDictionary<Guid, ISerializableFile> GetFiles( Stream iso, Context context, XmlDocument doc, BytesFromIso reader, GenericCharMap charmap )
        {
            Dictionary<Guid, ISerializableFile> files = new Dictionary<Guid, ISerializableFile>();
            foreach ( XmlNode node in doc.SelectNodes( "//Files/*" ) )
            {
                string displayName = node.SelectSingleNode( "DisplayName" ).InnerText;
                Guid guid = new Guid( node.SelectSingleNode( "Guid" ).InnerText );
                int size = Int32.Parse( node.SelectSingleNode( "Size" ).InnerText );
                FileType filetype = (FileType)Enum.Parse( typeof( FileType ), node.Name );

                int sectionCount = Int32.Parse( node.SelectSingleNode( "Sections/@count" ).InnerText );
                int[] sectionLengths = new int[sectionCount];
                for ( int i = 0; i < sectionCount; i++ )
                {
                    sectionLengths[i] = Int32.Parse( node.SelectSingleNode( string.Format( "Sections/Section[@value='{0}']/@entries", i ) ).InnerText );
                }
                XmlNodeList sectors = node.SelectNodes( "Sectors/*" );
                Dictionary<SectorType, IList<KeyValuePair<Enum, int>>> dict = new Dictionary<SectorType, IList<KeyValuePair<Enum, int>>>( 3 );
                bool first = true;
                IList<byte> bytes = null;
                foreach ( XmlNode sectorNode in sectors )
                {
                    SectorType sectorType = (SectorType)Enum.Parse( typeof( SectorType ), sectorNode.Name );
                    if ( !dict.ContainsKey( sectorType ) )
                    {
                        dict.Add( sectorType, new List<KeyValuePair<Enum, int>>() );
                    }
                    int offset = Int32.Parse( sectorNode.Attributes["offset"].InnerText );
                    Enum fileEnum = null;
                    switch ( sectorType )
                    {
                        case SectorType.BootBin:
                            dict[sectorType].Add( new KeyValuePair<Enum, int>( PspIso.Sectors.PSP_GAME_SYSDIR_BOOT_BIN, offset ) );
                            dict[sectorType].Add( new KeyValuePair<Enum, int>( PspIso.Sectors.PSP_GAME_SYSDIR_EBOOT_BIN, offset ) );
                            fileEnum = PspIso.Sectors.PSP_GAME_SYSDIR_BOOT_BIN;
                            break;
                        case SectorType.FFTPack:
                            Datatypes.FFTPack.Files fftPackFile = (Datatypes.FFTPack.Files)Enum.Parse( typeof( Datatypes.FFTPack.Files ), sectorNode.SelectSingleNode( "@index" ).InnerText );
                            dict[sectorType].Add( new KeyValuePair<Enum, int>( fftPackFile, offset ) );
                            fileEnum = fftPackFile;
                            break;
                        case SectorType.Sector:
                            PsxIso.Sectors file = (PsxIso.Sectors)Enum.Parse( typeof( PsxIso.Sectors ), sectorNode.SelectSingleNode( "@filename" ).InnerText );
                            dict[sectorType].Add( new KeyValuePair<Enum, int>( file, offset ) );
                            fileEnum = file;
                            break;
                    }
                    if ( first )
                    {
                        bytes = reader( iso, fileEnum, offset, size );
                        first = false;
                    }
                }

                IList<IList<string>> entryNames = GetEntryNames( node.SelectSingleNode( "Sections" ), node.SelectSingleNode( "//Templates" ) );
                IList<string> sectionNames = GetSectionNames( node.SelectSingleNode( "Sections" ) );
                IList<IList<int>> disallowedEntries = GetDisallowedEntries( node, sectionLengths.Length );

                FileInfo fi = new FileInfo
                {
                    Context = context,
                    DisplayName = displayName,
                    DisallowedEntries = disallowedEntries.AsReadOnly(),
                    EntryNames = entryNames.AsReadOnly(),
                    FileType = filetype,
                    Guid = guid,
                    SectionLengths = sectionLengths.AsReadOnly(),
                    Sectors = new ReadOnlyDictionary<SectorType, IList<KeyValuePair<Enum, int>>>( dict ),
                    SectionNames = sectionNames,
                    Size = size
                };

                switch ( filetype )
                {
                    case FileType.CompressedFile:
                    case FileType.SectionedFile:
                        files.Add( guid, new SectionedFile( charmap, fi, bytes ) );
                        break;
                    case FileType.OneShotFile:
                    case FileType.PartitionedFile:
                        files.Add( guid, new PartitionedFile( charmap, fi, bytes ) );
                        break;
                }
            }

            return new ReadOnlyDictionary<Guid, ISerializableFile>( files );
        }

        private static IDictionary<SectionType, IList<QuickEdit.QuickEditEntry>> GetQuickEditLookup( XmlNode quickEditNode )
        {
            Dictionary<SectionType, IList<QuickEdit.QuickEditEntry>> result = new Dictionary<SectionType, IList<QuickEdit.QuickEditEntry>>();
            foreach ( XmlNode node in quickEditNode.SelectNodes( "*" ) )
            {
                SectionType type = (SectionType)Enum.Parse( typeof( SectionType ), node.Name );
                List<QuickEdit.QuickEditEntry> entries = new List<QuickEdit.QuickEditEntry>();
                foreach ( XmlNode fileNode in node.SelectNodes( "*" ) )
                {
                    if ( fileNode.Name == "MainFile" )
                    {
                        entries.Add(
                            new QuickEdit.QuickEditEntry
                            {
                                Guid = new Guid( fileNode.SelectSingleNode( "Guid" ).InnerText ),
                                Section = Int32.Parse( fileNode.SelectSingleNode( "Section" ).InnerText ),
                                Main = true,
                                Offset = Int32.Parse( fileNode.SelectSingleNode( "Offset" ).InnerText ),
                                Length = Int32.Parse( fileNode.SelectSingleNode( "Length" ).InnerText )
                            } );
                    }
                    else
                    {
                        entries.Add(
                            new QuickEdit.QuickEditEntry
                            {
                                Guid = new Guid( fileNode.SelectSingleNode( "Guid" ).InnerText ),
                                Section = Int32.Parse( fileNode.SelectSingleNode( "Section" ).InnerText ),
                                Main = false,
                                Offset = Int32.Parse( fileNode.SelectSingleNode( "Offset" ).InnerText )
                            } );
                    }
                }

                result[type] = entries.AsReadOnly();
            }

            return new ReadOnlyDictionary<SectionType, IList<QuickEdit.QuickEditEntry>>( result );
        }

        public static FFTText GetPspText( Stream iso )
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( Properties.Resources.psp );

            return GetText( iso, Context.US_PSP, doc, BytesFromPspIso, TextUtilities.PSPMap );
        }

        public static FFTText GetPsxText( Stream iso )
        {
            XmlDocument doc = new XmlDocument();
            doc.Load( Properties.Resources.psx );
            return GetText( iso, Context.US_PSX, doc, BytesFromPsxIso, TextUtilities.PSXMap );
        }

        private static FFTText GetText( Stream iso, Context context, XmlDocument doc, BytesFromIso reader, GenericCharMap charmap )
        {
            IDictionary<Guid, ISerializableFile> files = GetFiles( iso, context, doc, reader, charmap );
            var quickEdit = new QuickEdit( files, GetQuickEditLookup( doc.SelectSingleNode( "//QuickEdit" ) ) );
            return null;
        }

        private static IList<string> GetSectionNames( XmlNode sectionsNode )
        {
            List<string> result = new List<string>();
            foreach ( XmlNode section in sectionsNode.SelectNodes( "Section" ) )
            {
                XmlAttribute nameAttr = section.Attributes["name"];
                result.Add( nameAttr != null ? nameAttr.InnerText : "" );
            }
            return result.AsReadOnly();
        }

        private static IList<IList<int>> GetDisallowedEntries( XmlNode node, int numSections )
        {
            IList<IList<int>> result = new IList<int>[numSections];
            XmlNode disallowed = node.SelectSingleNode( "DisallowedEntries" );
            if ( disallowed != null )
            {
                foreach ( XmlNode node2 in disallowed.SelectNodes( "Section" ) )
                {
                    int sec = Int32.Parse( node2.Attributes["value"].InnerText );
                    List<int> ourResult = new List<int>();
                    foreach ( XmlNode ent in node2.SelectNodes( "entry" ) )
                    {
                        ourResult.Add( Int32.Parse( ent.InnerText ) );
                    }
                    result[sec] = ourResult.AsReadOnly();
                }
            }
            for ( int i = 0; i < result.Count; i++ )
            {
                if ( result[i] == null )
                {
                    result[i] = new int[0].AsReadOnly();
                }
            }
            return result.AsReadOnly();
        }

        private static IList<IList<string>> GetEntryNames( XmlNode sectionsNode, XmlNode templatesNode )
        {
            int sectionCount = Int32.Parse( sectionsNode.Attributes["count"].InnerText );
            IList<IList<string>> result = new IList<string>[sectionCount];

            for ( int i = 0; i < sectionCount; i++ )
            {
                XmlNode currentNode = sectionsNode.SelectSingleNode( string.Format( "Section[@value='{0}']", i ) );
                int currentCount = Int32.Parse( currentNode.Attributes["entries"].InnerText );
                XmlNode emptyNode = currentNode.Attributes["empty"];
                bool empty = emptyNode != null && Boolean.Parse( emptyNode.InnerText );
                if ( empty )
                {
                    result[i] = new string[currentCount].AsReadOnly();
                }
                else
                {
                    string[] currentSection = new string[currentCount];
                    foreach ( XmlNode entryNode in currentNode.SelectNodes( "entry" ) )
                    {
                        int index = Int32.Parse( entryNode.Attributes["value"].InnerText );
                        currentSection[index] = entryNode.Attributes["name"].InnerText;
                    }
                    foreach ( XmlNode includeNode in currentNode.SelectNodes( "include" ) )
                    {
                        XmlNode included = templatesNode.SelectSingleNode( includeNode.Attributes["name"].InnerText );
                        int start = Int32.Parse( includeNode.Attributes["start"].InnerText );
                        int end = Int32.Parse( includeNode.Attributes["end"].InnerText );
                        int offset = Int32.Parse( includeNode.Attributes["offset"].InnerText );
                        for ( int j = start; j <= end; j++ )
                        {
                            currentSection[j + offset] = included.SelectSingleNode( string.Format( "entry[@value='{0}']", j ) ).Attributes["name"].InnerText;
                        }
                    }

                    result[i] = currentSection.AsReadOnly();
                }
            }

            return result.AsReadOnly();
        }
    }
}