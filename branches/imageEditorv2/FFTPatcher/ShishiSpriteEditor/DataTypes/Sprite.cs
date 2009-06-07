using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using PatcherLib.Utilities;

namespace FFTPatcher.SpriteEditor
{
    public class SpriteTooLargeException : Exception
    {
        public int Size { get; private set; }
        public int MaxSize { get; private set; }

        public SpriteTooLargeException(int size, int maxSize)
        {
            this.Size = size;
            this.MaxSize = maxSize;
        }

        public override string Message
        {
            get
            {
                return string.Format("Sprite size is {0} bytes. Max size is {1} bytes.", Size, MaxSize);
            }
        }
    }

    [System.Diagnostics.DebuggerDisplay("SHP: {SHP}, SEQ: {SEQ}, Sec: {Sector}, Size: {Size}")]
    public class Sprite
    {
        private SpriteAttributes attributes;
        private SpriteLocation location;
        private string name;
        private AbstractSprite cachedSprite;
        private const int sp2MaxLength = 256*256/2;

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
        public PatcherLib.Datatypes.Context Context { get; private set; }
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

        internal void ImportBitmap(Stream iso, System.Drawing.Bitmap bmp)
        {
            bool bad = false;
            AbstractSprite sprite = GetAbstractSpriteFromIso(iso);
            sprite.ImportBitmap(bmp, out bad);
            byte[] sprBytes = sprite.ToByteArray(0);
            if (sprBytes.Length > Size)
            {
                throw new SpriteTooLargeException(sprBytes.Length, (int)Size);
            }

            ImportSprite(iso, sprBytes);
            for (int i = 0; i < NumChildren; i++)
            {
                ImportSp2(iso, sprite.ToByteArray(i + 1), i);
            }
        }

        internal void ImportSp2(Stream iso, string filename, int index)
        {
            ImportSp2( iso, File.ReadAllBytes( filename ), index );
        }

        internal void ImportSp2( Stream iso, byte[] bytes, int index )
        {
            if ( index >= NumChildren )
            {
                throw new IndexOutOfRangeException();
            }

            if (bytes.Length > sp2MaxLength)
            {
                throw new ArgumentOutOfRangeException("bytes", "SP2 file is too large");
            }

            var loc = location.SubSpriteLocations[index];
            if (Context == PatcherLib.Datatypes.Context.US_PSX)
            {
                PatcherLib.Iso.PsxIso.PatchPsxIso(
                    iso,
                    new PatcherLib.Datatypes.PatchedByteArray((PatcherLib.Iso.PsxIso.Sectors)loc.Sector, 0, bytes));
            }
            else if (Context == PatcherLib.Datatypes.Context.US_PSP)
            {
                PatcherLib.Iso.PspIso.ApplyPatch(
                    iso,
                    PatcherLib.Iso.PspIso.PspIsoInfo.GetPspIsoInfo(iso),
                    new PatcherLib.Datatypes.PatchedByteArray((PatcherLib.Iso.FFTPack.Files)loc.Sector, 0, bytes));
            }
            else
            {
                throw new InvalidOperationException();
            }
            cachedSprite = null;
        }

        internal void ImportSprite( Stream iso, string filename )
        {
            ImportSprite( iso, File.ReadAllBytes( filename ) );
        }

        internal void ImportSprite( Stream iso, byte[] bytes )
        {
            if (bytes.Length > Size)
            {
                throw new SpriteTooLargeException(bytes.Length, (int)Size);
            }
            if (Context == PatcherLib.Datatypes.Context.US_PSX)
            {
                PatcherLib.Iso.PsxIso.PatchPsxIso(
                    iso,
                    new PatcherLib.Datatypes.PatchedByteArray((PatcherLib.Iso.PsxIso.Sectors)Sector, 0, bytes));
            }
            else if (Context == PatcherLib.Datatypes.Context.US_PSP)
            {
                PatcherLib.Iso.PspIso.ApplyPatch(
                    iso,
                    PatcherLib.Iso.PspIso.PspIsoInfo.GetPspIsoInfo(iso),
                    new PatcherLib.Datatypes.PatchedByteArray((PatcherLib.Iso.FFTPack.Files)Sector, 0, bytes));
            }
            else
            {
                throw new InvalidOperationException();
            }
            cachedSprite = null;
        }

        internal Sprite(PatcherLib.Datatypes.Context context, string name, SpriteAttributes attributes, SpriteLocation location)
        {
            this.Context = context;
            this.name = name;
            this.attributes = attributes;
            this.location = location;
        }

        public AbstractSprite GetAbstractSpriteFromIso(System.IO.Stream iso)
        {
            return GetAbstractSpriteFromIso(iso, false);
        }

        public AbstractSprite GetAbstractSpriteFromIso(System.IO.Stream iso, bool ignoreCache)
        {
            if (Context == PatcherLib.Datatypes.Context.US_PSX)
            {
                return GetAbstractSpriteFromPsxIso(iso, ignoreCache);
            }
            else if (Context == PatcherLib.Datatypes.Context.US_PSP)
            {
                return GetAbstractSpriteFromPspIso(iso, PatcherLib.Iso.PspIso.PspIsoInfo.GetPspIsoInfo(iso), ignoreCache);
            }
            else
            {
                return null;
            }
        }

        private AbstractSprite GetAbstractSpriteFromPspIso(System.IO.Stream iso, PatcherLib.Iso.PspIso.PspIsoInfo info, bool ignoreCache)
        {
            if (cachedSprite == null || ignoreCache)
            {
                IList<byte> bytes = PatcherLib.Iso.PspIso.GetFile(iso, info, (PatcherLib.Iso.FFTPack.Files)Sector);
                System.Diagnostics.Debug.Assert(bytes.Count == this.Size);
                switch (SHP)
                {
                    case SpriteType.TYPE1:
                        cachedSprite = new TYPE1Sprite(bytes);
                        break;
                    case SpriteType.TYPE2:
                        cachedSprite = new TYPE2Sprite(bytes);
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
                                sp2Bytes[i] = PatcherLib.Iso.PspIso.GetFile(
                                    iso,
                                    info,
                                    (PatcherLib.Iso.FFTPack.Files)location.SubSpriteLocations[i].Sector,
                                    0,
                                    (int)location.SubSpriteLocations[i].Size).ToArray();
                            }
                        }
                        cachedSprite = new MonsterSprite(bytes, sp2Bytes);
                        break;
                    case SpriteType.KANZEN:
                        cachedSprite = new KANZEN(bytes);
                        break;
                    case SpriteType.CYOKO:
                        cachedSprite = new CYOKO(bytes);
                        break;
                    case SpriteType.ARUTE:
                        cachedSprite = new ARUTE(bytes);
                        break;
                    default:
                        cachedSprite = null;
                        break;
                }
            }

            return cachedSprite;
        }

        private AbstractSprite GetAbstractSpriteFromPsxIso( System.IO.Stream iso, bool ignoreCache )
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

        private AbstractSprite GetAbstractSpriteFromPsxIso( System.IO.Stream iso )
        {
            return GetAbstractSpriteFromPsxIso( iso, false );
        }

        public override string ToString()
        {
            return name;
        }
    }
}
