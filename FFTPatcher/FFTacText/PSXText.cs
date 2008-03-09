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
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using FFTPatcher.TextEditor.Files;

namespace FFTPatcher.TextEditor
{
    public enum Filetype
    {
        PSX,
        PSP
    }

    public class FFTText : IXmlSerializable
    {

		#region Properties (3) 


        public Filetype Filetype { get; private set; }

        public IList<IPartitionedFile> PartitionedFiles { get; private set; }

        public IList<IStringSectioned> SectionedFiles { get; private set; }


		#endregion Properties 

		#region Constructors (1) 

        private FFTText()
        {
            PartitionedFiles = new List<IPartitionedFile>();
            SectionedFiles = new List<IStringSectioned>();
        }

		#endregion Constructors 

		#region Methods (5) 


        private void AddToAppropriateCollection(object o)
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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml( XmlReader reader )
        {
            reader.MoveToAttribute( "files" );
            int numberOfFiles = reader.ReadContentAsInt();
            reader.MoveToElement();
            reader.MoveToAttribute( "type" );
            Filetype = (Filetype)Enum.Parse( typeof( Filetype ), reader.ReadContentAsString() );
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
        }

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
