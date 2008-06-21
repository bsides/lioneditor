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
using System.Text;

namespace FFTPatcher.TextEditor
{
    public static class Resources
    {

		#region Static Fields (1) 

        private static Dictionary<string, object> resourceMapping = new Dictionary<string, object>();

		#endregion Static Fields 

		#region Static Properties (1) 


        public static string EntryNames { get { return resourceMapping["EntryNames"] as string; } }


		#endregion Static Properties 

		#region Constructors (1) 

        static Resources()
        {
            resourceMapping["EntryNames"] = GZip.Decompress( Properties.Resources.EntryNames ).ToUTF8String();
        }

		#endregion Constructors 

    }
}
