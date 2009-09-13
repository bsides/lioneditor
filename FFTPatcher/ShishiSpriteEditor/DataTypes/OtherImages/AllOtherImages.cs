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
                    name = img.GetSaveFileName();

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
                    name = img.GetSaveFileName();
                    //if (img.Context == Context.US_PSX )
                    //{
                    //    var pos = img.Position as PatcherLib.Iso.PsxIso.KnownPosition;
                    //    name = string.Format( "{0}_{1}.png", pos.Sector, pos.StartLocation );
                    //}
                    //else if (img.Position is PatcherLib.Iso.PspIso.KnownPosition)
                    //{
                    //    var pos = img.Position as PatcherLib.Iso.PspIso.KnownPosition;
                    //    name = string.Format( "{0}_{1}.png", pos.SectorEnum, pos.StartLocation );
                    //}

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

            result.Add( new AbstractImage[] {
                new Greyscale4bppImage("World map titles",256,256, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)900,0x00, 0x8000))
            } );

            result.Add( new AbstractImage[] {
                new PalettedImage4bpp("New Game (Selected)", 128,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x90, 0x400),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x50, 0x40)),
                new PalettedImage4bpp("New Game (Unselected)", 128,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x4D0, 0x400),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x490, 0x40)),
                new PalettedImage4bpp("Continue (Selected)", 128,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x910, 0x400),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x8D0, 0x40)),
                new PalettedImage4bpp("Continue (Unselected)", 128,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0xD50, 0x400),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0xD10, 0x40)),
                new PalettedImage4bpp("Tutorial (Selected)", 128,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x1190, 0x400),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x1150, 0x40)),
                new PalettedImage4bpp("Tutorial (Unselected)", 128,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x15D0, 0x400),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x1590, 0x40)),
                new PalettedImage4bpp("Copyright", 512,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x1a10, 0x1000),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x19D0, 0x40)),
                new PalettedImage8bpp("Title screen", 512,512, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x2e10, 512*512),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)936, 0x2A10, 0x400)),
            } );
            result.Add( new AbstractImage[] {
                new PalettedImage4bpp("Hourglass", 256,64, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x1a0, 0x2000),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x160, 0x40)),
                new PalettedImage4bpp("Some numbers", 128,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x21e0, 0x400),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x21a0, 0x40)),
                new PalettedImage4bpp("Circle", 32,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x2620, 0x200),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x25e0, 0x40)),
                new PalettedImage4bpp("Cross", 32,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x2860, 0x200),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x2820, 0x40)),
                new PalettedImage4bpp("", 128,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x2aa0, 0x800),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x2a60, 0x40)),
                new PalettedImage4bpp("Some other numbers", 256,64, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x32e0, 0x2000),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x32a0, 0x40)),
                new PalettedImage4bpp("Red", 32,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x5320, 0x100),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x52e0, 0x40)),
                new PalettedImage4bpp("1P", 16,8, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x55A0, 0x40),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x5560, 0x40)),
                new PalettedImage4bpp("2P", 16,8, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x5620, 0x40),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x55e0, 0x40)),
                new PalettedImage4bpp("Red", 32,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x56a0, 0x100),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x5660, 0x40)),
                new PalettedImage4bpp("Blue", 32,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x57e0, 0x100),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x57a0, 0x40)),
                new PalettedImage4bpp("TRAP", 64,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x5920, 0x200),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x58e0, 0x40)),
                new PalettedImage4bpp("Death Sentence Trap", 32,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x5b60, 0x200),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x5b20, 0x40)),
                new PalettedImage4bpp("Sleep Trap", 32,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x5da0, 0x200),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x5d60, 0x40)),
                new PalettedImage4bpp("??", 32,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x5fe0, 0x200),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x5fa0, 0x40)),
                new PalettedImage4bpp("Bomb Trap", 32,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x6220, 0x200),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x61e0, 0x40)),
                new PalettedImage4bpp("Poison Trap", 32,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x6460, 0x200),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x6420, 0x40)),
                new PalettedImage4bpp("Poison Trap", 32,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x6460, 0x200),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x6660, 0x40)),
                new PalettedImage4bpp("??", 32,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x68a0+0x40, 0x200),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x68a0, 0x40)),
                new PalettedImage4bpp("??", 32,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x6ae0+0x40, 0x200),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x6ae0, 0x40)),
                new PalettedImage4bpp("??", 32,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x6d20+0x40, 0x200),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x6d20, 0x40)),

                new PalettedImage4bpp("??", 64,32, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x6f60+0x40, 0x400),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x6f60, 0x40)),
                new PalettedImage4bpp("Even more numbers", 128,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x73a0+0x40, 0x400),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x73a0, 0x40)),
                new PalettedImage4bpp("Button symbols", 128,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x77e0+0x40, 0x400),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x77e0, 0x40)),
                new PalettedImage4bpp("Button symbols", 128,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x7c20+0x40, 0x400),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x7c20, 0x40)),
                new PalettedImage4bpp("Square", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8060+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8060, 0x40)),
                new PalettedImage4bpp("Up", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8120+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8120, 0x40)),
                new PalettedImage4bpp("Down", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x81e0+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x81e0, 0x40)),
                new PalettedImage4bpp("Left", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x82a0+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x82a0, 0x40)),
                new PalettedImage4bpp("Right", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8360+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8360, 0x40)),
                new PalettedImage4bpp("Cross", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8420+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8420, 0x40)),
                new PalettedImage4bpp("Circle", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x84e0+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x84e0, 0x40)),
                new PalettedImage4bpp("Triangle", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x85a0+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x85a0, 0x40)),
                new PalettedImage4bpp("Square", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8660+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8660, 0x40)),
                new PalettedImage4bpp("Up", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8720+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8720, 0x40)),
                new PalettedImage4bpp("Down", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x87e0+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x87e0, 0x40)),
                new PalettedImage4bpp("Left", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x88a0+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x88a0, 0x40)),
                new PalettedImage4bpp("Right", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8960+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8960, 0x40)),
                new PalettedImage4bpp("Cross", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8a20+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8a20, 0x40)),
                new PalettedImage4bpp("2P", 16,16, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8ae0+0x40, 0x80),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8ae0, 0x40)),

                new PalettedImage4bpp("Dialogue box", 64,64, 1, Palette.ColorDepth._32bit, new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8ba0+0x40, 0x800),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)898, 0x8ba0, 0x40)),
            } );

            IList<AbstractImage> mapThumbnails = new AbstractImage[70];
            IList<string> mapNames = PatcherLib.PSPResources.Lists.MapNames;
            for (int i = 0; i < 70; i++)
            {
                mapThumbnails[i] = new PalettedImage8bpp( string.Format( "Thumbnail - {0}", mapNames[i] ), 128, 128, 1, Palette.ColorDepth._32bit,
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)899, 0x640 + 0x4400 * i, 128 * 128 ),
                    new PatcherLib.Iso.PspIso.KnownPosition( (PatcherLib.Iso.FFTPack.Files)899, 0x240+0x4400*i, 256 * 4 ) );
            }
            result.Add( mapThumbnails.AsReadOnly() );

            IList<AbstractImage> bonusPics = new AbstractImage[71];
            for ( int i = 0; i < 71; i++ )
            {
                bonusPics[i] = new PalettedImage4bpp( string.Format( "BONUS{0}", i ), 256, 200, 1,
                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_BONUS_BIN, 0x6800 * i, 256 * 200 / 2 ),
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


            var kvpList = new List<KeyValuePair<ShopsFlags, string>>( PatcherLib.PSPResources.Lists.ShopNames );
            kvpList.RemoveAll( kvp => kvp.Key == ShopsFlags.None || kvp.Key == ShopsFlags.Empty );
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
            result.Add( new AbstractImage[] {
                new PSXWorldMap(),
                new StupidTM2Image( "???", 128, 256,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 190476, 256 * 2 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 190996, 192404 - 190996 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 192412, 192512 - 192412 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 192524, 192552 - 192524 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 192560, 194480 - 192560 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 194488, 194560 - 194488 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 194572, 194628 - 194572 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 194636, 196556 - 194636 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 196564, 196608 - 196564 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 196620, 196704 - 196620 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 196712, 198632 - 196712 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 198640, 198656 - 198640 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 198668, 198780 - 198668 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 198788, 200580 - 198788 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 200588, 200704 - 200588 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 200716, 200728 - 200716 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 200736, 202656 - 200736 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 202664, 202752 - 202664 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 202764, 202804 - 202764 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 202812, 204732 - 202812 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 204740, 204800 - 204740 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 204812, 204880 - 204812 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 204888, 206808 - 204888 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 206816, 206848 - 206816 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 206860, 206956 - 206860 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 206964, 208884 - 206964 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 208892, 208896 - 208892 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 208908, 209032 - 208908 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 209040, 210832 - 209040 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 210840, 210944 - 210840 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 210956, 210980 - 210956 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 210988, 212908 - 210988 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 212916, 212992 - 212916 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 213004, 213056 - 213004 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 213064, 214984 - 213064 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 214992, 215040 - 214992 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 215052, 215132 - 215052 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 215140, 217060 - 215140 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 217068, 217088 - 217068 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 217100, 217208 - 217100 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 217216, 219008 - 217216 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 219016, 219136 - 219016 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 219148, 219156 - 219148 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 219164, 221084 - 219164 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 221092, 221184 - 221092 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 221196, 221232 - 221196 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 221240, 223160 - 221240 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 223168, 223232 - 223168 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 223244, 223308 - 223244 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 223316, 224212 - 223316 ) ),
                new StupidTM2Image4bpp( "???", 256, 256,
                    //new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 237580, 238092 - 237580 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 239628, 239660 - 239628 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 239668, 241588 - 239668 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 241596, 241664 - 241596 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 241676, 241736 - 241676 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 241744, 243664 - 241744 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 243672, 243712 - 243672 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 243724, 243812 - 243724 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 243820, 245740 - 243820 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 245748, 245760 - 245748 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 245772, 245888 - 245772 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 245896, 247688 - 245896 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 247696, 247808 - 247696 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 247820, 247836 - 247820 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 247844, 249764 - 247844 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 249772, 249856 - 249772 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 249868, 249912 - 249868 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 249920, 251840 - 249920 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 251848, 251904 - 251848 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 251916, 251988 - 251916 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 251996, 253916 - 251996 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 253924, 253952 - 253924 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 253964, 254064 - 253964 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 254072, 255992 - 254072 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 256012, 257932 - 256012 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 257940, 258048 - 257940 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 258060, 258080 - 258060 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 258088, 260008 - 258088 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 260016, 260096 - 260016 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 260108, 260156 - 260108 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 260164, 262084 - 260164 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 262092, 262144 - 262092 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 262156, 262232 - 262156 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 262240, 264160 - 262240 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 264168, 264192 - 264168 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 264204, 264308 - 264204 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 264316, 266236 - 264316 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 266252, 268172 - 266252 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 268180, 268288 - 268180 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 268300, 268320 - 268300 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 268328, 270248 - 268328 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 270256, 270336 - 270256 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 270348, 270396 - 270348 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 270404, 272324 - 270404 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 272332, 272384 - 272332 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 272396, 272472 - 272396 ),
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 272480, 272864 - 272480 ) )
            } );



            IList<AbstractImage> bonusPics = new AbstractImage[36];
            for (int i = 0; i < 36; i++)
            {
                bonusPics[i] = new PalettedImage4bpp( string.Format( "BONUS{0}", i ), 256, 200, 1,
                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_BONUS_BIN, 0x6800 * i, 256 * 200 / 2 ),
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
                //new PalettedImage4bpp("OPNTEX3", 256, 256, 1, 
                //    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 137216+0x40, 256*256/2),
                //    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 137216+0x14, 16*2)),
                //new PalettedImage4bpp("OPNTEX4", 256, 256, 1, 
                //    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 172032+0x40, 256*256/2),
                //    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 172032+0x14, 16*2)),

                new Greyscale4bppImage("OPNTEX3", 256, 256, 
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 137216+0x40, 256*256/2)),
                new Greyscale4bppImage("OPNTEX4", 256, 256, 
                    new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.OPEN_OPNTEX_BIN, 172032+0x40, 256*256/2)),

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
                new Greyscale4bppImage("CHAPTER1", 256, 62,new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER1_BIN, 0, 0x1FE0 )),
                new Greyscale4bppImage("CHAPTER2", 256, 62,new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER2_BIN, 0, 0x1FE0 )),
                new Greyscale4bppImage("CHAPTER3", 256, 62,new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER3_BIN, 0, 0x1FE0 )),
                new Greyscale4bppImage("CHAPTER4", 256, 62,new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_CHAPTER4_BIN, 0, 0x1FE0 )),
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



            var kvpList = new List<KeyValuePair<ShopsFlags, string>>( PatcherLib.PSXResources.Lists.ShopNames );
            kvpList.RemoveAll( kvp => kvp.Key == ShopsFlags.None || kvp.Key == ShopsFlags.Empty );
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
            maptitles.Add(
                new RawNybbleImage( "Orbonne dupe", 256, 20,
                    new PatcherLib.Iso.PsxIso.KnownPosition( (PatcherLib.Iso.PsxIso.Sectors)6296, 0x680, 256 * 20 / 2 ) ) );

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
            var aoi = new AllOtherImages( BuildPsxImages() );
            return aoi;
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
                var aoi = GetPsx();
