using System;
using System.Collections.Generic;
using System.Text;
using PatcherLib.Datatypes;
using System.IO;
using System.Drawing;
using PatcherLib.Utilities;

namespace FFTPatcher.SpriteEditor
{
    public class AllOtherImages : IEnumerable<AbstractImage>
    {
        private static List<AbstractImage> BuildPspImages()
        {
            List<AbstractImage> images = new List<AbstractImage>
            {
                new Raw16BitImage( "END1", 256, 256,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END1_BIN, 0, 131072 ) ),
                new Raw16BitImage( "END2", 256, 256,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END2_BIN, 0, 131072 ) ),
                new Raw16BitImage( "END3", 256, 256,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END3_BIN, 0, 131072 ) ),
                new Raw16BitImage( "END4", 256, 256,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END4_BIN, 0, 131072 ) ),
                new Raw16BitImage( "END5", 256, 256,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END5_BIN, 0, 131072 ) ),
                new Raw16BitImage( "OPENBK1", 320, 240,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 0, 153600 ) ),
                new Raw16BitImage( "OPENBK2", 256, 240,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 153600, 122880 ) ),
                new Raw16BitImage( "OPENBK3", 210, 180,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 276480, 75600) ),
                new Raw16BitImage( "OPENBK4", 210, 180,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 352080, 75600 ) ),
                new Raw16BitImage( "OPENBK5", 210, 180,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 427680, 75600 ) ),
                new Raw16BitImage( "OPENBK6", 210, 180,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 503280, 75600 ) ),
                new Raw16BitImage( "OPENBK7", 512, 240,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 578880, 245760 ) ),
            };

            for ( int i = 0; i < 130; i++ )
            {
                images.Add( new Raw16BitImage( string.Format( "WLDBK{0:000}", i ), 256, 240,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDBK_BIN, 256 * 240 * 2 * i, 256 * 240 * 2 ) ) );
            }

            return images;
        }

        private static List<AbstractImage> BuildPsxImages()
        {
            List<AbstractImage> psxImages = new List<AbstractImage>
            {
                new Raw16BitImage( "END1", 256, 256,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_END1_BIN, 0, 131072 ) ),
                new Raw16BitImage( "END2", 256, 256,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_END2_BIN, 0, 131072 ) ),
                new Raw16BitImage( "END3", 256, 256,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_END3_BIN, 0, 131072 ) ),
                new Raw16BitImage( "END4", 256, 256,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_END4_BIN, 0, 131072 ) ),
                new Raw16BitImage( "END5", 256, 256,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_END5_BIN, 0, 131072 ) ),
                new Raw16BitImage( "OPENBK1", 320, 240,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNBK_BIN, 0, 153600 ) ),
                new Raw16BitImage( "OPENBK2", 256, 240,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNBK_BIN, 153600, 122880 ) ),
                new Raw16BitImage( "OPENBK3", 210, 180,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNBK_BIN, 276480, 75600) ),
                new Raw16BitImage( "OPENBK4", 210, 180,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNBK_BIN, 352080, 75600 ) ),
                new Raw16BitImage( "OPENBK5", 210, 180,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNBK_BIN, 427680, 75600 ) ),
                new Raw16BitImage( "OPENBK6", 210, 180,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNBK_BIN, 503280, 75600 ) ),
                new Raw16BitImage( "OPENBK7", 512, 240,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNBK_BIN, 578880, 245760 ) ),
                new Raw16BitImage( "SCEAP", 320, 32,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCEAP_DAT, 0, 20480 ) )     
            };

            for ( int i = 0; i < 130; i++ )
            {
                psxImages.Add( new Raw16BitImage( string.Format( "WLDBK{0:000}", i ), 256, 240,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDBK_BIN, 256 * 240 * 2 * i, 256 * 240 * 2 ) ) );
            }

            //psxImages.Add( new PalettedImage( "CHAPTER1", 256, 62, 1, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER1_BIN, 0, 0x1FE0 ),
            //    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER1_BIN, 0x1FE0, 32 ) ) );
            //psxImages.Add( new PalettedImage( "CHAPTER2", 256, 62, 1, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER2_BIN, 0, 0x1FE0 ),
            //    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER2_BIN, 0x1FE0, 32 ) ) );
            //psxImages.Add( new PalettedImage( "CHAPTER3", 256, 62, 1, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER3_BIN, 0, 0x1FE0 ),
            //    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER3_BIN, 0x1FE0, 32 ) ) );
            //psxImages.Add( new PalettedImage( "CHAPTER4", 256, 62, 1, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER4_BIN, 0, 0x1FE0 ),
            //    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER4_BIN, 0x1FE0, 32 ) ) );

            return psxImages;
        }


        private IList<AbstractImage> images;

        public AbstractImage this[int i]
        {
            get { return this.images[i]; }
        }

        public static AllOtherImages GetPsx()
        {
            return new AllOtherImages( BuildPsxImages() );
        }

        public static AllOtherImages GetPsp()
        {
            return new AllOtherImages( BuildPspImages() );
        }

        public static AllOtherImages FromIso( Stream iso )
        {
            if ( iso.Length % PatcherLib.Iso.IsoPatch.SectorSizes[PatcherLib.Iso.IsoPatch.IsoType.Mode2Form1] == 0 )
            {
                // assume psx
                return GetPsx();
            }
            else if ( iso.Length % PatcherLib.Iso.IsoPatch.SectorSizes[PatcherLib.Iso.IsoPatch.IsoType.Mode1] == 0 )
            {
                // assume psp
                return GetPsp();
            }
            else
            {
                throw new ArgumentException( "iso" );
            }
        }

        private AllOtherImages( IList<AbstractImage> images )
        {
            this.images = images.AsReadOnly();
        }

        #region IEnumerable<AbstractImage> Members

        public IEnumerator<AbstractImage> GetEnumerator()
        {
            return images.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return images.GetEnumerator();
        }

        #endregion
    }
}
