using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.TextEditor.Files
{
    public abstract class AbstractCompressedFile : AbstractStringSectioned, ICompressed
    {

		#region Events (2) 

        public event EventHandler<CompressionEventArgs> CompressionFinished;

        public event EventHandler<CompressionEventArgs> ProgressChanged;

		#endregion Events 

		#region Methods (4) 


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

            return result;
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

        public IList<byte> Compress()
        {
            TextUtilities.ProgressCallback p = new TextUtilities.ProgressCallback(
                delegate( int progress )
                {
                    FireProgressChangedEvent( progress );
                } );

            IList<byte> bytes = TextUtilities.Recompress( ToUncompressedBytes().Sub( 0x80 ), p );

            List<UInt32> sectionOffsets = new List<UInt32>();
            sectionOffsets.Add( 0 );

            int i = 0;
            for( int s = 0; s < Sections.Count - 1; s++ )
            {
                IList<string> section = Sections[s];
                int feFound = 0;
                for( int j = i; j < bytes.Count && feFound != section.Count; j++, i++ )
                {
                    if( bytes[j] == 0xFE )
                        feFound++;
                }
                sectionOffsets.Add( (UInt32)i );
            }

            List<byte> result = new List<byte>( 0x80 + bytes.Count );
            result.AddRange( BuildHeaderFromSectionOffsets( sectionOffsets ) );
            result.AddRange( bytes );

            FireCompressionFinishedEvent( result );

            return result;
        }


		#endregion Methods 

    }
}
