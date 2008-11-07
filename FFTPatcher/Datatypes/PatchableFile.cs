using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public abstract class PatchableFile : IChangeable
    {
		#region Public Methods (1) 

        public abstract IList<PatchedByteArray> GetPatches( Context context );

		#endregion Public Methods 



        #region IChangeable Members

        public abstract bool HasChanged { get; }

        #endregion
    }
}
