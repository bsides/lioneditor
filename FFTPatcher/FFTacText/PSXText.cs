using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.IO;
using FFTPatcher.TextEditor.Files;
using System.Collections.Generic;
using System.Text;
using FFTPatcher.TextEditor.Files.PSX;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace FFTPatcher.TextEditor
{
    public class PSXText
    {

		#region Properties (2) 


        public IList<IPartitionedFile> PartitionedFiles { get; private set; }

        public IList<IStringSectioned> SectionedFiles { get; private set; }


		#endregion Properties 

		#region Constructors (3) 

        public PSXText()
        {
            PartitionedFiles = new List<IPartitionedFile>();
            SectionedFiles = new List<IStringSectioned>();

            PartitionedFiles.Add( new WLDMES( PSXResources.WLDMES_BIN ) );
            PartitionedFiles.Add( new SNPLMESBIN( PSXResources.SNPLMES_BIN ) );

            SectionedFiles.Add( new ATCHELPLZW( PSXResources.ATCHELP_LZW ) );
            SectionedFiles.Add( new ATTACKOUT( PSXResources.ATTACK_OUT_partial ) );
            SectionedFiles.Add( new HELPMENU( PSXResources.HELPMENU_OUT ) );
            SectionedFiles.Add( new JOINLZW( PSXResources.JOIN_LZW ) );
            SectionedFiles.Add( new OPENLZW( PSXResources.OPEN_LZW ) );
            SectionedFiles.Add( new SAMPLELZW( PSXResources.SAMPLE_LZW ) );
            SectionedFiles.Add( new WLDHELPLZW( PSXResources.WLDHELP_LZW ) );
            SectionedFiles.Add( new WORLDLZW( PSXResources.WORLD_LZW ) );
        }

        public PSXText( string filename )
        {
            XmlDocument doc = new XmlDocument();
            doc.Load( filename );
            LoadFromXmlNode( doc );
        }

        public PSXText( XmlNode document )
        {
            LoadFromXmlNode( document );
        }

		#endregion Constructors 

		#region Methods (6) 


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

        private void LoadFromXmlNode( XmlNode document )
        {
            PartitionedFiles = new List<IPartitionedFile>();
            SectionedFiles = new List<IStringSectioned>();

            XmlNodeList nodes = document.SelectNodes( "/ffttext/file" );
            foreach( XmlNode node in nodes )
            {
                string type = node.Attributes["type"].InnerText;
                Type t = Type.GetType( type );
                ConstructorInfo ci = t.GetConstructor( new Type[] { typeof( IList<byte> ) } );

                byte[] bytes = GZip.Decompress( Convert.FromBase64String( node.InnerText ) );
                object o = ci.Invoke( new object[] { bytes } );
                AddToAppropriateCollection( o );
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

        public void Save( string filename )
        {
            XmlTextWriter writer = new XmlTextWriter( filename, Encoding.UTF8 );
            writer.Formatting = Formatting.Indented;
            writer.IndentChar = ' ';
            writer.Indentation = 2;

            Save( writer, true );
        }

        public void Save( XmlTextWriter writer, bool closeStream )
        {
            writer.WriteStartDocument();
            writer.WriteStartElement( "ffttext" );
            writer.WriteAttributeString( "type", "psx" );
            writer.WriteAttributeString( "version", "1" );

            foreach( IPartitionedFile file in PartitionedFiles )
            {
                writer.WriteStartElement( "file" );
                writer.WriteAttributeString( "type", file.GetType().ToString() );
                writer.WriteString( Utilities.GetPrettyBase64( GZip.Compress( file.ToByteArray() ).ToArray() ) );
                writer.WriteEndElement();
            }

            foreach( IStringSectioned file in SectionedFiles )
            {
                writer.WriteStartElement( "file" );
                writer.WriteAttributeString( "type", file.GetType().ToString() );
                byte[] byteArray;
                if( file is ICompressed )
                {
                    byteArray = (file as ICompressed).ToUncompressedBytes().ToArray();
                }
                else
                {
                    byteArray = file.ToByteArray();
                }
                writer.WriteString( Utilities.GetPrettyBase64( GZip.Compress( byteArray ).ToArray() ) );
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();

            if( closeStream )
            {
                writer.Flush();
                writer.Close();
            }
        }

        public void Save( XmlTextWriter writer )
        {
            Save( writer, false );
        }


		#endregion Methods 

    }
}
