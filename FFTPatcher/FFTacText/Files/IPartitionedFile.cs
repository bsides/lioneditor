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
    /// A <see cref="IFile"/> that is divided into partitions of equal length.
    /// </summary>
    public interface IPartitionedFile : IXmlSerializable, IFile
    {
        /// <summary>
        /// Gets the filename.
        /// </summary>
        string Filename { get; }

        /// <summary>
        /// Gets a collection of <see cref="IPartition"/>, representing the entries in this file.
        /// </summary>
        IList<IPartition> Sections { get; }

        /// <summary>
        /// Gets a collection of strings with a description of each section in this file.
        /// </summary>
        IList<string> SectionNames { get; }

        /// <summary>
        /// Gets a collection of lists of strings, each string being a description of an entry in this file.
        /// </summary>
        IList<IList<string>> EntryNames { get; }

        /// <summary>
        /// Gets the length of every section in this file.
        /// </summary>
        int SectionLength { get; }

        /// <summary>
        /// Gets the number of sections in this file.
        /// </summary>
        int NumberOfSections { get; }

        /// <summary>
        /// Serializes this file to an XML node.
        /// </summary>
        /// <param name="writer">The writer to use to write the node</param>
        /// <param name="compressed">Whether or not this object's data should be compressed.</param>
        void WriteXml( XmlWriter writer, bool compressed );
    }
}
