using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.TextEditor.Files
{
    public abstract class AbstractFile : IFile
    {
        /// <summary>
        /// Gets the filenames and locations for this file.
        /// </summary>
        public abstract IDictionary<Enum, long> Locations { get; }

        /// <summary>
        /// Gets the charmap to use for this file.
        /// </summary>
        public abstract GenericCharMap CharMap { get; }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        public abstract int MaxLength { get; }

        /// <summary>
        /// Creates a byte array representing this file.
        /// </summary>
        public abstract byte[] ToByteArray();

        /// <summary>
        /// Gets all patches that this file needs to apply to the ISO for full functionality.
        /// </summary>
        public abstract IList<PatchedByteArray> GetAllPatches();
    }
}
