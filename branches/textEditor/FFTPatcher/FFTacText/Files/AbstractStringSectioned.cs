/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace FFTPatcher.TextEditor.Files
{
    /// <summary>
    /// A file that is divided into sections of variable length.
    /// </summary>
    public abstract class AbstractStringSectioned : AbstractFile, IStringSectioned
    {
        /// <summary>
        /// The location where the data starts in normal sectioned files.
        /// </summary>
        protected const int dataStart = 0x80;
        private int[][] entryLengths = null;
        private IList<IList<string>> entryNames;
        private IList<string> sectionNames;

        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected abstract int NumberOfSections { get; }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        public abstract string Filename { get; }

        private int[][] EntryLengths
        {
            get
            {
                if( entryLengths == null )
                {
                    InitializeEntryLengths();
                }

                return entryLengths;
            }
        }

        /// <summary>
        /// Gets the actual length of this file if it were turned into a byte array.
        /// </summary>
        public int ActualLength { get { return ToFinalBytes().Count; } }

        /// <summary>
        /// Gets a collection of lists of strings, each string being a description of an entry in this file.
        /// </summary>
        public IList<IList<string>> EntryNames
        {
            get
            {
                if( entryNames == null )
                {
                    entryNames = TextEditor.EntryNames.GetEntryNames( GetType() );
                }

                return entryNames;
            }
        }

        /// <summary>
        /// Gets a collection of strings with a description of each section in this file.
        /// </summary>
        public IList<string> SectionNames
        {
            get
            {
                if( sectionNames == null )
                {
                    sectionNames = TextEditor.EntryNames.GetSectionNames( GetType() );
                }
                return sectionNames;
            }
        }

        protected bool SectionsDirty
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the estimated length of this file if it were turned into a byte array.
        /// </summary>
        public virtual int EstimatedLength
        {
            get
            {
                int sum = 0;
                foreach( int[] i in EntryLengths )
                {
                    sum += i.Sum();
                }
                return sum + dataStart;
            }
        }

        /// <summary>
        /// Gets a collection of lists of strings, representing the entries in this file..
        /// </summary>
        public virtual IList<IList<string>> Sections { get; protected set; }

        /// <summary>
        /// Gets or sets a specific entry in a specific section.
        /// </summary>
        /// <param name="section">The section in which contains the entry to get or set</param>
        /// <param name="entry">The specific entry to get or set</param>
        public virtual string this[int section, int entry]
        {
            get { return Sections[section][entry]; }
            set
            {
                EntryLengths[section][entry] = CharMap.StringToByteArray( value ).Length;
                Sections[section][entry] = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractStringSectioned"/> class.
        /// </summary>
        protected AbstractStringSectioned()
        {
            Sections = new List<IList<string>>( NumberOfSections );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractStringSectioned"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        protected AbstractStringSectioned( IList<byte> bytes )
            : this()
        {
            for( int i = 0; i < NumberOfSections; i++ )
            {
                uint start = Utilities.BytesToUInt32( bytes.Sub( i * 4, i * 4 + 3 ) );
                uint stop = Utilities.BytesToUInt32( bytes.Sub( (i + 1) * 4, (i + 1) * 4 + 3 ) ) - 1;
                if( i == NumberOfSections - 1 )
                {
                    stop = (uint)bytes.Count - 1 - dataStart;
                }

                IList<byte> thisSection = bytes.Sub( (int)(start + dataStart), (int)(stop + dataStart) );
                NotifyStringList l = new NotifyStringList( TextUtilities.ProcessList( thisSection, CharMap ) );
                l.ListMemberChanged += OnStringListChanged;
                Sections.Add( l );
            }
        }

        protected virtual void OnStringListChanged( object sender, NotifyStringList.ListMemberChangedEventArgs args )
        {
            SectionsDirty = true;
        }

        private void InitializeEntryLengths()
        {
            entryLengths = new int[NumberOfSections][];

            for( int i = 0; i < NumberOfSections; i++ )
            {
                IList<string> section = Sections[i];
                entryLengths[i] = new int[section.Count];
                for( int j = 0; j < section.Count; j++ )
                {
                    entryLengths[i][j] = CharMap.StringToByteArray( this[i, j] ).Length;
                }
            }
        }

        private void ReadXmlBase64( XmlReader reader )
        {
            reader.ReadStartElement();
            string s = Encoding.UTF8.GetString( GZip.Decompress( Convert.FromBase64String( reader.ReadString() ) ) );
            string[] sectionArray = s.Split( '\u2801' );

            Sections = new IList<string>[sectionArray.Length];

            for( int i = 0; i < sectionArray.Length; i++ )
            {
                NotifyStringList l = new NotifyStringList(sectionArray[i].Split( '\u2800' ) );
                l.ListMemberChanged += OnStringListChanged;
                Sections[i] = l;
            }
            reader.ReadEndElement();
        }

        private void ReadXmlUncompressed( XmlReader reader )
        {
            reader.MoveToAttribute( "sections" );
            int sectionCount = reader.ReadContentAsInt();
            reader.MoveToElement();
            reader.ReadStartElement();
            Sections = new IList<string>[sectionCount];

            for( int i = 0; i < sectionCount; i++ )
            {
                reader.MoveToAttribute( "entries" );
                int entryCount = reader.ReadContentAsInt();
                reader.MoveToElement();
                reader.MoveToAttribute( "value" );
                int currentSection = reader.ReadContentAsInt();
                reader.MoveToElement();
                reader.ReadStartElement( "section" );

                IList<string> whatever = new string[entryCount];

                for( int j = 0; j < entryCount; j++ )
                {
                    reader.MoveToAttribute( "value" );
                    int currentEntry = reader.ReadContentAsInt();
                    reader.MoveToElement();
                    reader.ReadStartElement( "entry" );
                    whatever[currentEntry] = reader.ReadString().Replace( @"\n", "\r\n" );
                    reader.ReadEndElement();
                }

                NotifyStringList l = new NotifyStringList( whatever );
                l.ListMemberChanged += OnStringListChanged;
                Sections[currentSection] = l;

                reader.ReadEndElement();
            }

            reader.ReadEndElement();
        }

        private void WriteXmlBase64( XmlWriter writer )
        {
            writer.WriteAttributeString( "compressed", "true" );
            StringBuilder sb = new StringBuilder();
            foreach( IList<string> section in Sections )
            {
                foreach( string entry in section )
                {
                    sb.Append( entry );
                    sb.Append( "\u2800" );
                }
                sb.Remove( sb.Length - 1, 1 );
                sb.Append( "\u2801" );
            }
            sb.Remove( sb.Length - 1, 1 );
            writer.WriteString( Utilities.GetPrettyBase64( GZip.Compress( Encoding.UTF8.GetBytes( sb.ToString() ) ) ) );
        }

        private void WriteXmlUncompressed( XmlWriter writer )
        {
            writer.WriteAttributeString( "compressed", "false" );
            writer.WriteAttributeString( "sections", Sections.Count.ToString() );

            for( int i = 0; i < Sections.Count; i++ )
            {
                IList<string> section = Sections[i];

                writer.WriteStartElement( "section" );
                writer.WriteAttributeString( "value", i.ToString() );
                writer.WriteAttributeString( "entries", section.Count.ToString() );

                for( int j = 0; j < section.Count; j++ )
                {
                    writer.WriteStartElement( "entry" );
                    writer.WriteAttributeString( "value", j.ToString() );
                    writer.WriteString( section[j].Replace( "\r\n", @"\n" ) );
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Gets the length in bytes of a specific entry.
        /// </summary>
        /// <param name="section">The section which contains the entry whose length is needed.</param>
        /// <param name="entry">The specific entry whose length is needed.</param>
        /// <returns>The length of the entry, in bytes.</returns>
        public int GetEntryLength( int section, int entry )
        {
            return EntryLengths[section][entry];
        }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Gets the byte arrays for each section.
        /// </summary>
        public IList<IList<byte>> GetSectionByteArraysUncompressed()
        {
            return GetSectionByteArraysUncompressed( Sections, CharMap );
        }

        public virtual IList<IList<byte>> GetSectionByteArrays( IList<IList<string>> sections, GenericCharMap charmap )
        {
            return GetSectionByteArraysUncompressed( sections, charmap );
        }

        public virtual IList<IList<byte>> GetSectionByteArrays()
        {
            return GetSectionByteArraysUncompressed();
        }

        public IList<IList<byte>> GetSectionByteArraysUncompressed( IList<IList<string>> sections, GenericCharMap charmap )
        {
            IList<IList<byte>> result = new List<IList<byte>>( sections.Count );
            foreach( IList<string> section in sections )
            {
                List<byte> sectionBytes = new List<byte>();
                foreach( string s in section )
                {
                    sectionBytes.AddRange( charmap.StringToByteArray( s ) );
                }

                result.Add( sectionBytes );
            }

            return result;
        }


        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
        public void ReadXml( XmlReader reader )
        {
            bool b = reader.MoveToAttribute( "compressed" );
            bool compressed = reader.ReadContentAsBoolean();
            reader.MoveToElement();
            if( compressed )
            {
                ReadXmlBase64( reader );
            }
            else
            {
                ReadXmlUncompressed( reader );
            }
        }

        /// <summary>
        /// Creates a byte array representing this file.
        /// </summary>
        public override byte[] ToByteArray()
        {
            IList<byte> result = new List<byte>( ToFinalBytes() );

            if( result.Count < MaxLength )
            {
                result.AddRange( new byte[MaxLength - result.Count] );
            }

            return result.ToArray();
        }

        public override byte[] ToByteArray( IDictionary<string, byte> dteTable )
        {
            IList<byte> result = new List<byte>( ToFinalBytes( dteTable ) );
            if ( result.Count < MaxLength )
            {
                result.AddRange( new byte[MaxLength - result.Count] );
            }

            return result.ToArray();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        public void WriteXml( XmlWriter writer )
        {
            WriteXmlUncompressed( writer );
        }

        /// <summary>
        /// Serializes this file to an XML node.
        /// </summary>
        /// <param name="writer">The writer to use to write the node</param>
        /// <param name="compressed">Whether or not this object's data should be compressed.</param>
        public void WriteXml( XmlWriter writer, bool compressed )
        {
            if( compressed )
            {
                WriteXmlBase64( writer );
            }
            else
            {
                WriteXmlUncompressed( writer );
            }
        }



        /// <summary>
        /// Gets a list of bytes that represent this file in its on-disc form.
        /// </summary>
        protected virtual IList<byte> ToFinalBytes()
        {
            return ToUncompressedBytes();
        }

        protected virtual IList<byte> ToFinalBytes( IDictionary<string, byte> dteTable )
        {
            return ToUncompressedBytes( dteTable );
        }

        /// <summary>
        /// Gets a list of indices for named sections.
        /// </summary>
        public virtual IList<NamedSection> GetNamedSections()
        {
            return new List<NamedSection>();
        }

        public virtual IList<byte> ToUncompressedBytes( IDictionary<string, byte> dteTable )
        {
            IList<IList<string>> strings = new List<IList<string>>( Sections.Count );
            foreach ( IList<string> sec in Sections )
            {
                IList<string> s = new List<string>( sec.Count );
                s.AddRange( sec );

                TextUtilities.DoDTEEncoding( s, dteTable );

                strings.Add( s );
            }

            return ToUncompressedBytes( GetSectionByteArraysUncompressed( strings, CharMap ) );
        }

        private static IList<byte> ToUncompressedBytes( IList<IList<byte>> byteSections )
        {
            int numberOfSections = byteSections.Count;
            List<byte> result = new List<byte>();
            result.AddRange( new byte[] { 0x00, 0x00, 0x00, 0x00 } );
            int old = 0;

            for ( int i = 0; i < numberOfSections - 1; i++ )
            {
                result.AddRange( ( (UInt32)( byteSections[i].Count + old ) ).ToBytes() );
                old += byteSections[i].Count;
            }
            
            result.AddRange( new byte[dataStart - numberOfSections * 4] );

            foreach ( List<byte> bytes in byteSections )
            {
                result.AddRange( bytes );
            }

            return result;
        }

        /// <summary>
        /// Creates a collection of bytes representing the uncompressed contents of this file.
        /// </summary>
        public virtual IList<byte> ToUncompressedBytes()
        {
            return ToUncompressedBytes( GetSectionByteArraysUncompressed() );
        }

        public override IDictionary<string, int> CalculateBytesSaved( Set<string> replacements )
        {
            StringBuilder virgin = new StringBuilder( MaxLength );
            Sections.ForEach( s => s.ForEach( t => virgin.Append( t ) ) );
            string virginString = virgin.ToString();

            return TextUtilities.GetPairAndTripleCounts( virginString, replacements );
        }

        public override bool IsDTENeeded()
        {
            return EstimatedLength >= MaxLength;
        }

        protected IList<IList<string>> GetCopyOfSections()
        {
            string[][] result = new string[Sections.Count][];
            for( int i = 0; i < Sections.Count; i++ )
            {
                result[i] = new string[Sections[i].Count];
                Sections[i].CopyTo( result[i], 0 );
            }
            return result;
        }

        public override Set<KeyValuePair<string, byte>> GetPreferredDTEPairs( Set<string> replacements, Set<KeyValuePair<string, byte>> currentPairs, Stack<byte> dteBytes )
        {
            IList<IList<string>> sections = GetCopyOfSections();
            IList<byte> bytes = GetSectionByteArrays( sections, CharMap ).Join();

            Set<KeyValuePair<string, byte>> result = new Set<KeyValuePair<string, byte>>();
            
            StringBuilder sb = new StringBuilder( MaxLength );
            Sections.ForEach( s => s.ForEach( t => sb.Append( t ) ) );

            var dict = TextUtilities.GetPairAndTripleCounts( sb.ToString(), replacements );

            int bytesNeeded = bytes.Count - (MaxLength - dataStart);

            int j = 0;


            while ( bytesNeeded > 0 && j < currentPairs.Count )
            {
                if ( dict.ContainsKey( currentPairs[j].Key ) )
                {
                    result.Add( currentPairs[j] );

                    TextUtilities.DoDTEEncoding( sections, FFTPatcher.Utilities.DictionaryFromKVPs( result ) );
                    bytes = GetSectionByteArrays( sections, CharMap ).Join();

                    bytesNeeded = bytes.Count - ( MaxLength - dataStart );
                }
                j++;
            }

            j = 0;

            var l = new List<KeyValuePair<string, int>>( dict );
            l.Sort( ( a, b ) => b.Value.CompareTo( a.Value ) );

            while ( bytesNeeded > 0 && l.Count > 0 && dteBytes.Count > 0 )
            {
                result.Add( new KeyValuePair<string, byte>( l[0].Key, dteBytes.Pop() ) );
                TextUtilities.DoDTEEncoding( sections, FFTPatcher.Utilities.DictionaryFromKVPs( result ) );
                bytes = GetSectionByteArrays( sections, CharMap ).Join();
                bytesNeeded = bytes.Count - (MaxLength - dataStart);

                StringBuilder sb2 = new StringBuilder( MaxLength );
                sections.ForEach( s => s.ForEach( t => sb2.Append( t ) ) );
                l = new List<KeyValuePair<string, int>>( TextUtilities.GetPairAndTripleCounts( sb2.ToString(), replacements ) );
                l.Sort( ( a, b ) => b.Value.CompareTo( a.Value ) );
            }

            if ( bytesNeeded > 0 )
            {
                return null;
            }

            return result;
        }

 

    }
}