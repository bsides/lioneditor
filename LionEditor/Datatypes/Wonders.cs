using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using LionEditor.Properties;
using System.Xml;

namespace LionEditor
{
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


    public class Wonders
    {
        private List<Wonder> allWonders;
        public List<Wonder> AllWonders
        {
            get { return allWonders; }
        }

        private static StringCollection wonderList;
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
