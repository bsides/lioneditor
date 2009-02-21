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

using System.Collections.Generic;
using System.IO;
using System.Xml;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

namespace FFTPatcher.TextEditor
{
    /// <summary>
    /// Resources for the application.
    /// </summary>
    public static class Resources
    {

        #region Static Fields (1)

        private static Dictionary<string, XmlNode> resourceMapping = new Dictionary<string, XmlNode>();

        #endregion Static Fields

        #region Static Properties (1)

        public static XmlNode PSX { get { return resourceMapping["psx.xml"]; } }
        public static XmlNode PSP { get { return resourceMapping["psp.xml"]; } }

        #endregion Static Properties

        #region Constructors (1)

        static Resources()
        {
            using ( MemoryStream memStream = new MemoryStream( FFTPatcher.TextEditor.Properties.Resources.Resources_tar, false ) )
            using ( GZipInputStream gzStream = new GZipInputStream( memStream ) )
            using ( TarInputStream tarStream = new TarInputStream( gzStream ) )
            {
                TarEntry entry;
                entry = tarStream.GetNextEntry();
                while ( entry != null )
                {
                    if ( entry.Size != 0 && !string.IsNullOrEmpty( entry.Name ) )
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load( tarStream );
                        resourceMapping[entry.Name] = doc;
                    }
                    entry = tarStream.GetNextEntry();
                }

            }
        }

        #endregion Constructors

    }
}
