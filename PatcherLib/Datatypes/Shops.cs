using System;

namespace PatcherLib.Datatypes
{
    [Flags]
    public enum Shops
    {
        Lesalia = 0x8000,
        Riovanes = 0x4000,
        Igros = 0x2000,
        Lionel = 0x1000,
        Limberry = 0x0800,
        Zeltennia = 0x0400,
        Gariland = 0x0200,
        Yardrow = 0x0100,
        Goland = 0x0080,
        Dorter = 0x0040,
        Zaland = 0x0020,
        Goug = 0x0010,
        Warjilis = 0x0008,
        Bervenia = 0x0004,
        Zarghidas = 0x0002,
        None = 0x0001,
        Empty = 0
    }
}
