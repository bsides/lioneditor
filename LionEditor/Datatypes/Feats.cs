using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using LionEditor.Properties;
using System.Xml;

namespace LionEditor
{
    public enum State
    {
        Active = 0x0A,
        Complete = 0x0C,
        None = 0x00
    }

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


    public class Feats
    {
        private List<Feat> allFeats;
        public List<Feat> AllFeats
        {
            get { return allFeats; }
        }

        private static StringCollection featList;
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
