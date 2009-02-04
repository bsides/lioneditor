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
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using FFTPatcher.Datatypes;
using FFTPatcher.TextEditor.Files;

namespace FFTPatcher.TextEditor
{
    /// <summary>
    /// Represents a collection of FFT text files.
    /// </summary>
    public class FFTText
    {

		#region Fields (1) 

        /// <summary>
        /// Gets the current version of FFTText files.
        /// </summary>
        public const int CurrentVersion = 3;

		#endregion Fields 

		#region Properties (5) 


        /// <summary>
        /// Gets the character map.
        /// </summary>
        public GenericCharMap CharMap { get; private set; }

        /// <summary>
        /// Gets the filetype.
        /// </summary>
        public Context Filetype { get; private set; }

		#endregion Properties 

		#region Constructors (1) 

        private FFTText()
        {
        }

		#endregion Constructors 

		#region Methods (9) 

        /// <summary>
        /// Gets the menu items for this object.
        /// </summary>
        public IList<MenuItem> GetMenuItems()
        {
            List<MenuItem> result = new List<MenuItem>();
            result.Sort( ( left, right ) => left.Text.CompareTo( right.Text ) );

            return result;
        }

        private static Stack<byte> GetAllowedDteBytes()
        {
            Stack<byte> result = new Stack<byte>();

            for ( byte b = 0xCF; b >= 0xB6; b-- )
            {
                result.Push( b );
            }
            result.Push( 0xb4 );
            result.Push( 0xb3 );
            for ( byte b = 0xB1; b >= 0x94; b-- )
            {
                result.Push( b );
            }
            result.Push( 0x92 );
            result.Push( 0x90 );
            result.Push( 0x8F );
            result.Push( 0x8C );
            for ( byte b = 0x8A; b >= 0x60; b-- )
            {
                result.Push( b );
            }
            for ( byte b = 0x5E; b >= 0x47; b-- )
            {
                result.Push( b );
            }
            result.Push( 0x45 );
            result.Push( 0x43 );
            result.Push( 0x41 );
            result.Push( 0x3f );

            return result;
        }

        public void ClearOutGarbagePSX()
        {
            // OPEN.LZW, Section 25 (1-indexed) (#96 has no {END})
            //           section 26
            // SAMPLE.LZW, section 10
        }

