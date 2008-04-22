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

using System.Collections.Generic;
using System.Xml;
using LionEditor.Properties;

namespace LionEditor
{
    /// <summary>
    /// Represents a character's sprite set, which also affects his "primary job"
    /// </summary>
    public class SpriteSet
    {
        #region Fields

        private string name = string.Empty;
        private static List<SpriteSet> allSprites;
        private static XmlDocument doc;
        private byte value;

        #endregion

        #region Properties

        public string Name
        {
            get { return name; }
        }

        public byte Value
        {
            get { return value; }
        }

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

        #endregion
        
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
