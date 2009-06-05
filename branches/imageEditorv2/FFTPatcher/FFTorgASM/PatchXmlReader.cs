using System;
using System.Collections.Generic;
using System.Xml;
using PatcherLib.Datatypes;
using PatcherLib.Iso;

namespace FFTorgASM
{
    static class PatchXmlReader
    {
        public static readonly System.Text.RegularExpressions.Regex stripRegex = 
            new System.Text.RegularExpressions.Regex( @"\s" );

        public static bool TryGetPatches( string xmlString, out IList<AsmPatch> patches )
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml( xmlString );
                patches = GetPatches( doc.SelectSingleNode( "/Patches" ) );
                return true;
            }
            catch ( Exception )
            {
                patches = null;
                return false;
            }
        }

        private static void GetPatch( XmlNode node, out string name, out string description, out IList<PatchedByteArray> staticPatches )
        {
            name = node.Attributes["name"].InnerText;
            XmlNode descriptionNode = node.SelectSingleNode( "Description" );
            description = name;
            if ( descriptionNode != null )
            {
                description = descriptionNode.InnerText;
            }

            XmlNodeList currentLocs = node.SelectNodes( "Location" );
            List<PatchedByteArray> patches = new List<PatchedByteArray>( currentLocs.Count );

            foreach ( XmlNode location in currentLocs )
            {
                UInt32 offset = UInt32.Parse( location.Attributes["offset"].InnerText, System.Globalization.NumberStyles.HexNumber );
                PsxIso.Sectors sector = (PsxIso.Sectors)Enum.Parse( typeof( PsxIso.Sectors ), location.Attributes["file"].InnerText );
                byte[] bytes = GetBytes( location.InnerText );
                patches.Add( new PatchedByteArray( sector, offset, bytes ) );
            }

            currentLocs = node.SelectNodes("STRLocation");
            foreach (XmlNode location in currentLocs)
            {
                PsxIso.Sectors sector = (PsxIso.Sectors)Enum.Parse(typeof(PsxIso.Sectors), location.Attributes["file"].InnerText);
                string filename = location.Attributes["input"].InnerText;

                patches.Add(new STRPatchedByteArray(sector, filename));
            }

            staticPatches = patches.AsReadOnly();
        }

        public static IList<AsmPatch> GetPatches( XmlNode rootNode )
        {
            XmlNodeList patchNodes = rootNode.SelectNodes( "Patch" );
            List<AsmPatch> result = new List<AsmPatch>( patchNodes.Count );
            foreach ( XmlNode node in patchNodes )
            {
                XmlAttribute ignoreNode = node.Attributes["ignore"];
                if ( ignoreNode != null && Boolean.Parse( ignoreNode.InnerText ) )
                    continue;

                string name;
                string description;
                IList<PatchedByteArray> staticPatches;
                GetPatch( node, out name, out description, out staticPatches );
                List<KeyValuePair<string, PatchedByteArray>> variables = new List<KeyValuePair<string, PatchedByteArray>>();
                foreach ( XmlNode varNode in node.SelectNodes( "Variable" ) )
                {
                    string varName = varNode.Attributes["name"].InnerText;
                    PsxIso.Sectors varSec = (PsxIso.Sectors)Enum.Parse( typeof( PsxIso.Sectors ), varNode.Attributes["file"].InnerText );
                    UInt32 varOffset = UInt32.Parse( varNode.Attributes["offset"].InnerText, System.Globalization.NumberStyles.HexNumber );
                    XmlAttribute defaultAttr = varNode.Attributes["default"];
                    byte def = 0;
                    if ( defaultAttr != null )
                    {
                        def = Byte.Parse( defaultAttr.InnerText, System.Globalization.NumberStyles.HexNumber );
                    }

                    variables.Add( new KeyValuePair<string, PatchedByteArray>( varName, new PatchedByteArray( varSec, varOffset, new byte[1] { def } ) ) );
                }
                result.Add( new AsmPatch( name, description, staticPatches, variables ) );
            }
            return result.AsReadOnly();
        }

        private static byte[] GetBytes( string byteText )
        {
            string strippedText = stripRegex.Replace( byteText, string.Empty );
    
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
