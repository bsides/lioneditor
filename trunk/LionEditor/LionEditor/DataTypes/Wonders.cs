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
using System.Collections.Specialized;
using System.Xml;
using LionEditor.Properties;

namespace LionEditor
{
    /// <summary>
    /// Represents a single wonder
    /// </summary>
    public class Wonder
    {
        public StupidDate Date;
        public string Name;
        public bool Discovered;

        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// Represents both Wonders data structures in memory
    /// </summary>
    public class Wonders
    {
        #region Fields

        private List<Wonder> allWonders;
        private static StringCollection wonderList;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the wonders represented by this instance
        /// </summary>
        public List<Wonder> AllWonders
        {
            get { return allWonders; }
        }

        /// <summary>
        /// Gets a collection of names of all wonders
        /// </summary>
        private static StringCollection WonderList
        {
            get
            {
                if( wonderList == null )
                {
                    wonderList = new StringCollection();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml( Resources.Wonders );

                    for( int i = 0; i < 16; i++ )
                    {
                        XmlNode node = doc.SelectSingleNode( string.Format( "//Wonder[@offset='{0}']/@name", i ) );
                        wonderList.Add( node.InnerText );
                    }
                }

                return wonderList;
            }
        }

        #endregion

        public Wonders( byte[] dates, byte[] states )
        {
            allWonders = new List<Wonder>( 16 );

            for( int i = 0; i < 16; i++ )
            {
                Wonder w = new Wonder();
                w.Date = StupidDate.GetDateFromOffset( i, dates );
                w.Name = WonderList[i];
                w.Discovered = (states[i / 8] & (0x01 << (i % 8))) > 0;

                allWonders.Add( w );
            }
        }

        public byte[] DatesToByteArray()
        {
            byte[] result = new byte[18];
            for( int i = 0; i < 16; i++ )
            {
                AllWonders[i].Date.SetDateAtOffset( i, result );
            }

            return result;
        }

        public byte[] StatesToByteArray()
        {
            byte[] result = new byte[2];
            for( int i = 0; i < 16; i++ )
            {
                result[i / 8] |= (byte)((AllWonders[i].Discovered) ? (1 << (i % 8)) : 0);
            }

            return result;
        }
    }
}
