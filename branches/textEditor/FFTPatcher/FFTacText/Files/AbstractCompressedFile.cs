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

namespace FFTPatcher.TextEditor.Files
{
    /// <summary>
    /// An <see cref="AbstractStringSectioned"/> whose text can be compressed.
    /// </summary>
    public abstract class AbstractCompressedFile : AbstractStringSectioned, ICompressed
    {

        #region Properties (2)


        /// <summary>
        /// Gets the estimated length of this file if it were turned into a byte array.
        /// </summary>
        public override int EstimatedLength
        {
            get { return (int)(base.EstimatedLength * 0.65346430772862594919277); }
        }



        /// <summary>
        /// Gets the entries that are excluded from compression.
        /// </summary>
        public virtual IDictionary<int, IList<int>> ExcludedEntries
        {
            get { return null; }
        }


        #endregion Properties

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractCompressedFile"/> class.
        /// </summary>
        protected AbstractCompressedFile()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractCompressedFile"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        protected AbstractCompressedFile( IList<byte> bytes )
            : base( bytes )
        {
        }

        #endregion Constructors

        #region Events (2)

        /// <summary>
        /// Occurs when a compression operation has finished.
        /// </summary>
        public event EventHandler<CompressionEventArgs> CompressionFinished;

        /// <summary>
        /// Occurs when the progress of a compresison operation has changed.
        /// </summary>
        public event EventHandler<CompressionEventArgs> ProgressChanged;

        #endregion Events

        #region Methods (5)


        private IList<byte> BuildHeaderFromSectionOffsets( IList<UInt32> offsets )
        {
            List<byte> result = new List<byte>( 0x80 );
            foreach( UInt32 offset in offsets )
            {
                result.AddRange( offset.ToBytes() );
            }

            while( result.Count < 0x80 )
            {
                result.Add( 0x00 );
            }

            return result.Sub( 0, 0x7F );
        }

        private void FireCompressionFinishedEvent( IList<byte> result )
        {
            if( CompressionFinished != null )
            {
                CompressionFinished( this, new CompressionEventArgs( 100, result ) );
            }
        }

        private void FireProgressChangedEvent( int progress )
        {
            if( ProgressChanged != null )
            {
                ProgressChanged( this, new CompressionEventArgs( progress ) );
            }
        }

        /// <summary>
        /// Compresses this instance.
        /// </summary>
        public IList<byte> Compress()
        {
            TextUtilities.ProgressCallback p = progress => FireProgressChangedEvent( progress );

            TextUtilities.CompressionResult r = TextUtilities.Compress( this, ExcludedEntries, p );

            List<UInt32> offsets = new List<UInt32>( 32 );
            offsets.Add( 0 );
            int pos = 0;
            for( int i = 0; i < r.SectionLengths.Count; i++ )
            {
                pos += r.SectionLengths[i];
                offsets.Add( (UInt32)pos );
            }

            List<byte> result = new List<byte>( 0x80 + r.Bytes.Count );
            result.AddRange( BuildHeaderFromSectionOffsets( offsets ) );
            result.AddRange( r.Bytes );

            FireCompressionFinishedEvent( result );

            return result;
        }



        /// <summary>
        /// Gets a list of bytes that represent this file in its on-disc form.
        /// </summary>
        /// <returns></returns>
        protected override IList<byte> ToFinalBytes()
        {
            return Compress();
        }


        #endregion Methods

    }
}
