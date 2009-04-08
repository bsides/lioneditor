using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PatcherLib.Datatypes;

namespace FFTPatcher.SpriteEditor
{
    class AllSpriteAttributes
    {
        const int numSprites = 154;

        private static PatcherLib.Iso.PsxIso.KnownPosition pos =
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, 0x2d74c, numSprites * 4);

        private IList<SpriteAttributes> sprites;

        public SpriteAttributes this[int i]
        {
            get
            {
                if (i >= numSprites)
                    throw new IndexOutOfRangeException(string.Format("index must be less than {0}", numSprites));
                return sprites[i];
            }
        }

        public static AllSpriteAttributes FromPsxIso(Stream iso)
        {
            byte[] bytes = PatcherLib.Iso.PsxIso.ReadFile(iso, pos);
            AllSpriteAttributes result = new AllSpriteAttributes();
            IList<SpriteAttributes> sprites = new SpriteAttributes[numSprites];
            for (int i = 0; i < numSprites; i++)
            {
                sprites[i] = SpriteAttributes.BuildPsx(
                    new PatcherLib.Iso.PsxIso.KnownPosition(pos.Sector, pos.StartLocation + i * 4, 4),
                    bytes.Sub(i * 4, (i + 1) * 4 - 1));
            }
            result.sprites = sprites;
            return result;
        }
    }
}
