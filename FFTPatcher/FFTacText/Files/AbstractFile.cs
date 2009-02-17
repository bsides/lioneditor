using System;
using System.Collections.Generic;
using System.Text;
using PatcherLib.Datatypes;
using PatcherLib.Iso;
using PatcherLib.Utilities;

namespace FFTPatcher.TextEditor
{
    abstract class AbstractFile : ISerializableFile
    {
        private bool dirty = true;
        private IList<byte> cachedBytes;

        protected AbstractFile( GenericCharMap charmap, FFTTextFactory.FileInfo layout, IList<IList<string>> strings, bool compressible )
            : this( charmap, layout, compressible )
        {
            List<IList<string>> sections = new List<IList<string>>( NumberOfSections );
            for ( int i = 0; i < NumberOfSections; i++ )
            {
                string[] thisSection = new string[strings[i].Count];
                strings[i].CopyTo( thisSection, 0 );
                for ( int j = 0; j < thisSection.Length; j++ )
                {
                    if ( !CharMap.ValidateString( thisSection[j] ) )
                    {
                        throw new InvalidStringException( layout.Guid.ToString(), i, j, thisSection[j] );
                    }
                }
                sections.Add( thisSection );
            }

            this.Sections = sections.AsReadOnly();
        }

        protected AbstractFile( GenericCharMap charmap, FFTPatcher.TextEditor.FFTTextFactory.FileInfo layout, bool compressible )
        {
            NumberOfSections = layout.SectionLengths.Count;
            Layout = layout;
            CharMap = charmap;
            EntryNames = layout.EntryNames.AsReadOnly();
            SectionLengths = layout.SectionLengths.AsReadOnly();
            SectionNames = layout.SectionNames.AsReadOnly();
            DisplayName = layout.DisplayName;
            Compressible = compressible;
            CompressionAllowed = layout.CompressionAllowed.AsReadOnly();
            DteAllowed = layout.DteAllowed.AsReadOnly();
        }

        public static AbstractFile ConstructFile( FileType type, GenericCharMap charmap, FFTPatcher.TextEditor.FFTTextFactory.FileInfo layout, IList<byte> bytes )
        {
            switch ( type )
            {
                case FileType.CompressedFile:
                    return new SectionedFile( charmap, layout, bytes, true );
                    break;
                case FileType.SectionedFile:
                    return new SectionedFile( charmap, layout, bytes, false );
                    break;
                case FileType.OneShotFile:
                case FileType.PartitionedFile:
                    return new PartitionedFile( charmap, layout, bytes );
                    break;
            }
            return null;
        }

        public static AbstractFile ConstructFile( FileType type, GenericCharMap charmap, FFTPatcher.TextEditor.FFTTextFactory.FileInfo layout, IList<IList<string>> strings )
        {
            switch ( type )
            {
                case FileType.CompressedFile:
                    return new SectionedFile( charmap, layout, strings, true );
                    break;
                case FileType.SectionedFile:
                    return new SectionedFile( charmap, layout, strings, false );
                    break;
                case FileType.CompressibleOneShotFile:
                    return new CompressibleOneShotFile( charmap, layout, strings );
                    break;
                case FileType.OneShotFile:
                case FileType.PartitionedFile:
                    return new PartitionedFile( charmap, layout, strings );
                    break;
            }
            return null;
        }

        public virtual string this[int section, int entry]
        {
            get { return Sections[section][entry]; }
            set 
            {
                if ( section < SectionLengths.Count && 
                     entry < SectionLengths[section] && 
                     !Layout.DisallowedEntries[section].Contains( entry ) &&
                     Sections[section][entry] != value )
                {
                    dirty = true;
                    Sections[section][entry] = value;
                }
            }
        }

        public FFTPatcher.TextEditor.FFTTextFactory.FileInfo Layout { get; private set; }

        public GenericCharMap CharMap { get; private set; }

        protected IList<IList<string>> Sections { get; set; }

        public IList<IList<string>> EntryNames { get; private set; }

        public int NumberOfSections { get; private set; }

        public IList<int> SectionLengths { get; private set; }

        public IList<string> SectionNames { get; private set; }

        public IList<bool> DteAllowed { get; private set; }
        public IList<bool> CompressionAllowed { get; private set; }

        public bool Compressible { get; private set; }

        public string DisplayName { get; private set; }

