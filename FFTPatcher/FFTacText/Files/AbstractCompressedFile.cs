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
    public abstract class AbstractCompressedFile : AbstractStringSectioned
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


        #region Methods (5)


        private static IList<byte> BuildHeaderFromSectionOffsets( IList<UInt32> offsets )
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

        /// <summary>
        /// Compresses this instance.
        /// </summary>
        private IList<byte> Compress( out IList<UInt32> offsets )
        {
            var result = Compress( Sections, out offsets );

            return result;
        }

        private IList<byte> Compress( IList<IList<string>> sections, out IList<UInt32> offsets )
        {
            return Compress( sections, out offsets, CharMap );
        }

        private IList<byte> Compress( IList<IList<string>> strings, out IList<UInt32> offsets, GenericCharMap map )
        {
            TextUtilities.CompressionResult r = TextUtilities.Compress( strings, map, ExcludedEntries );

            offsets = new List<UInt32>( 32 );
            offsets.Add( 0 );
            int pos = 0;
            for ( int i = 0; i < r.SectionLengths.Count; i++ )
            {
                pos += r.SectionLengths[i];
                offsets.Add( (UInt32)pos );
            }

            return r.Bytes.AsReadOnly();
        }

        private IList<byte> Compress( IDictionary<string, byte> dteTable, out IList<UInt32> offsets )
        {
            IList<IList<string>> strings = new List<IList<string>>( Sections.Count );
            foreach ( IList<string> sec in Sections )
            {
                IList<string> s = new List<string>( sec.Count );
                s.AddRange( sec );

                TextUtilities.DoDTEEncoding( s, dteTable );

                strings.Add( s );
            }

            var result = Compress( strings, out offsets );

            return result;
        }

        /// <summary>
        /// Gets a list of bytes that represent this file in its on-disc form.
        /// </summary>
        /// <returns></returns>
        protected override IList<byte> ToFinalBytes()
        {
            IList<UInt32> offsets;
            IList<byte> compressedBytes = Compress( out offsets );
            List<byte> result = new List<byte>( 0x80 + compressedBytes.Count );
            result.AddRange( BuildHeaderFromSectionOffsets( offsets ) );
            result.AddRange( compressedBytes );
            return result.AsReadOnly();
        }

        protected override IList<byte> ToFinalBytes( IDictionary<string, byte> dteTable )
        {
            IList<UInt32> offsets;
            IList<byte> compressedBytes = Compress( dteTable, out offsets );
            List<byte> result = new List<byte>( 0x80 + compressedBytes.Count );
            result.AddRange( BuildHeaderFromSectionOffsets( offsets ) );
            result.AddRange( compressedBytes );
            return result.AsReadOnly();
        }

        public override IList<IList<byte>> GetSectionByteArrays()
        {
            return GetSectionByteArrays( Sections, CharMap );
        }

        public override IList<IList<byte>> GetSectionByteArrays( IList<IList<string>> sections, GenericCharMap charmap )
        {
            IList<IList<byte>> result = new IList<byte>[NumberOfSections];
            IList<UInt32> offsets;
            IList<byte> compression = Compress( sections, out offsets, charmap );
            uint pos = 0;

            offsets = new List<UInt32>( offsets );
            offsets.Add( (uint)compression.Count );

            for ( int i = 0; i < NumberOfSections; i++ )
            {
                result[i] = compression.Sub( (int)offsets[i], (int)offsets[i + 1] - 1 );
            }
            return result;
        }

        #endregion Methods

    }
}
