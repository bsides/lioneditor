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
    public enum State
    {
        Active = 0x0A,
        Complete = 0x0C,
        None = 0x00
    }

    /// <summary>
    /// Represents a single Feat
    /// </summary>
    public class Feat
    {
        public StupidDate Date;
        public State State;
        public string Name;

        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// Represents both Feats data structures in memory
    /// </summary>
    public class Feats
    {
        #region Fields

        private List<Feat> allFeats;
        private static StringCollection featList;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Feats represented by this instance
        /// </summary>
        public List<Feat> AllFeats
        {
            get { return allFeats; }
        }

        /// <summary>
        /// Gets a collection of names for all Feats
        /// </summary>
        private static StringCollection FeatList
        {
            get
            {
                if( featList == null )
                {
                    featList = new StringCollection();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml( Resources.Feats );

                    for( int i = 0; i < 96; i++ )
                    {
                        XmlNode node = doc.SelectSingleNode( string.Format( "//Feat[@offset='{0}']/@name", i ) );
                        featList.Add( node.InnerText );
                    }

                }

                return featList;
            }
        }

        #endregion

        public Feats( byte[] dates, byte[] states )
        {
            allFeats = new List<Feat>( 96 );

            for( int i = 0; i < 96; i++ )
            {
                Feat f = new Feat();
                f.Date = StupidDate.GetDateFromOffset( i, dates );
                switch( i % 2 )
                {
                    case 1:
                        f.State = (State)((states[i / 2] & 0xF0) >> 4);
                        break;
                    case 0:
                        f.State = (State)((states[i / 2] & 0x0F));
                        break;
                }
                f.Name = FeatList[i];

                allFeats.Add( f );
            }
        }

        public byte[] DatesToByteArray()
        {
            byte[] result = new byte[108];
            for( int i = 0; i < 96; i++ )
            {
                AllFeats[i].Date.SetDateAtOffset( i, result );
            }

            return result;
        }

        public byte[] StatesToByteArray()
        {
            byte[] result = new byte[48];
            for( int i = 0; i < 96; i++ )
            {
                if( i % 2 == 1 )
                {
                    result[i / 2] |= (byte)((byte)(AllFeats[i].State) << 4);
                }
                else
                {
                    result[i / 2] |= (byte)((byte)AllFeats[i].State);
                }
            }

            return result;
        }
    }
}
