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

namespace FFTPatcher.TextEditor.Files
{
    /// <summary>
    /// A file with many partitions of equal length.
    /// </summary>
    public abstract class AbstractPartitionedFile : AbstractFile, IPartitionedFile
    {

		#region Abstract Properties (7) 


        /// <summary>
        /// Gets a collection of lists of strings, each string being a description of an entry in this file.
        /// </summary>
        public abstract IList<IList<string>> EntryNames { get; }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        public abstract string Filename { get; }

        /// <summary>
        /// Gets the number of sections in this file.
        /// </summary>
        public abstract int NumberOfSections { get; }

        /// <summary>
        /// Gets the length of every section in this file.
        /// </summary>
        public abstract int SectionLength { get; }

        /// <summary>
        /// Gets a collection of strings with a description of each section in this file.
        /// </summary>
        public abstract IList<string> SectionNames { get; }


		#endregion Abstract Properties 

		#region Properties (4) 


        /// <summary>
        /// Gets the actual length.
        /// </summary>
        public int ActualLength
        {
            get { return ToFinalBytes().Count; }
        }

        /// <summary>
        /// Gets the estimated length of this file.
        /// </summary>
        public int EstimatedLength
        {
            get { return ToFinalBytes().Count; }
        }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        public override int MaxLength { get { return NumberOfSections * SectionLength; } }

        /// <summary>
        /// Gets a collection of <see cref="IPartition"/>, representing the entries in this file.
        /// </summary>
        public IList<IPartition> Sections { get; protected set; }


		#endregion Properties 

		#region Constructors (2) 

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractPartitionedFile"/> class.
        /// </summary>
        protected AbstractPartitionedFile()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractPartitionedFile"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        protected AbstractPartitionedFile( IList<byte> bytes )
        {
            Sections = new List<IPartition>( NumberOfSections );
            for( int i = 0; i < NumberOfSections; i++ )
            {
                Sections.Add( new FilePartition( this, bytes.Sub( i * SectionLength, (i + 1) * SectionLength - 1 ), EntryNames[i], SectionLength, CharMap ) );
            }
        }

		#endregion Constructors 

		#region Methods (11) 


        private void ReadXmlBase64( XmlReader reader )
        {
            reader.ReadStartElement();
            string s = Encoding.UTF8.GetString( GZip.Decompress( Convert.FromBase64String( reader.ReadString() ) ) );
            string[] sectionArray = s.Split( '\u2801' );

            Sections = new IPartition[sectionArray.Length];

            for( int i = 0; i < sectionArray.Length; i++ )
            {
                string[] entries = sectionArray[i].Split( '\u2800' );
                Sections[i] = new FilePartition( this, entries, SectionLength, EntryNames[i], CharMap );
            }

            reader.ReadEndElement();
        }

        private void ReadXmlUncompressed( XmlReader reader )
        {
            reader.MoveToAttribute( "sections" );
            int sectionCount = reader.ReadContentAsInt();
            reader.MoveToElement();
            reader.ReadStartElement();
            Sections = new IPartition[sectionCount];

            for( int i = 0; i < sectionCount; i++ )
            {
                reader.MoveToAttribute( "entries" );
                int entryCount = reader.ReadContentAsInt();
                reader.MoveToElement();
                reader.MoveToAttribute( "value" );
                int currentSection = reader.ReadContentAsInt();
                reader.MoveToElement();
                reader.ReadStartElement( "section" );

                string[] currentStrings = new string[entryCount];

                for( int j = 0; j < entryCount; j++ )
                {
                    reader.MoveToAttribute( "value" );
                    int currentEntry = reader.ReadContentAsInt();
                    reader.MoveToElement();
                    reader.ReadStartElement( "entry" );
                    currentStrings[currentEntry] = reader.ReadString().Replace( @"\n", "\r\n" );
                    reader.ReadEndElement();
                }

                Sections[currentSection] = new FilePartition( this, currentStrings, SectionLength, EntryNames[currentSection], CharMap );

                reader.ReadEndElement();
            }

            reader.ReadEndElement();
        }

        private void WriteXmlBase64( XmlWriter writer )
        {
            writer.WriteAttributeString( "compressed", "true" );

            StringBuilder sb = new StringBuilder();
            foreach( IPartition section in Sections )
            {
                foreach( string entry in section.Entries )
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
                IPartition section = Sections[i];

                writer.WriteStartElement( "section" );
                writer.WriteAttributeString( "value", i.ToString() );
                writer.WriteAttributeString( "entries", section.Entries.Count.ToString() );

                for( int j = 0; j < section.Entries.Count; j++ )
                {
                    writer.WriteStartElement( "entry" );
                    writer.WriteAttributeString( "value", j.ToString() );
                    writer.WriteString( section.Entries[j].Replace( "\r\n", @"\n" ) );
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Gets a list of bytes that represent this file in its on-disc form.
        /// </summary>
        protected IList<byte> ToFinalBytes()
        {
            List<byte> result = new List<byte>();
            foreach( IPartition section in Sections )
            {
                result.AddRange( section.ToByteArray() );
            }

            return result;
        }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
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
        /// <returns></returns>
        public override byte[] ToByteArray()
        {
            return ToFinalBytes().ToArray();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
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

        public override IDictionary<string, int> CalculateBytesSaved( Set<string> replacements )
        {
            System.Diagnostics.Debug.Print( "FIX THIS" );

            Dictionary<string, int> result = new Dictionary<string, int>( replacements.Count );

            replacements.ForEach( s => result[s] = 0 );

            return result;
        }

        public override bool IsDTENeeded()
        {
            return false;
        }

        public override Set<KeyValuePair<string, byte>> GetPreferredDTEPairs( Set<string> replacements, Set<KeyValuePair<string, byte>> currentPairs, Stack<byte> dteBytes )
        {
            return new Set<KeyValuePair<string, byte>>();
        }

        public override byte[] ToByteArray( IDictionary<string, byte> dteTable )
        {
            return ToByteArray();
        }

		#endregion Methods 

    }
}
