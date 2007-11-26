using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using LionEditor.Properties;
using System.Xml;

namespace LionEditor
{
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


    public class Artefacts
    {
        private List<Artefact> allArtefacts;
        public List<Artefact> AllArtefacts
        {
            get { return allArtefacts; }
        }

        private static StringCollection artefactList;
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

        public Artefacts( byte[] dates, byte[] states )
        {
            allArtefacts = new List<Artefact>( 48 );

            for( int i = 0; i < 47; i++ )
            {
                Artefact w = new Artefact();
                w.Date = StupidDate.GetDateFromOffset( i, dates );
                w.Name = ArtefactList[i];
                w.Discovered = (states[(i+1) / 8] & (0x01 << ((i+1) % 8))) > 0;

                allArtefacts.Add( w );
            }
        }

        public byte[] DatesToByteArray()
        {
            byte[] result = new byte[53];
            for( int i = 1; i < 48; i++ )
            {
                AllArtefacts[i-1].Date.SetDateAtOffset( i-1, result );
            }

            return result;
        }

        public byte[] StatesToByteArray()
        {
            byte[] result = new byte[6];
            for( int i = 1; i < 48; i++ )
            {
                result[(i-1) / 8] |= (byte)((AllArtefacts[i-1].Discovered) ? (1 << ((i-1) % 8)) : 0);
            }

            return result;
        }
    }
}
