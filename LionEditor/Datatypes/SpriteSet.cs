/*
	Copyright 2007, Joe Davidson <joedavidson@gmail.com>

	This file is part of LionEditor.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LionEditor.Properties;

namespace LionEditor
{
    public class SpriteSet
    {
        private string name = string.Empty;
        public string Name
        {
            get { return name; }
        }

        private static List<SpriteSet> allSprites;
        public static List<SpriteSet> AllSprites
        {
            get
            {
                if( allSprites == null )
                {
                    allSprites = new List<SpriteSet>( 256 );
                    for( int i = 0; i <= 0xFF; i++ )
                    {
                        allSprites.Add( new SpriteSet( (byte)i ) );
                    }
                }

                return allSprites;
            }
        }

        private static XmlDocument doc;

        private byte value;
        public byte Value
        {
            get { return value; }
        }

        private SpriteSet( byte b )
        {
            if( doc == null )
            {
                doc = new XmlDocument();
                doc.LoadXml( Resources.Sprites );
            }

            this.value = b;

            XmlNode node = doc.SelectSingleNode( string.Format( "//Sprite[byte='{0}']", b.ToString( "X2" ) ) );

            if( node != null )
            {
                this.name = node.SelectSingleNode( "sprite" ).InnerText;
            }
        }

        public override string ToString()
        {
            return string.Format( "{0} ({1:X2})", name, value );
        }
    }
}
