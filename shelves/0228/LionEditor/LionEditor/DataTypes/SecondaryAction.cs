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
using System.Xml;
using LionEditor.Properties;

namespace LionEditor
{
    /// <summary>
    /// Represents a secondary action a character can have
    /// </summary>
    public class SecondaryAction : IComparable
    {
        #region Fields

        private static List<SecondaryAction> actionList;
        private static Dictionary<byte, SecondaryAction> actionDict;
        private byte _byte;
        private string name;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of all secondary actions
        /// </summary>
        public static List<SecondaryAction> ActionList
        {
            get
            {
                if( actionList == null )
                {
                    LoadActions();
                }

                return actionList;
            }
        }

        /// <summary>
        /// Gets a dictionary of all secondary actions, whose keys are byte values
        /// </summary>
        public static Dictionary<byte, SecondaryAction> ActionDictionary
        {
            get
            {
                if( actionDict == null )
                {
                    LoadActions();
                }

                return new Dictionary<byte, SecondaryAction>( actionDict );
            }
        }

        /// <summary>
        /// Gets the byte representing this secondary action
        /// </summary>
        public byte Byte
        {
            get { return _byte; }
        }

        public string String
        {
            get { return this.ToString(); }
        }

        /// <summary>
        /// Gets the name of this secondary action
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        #endregion

        #region Utilities

        private static void LoadActions()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( Resources.SecondaryAction );

            XmlNodeList actions = doc.SelectNodes( "//SecondaryAction" );
            actionList = new List<SecondaryAction>( actions.Count );
            actionDict = new Dictionary<byte, SecondaryAction>( actions.Count );

            foreach( XmlNode node in actions )
            {
                SecondaryAction action = new SecondaryAction( Convert.ToByte( node.Attributes["byte"].InnerText, 16 ), node.InnerText );
                actionList.Add( action );
                actionDict.Add( action.Byte, action );
            }

            actionList.Sort();
        }

        public override string ToString()
        {
            return string.Format( "{0} ({1:X02})", this.Name, this.Byte );
        }

        #region IComparable Members

        public int CompareTo( object obj )
        {
            SecondaryAction a = obj as SecondaryAction;
            if( a != null )
            {
                return this.ToString().CompareTo( a.ToString() );
            }

            return -1;
        }

        #endregion

        #endregion
        
        private SecondaryAction( byte b, string name )
        {
            this._byte = b;
            this.name = name;
        }

    }
}
