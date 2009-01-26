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

        public abstract IList<PatchedByteArray> GetAllPatches( IDictionary<string, byte> dteTable );

        /// <summary>
        /// Determines how many bytes would be saved if the specified string could be replaced with a single byte.
        /// </summary>
        public abstract IDictionary<string, int> CalculateBytesSaved( Set<string> replacements );

        /// <summary>
        /// Determines if this file will require DTE in order to fit on disc.
        /// </summary>
        /// <returns></returns>
        public abstract bool IsDTENeeded();

        /// <summary>
        /// Gets the DTE pairs that this file needs in order to fit on disc, given a set of possible DTE pairs
        /// and a set of the pairs that are already required.
        /// </summary>
        /// <param name="replacements"></param>
        /// <param name="currentPairs"></param>
        /// <returns></returns>
        public abstract Set<KeyValuePair<string, byte>> GetPreferredDTEPairs( Set<string> replacements, Set<KeyValuePair<string, byte>> currentPairs, Stack<byte> dteBytes );

        /// <summary>
        /// Creates a byte array representing this file with DTE substitutions performed as specified.
        /// </summary>
        public abstract byte[] ToByteArray( IDictionary<string, byte> dteTable );

    }
}
