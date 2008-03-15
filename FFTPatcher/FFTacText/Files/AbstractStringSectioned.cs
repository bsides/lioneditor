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
    public abstract class AbstractStringSectioned : IStringSectioned
    {

		#region Fields (1) 

        protected const int dataStart = 0x80;

		#endregion Fields 

		#region Abstract Properties (7) 


        protected abstract int NumberOfSections { get; }

        public abstract TextUtilities.CharMapType CharMap { get; }

        public abstract IList<IList<string>> EntryNames { get; }

        public abstract string Filename { get; }

        public abstract IDictionary<string, long> Locations { get; }

        public abstract int MaxLength { get; }

        public abstract IList<string> SectionNames { get; }


		#endregion Abstract Properties 

		#region Properties (3) 


        public int ActualLength { get { return ToFinalBytes().Count; } }

        public IList<IList<string>> Sections { get; protected set; }



        public virtual int EstimatedLength { get { return ToUncompressedBytes().Count; } }


		#endregion Properties 

		#region Constructors (2) 

        protected AbstractStringSectioned()
        {
        }

        protected AbstractStringSectioned( IList<byte> bytes )
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

                IList<byte> thisSection = bytes.Sub( (int)(start + dataStart), (int)(stop + dataStart) );
                Sections.Add( TextUtilities.ProcessList( thisSection, CharMap ) );
            }
        }

		#endregion Constructors 

		#region Methods (11) 


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
                    writer.WriteAttributeString( "xml:space", "preserve" );
                    writer.WriteString( section[j].Replace( "\r\n", @"\n" ) );
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        public XmlSchema GetSchema()
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
            IList<byte> result = ToFinalBytes();

            if( result.Count < MaxLength )
            {
                result.AddRange( new byte[MaxLength - result.Count] );
            }

            return result.ToArray();
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



        protected virtual IList<byte> ToFinalBytes()
        {
            return ToUncompressedBytes();
        }

        public virtual IList<byte> ToUncompressedBytes()
        {
            GenericCharMap map = CharMap == TextUtilities.CharMapType.PSX ?
                TextUtilities.PSXMap as GenericCharMap :
                TextUtilities.PSPMap as GenericCharMap;
            List<List<byte>> byteSections = new List<List<byte>>( NumberOfSections );
            foreach( IList<string> section in Sections )
            {
                List<byte> sectionBytes = new List<byte>();
                foreach( string s in section )
                {
                    sectionBytes.AddRange( map.StringToByteArray( s ) );
                }
                byteSections.Add( sectionBytes );
            }

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


		#endregion Methods 

    }
}