﻿using System;
using System.Collections.Generic;
using PatcherLib.Datatypes;
using PatcherLib.Utilities;

namespace FFTPatcher.TextEditor
{
    class PartitionedFile : AbstractFile
    {
        public int PartitionSize { get; private set; }

        public PartitionedFile( GenericCharMap map, FFTTextFactory.FileInfo layout, IList<IList<string>> strings )
            : base( map, layout, strings, false )
        {
            PartitionSize = layout.Size / NumberOfSections;
        }

        public PartitionedFile( GenericCharMap map, FFTPatcher.TextEditor.FFTTextFactory.FileInfo layout, IList<byte> bytes )
            : base( map, layout, false )
        {
            PartitionSize = layout.Size / NumberOfSections;
            List<IList<string>> sections = new List<IList<string>>( NumberOfSections );
            for ( int i = 0; i < NumberOfSections; i++ )
            {
                sections.Add(TextUtilities.ProcessList(bytes.Sub(i * PartitionSize, (i + 1) * PartitionSize - 1), map));
                if ( sections[i].Count < SectionLengths[i] )
                {
                    string[] newSection = new string[SectionLengths[i]];
                    sections[i].CopyTo( newSection, 0 );
                    new string[SectionLengths[i] - sections[i].Count].CopyTo( newSection, sections[i].Count );
                    sections[i] = newSection;
                }
                else if (sections[i].Count > SectionLengths[i])
                {
                    sections[i] = sections[i].Sub(0, SectionLengths[i] - 1);
                }

                System.Diagnostics.Debug.Assert(sections[i].Count == SectionLengths[i]);
            }
            Sections = sections.AsReadOnly();
            PopulateDisallowedSections();
        }

        protected override IList<byte> ToByteArray()
        {
            List<byte> result = new List<byte>( Layout.Size );
            foreach ( IList<string> section in Sections )
            {
                List<byte> currentPart = new List<byte>( PartitionSize );
                section.ForEach( s => currentPart.AddRange( CharMap.StringToByteArray( s ) ) );
                currentPart.AddRange( new byte[Math.Max( PartitionSize - currentPart.Count, 0 )] );
                result.AddRange( currentPart );
            }

            return result.AsReadOnly();
        }

        protected override IList<byte> ToByteArray( IDictionary<string, byte> dteTable )
        {
            // Clone the sections
            var secs = GetCopyOfSections();
            TextUtilities.DoDTEEncoding(secs, DteAllowed, dteTable);
            List<byte> result = new List<byte>(Layout.Size);
            foreach (IList<string> section in secs)
            {
                List<byte> currentPart = new List<byte>(PartitionSize);
                section.ForEach(s => currentPart.AddRange(CharMap.StringToByteArray(s)));
                if (currentPart.Count > PartitionSize)
                {
                    return null;
                }
                currentPart.AddRange(new byte[Math.Max(PartitionSize - currentPart.Count, 0)]);
                result.AddRange(currentPart);
            }

            if (result.Count > Layout.Size)
            {
                return null;
            }
            return result.AsReadOnly();
        }
    }
}
