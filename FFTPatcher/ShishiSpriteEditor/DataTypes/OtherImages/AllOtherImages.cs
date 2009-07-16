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
        private static IList<IList<AbstractImage>> BuildPspImages()
        {
            List<IList<AbstractImage>> result = new List<IList<AbstractImage>>();
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
            List<IList<AbstractImage>> result = new List<IList<AbstractImage>>();

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