        /// <summary>
        /// Updates a PSX FFT ISO with the text files in this instance.
        /// </summary>
        public void UpdatePsxIso( BackgroundWorker worker, DoWorkEventArgs doWork )
        {
            string filename = doWork.Argument as string;
            List<PatchedByteArray> patches = new List<PatchedByteArray>();
            int numberOfTasks = 1 + // "determining which"
                                //SectionedFiles.Count + // each file
                                1 + // font.bin
                                1; // applying patches


            int tasksCompleted = 0;
            Action<string> progress =
                delegate(string s)
                {
                    worker.ReportProgress( ++tasksCompleted * 100 / numberOfTasks, s);
                };

            if ( Filetype != Context.US_PSX )
            {
                throw new InvalidOperationException();
            }

            //IList<IStringSectioned> filesNeedingDte = new List<IStringSectioned>();

            //progress( "Determining files which are too large" );
            //foreach ( IStringSectioned sectioned in SectionedFiles )
            //{
            //    progress( string.Format( "Processing {0}", sectioned.GetType().Name ) );
            //    if ( !(sectioned is Files.PSX.WORLDBIN) && sectioned.IsDTENeeded() )
            //    {
            //        filesNeedingDte.Add( sectioned );
            //    }
            //}

            //List<IFile> normalFiles = new List<IFile>();
            //SectionedFiles.ForEach( f => normalFiles.Add( f ) );
            //PartitionedFiles.ForEach( p => normalFiles.Add( p ) );
            //normalFiles.RemoveAll( f => f is IStringSectioned && filesNeedingDte.Contains( f as IStringSectioned ) );

            //Set<string> validDtePairs = Program.groups;
            //Set<KeyValuePair<string, byte>> currentPairs = 
            //    new Set<KeyValuePair<string, byte>>( ( x, y ) => x.Key.Equals( y.Key ) && ( x.Value == y.Value ) ? 0 : -1 );
            //IDictionary<IStringSectioned, Set<KeyValuePair<string, byte>>> filePreferredPairs = new Dictionary<IStringSectioned, Set<KeyValuePair<string, byte>>>( filesNeedingDte.Count );

            //filesNeedingDte.Sort( ( x, y ) => ( y.ActualLength - y.MaxLength ).CompareTo( x.ActualLength - x.MaxLength ) );

            //IStringSectioned worldbin = SectionedFiles.Find( f => ( f is Files.PSX.WORLDBIN ) );
            //if ( worldbin.IsDTENeeded() )
            //{
            //    filesNeedingDte.Add( worldbin );
            //    normalFiles.Remove( worldbin );
            //}

            //Stack<byte> dteBytes = GetAllowedDteBytes();
            //numberOfTasks += filesNeedingDte.Count * 2;
            //numberOfTasks += normalFiles.Count;
            //foreach ( var f in filesNeedingDte )
            //{
            //    progress( string.Format( "Calculating DTE for {0}", f.GetType().Name ) );
            //    var r = f.GetPreferredDTEPairs( validDtePairs, currentPairs, dteBytes );
            //    filePreferredPairs[f] = r;
            //    currentPairs.AddRange( r );
            //}

            //foreach ( var f in filesNeedingDte )
            //{
            //    IDictionary<string, byte> currentFileEncoding = Utilities.DictionaryFromKVPs( filePreferredPairs[f] );

            //    progress( string.Format( "Getting patches for {0}", f.GetType().Name ) );
            //    foreach ( var otherPatch in f.GetAllPatches( currentFileEncoding ) )
            //    {
            //        patches.Add( otherPatch );
            //    }
            //}

            //foreach ( var f in normalFiles )
            //{
            //    progress( string.Format( "Getting patches for {0}", f.GetType().Name ) );
            //    foreach ( var otherPatch in f.GetAllPatches() )
            //    {
            //        patches.Add( otherPatch );
            //    }
            //}

            //progress( string.Format( "Updating font for DTE" ) );
            //patches.AddRange( GeneratePsxFontBinPatches( currentPairs ) );

            //using ( FileStream stream = new FileStream( filename, FileMode.Open ) )
            //{
            //    progress( string.Format( "Applying patches" ) );
            //    foreach ( PatchedByteArray patch in patches )
            //    {
            //        IsoPatch.PatchFileAtSector( IsoPatch.IsoType.Mode2Form1, stream, true, patch.Sector, patch.Offset, patch.Bytes, true );
            //    }
            //}
        }

        private void GenerateFontBinPatches( 
            IEnumerable<KeyValuePair<string, byte>> dteEncodings, 
            FFTPatcher.Datatypes.FFTFont baseFont, 
            IList<string> baseCharSet,
            out byte[] fontBytes,
            out byte[] widthBytes )
        {
            FFTPatcher.Datatypes.FFTFont font = 
                new FFTPatcher.Datatypes.FFTFont( baseFont.ToByteArray(), baseFont.ToWidthsByteArray() );
            IList<string> charSet = new List<string>( baseCharSet );

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

            fontBytes = font.ToByteArray();
            widthBytes = font.ToWidthsByteArray();
        }

