using System;
using System.Collections.Generic;
using System.Text;
using PatcherLib.Utilities;
using PatcherLib.Datatypes;
using System.IO;

namespace FFTPatcher.SpriteEditor
{
    
    public class SpriteLocation
    {
        public UInt32 Sector { get; set; }
        public UInt32 Size { get; set; }
        public SpriteLocation(IList<byte> bytes)
        {
            System.Diagnostics.Debug.Assert(bytes.Count == 8);
            Sector = bytes.Sub(0, 3).ToUInt32();
            Size = bytes.Sub(4).ToUInt32();
        }

        internal SpriteLocation()
        {
        }
    }

    class SpriteFileLocations
    {
        private static PatcherLib.Iso.PsxIso.KnownPosition SpriteLocationsPosition =
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, 0x2DCDC, numSprites * 8);
        private static PatcherLib.Iso.PsxIso.KnownPosition SP2LocationsPosition =
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, 0x2E60C, numSp2 * 8);

        const int numSprites = 154;
        const int numSp2 = 0xD8 / 8;

        private IList<SpriteLocation> sprites;
        private IList<SpriteLocation> sp2;

        private SpriteFileLocations()
        {
        }

        public static SpriteFileLocations FromPsxIso(Stream iso)
        {
            SpriteFileLocations result = new SpriteFileLocations();

            byte[] spriteBytes = PatcherLib.Iso.PsxIso.ReadFile(iso, SpriteLocationsPosition);
            byte[] sp2Bytes = PatcherLib.Iso.PsxIso.ReadFile(iso, SP2LocationsPosition);

            IList<SpriteLocation> sprites = new SpriteLocation[numSprites];
            for (int i = 0; i < numSprites; i++)
            {
                sprites[i] = new SpriteLocation(spriteBytes.Sub(i * 8, (i + 1) * 8 - 1));
            }
            result.sprites = sprites;

            IList<SpriteLocation> sp2 = new SpriteLocation[numSp2];
            for (int i = 0; i < numSp2; i++)
            {
                sp2[i] = new SpriteLocation(sp2Bytes.Sub(i * 8, (i + 1) * 8 - 1));
            }
            result.sp2 = sp2;

            return result;
        }

        public SpriteLocation this[int i]
        {
            get
            {
                if (i >= numSprites)
                {
                    throw new IndexOutOfRangeException(string.Format("max index is {0}", numSprites - 1));
                }
                return sprites[i];
            }
        }

        public IList<PatchedByteArray> GetPatches()
        {
            PatchedByteArray[] result = new PatchedByteArray[2];
            result[0] = GetSpritePatch();
            result[1] = GetSp2Patch();
            return result;
        }

        private PatchedByteArray GetSpritePatch()
        {
            List<byte> result = new List<byte>(SpriteLocationsPosition.Length);
            foreach (SpriteLocation s in sprites)
            {
                result.AddRange(s.Sector.ToBytes());
                result.AddRange(s.Size.ToBytes());
            }
            return SpriteLocationsPosition.GetPatchedByteArray(result.ToArray());
        }

        public PatchedByteArray GetSp2Patch()
        {
            List<byte> result = new List<byte>(SP2LocationsPosition.Length);
            foreach (SpriteLocation s in sp2)
            {
                result.AddRange(s.Sector.ToBytes());
                result.AddRange(s.Size.ToBytes());
            }
            return SP2LocationsPosition.GetPatchedByteArray(result.ToArray());
        }

        // This data stored at offset 0x2E60C in BATTLE.BIN
        private static IList<SpriteLocation> DefaultSp2Locations = new SpriteLocation[numSp2] {
            new SpriteLocation { Sector = 0x0000EB01, Size = 0x00008000 }, // BOM2
            new SpriteLocation { Sector = 0x0000EB31, Size = 0x00008000 }, // HYOU2
            new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // blank
            new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // blank
            new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // blank
            new SpriteLocation { Sector = 0x0000EAD1, Size = 0x00008000 }, // ARLI2
            new SpriteLocation { Sector = 0x0000EBA1, Size = 0x00008000 }, // TORI2
            new SpriteLocation { Sector = 0x0000EBB1, Size = 0x00008000 }, // URI2
            new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
            new SpriteLocation { Sector = 0x0000EB81, Size = 0x00008000 }, // MINOTA2
            new SpriteLocation { Sector = 0x0000EB91, Size = 0x00008000 }, // MOL2
            new SpriteLocation { Sector = 0x0000EAE1, Size = 0x00008000 }, // BEHI2
            new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
            new SpriteLocation { Sector = 0x0000EB21, Size = 0x00008000 }, // DORA22
            new SpriteLocation { Sector = 0x0000EAF1, Size = 0x00008000 }, // BIBU2
            new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
            new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
            new SpriteLocation { Sector = 0x0000EB11, Size = 0x00008000 }, // DEMON2
            new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
            new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // BLank
            new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
            new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
            new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
            new SpriteLocation { Sector = 0x0000EB71, Size = 0x00008000 }, // IRON5
            new SpriteLocation { Sector = 0x0000EB61, Size = 0x00008000 }, // IRON4
            new SpriteLocation { Sector = 0x0000EB41, Size = 0x00008000 }, // IRON2
            new SpriteLocation { Sector = 0x0000EB51, Size = 0x00008000 } // IRON3
            }.AsReadOnly();

        // This data stored at offset 0x2DCDC in BATTLE.BIN
        private static IList<SpriteLocation> DefaultSpriteLocations = new SpriteLocation[numSprites] {
            new SpriteLocation { Sector = 0x0000E68D, Size = 0x0000B000 }, // RAMUZA
            new SpriteLocation { Sector = 0x0000E6A3, Size = 0x0000B000 }, // RAMUZA2
            new SpriteLocation { Sector = 0x0000E6B9, Size = 0x0000B000 }, // RAMUZA3
            new SpriteLocation { Sector = 0x0000E0F2, Size = 0x0000B000 }, // DILY
            new SpriteLocation { Sector = 0x0000E108, Size = 0x0000B000 }, // DILY2
            new SpriteLocation { Sector = 0x0000E11E, Size = 0x0000B000 }, // DILY3
            new SpriteLocation { Sector = 0x0000DF8D, Size = 0x0000B000 }, // ARU
            new SpriteLocation { Sector = 0x0000E97C, Size = 0x0000B000 }, // ZARU
            new SpriteLocation { Sector = 0x0000E0AF, Size = 0x0000B000 }, // DAISU
            new SpriteLocation { Sector = 0x0000E676, Size = 0x0000B800 }, // RAGU
            new SpriteLocation { Sector = 0x0000E239, Size = 0x0000B000 }, // GORU
            new SpriteLocation { Sector = 0x0000E387, Size = 0x0000B000 }, // HIME
            new SpriteLocation { Sector = 0x0000E64A, Size = 0x0000B000 }, // ORU
            new SpriteLocation { Sector = 0x0000E1CD, Size = 0x0000B000 }, // FYUNE
            new SpriteLocation { Sector = 0x0000E6CF, Size = 0x0000B000 }, // REZE
            new SpriteLocation { Sector = 0x0000E9BF, Size = 0x0000B800 }, // ZARUMOU
            new SpriteLocation { Sector = 0x0000E013, Size = 0x0000B000 }, // BARUNA
            new SpriteLocation { Sector = 0x0000E1E3, Size = 0x0000A800 }, // GANDO
            new SpriteLocation { Sector = 0x0000E7AC, Size = 0x0000B800 }, // SIMON
            new SpriteLocation { Sector = 0x0000E24F, Size = 0x0000B000 }, // GYUMU
            new SpriteLocation { Sector = 0x0000E634, Size = 0x0000B000 }, // ORAN
            new SpriteLocation { Sector = 0x0000E1F8, Size = 0x0000B000 }, // GARU
            new SpriteLocation { Sector = 0x0000E265, Size = 0x0000B000 }, // H61
            new SpriteLocation { Sector = 0x0000E134, Size = 0x0000B000 }, // DORA
            new SpriteLocation { Sector = 0x0000E660, Size = 0x0000B000 }, // RAFA
            new SpriteLocation { Sector = 0x0000E4E8, Size = 0x0000A800 }, // MARA
            new SpriteLocation { Sector = 0x0000E178, Size = 0x0000B000 }, // ERU
            new SpriteLocation { Sector = 0x0000E18E, Size = 0x00009800 }, // FURAIA
            new SpriteLocation { Sector = 0x0000DFE7, Size = 0x0000B000 }, // BARITEN
            new SpriteLocation { Sector = 0x0000E3F4, Size = 0x0000B000 }, // KANBA
            new SpriteLocation { Sector = 0x0000E040, Size = 0x0000B000 }, // BEIO
            new SpriteLocation { Sector = 0x0000E925, Size = 0x0000B000 }, // WIGU
            new SpriteLocation { Sector = 0x0000DFFD, Size = 0x0000B000 }, // BARU
            new SpriteLocation { Sector = 0x0000E5B0, Size = 0x0000B000 }, // MUSU
            new SpriteLocation { Sector = 0x0000E6FC, Size = 0x0000B000 }, // RUDO
            new SpriteLocation { Sector = 0x0000E8E3, Size = 0x0000B000 }, // VORU
            new SpriteLocation { Sector = 0x0000E27B, Size = 0x0000B000 }, // H75
            new SpriteLocation { Sector = 0x0000E291, Size = 0x0000B000 }, // H76
            new SpriteLocation { Sector = 0x0000E2A7, Size = 0x0000B000 }, // H77
            new SpriteLocation { Sector = 0x0000E2BD, Size = 0x0000B000 }, // H78
            new SpriteLocation { Sector = 0x0000E2D3, Size = 0x0000B000 }, // H79
            new SpriteLocation { Sector = 0x0000E2E9, Size = 0x0000B000 }, // H80
            new SpriteLocation { Sector = 0x0000E2FF, Size = 0x0000B000 }, // H81
            new SpriteLocation { Sector = 0x0000E315, Size = 0x0000B000 }, // H82
            new SpriteLocation { Sector = 0x0000E32B, Size = 0x0000B000 }, // H83
            new SpriteLocation { Sector = 0x0000DFA3, Size = 0x0000B000 }, // ARUFU
            new SpriteLocation { Sector = 0x0000E341, Size = 0x0000B000 }, // H85
            new SpriteLocation { Sector = 0x0000DFB9, Size = 0x0000B000 }, // ARUMA
            new SpriteLocation { Sector = 0x0000DF62, Size = 0x0000B000 }, // AJORA
            new SpriteLocation { Sector = 0x0000E081, Size = 0x0000A800 }, // CLOUD
            new SpriteLocation { Sector = 0x0000E992, Size = 0x0000B000 }, // ZARU2
            new SpriteLocation { Sector = 0x0000DF4C, Size = 0x0000B000 }, // AGURI
            new SpriteLocation { Sector = 0x0000E3DE, Size = 0x0000B000 }, // ITEM_W
            new SpriteLocation { Sector = 0x0000E7D9, Size = 0x0000C000 }, // SIRO_W
            new SpriteLocation { Sector = 0x0000E48D, Size = 0x0000B800 }, // KURO_M
            new SpriteLocation { Sector = 0x0000E608, Size = 0x0000B000 }, // ONMYO_M
            new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
            new SpriteLocation { Sector = 0x0000E796, Size = 0x0000B000 }, // SERIA
            new SpriteLocation { Sector = 0x0000E4D2, Size = 0x0000B000 }, // LEDY
            new SpriteLocation { Sector = 0x0000E8CC, Size = 0x0000B800 }, // VERI
            new SpriteLocation { Sector = 0x0000E461, Size = 0x0000B000 }, // KNIGHT_M
            new SpriteLocation { Sector = 0x0000E9A8, Size = 0x0000B800 }, // ZARUE
            new SpriteLocation { Sector = 0x0000E93B, Size = 0x0000B000 }, // YUMI_M
            new SpriteLocation { Sector = 0x0000E357, Size = 0x0000C000 }, // HASYU

            new SpriteLocation { Sector = 0x0000DFCF, Size = 0x0000C000 }, // ARUTE
            new SpriteLocation { Sector = 0x0000E48D, Size = 0x0000B800 }, // KURO_M
            new SpriteLocation { Sector = 0x0000E4BA, Size = 0x0000C000 }, // KYUKU
            new SpriteLocation { Sector = 0x0000E88C, Size = 0x0000B000 }, // TOKI_W
            new SpriteLocation { Sector = 0x0000DF35, Size = 0x0000B800 }, // ADORA
            new SpriteLocation { Sector = 0x0000E608, Size = 0x0000B000 }, // ONMYO_M
            new SpriteLocation { Sector = 0x0000E81D, Size = 0x0000B000 }, // SYOU_W
            new SpriteLocation { Sector = 0x0000E6E5, Size = 0x0000B800 }, // REZE_D
            new SpriteLocation { Sector = 0x0000E40A, Size = 0x0000C000 }, // KANZEN
            new SpriteLocation { Sector = 0x0000E9D6, Size = 0x00009800 }, // 10M
            new SpriteLocation { Sector = 0x0000E9E9, Size = 0x00009800 }, // 10W
            new SpriteLocation { Sector = 0x0000E9FC, Size = 0x00009800 }, // 20M
            new SpriteLocation { Sector = 0x0000EA0F, Size = 0x00009800 }, // 20W
            new SpriteLocation { Sector = 0x0000EA22, Size = 0x00009800 }, // 40M
            new SpriteLocation { Sector = 0x0000EA35, Size = 0x00009800 }, // 40W
            new SpriteLocation { Sector = 0x0000EA48, Size = 0x00009800 }, // 60M
            new SpriteLocation { Sector = 0x0000EA5B, Size = 0x00009800 }, // 60W
            new SpriteLocation { Sector = 0x0000EA6E, Size = 0x00009800 }, // CYOMON1
            new SpriteLocation { Sector = 0x0000EA81, Size = 0x00009800 }, // CYOMON2
            new SpriteLocation { Sector = 0x0000EA94, Size = 0x00009800 }, // CYOMON3
            new SpriteLocation { Sector = 0x0000EAA7, Size = 0x00009800 }, // CYOMON4
            new SpriteLocation { Sector = 0x0000EABA, Size = 0x0000B800 }, // SOURYO
            new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
            new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
            new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
            new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
            new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
            new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
            new SpriteLocation { Sector = 0x0000E0C5, Size = 0x0000B000 }, // DAMI
            new SpriteLocation { Sector = 0x0000E422, Size = 0x0000A000 }, // KASANEK
            new SpriteLocation { Sector = 0x0000E436, Size = 0x0000A000 }, // KASANEM

            new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
            new SpriteLocation { Sector = 0x0000E513, Size = 0x0000B000 }, // MINA_W
            new SpriteLocation { Sector = 0x0000E3C8, Size = 0x0000B000 }, // ITEM_M
            new SpriteLocation { Sector = 0x0000E3DE, Size = 0x0000B000 }, // ITEM_W
            new SpriteLocation { Sector = 0x0000E461, Size = 0x0000B000 }, // KNIGHT_M
            new SpriteLocation { Sector = 0x0000E477, Size = 0x0000B000 }, // KNIGHT_W
            new SpriteLocation { Sector = 0x0000E93B, Size = 0x0000B000 }, // YUMI_M
            new SpriteLocation { Sector = 0x0000E951, Size = 0x0000B000 }, // YUMI_W
            new SpriteLocation { Sector = 0x0000E558, Size = 0x0000B000 }, // MONK_M
            new SpriteLocation { Sector = 0x0000E56E, Size = 0x0000B000 }, // MONK_W
            new SpriteLocation { Sector = 0x0000E7C3, Size = 0x0000B000 }, // SIRO_M
            new SpriteLocation { Sector = 0x0000E7D9, Size = 0x0000C000 }, // SIRO_W
            new SpriteLocation { Sector = 0x0000E48D, Size = 0x0000B800 }, // KURO_M
            new SpriteLocation { Sector = 0x0000E4A4, Size = 0x0000B000 }, // KURO_W
            new SpriteLocation { Sector = 0x0000E876, Size = 0x0000B000 }, // TOKI_M
            new SpriteLocation { Sector = 0x0000E88C, Size = 0x0000B000 }, // TOKI_W
            new SpriteLocation { Sector = 0x0000E806, Size = 0x0000B800 }, // SYOU_M
            new SpriteLocation { Sector = 0x0000E81D, Size = 0x0000B000 }, // SYOU_W 
            new SpriteLocation { Sector = 0x0000E84A, Size = 0x0000B000 }, // THIEF_M
            new SpriteLocation { Sector = 0x0000E860, Size = 0x0000B000 }, // THIEF_W
            new SpriteLocation { Sector = 0x0000E8F9, Size = 0x0000B000 }, // WAJU_M
            new SpriteLocation { Sector = 0x0000E90F, Size = 0x0000B000 }, // WAJU_W 
            new SpriteLocation { Sector = 0x0000E608, Size = 0x0000B000 }, // ONMYO_M
            new SpriteLocation { Sector = 0x0000E61E, Size = 0x0000B000 }, // ONMYO_W
            new SpriteLocation { Sector = 0x0000E1A1, Size = 0x0000B000 }, // FUSUI_M
            new SpriteLocation { Sector = 0x0000E1B7, Size = 0x0000B000 }, // FUSUI_W
            new SpriteLocation { Sector = 0x0000E712, Size = 0x0000B000 }, // RYU_M
            new SpriteLocation { Sector = 0x0000E728, Size = 0x0000B000 }, // RYU_W
            new SpriteLocation { Sector = 0x0000E73E, Size = 0x0000B000 }, // SAMU_M
            new SpriteLocation { Sector = 0x0000E754, Size = 0x0000B000 }, // SAMU_W
            new SpriteLocation { Sector = 0x0000E5C6, Size = 0x0000B000 }, // NINJA_M
            new SpriteLocation { Sector = 0x0000E5DC, Size = 0x0000B000 }, // NINJA_W
            new SpriteLocation { Sector = 0x0000E76A, Size = 0x0000B000 }, // SAN_M
            new SpriteLocation { Sector = 0x0000E780, Size = 0x0000B000 }, // SAN_W
            new SpriteLocation { Sector = 0x0000E20E, Size = 0x0000B000 }, // GIN_M
            new SpriteLocation { Sector = 0x0000E5F2, Size = 0x0000B000 }, // ODORI_W
            new SpriteLocation { Sector = 0x0000E584, Size = 0x0000B000 }, // MONO_M
            new SpriteLocation { Sector = 0x0000E59A, Size = 0x0000B000 }, // MONO_W
            new SpriteLocation { Sector = 0x0000E096, Size = 0x0000C800 }, // CYOKO
            new SpriteLocation { Sector = 0x0000E224, Size = 0x0000A800 }, // GOB
            new SpriteLocation { Sector = 0x0000E06C, Size = 0x0000A800 }, // BOM
            new SpriteLocation { Sector = 0x0000E39D, Size = 0x0000B000 }, // HYOU
            new SpriteLocation { Sector = 0x0000E3B3, Size = 0x0000A800 }, // IKA
            new SpriteLocation { Sector = 0x0000E7F1, Size = 0x0000A800 }, // SUKERU
            new SpriteLocation { Sector = 0x0000E967, Size = 0x0000A800 }, // YUREI
            new SpriteLocation { Sector = 0x0000DF78, Size = 0x0000A800 }, // ARLI 
            new SpriteLocation { Sector = 0x0000E8A2, Size = 0x0000B000 }, // TORI
            new SpriteLocation { Sector = 0x0000E8B8, Size = 0x0000A000 }, // URI
            new SpriteLocation { Sector = 0x0000E44A, Size = 0x0000B800 }, // KI
            new SpriteLocation { Sector = 0x0000E529, Size = 0x0000C000 }, // MINOTA
            new SpriteLocation { Sector = 0x0000E541, Size = 0x0000B800 }, // MOL
            new SpriteLocation { Sector = 0x0000E029, Size = 0x0000B800 }, // BEHI
            new SpriteLocation { Sector = 0x0000E14A, Size = 0x0000B800 }, // DORA1
            new SpriteLocation { Sector = 0x0000E161, Size = 0x0000B800 }, // DORA2
            new SpriteLocation { Sector = 0x0000E056, Size = 0x0000B000 }, // BIBUROS
            new SpriteLocation { Sector = 0x0000E36F, Size = 0x0000C000 }, // HEBI
            new SpriteLocation { Sector = 0x0000E14A, Size = 0x0000B800 }, // DORA1
            new SpriteLocation { Sector = 0x0000E0DB, Size = 0x0000B800 }, // DEMON
            new SpriteLocation { Sector = 0x0000E833, Size = 0x0000B800 } // TETSU
            }.AsReadOnly();
    }
}
