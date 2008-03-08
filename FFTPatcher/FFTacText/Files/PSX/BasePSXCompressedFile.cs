using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.TextEditor.Files.PSX
{
    public abstract class BasePSXCompressedFile : AbstractCompressedFile
    {

		#region Properties (2) 


        public override TextUtilities.CharMapType CharMap { get { return TextUtilities.CharMapType.PSX; } }

        public override int EstimatedLength
        {
            get { return (int)(base.EstimatedLength * 0.65346430772862594919277); }
        }


		#endregion Properties 

    }
}
