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
using FFTPatcher.Datatypes;

namespace FFTPatcher
{
    public static class DigestGenerator
    {

        #region Methods (3)


        public static void WriteDigestEntry( XmlWriter writer, string name, object def, object cur )
        {
            writer.WriteStartElement( name );
            writer.WriteAttributeString( "changed", (!def.Equals( cur )).ToString() );
            writer.WriteAttributeString( "default", def.ToString() );
            writer.WriteAttributeString( "value", cur.ToString() );
            writer.WriteEndElement();
        }

        public static void WriteXmlDigest( ISupportDigest digest, XmlWriter writer, bool writeStartElement, bool writeEndElement )
        {
            if( writeStartElement )
            {
                writer.WriteStartElement( digest.GetType().ToString() );
            }

            object defaultObject = ReflectionHelpers.GetFieldOrProperty<object>( digest, "Default" );
            writer.WriteAttributeString( "changed", digest.HasChanged.ToString() );
            foreach( string value in digest.DigestableProperties )
            {
                object def = ReflectionHelpers.GetFieldOrProperty<object>( defaultObject, value );
                object cur = ReflectionHelpers.GetFieldOrProperty<object>( digest, value );
                if( def != null && cur != null )
                {
                    WriteDigestEntry( writer, value, def, cur );
                }
            }

            if( writeEndElement )
            {
                writer.WriteEndElement();
            }
        }

        public static void WriteXmlDigest( ISupportDigest digest, XmlWriter writer )
        {
            WriteXmlDigest( digest, writer, true, true );
        }


        #endregion Methods

    }
}
