using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FFTPatcher.TextEditor
{
    public class PSXText : IXmlSerializable
    {

		#region Methods (3) 


        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml( XmlReader reader )
        {
        }

        public void WriteXml( XmlWriter writer )
        {
        }


		#endregion Methods 

    }
}
