using System;
using System.Collections.Generic;
using System.Text;

namespace PatcherLib.Iso
{
    public abstract class KnownPosition
    {
        public abstract void PatchIso(System.IO.Stream iso, IList<byte> bytes);
        public abstract IList<byte> ReadIso(System.IO.Stream iso);
        public abstract PatcherLib.Datatypes.PatchedByteArray GetPatchedByteArray(byte[] bytes);
        public abstract int Length { get; }
    }
}
