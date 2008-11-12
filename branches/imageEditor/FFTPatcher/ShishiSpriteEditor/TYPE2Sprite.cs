using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    public class TYPE2Sprite : AbstractCompressedSprite
    {
        public static TYPE2Sprite AJORA { get { return new TYPE2Sprite( Properties.Resources.AJORA_SPR ); } }
        public static TYPE2Sprite ARUFU { get { return new TYPE2Sprite( Properties.Resources.ARUFU_SPR ); } }
        public static TYPE2Sprite ARUMA { get { return new TYPE2Sprite( Properties.Resources.ARUMA_SPR ); } }
        public static TYPE2Sprite BARITEN { get { return new TYPE2Sprite( Properties.Resources.BARITEN_SPR ); } }
        public static TYPE2Sprite BARU { get { return new TYPE2Sprite( Properties.Resources.BARU_SPR ); } }
        public static TYPE2Sprite ERU { get { return new TYPE2Sprite( Properties.Resources.ERU_SPR ); } }
        public static TYPE2Sprite FURAIA { get { return new TYPE2Sprite( Properties.Resources.FURAIA_SPR ); } }
        public static TYPE2Sprite FUSUI_M { get { return new TYPE2Sprite( Properties.Resources.FUSUI_M_SPR ); } }
        public static TYPE2Sprite FUSUI_W { get { return new TYPE2Sprite( Properties.Resources.FUSUI_W_SPR ); } }
        public static TYPE2Sprite FYUNE { get { return new TYPE2Sprite( Properties.Resources.FYUNE_SPR ); } }
        public static TYPE2Sprite GIN_M { get { return new TYPE2Sprite( Properties.Resources.GIN_M_SPR ); } }
        public static TYPE2Sprite GYUMU { get { return new TYPE2Sprite( Properties.Resources.GYUMU_SPR ); } }
        public static TYPE2Sprite H79 { get { return new TYPE2Sprite( Properties.Resources.H79_SPR ); } }
        public static TYPE2Sprite H82 { get { return new TYPE2Sprite( Properties.Resources.H82_SPR ); } }
        public static TYPE2Sprite H83 { get { return new TYPE2Sprite( Properties.Resources.H83_SPR ); } }
        public static TYPE2Sprite HIME { get { return new TYPE2Sprite( Properties.Resources.HIME_SPR ); } }
        public static TYPE2Sprite IKA { get { return new TYPE2Sprite( Properties.Resources.IKA_SPR ); } }
        public static TYPE2Sprite ITEM_M { get { return new TYPE2Sprite( Properties.Resources.ITEM_M_SPR ); } }
        public static TYPE2Sprite ITEM_W { get { return new TYPE2Sprite( Properties.Resources.ITEM_W_SPR ); } }
        public static TYPE2Sprite KURO_M { get { return new TYPE2Sprite( Properties.Resources.KURO_M_SPR ); } }
        public static TYPE2Sprite KURO_W { get { return new TYPE2Sprite( Properties.Resources.KURO_W_SPR ); } }
        public static TYPE2Sprite LEDY { get { return new TYPE2Sprite( Properties.Resources.LEDY_SPR ); } }
        public static TYPE2Sprite MONO_M { get { return new TYPE2Sprite( Properties.Resources.MONO_M_SPR ); } }
        public static TYPE2Sprite MONO_W { get { return new TYPE2Sprite( Properties.Resources.MONO_W_SPR ); } }
        public static TYPE2Sprite ODORI_W { get { return new TYPE2Sprite( Properties.Resources.ODORI_W_SPR ); } }
        public static TYPE2Sprite ONMYO_M { get { return new TYPE2Sprite( Properties.Resources.ONMYO_M_SPR ); } }
        public static TYPE2Sprite ONMYO_W { get { return new TYPE2Sprite( Properties.Resources.ONMYO_W_SPR ); } }
        public static TYPE2Sprite RAFA { get { return new TYPE2Sprite( Properties.Resources.RAFA_SPR ); } }
        public static TYPE2Sprite RAGU { get { return new TYPE2Sprite( Properties.Resources.RAGU_SPR ); } }
        public static TYPE2Sprite REZE { get { return new TYPE2Sprite( Properties.Resources.REZE_SPR ); } }
        public static TYPE2Sprite SAN_M { get { return new TYPE2Sprite( Properties.Resources.SAN_M_SPR ); } }
        public static TYPE2Sprite SAN_W { get { return new TYPE2Sprite( Properties.Resources.SAN_W_SPR ); } }
        public static TYPE2Sprite SERIA { get { return new TYPE2Sprite( Properties.Resources.SERIA_SPR ); } }
        public static TYPE2Sprite SIMON { get { return new TYPE2Sprite( Properties.Resources.SIMON_SPR ); } }
        public static TYPE2Sprite SIRO_M { get { return new TYPE2Sprite( Properties.Resources.SIRO_M_SPR ); } }
        public static TYPE2Sprite SIRO_W { get { return new TYPE2Sprite( Properties.Resources.SIRO_W_SPR ); } }
        public static TYPE2Sprite SOURYO { get { return new TYPE2Sprite( Properties.Resources.SOURYO_SPR ); } }
        public static TYPE2Sprite SYOU_M { get { return new TYPE2Sprite( Properties.Resources.SYOU_M_SPR ); } }
        public static TYPE2Sprite SYOU_W { get { return new TYPE2Sprite( Properties.Resources.SYOU_W_SPR ); } }
        public static TYPE2Sprite TOKI_M { get { return new TYPE2Sprite( Properties.Resources.TOKI_M_SPR ); } }
        public static TYPE2Sprite TOKI_W { get { return new TYPE2Sprite( Properties.Resources.TOKI_W_SPR ); } }
        public static TYPE2Sprite WAJU_M { get { return new TYPE2Sprite( Properties.Resources.WAJU_M_SPR ); } }
        public static TYPE2Sprite WAJU_W { get { return new TYPE2Sprite( Properties.Resources.WAJU_W_SPR ); } }
        public static TYPE2Sprite ZARUMOU { get { return new TYPE2Sprite( Properties.Resources.ZARUMOU_SPR ); } }

        public TYPE2Sprite( IList<byte> bytes )
            : base( bytes )
        {
        }
    }
}
