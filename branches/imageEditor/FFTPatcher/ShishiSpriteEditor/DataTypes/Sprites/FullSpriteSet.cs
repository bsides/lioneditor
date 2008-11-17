using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FFTPatcher.Datatypes;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Windows.Forms;

namespace FFTPatcher.SpriteEditor
{
    public class FullSpriteSet
    {
        private IList<AbstractSprite> sprites;
        public IList<AbstractSprite> Sprites
        {
            get { return sprites.AsReadOnly(); }
        }

        public ImageList Thumbnails { get; private set; }

        private FullSpriteSet( IList<AbstractSprite> sprites, System.ComponentModel.BackgroundWorker worker, int tasksComplete, int tasks )
        {
            bool haveWorker = worker != null;
            if ( haveWorker )
                worker.ReportProgress( ( tasksComplete++ * 100 ) / tasks, "Sorting" );
            sprites.Sort( ( a, b ) => a.Name.CompareTo( b.Name ) );
            this.sprites = sprites;
            Thumbnails = new ImageList();
            Thumbnails.ImageSize = new System.Drawing.Size( 80, 48 );
            foreach ( var sprite in sprites )
            {
                if ( haveWorker )
                    worker.ReportProgress( ( tasksComplete++ * 100 ) / tasks, string.Format( "Generating thumbnail for {0}", sprite.Name ) );

                Thumbnails.Images.Add( sprite.Name, sprite.GetThumbnail() );
                if ( sprite is AbstractShapedSprite )
                {
                    ( sprite as AbstractShapedSprite ).CacheFrames();
                }
            }
        }

