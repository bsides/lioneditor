using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.TextEditor
{
    abstract class AbstractFile : ISerializableFile
    {
        private bool dirty = true;
        private IList<byte> cachedBytes;

        protected AbstractFile( GenericCharMap charmap, FFTPatcher.TextEditor.FFTTextFactory.FileInfo layout, bool compressible )
        {
            NumberOfSections = layout.SectionLengths.Count;
            Layout = layout;
            CharMap = charmap;
            EntryNames = layout.EntryNames;
            SectionLengths = layout.SectionLengths.AsReadOnly();
            SectionNames = layout.SectionNames;
            DisplayName = layout.DisplayName;
            Compressible = compressible;
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
            IList<byte> bytes = GetSectionByteArrays( secs, CharMap, Compressible ).Join();

            Set<KeyValuePair<string, byte>> result = new Set<KeyValuePair<string, byte>>();

            int bytesNeeded = bytes.Count - ( Layout.Size - DataStart );

            if ( bytesNeeded <= 0 )
            {
                return result;
            }

            StringBuilder sb = new StringBuilder( Layout.Size );
            Sections.ForEach( s => s.ForEach( t => sb.Append( t ) ) );

            var dict = TextUtilities.GetPairAndTripleCounts( sb.ToString(), replacements );

            result.AddRange( currentPairs );
            TextUtilities.DoDTEEncoding( secs, FFTPatcher.Utilities.DictionaryFromKVPs( result ) );
            bytes = GetSectionByteArrays( secs, CharMap, Compressible ).Join();

            bytesNeeded = bytes.Count - ( Layout.Size - DataStart );

            if ( bytesNeeded <= 0 )
            {
                return result;
            }

            int j = 0;
            var l = new List<KeyValuePair<string, int>>( dict );
            l.Sort( ( a, b ) => b.Value.CompareTo( a.Value ) );

            while ( bytesNeeded > 0 && l.Count > 0 && dteBytes.Count > 0 )
            {
                result.Add( new KeyValuePair<string, byte>( l[0].Key, dteBytes.Pop() ) );
                TextUtilities.DoDTEEncoding( secs, FFTPatcher.Utilities.DictionaryFromKVPs( result ) );
                bytes = GetSectionByteArrays( secs, CharMap, Compressible ).Join();
                bytesNeeded = bytes.Count - ( Layout.Size - DataStart );

                if ( bytesNeeded > 0 )
                {
                    StringBuilder sb2 = new StringBuilder( Layout.Size );
                    secs.ForEach( s => s.ForEach( t => sb2.Append( t ) ) );
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

        protected static IList<byte> Compress( IList<IList<string>> strings, out IList<UInt32> offsets, GenericCharMap charmap )
        {
            TextUtilities.CompressionResult r = TextUtilities.Compress( strings, charmap, null );
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
            return Compress( strings, out offsets, CharMap );
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

        private static IList<IList<byte>> GetCompressedSectionByteArrays( IList<IList<string>> sections, GenericCharMap charmap )
        {
            IList<IList<byte>> result = new IList<byte>[sections.Count];
            IList<UInt32> offsets;
            IList<byte> compression = Compress( sections, out offsets, charmap );
            uint pos = 0;
            offsets = new List<UInt32>( offsets );
            offsets.Add( (uint)compression.Count );
            for ( int i = 0; i < sections.Count; i++ )
            {
                result[i] = compression.Sub( (int)offsets[i], (int)offsets[i + 1] - 1 );
            }
            return result;
        }

        protected static IList<IList<byte>> GetSectionByteArrays( IList<IList<string>> strings, GenericCharMap charmap, bool compressible )
        {
            if ( compressible )
            {
                return GetCompressedSectionByteArrays( strings, charmap );
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
                            result.Add( new PatchedByteArray( PspIso.Files.PSP_GAME.SYSDIR.BOOT_BIN, kvp2.Value, bytes ) );
                            result.Add( new PatchedByteArray( PspIso.Files.PSP_GAME.SYSDIR.EBOOT_BIN, kvp2.Value, bytes ) );
                            break;
                        case SectorType.FFTPack:
                            result.Add( new PatchedByteArray( (Datatypes.FFTPack.Files)kvp2.Key, kvp2.Value, bytes ) );
                            break;
                        case SectorType.Sector:
                            result.Add( new PatchedByteArray( (PsxIso.Sectors)kvp2.Key, kvp2.Value, bytes ) );
                            break;
                    }
                }
            }

            return result;
        }
    }
}
