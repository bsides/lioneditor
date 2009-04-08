using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PatcherLib.Datatypes;
using PatcherLib;
using PatcherLib.Utilities;

namespace FFTPatcher.SpriteEditor
{
    public class AllSprites
    {
        private IList<Sprite> sprites;
        private AllSpriteAttributes attrs;
        private SpriteFileLocations locs;

        public const int NumSprites = 154;
        const long defaultIsoLength = 541315152;
        const long expandedIsoLength = 552680016;
        const long defaultSectorCount = 230151;
        const long expandedSectorCount = 0x000395E7;


        public Sprite this[int i]
        {
            get { return sprites[i]; }
        }

        public static AllSprites FromPsxIso(Stream iso)
        {
            return new AllSprites(AllSpriteAttributes.FromPsxIso(iso), SpriteFileLocations.FromPsxIso(iso));
        }

        public static bool DetectExpansionOfPsxIso(Stream iso)
        {
            UInt32 sectors = PatcherLib.Iso.PsxIso.ReadFile(iso, PatcherLib.Iso.PsxIso.NumberOfSectorsLittleEndian).ToUInt32();

            return iso.Length > defaultIsoLength &&
                iso.Length >= expandedIsoLength &&
                sectors > defaultSectorCount &&
                sectors >= expandedSectorCount &&
                !SpriteFileLocations.IsoHasDefaultSpriteLocations( iso );
        }

        private AllSprites(AllSpriteAttributes attrs, SpriteFileLocations locs)
        {
            sprites = new Sprite[NumSprites];
            for (int i = 0; i < NumSprites; i++)
            {
                sprites[i] = new Sprite(string.Format("{0:X2} - {1}", i, PSXResources.SpriteFiles[i]), attrs[i], locs[i]);
            }
            this.attrs = attrs;
            this.locs = locs;
        }

    }
}
