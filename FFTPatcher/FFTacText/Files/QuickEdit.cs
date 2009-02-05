using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.TextEditor.Files
{
    class QuickEdit : IFile
    {
        public IList<string> SectionNames { get; private set; }
        public IList<IList<string>> EntryNames { get; private set; }

        public GenericCharMap CharMap { get; private set; }

        public int NumberOfSections { get; private set; }

        public string this[int section, int entry]
        {
            get { return sections[section][entry]; }
            set
            {
                sections[section][entry] = value;
                IList<QuickEditEntry> needToUpdate = lookup[sectionTypes[section]];
                foreach ( var v in needToUpdate )
                {
                    files[v.Guid][v.Section, entry] = value;
                }
            }
        }

        public IList<int> SectionLengths { get; private set; }

        private IList<IList<string>> sections;
        private Dictionary<SectionType, IList<QuickEditEntry>> lookup;
        private IList<SectionType> sectionTypes;
        private Dictionary<Guid, ISerializableFile> files;

        public QuickEdit( IDictionary<Guid, ISerializableFile> files, IDictionary<SectionType, IList<QuickEditEntry>> sections )
        {
            this.files = new Dictionary<Guid, ISerializableFile>( files );
            lookup = new Dictionary<SectionType, IList<QuickEditEntry>>( sections );

            List<IList<string>> sections2 = new List<IList<string>>( sections.Count );
            List<IList<string>> entryNames = new List<IList<string>>( sections.Count );
            List<SectionType> sectionTypes = new List<SectionType>( sections.Count );
            List<int> sectionLengths = new List<int>( sections.Count );
            List<string> sectionNames = new List<string>();
            foreach ( KeyValuePair<SectionType, IList<QuickEditEntry>> kvp in sections )
            {
                CharMap = CharMap ?? files[kvp.Value[0].Guid].CharMap;

                IList<QuickEditEntry> entries = kvp.Value;
                QuickEditEntry mainEntry = entries.FindAll( e => e.Main )[0];
                ISerializableFile mainFile = files[mainEntry.Guid];
                int entryCount = mainEntry.Length;
                List<string> names = new List<string>( entryCount );
                List<string> values = new List<string>( entryCount );
                for ( int i = mainEntry.Offset; i < ( mainEntry.Offset + entryCount ); i++ )
                {
                    names.Add( mainFile.EntryNames[mainEntry.Section][i] );
                    values.Add( mainFile[mainEntry.Section, i] );
                }
                entryNames.Add( names.AsReadOnly() );
                sections2.Add( values.ToArray() );
                sectionLengths.Add( entryCount );
                sectionTypes.Add( kvp.Key );
                sectionNames.Add( kvp.Key.ToString() );
            }

            this.sections = sections2.AsReadOnly();
            EntryNames = entryNames.AsReadOnly();
            NumberOfSections = sections.Count;
            this.sectionTypes = sectionTypes.AsReadOnly();
            this.SectionNames = sectionNames.AsReadOnly();
            this.SectionLengths = sectionLengths.AsReadOnly();
        }

        public struct QuickEditEntry
        {
            public Guid Guid { get; set; }
            public bool Main { get; set; }
            public int Section { get; set; }
            public int Offset { get; set; }
            public int Length { get; set; }
        }


        public string DisplayName
        {
            get { return "QuickEdit"; }
        }
    }
}
