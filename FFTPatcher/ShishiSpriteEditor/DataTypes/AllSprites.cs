using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PatcherLib.Datatypes;

namespace FFTPatcher.SpriteEditor
{
    class AllSprites
    {
        private IList<Sprite> sprites;
        private AllSpriteAttributes attrs;
        private SpriteFileLocations locs;

        const int numSprites = 154;
        public static AllSprites FromPsxIso(Stream iso)
        {
            return new AllSprites(AllSpriteAttributes.FromPsxIso(iso), SpriteFileLocations.FromPsxIso(iso));
        }

        private AllSprites(AllSpriteAttributes attrs, SpriteFileLocations locs)
        {
            sprites = new Sprite[numSprites];
            for (int i = 0; i < numSprites; i++)
            {
                sprites[i] = new Sprite(attrs[i], locs[i]);
            }
            this.attrs = attrs;
            this.locs = locs;
        }

        public IList<PatchedByteArray> GetPatches()
        {
            PatchedByteArray[] result = new PatchedByteArray[3];
            result[0] = attrs.GetPatchedByteArray();
            locs.GetPatches().CopyTo(result, 1);
            return result;
        }
    }
}
