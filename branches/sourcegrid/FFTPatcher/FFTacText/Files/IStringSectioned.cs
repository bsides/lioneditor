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
using System.Xml;
using System.Xml.Serialization;

namespace FFTPatcher.TextEditor.Files
{
    /// <summary>
    /// A <see cref="IFile"/> that is divided into sections.
    /// </summary>
    public interface IStringSectioned : IXmlSerializable, IFile
    {
        /// <summary>
        /// Gets the filename.
        /// </summary>
        string Filename { get; }

        /// <summary>
        /// Gets a collection of lists of strings, representing the entries in this file..
        /// </summary>
        IList<IList<string>> Sections { get; }

        /// <summary>
        /// Gets or sets a specific entry in a specific section.
        /// </summary>
        /// <param name="section">The section which contains the entry to get or set</param>
        /// <param name="entry">The specific entry to get or set</param>
        string this[int section, int entry] { get; set; }

        /// <summary>
        /// Gets a collection of strings with a description of each section in this file.
        /// </summary>
        IList<string> SectionNames { get; }

        /// <summary>
        /// Gets a collection of lists of strings, each string being a description of an entry in this file.
        /// </summary>
        IList<IList<string>> EntryNames { get; }

        /// <summary>
        /// Gets the estimated length of this file if it were turned into a byte array.
        /// </summary>
        int EstimatedLength { get; }

        /// <summary>
        /// Gets the actual length of this file if it were turned into a byte array.
        /// </summary>
        /// <value>The actual length.</value>
        int ActualLength { get; }

        /// <summary>
        /// Serializes this file to an XML node.
        /// </summary>
        /// <param name="writer">The writer to use to write the node</param>
        /// <param name="compressed">Whether or not this object's data should be compressed.</param>
        void WriteXml( XmlWriter writer, bool compressed );

        /// <summary>
        /// Gets the length in bytes of a specific entry.
        /// </summary>
        /// <param name="section">The section which contains the entry whose length is needed.</param>
        /// <param name="entry">The specific entry whose length is needed.</param>
        /// <returns>The length of the entry, in bytes.</returns>
        int GetEntryLength( int section, int entry );
    }
}
