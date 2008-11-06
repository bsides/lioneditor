using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FFTPatcher.SpriteEditor.Files.EVENT
{
    class EVTFACEbin : BaseFile 
    {
        private const int numPortraits = 64;

        public IList<IList<byte>> PortraitsBytes { get; private set; }
        public IList<Palette> Palettes { get; private set; }
        public IList<IList<byte>> PortraitsPixels { get; private set; }

        public EVTFACEbin()
            : this( Program.Images["EVENT/EVTFACE.BIN"] )
        {
        }

        public EVTFACEbin( IList<byte> bytes )
            : base( bytes )
        {
            PortraitsBytes = new IList<byte>[numPortraits];
            Palettes = new Palette[numPortraits];
            PortraitsPixels = new IList<byte>[numPortraits];

            for ( int i = 0; i < 8; i++ )
            {
                for ( int j = 0; j < 8; j++ )
                {
                    PortraitsBytes[i * 8 + j] = Bytes.Sub( i * 8192 + j * 768, i * 8192 + j * 768 + 768 - 1 );
                    Palettes[i * 8 + j] = new Palette( Bytes.Sub( i * 8192 + 8 * 768 + j * 32, i * 8192 + 8 * 768 + j * 32 + 32 - 1 ) );
                }
            }

            for ( int i = 0; i < numPortraits; i++ )
            {
                byte[] b = new byte[768 * 2];
                IList<byte> currentPortrait = PortraitsBytes[i];
                for ( int j = 0; j < 768; j++ )
                {
                    b[j * 2] = (byte)( currentPortrait[j] & 0x0F );
                    b[j * 2 + 1] = (byte)( ( currentPortrait[j] & 0xF0 ) >> 4 );
                }

                PortraitsPixels[i] = b;
            }
        }

        public Image ToImage()
        {
            Bitmap result = new Bitmap( 256, 384 );
            for ( int i = 0; i < numPortraits; i++ )
            {
                int xOffset = i % 8 * 32;
                int yOffset = i / 8 * 48;
                for ( int x = 0; x < 32; x++ )
                {
                    for ( int y = 0; y < 48; y++ )
                    {
                        result.SetPixel(
                            x + xOffset,
                            y + yOffset,
                            Palettes[i].Colors[PortraitsPixels[i][y * 32 + x]] );
                    }
                }
            }

            return result;
        }
    }
}