        protected virtual int DataStart { get { return 0; } }

        protected abstract IList<byte> ToByteArray();
        protected abstract IList<byte> ToByteArray( IDictionary<string, byte> dteTable );

        public byte[] ToCDByteArray( IDictionary<string, byte> dteTable )
        {
            return ToByteArray( dteTable ).ToArray();
        }

        public byte[] ToCDByteArray()
        {
            if ( dirty )
            {
                cachedBytes = ToByteArray();
                dirty = false;
            }

            byte[] result = new byte[cachedBytes.Count];
            cachedBytes.CopyTo( result, 0 );
            return result;
        }

        public Set<KeyValuePair<string, byte>> GetPreferredDTEPairs( Set<string> replacements, Set<KeyValuePair<string, byte>> currentPairs, Stack<byte> dteBytes )
        {
            var secs = GetCopyOfSections();
            IList<byte> bytes = GetSectionByteArrays( secs, CharMap, CompressionAllowed ).Join();

            Set<KeyValuePair<string, byte>> result = new Set<KeyValuePair<string, byte>>();

            int bytesNeeded = bytes.Count - ( Layout.Size - DataStart );

            if ( bytesNeeded <= 0 )
            {
                return result;
            }


            result.AddRange( currentPairs );
            TextUtilities.DoDTEEncoding( secs, DteAllowed, PatcherLib.Utilities.Utilities.DictionaryFromKVPs( result ) );
            bytes = GetSectionByteArrays( secs, CharMap, CompressionAllowed ).Join();

            bytesNeeded = bytes.Count - ( Layout.Size - DataStart );

            if ( bytesNeeded <= 0 )
            {
                return result;
            }

            StringBuilder sb = new StringBuilder( Layout.Size );
            for ( int i = 0; i < secs.Count; i++ )
            {
                if ( DteAllowed[i] )
                {
                    secs[i].ForEach( t => sb.Append( t ).Append( "{0xFE}" ) );
                }
            }

            var dict = TextUtilities.GetPairAndTripleCounts( sb.ToString(), replacements );

            var l = new List<KeyValuePair<string, int>>( dict );
            l.Sort( ( a, b ) => b.Value.CompareTo( a.Value ) );

            while ( bytesNeeded > 0 && l.Count > 0 && dteBytes.Count > 0 )
            {
                result.Add( new KeyValuePair<string, byte>( l[0].Key, dteBytes.Pop() ) );
                TextUtilities.DoDTEEncoding( secs, DteAllowed, PatcherLib.Utilities.Utilities.DictionaryFromKVPs( result ) );
                bytes = GetSectionByteArrays( secs, CharMap, CompressionAllowed ).Join();
                bytesNeeded = bytes.Count - ( Layout.Size - DataStart );

                if ( bytesNeeded > 0 )
                {
                    StringBuilder sb2 = new StringBuilder( Layout.Size );
                    for ( int i = 0; i < secs.Count; i++ )
                    {
                        if ( DteAllowed[i] )
                        {
                            secs[i].ForEach( t => sb2.Append( t ).Append( "{0xFE}" ) );
                        }
                    }
                    l = new List<KeyValuePair<string, int>>( TextUtilities.GetPairAndTripleCounts( sb2.ToString(), replacements ) );
                    l.Sort( ( a, b ) => b.Value.CompareTo( a.Value ) );
                }
            }

            if ( bytesNeeded > 0 )
            {
                return null;
            }

            return result;
        }

        protected IList<IList<string>> GetCopyOfSections()
        {
            string[][] result = new string[Sections.Count][];
            for ( int i = 0; i < Sections.Count; i++ )
            {
                result[i] = new string[Sections[i].Count];
                Sections[i].CopyTo( result[i], 0 );
            }
            return result;
        }

        protected static IList<byte> Compress( IList<IList<string>> strings, out IList<UInt32> offsets, GenericCharMap charmap, IList<bool> allowedSections )
        {
            TextUtilities.CompressionResult r = TextUtilities.Compress( strings, charmap, allowedSections );
            offsets = new List<UInt32>( 32 );
            offsets.Add( 0 );
            int pos = 0;
            for ( int i = 0; i < r.SectionLengths.Count; i++ )
            {
                pos += r.SectionLengths[i];
                offsets.Add( (UInt32)pos );
            }

            return r.Bytes.AsReadOnly();
        }

