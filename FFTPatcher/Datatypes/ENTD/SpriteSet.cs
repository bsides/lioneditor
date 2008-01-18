/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Xml;


namespace FFTPatcher.Datatypes
{
    public class SpriteSet
    {
        private static SpriteSet[] psxSpriteSets = new SpriteSet[256];
        private static SpriteSet[] pspSpriteSets = new SpriteSet[256];
        public static SpriteSet[] SpriteSets
        {
            get { return FFTPatch.Context == Context.US_PSP ? pspSpriteSets : psxSpriteSets; }
        }

        static SpriteSet()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( Resources.SpriteSets );
            for( int i = 0; i < 256; i++ )
            {
                XmlNode node = doc.SelectSingleNode( string.Format( "//Sprites/Sprite[byte='{0:X2}']", i ) );
                string name = string.Empty;
                if( node != null )
                {
                    name = node.SelectSingleNode( "sprite" ).InnerText;
                }

                pspSpriteSets[i] = new SpriteSet( (byte)i, name );
            }
            doc.LoadXml( FFTPatcher.Properties.PSXResources.SpriteSets );
            for( int i = 0; i < 256; i++ )
            {
                XmlNode node = doc.SelectSingleNode( string.Format( "//Sprites/Sprite[byte='{0:X2}']", i ) );
                string name = string.Empty;
                if( node != null )
                {
                    name = node.SelectSingleNode( "sprite" ).InnerText;
                }

                psxSpriteSets[i] = new SpriteSet( (byte)i, name );
            }
        }

        public byte Value { get; private set; }
        public string Name { get; private set; }

        private SpriteSet( byte value, string name )
        {
            Value = value;
            Name = name;
        }

        public byte ToByte()
        {
            return Value;
        }

        public override string ToString()
        {
            return string.Format( "{0:X2} {1}", Value, Name );
        }
    }
}
