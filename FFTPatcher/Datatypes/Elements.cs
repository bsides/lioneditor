/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

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

        public Elements( byte b )
        {
            bool[] flags = Utilities.BooleansFromByte( b );
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
            return Utilities.ByteFromBooleans( Fire, Lightning, Ice, Wind, Earth, Water, Holy, Dark );
        }

        public bool[] ToBoolArray()
        {
            return new bool[8] {
                Fire, Lightning, Ice, Wind, Earth, Water, Holy, Dark };
        }
    }
}
