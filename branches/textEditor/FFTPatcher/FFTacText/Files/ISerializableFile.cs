using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.TextEditor
{
    interface ISerializableFile : IFile
    {
        FFTPatcher.TextEditor.FFTTextFactory.FileInfo Layout { get; }
        byte[] ToByteArray();
        byte[] ToTextByteArray();
    }
}
