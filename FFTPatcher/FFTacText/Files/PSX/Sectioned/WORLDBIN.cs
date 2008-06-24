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

namespace FFTPatcher.TextEditor.Files.PSX
{
    /// <summary>
    /// Represents the text in the WORLD.BIN file.
    /// </summary>
    public class WORLDBIN : IStringSectioned
    {

		#region Fields (6) 

        private const UInt32 baseAddress = 0x8018E4E8;
        private int[][] entryLengths = null;
        private IList<IList<string>> entryNames;
        private const string filename = "WORLD.BIN";
        private static IDictionary<string, long> locations;
        private IList<string> sectionNames;

		#endregion Fields 

		#region Constructors (2) 

        /// <summary>
        /// Initializes a new instance of the <see cref="WORLDBIN"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        public WORLDBIN( IList<byte> bytes )
            : this()
        {
            IList<UInt32> offsets = GetOffsets( bytes.Sub( 0x5734 ) );

            for( int i = 0; i < 8; i++ )
            {
                IList<byte> thisSection;
                if( i == NumberOfSections - 1 )
                {
                    thisSection = bytes.Sub( (int)offsets[i], bytes.Count - 0x24 - 1 );
                }
                else
                {
                    thisSection = bytes.Sub( (int)offsets[i], (int)offsets[i + 1] - 1 );
                }

                Sections.Add( TextUtilities.ProcessList( thisSection, CharMap ) );
            }

            entryLengths = new int[NumberOfSections][];

            for( int i = 0; i < NumberOfSections; i++ )
            {
                IList<string> section = Sections[i];
                entryLengths[i] = new int[section.Count];
                for( int j = 0; j < section.Count; i++ )
                {
                    entryLengths[i][j] = CharMap.StringToByteArray( this[i, j] ).Length;
                }
            }
        }

        private WORLDBIN()
        {
            Sections = new List<IList<string>>( NumberOfSections );
        }

		#endregion Constructors 

		#region Properties (12) 

        /// <summary>
        /// Gets the actual length of this file if it were turned into a byte array.
        /// </summary>
        /// <value>The actual length.</value>
        public int ActualLength { get { return ToFinalBytes().Count; } }

        /// <summary>
        /// Gets the character map used for this file.
        /// </summary>
        public GenericCharMap CharMap { get { return TextUtilities.PSXMap; } }

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
        /// Gets a collection of lists of strings, each string being a description of an entry in this file.
        /// </summary>
        /// <value></value>
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
        /// Gets the estimated length of this file if it were turned into a byte array.
        /// </summary>
        /// <value></value>
        public int EstimatedLength { get { return ToUncompressedBytes().Count; } }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <value></value>
        public string Filename
        {
            get { return filename; }
        }

        /// <summary>
        /// Gets the filenames and locations for this file.
        /// </summary>
        /// <value></value>
        public IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "WORLD/WORLD.BIN", 0xAE4E8 );
                }

                return locations;
            }
        }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        /// <value></value>
        public int MaxLength
        {
            get { return 0x5758; }
        }

        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        public int NumberOfSections
        {
            get { return 8; }
        }

        /// <summary>
        /// Gets a collection of strings with a description of each section in this file.
        /// </summary>
        /// <value></value>
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
        /// <value></value>
        public IList<IList<string>> Sections { get; private set; }

        /// <summary>
        /// Gets or sets a specific entry in a specific section.
        /// </summary>
        /// <param name="section">The section in which contains the entry to get or set</param>
        /// <param name="entry">The specific entry to get or set</param>
        public string this[int section, int entry]
        {
            get { return Sections[section][entry]; }
            set
            {
                EntryLengths[section][entry] = CharMap.StringToByteArray( value ).Length;
                Sections[section][entry] = value;
            }
        }

		#endregion Properties 

		#region Methods (17) 


		// Public Methods (9) 

        /// <summary>
        /// Gets the length in bytes of a specific entry.
        /// </summary>
        /// <param name="section">The section which contains the entry whose length is needed.</param>
        /// <param name="entry">The specific entry whose length is needed.</param>
        /// <returns>The length of the entry, in bytes.</returns>
        public int GetEntryLength( int section, int entry )
        {
            return entryLengths[section][entry];
        }

        /// <summary>
        /// Gets a list of indices for named sections.
        /// </summary>
        public IList<NamedSection> GetNamedSections()
        {
            var result = new List<NamedSection>();
            result.Add( new NamedSection( this, SectionType.JobNames, 1 ) );
            result.Add( new NamedSection( this, SectionType.JobRequirements, 5, true, 94 ) );
            result.Add( new NamedSection( this, SectionType.AbilityNames, 2, true, 512 ) );
            result.Add( new NamedSection( this, SectionType.SkillsetNames, 0 ) );
            
            return result;
        }

        /// <summary>
        /// Gets other patches necessary to make modifications to this file functional.
        /// </summary>
        /// <returns></returns>
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
        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( ToFinalBytes() );
            if( result.Count < MaxLength )
            {
                result.InsertRange( result.Count - 1 - 0x24, new byte[MaxLength - result.Count] );
            }

            return result.ToArray();
        }

        /// <summary>
        /// Creates an uncompressed byte array representing this file.
        /// </summary>
        public IList<byte> ToUncompressedBytes()
        {
            GenericCharMap map = CharMap;

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

            foreach( List<byte> bytes in byteSections )
            {
                result.AddRange( bytes );
            }

            int[] lengths = new int[8];
            for( int i = 0; i < byteSections.Count; i++ )
            {
                lengths[i] = byteSections[i].Count;
            }

            result.AddRange( BuildAddresses( lengths ) );

            return result;
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
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        public void WriteXml( XmlWriter writer )
        {
            WriteXml( writer, false );
        }



		// Private Methods (8) 

        private IList<byte> BuildAddresses( int[] lengths )
        {
            UInt32[] addresses = new UInt32[8];
            addresses[0] = baseAddress;
            for( int i = 1; i < 8; i++ )
            {
                addresses[i] = (UInt32)(addresses[i - 1] + lengths[i - 1]);
            }

            byte[] result = new byte[0x24];
            addresses[0].ToBytes().CopyTo( result, 0 );
            addresses[1].ToBytes().CopyTo( result, 4 );
            addresses[2].ToBytes().CopyTo( result, 8 );
            addresses[3].ToBytes().CopyTo( result, 12 );
            addresses[4].ToBytes().CopyTo( result, 16 );
            addresses[5].ToBytes().CopyTo( result, 20 );
            addresses[6].ToBytes().CopyTo( result, 28 );
            addresses[7].ToBytes().CopyTo( result, 32 );

            return result;
        }

        private IList<UInt32> GetOffsets( IList<byte> bytes )
        {
            UInt32[] result = new UInt32[8];
            result[0] = bytes.Sub( 0, 3 ).ToUInt32() - baseAddress;
            result[1] = bytes.Sub( 4, 7 ).ToUInt32() - baseAddress;
            result[2] = bytes.Sub( 8, 11 ).ToUInt32() - baseAddress;
            result[3] = bytes.Sub( 12, 15 ).ToUInt32() - baseAddress;
            result[4] = bytes.Sub( 16, 19 ).ToUInt32() - baseAddress;
            result[5] = bytes.Sub( 20, 23 ).ToUInt32() - baseAddress;
            result[6] = bytes.Sub( 28, 31 ).ToUInt32() - baseAddress;
            result[7] = bytes.Sub( 32, 35 ).ToUInt32() - baseAddress;

            return result;
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

        private IList<byte> ToFinalBytes()
        {
            return ToUncompressedBytes();
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


		#endregion Methods 

    }
}
