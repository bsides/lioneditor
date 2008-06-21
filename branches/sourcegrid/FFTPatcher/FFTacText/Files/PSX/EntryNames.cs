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

using System;
using System.Collections.Generic;
using System.Xml;

namespace FFTPatcher.TextEditor.Files.PSX
{
    internal static class EntryNames
    {
        static XmlDocument doc;
        static EntryNames()
        {
            doc = new XmlDocument();
            doc.LoadXml( Properties.PSXResources.EntryNames );
        }

        public static IList<IList<string>> GetEntryNames( Type t )
        {
            string typeName = t.ToString();
            XmlNode node = doc.SelectSingleNode( string.Format( "/EntryNames/Files/File[@name='{0}']", typeName ) );
            if( node == null )
            {
                throw new ArgumentException( "type not found", "t" );
            }

            int sectionCount = Int32.Parse( node.Attributes["sections"].InnerText );
            string[][] result = new string[sectionCount][];
            for( int i = 0; i < sectionCount; i++ )
            {
                result[i] = GetEntries( node.SelectSingleNode( string.Format( "Section[@value='{0}']", i ) ) );
            }

            return result;
        }

        private static string[] GetEntries( XmlNode sectionNode )
        {
            int entryCount = Int32.Parse( sectionNode.Attributes["entries"].InnerText );
            bool empty = Boolean.Parse( sectionNode.Attributes["empty"].InnerText );
            string[] result = new string[entryCount];

            if( empty )
            {
                for( int i = 0; i < entryCount; i++ )
                {
                    result[i] = string.Empty;
                }
            }
            else
            {
                ApplyEntryNodes( sectionNode, result );
                ApplyIncludeNodes( sectionNode, result );
            }

            return result;
        }

        private static void ApplyEntryNodes( XmlNode parentNode, string[] output )
        {
            XmlNodeList entryNodes = parentNode.SelectNodes( "entry" );
            foreach( XmlNode node in entryNodes )
            {
                int index = Int32.Parse( node.Attributes["value"].InnerText );
                output[index] = node.Attributes["name"].InnerText;
            }
        }

        private static void ApplyIncludeNodes( XmlNode parentNode, string[] output )
        {
            XmlNodeList includeNodes = parentNode.SelectNodes( "include" );
            foreach( XmlNode includeNode in includeNodes )
            {
                int start = Int32.Parse( includeNode.Attributes["start"].InnerText );
                int end = Int32.Parse( includeNode.Attributes["end"].InnerText );
                int offset = Int32.Parse( includeNode.Attributes["offset"].InnerText );
                string include = includeNode.Attributes["name"].InnerText;
                ApplyEntries( includeNode.OwnerDocument.SelectSingleNode( string.Format( "/EntryNames/Lists/{0}", include ) ), output, start, end, offset );
            }
        }

        private static void ApplyEntries( XmlNode parentNode, string[] output, int start, int end, int offset )
        {
            for( int i = start; i <= end; i++ )
            {
                XmlNode node = parentNode.SelectSingleNode( string.Format( "entry[@value='{0}']", i ) );
                output[i + offset] = node.Attributes["name"].InnerText;
            }
        }
    }
}
