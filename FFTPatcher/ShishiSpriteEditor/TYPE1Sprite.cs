using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    public class TYPE1Sprite : AbstractCompressedSprite
    {
        public static TYPE1Sprite AGURI { get { return new TYPE1Sprite( Properties.Resources.AGURI_SPR ); } }
        public static TYPE1Sprite ARU { get { return new TYPE1Sprite( Properties.Resources.ARU_SPR ); } }
        public static TYPE1Sprite BARUNA { get { return new TYPE1Sprite( Properties.Resources.BARUNA_SPR ); } }
        public static TYPE1Sprite BEIO { get { return new TYPE1Sprite( Properties.Resources.BEIO_SPR ); } }
        public static TYPE1Sprite CLOUD { get { return new TYPE1Sprite( Properties.Resources.CLOUD_SPR ); } }
        public static TYPE1Sprite DAISU { get { return new TYPE1Sprite( Properties.Resources.DAISU_SPR ); } }
        public static TYPE1Sprite DILY { get { return new TYPE1Sprite( Properties.Resources.DILY_SPR ); } }
        public static TYPE1Sprite DILY2 { get { return new TYPE1Sprite( Properties.Resources.DILY2_SPR ); } }
        public static TYPE1Sprite DILY3 { get { return new TYPE1Sprite( Properties.Resources.DILY3_SPR ); } }
        public static TYPE1Sprite GANDO { get { return new TYPE1Sprite( Properties.Resources.GANDO_SPR ); } }
        public static TYPE1Sprite GARU { get { return new TYPE1Sprite( Properties.Resources.GARU_SPR ); } }
        public static TYPE1Sprite GOB { get { return new TYPE1Sprite( Properties.Resources.GOB_SPR ); } }
        public static TYPE1Sprite GORU { get { return new TYPE1Sprite( Properties.Resources.GORU_SPR ); } }
        public static TYPE1Sprite H61 { get { return new TYPE1Sprite( Properties.Resources.H61_SPR ); } }
        public static TYPE1Sprite H75 { get { return new TYPE1Sprite( Properties.Resources.H75_SPR ); } }
        public static TYPE1Sprite H76 { get { return new TYPE1Sprite( Properties.Resources.H76_SPR ); } }
        public static TYPE1Sprite H77 { get { return new TYPE1Sprite( Properties.Resources.H77_SPR ); } }
        public static TYPE1Sprite H78 { get { return new TYPE1Sprite( Properties.Resources.H78_SPR ); } }
        public static TYPE1Sprite H80 { get { return new TYPE1Sprite( Properties.Resources.H80_SPR ); } }
        public static TYPE1Sprite H81 { get { return new TYPE1Sprite( Properties.Resources.H81_SPR ); } }
        public static TYPE1Sprite H85 { get { return new TYPE1Sprite( Properties.Resources.H85_SPR ); } }
        public static TYPE1Sprite KANBA { get { return new TYPE1Sprite( Properties.Resources.KANBA_SPR ); } }
        public static TYPE1Sprite KNIGHT_M { get { return new TYPE1Sprite( Properties.Resources.KNIGHT_M_SPR ); } }
        public static TYPE1Sprite KNIGHT_W { get { return new TYPE1Sprite( Properties.Resources.KNIGHT_W_SPR ); } }
        public static TYPE1Sprite MARA { get { return new TYPE1Sprite( Properties.Resources.MARA_SPR ); } }
        public static TYPE1Sprite MINA_M { get { return new TYPE1Sprite( Properties.Resources.MINA_M_SPR ); } }
        public static TYPE1Sprite MINA_W { get { return new TYPE1Sprite( Properties.Resources.MINA_W_SPR ); } }
        public static TYPE1Sprite MONK_M { get { return new TYPE1Sprite( Properties.Resources.MONK_M_SPR ); } }
        public static TYPE1Sprite MONK_W { get { return new TYPE1Sprite( Properties.Resources.MONK_W_SPR ); } }
        public static TYPE1Sprite MUSU { get { return new TYPE1Sprite( Properties.Resources.MUSU_SPR ); } }
        public static TYPE1Sprite NINJA_M { get { return new TYPE1Sprite( Properties.Resources.NINJA_M_SPR ); } }
        public static TYPE1Sprite NINJA_W { get { return new TYPE1Sprite( Properties.Resources.NINJA_W_SPR ); } }
        public static TYPE1Sprite ORAN { get { return new TYPE1Sprite( Properties.Resources.ORAN_SPR ); } }
        public static TYPE1Sprite ORU { get { return new TYPE1Sprite( Properties.Resources.ORU_SPR ); } }
        public static TYPE1Sprite RAMUZA { get { return new TYPE1Sprite( Properties.Resources.RAMUZA_SPR ); } }
        public static TYPE1Sprite RAMUZA2 { get { return new TYPE1Sprite( Properties.Resources.RAMUZA2_SPR ); } }
        public static TYPE1Sprite RAMUZA3 { get { return new TYPE1Sprite( Properties.Resources.RAMUZA3_SPR ); } }
        public static TYPE1Sprite RUDO { get { return new TYPE1Sprite( Properties.Resources.RUDO_SPR ); } }
        public static TYPE1Sprite RYU_M { get { return new TYPE1Sprite( Properties.Resources.RYU_M_SPR ); } }
        public static TYPE1Sprite RYU_W { get { return new TYPE1Sprite( Properties.Resources.RYU_W_SPR ); } }
        public static TYPE1Sprite SAMU_M { get { return new TYPE1Sprite( Properties.Resources.SAMU_M_SPR ); } }
        public static TYPE1Sprite SAMU_W { get { return new TYPE1Sprite( Properties.Resources.SAMU_W_SPR ); } }
        public static TYPE1Sprite SUKERU { get { return new TYPE1Sprite( Properties.Resources.SUKERU_SPR ); } }
        public static TYPE1Sprite THIEF_M { get { return new TYPE1Sprite( Properties.Resources.THIEF_M_SPR ); } }
        public static TYPE1Sprite THIEF_W { get { return new TYPE1Sprite( Properties.Resources.THIEF_W_SPR ); } }
        public static TYPE1Sprite VORU { get { return new TYPE1Sprite( Properties.Resources.VORU_SPR ); } }
        public static TYPE1Sprite WIGU { get { return new TYPE1Sprite( Properties.Resources.WIGU_SPR ); } }
        public static TYPE1Sprite YUMI_M { get { return new TYPE1Sprite( Properties.Resources.YUMI_M_SPR ); } }
        public static TYPE1Sprite YUMI_W { get { return new TYPE1Sprite( Properties.Resources.YUMI_W_SPR ); } }
        public static TYPE1Sprite ZARU { get { return new TYPE1Sprite( Properties.Resources.ZARU_SPR ); } }
        public static TYPE1Sprite ZARU2 { get { return new TYPE1Sprite( Properties.Resources.ZARU2_SPR ); } }
        public TYPE1Sprite( IList<byte> bytes )
            : base( bytes )
        {
        }
    }
}
