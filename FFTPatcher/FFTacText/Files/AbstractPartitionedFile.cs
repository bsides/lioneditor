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

using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace FFTPatcher.TextEditor.Files
{
    public abstract class AbstractPartitionedFile : IPartitionedFile
    {

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

		#region Methods (5) 


        protected IList<byte> ToFinalBytes()
        {
            List<byte> result = new List<byte>();
            foreach( IPartition section in Sections )
            {
                result.AddRange( section.ToByteArray() );
            }

            return result;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml( XmlReader reader )
        {
            reader.MoveToAttribute( "sections" );
            int numberOfSections = reader.ReadContentAsInt();
            reader.MoveToElement();
            reader.ReadStartElement();

            Sections = new IPartition[numberOfSections];

            for( int i = 0; i < numberOfSections; i++ )
            {
                reader.MoveToAttribute( "entries" );
                int numberOfEntries = reader.ReadContentAsInt();
                reader.MoveToElement();
                reader.MoveToAttribute( "value" );
                int index = reader.ReadContentAsInt();
                reader.MoveToElement();
                reader.ReadStartElement( "section" );

                string[] entries = new string[numberOfEntries];

                for( int j = 0; j < numberOfEntries; j++ )
                {
                    reader.MoveToAttribute( "value" );
                    int entryIndex = reader.ReadContentAsInt();
                    reader.MoveToElement();
                    reader.ReadStartElement( "entry" );
                    entries[entryIndex] = reader.ReadString();
                    reader.ReadEndElement();
                }

                Sections[index] = new FilePartition( entries, SectionLength, EntryNames[index], CharMap );
                reader.ReadEndElement();
            }

            reader.ReadEndElement();
        }

        public byte[] ToByteArray()
        {
            return ToFinalBytes().ToArray();
        }

        public void WriteXml( XmlWriter writer )
        {
            writer.WriteAttributeString( "sections", Sections.Count.ToString() );
            for( int i = 0; i < Sections.Count; i++ )
            {
                writer.WriteStartElement( "section" );
                writer.WriteAttributeString( "value", i.ToString() );
                writer.WriteAttributeString( "entries", Sections[i].Entries.Count.ToString() );
                for( int j = 0; j < Sections[i].Entries.Count; j++ )
                {
                    writer.WriteStartElement( "entry" );
                    writer.WriteAttributeString( "value", j.ToString() );
                    writer.WriteString( Sections[i].Entries[j] );
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }


		#endregion Methods 
        protected abstract TextUtilities.CharMapType CharMap { get; }
        public abstract IList<IList<string>> EntryNames { get; }
        public abstract IDictionary<string, long> Locations { get; }
        public abstract int NumberOfSections { get; }
        public abstract int SectionLength { get; }
        public abstract IList<string> SectionNames { get; }

    }
}
