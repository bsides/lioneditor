/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace FFTPatcher.Datatypes
{
    /// <summary>
    /// Represents a special "name" a unit can have.
    /// </summary>
    public class SpecialName
    {
        private static SpecialName[] pspNames = new SpecialName[256];
        private static SpecialName[] psxNames = new SpecialName[256];
        public static SpecialName[] SpecialNames
        {
            get { return FFTPatch.Context == Context.US_PSP ? pspNames : psxNames; }
        }

        static SpecialName()
        {
            string[] pspStrings = Utilities.GetStringsFromNumberedXmlNodes(
                Resources.SpecialNames,
                "//SpecialNames/SpecialName[@byte='{0:X2}']/@name",
                256 );
            string[] psxStrings = Utilities.GetStringsFromNumberedXmlNodes(
                PSXResources.SpecialNames,
                "//SpecialNames/SpecialName[@byte='{0:X2}']/@name",
                256 );
            for( int i = 0; i < 256; i++ )
            {
                pspNames[i] = new SpecialName( (byte)i, pspStrings[i] );
                psxNames[i] = new SpecialName( (byte)i, psxStrings[i] );
            }
        }

        public byte Value { get; private set; }
        public string Name { get; private set; }

        private SpecialName( byte value, string name )
        {
            Value = value;
            Name = name;
        }

        public byte ToByte()
        {
            return Value;
        }

        public override string ToString()
        {
            return string.Format( "{0:X2} {1}", Value, Name );
        }
    }
}
