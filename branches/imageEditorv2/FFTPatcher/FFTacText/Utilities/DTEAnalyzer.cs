using System;
using System.Collections.Generic;
using System.Text;
using PatcherLib.Datatypes;
using PatcherLib;
using PatcherLib.Utilities;

namespace FFTPatcher.TextEditor
{
    static partial class DTE
    {
        static class DTEAnalyzer
        {
            static class PSX
            {
                static FFTFont defaultFont = TextUtilities.PSXFont;
                static GenericCharMap defaultMap = TextUtilities.PSXMap;

                static GenericCharMap GetCharMap(IList<byte> fontBytes, IList<byte> widthBytes, IList<byte> dteTable)
                {
                    Dictionary<int, string> myCharMap = new Dictionary<int, string>(defaultMap);

                    IList<string> dtePairs = GetDtePairs(dteTable);
                    for (int i = 0; i < dtePairs.Count; i++)
                    {
                        if (string.IsNullOrEmpty(dtePairs[i])) continue;

                        myCharMap[i + DTE.MinDteByte] = dtePairs[i];
                    }

                    return new NonDefaultCharMap(myCharMap);
                }

                static IList<string> GetDtePairs(IList<byte> dteTable)
                {
                    string[] result = new string[dteTable.Count / 2];
                    for (int i = 0; i < dteTable.Count; i += 2)
                    {
                        if (dteTable[i] == 0 && dteTable[i + 1] == 0) continue;

                        string firstChar = defaultMap[(int)dteTable[i + 0]];
                        string secondChar = defaultMap[(int)dteTable[i + 1]];
                        result[i / 2] = firstChar + secondChar;
                    }

                    return result.AsReadOnly();
                }
            }

            static class PSP
            {
            }
        }
    }
}