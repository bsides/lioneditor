using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LionEditor.Properties;

namespace LionEditor
{
    public class SecondaryAction
    {
        private static List<SecondaryAction> actionList;

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

        private static Dictionary<byte, SecondaryAction> actionDict;
        public static Dictionary<byte, SecondaryAction> ActionDictionary
        {
            get
            {
                if( actionDict == null )
                {
                    LoadActions();
                }

                return actionDict;
            }
        }

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
        }

        private byte _byte;
        private string name;

        public byte Byte
        {
            get { return _byte; }
        }

        public string Name
        {
            get { return name; }
        }

        private SecondaryAction( byte b, string name )
        {
            this._byte = b;
            this.name = name;
        }
    }
}
