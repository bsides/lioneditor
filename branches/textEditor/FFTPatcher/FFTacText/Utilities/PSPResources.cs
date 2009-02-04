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

using System.Collections.Generic;

namespace FFTPatcher.TextEditor
{
    /// <summary>
    /// Resources for the PSP version.
    /// </summary>
    public static class PSPResources
    {

        #region Static Fields (1)

        private static Dictionary<string, object> resourceMapping = new Dictionary<string, object>();

        #endregion Static Fields

        #region Static Properties (1)


        /// <summary>
        /// Gets the default set of text to be deserialized into a <see cref="FFTText"/>
        /// </summary>
        public static byte[] DefaultDocument { get { return resourceMapping["pspText_ffttext"] as byte[]; } }


        #endregion Static Properties

        #region Constructors (1)

        //public static byte[] ATCHELP_LZW { get { return resourceMapping["ATCHELP_LZW"] as byte[]; } }
        //public static byte[] BOOT_299024 { get { return resourceMapping["BOOT_299024"] as byte[]; } }
        //public static byte[] BOOT_29E334 { get { return resourceMapping["BOOT_29E334"] as byte[]; } }
        //public static byte[] BOOT_2A1630 { get { return resourceMapping["BOOT_2A1630"] as byte[]; } }
        //public static byte[] BOOT_2EB4C0 { get { return resourceMapping["BOOT_2EB4C0"] as byte[]; } }
        //public static byte[] BOOT_32D368 { get { return resourceMapping["BOOT_32D368"] as byte[]; } }
        //public static byte[] OPEN_LZW { get { return resourceMapping["OPEN_LZW"] as byte[]; } }
        //public static byte[] WLDMES_BIN { get { return resourceMapping["WLDMES_BIN"] as byte[]; } }
        static PSPResources()
        {
            //resourceMapping["pspText_ffttext"] = Encoding.UTF8.GetString( GZip.Decompress( Properties.PSPResources.pspText_ffttext ) );
            //resourceMapping["pspText_ffttext"] = GZip.Decompress( Properties.PSPResources.pspText_ffttext );
            //resourceMapping["ATCHELP_LZW"] = GZip.Decompress( Properties.PSPResources.ATCHELP_LZW );
            //resourceMapping["BOOT_299024"] = GZip.Decompress( Properties.PSPResources.BOOT_299024 );
            //resourceMapping["BOOT_29E334"] = GZip.Decompress( Properties.PSPResources.BOOT_29E334 );
            //resourceMapping["BOOT_2A1630"] = GZip.Decompress( Properties.PSPResources.BOOT_2A1630 );
            //resourceMapping["BOOT_2EB4C0"] = GZip.Decompress( Properties.PSPResources.BOOT_2EB4C0 );
            //resourceMapping["BOOT_32D368"] = GZip.Decompress( Properties.PSPResources.BOOT_32D368 );
            //resourceMapping["OPEN_LZW"] = GZip.Decompress( Properties.PSPResources.OPEN_LZW );
            //resourceMapping["WLDMES_BIN"] = GZip.Decompress( Properties.PSPResources.WLDMES_BIN );
        }

        #endregion Constructors

    }
}
