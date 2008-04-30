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


        public static void WriteDigestEntry( XmlWriter writer, string name, object def, object cur, bool changesOnly )
        {
            bool changed = !def.Equals( cur );
            if( !changesOnly || changed )
            {
                writer.WriteStartElement( name );
                writer.WriteAttributeString( "changed", changed.ToString() );
                writer.WriteAttributeString( "default", def.ToString() );
                writer.WriteAttributeString( "value", cur.ToString() );
                writer.WriteEndElement();
            }
        }

        public static void WriteXmlDigest( ISupportDigest digest, XmlWriter writer, bool writeStartElement, bool writeEndElement, bool changesOnly )
        {
            bool changed = digest.HasChanged;
            if( !changesOnly || changed )
            {
                if( writeStartElement )
                {
                    writer.WriteStartElement( digest.GetType().ToString() );
                }

                object defaultObject = ReflectionHelpers.GetFieldOrProperty<object>( digest, "Default" );
                writer.WriteAttributeString( "changed", changed.ToString() );
                foreach( string value in digest.DigestableProperties )
                {
                    object def = ReflectionHelpers.GetFieldOrProperty<object>( defaultObject, value );
                    object cur = ReflectionHelpers.GetFieldOrProperty<object>( digest, value );
                    if( def != null && cur != null )
                    {
                        WriteDigestEntry( writer, value, def, cur, changesOnly );
                    }
                }

                if( writeEndElement )
                {
                    writer.WriteEndElement();
                }
            }
        }

        public static void WriteXmlDigest( ISupportDigest digest, XmlWriter writer, bool changesOnly )
        {
            WriteXmlDigest( digest, writer, true, true, changesOnly );
        }


        #endregion Methods

    }
}
