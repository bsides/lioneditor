using System.Collections.Generic;

namespace FFTPatcher.TextEditor
{
    public interface IFile
    {
        IList<string> SectionNames { get; }
        IList<IList<string>> EntryNames { get; }
        GenericCharMap CharMap { get; }
        int NumberOfSections { get; }
        string this[int section, int entry] { get; set; }
        IList<int> SectionLengths { get; }
        string DisplayName { get; }
    }
}
