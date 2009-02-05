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

        public IList<PatchedByteArray> GetPatches()
        {
            IList<ISerializableFile> files = new List<ISerializableFile>( Files.Count );
            Files.FindAll( f => f is ISerializableFile ).ForEach( f => files.Add( (ISerializableFile)f ) );

            List<ISerializableFile> dteFiles = new List<ISerializableFile>();

            List<PatchedByteArray> patches = new List<PatchedByteArray>();

            foreach ( ISerializableFile file in files )
            {
                if ( file.IsDteNeeded() )
                {
                    dteFiles.Add( file );
                }
                else
                {
                    patches.AddRange( file.GetNonDtePatches() );
                }
            }

            Set<string> pairs = DTE.GetDteGroups( this.Filetype );
            Set<KeyValuePair<string, byte>> currentPairs =
                new Set<KeyValuePair<string, byte>>( ( x, y ) => x.Key.Equals( y.Key ) && ( x.Value == y.Value ) ? 0 : -1 );
            var filePreferredPairs =
                new Dictionary<ISerializableFile, Set<KeyValuePair<string, byte>>>( dteFiles.Count );
            dteFiles.Sort( ( x, y ) => ( y.ToCDByteArray().Length - y.Layout.Size ).CompareTo( x.ToCDByteArray().Length - x.Layout.Size ) );
            Stack<byte> dteBytes = DTE.GetAllowedDteBytes();

            foreach ( var dte in dteFiles )
            {
                filePreferredPairs[dte] = dte.GetPreferredDTEPairs( pairs, currentPairs, dteBytes );
                currentPairs.AddRange( filePreferredPairs[dte] );
            }

            foreach ( var dte in dteFiles )
            {
                var currentFileEncoding = Utilities.DictionaryFromKVPs( filePreferredPairs[dte] );
                patches.AddRange( dte.GetDtePatches( currentFileEncoding ) );
            }

            patches.AddRange( DTE.GenerateDtePatches( this.Filetype, currentPairs ) );

            return patches.AsReadOnly();
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
    }
}
