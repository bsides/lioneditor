using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    public class MonsterSprite : AbstractCompressedSprite
    {
        public static MonsterSprite ADORA { get { return new MonsterSprite( Properties.Resources.ADORA_SPR ); } }
        public static MonsterSprite ARLI { get { return new MonsterSprite( Properties.Resources.ARLI_SPR, Properties.Resources.ARLI2_SP2 ); } }
        public static MonsterSprite BEHI { get { return new MonsterSprite( Properties.Resources.BEHI_SPR, Properties.Resources.BEHI2_SP2 ); } }
        public static MonsterSprite BIBUROS { get { return new MonsterSprite( Properties.Resources.BIBUROS_SPR, Properties.Resources.BIBU2_SP2 ); } }
        public static MonsterSprite BOM { get { return new MonsterSprite( Properties.Resources.BOM_SPR, Properties.Resources.BOM2_SP2 ); } }
        public static MonsterSprite DEMON { get { return new MonsterSprite( Properties.Resources.DEMON_SPR, Properties.Resources.DEMON2_SP2 ); } }
        public static MonsterSprite DORA { get { return new MonsterSprite( Properties.Resources.DORA_SPR ); } }
        public static MonsterSprite DORA1 { get { return new MonsterSprite( Properties.Resources.DORA1_SPR ); } }
        public static MonsterSprite DORA2 { get { return new MonsterSprite( Properties.Resources.DORA2_SPR, Properties.Resources.DORA22_SP2 ); } }
        public static MonsterSprite HASYU { get { return new MonsterSprite( Properties.Resources.HASYU_SPR ); } }
        public static MonsterSprite HEBI { get { return new MonsterSprite( Properties.Resources.HEBI_SPR ); } }
        public static MonsterSprite HYOU { get { return new MonsterSprite( Properties.Resources.HYOU_SPR, Properties.Resources.HYOU2_SP2 ); } }
        public static MonsterSprite KI { get { return new MonsterSprite( Properties.Resources.KI_SPR ); } }
        public static MonsterSprite KYUKU { get { return new MonsterSprite( Properties.Resources.KYUKU_SPR ); } }
        public static MonsterSprite MINOTA { get { return new MonsterSprite( Properties.Resources.MINOTA_SPR, Properties.Resources.MINOTA2_SP2 ); } }
        public static MonsterSprite MOL { get { return new MonsterSprite( Properties.Resources.MOL_SPR, Properties.Resources.MOL2_SP2 ); } }
        public static MonsterSprite REZE_D { get { return new MonsterSprite( Properties.Resources.REZE_D_SPR ); } }
        public static MonsterSprite TETSU { get { return new MonsterSprite( Properties.Resources.TETSU_SPR, Properties.Resources.IRON2_SP2, Properties.Resources.IRON3_SP2, Properties.Resources.IRON4_SP2, Properties.Resources.IRON5_SP2 ); } }
        public static MonsterSprite TORI { get { return new MonsterSprite( Properties.Resources.TORI_SPR, Properties.Resources.TORI2_SP2 ); } }
        public static MonsterSprite URI { get { return new MonsterSprite( Properties.Resources.URI_SPR, Properties.Resources.URI2_SP2 ); } }
        public static MonsterSprite VERI { get { return new MonsterSprite( Properties.Resources.VERI_SPR ); } }
        public static MonsterSprite YUREI { get { return new MonsterSprite( Properties.Resources.YUREI_SPR ); } }
        public static MonsterSprite ZARUE { get { return new MonsterSprite( Properties.Resources.ZARUE_SPR ); } }
        
        public override int Height
        {
            get
            {
                return 488 + sp2Count * 256;
            }
        }

        private int sp2Count;

        public MonsterSprite( IList<byte> bytes, params IList<byte>[] sp2Files )
            : base( bytes, sp2Files )
        {
        }

        protected override IList<byte> BuildPixels( IList<byte> bytes, IList<byte>[] extraBytes )
        {
            List<byte> result = new List<byte>( 36864 * 2 );
            foreach( byte b in bytes )
            {
                result.Add( b.GetLowerNibble() );
                result.Add( b.GetUpperNibble() );
            }

            result.AddRange( Decompress( bytes.Sub( 36864 ) ) );

            foreach( IList<byte> extra in extraBytes )
            {
                foreach( byte b in extra )
                {
                    result.Add( b.GetLowerNibble() );
                    result.Add( b.GetUpperNibble() );
                }
            }

            return result.ToArray();
        }

        public override IList<byte[]> ToByteArrays()
        {
            List<byte[]> result = new List<byte[]>( base.ToByteArrays() );
            for( int i = 0; i < sp2Count; i++ )
            {
                IList<byte> sp2 = Pixels.Sub( 256 * 488 + i * 256, 256 * 488 + (i + 1) * 256 * 256 - 1 );
                byte[] sp2Array = new byte[32768];
                for( int j = 0; j < sp2.Count; j += 2 )
                {
                    sp2Array[j / 2] = (byte)((sp2[2 * j + 1] << 4) | (sp2[2 * j] & 0x0F));
                }
                result.Add( sp2Array );
            }

            return result;
        }

        protected override void ToBitmapInner( bool proper, System.Drawing.Bitmap bmp, System.Drawing.Imaging.BitmapData bmd )
        {
            base.ToBitmapInner( proper, bmp, bmd );
            for( int i = 256 * 488; (i < Pixels.Count) && (i / Width < Height); i++ )
            {
                bmd.SetPixel( i % Width, i / Width, Pixels[i] );
            }
        }
    }
}
