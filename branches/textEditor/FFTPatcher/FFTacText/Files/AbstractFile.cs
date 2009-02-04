using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.TextEditor
{
    abstract class AbstractFile : ISerializableFile
    {
        public virtual byte[] ToByteArray()
        {
            throw new NotImplementedException();
        }

        public virtual byte[] ToTextByteArray()
        {
            throw new NotImplementedException();
        }

        protected AbstractFile( GenericCharMap charmap, FFTPatcher.TextEditor.FFTTextFactory.FileInfo layout )
        {
            NumberOfSections = layout.SectionLengths.Count;
            Layout = layout;
            CharMap = charmap;
            EntryNames = layout.EntryNames;
            SectionLengths = layout.SectionLengths.AsReadOnly();
            SectionNames = layout.SectionNames;
            DisplayName = layout.DisplayName;
        }

        public virtual string this[int section, int entry]
        {
            get { return Sections[section][entry]; }
            set 
            {
                if ( section < SectionLengths.Count && 
                     entry < SectionLengths[section] && 
                     !Layout.DisallowedEntries[section].Contains( entry ) )
                {
                    Sections[section][entry] = value;
                }
            }
        }

        public FFTPatcher.TextEditor.FFTTextFactory.FileInfo Layout { get; private set; }

        public GenericCharMap CharMap { get; private set; }

        protected IList<IList<string>> Sections { get; set; }

        public IList<IList<string>> EntryNames { get; private set; }

        public int NumberOfSections { get; private set; }

        public IList<int> SectionLengths { get; private set; }

        public IList<string> SectionNames { get; private set; }


        public string DisplayName { get; private set; }
    }
}