#if STUPIDBONUSBIN
                var bonus = aoi[1];
                using (Image img = Image.FromFile( @"N:\My Dropbox\FFTC\Tests\bonus.psx.Complete.gif" ))
                using (Bitmap bmp = new Bitmap( img ))
                {
                    System.Drawing.Imaging.ColorPalette pal = img.Palette;
                    for (int i = 0; i < 36; i++)
                    {
                        List<byte> bytes = new List<byte>();
                        int startY = i * 208;
                        const int width = 256;
                        const int height = 200;
                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < width; x++)
                            {
                                Color c = bmp.GetPixel( x, y + startY );
                                int cIndex = pal.Entries.IndexOf( c );
                                if (cIndex == -1 || cIndex >= 16) throw new Exception();

                                bytes.Add( (byte)cIndex );
                            }
                        }

                        List<byte> realBytes = new List<byte>();
                        for (int j = 0; j < width * height; j += 2)
                        {
                            realBytes.Add( (byte)(((bytes[j + 1] & 0xF) << 4) | (bytes[j] & 0xF)) );
                        }

                        PatcherLib.Iso.PsxIso.PatchPsxIso(
                            iso, new PatchedByteArray( PatcherLib.Iso.PsxIso.Sectors.EVENT_BONUS_BIN,
                                i * 0x6800, realBytes.ToArray() ) );
                    }
                }
#endif
                return aoi;
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
