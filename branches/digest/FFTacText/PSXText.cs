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
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using FFTPatcher.TextEditor.Files;
using FFTPatcher.TextEditor.Files.PSP;

namespace FFTPatcher.TextEditor
{
    public enum Filetype
    {
        PSX,
        PSP
    }

    /// <summary>
    /// Represents a collection of FFT text files.
    /// </summary>
    public class FFTText : IXmlSerializable
    {

		#region Fields (1) 

        public const int CurrentVersion = 2;

		#endregion Fields 

		#region Properties (4) 


        /// <summary>
        /// Gets the character map.
        /// </summary>
        public GenericCharMap CharMap { get; private set; }

        /// <summary>
        /// Gets the filetype.
        /// </summary>
        public Filetype Filetype { get; private set; }

        /// <summary>
        /// Gets a collection of the <see cref="IPartitionedFile"/>s in this object.
        /// </summary>
        public IList<IPartitionedFile> PartitionedFiles { get; private set; }

        /// <summary>
        /// Gets a collection of the <see cref="IStringSectioned"/>s in this object.
        /// </summary>
        public IList<IStringSectioned> SectionedFiles { get; private set; }


		#endregion Properties 

		#region Constructors (1) 

        private FFTText()
        {
            PartitionedFiles = new List<IPartitionedFile>();
            SectionedFiles = new List<IStringSectioned>();
        }

		#endregion Constructors 

		#region Methods (6) 


        private void AddToAppropriateCollection( object o )
        {
            if( o is IPartitionedFile )
            {
                PartitionedFiles.Add( o as IPartitionedFile );
            }
            else if( o is IStringSectioned )
            {
                SectionedFiles.Add( o as IStringSectioned );
            }
        }

