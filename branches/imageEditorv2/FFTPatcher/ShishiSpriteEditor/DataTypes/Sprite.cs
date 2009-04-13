using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace FFTPatcher.SpriteEditor
{
    [System.Diagnostics.DebuggerDisplay("SHP: {SHP}, SEQ: {SEQ}, Sec: {Sector}, Size: {Size}")]
    public class Sprite
    {
        private SpriteAttributes attributes;
        private SpriteLocation location;
        private string name;
        private AbstractSprite cachedSprite;

        public SpriteType SHP { get { return attributes.SHP; } }
        public SpriteType SEQ { get { return attributes.SEQ; } }
        public bool Flag1 { get { return attributes.Flag1; } }
        public bool Flag2 { get { return attributes.Flag2; } }
        public bool Flag3 { get { return attributes.Flag3; } }
        public bool Flag4 { get { return attributes.Flag4; } }
        public bool Flag5 { get { return attributes.Flag5; } }
        public bool Flag6 { get { return attributes.Flag6; } }
        public bool Flag7 { get { return attributes.Flag7; } }
        public bool Flag8 { get { return attributes.Flag8; } }
        public bool Flying { get { return attributes.Flying; } }
        public UInt32 Sector { get { return location.Sector; } }
        public UInt32 Size { get { return location.Size; } }

        public int NumChildren { get { return location.SubSpriteLocations.Count; } }

        internal void SetSHP(Stream iso, SpriteType shp)
        {
            if (SHP != shp)
            {
                if (SHP == SpriteType.MON || shp == SpriteType.MON)
                {
                    cachedSprite = null;
                }

                attributes.SetSHP(iso, shp);
            }
        }

        internal void SetSEQ(Stream iso, SpriteType seq)
        {
            attributes.SetSEQ(iso, seq);
        }

        internal void SetFlying(Stream iso, bool flying)
        {
            attributes.SetFlying(iso, flying);
        }

        internal void SetFlag(Stream iso, int index, bool flag)
        {
            attributes.SetFlag(iso, index, flag);
        }

        internal void ImportBitmap( Stream iso, string filename )
        {
            using ( Stream s = File.OpenRead( filename ) )
            using ( System.Drawing.Bitmap b = new System.Drawing.Bitmap( s ) )
            {
                ImportBitmap( iso, b );
            }
        }

        internal void ImportBitmap( Stream iso, System.Drawing.Bitmap bmp )
        {
            bool bad = false;
            AbstractSprite sprite = GetAbstractSpriteFromPsxIso( iso );
            sprite.ImportBitmap( bmp, out bad );
            ImportSprite( iso, sprite.ToByteArray( 0 ) );
        }

        internal void ImportSp2( Stream iso, string filename, int index )
        {
            ImportSp2( iso, File.ReadAllBytes( filename ), index );
        }

        internal void ImportSp2( Stream iso, byte[] bytes, int index )
        {
            if ( index >= NumChildren )
            {
                throw new IndexOutOfRangeException();
            }

            var loc = location.SubSpriteLocations[index];
            PatcherLib.Iso.PsxIso.PatchPsxIso(
                iso,
                new PatcherLib.Datatypes.PatchedByteArray( (PatcherLib.Iso.PsxIso.Sectors)loc.Sector, 0, bytes ) );
            cachedSprite = null;
        }

        internal void ImportSprite( Stream iso, string filename )
        {
            ImportSprite( iso, File.ReadAllBytes( filename ) );
        }

        internal void ImportSprite( Stream iso, byte[] bytes )
        {
            PatcherLib.Iso.PsxIso.PatchPsxIso(
                iso,
                new PatcherLib.Datatypes.PatchedByteArray( (PatcherLib.Iso.PsxIso.Sectors)Sector, 0, bytes ) );
            cachedSprite = null;
        }

        internal Sprite(string name, SpriteAttributes attributes, SpriteLocation location)
        {
            this.name = name;
            this.attributes = attributes;
            this.location = location;
        }

        public AbstractSprite GetAbstractSpriteFromPsxIso( System.IO.Stream iso, bool ignoreCache )
        {
            if ( cachedSprite == null || ignoreCache )
            {
                byte[] bytes = PatcherLib.Iso.PsxIso.ReadFile( iso, (PatcherLib.Iso.PsxIso.Sectors)Sector, 0, (int)Size );
                switch ( SHP )
                {
                    case SpriteType.TYPE1:
                        cachedSprite = new TYPE1Sprite( bytes );
                        break;
                    case SpriteType.TYPE2:
                        cachedSprite = new TYPE2Sprite( bytes );
                        break;
                    case SpriteType.RUKA:
                        cachedSprite = new MonsterSprite(bytes);
                        break;
                    case SpriteType.MON:
                        byte[][] sp2Bytes = new byte[location.SubSpriteLocations.Count][];
                        if (location.SubSpriteLocations.Count > 0)
                        {
                            for (int i = 0; i < location.SubSpriteLocations.Count; i++)
                            {
                                sp2Bytes[i] = PatcherLib.Iso.PsxIso.ReadFile(
                                    iso,
                                    (PatcherLib.Iso.PsxIso.Sectors)location.SubSpriteLocations[i].Sector,
                                    0,
                                    (int)location.SubSpriteLocations[i].Size);
                            }
                        }
                        cachedSprite = new MonsterSprite(bytes, sp2Bytes);
                        break;
                    case SpriteType.KANZEN:
                        cachedSprite = new KANZEN( bytes );
                        break;
                    case SpriteType.CYOKO:
                        cachedSprite = new CYOKO( bytes );
                        break;
                    case SpriteType.ARUTE:
                        cachedSprite = new ARUTE( bytes );
                        break;
                    default:
                        cachedSprite = null;
                        break;
                }
            }

            return cachedSprite;
        }

        public AbstractSprite GetAbstractSpriteFromPsxIso( System.IO.Stream iso )
        {
            return GetAbstractSpriteFromPsxIso( iso, false );
        }

        public override string ToString()
        {
            return name;
        }
    }
}
