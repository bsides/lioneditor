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
using System.Diagnostics;
using System.ComponentModel;

namespace FFTPatcher.TextEditor
{
    /// <summary>
    /// Filetypes
    /// </summary>
    public enum Filetype
    {
        /// <summary>
        /// Playstation
        /// </summary>
        PSX,
        /// <summary>
        /// PSP
        /// </summary>
        PSP
    }

    /// <summary>
    /// Represents a collection of FFT text files.
    /// </summary>
    public class FFTText : IXmlSerializable
    {

		#region Fields (1) 

        /// <summary>
        /// Gets the current version of FFTText files.
        /// </summary>
        public const int CurrentVersion = 2;

		#endregion Fields 

		#region Properties (5) 


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
        /// Gets the quick edit structure.
        /// </summary>
        public IQuickEdit QuickEdit 
        {
            get { return QuickEditFactory.GetQuickEdit( this ); }
        }

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

		#region Methods (9) 


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

            result.Sort( ( left, right ) => left.Text.CompareTo( right.Text ) );

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
                reader.MoveToElement();
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

                switch( version )
                {
                    case 1:
                        AddMisingFiles( Filetype, version );
                        break;
                    default:
                        throw new InvalidOperationException( string.Format( "Can't upgrade from version {0} file", version ) );
                }
            }
        }

        private void AddMisingFiles( Filetype Filetype, int version )
        {
            FFTText newVersion = null;
            XmlSerializer xs = new XmlSerializer( typeof( FFTText ) );
            using( MemoryStream ms = new MemoryStream( Filetype == Filetype.PSP ? PSPResources.DefaultDocument : PSXResources.DefaultDocument ) )
            using( XmlTextReader reader = new XmlTextReader( ms ) )
            {
                reader.WhitespaceHandling = WhitespaceHandling.None;
                newVersion = xs.Deserialize( reader ) as FFTText;
            }

            foreach( IStringSectioned sectioned in newVersion.SectionedFiles )
            {
                if( SectionedFiles.Find( found => sectioned.GetType().Equals( found.GetType() ) ) == null )
                {
                    SectionedFiles.Add( sectioned );
                }
            }

            foreach( IPartitionedFile partitioned in newVersion.PartitionedFiles )
            {
                if( PartitionedFiles.Find( found => partitioned.GetType().Equals( found.GetType() ) ) == null )
                {
                    PartitionedFiles.Add( partitioned );
                }
            }
        }

        public void UpdateIso( BackgroundWorker worker, DoWorkEventArgs doWork )
        {
            if ( Filetype == Filetype.PSP )
            {
                UpdatePspIso( worker, doWork );
            }
            else if ( Filetype == Filetype.PSX )
            {
                UpdatePsxIso( worker, doWork );
            }
        }

        /// <summary>
        /// Updates a PSP War of the Lions ISO with the text files in this instance..
        /// </summary>
        /// <param name="stream">The stream that represents a War of the Lions image.</param>
        public void UpdatePspIso( BackgroundWorker worker, DoWorkEventArgs doWork )
        {
            if ( Filetype != Filetype.PSP )
            {
                throw new InvalidOperationException();
            }

            List<IFFTPackFile> fftpackFiles = new List<IFFTPackFile>();
            List<IBootBin> bootBinFiles = new List<IBootBin>();
            foreach ( IStringSectioned sectioned in SectionedFiles )
            {
                if ( sectioned is IFFTPackFile )
                {
                    fftpackFiles.Add( sectioned as IFFTPackFile );
                }
                else if ( sectioned is IBootBin )
                {
                    bootBinFiles.Add( sectioned as IBootBin );
                }
            }

            foreach ( IPartitionedFile part in PartitionedFiles )
            {
                if ( part is IFFTPackFile )
                {
                    fftpackFiles.Add( part as IFFTPackFile );
                }
                else if ( part is IBootBin )
                {
                    bootBinFiles.Add( part as IBootBin );
                }
            }

            int bootBinCount = 0;
            bootBinFiles.ForEach( file => bootBinCount += file.Locations.Count );

            int defaultNumberOfTasks = fftpackFiles.Count * 2 + bootBinFiles.Count + bootBinCount;
            int tasksCompleted = 0;
            MethodInvoker progress =
                delegate()
                {
                    worker.ReportProgress( ++tasksCompleted * 100 / defaultNumberOfTasks );
                };

            using ( FileStream stream = new FileStream( doWork.Argument as string, FileMode.Open ) )
            {
                foreach ( IFFTPackFile packFile in fftpackFiles )
                {
                    byte[] bytes = packFile.ToByteArray();
                    progress();
                    PspIso.UpdateFFTPack( stream, (int)packFile.Index, bytes );
                    progress();
                }

                foreach ( IBootBin bootFile in bootBinFiles )
                {
                    byte[] bytes = bootFile.ToByteArray();
                    progress();
                    foreach ( long location in bootFile.Locations )
                    {
                        PspIso.UpdateBootBin( stream, location, bytes );
                        progress();
                    }
                }
            }
        }

        /// <summary>
        /// Updates a PSX FFT ISO with the text files in this instance.
        /// </summary>
        public void UpdatePsxIso( BackgroundWorker worker, DoWorkEventArgs doWork )
        {
            string filename = doWork.Argument as string;
            int defaultNumberOfTasks = SectionedFiles.Count + PartitionedFiles.Count;
            List<PatchedByteArray> patches = new List<PatchedByteArray>();

            int tasksCompleted = 0;
            MethodInvoker progress =
                delegate()
                {
                    int numberOfTasks = defaultNumberOfTasks + patches.Count * 2;
                    worker.ReportProgress( ++tasksCompleted * 100 / numberOfTasks );
                };
            if ( Filetype != Filetype.PSX )
            {
                throw new InvalidOperationException();
            }

            foreach ( IStringSectioned sectioned in SectionedFiles )
            {
                byte[] bytes = sectioned.ToByteArray();
                progress();
                foreach( KeyValuePair<Enum, long> kvp in sectioned.Locations )
                {
                    patches.Add( new PatchedByteArray( (PsxIso.Sectors)kvp.Key, kvp.Value, bytes ) );
                    progress();
                    foreach ( PatchedByteArray otherPatch in sectioned.GetAllPatches() )
                    {
                        patches.Add( otherPatch );
                        progress();
                    }
                }
            }

            foreach ( IPartitionedFile partitioned in PartitionedFiles )
            {
                byte[] bytes = partitioned.ToByteArray();
                progress();
                foreach ( KeyValuePair<Enum, long> kvp in partitioned.Locations )
                {
                    patches.Add( new PatchedByteArray( (PsxIso.Sectors)kvp.Key, kvp.Value, bytes ) );
                    progress();
                    foreach ( PatchedByteArray otherPatch in partitioned.GetAllPatches() )
                    {
                        patches.Add( otherPatch );
                        progress();
                    }
                }
            }

            string fullpath = Path.GetFullPath( filename );
            string ppfFilename =
                fullpath.Remove( fullpath.LastIndexOf( Path.GetExtension( fullpath ) ) ) + ".fftactext.ppf";
            using ( FileStream stream = new FileStream( filename, FileMode.Open ) )
            using ( FileStream ppfStream = new FileStream( ppfFilename, FileMode.Create ) )
            {
                var ppfDict = new Dictionary<long, IsoPatch.NewOldValue>();
                foreach ( PatchedByteArray patch in patches )
                {
                    IsoPatch.PatchFileAtSector( IsoPatch.IsoType.Mode2Form1, stream, true, patch.Sector, patch.Offset, patch.Bytes, true, true, ppfDict );
                    progress();
                }
                var ppf = IsoPatch.GeneratePpf( ppfDict );
                ppfStream.Write( ppf.ToArray(), 0, ppf.Count );
            }
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        public void WriteXml( XmlWriter writer )
        {
            writer.WriteAttributeString( "type", this.Filetype.ToString() );
            writer.WriteAttributeString( "version", CurrentVersion.ToString( System.Globalization.CultureInfo.InvariantCulture ) );
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
