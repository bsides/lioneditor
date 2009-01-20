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

		#region Static Fields (1) 

        private static IDictionary<Enum, long> locations;

		#endregion Static Fields 

		#region Fields (5) 

        private const UInt32 baseAddress = 0x8018E4E8;
        private int[][] entryLengths = null;
        private IList<IList<string>> entryNames;
        private const string filename = "WORLD.BIN";
        private IList<string> sectionNames;

		#endregion Fields 

		#region Properties (12) 


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
        /// <value>The actual length.</value>
        public int ActualLength { get { return ToFinalBytes().Count; } }

        /// <summary>
        /// Gets the character map used for this file.
        /// </summary>
        public GenericCharMap CharMap { get { return TextUtilities.PSXMap; } }

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
                    entryNames = TextEditor.EntryNames.GetEntryNames( GetType() );
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
        public IDictionary<Enum, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<Enum, long>();
                    locations.Add( PsxIso.Sectors.WORLD_WORLD_BIN, 0xAE4E8 );
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
            get { return 0x5732; }
        }

        private static readonly IList<int> SectionMaxLengths = 
            new int[] { 0x44F, 0x582, 0x1333, 0x387, 0xFD5, 0x1A3D, 0x112, 0xB76 }.AsReadOnly();

        private static readonly IList<int> SectionOffsets =
            new int[] { 0x00, 0x450, 0x9D4, 0x1D08, 0x2090, 0x3068, 0x4AA8, 0x4BBC }.AsReadOnly();

        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        public int NumberOfSections
        {
            get { return numberOfSections; }
        }

        private const int numberOfSections = 8;

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
                    sectionNames = TextEditor.EntryNames.GetSectionNames( GetType() );
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

		#region Constructors (2) 

        private WORLDBIN()
        {
            Sections = new List<IList<string>>( NumberOfSections );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WORLDBIN"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        public WORLDBIN( IList<byte> bytes )
            : this()
        {
            for( int i = 0; i < 8; i++ )
            {
                Sections.Add( 
                    TextUtilities.ProcessList( 
                        bytes.Sub( SectionOffsets[i], SectionOffsets[i] + SectionMaxLengths[i] - 1 ), CharMap ) );
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

		#endregion Constructors 

		#region Methods (17) 

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
        public IList<PatchedByteArray> GetAllPatches()
        {
            var result = new List<PatchedByteArray>();
            byte[] bytes = ToByteArray();
            Locations.ForEach( kvp => result.Add( new PatchedByteArray( (PsxIso.Sectors)kvp.Key, kvp.Value, bytes ) ) );
            return result;
        }

        public IList<PatchedByteArray> GetAllPatches( IDictionary<string, byte> dteTable )
        {
            var result = new List<PatchedByteArray>();
            byte[] bytes = ToByteArray( dteTable );
            Locations.ForEach( kvp => result.Add( new PatchedByteArray( (PsxIso.Sectors)kvp.Key, kvp.Value, bytes ) ) );
            return result;
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
            return ToFinalBytes().ToArray();
        }

        private static IList<IList<byte>> GetSectionBytes( IList<IList<string>> sections, GenericCharMap map )
        {
            IList<IList<byte>> byteSections = new List<IList<byte>>( sections.Count );

            foreach ( IList<string> section in sections )
            {
                IList<byte> sectionBytes = new List<byte>();
                foreach ( string s in section )
                {
                    sectionBytes.AddRange( map.StringToByteArray( s ) );
                }
                byteSections.Add( sectionBytes );
            }

            return byteSections;
        }

        public byte[] ToByteArray( IDictionary<string, byte> dteTable )
        {
            IList<IList<string>> strings = new List<IList<string>>( Sections.Count );
            foreach ( IList<string> sec in Sections )
            {
                IList<string> s = new List<string>( sec.Count );
                s.AddRange( sec );

                TextUtilities.DoDTEEncoding( s, dteTable );

                strings.Add( s );
            }

            return ToUncompressedBytes( GetSectionBytes( strings, CharMap ) ).ToArray();
        }

        /// <summary>
        /// Creates an uncompressed byte array representing this file.
        /// </summary>
        public IList<byte> ToUncompressedBytes( IList<IList<byte>> byteSections )
        {
            byte[] result = new byte[MaxLength];

            for ( int i = 0; i < numberOfSections; i++ )
            {
                byteSections[i].CopyTo( result, SectionOffsets[i] );
            }

            return result.AsReadOnly();
        }

        public IList<byte> ToUncompressedBytes()
        {
            return ToUncompressedBytes( GetSectionBytes( Sections, CharMap ) );
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

        /// <summary>
        /// Determines how many bytes would be saved if the specified string could be replaced with a single byte.
        /// </summary>
        public IDictionary<string, int> CalculateBytesSaved( Set<string> replacements )
        {
            GenericCharMap map = CharMap;
            StringBuilder virgin = new StringBuilder( MaxLength );
            Sections.ForEach( s => s.ForEach( t => virgin.Append( t ) ) );
            virgin.Replace( "\r\n", "{}" );
            string virginString = virgin.ToString();

            return TextUtilities.GetPairAndTripleCounts( virginString, replacements );
        }

        public bool IsDTENeeded()
        {
            IList<IList<byte>> sections = GetSectionBytes( Sections, CharMap );
            for ( int i = 0; i < numberOfSections; i++ )
            {
                if ( sections[i].Count > SectionMaxLengths[i] )
                {
                    return true;
                }
            }

            return false;
        }

        public Set<string> GetPreferredDTEPairs( Set<string> replacements, Set<string> currentPairs )
        {
            IList<IList<byte>> sections = GetSectionBytes( Sections, CharMap );
            Set<string> result = new Set<string>();
            IList<string> currentPairsList = currentPairs.GetElements();

            for ( int i = 0; i < numberOfSections; i++ )
            {
                if ( sections[i].Count > SectionMaxLengths[i] )
                {
                    var dict = TextUtilities.GetPairAndTripleCounts( Sections[i].Join( string.Empty ), replacements );

                    int bytesNeeded = sections[i].Count - SectionMaxLengths[i];
                    int j  = 0;

                    while ( bytesNeeded > 0 && j < currentPairsList.Count )
                    {
                        if ( dict.ContainsKey( currentPairsList[j] ) )
                        {
                            bytesNeeded -= dict[currentPairsList[j]];
                            dict.Remove( currentPairsList[j] );
                            result.Add( currentPairsList[j] );
                        }
                        j++;
                    }

                    j = 0;

                    IList<string> resultList = result.GetElements();

                    while ( bytesNeeded > 0 && j < resultList.Count )
                    {
                        if ( dict.ContainsKey( resultList[j] ) )
                        {
                            bytesNeeded -= dict[resultList[j]];
                            dict.Remove( resultList[j] );
                        }
                        j++;
                    }

                    j = 0;
                    var l = new List<KeyValuePair<string, int>>( dict );
                    l.Sort( ( a, b ) => b.Value.CompareTo( a.Value ) );
                    while ( bytesNeeded > 0 && j < l.Count )
                    {
                        bytesNeeded -= dict[l[j].Key];
                        result.Add( l[j].Key );
                        j++;
                    }

                    if ( bytesNeeded > 0 )
                    {
                        return null;
                    }
                }
            }

            return result;
        }

        #endregion Methods


    }
}
