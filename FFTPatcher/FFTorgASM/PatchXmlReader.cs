using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using PatcherLib.Iso;
using PatcherLib.Datatypes;

namespace FFTorgASM
{
    static class PatchXmlReader
    {
        public static IList<AsmPatch> GetPatches( XmlNode rootNode )
        {
            XmlNodeList patchNodes = rootNode.SelectNodes( "Patch" );
            List<AsmPatch> result = new List<AsmPatch>( patchNodes.Count );
            foreach ( XmlNode node in patchNodes )
            {
                string name = node.Attributes["name"].InnerText;
                XmlNodeList currentLocs = node.SelectNodes( "Location" );
                List<PatchedByteArray> patches = new List<PatchedByteArray>( currentLocs.Count );

                foreach ( XmlNode location in currentLocs )
                {
                    UInt32 offset = UInt32.Parse( location.Attributes["offset"].InnerText, System.Globalization.NumberStyles.HexNumber );
                    PsxIso.Sectors sector = (PsxIso.Sectors)Enum.Parse( typeof( PsxIso.Sectors ), location.Attributes["file"].InnerText );
                    byte[] bytes = GetBytes( location.InnerText );
                    patches.Add( new PatchedByteArray( sector, offset, bytes ) );
                }
                result.Add( new AsmPatch( name, patches ) );
            }
            return result.AsReadOnly();
        }

        private static byte[] GetBytes(string byteText)
        {
            string strippedText = byteText
                .Replace( " ", string.Empty )
                .Replace( Environment.NewLine, string.Empty )
                .Replace( "\r", string.Empty )
                .Replace( "\n", string.Empty );
            int bytes = strippedText.Length / 2;
            byte[] result = new byte[bytes];

            for ( int i = 0; i < bytes; i++ )
            {
                result[i] = Byte.Parse( strippedText.Substring( i * 2, 2 ), System.Globalization.NumberStyles.HexNumber );
            }
            return result;
        }
    }
}
