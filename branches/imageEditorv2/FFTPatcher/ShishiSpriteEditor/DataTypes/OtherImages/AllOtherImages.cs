using System;
using System.Collections.Generic;
using System.Text;
using PatcherLib.Datatypes;
using System.IO;
using System.Drawing;
using PatcherLib.Utilities;

namespace FFTPatcher.SpriteEditor
{
    public class AllOtherImages
    {
        private AllImagesDoWorkResult LoadAllImages( Stream iso, string path, Action<int> progressReporter )
        {
            bool progress = progressReporter != null;
            int total = 0;
            int complete = 0;
            int imagesProcessed = 0;

            if (progress)
            {
                images.ForEach( i => total += i.Count );
            }

            foreach (var imgList in images)
            {
                foreach (var img in imgList)
                {
                    string name = string.Empty;
                    if (img.Position is PatcherLib.Iso.PsxIso.KnownPosition)
                    {
                        var pos = img.Position as PatcherLib.Iso.PsxIso.KnownPosition;
                        name = string.Format( "{0}_{1}.png", pos.Sector, pos.StartLocation );
                    }
                    else if (img.Position is PatcherLib.Iso.PspIso.KnownPosition)
                    {
                        var pos = img.Position as PatcherLib.Iso.PspIso.KnownPosition;
                        name = string.Format( "{0}_{1}.png", pos.SectorEnum, pos.StartLocation );
                    }
                    name = Path.Combine( path, name );
                    if (File.Exists( name ))
                    {
                        img.WriteImageToIso( iso, name );
                        imagesProcessed++;
                    }
                    if (progress)
                    {
                        progressReporter( (100 * (complete++)) / total );
                    }
                }
            }

            return new AllImagesDoWorkResult( AllImagesDoWorkResult.Result.Success, imagesProcessed );
        }

        public void LoadAllImages( Stream iso, string path )
        {
            LoadAllImages( iso, path, null );
        }

        public class AllImagesDoWorkData
        {
            public Stream ISO { get; private set; }
            public string Path { get; private set; }
            public AllImagesDoWorkData( Stream iso, string path )
            {
                ISO = iso;
                Path = path;
            }
        }

        public class AllImagesDoWorkResult
        {
            public enum Result
            {
                Success,
                Failure,
            }

            public Result DoWorkResult { get; private set; }
            public int ImagesProcessed { get; private set; }
            public AllImagesDoWorkResult( Result result, int images )
            {
                DoWorkResult = result;
                ImagesProcessed = images;
            }

        }

        internal void LoadAllImages( object sender, System.ComponentModel.DoWorkEventArgs e )
        {
            System.ComponentModel.BackgroundWorker worker = sender as System.ComponentModel.BackgroundWorker;
            AllImagesDoWorkData data = e.Argument as AllImagesDoWorkData;
            if (data == null)
                return;
            e.Result = LoadAllImages( data.ISO, data.Path, worker.WorkerReportsProgress ? (Action<int>)worker.ReportProgress : null );
        }

        internal void DumpAllImages( object sender, System.ComponentModel.DoWorkEventArgs e )
        {
            System.ComponentModel.BackgroundWorker worker = sender as System.ComponentModel.BackgroundWorker;
            AllImagesDoWorkData data = e.Argument as AllImagesDoWorkData;
            if (data == null)
                return;
            var result = DumpAllImages( data.ISO, data.Path, worker.WorkerReportsProgress ? (Action<int>)worker.ReportProgress : null );
            e.Result = result;
        }

