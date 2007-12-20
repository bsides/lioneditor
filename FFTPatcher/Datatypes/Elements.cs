using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public class Elements
    {
        public bool Fire { get; set; }
        public bool Lightning { get; set; }
        public bool Ice { get; set; }
        public bool Wind { get; set; }
        public bool Earth { get; set; }
        public bool Water { get; set; }
        public bool Holy { get; set; }
        public bool Dark { get; set; }

        public Elements(byte b)
        {
            bool[] flags = Utilities.BooleansFromByte(b);
            Fire = flags[7];
            Lightning = flags[6];
            Ice = flags[5];
            Wind = flags[4];
            Earth = flags[3];
            Water = flags[2];
            Holy = flags[1];
            Dark = flags[0];
        }

        public byte ToByte()
        {
            return Utilities.ByteFromBooleans(Fire, Lightning, Ice, Wind, Earth, Water, Holy, Dark);
        }
    }
}
