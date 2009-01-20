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

        public void UpgradeStrings()
        {
            foreach ( IStringSectioned stringSectioned in SectionedFiles )
            {
                foreach ( IList<string> section in stringSectioned.Sections )
                {
                    for ( int i = 0; i < section.Count; i++ )
                    {
                        section[i] = TextUtilities.UpgradeString( section[i], CharMap );
                    }
                }
            }
            foreach ( IPartitionedFile partitioned in PartitionedFiles )
            {
                foreach ( IPartition partition in partitioned.Sections )
                {
                    for ( int i = 0; i < partition.Entries.Count; i++ )
                    {
                        partition.Entries[i] = TextUtilities.UpgradeString( partition.Entries[i], this.CharMap );
                    }
                }
            }
        }

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

            IList<IFile> filesNeedingDte = new List<IFile>();
            
            foreach ( IStringSectioned sectioned in SectionedFiles )
            {
                if ( sectioned.IsDTENeeded() )
                {
                    filesNeedingDte.Add( sectioned );
                }
            }
            foreach ( IPartitionedFile part in PartitionedFiles )
            {
                if ( part.IsDTENeeded() )
                {
                    filesNeedingDte.Add( part );
                }
            }

            List<IFile> normalFiles = new List<IFile>();
            SectionedFiles.ForEach( f => normalFiles.Add( f ) );
            PartitionedFiles.ForEach( p => normalFiles.Add( p ) );
            normalFiles.RemoveAll( f => filesNeedingDte.Contains( f ) );

            Set<string> validDtePairs = Program.groups;
            Set<string> currentPairs = new Set<string>();
            IDictionary<IFile, Set<string>> filePreferredPairs = new Dictionary<IFile, Set<string>>( filesNeedingDte.Count );

            foreach ( var f in filesNeedingDte )
            {
                var r = f.GetPreferredDTEPairs( validDtePairs, currentPairs );
                filePreferredPairs[f] = r;
                currentPairs.AddRange( r );
            }

            // TODO check pairs returned against max dte pairs

            IDictionary<IFile, byte[]> fileBytes = new Dictionary<IFile, byte[]>();
            IDictionary<string, byte> dteEncodings = new Dictionary<string, byte>();
            IList<string> dtePairs = currentPairs.GetElements();
            for ( int i = 0; i < dtePairs.Count; i++ )
            {
                byte b = (byte)( i + 0x3E );
                if ( b > 0xB1 )
                    b += 1;
                if ( b > 0xB4 )
                    b += 1;
                dteEncodings[dtePairs[i]] = (byte)b;
            }

            foreach ( var f in filesNeedingDte )
            {
                IDictionary<string, byte> currentFileEncoding = new Dictionary<string, byte>();
                foreach ( string s in filePreferredPairs[f] )
                {
                    currentFileEncoding[s] = dteEncodings[s];
                }

                foreach ( var otherPatch in f.GetAllPatches( currentFileEncoding ) )
                {
                    patches.Add( otherPatch );
                }
            }

            foreach ( var f in normalFiles )
            {
                foreach ( var otherPatch in f.GetAllPatches() )
                {
                    patches.Add( otherPatch );
                }
            }

            patches.AddRange( GeneratePsxFontBinPatches( dteEncodings ) );

            using ( FileStream stream = new FileStream( filename, FileMode.Open ) )
            {
                foreach ( PatchedByteArray patch in patches )
                {
                    IsoPatch.PatchFileAtSector( IsoPatch.IsoType.Mode2Form1, stream, true, patch.Sector, patch.Offset, patch.Bytes, true );
                    progress();
                }
            }
        }

        private IList<PatchedByteArray> GeneratePsxFontBinPatches( IDictionary<string, byte> dteEncodings )
        {
            // BATTLE.BIN -> 0xE7614
            // FONT.BIN -> 0
            // WORLD.BIN -> 0x5B8F8
            var charSet = FFTPatcher.PSXResources.CharacterSet;
            FFTPatcher.Datatypes.FFTFont font = new FFTPatcher.Datatypes.FFTFont( FFTPatcher.PSXResources.FontBin, FFTPatcher.PSXResources.FontWidthsBin );
            foreach ( var kvp in dteEncodings )
            {
                int[] chars = new int[] { charSet.IndexOf( kvp.Key.Substring( 0, 1 ) ), charSet.IndexOf( kvp.Key.Substring( 1, 1 ) ) };
                int[] widths = new int[] { font.Glyphs[chars[0]].Width, font.Glyphs[chars[1]].Width };
                int newWidth = widths[0] + widths[1];

                font.Glyphs[kvp.Value].Width = (byte)newWidth;
                IList<FFTPatcher.Datatypes.FontColor> newPixels = font.Glyphs[kvp.Value].Pixels;
                for ( int i = 0; i < newPixels.Count; i++ )
                {
                    newPixels[i] = FFTPatcher.Datatypes.FontColor.Transparent;
                }

                const int fontHeight = 14;
                const int fontWidth = 10;

                int offset = 0;
                for ( int c = 0; c < chars.Length; c++ )
                {
                    var pix = font.Glyphs[chars[c]].Pixels;

                    for ( int x = 0; x < widths[c]; x++ )
                    {
                        for ( int y = 0; y < fontHeight; y++ )
                        {
                            newPixels[y * fontWidth + x + offset] = pix[y * fontWidth + x];
                        }
                    }

                    offset += widths[c];
                }
            }

            byte[] bytes = font.ToByteArray();
            byte[] widthBytes = font.ToWidthsByteArray();

            // widths:
            // 0x363234 => 1510 = BATTLE.BIN
            // 0xBD84908 => 84497 = WORLD.BIN
            return
                new PatchedByteArray[] {
                    new PatchedByteArray(PsxIso.BATTLE_BIN, 0xE7614, bytes),
                    new PatchedByteArray(PsxIso.EVENT.FONT_BIN, 0x00, bytes),
                    new PatchedByteArray(PsxIso.WORLD.WORLD_BIN, 0x5B8f8, bytes),
                    new PatchedByteArray(PsxIso.BATTLE_BIN, 0xFF0FC, widthBytes),
                    new PatchedByteArray(PsxIso.WORLD.WORLD_BIN, 0x733E0, widthBytes)
                };
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
