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
    /// Represents a single Artefact
    /// </summary>
    public class Artefact
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
    /// Represents both Artefacts data structures in memory
    /// </summary>
    public class Artefacts
    {

        #region Fields

        private List<Artefact> allArtefacts;
        private static StringCollection artefactList;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the artefacts represented by this instance
        /// </summary>
        public List<Artefact> AllArtefacts
        {
            get { return allArtefacts; }
        }

        /// <summary>
        /// Gets a collection of names for all artefacts
        /// </summary>
        private static StringCollection ArtefactList
        {
            get
            {
                if( artefactList == null )
                {
                    artefactList = new StringCollection();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml( Resources.Artefacts );

                    for( int i = 0; i < 48; i++ )
                    {
                        XmlNode node = doc.SelectSingleNode( string.Format( "//Artefact[@offset='{0}']/@name", i ) );
                        if( node != null )
                        {
                            artefactList.Add( node.InnerText );
                        }
                    }
                }

                return artefactList;
            }
        }

        #endregion

        public Artefacts( byte[] dates, byte[] states )
        {
            allArtefacts = new List<Artefact>( 48 );

            for( int i = 0; i < 47; i++ )
            {
                Artefact w = new Artefact();
                w.Date = StupidDate.GetDateFromOffset( i, dates );
                w.Name = ArtefactList[i];
                w.Discovered = (states[(i + 1) / 8] & (0x01 << ((i + 1) % 8))) > 0;

                allArtefacts.Add( w );
            }
        }

        public byte[] DatesToByteArray()
        {
            byte[] result = new byte[53];
            for( int i = 1; i < 48; i++ )
            {
                AllArtefacts[i - 1].Date.SetDateAtOffset( i - 1, result );
            }

            return result;
        }

        public byte[] StatesToByteArray()
        {
            byte[] result = new byte[6];
            for( int i = 1; i < 48; i++ )
            {
                result[i / 8] |= (byte)((AllArtefacts[i - 1].Discovered) ? (1 << (i % 8)) : 0);
            }

            return result;
        }
    }
}