        protected IList<byte> Compress( IList<IList<string>> strings, out IList<UInt32> offsets )
        {
            return Compress( strings, out offsets, CharMap, CompressionAllowed );
        }

        protected IList<byte> Compress( out IList<UInt32> offsets )
        {
            return Compress( this.Sections, out offsets );
        }

        protected IList<IList<byte>> GetUncompressedSectionBytes()
        {
            return GetUncompressedSectionBytes( Sections, CharMap );
        }

        protected static IList<IList<byte>> GetUncompressedSectionBytes( IList<IList<string>> strings, GenericCharMap charmap )
        {
            IList<IList<byte>> result = new List<IList<byte>>( strings.Count );
            foreach ( IList<string> section in strings )
            {
                List<byte> bytes = new List<byte>();
                section.ForEach( s => bytes.AddRange( charmap.StringToByteArray( s ) ) );
                result.Add( bytes.AsReadOnly() );
            }
            return result.AsReadOnly();

        }

        private static IList<IList<byte>> GetCompressedSectionByteArrays( IList<IList<string>> sections, GenericCharMap charmap, IList<bool> compressibleSections )
        {
            IList<IList<byte>> result = new IList<byte>[sections.Count];
            IList<UInt32> offsets;
            IList<byte> compression = Compress( sections, out offsets, charmap, compressibleSections );
            uint pos = 0;
            offsets = new List<UInt32>( offsets );
            offsets.Add( (uint)compression.Count );
            for ( int i = 0; i < sections.Count; i++ )
            {
                result[i] = compression.Sub( (int)offsets[i], (int)offsets[i + 1] - 1 );
            }
            return result;
        }

        protected static IList<IList<byte>> GetSectionByteArrays( IList<IList<string>> strings, GenericCharMap charmap, IList<bool> compressibleSections )
        {
            if ( compressibleSections.Contains( true ) )
            {
                return GetCompressedSectionByteArrays( strings, charmap, compressibleSections );
            }
            else
            {
                return GetUncompressedSectionBytes( strings, charmap );
            }
        }

        protected IList<byte> Compress( IDictionary<string, byte> dteTable, out IList<UInt32> offsets )
        {
            return Compress( GetDteStrings( dteTable ), out offsets );
        }

        protected IList<IList<string>> GetDteStrings( IDictionary<string, byte> dteTable )
        {
            IList<IList<string>> result = new List<IList<string>>( Sections.Count );
            foreach ( IList<string> sec in Sections )
            {
                IList<string> s = new List<string>( sec );
                TextUtilities.DoDTEEncoding( s, dteTable );
                result.Add( s.AsReadOnly() );
            }

            return result.AsReadOnly();
        }

        public bool IsDteNeeded()
        {
            return ToCDByteArray().Length > Layout.Size;
        }

        public IList<PatchedByteArray> GetNonDtePatches()
        {
            return GetPatches( ToCDByteArray() );
        }

        public IList<PatchedByteArray> GetDtePatches( IDictionary<string, byte> dteBytes )
        {
            return GetPatches( ToCDByteArray( dteBytes ) );
        }

        private IList<PatchedByteArray> GetPatches( byte[] bytes )
        {
            List<PatchedByteArray> result = new List<PatchedByteArray>();
            foreach ( var kvp in Layout.Sectors )
            {
                SectorType type = kvp.Key;
                foreach ( var kvp2 in kvp.Value )
                {
                    switch ( type )
                    {
                        case SectorType.BootBin:
                            result.Add( new PatchedByteArray( PatcherLib.Iso.PspIso.Files.PSP_GAME.SYSDIR.BOOT_BIN, kvp2.Value, bytes ) );
                            result.Add( new PatchedByteArray( PatcherLib.Iso.PspIso.Files.PSP_GAME.SYSDIR.EBOOT_BIN, kvp2.Value, bytes ) );
                            break;
                        case SectorType.FFTPack:
                            result.Add( new PatchedByteArray( (FFTPack.Files)kvp2.Key, kvp2.Value, bytes ) );
                            break;
                        case SectorType.Sector:
                            result.Add( new PatchedByteArray( (PatcherLib.Iso.PsxIso.Sectors)kvp2.Key, kvp2.Value, bytes ) );
                            break;
                    }
                }
            }

            return result;
        }
    }
}