        private IList<PatchedByteArray> GeneratePspFontBinPatches( IEnumerable<KeyValuePair<string, byte>> dteEncodings )
        {
            var charSet = FFTPatcher.PSPResources.CharacterSet;
            FFTPatcher.Datatypes.FFTFont font = new FFTPatcher.Datatypes.FFTFont( FFTPatcher.PSPResources.FontBin, FFTPatcher.PSPResources.FontWidthsBin );

            byte[] fontBytes;
            byte[] widthBytes;

            GenerateFontBinPatches( dteEncodings, font, charSet, out fontBytes, out widthBytes );

            return
                new PatchedByteArray[] {
                    new PatchedByteArray(PspIso.Files.PSP_GAME.SYSDIR.BOOT_BIN, 0x27b80c, fontBytes),
                    new PatchedByteArray(PspIso.Files.PSP_GAME.SYSDIR.BOOT_BIN, 0x2f73b8, fontBytes),
                    new PatchedByteArray(PspIso.Files.PSP_GAME.SYSDIR.EBOOT_BIN, 0x27b80c, fontBytes),
                    new PatchedByteArray(PspIso.Files.PSP_GAME.SYSDIR.EBOOT_BIN, 0x2f73b8, fontBytes),
                    new PatchedByteArray(PspIso.Files.PSP_GAME.SYSDIR.BOOT_BIN, 0x293f40, widthBytes),
                    new PatchedByteArray(PspIso.Files.PSP_GAME.SYSDIR.BOOT_BIN, 0x30fac0, widthBytes),
                    new PatchedByteArray(PspIso.Files.PSP_GAME.SYSDIR.EBOOT_BIN, 0x293f40, widthBytes),
                    new PatchedByteArray(PspIso.Files.PSP_GAME.SYSDIR.EBOOT_BIN, 0x30fac0, widthBytes)
                };
        }

        private IList<PatchedByteArray> GeneratePsxFontBinPatches( IEnumerable<KeyValuePair<string, byte>> dteEncodings )
        {
            // BATTLE.BIN -> 0xE7614
            // FONT.BIN -> 0
            // WORLD.BIN -> 0x5B8F8

            var charSet = FFTPatcher.PSXResources.CharacterSet;
            FFTPatcher.Datatypes.FFTFont font = new FFTPatcher.Datatypes.FFTFont( FFTPatcher.PSXResources.FontBin, FFTPatcher.PSXResources.FontWidthsBin );

            byte[] fontBytes;
            byte[] widthBytes;

            GenerateFontBinPatches( dteEncodings, font, charSet, out fontBytes, out widthBytes );

            // widths:
            // 0x363234 => 1510 = BATTLE.BIN
            // 0xBD84908 => 84497 = WORLD.BIN
            return
                new PatchedByteArray[] {
                    new PatchedByteArray(PsxIso.BATTLE_BIN, 0xE7614, fontBytes),
                    new PatchedByteArray(PsxIso.EVENT.FONT_BIN, 0x00, fontBytes),
                    new PatchedByteArray(PsxIso.WORLD.WORLD_BIN, 0x5B8f8, fontBytes),
                    new PatchedByteArray(PsxIso.BATTLE_BIN, 0xFF0FC, widthBytes),
                    new PatchedByteArray(PsxIso.WORLD.WORLD_BIN, 0x733E0, widthBytes)
                };
        }

        public IList<IFile> Files { get; private set; }

        internal FFTText( IDictionary<Guid, ISerializableFile> files, QuickEdit quickEdit )
        {
            List<IFile> filesList = new List<IFile>( files.Count + 1 );
            files.ForEach( kvp => filesList.Add( kvp.Value ) );
            filesList.Sort( ( a, b ) => a.DisplayName.CompareTo( b.DisplayName ) );
            filesList.Add( quickEdit );
            Files = filesList.AsReadOnly();
        }

        public static FFTText ReadPSPIso( string filename )
        {
            using ( FileStream stream = File.Open( filename, FileMode.Open, FileAccess.Read ) )
            {
                return ReadPSPIso( stream );
            }
        }

        public static FFTText ReadPSPIso( FileStream stream )
        {
            return FFTTextFactory.GetPspText( stream );
        }

        public static FFTText ReadPSXIso( string filename )
        {
            using ( FileStream stream = File.Open( filename, FileMode.Open, FileAccess.Read ) )
            {
                return ReadPSXIso( stream );
            }
        }

        public static FFTText ReadPSXIso( FileStream stream )
        {
            return FFTTextFactory.GetPsxText( stream );
        }

		#endregion Methods 

    

        public void ReadXml( XmlReader reader )
        {
            throw new NotImplementedException();
        }
    }
}
