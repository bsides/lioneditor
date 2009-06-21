using System;
using System.Collections.Generic;
using System.Text;
using PatcherLib.Datatypes;
using PatcherLib;
using PatcherLib.Utilities;
using System.IO;
using PatcherLib.Iso;

namespace FFTPatcher.TextEditor
{
    static partial class DTE
    {
        public static class DTEAnalyzer
        {
            public static class PSX
            {
                static FFTFont defaultFont = TextUtilities.PSXFont;
                static GenericCharMap defaultMap = TextUtilities.PSXMap;

                public static GenericCharMap GetCharMap( Stream iso )
                {
                    IList<byte> dteBytes = PsxIso.ReadFile( iso, DTE.PsxDteTable );
                    return GetCharMap( dteBytes );
                }

                public static GenericCharMap GetCharMap( IList<byte> dteTable )
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