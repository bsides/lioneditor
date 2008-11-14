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

        private void FinishInit()
        {
            Thumbnails = new ImageList();
            Thumbnails.ImageSize = new System.Drawing.Size( 80, 48 );
            foreach( var sprite in sprites )
            {
                Thumbnails.Images.Add( sprite.Name, sprite.GetThumbnail() );
            }
        }

        private FullSpriteSet( IList<AbstractSprite> sprites )
        {
            sprites.Sort( ( a, b ) => a.Name.CompareTo( b.Name ) );
            this.sprites = sprites;
            FinishInit();
        }

        public FullSpriteSet( Stream iso, Context isoType )
        {
            DoInit( iso, isoType );
            FinishInit();
        }

        public FullSpriteSet( string isoFileName, Context isoType )
        {
            using ( FileStream stream = File.Open( isoFileName, FileMode.Open, FileAccess.Read ) )
            {
                DoInit( stream, isoType );
            }

            FinishInit();
        }

        private void DoInit( Stream iso, Context isoType )
        {
            if ( isoType == Context.US_PSP )
            {
                DoInitPSP( iso );
            }
            else if ( isoType == Context.US_PSX )
            {
                DoInitPSX( iso );
            }
            else
            {
                throw new ArgumentException( "invalid iso type", "isoType" );
            }
        }

        private void DoInitPSX( Stream iso )
        {
            sprites = new List<AbstractSprite>();
            sprites.Add( new MonsterSprite( "ADORA", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ADORA_SPR, 0, 47100 ) ) );
            sprites.Add( new MonsterSprite( "ARLI",
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ARLI_SPR, 0, 41475 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ARLI2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "BEHI",
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BEHI_SPR, 0, 46393 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BEHI2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "BIBUROS",
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BIBUROS_SPR, 0, 44353 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BIBU2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "BOM",
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BOM_SPR, 0, 42546 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.BOM2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "DEMON",
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DEMON_SPR, 0, 45648 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DEMON2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "DORA1", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DORA1_SPR, 0, 46754 ) ) );
            sprites.Add( new MonsterSprite( "DORA2",
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DORA2_SPR, 0, 46437 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.DORA22_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "HASYU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.HASYU_SPR, 0, 47430 ) ) );
            sprites.Add( new MonsterSprite( "HEBI", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.HEBI_SPR, 0, 48525 ) ) );
            sprites.Add( new MonsterSprite( "HYOU",
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.HYOU_SPR, 0, 43553 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.HYOU2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "KI", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.KI_SPR, 0, 45205 ) ) );
            sprites.Add( new MonsterSprite( "KYUKU", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.KYUKU_SPR, 0, 48094 ) ) );
            sprites.Add( new MonsterSprite( "MINOTA",
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MINOTA_SPR, 0, 47737 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MINOTA2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "MOL",
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MOL_SPR, 0, 47102 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.MOL2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "REZE_D", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.REZE_D_SPR, 0, 46744 ) ) );
            sprites.Add( new MonsterSprite( "TETSU",
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.TETSU_SPR, 0, 46001 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.IRON2_SP2, 0, 32768 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.IRON3_SP2, 0, 32768 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.IRON4_SP2, 0, 32768 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.IRON5_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "TORI",
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.TORI_SPR, 0, 43332 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.TORI2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "URI",
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.URI_SPR, 0, 40595 ),
                IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.URI2_SP2, 0, 32768 ) ) );
            sprites.Add( new MonsterSprite( "VERI", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.VERI_SPR, 0, 46848 ) ) );
            sprites.Add( new MonsterSprite( "YUREI", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.YUREI_SPR, 0, 41970 ) ) );
            sprites.Add( new MonsterSprite( "ZARUE", IsoPatch.ReadFile( IsoPatch.IsoType.Mode2Form1, iso, (int)PsxIso.BATTLE.ZARUE_SPR, 0, 47018 ) ) );
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
        }

        private void DoInitPSP( Stream iso )
        {
        }

        public void SaveFile( string filename )
        {
            using( ZipOutputStream stream = new ZipOutputStream( File.Open( filename, FileMode.Create, FileAccess.ReadWrite ) ) )
            {
                Dictionary<string, Dictionary<string, int>> files = new Dictionary<string, Dictionary<string, int>>();

                foreach( var sprite in Sprites )
                {
                    IList<byte[]> bytes = sprite.ToByteArrays();
                    string type = sprite.GetType().FullName;
                    if( !files.ContainsKey( type ) )
                    {
                        files[type] = new Dictionary<string, int>();
                    }

                    files[type][sprite.Name] = bytes.Count;

                    for( int i = 0; i < bytes.Count; i++ )
                    {
                        WriteFileToZip( 
                            stream, 
                            string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}/{1}/{2}", sprite.GetType().FullName, sprite.Name, i ), 
                            bytes[i] );
                    }
                }

                BinaryFormatter f = new BinaryFormatter();
                stream.PutNextEntry( new ZipEntry( "manifest" ) );
                f.Serialize( stream, files );

                const string fileVersion = "1.0";
                WriteFileToZip( stream, "version", Encoding.UTF8.GetBytes( fileVersion ) );
            }
        }

        public static FullSpriteSet FromFile( string filename )
        {
            Dictionary<string, Dictionary<string, int>> manifest;

            List<AbstractSprite> sprites = new List<AbstractSprite>();

            using( ZipFile zf = new ZipFile( filename ) )
            {
                BinaryFormatter f = new BinaryFormatter();
                manifest = f.Deserialize( zf.GetInputStream( zf.GetEntry( "manifest" ) ) ) as Dictionary<string, Dictionary<string, int>>;

                foreach( string type in manifest.Keys )
                {
                    Type spriteType = Type.GetType( type );
                    ConstructorInfo shortestConstructor = spriteType.GetConstructor( BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof( IList<byte> ) }, null );
                    ConstructorInfo shortConstructor = spriteType.GetConstructor( BindingFlags.Public | BindingFlags.Instance , null, new Type[] { typeof( string ), typeof( IList<byte> ) }, null );
                    ConstructorInfo longConstructor = spriteType.GetConstructor( BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof( string ), typeof( IList<byte> ), typeof( IList<byte>[] ) }, null );

                    foreach( string name in manifest[type].Keys )
                    {
                        int size = manifest[type][name];
                        byte[][] bytes = new byte[size][];
                        for( int i = 0; i < size; i++ )
                        {
                            ZipEntry entry = zf.GetEntry( string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}/{1}/{2}", type, name, i ) );
                            bytes[i] = new byte[entry.Size];
                            StreamUtils.ReadFully( zf.GetInputStream( entry ), bytes[i] );
                        }

                        if( shortestConstructor != null )
                        {
                            sprites.Add( shortestConstructor.Invoke( new object[] { bytes[0] } ) as AbstractSprite );
                        }
                        else if( shortConstructor != null )
                        {
                            sprites.Add( shortConstructor.Invoke( new object[] { name, bytes[0] } ) as AbstractSprite );
                        }
                        else if( longConstructor != null )
                        {
                            IList<byte>[] extraParams = new IList<byte>[0];
                            if( bytes.Length > 1 )
                            {
                                extraParams = bytes.Sub( 1 ).ToArray();
                            }
                            sprites.Add( longConstructor.Invoke( new object[] { name, bytes[0], extraParams } ) as AbstractSprite );
                        }
                        else
                        {
                            throw new FormatException( "manifest malformated" );
                        }
                    }
                }
                
            }

            return new FullSpriteSet( sprites );
        }

        private static void WriteFileToZip( ZipOutputStream stream, string filename, byte[] bytes )
        {
            stream.PutNextEntry( new ZipEntry( filename ) );
            stream.Write( bytes, 0, bytes.Length );
        }

    }
}
