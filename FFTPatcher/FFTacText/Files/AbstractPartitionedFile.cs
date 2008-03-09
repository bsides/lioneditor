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

namespace FFTPatcher.TextEditor.Files
{
    public abstract class AbstractPartitionedFile : IPartitionedFile
    {

		#region Abstract Properties (7) 


        protected abstract TextUtilities.CharMapType CharMap { get; }

        public abstract IList<IList<string>> EntryNames { get; }

        public abstract string Filename { get; }

        public abstract IDictionary<string, long> Locations { get; }

        public abstract int NumberOfSections { get; }

        public abstract int SectionLength { get; }

        public abstract IList<string> SectionNames { get; }


		#endregion Abstract Properties 

		#region Properties (3) 


        public int ActualLength
        {
            get { return ToFinalBytes().Count; }
        }

        public int EstimatedLength
        {
            get { return ToFinalBytes().Count; }
        }

        public IList<IPartition> Sections { get; protected set; }


		#endregion Properties 

		#region Constructors (2) 

        protected AbstractPartitionedFile()
        {
        }

        protected AbstractPartitionedFile( IList<byte> bytes )
        {
            Sections = new List<IPartition>( NumberOfSections );
            for( int i = 0; i < NumberOfSections; i++ )
            {
                Sections.Add( new FilePartition( bytes.Sub( i * SectionLength, (i + 1) * SectionLength - 1 ), EntryNames[i], CharMap ) );
            }
        }

		#endregion Constructors 

		#region Methods (2) 


        protected IList<byte> ToFinalBytes()
        {
            List<byte> result = new List<byte>();
            foreach( IPartition section in Sections )
            {
                result.AddRange( section.ToByteArray() );
            }

            return result;
        }

        public byte[] ToByteArray()
        {
            return ToFinalBytes().ToArray();
        }


		#endregion Methods 

    }
}