        private AllImagesDoWorkResult DumpAllImages( Stream iso, string path, Action<int> progressReporter )
        {
            bool progress = progressReporter != null;
            int total = 0;
            int complete = 0;
            int imagesProcessed = 0;
            if (progress)
                images.ForEach( i => total += i.Count );

            if (!Directory.Exists( path ))
            {
                Directory.CreateDirectory( path );
            }
            foreach (var imgList in images)
            {
                foreach (var img in imgList)
                {
                    string name = string.Empty;
                    if (img.Position is PatcherLib.Iso.PsxIso.KnownPosition)
                    {
                        var pos = img.Position as PatcherLib.Iso.PsxIso.KnownPosition;
                        name = string.Format( "{0}_{1}.png", pos.Sector, pos.StartLocation );
                    }
                    else if (img.Position is PatcherLib.Iso.PspIso.KnownPosition)
                    {
                        var pos = img.Position as PatcherLib.Iso.PspIso.KnownPosition;
                        name = string.Format( "{0}_{1}.png", pos.SectorEnum, pos.StartLocation );
                    }

                    if (!string.IsNullOrEmpty( name ))
                    {
                        Bitmap bmp = img.GetImageFromIso( iso );
                        bmp.Save( Path.Combine( path, name ), System.Drawing.Imaging.ImageFormat.Png );
                        imagesProcessed++;
                    }

                    if (progress)
                    {
                        progressReporter( (100 * (complete++)) / total );
                    }
                }
            }

            return new AllImagesDoWorkResult( AllImagesDoWorkResult.Result.Success, imagesProcessed );
        }

        public void DumpAllImages( Stream iso, string path )
        {
            DumpAllImages( iso, path, null );
        }

