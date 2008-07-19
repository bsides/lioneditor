using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public abstract class PatchableFile : IChangeable
    {
        public abstract IList<PatchedByteArray> GetPatches( Context context );

        #region IChangeable Members

        public abstract bool HasChanged { get; }

        #endregion
    }
}
