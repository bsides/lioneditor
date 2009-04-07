using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace FFTPatcher.SpriteEditor
{
    [System.Diagnostics.DebuggerDisplay("SHP: {SHP}, SEQ: {SEQ}, Sec: {Sector}, Size: {Size}")]
    public class Sprite
    {
        private SpriteAttributes attributes;
        private SpriteLocation location;
        private string name;

        public SpriteAttributes.SpriteType SHP { get { return attributes.SHP; } private set { attributes.SHP = value; } }
        public SpriteAttributes.SpriteType SEQ { get { return attributes.SEQ; } private set { attributes.SEQ = value; } }
        public bool Flag1 { get { return attributes.Flag1; } private set { attributes.Flag1 = value; } }
        public bool Flag2 { get { return attributes.Flag2; } private set { attributes.Flag2 = value; } }
        public bool Flag3 { get { return attributes.Flag3; } private set { attributes.Flag3 = value; } }
        public bool Flag4 { get { return attributes.Flag4; } private set { attributes.Flag4 = value; } }
        public bool Flag5 { get { return attributes.Flag5; } private set { attributes.Flag5 = value; } }
        public bool Flag6 { get { return attributes.Flag6; } private set { attributes.Flag6 = value; } }
        public bool Flag7 { get { return attributes.Flag7; } private set { attributes.Flag7 = value; } }
        public bool Flag8 { get { return attributes.Flag8; } private set { attributes.Flag8 = value; } }
        public bool Flying { get { return attributes.Flying; } private set { attributes.Flying = value; } }
        public UInt32 Sector { get { return location.Sector; } private set { location.Sector = value; } }
        public UInt32 Size { get { return location.Size; } private set { location.Size = value; } }

        public Sprite(string name, SpriteAttributes attributes, SpriteLocation location)
        {
            this.name = name;
            this.attributes = attributes;
            this.location = location;
        }

        public AbstractSprite GetAbstractSpriteFromPsxIso( System.IO.Stream iso )
        {
            byte[] bytes = PatcherLib.Iso.PsxIso.ReadFile( iso, (PatcherLib.Iso.PsxIso.Sectors)Sector, 0, (int)Size );
            switch ( SHP )
            {
                case SpriteAttributes.SpriteType.TYPE1:
                    return new TYPE1Sprite( bytes );
                case SpriteAttributes.SpriteType.TYPE2:
                    return new TYPE2Sprite( "butts", bytes );
                case SpriteAttributes.SpriteType.MON:
                case SpriteAttributes.SpriteType.RUKA:
                    return new MonsterSprite( bytes );
                case SpriteAttributes.SpriteType.KANZEN:
                    return new KANZEN( bytes );
                case SpriteAttributes.SpriteType.CYOKO:
                    return new CYOKO( bytes );
                case SpriteAttributes.SpriteType.ARUTE:
                    return new ARUTE( bytes );
                default:
                    return null;
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