        private static IList<IList<AbstractImage>> BuildPspImages()
        {
            List<IList<AbstractImage>> result = new List<IList<AbstractImage>>();
            //result.Add( new AbstractImage[] { 
                //new PalettedImage8bpp( "??", 240, 256, 1, Palette.ColorDepth._16bit, 
                //    new PatcherLib.Iso.PspIso.KnownPosition((PatcherLib.Iso.FFTPack.Files)770, 0x20C, 0x42DF4),
                //    new PatcherLib.Iso.PspIso.KnownPosition((PatcherLib.Iso.FFTPack.Files)770, 0xC, 0x200)),

            //} );
            IList<AbstractImage> bonusPics = new AbstractImage[71];
            for ( int i = 0; i < 71; i++ )
            {
                bonusPics[i] = new PalettedImage4bpp( string.Format( "BONUS{0}", i ), 256, 50, 1,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_BONUS_BIN, 0x6800 * i + 135 * 256 / 2, 256 * 50 / 2 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_BONUS_BIN, 0x6800 * i + 0x6400 + 0x20 * 2, 0x20 ) );
            }
            result.Add( bonusPics.AsReadOnly() );
            result.Add( new AbstractImage[] {
                new PalettedImage8bpp("OPNTEX1", 128,256, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 34816+0x220, 128*256),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 34816+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX2", 256,256, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 69632+0x220, 256*256),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 69632+0x14, 256*2)),
                new PalettedImage4bpp("OPNTEX3", 256, 256, 1, 
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 137216+0x40, 256*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 137216+0x14, 16*2)),
                new PalettedImage4bpp("OPNTEX4", 256, 256, 1, 
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 172032+0x40, 256*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 172032+0x14, 16*2)),
                new PalettedImage4bpp("OPNTEX5", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x3B000+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x3B000, 16*4)),
                new PalettedImage4bpp("OPNTEX6", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x4B800+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x4B800, 16*4)),
                new PalettedImage4bpp("OPNTEX7", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x5c000+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x5c000, 16*4)),
                new PalettedImage4bpp("OPNTEX8", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x6c800+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x6c800, 16*4)),
                new PalettedImage4bpp("OPNTEX9", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x7d000+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x7d000, 16*4)),
                new PalettedImage4bpp("OPNTEX10", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x8d800+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x8d800, 16*4)),
                new PalettedImage4bpp("OPNTEX11", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x9e000+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x9e000, 16*4)),
                new PalettedImage4bpp("OPNTEX12", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0xae800+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0xae800, 16*4)),
                new PalettedImage4bpp("OPNTEX13", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0xbf000+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0xbf000, 16*4)),
                new PalettedImage4bpp("OPNTEX14", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0xcf800+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0xcf800, 16*4)),
                new PalettedImage4bpp("OPNTEX15", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0xe0000+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0xe0000, 16*4)),
                new PalettedImage4bpp("OPNTEX16", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0xf0800+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0xf0800, 16*4)),
                new PalettedImage4bpp("OPNTEX17", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x101000+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x101000, 16*4)),
                new PalettedImage4bpp("OPNTEX18", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x111800+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x111800, 16*4)),
                new PalettedImage4bpp("OPNTEX19", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x122000+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x122000, 16*4)),
                new PalettedImage4bpp("OPNTEX10", 512, 256, 1,  Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x132800+0x40, 512*256/2),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.OPEN_OPNTEX_BIN, 0x132800, 16*4)),
            }.AsReadOnly() );
            result.Add( new AbstractImage[] {
                new PalettedImage8bpp( "END1", 512, 256, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END1_BIN, 0x400, 512*256 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END1_BIN, 0, 0x400 ) ),
                new PalettedImage8bpp( "END2", 512, 256, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END2_BIN, 0x400, 512*256 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END2_BIN, 0, 0x400 ) ),
                new PalettedImage8bpp( "END3", 512, 256, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END3_BIN, 0x400, 512*256 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END3_BIN, 0, 0x400 ) ),
                new PalettedImage8bpp( "END4", 512, 256, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END4_BIN, 0x400, 512*256 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END4_BIN, 0, 0x400 ) ),
                new PalettedImage8bpp( "END5", 512, 256, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END5_BIN, 0x400, 512*256 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_END5_BIN, 0, 0x400 ) ),
            }.AsReadOnly() );

            result.Add( new AbstractImage[] {
                new Raw16BitImage( "OPENBK1 (unused)", 320, 240,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 0, 153600 ) ),
                new PalettedImage8bpp( "OPENBK2", 512, 256, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 153600+0x400,131072),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 153600,0x400)),
                new PalettedImage8bpp( "OPENBK3", 512, 256, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 286720+0x400,131072),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 286720,0x400)),
                new PalettedImage8bpp( "OPENBK4", 512, 256, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 419840+0x400,131072),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 419840,0x400)),
                new PalettedImage8bpp( "OPENBK5", 512, 256, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 552960+0x400,131072),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 552960,0x400)),
                new PalettedImage8bpp( "OPENBK6", 512, 256, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 686080+0x400,131072),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 686080,0x400)),
                new PalettedImage8bpp( "OPENBK7", 512, 256, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 819200+0x400,131072),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.OPEN_OPNBK_BIN, 819200,0x400)),
            }.AsReadOnly() );

            result.Add( new AbstractImage[] {
                new PalettedImage8bpp("BK_FITR", 512,256,1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_FITR_TIM,0x400,512*256),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_FITR_TIM,0x0,0x400)),
                new PalettedImage8bpp("BK_FITR2", 512,256,1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_FITR2_TIM,0x400,512*256),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_FITR2_TIM,0x0,0x400)),
                new PalettedImage8bpp("BK_FITR3", 512,256,1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_FITR3_TIM,0x400,512*256),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_FITR3_TIM,0x0,0x400)),
                new PalettedImage8bpp("BK_HONE", 512,256,1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_HONE_TIM,0x400,512*256),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_HONE_TIM,0x0,0x400)),
                new PalettedImage8bpp("BK_HONE2", 512,256,1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_HONE2_TIM,0x400,512*256),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_HONE2_TIM,0x0,0x400)),
                new PalettedImage8bpp("BK_HONE3", 512,256,1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_HONE3_TIM,0x400,512*256),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_HONE3_TIM,0x0,0x400)),
                 new PalettedImage8bpp("BK_SHOP", 512,256,1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_SHOP_TIM,0x400,512*256),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_SHOP_TIM,0x0,0x400)),
                new PalettedImage8bpp("BK_SHOP2", 512,256,1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_SHOP2_TIM,0x400,512*256),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_SHOP2_TIM,0x0,0x400)),
                new PalettedImage8bpp("BK_SHOP3", 512,256,1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_SHOP3_TIM,0x400,512*256),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.MENU_BK_SHOP3_TIM,0x0,0x400)),
            }.AsReadOnly() );

            var wldbkImages = new AbstractImage[130];
            for ( int i = 0; i < 130; i++ )
            {
                wldbkImages[i] = new PalettedImage8bpp( string.Format( "WLDBK{0:000}", i ), 512, 256, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDBK_BIN, 0x20800 * i + 0x400, 131072 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDBK_BIN, 0x20800 * i, 0x400 ) );
            }
            result.Add( wldbkImages.AsReadOnly() );

            result.Add( new AbstractImage[] {
                new PalettedImage4bpp( "CHAPTER1", 256, 62, 1, new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_CHAPTER1_OUT, 0, 0x1FE0 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_CHAPTER1_OUT, 0x1FE0, 32 ) ),
                new PalettedImage4bpp( "CHAPTER2", 256, 62, 1, new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_CHAPTER2_OUT, 0, 0x1FE0 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_CHAPTER2_OUT, 0x1FE0, 32 ) ),
                new PalettedImage4bpp( "CHAPTER3", 256, 62, 1, new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_CHAPTER3_OUT, 0, 0x1FE0 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_CHAPTER3_OUT, 0x1FE0, 32 ) ),
                new PalettedImage4bpp( "CHAPTER4", 256, 62, 1, new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_CHAPTER4_OUT, 0, 0x1FE0 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_CHAPTER4_OUT, 0x1FE0, 32 ) ),
            }.AsReadOnly() );


            var kvpList = new List<KeyValuePair<Shops, string>>( PatcherLib.PSPResources.Lists.ShopNames );
            kvpList.RemoveAll( kvp => kvp.Key == Shops.None || kvp.Key == Shops.Empty );
            kvpList.Sort( ( a, b ) => ( (int)b.Key ).CompareTo( (int)a.Key ) );
            var townNamesList = new List<string>();
            kvpList.ForEach( kvp => townNamesList.Add( kvp.Value ) );
            townNamesList.Add( "??" );
            townNamesList.Add( "Fort Besselat" );

            AbstractImage[] townImages = new AbstractImage[19];
            for ( int i = 0; i < 17; i++ )
            {
                townImages[i] = new PalettedImage8bpp( townNamesList[i], 120, 80, 1, Palette.ColorDepth._16bit, new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDPIC_BIN, 0x220 + 10240 * i, 9600 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDPIC_BIN, 0x14 + 10240 * i, 0x200 ) );
            }
            townImages[17] = new PalettedImage8bpp( "Deep Dungeon", 120, 80, 1, Palette.ColorDepth._16bit, new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDPIC_BIN, 0x220 + 843776, 9600 ),
                new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDPIC_BIN, 0x14 + 843776, 0x200 ) );
            townImages[18] = new PalettedImage8bpp( "Orbonne Monastery", 120, 80, 1, Palette.ColorDepth._16bit, new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDPIC_BIN, 0x220 + 854016, 9600 ),
                new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDPIC_BIN, 0x14 + 854016, 0x200 ) );
            result.Add( townImages.AsReadOnly() );

            var names = PatcherLib.PSPResources.Lists.Wonders;
            AbstractImage[] wonderImages = new AbstractImage[33 - 17];
            for (int i = 17; i < 33; i++)
            {
                wonderImages[i - 17] = new PalettedImage8bpp( names[i - 17], 120, 80, 1, Palette.ColorDepth._16bit, new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDPIC_BIN, 0x220 + 10240 * i, 9600 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDPIC_BIN, 0x14 + 10240 * i, 0x200 ) );
            }
            result.Add( wonderImages.AsReadOnly() );
            
            names = PatcherLib.PSPResources.Lists.Artefacts;
            var artefactsImages = new AbstractImage[45];
            for (int j = 0; j < 45; j++)
            {
                int[] size = artefactsSizes[j];
                artefactsImages[j] = new PalettedImage8bpp( names[j], size[0], size[1], 1, Palette.ColorDepth._16bit, new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDPIC_BIN, 0x220 + artefactsPositions[j], 9600 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.WORLD_WLDPIC_BIN, 0x14 + artefactsPositions[j], 0x200 ) );
            }
            result.Add( artefactsImages.AsReadOnly() );

            result.Add( new AbstractImage[] {
                new PalettedImage8bpp("GAMEOVER", 256, 254, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.EVENT_GAMEOVER_BIN, 0, 0xFE00),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.EVENT_GAMEOVER_BIN, 0xFE00, 512))
            }.AsReadOnly() );
            result.Add( new AbstractImage[] {
                new PalettedImage8bpp("ZODIAC", 256, 254, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.BATTLE_ZODIAC_BIN, 0, 0xFE00),
                    new PatcherLib.Iso.PspIso.KnownPosition(PatcherLib.Iso.FFTPack.Files.BATTLE_ZODIAC_BIN, 0xFE00, 512))
            }.AsReadOnly() );

            List<AbstractImage> maptitles = new List<AbstractImage>();
            var mapTitleNames = PatcherLib.PSPResources.Lists.MapNames;
            for (int i = 0; i < 0x9D800 / 256 / 40 * 2; i++)
            {
                maptitles.Add(
                    new RawNybbleImage( mapTitleNames[i + 1], 256, 40,
                        new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_MAPTITLE_BIN, i * (256 * 40 / 2), 256 * 40 / 2 ) ) );
            }

            result.Add( maptitles.AsReadOnly() );

            return result;
        }
        private static int[][] artefactsSizes = new int[][] 
                {
                    new int[] {80, 88 },
                    new int[] {88, 96 },
                    new int[] {88, 88 },
                    new int[] {104, 80 },
                    new int[] {112, 80 },
                    new int[] {64, 88 },
                    new int[] {64, 96 },
                    new int[] {88, 96 },
                    new int[] {88, 80 },
                    new int[] {96, 80 },
                    new int[] {32, 96 },
                    new int[] {64, 96 },
                    new int[] {96, 64 },
                    new int[] {80, 80 },
                    new int[] {72, 88 },
                    new int[] {72, 88 },
                    new int[] {80, 88 },
                    new int[] {80, 80 },

                    new int[] {72, 96 },
                    new int[] {96, 80 },
                    new int[] {80, 88 },
                    new int[] {80, 88 },
                    new int[] {104, 88 },
                    new int[] {104, 80 },
                    new int[] {88, 96 },
                    new int[] {88, 80 },
                    new int[] {104, 80 },
                    new int[] {80, 96 },
                    new int[] {104, 80 },
                    new int[] {64, 88 },

                    new int[] {88, 96 },
                    new int[] {72, 88 },
                    new int[] {72, 96 },
                    new int[] {88, 80 },
                    new int[] {80, 96 },
                    new int[] {64, 96 },
                    new int[] { 72,96},
                    new int[] {56, 96 },
                    new int[] {96, 80 },
                    new int[] {64, 96 },
                    new int[] {64, 96 },
                    new int[] {64, 96 },
                    new int[] {64, 88 },
                    new int[] {56, 96 },
                    new int[] {72, 80 }
                };
        private static int[] artefactsPositions = new int[] { 337920, 346112, 356352, 366592, 376832, 387072, 395264, 403456, 413696, 421888, 432128, 436224, 444416, 452608, 460800, 468992, 477184, 485376, 493568, 501760, 512000, 520192, 528384, 538624, 548864, 559104, 567296, 577536, 587776, 598016, 606208, 616448, 624640, 632832, 641024, 651264, 659456, 667648, 673792, 684032, 692224, 700416, 708608, 716800, 722944 };

        private static IList<IList<AbstractImage>> BuildPsxImages()
        {
            // 0, 135
            List<IList<AbstractImage>> result = new List<IList<AbstractImage>>();
            IList<AbstractImage> bonusPics = new AbstractImage[36];
            for ( int i = 0; i < 36; i++ )
            {
                bonusPics[i] = new PalettedImage4bpp( string.Format("BONUS{0}",i), 256, 50, 1,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_BONUS_BIN, 0x6800 * i + 135 * 256 / 2, 256 * 250 / 2 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_BONUS_BIN, 0x6800 * i + 0x6400 + 0x20 * 2, 0x20 ) );
            }
            result.Add( bonusPics.AsReadOnly() );
            result.Add( new AbstractImage[] {
                new PalettedImage8bpp("OPNTEX1", 128,256, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 34816+0x220, 128*256),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 34816+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX2", 256,256, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 69632+0x220, 256*256),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 69632+0x14, 256*2)),
                new PalettedImage4bpp("OPNTEX3", 256, 256, 1, 
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 137216+0x40, 256*256/2),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 137216+0x14, 16*2)),
                new PalettedImage4bpp("OPNTEX4", 256, 256, 1, 
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 172032+0x40, 256*256/2),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 172032+0x14, 16*2)),

                new PalettedImage8bpp("OPNTEX5", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 241664+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 241664+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX6", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 288768+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 288768+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX7", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 335872+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 335872+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX8", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 382976+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 382976+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX9", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 430080+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 430080+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX10", 258,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 477184+0x220, 258*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 477184+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX11", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 524288+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 524288+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX12", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 571392+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 571392+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX13", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 618496+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 618496+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX14", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 665600+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 665600+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX15", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 712704+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 712704+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX16", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 759808+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 759808+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX17", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 806912+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 806912+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX18", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 854016+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 854016+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX19", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 901120+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 901120+0x14, 256*2)),
                new PalettedImage8bpp("OPNTEX20", 256,180, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 948224+0x220, 256*180),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 948224+0x14, 256*2)),
            }.AsReadOnly() );
            result.Add( new AbstractImage[] {
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
            }.AsReadOnly() );
            result.Add( new AbstractImage[] {
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
            }.AsReadOnly() );
            result.Add( new AbstractImage[] {
                new Raw16BitImage( "BK_FITR", 256, 128,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.MENU_BK_FITR_TIM, 0x14, 0x10000 ) ),
                new Raw16BitImage( "BK_FITR2", 256, 128,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.MENU_BK_FITR2_TIM, 0x14, 0x10000 ) ),
                new Raw16BitImage( "BK_FITR3", 256, 128,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.MENU_BK_FITR3_TIM, 0x14, 0x10000 ) ),
                new Raw16BitImage( "BK_HONE", 256, 128,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.MENU_BK_HONE_TIM, 0x14, 0x10000 ) ),
                new Raw16BitImage( "BK_HONE2", 256, 128,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.MENU_BK_HONE2_TIM, 0x14, 0x10000 ) ),
                new Raw16BitImage( "BK_HONE3", 256, 128,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.MENU_BK_HONE3_TIM, 0x14, 0x10000 ) ),
                new Raw16BitImage( "BK_SHOP", 256, 128,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.MENU_BK_SHOP_TIM, 0x14, 0x10000 ) ),
                new Raw16BitImage( "BK_SHOP2", 256, 128,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.MENU_BK_SHOP2_TIM, 0x14, 0x10000 ) ),
                new Raw16BitImage( "BK_SHOP3", 256, 128,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.MENU_BK_SHOP3_TIM, 0x14, 0x10000 ) )     
            }.AsReadOnly() );

            AbstractImage[] wldbkImages = new AbstractImage[130];
            for ( int i = 0; i < 130; i++ )
            {
                wldbkImages[i] = new Raw16BitImage( string.Format( "WLDBK{0:000}", i ), 256, 240,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDBK_BIN, 256 * 240 * 2 * i, 256 * 240 * 2 ) );
            }
            result.Add( wldbkImages.AsReadOnly() );

            result.Add( new AbstractImage[] {
                new PalettedImage4bpp( "CHAPTER1", 256, 62, 1, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER1_BIN, 0, 0x1FE0 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER1_BIN, 0x1FE0, 32 ) ),
                new PalettedImage4bpp( "CHAPTER2", 256, 62, 1, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER2_BIN, 0, 0x1FE0 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER2_BIN, 0x1FE0, 32 ) ),
                new PalettedImage4bpp( "CHAPTER3", 256, 62, 1, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER3_BIN, 0, 0x1FE0 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER3_BIN, 0x1FE0, 32 ) ),
                new PalettedImage4bpp( "CHAPTER4", 256, 62, 1, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER4_BIN, 0, 0x1FE0 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER4_BIN, 0x1FE0, 32 ) ),
            }.AsReadOnly() );


            //List<AbstractImage> psxImages = new List<AbstractImage>
            //{
            //    //new PalettedImage8bpp( "WLDTEX", 512, 240, 1, Palette.ColorDepth._16bit, new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2,0x0c+0x200,512*240),
            //    //    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2,0x0c,0x200)),
            //    new PalettedImage8bpp ("OPNTEX", 256,256,1, Palette.ColorDepth._16bit, 
            //        new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 69632+0x214, 256*256),
            //        new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 69632+0x14, 0x200)),
            //};
            result.Add( new AbstractImage[] 
            {
                new Raw16BitImage( "SCEAP", 320, 32,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCEAP_DAT, 0, 20480 ) ),
            }.AsReadOnly() );



            var kvpList = new List<KeyValuePair<Shops, string>>( PatcherLib.PSXResources.Lists.ShopNames );
            kvpList.RemoveAll( kvp => kvp.Key == Shops.None || kvp.Key == Shops.Empty );
            kvpList.Sort( ( a, b ) => ( (int)b.Key ).CompareTo( (int)a.Key ) );
            var townNamesList = new List<string>();
            kvpList.ForEach( kvp => townNamesList.Add( kvp.Value ) );
            townNamesList.Add( "??" );
            townNamesList.Add( "Bethla Garrison" );

            AbstractImage[] townImages = new AbstractImage[19];
            for (int i = 0; i < 17; i++)
            {
                townImages[i] = new PalettedImage8bpp( townNamesList[i], 120, 80, 1, Palette.ColorDepth._16bit, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDPIC_BIN, 0x220 + 10240 * i, 9600 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDPIC_BIN, 0x14 + 10240 * i, 0x200 ) );
            }
            townImages[17] = new PalettedImage8bpp( "Deep Dungeon", 120, 80, 1, Palette.ColorDepth._16bit, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDPIC_BIN, 0x220 + 843776, 9600 ),
                new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDPIC_BIN, 0x14 + 843776, 0x200 ) );
            townImages[18] = new PalettedImage8bpp( "Orbonne Monastery", 120, 80, 1, Palette.ColorDepth._16bit, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDPIC_BIN, 0x220 + 854016, 9600 ),
                new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDPIC_BIN, 0x14 + 854016, 0x200 ) );
            result.Add( townImages.AsReadOnly() );

            var names = PatcherLib.PSXResources.Lists.UnexploredLands;
            AbstractImage[] unexploredImages = new AbstractImage[33-17];
            for (int i = 17; i < 33; i++)
            {
                unexploredImages[i - 17] = new PalettedImage8bpp( names[i - 17], 120, 80, 1, Palette.ColorDepth._16bit, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDPIC_BIN, 0x220 + 10240 * i, 9600 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDPIC_BIN, 0x14 + 10240 * i, 0x200 ) );
            }
            result.Add( unexploredImages.AsReadOnly() );

            names = PatcherLib.PSXResources.Lists.Treasures;
            AbstractImage[] treasureImages = new AbstractImage[45];
            for (int j = 0; j < 45; j++)
            {
                int[] size = artefactsSizes[j];
                treasureImages[j] = new PalettedImage8bpp( names[j], size[0], size[1], 1, Palette.ColorDepth._16bit, new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDPIC_BIN, 0x220 + artefactsPositions[j], 9600 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDPIC_BIN, 0x14 + artefactsPositions[j], 0x200 ) );
            }

            result.Add( treasureImages.AsReadOnly() );

            result.Add( new AbstractImage[] {
                new PalettedImage8bpp("GAMEOVER", 256, 254, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.EVENT_GAMEOVER_BIN, 0, 0xFE00),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.EVENT_GAMEOVER_BIN, 0xFE00, 512))
            }.AsReadOnly() );

            result.Add( new AbstractImage[] {
                new PalettedImage8bpp("ZODIAC", 256, 254, 1, Palette.ColorDepth._16bit,
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.BATTLE_ZODIAC_BIN, 0, 0xFE00),
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.BATTLE_ZODIAC_BIN, 0xFE00, 512))
            }.AsReadOnly() );

            List<AbstractImage> maptitles = new List<AbstractImage>();
            var mapTitleNames = PatcherLib.PSXResources.Lists.MapNames;
            for (int i = 0; i < 0x4b000 / 256 / 20 * 2; i++)
            {
                maptitles.Add(
                    new RawNybbleImage( mapTitleNames[i + 1], 256, 20,
                        new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_MAPTITLE_BIN, i * (256 * 20 / 2), 256 * 20 / 2 ) ) );
            }

            result.Add( maptitles.AsReadOnly() );

            return result.AsReadOnly();
        }


        private IList<IList<AbstractImage>> images;

        public AbstractImage this[int i, int j]
        {
            get { return this.images[i][j]; }
        }

        public IList<AbstractImage> this[int i]
        {
            get { return this.images[i]; }
        }

        public int ListCount { get { return images.Count; } }

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

        private AllOtherImages( IList<IList<AbstractImage>> images )
        {
            this.images = images.AsReadOnly();
        }

    }
}
