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
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace FFTPatcher.TextEditor.Files
{
    /// <summary>
    /// A file that is divided into sections of variable length.
    /// </summary>
    public abstract class AbstractStringSectioned : IStringSectioned
    {

        #region Fields (4)

        /// <summary>
        /// The location where the data starts in normal sectioned files.
        /// </summary>
        protected const int dataStart = 0x80;
        private int[][] entryLengths = null;
        private IList<IList<string>> entryNames;
        private IList<string> sectionNames;

        #endregion Fields

        #region Abstract Properties (5)


        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected abstract int NumberOfSections { get; }

        /// <summary>
        /// Gets the character map that is used for this file.
        /// </summary>
        public abstract GenericCharMap CharMap { get; }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        public abstract string Filename { get; }

        /// <summary>
        /// Gets the filenames and locations for this file.
        /// </summary>
        public abstract IDictionary<string, long> Locations { get; }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        public abstract int MaxLength { get; }


        #endregion Abstract Properties

        #region Properties (7)


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
                    entryNames = Files.EntryNames.GetEntryNames( GetType() );
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
                    sectionNames = Files.EntryNames.GetSectionNames( GetType() );
                }
                return sectionNames;
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


        #endregion Properties

        #region Constructors (2)

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
                Sections.Add( TextUtilities.ProcessList( thisSection, CharMap ) );
            }
        }

        #endregion Constructors

        #region Methods (15)


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

            Sections = new string[sectionArray.Length][];

            for( int i = 0; i < sectionArray.Length; i++ )
            {
                Sections[i] = sectionArray[i].Split( '\u2800' );
            }
            reader.ReadEndElement();
        }

        private void ReadXmlUncompressed( XmlReader reader )
        {
            reader.MoveToAttribute( "sections" );
            int sectionCount = reader.ReadContentAsInt();
            reader.MoveToElement();
            reader.ReadStartElement();
            Sections = new string[sectionCount][];

            for( int i = 0; i < sectionCount; i++ )
            {
                reader.MoveToAttribute( "entries" );
                int entryCount = reader.ReadContentAsInt();
                reader.MoveToElement();
                reader.MoveToAttribute( "value" );
                int currentSection = reader.ReadContentAsInt();
                reader.MoveToElement();
                reader.ReadStartElement( "section" );

                Sections[currentSection] = new string[entryCount];

                for( int j = 0; j < entryCount; j++ )
                {
                    reader.MoveToAttribute( "value" );
                    int currentEntry = reader.ReadContentAsInt();
                    reader.MoveToElement();
                    reader.ReadStartElement( "entry" );
                    Sections[currentSection][currentEntry] = reader.ReadString().Replace( @"\n", "\r\n" );
                    reader.ReadEndElement();
                }

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
        /// Gets other patches necessary to make modifications to this file functional.
        /// </summary>
        public IList<PatchedByteArray> GetOtherPatches()
        {
            return new PatchedByteArray[0];
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
        public IList<IList<byte>> GetSectionByteArrays()
        {
            IList<IList<byte>> result = new List<IList<byte>>( NumberOfSections );

            foreach( IList<string> section in Sections )
            {
                List<byte> sectionBytes = new List<byte>();
                foreach( string s in section )
                {
                    sectionBytes.AddRange( CharMap.StringToByteArray( s ) );
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
        public byte[] ToByteArray()
        {
            IList<byte> result = ToFinalBytes();

            if( result.Count < MaxLength )
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

        /// <summary>
        /// Creates a collection of bytes representing the uncompressed contents of this file.
        /// </summary>
        public virtual IList<byte> ToUncompressedBytes()
        {
            IList<IList<byte>> byteSections = GetSectionByteArrays();

            List<byte> result = new List<byte>();
            result.AddRange( new byte[] { 0x00, 0x00, 0x00, 0x00 } );
            int old = 0;
            for( int i = 0; i < NumberOfSections - 1; i++ )
            {
                result.AddRange( ((UInt32)(byteSections[i].Count + old)).ToBytes() );
                old += byteSections[i].Count;
            }
            result.AddRange( new byte[dataStart - NumberOfSections * 4] );

            foreach( List<byte> bytes in byteSections )
            {
                result.AddRange( bytes );
            }

            return result;
        }

        /// <summary>
        /// Gets a list of indices for named sections.
        /// </summary>
        public virtual IList<NamedSection> GetNamedSections()
        {
            return new List<NamedSection>();
        }

        #endregion Methods

    }
}