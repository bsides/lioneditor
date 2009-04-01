using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    class Sprite
    {
        private SpriteAttributes attributes;
        private SpriteLocation location;

        public SpriteAttributes.SpriteType SHP { get { return attributes.SHP; } set { attributes.SHP = value; } }
        public SpriteAttributes.SpriteType SEQ { get { return attributes.SEQ; } set { attributes.SEQ = value; } }
        public bool Flag1 { get { return attributes.Flag1; } set { attributes.Flag1 = value; } }
        public bool Flag2 { get { return attributes.Flag2; } set { attributes.Flag2 = value; } }
        public bool Flag3 { get { return attributes.Flag3; } set { attributes.Flag3 = value; } }
        public bool Flag4 { get { return attributes.Flag4; } set { attributes.Flag4 = value; } }
        public bool Flag5 { get { return attributes.Flag5; } set { attributes.Flag5 = value; } }
        public bool Flag6 { get { return attributes.Flag6; } set { attributes.Flag6 = value; } }
        public bool Flag7 { get { return attributes.Flag7; } set { attributes.Flag7 = value; } }
        public bool Flag8 { get { return attributes.Flag8; } set { attributes.Flag8 = value; } }
        public bool Flying { get { return attributes.Flying; } set { attributes.Flying = value; } }
        public UInt32 Sector { get { return location.Sector; } set { location.Sector = value; } }
        public UInt32 Size { get { return location.Size; } set { location.Size = value; } }

        public Sprite(SpriteAttributes attributes, SpriteLocation location)
        {
            this.attributes = attributes;
            this.location = location;
        }
    }
}
