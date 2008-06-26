using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.TextEditor.Files.PSP
{
    public class BOOT326F24 : AbstractDelimitedFile
    {
        public override GenericCharMap CharMap
        {
            get { return TextUtilities.PSPMap; }
        }

        public override string Filename
        {
            get { return "BOOT.BIN"; }
        }

        private static Dictionary<string, long> locations;
        public override IDictionary<string, long> Locations
        {
            get
            {
                if ( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "BOOT.BIN", 0x326F24 );
                }
                return locations;
            }
        }

        public override int MaxLength
        {
            get { return 0x6441; }
        }

        public BOOT326F24( IList<byte> bytes )
            : base( bytes )
        {
        }

        private BOOT326F24()
        {
        }

    }

}
