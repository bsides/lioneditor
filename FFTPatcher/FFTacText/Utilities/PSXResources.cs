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
    public static class PSXResources
    {

		#region Static Fields (1) 

        private static Dictionary<string, object> resourceMapping = new Dictionary<string, object>();

		#endregion Static Fields 

		#region Static Properties (10) 


        public static byte[] ATCHELP_LZW { get { return resourceMapping["ATCHELP_LZW"] as byte[]; } }

        public static byte[] ATTACK_OUT_partial { get { return resourceMapping["ATTACK_OUT_partial"] as byte[]; } }

        public static byte[] HELPMENU_OUT { get { return resourceMapping["HELPMENU_OUT"] as byte[]; } }

        public static byte[] JOIN_LZW { get { return resourceMapping["JOIN_LZW"] as byte[]; } }

        public static byte[] OPEN_LZW { get { return resourceMapping["OPEN_LZW"] as byte[]; } }

        public static byte[] SAMPLE_LZW { get { return resourceMapping["SAMPLE_LZW"] as byte[]; } }

        public static byte[] SNPLMES_BIN { get { return resourceMapping["SNPLMES_BIN"] as byte[]; } }

        public static byte[] WLDHELP_LZW { get { return resourceMapping["WLDHELP_LZW"] as byte[]; } }

        public static byte[] WLDMES_BIN { get { return resourceMapping["WLDMES_BIN"] as byte[]; } }

        public static byte[] WORLD_LZW { get { return resourceMapping["WORLD_LZW"] as byte[]; } }


		#endregion Static Properties 

		#region Constructors (1) 

        static PSXResources()
        {
            resourceMapping["ATCHELP_LZW"] = GZip.Decompress( Properties.PSXResources.ATCHELP_LZW );
            resourceMapping["ATTACK_OUT_partial"] = GZip.Decompress( Properties.PSXResources.ATTACK_OUT_partial );
            resourceMapping["JOIN_LZW"] = GZip.Decompress( Properties.PSXResources.JOIN_LZW );
            resourceMapping["HELPMENU_OUT"] = GZip.Decompress( Properties.PSXResources.HELPMENU_OUT );
            resourceMapping["OPEN_LZW"] = GZip.Decompress( Properties.PSXResources.OPEN_LZW );
            resourceMapping["SAMPLE_LZW"] = GZip.Decompress( Properties.PSXResources.SAMPLE_LZW );
            resourceMapping["WLDHELP_LZW"] = GZip.Decompress( Properties.PSXResources.WLDHELP_LZW );
            resourceMapping["WORLD_LZW"] = GZip.Decompress( Properties.PSXResources.WORLD_LZW );
            resourceMapping["SNPLMES_BIN"] = GZip.Decompress( Properties.PSXResources.SNPLMES_BIN );
            resourceMapping["WLDMES_BIN"] = GZip.Decompress( Properties.PSXResources.WLDMES_BIN );
        }

		#endregion Constructors 

    }
}