        /// <summary>
        /// Gets the menu items for this object.
        /// </summary>
        public IList<MenuItem> GetMenuItems()
        {
            List<MenuItem> result = new List<MenuItem>();
            foreach( IStringSectioned sectioned in SectionedFiles )
            {
                MenuItem mi = new MenuItem( sectioned.Filename );
                mi.Tag = sectioned;
                result.Add( mi );
            }

            foreach( IPartitionedFile partitioned in PartitionedFiles )
            {
                MenuItem mi = new MenuItem( partitioned.Filename );
                for( int i = 0; i < partitioned.Sections.Count; i++ )
                {
                    MenuItem sub = new MenuItem( string.Format( "Section {0}", i + 1 ) );
                    sub.Tag = partitioned.Sections[i];
                    if( (i != 0) && (i % 25 == 0) )
                    {
                        sub.Break = true;
                    }
                    mi.MenuItems.Add( sub );
                    result.Add( sub );
                }
                result.Add( mi );
            }

            result.Sort(
                delegate( MenuItem one, MenuItem two )
                {
                    return one.Text.CompareTo( two.Text );
                } );

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
            reader.MoveToAttribute( "files" );
            int numberOfFiles = reader.ReadContentAsInt();
            reader.MoveToElement();
            reader.MoveToAttribute( "type" );
            Filetype = (Filetype)Enum.Parse( typeof( Filetype ), reader.ReadContentAsString() );
            reader.MoveToElement();
            reader.MoveToAttribute( "version" );
            int version = reader.ReadContentAsInt();
            reader.MoveToElement();

            reader.ReadStartElement();

            for( int i = 0; i < numberOfFiles; i++ )
            {
                reader.MoveToAttribute( "type" );
                string type = reader.ReadContentAsString();
                reader.MoveToElement();
                Type t = Type.GetType( type );
                ConstructorInfo ci = t.GetConstructor( BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null );
                var o = ci.Invoke( new object[0] ) as IXmlSerializable;
                o.ReadXml( reader );
                AddToAppropriateCollection( o );
            }

            reader.ReadEndElement();

            GenericCharMap charmap = Filetype == Filetype.PSP ? (GenericCharMap)TextUtilities.PSPMap : (GenericCharMap)TextUtilities.PSXMap;
            CharMap = charmap;

            if( version != CurrentVersion )
            {
                foreach( IStringSectioned stringSectioned in SectionedFiles )
                {
                    foreach( IList<string> section in stringSectioned.Sections )
                    {
                        for( int i = 0; i < section.Count; i++ )
                        {
                            section[i] = TextUtilities.UpgradeString( section[i], charmap );
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates a PSP War of the Lions ISO with the text files in this instance..
        /// </summary>
        /// <param name="stream">The stream that represents a War of the Lions image.</param>
        public void UpdatePspIso( FileStream stream )
        {
            if( Filetype != Filetype.PSP )
            {
                throw new InvalidOperationException();
            }

            List<IFFTPackFile> fftpackFiles = new List<IFFTPackFile>();
            List<IBootBin> bootBinFiles = new List<IBootBin>();
            foreach( IStringSectioned sectioned in SectionedFiles )
            {
                if( sectioned is IFFTPackFile )
                {
                    fftpackFiles.Add( sectioned as IFFTPackFile );
                }
                else if( sectioned is IBootBin )
                {
                    bootBinFiles.Add( sectioned as IBootBin );
                }
            }

            foreach( IPartitionedFile part in PartitionedFiles )
            {
                if( part is IFFTPackFile )
                {
                    fftpackFiles.Add( part as IFFTPackFile );
                }
                else if( part is IBootBin )
                {
                    bootBinFiles.Add( part as IBootBin );
                }
            }

            foreach( IFFTPackFile packFile in fftpackFiles )
            {
                PspIso.UpdateFFTPack( stream, packFile.Index, packFile.ToByteArray() );
            }

            foreach( IBootBin bootFile in bootBinFiles )
            {
                byte[] bytes = bootFile.ToByteArray();
                foreach( long location in bootFile.Locations )
                {
                    PspIso.UpdateBootBin( stream, location, bytes );
                }
            }
        }

        private string GetISOFilename( string filename )
        {
            string result = filename;
            if( !result.StartsWith( "/" ) )
            {
                result = "/" + result;
            }
            if( !result.EndsWith( ";1" ) )
            {
                result += ";1";
            }

            return result;
        }

        /// <summary>
        /// Updates a PSX FFT ISO with the text files in this instance.
        /// </summary>
        public void UpdatePsxIso( string filename )
        {
            if( Filetype != Filetype.PSX )
            {
                throw new InvalidOperationException();
            }

            List<CDTool.PatchedByteArray> patches = new List<CDTool.PatchedByteArray>();
            foreach( IStringSectioned sectioned in SectionedFiles )
            {
                byte[] bytes = sectioned.ToByteArray();
                foreach( KeyValuePair<string, long> kvp in sectioned.Locations )
                {

                    patches.Add( new CDTool.PatchedByteArray( GetISOFilename( kvp.Key ), kvp.Value, bytes ) );
                }
            }

            foreach( IPartitionedFile partitioned in PartitionedFiles )
            {
                byte[] bytes = partitioned.ToByteArray();
                foreach( KeyValuePair<string, long> kvp in partitioned.Locations )
                {
                    patches.Add( new CDTool.PatchedByteArray( GetISOFilename( kvp.Key ), kvp.Value, bytes ) );
                }
            }

            CDTool.PatchISO( filename, patches.ToArray() );
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        public void WriteXml( XmlWriter writer )
        {
            writer.WriteAttributeString( "type", this.Filetype.ToString() );
            writer.WriteAttributeString( "version", "1" );
            writer.WriteAttributeString( "files", string.Format( "{0}", PartitionedFiles.Count + SectionedFiles.Count ) );

            foreach( IPartitionedFile file in PartitionedFiles )
            {
                writer.WriteStartElement( "file" );
                writer.WriteAttributeString( "type", file.GetType().ToString() );
                file.WriteXml( writer );
                writer.WriteEndElement();
            }

            foreach( IStringSectioned file in SectionedFiles )
            {
                writer.WriteStartElement( "file" );
                writer.WriteAttributeString( "type", file.GetType().ToString() );
                file.WriteXml( writer );
                writer.WriteEndElement();
            }
        }


		#endregion Methods 

    }
}