        private static FullSpriteSet DoInitPSX( Stream iso )
        {
            var sprites = new List<AbstractSprite>();
            sprites.Add( new MonsterSprite( "ADORA", new string[] { "ADORA.SPR" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ADORA_SPR, 0, 47100 ) ) );
            sprites.Add( new MonsterSprite( "ARLI", new string[] { "ARLI.SPR", "ARLI2.SP2" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ARLI_SPR, 0, 41475 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ARLI2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "BEHI", new string[] { "BEHI.SPR", "BEHI2.SP2" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BEHI_SPR, 0, 46393 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BEHI2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "BIBUROS", new string[] { "BIBUROS.SPR", "BIBU2.SP2" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BIBUROS_SPR, 0, 44353 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BIBU2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "BOM", new string[] { "BOM.SPR", "BOM2.SP2" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BOM_SPR, 0, 42546 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BOM2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "DEMON", new string[] { "DEMON.SPR", "DEMON2.SP2" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DEMON_SPR, 0, 45648 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DEMON2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "DORA1", new string[] { "DORA1.SPR" }, 
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DORA1_SPR, 0, 46754 ) ) );
            sprites.Add( new MonsterSprite( "DORA2", new string[] { "DORA2.SPR", "DORA22.SP2" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DORA2_SPR, 0, 46437 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DORA22_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "HASYU", new string[] { "HASYU.SPR" }, 
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.HASYU_SPR, 0, 47430 ) ) );
            sprites.Add( new MonsterSprite( "HEBI", new string[] { "HEBI.SPR" }, 
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.HEBI_SPR, 0, 48525 ) ) );
            sprites.Add( new MonsterSprite( "HYOU", new string[] { "HYOU.SPR", "HYOU2.SP2" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.HYOU_SPR, 0, 43553 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.HYOU2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "KI", new string[] { "KI.SPR" }, 
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.KI_SPR, 0, 45205 ) ) );
            sprites.Add( new MonsterSprite( "KYUKU", new string[] { "KYUKU.SPR" }, 
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.KYUKU_SPR, 0, 48094 ) ) );
            sprites.Add( new MonsterSprite( "MINOTA", new string[] { "MINOTA.SPR", "MINOTA2.SP2" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MINOTA_SPR, 0, 47737 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MINOTA2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "MOL", new string[] { "MOL.SPR", "MOL2.SP2" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MOL_SPR, 0, 47102 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MOL2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "REZE_D", new string[] { "REZE_D.SPR" }, 
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.REZE_D_SPR, 0, 46744 ) ) );
            sprites.Add( new MonsterSprite( "TETSU", new string[] { "TETSU.SPR", "IRON2.SP2", "IRON3.SP2", "IRON4.SP2", "IRON5.SP2" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.TETSU_SPR, 0, 46001 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.IRON2_SP2, 0, 32768 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.IRON3_SP2, 0, 32768 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.IRON4_SP2, 0, 32768 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.IRON5_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "TORI", new string[] { "TORI.SPR", "TORI2.SP2" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.TORI_SPR, 0, 43332 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.TORI2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "URI", new string[] { "URI.SPR", "URI2.SP2" },
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.URI_SPR, 0, 40595 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.URI2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "VERI", new string[] { "VERI.SPR" }, 
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.VERI_SPR, 0, 46848 ) ) );
            sprites.Add( new MonsterSprite( "YUREI", new string[] { "YUREI.SPR" }, 
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.YUREI_SPR, 0, 41970 ) ) );
            sprites.Add( new MonsterSprite( "ZARUE", new string[] { "ZARUE.SPR" }, 
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ZARUE_SPR, 0, 47018 ) ) );
            sprites.Add( new TYPE1Sprite( "AGURI", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.AGURI_SPR, 0, 43309 ) ) );
            sprites.Add( new TYPE1Sprite( "ARU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ARU_SPR, 0, 43358 ) ) );
            sprites.Add( new TYPE1Sprite( "BARUNA", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BARUNA_SPR, 0, 44172 ) ) );
            sprites.Add( new TYPE1Sprite( "BEIO", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BEIO_SPR, 0, 43746 ) ) );
            sprites.Add( new TYPE1Sprite( "CLOUD", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.CLOUD_SPR, 0, 42953 ) ) );
            sprites.Add( new TYPE1Sprite( "DAISU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DAISU_SPR, 0, 43648 ) ) );
            sprites.Add( new TYPE1Sprite( "DILY", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DILY_SPR, 0, 43462 ) ) );
            sprites.Add( new TYPE1Sprite( "DILY2", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DILY2_SPR, 0, 43163 ) ) );
            sprites.Add( new TYPE1Sprite( "DILY3", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DILY3_SPR, 0, 44422 ) ) );
            sprites.Add( new TYPE1Sprite( "GANDO", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.GANDO_SPR, 0, 42967 ) ) );
            sprites.Add( new TYPE1Sprite( "GARU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.GARU_SPR, 0, 43687 ) ) );
            sprites.Add( new TYPE1Sprite( "GOB", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.GOB_SPR, 0, 41268 ) ) );
            sprites.Add( new TYPE1Sprite( "GORU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.GORU_SPR, 0, 44734 ) ) );
            sprites.Add( new TYPE1Sprite( "H61", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.H61_SPR, 0, 44172 ) ) );
            sprites.Add( new TYPE1Sprite( "H75", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.H75_SPR, 0, 43476 ) ) );
            sprites.Add( new TYPE1Sprite( "H76", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.H76_SPR, 0, 43557 ) ) );
            sprites.Add( new TYPE1Sprite( "H77", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.H77_SPR, 0, 43560 ) ) );
            sprites.Add( new TYPE1Sprite( "H78", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.H78_SPR, 0, 43560 ) ) );
            sprites.Add( new TYPE1Sprite( "H80", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.H80_SPR, 0, 43362 ) ) );
            sprites.Add( new TYPE1Sprite( "H81", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.H81_SPR, 0, 43462 ) ) );
            sprites.Add( new TYPE1Sprite( "H85", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.H85_SPR, 0, 43362 ) ) );
            sprites.Add( new TYPE1Sprite( "KANBA", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.KANBA_SPR, 0, 43309 ) ) );
            sprites.Add( new TYPE1Sprite( "KNIGHT_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.KNIGHT_M_SPR, 0, 44406 ) ) );
            sprites.Add( new TYPE1Sprite( "KNIGHT_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.KNIGHT_W_SPR, 0, 44433 ) ) );
            sprites.Add( new TYPE1Sprite( "MARA", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MARA_SPR, 0, 42967 ) ) );
            sprites.Add( new TYPE1Sprite( "MINA_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MINA_M_SPR, 0, 43433 ) ) );
            sprites.Add( new TYPE1Sprite( "MINA_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MINA_W_SPR, 0, 43529 ) ) );
            sprites.Add( new TYPE1Sprite( "MONK_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MONK_M_SPR, 0, 43336 ) ) );
            sprites.Add( new TYPE1Sprite( "MONK_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MONK_W_SPR, 0, 43195 ) ) );
            sprites.Add( new TYPE1Sprite( "MUSU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MUSU_SPR, 0, 43687 ) ) );
            sprites.Add( new TYPE1Sprite( "NINJA_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.NINJA_M_SPR, 0, 43572 ) ) );
            sprites.Add( new TYPE1Sprite( "NINJA_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.NINJA_W_SPR, 0, 43622 ) ) );
            sprites.Add( new TYPE1Sprite( "ORAN", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ORAN_SPR, 0, 44368 ) ) );
            sprites.Add( new TYPE1Sprite( "ORU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ORU_SPR, 0, 44593 ) ) );
            sprites.Add( new TYPE1Sprite( "RAMUZA", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.RAMUZA_SPR, 0, 43354 ) ) );
            sprites.Add( new TYPE1Sprite( "RAMUZA2", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.RAMUZA2_SPR, 0, 43154 ) ) );
            sprites.Add( new TYPE1Sprite( "RAMUZA3", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.RAMUZA3_SPR, 0, 43009 ) ) );
            sprites.Add( new TYPE1Sprite( "RUDO", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.RUDO_SPR, 0, 43817 ) ) );
            sprites.Add( new TYPE1Sprite( "RYU_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.RYU_M_SPR, 0, 44265 ) ) );
            sprites.Add( new TYPE1Sprite( "RYU_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.RYU_W_SPR, 0, 43599 ) ) );
            sprites.Add( new TYPE1Sprite( "SAMU_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.SAMU_M_SPR, 0, 44235 ) ) );
            sprites.Add( new TYPE1Sprite( "SAMU_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.SAMU_W_SPR, 0, 44495 ) ) );
            sprites.Add( new TYPE1Sprite( "SUKERU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.SUKERU_SPR, 0, 42442 ) ) );
            sprites.Add( new TYPE1Sprite( "THIEF_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.THIEF_M_SPR, 0, 43670 ) ) );
            sprites.Add( new TYPE1Sprite( "THIEF_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.THIEF_W_SPR, 0, 43442 ) ) );
            sprites.Add( new TYPE1Sprite( "VORU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.VORU_SPR, 0, 43554 ) ) );
            sprites.Add( new TYPE1Sprite( "WIGU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.WIGU_SPR, 0, 43748 ) ) );
            sprites.Add( new TYPE1Sprite( "YUMI_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.YUMI_M_SPR, 0, 43233 ) ) );
            sprites.Add( new TYPE1Sprite( "YUMI_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.YUMI_W_SPR, 0, 43107 ) ) );
            sprites.Add( new TYPE1Sprite( "ZARU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ZARU_SPR, 0, 43521 ) ) );
            sprites.Add( new TYPE1Sprite( "ZARU2", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ZARU2_SPR, 0, 43521 ) ) );
            sprites.Add( new TYPE2Sprite( "AJORA", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.AJORA_SPR, 0, 43822 ) ) );
            sprites.Add( new TYPE2Sprite( "ARUFU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ARUFU_SPR, 0, 43325 ) ) );
            sprites.Add( new TYPE2Sprite( "ARUMA", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ARUMA_SPR, 0, 43822 ) ) );
            sprites.Add( new TYPE2Sprite( "BARITEN", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BARITEN_SPR, 0, 43955 ) ) );
            sprites.Add( new TYPE2Sprite( "BARU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BARU_SPR, 0, 44632 ) ) );
            sprites.Add( new TYPE2Sprite( "DORA", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DORA_SPR, 0, 44442 ) ) );
            sprites.Add( new TYPE2Sprite( "ERU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ERU_SPR, 0, 43909 ) ) );
            sprites.Add( new TYPE2Sprite( "FUSUI_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.FUSUI_M_SPR, 0, 43845 ) ) );
            sprites.Add( new TYPE2Sprite( "FUSUI_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.FUSUI_W_SPR, 0, 43812 ) ) );
            sprites.Add( new TYPE2Sprite( "FYUNE", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.FYUNE_SPR, 0, 44698 ) ) );
            sprites.Add( new TYPE2Sprite( "GIN_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.GIN_M_SPR, 0, 44623 ) ) );
            sprites.Add( new TYPE2Sprite( "GYUMU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.GYUMU_SPR, 0, 43822 ) ) );
            sprites.Add( new TYPE2Sprite( "H79", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.H79_SPR, 0, 43207 ) ) );
            sprites.Add( new TYPE2Sprite( "H82", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.H82_SPR, 0, 43822 ) ) );
            sprites.Add( new TYPE2Sprite( "H83", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.H83_SPR, 0, 43332 ) ) );
            sprites.Add( new TYPE2Sprite( "HIME", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.HIME_SPR, 0, 44670 ) ) );
            sprites.Add( new TYPE2Sprite( "IKA", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.IKA_SPR, 0, 42126 ) ) );
            sprites.Add( new TYPE2Sprite( "ITEM_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ITEM_M_SPR, 0, 44438 ) ) );
            sprites.Add( new TYPE2Sprite( "ITEM_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ITEM_W_SPR, 0, 43955 ) ) );
            sprites.Add( new TYPE2Sprite( "KURO_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.KURO_M_SPR, 0, 45623 ) ) );
            sprites.Add( new TYPE2Sprite( "KURO_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.KURO_W_SPR, 0, 44669 ) ) );
            sprites.Add( new TYPE2Sprite( "LEDY", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.LEDY_SPR, 0, 43325 ) ) );
            sprites.Add( new TYPE2Sprite( "MONO_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MONO_M_SPR, 0, 44371 ) ) );
            sprites.Add( new TYPE2Sprite( "MONO_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MONO_W_SPR, 0, 43478 ) ) );
            sprites.Add( new TYPE2Sprite( "ODORI_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ODORI_W_SPR, 0, 43332 ) ) );
            sprites.Add( new TYPE2Sprite( "ONMYO_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ONMYO_M_SPR, 0, 43886 ) ) );
            sprites.Add( new TYPE2Sprite( "ONMYO_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ONMYO_W_SPR, 0, 44626 ) ) );
            sprites.Add( new TYPE2Sprite( "RAFA", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.RAFA_SPR, 0, 43207 ) ) );
            sprites.Add( new TYPE2Sprite( "RAGU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.RAGU_SPR, 0, 45379 ) ) );
            sprites.Add( new TYPE2Sprite( "REZE", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.REZE_SPR, 0, 44187 ) ) );
            sprites.Add( new TYPE2Sprite( "SAN_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.SAN_M_SPR, 0, 44395 ) ) );
            sprites.Add( new TYPE2Sprite( "SAN_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.SAN_W_SPR, 0, 44741 ) ) );
            sprites.Add( new TYPE2Sprite( "SERIA", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.SERIA_SPR, 0, 43332 ) ) );
            sprites.Add( new TYPE2Sprite( "SIMON", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.SIMON_SPR, 0, 45924 ) ) );
            sprites.Add( new TYPE2Sprite( "SIRO_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.SIRO_M_SPR, 0, 44378 ) ) );
            sprites.Add( new TYPE2Sprite( "SIRO_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.SIRO_W_SPR, 0, 47285 ) ) );
            sprites.Add( new TYPE2Sprite( "SOURYO", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.SOURYO_SPR, 0, 45899 ) ) );
            sprites.Add( new TYPE2Sprite( "SYOU_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.SYOU_M_SPR, 0, 45741 ) ) );
            sprites.Add( new TYPE2Sprite( "SYOU_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.SYOU_W_SPR, 0, 44838 ) ) );
            sprites.Add( new TYPE2Sprite( "TOKI_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.TOKI_M_SPR, 0, 44348 ) ) );
            sprites.Add( new TYPE2Sprite( "TOKI_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.TOKI_W_SPR, 0, 44543 ) ) );
            sprites.Add( new TYPE2Sprite( "WAJU_M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.WAJU_M_SPR, 0, 44283 ) ) );
            sprites.Add( new TYPE2Sprite( "WAJU_W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.WAJU_W_SPR, 0, 44062 ) ) );
            sprites.Add( new TYPE2Sprite( "ZARUMOU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ZARUMOU_SPR, 0, 45897 ) ) );
            sprites.Add( new ARUTE( IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ARUTE_SPR, 0, 47888 ) ) );
            sprites.Add( new CYOKO( IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.CYOKO_SPR, 0, 49572 ) ) );
            sprites.Add( new KANZEN( IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.KANZEN_SPR, 0, 48194 ) ) );
            sprites.Add( new ShortSprite( "10M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE._10M_SPR, 0, 37377 ) ) );
            sprites.Add( new ShortSprite( "10W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE._10W_SPR, 0, 37377 ) ) );
            sprites.Add( new ShortSprite( "20M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE._20M_SPR, 0, 37377 ) ) );
            sprites.Add( new ShortSprite( "20W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE._20W_SPR, 0, 37377 ) ) );
            sprites.Add( new ShortSprite( "40M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE._40M_SPR, 0, 37377 ) ) );
            sprites.Add( new ShortSprite( "40W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE._40W_SPR, 0, 37377 ) ) );
            sprites.Add( new ShortSprite( "60M", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE._60M_SPR, 0, 37377 ) ) );
            sprites.Add( new ShortSprite( "60W", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE._60W_SPR, 0, 37377 ) ) );
            sprites.Add( new ShortSprite( "CYOMON1", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.CYOMON1_SPR, 0, 37377 ) ) );
            sprites.Add( new ShortSprite( "CYOMON2", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.CYOMON2_SPR, 0, 37377 ) ) );
            sprites.Add( new ShortSprite( "CYOMON3", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.CYOMON3_SPR, 0, 37377 ) ) );
            sprites.Add( new ShortSprite( "CYOMON4", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.CYOMON4_SPR, 0, 37377 ) ) );
            sprites.Add( new ShortSprite( "FURAIA", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.FURAIA_SPR, 0, 37377 ) ) );

            sprites.Sort( ( a, b ) => a.Name.CompareTo( b.Name ) );

            //return new FullSpriteSet( sprites );
            return null;
        }

        private static FullSpriteSet DoInitPSP( Stream iso )
        {
            
            var sprites = new List<AbstractSprite>();
            sprites.Add( new MonsterSprite( "ADORA", new string[] { "ADORA.SPR" }, 
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ADORA_SPR ) ) );
            sprites.Add( new MonsterSprite( "ARLI", new string[] { "ARLI.SPR", "ARLI2.SP2" },
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ARLI_SPR ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ARLI2_SP2 ) ) );
            sprites.Add( new MonsterSprite( "BEHI", new string[] { "BEHI.SPR", "BEHI2.SP2" },
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.BEHI_SPR ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.BEHI2_SP2 ) ) );
            sprites.Add( new MonsterSprite( "BIBUROS", new string[] { "BIBUROS.SPR", "BIBU2.SP2" },
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.BIBUROS_SPR ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.BIBU2_SP2 ) ) );
            sprites.Add( new MonsterSprite( "BOM", new string[] { "BOM.SPR", "BOM2.SP2" },
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.BOM_SPR ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.BOM2_SP2 ) ) );
            sprites.Add( new MonsterSprite( "BremondtDarkDragon", new string[] { "BremondtDarkDragon.SPR" }, 
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.BremondtDarkDragon_SPR ) ) );
            sprites.Add( new MonsterSprite( "DEMON", new string[] { "DEMON.SPR", "DEMON2.SP2" },
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.DEMON_SPR ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.DEMON2_SP2 ) ) );
            sprites.Add( new MonsterSprite( "DORA1", new string[] { "DORA1.SPR" }, 
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.DORA1_SPR ) ) );
            sprites.Add( new MonsterSprite( "DORA2", new string[] { "DORA2.SPR", "DORA22.SP2" },
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.DORA2_SPR ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.DORA22_SP2 ) ) );
            sprites.Add( new MonsterSprite( "HASYU", new string[] { "HASYU.SPR" }, 
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.HASYU_SPR ) ) );
            sprites.Add( new MonsterSprite( "HEBI", new string[] { "HEBI.SPR" }, 
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.HEBI_SPR ) ) );
            sprites.Add( new MonsterSprite( "HYOU", new string[] { "HYOU.SPR", "HYOU2.SP2" },
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.HYOU_SPR ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.HYOU2_SP2 ) ) );
            sprites.Add( new MonsterSprite( "KI", new string[] { "KI.SPR" }, 
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.KI_SPR ) ) );
            sprites.Add( new MonsterSprite( "KYUKU", new string[] { "KYUKU.SPR" }, 
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.KYUKU_SPR ) ) );
            sprites.Add( new MonsterSprite( "MINOTA", new string[] { "MINOTA.SPR", "MINOTA2.SP2" },
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MINOTA_SPR ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MINOTA2_SP2 ) ) );
            sprites.Add( new MonsterSprite( "MOL", new string[] { "MOL.SPR", "MOL2.SP2" },
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MOL_SPR ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MOL2_SP2 ) ) );
            sprites.Add( new MonsterSprite( "REZE_D", new string[] { "REZE_D.SPR" }, 
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.REZE_D_SPR ) ) );
            sprites.Add( new MonsterSprite( "TETSU", new string[] { "TETSU.SPR", "IRON2.SP2","IRON3.SP2","IRON4.SP2","IRON5.SP2" },
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.TETSU_SPR ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.IRON2_SP2 ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.IRON3_SP2 ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.IRON4_SP2 ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.IRON5_SP2 ) ) );
            sprites.Add( new MonsterSprite( "TORI", new string[] { "TORI.SPR", "TORI2.SP2" },
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.TORI_SPR ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.TORI2_SP2 ) ) );
            sprites.Add( new MonsterSprite( "URI", new string[] { "URI.SPR", "URI2.SP2" },
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.URI_SPR ),
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.URI2_SP2 ) ) );
            sprites.Add( new MonsterSprite( "VERI", new string[] { "VERI.SPR" }, 
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.VERI_SPR ) ) );
            sprites.Add( new MonsterSprite( "YUREI", new string[] { "YUREI.SPR" }, 
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.YUREI_SPR ) ) );
            sprites.Add( new MonsterSprite( "ZARUE", new string[] { "ZARUE.SPR" }, 
                FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ZARUE_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "AGURI", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.AGURI_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "Aliste", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.Aliste_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "ARU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ARU_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "Balthier", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.Balthier_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "BARUNA", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.BARUNA_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "BEIO", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.BEIO_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "CLOUD", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.CLOUD_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "DAISU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.DAISU_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "DeathKnightArgath", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.DeathKnightArgath_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "DILY", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.DILY_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "DILY2", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.DILY2_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "DILY3", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.DILY3_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "FemaleDarkKnight", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.FemaleDarkKnight_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "FemaleOnionKnight", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.FemaleOnionKnight_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "GANDO", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.GANDO_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "GARU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.GARU_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "GOB", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.GOB_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "GORU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.GORU_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "H61", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.H61_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "H75", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.H75_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "H76", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.H76_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "H77", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.H77_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "H78", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.H78_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "H80", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.H80_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "H81", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.H81_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "H85", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.H85_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "KANBA", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.KANBA_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "KNIGHT_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.KNIGHT_M_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "KNIGHT_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.KNIGHT_W_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "MaleDarkKnight", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MaleDarkKnight_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "MaleOnionKnight", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MaleOnionKnight_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "MARA", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MARA_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "MINA_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MINA_M_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "MINA_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MINA_W_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "MONK_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MONK_M_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "MONK_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MONK_W_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "MUSU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MUSU_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "NINJA_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.NINJA_M_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "NINJA_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.NINJA_W_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "ORAN", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ORAN_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "ORU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ORU_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "RAMUZA", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.RAMUZA_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "RAMUZA2", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.RAMUZA2_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "RAMUZA3", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.RAMUZA3_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "RUDO", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.RUDO_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "RYU_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.RYU_M_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "RYU_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.RYU_W_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "SAMU_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.SAMU_M_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "SAMU_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.SAMU_W_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "SUKERU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.SUKERU_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "THIEF_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.THIEF_M_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "THIEF_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.THIEF_W_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "VORU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.VORU_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "WIGU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.WIGU_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "YUMI_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.YUMI_M_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "YUMI_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.YUMI_W_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "ZARU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ZARU_SPR ) ) );
            sprites.Add( new TYPE1Sprite( "ZARU2", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ZARU2_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "AJORA", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.AJORA_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "ARUFU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ARUFU_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "ARUMA", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ARUMA_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "BARITEN", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.BARITEN_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "BARU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.BARU_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "BremondtHuman", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.BremondtHuman_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "DORA", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.DORA_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "ERU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ERU_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "FUSUI_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.FUSUI_M_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "FUSUI_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.FUSUI_W_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "FYUNE", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.FYUNE_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "GIN_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.GIN_M_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "GYUMU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.GYUMU_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "H79", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.H79_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "H82", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.H82_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "H83", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.H83_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "HIME", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.HIME_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "IKA", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.IKA_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "ITEM_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ITEM_M_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "ITEM_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ITEM_W_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "KURO_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.KURO_M_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "KURO_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.KURO_W_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "LEDY", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.LEDY_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "MONO_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MONO_M_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "MONO_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.MONO_W_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "ODORI_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ODORI_W_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "ONMYO_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ONMYO_M_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "ONMYO_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ONMYO_W_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "RAFA", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.RAFA_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "RAGU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.RAGU_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "REZE", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.REZE_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "SAN_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.SAN_M_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "SAN_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.SAN_W_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "SERIA", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.SERIA_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "SIMON", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.SIMON_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "SIRO_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.SIRO_M_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "SIRO_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.SIRO_W_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "SOURYO", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.SOURYO_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "SYOU_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.SYOU_M_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "SYOU_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.SYOU_W_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "TOKI_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.TOKI_M_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "TOKI_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.TOKI_W_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "WAJU_M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.WAJU_M_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "WAJU_W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.WAJU_W_SPR ) ) );
            sprites.Add( new TYPE2Sprite( "ZARUMOU", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ZARUMOU_SPR ) ) );
            sprites.Add( new ARUTE( FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.ARUTE_SPR ) ) );
            sprites.Add( new CYOKO( FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.CYOKO_SPR ) ) );
            sprites.Add( new KANZEN( FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.KANZEN_SPR ) ) );
            sprites.Add( new ShortSprite( "10M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE._10M_SPR ) ) );
            sprites.Add( new ShortSprite( "10W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE._10W_SPR ) ) );
            sprites.Add( new ShortSprite( "20M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE._20M_SPR ) ) );
            sprites.Add( new ShortSprite( "20W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE._20W_SPR ) ) );
            sprites.Add( new ShortSprite( "40M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE._40M_SPR ) ) );
            sprites.Add( new ShortSprite( "40W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE._40W_SPR ) ) );
            sprites.Add( new ShortSprite( "60M", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE._60M_SPR ) ) );
            sprites.Add( new ShortSprite( "60W", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE._60W_SPR ) ) );
            sprites.Add( new ShortSprite( "CYOMON1", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.CYOMON1_SPR ) ) );
            sprites.Add( new ShortSprite( "CYOMON2", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.CYOMON2_SPR ) ) );
            sprites.Add( new ShortSprite( "CYOMON3", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.CYOMON3_SPR ) ) );
            sprites.Add( new ShortSprite( "CYOMON4", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.CYOMON4_SPR ) ) );
            sprites.Add( new ShortSprite( "FURAIA", FFTPack.GetFileFromIso( iso, FFTPack.BATTLE.FURAIA_SPR ) ) );

            sprites.Sort( ( a, b ) => a.Name.CompareTo( b.Name ) );

            return new FullSpriteSet( sprites, null, 0, 0 );
        }

        public void PatchPsxISO( string filename )
        {
            using ( Stream stream = File.Open( filename, FileMode.Open, FileAccess.ReadWrite ) )
            {
                PatchPsxISO( stream );
            }
        }

        public void PatchPsxISO( Stream stream )
        {
            throw new NotImplementedException();
            foreach ( var sprite in sprites )
            {
                //Enum.Parse(typeof(PsxIso.Sectors), 
            }
        }

        public void PatchPspISO( Stream stream )
        {
            throw new NotImplementedException();
        }

        public void PatchPspISO( string filename )
        {
            using ( Stream stream = File.Open( filename, FileMode.Open, FileAccess.ReadWrite ) )
            {
                PatchPspISO( stream );
            }
        }

        public void SaveShishiFile( string filename )
        {
            using( ZipOutputStream stream = new ZipOutputStream( File.Open( filename, FileMode.Create, FileAccess.ReadWrite ) ) )
            {
                Dictionary<string, Dictionary<string, List<string>>> files = new Dictionary<string, Dictionary<string, List<string>>>();

                foreach( var sprite in Sprites )
                {
                    byte[] pixels = sprite.Pixels.ToArray();
                    IList<byte> palettes = new List<byte>();
                    sprite.Palettes.ForEach( p => palettes.AddRange( p.ToByteArray() ) );
                    palettes = palettes.ToArray();

                    string type = sprite.GetType().FullName;
                    if( !files.ContainsKey( type ) )
                    {
                        files[type] = new Dictionary<string, List<string>>();
                    }

                    List<string> fileList = new List<string>( );
                    files[type][sprite.Name] = fileList;

                    WriteFileToZip(
                        stream,
                        string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}/{1}/Pixels", type, sprite.Name ),
                        pixels );
                    WriteFileToZip(
                        stream,
                        string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}/{1}/Palettes", type, sprite.Name ),
                        palettes.ToArray() );
                    WriteFileToZip(
                        stream,
                        string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}/{1}/Size", type, sprite.Name ),
                        sprite.OriginalSize.ToString( System.Globalization.CultureInfo.InvariantCulture ).ToByteArray() );
                    fileList.AddRange( sprite.Filenames );
                }

                BinaryFormatter f = new BinaryFormatter();
                stream.PutNextEntry( new ZipEntry( "manifest" ) );
                f.Serialize( stream, files );

                const string fileVersion = "1.0";
                WriteFileToZip( stream, "version", Encoding.UTF8.GetBytes( fileVersion ) );
            }
        }

        public static FullSpriteSet FromPsxISO( string filename )
        {
            using ( FileStream stream = File.OpenRead( filename ) )
            {
                return FromPsxISO( stream );
            }
        }

        public static FullSpriteSet FromPsxISO( Stream stream )
        {
            return DoInitPSX( stream );
        }

        public static FullSpriteSet FromPspISO( string filename )
        {
            using ( FileStream stream = File.OpenRead( filename ) )
            {
                return FromPspISO( stream );
            }
        }

        public static FullSpriteSet FromPspISO( Stream stream )
        {
            return DoInitPSP( stream );
        }

        public static FullSpriteSet FromShishiFile( string filename, System.ComponentModel.BackgroundWorker worker )
        {
            Dictionary<string, Dictionary<string, List<string>>> manifest;

            List<AbstractSprite> sprites = new List<AbstractSprite>();

            int tasks = 0;
            int tasksComplete = 0;
            using ( ZipFile zf = new ZipFile( filename ) )
            {
                BinaryFormatter f = new BinaryFormatter();
                manifest = f.Deserialize( zf.GetInputStream( zf.GetEntry( "manifest" ) ) ) as Dictionary<string, Dictionary<string, List<string>>>;

                foreach ( KeyValuePair<string, Dictionary<string, List<string>>> kvp in manifest )
                {
                    tasks += kvp.Value.Keys.Count * 3;
                }

                tasks += 1;
                foreach( string type in manifest.Keys )
                {
                    Type spriteType = Type.GetType( type );
                    ConstructorInfo constructor = spriteType.GetConstructor( BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof( SerializedSprite ) }, null );

                    foreach( string name in manifest[type].Keys )
                    {
                        List<string> filenames = manifest[type][name];
                        int size = filenames.Count;
                        byte[][] bytes = new byte[size][];

                        worker.ReportProgress( ( tasksComplete++ * 100 ) / tasks, string.Format( "Extracting {0}", name ) );

                        ZipEntry entry = zf.GetEntry( string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}/{1}/Pixels", type, name ) );
                        byte[] pixels = new byte[entry.Size];
                        StreamUtils.ReadFully( zf.GetInputStream( entry ), pixels );

                        entry = zf.GetEntry( string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}/{1}/Palettes", type, name ) );
                        byte[] palettes = new byte[entry.Size];
                        StreamUtils.ReadFully( zf.GetInputStream( entry ), palettes );

                        entry = zf.GetEntry( string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}/{1}/Size", type, name ) );
                        byte[] sizeBytes = new byte[entry.Size];
                        StreamUtils.ReadFully( zf.GetInputStream( entry ), sizeBytes );
                        int origSize = Int32.Parse( new string( Encoding.UTF8.GetChars( sizeBytes ) ), System.Globalization.CultureInfo.InvariantCulture );


                        worker.ReportProgress( ( tasksComplete++ * 100 ) / tasks, string.Format( "Building {0}", name ) );
                        sprites.Add( constructor.Invoke( new object[] { new SerializedSprite( name, origSize, filenames, pixels, palettes ) } ) as AbstractSprite );
                    }
                }
                
            }

            return new FullSpriteSet( sprites, worker, tasksComplete, tasks );
        }

        private static void WriteFileToZip( ZipOutputStream stream, string filename, byte[] bytes )
        {
            stream.PutNextEntry( new ZipEntry( filename ) );
            stream.Write( bytes, 0, bytes.Length );
        }

    }
}
