using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public abstract class PatchableFile
    {
        public abstract IList<PatchedByteArray> GetPatches( Context context );
    }
}
