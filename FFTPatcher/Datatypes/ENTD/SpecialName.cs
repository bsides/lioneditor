/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

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

using System.Xml;
using FFTPatcher.Properties;

namespace FFTPatcher.Datatypes
{
    public class SpecialName
    {
        private static SpecialName[] pspNames = new SpecialName[256];
        private static SpecialName[] psxNames = new SpecialName[256];
        public static SpecialName[] SpecialNames
        {
            get { return FFTPatch.Context == Context.US_PSP ? pspNames : psxNames; }
        }

        static SpecialName()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( Resources.SpecialNames );
            for( int i = 0; i < 256; i++ )
            {
                XmlNode node = doc.SelectSingleNode( string.Format( "//SpecialNames/SpecialName[@byte='{0:X2}']", i ) );
                string name = string.Empty;
                if( node != null )
                {
                    name = node.SelectSingleNode( "@name" ).InnerText;
                }

                pspNames[i] = new SpecialName( (byte)i, name );
            }
            doc.LoadXml( PSXResources.SpriteSets );
            for( int i = 0; i < 256; i++ )
            {
                XmlNode node = doc.SelectSingleNode( string.Format( "//SpecialNames/SpecialName[@byte='{0:X2}']", i ) );
                string name = string.Empty;
                if( node != null )
                {
                    name = node.SelectSingleNode( "@name" ).InnerText;
                }

                psxNames[i] = new SpecialName( (byte)i, name );
            }
        }

        public byte Value { get; private set; }
        public string Name { get; private set; }

        private SpecialName( byte value, string name )
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
