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

namespace FFTPatcher.TextEditor.Files
{
    public abstract class AbstractPartitionedFile : IPartitionedFile
    {

		#region Abstract Properties (7) 


        protected abstract TextUtilities.CharMapType CharMap { get; }

        public abstract IList<IList<string>> EntryNames { get; }

        public abstract string Filename { get; }

        public abstract IDictionary<string, long> Locations { get; }

        public abstract int NumberOfSections { get; }

        public abstract int SectionLength { get; }

        public abstract IList<string> SectionNames { get; }


		#endregion Abstract Properties 

		#region Properties (3) 


        public int ActualLength
        {
            get { return ToFinalBytes().Count; }
        }

        public int EstimatedLength
        {
            get { return ToFinalBytes().Count; }
        }

        public IList<IPartition> Sections { get; protected set; }


		#endregion Properties 

		#region Constructors (2) 

        protected AbstractPartitionedFile()
        {
        }

        protected AbstractPartitionedFile( IList<byte> bytes )
        {
            Sections = new List<IPartition>( NumberOfSections );
            for( int i = 0; i < NumberOfSections; i++ )
            {
                Sections.Add( new FilePartition( bytes.Sub( i * SectionLength, (i + 1) * SectionLength - 1 ), EntryNames[i], CharMap ) );
            }
        }

		#endregion Constructors 

		#region Methods (10) 


        private void ReadXmlBase64( XmlReader reader )
        {
            reader.ReadStartElement();
            string s = Encoding.UTF8.GetString( GZip.Decompress( Convert.FromBase64String( reader.ReadString() ) ) );
            string[] sectionArray = s.Split( '\u2801' );

            Sections = new IPartition[sectionArray.Length];

            for( int i = 0; i < sectionArray.Length; i++ )
            {
                string[] entries = sectionArray[i].Split( '\u2800' );
                Sections[i] = new FilePartition( entries, SectionLength, EntryNames[i], CharMap );
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
                    currentStrings[currentEntry] = reader.ReadString().Replace( "@\n", "\r\n" );
                    reader.ReadEndElement();
                }

                Sections[currentSection] = new FilePartition( currentStrings, currentStrings.Length, EntryNames[currentSection], CharMap );

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
                    writer.WriteAttributeString( "xml:space", "preserve" );
                    writer.WriteString( section.Entries[j].Replace( "\r\n", @"\n" ) );
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        protected IList<byte> ToFinalBytes()
        {
            List<byte> result = new List<byte>();
            foreach( IPartition section in Sections )
            {
                result.AddRange( section.ToByteArray() );
            }

            return result;
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

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

        public byte[] ToByteArray()
        {
            return ToFinalBytes().ToArray();
        }

        public void WriteXml( XmlWriter writer )
        {
            WriteXmlUncompressed( writer );
        }

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


		#endregion Methods 

    }
}
