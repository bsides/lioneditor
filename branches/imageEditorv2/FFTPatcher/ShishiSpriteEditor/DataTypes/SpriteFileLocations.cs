﻿using System;
using System.Collections.Generic;
using System.Text;
using PatcherLib.Utilities;
using PatcherLib.Datatypes;
using System.IO;
using PatcherLib.Iso;

namespace FFTPatcher.SpriteEditor
{
    
    internal class SpriteLocation
    {
        public UInt32 Sector { get; set; }
        public UInt32 Size { get; set; }

        private SpriteLocation(PsxIso.KnownPosition pos, IList<byte> bytes)
        {
            System.Diagnostics.Debug.Assert(bytes.Count == 8);
            Sector = bytes.Sub(0, 3).ToUInt32();
            Size = bytes.Sub(4).ToUInt32();
            psxPos = pos;
        }

        private PsxIso.KnownPosition psxPos = null;

        public static SpriteLocation BuildPsx(PsxIso.KnownPosition pos, IList<byte> bytes)
        {
            return new SpriteLocation(pos, bytes);
        }
    }

    internal class SpriteFileLocations
    {
        private static PatcherLib.Iso.PsxIso.KnownPosition spriteLocationsPosition =
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, 0x2DCDC, numSprites * 8);
        private static PatcherLib.Iso.PsxIso.KnownPosition SP2LocationsPosition =
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, 0x2E60C, numSp2 * 8);

        public static PatcherLib.Iso.PsxIso.KnownPosition SpriteLocationsPosition
        {
            get
            {
                return new PsxIso.KnownPosition(spriteLocationsPosition.Sector, spriteLocationsPosition.StartLocation, spriteLocationsPosition.Length);
            }
        }

        public static bool IsoHasDefaultSpriteLocations(Stream iso)
        {
            return PatcherLib.Utilities.Utilities.CompareArrays( 
                PatcherLib.Iso.PsxIso.ReadFile( iso, spriteLocationsPosition ), 
                defaultSpriteFileLocationsBytes );   
        }

        public static bool IsoHasPatchedSpriteLocations( Stream iso )
        {
            return PatcherLib.Utilities.Utilities.CompareArrays(
                PatcherLib.Iso.PsxIso.ReadFile( iso, spriteLocationsPosition ),
                patchedSpriteFileLocations );
        }

        const int numSprites = 154;
        const int numSp2 = 0xD8 / 8;

        private IList<SpriteLocation> sprites;
        private IList<SpriteLocation> sp2;

        private SpriteFileLocations()
        {
        }

        public IList<byte> DefaultSpriteFileLocationsBytes { get { return defaultSpriteFileLocationsBytes; } }
        public static SpriteFileLocations FromPsxIso(Stream iso)
        {
            SpriteFileLocations result = new SpriteFileLocations();

            byte[] spriteBytes = PatcherLib.Iso.PsxIso.ReadFile(iso, spriteLocationsPosition);
            byte[] sp2Bytes = PatcherLib.Iso.PsxIso.ReadFile(iso, SP2LocationsPosition);

            IList<SpriteLocation> sprites = new SpriteLocation[numSprites];
            for (int i = 0; i < numSprites; i++)
            {
                sprites[i] = SpriteLocation.BuildPsx(
                    new PsxIso.KnownPosition(spriteLocationsPosition.Sector, spriteLocationsPosition.StartLocation + i * 8, 8), 
                    spriteBytes.Sub(i * 8, (i + 1) * 8 - 1));
            }
            result.sprites = sprites;

            IList<SpriteLocation> sp2 = new SpriteLocation[numSp2];
            for (int i = 0; i < numSp2; i++)
            {
                sp2[i] = SpriteLocation.BuildPsx(
                    new PsxIso.KnownPosition(SP2LocationsPosition.Sector, SP2LocationsPosition.StartLocation, 8),
                    sp2Bytes.Sub(i * 8, (i + 1) * 8 - 1));
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
            List<byte> result = new List<byte>(spriteLocationsPosition.Length);
            foreach (SpriteLocation s in sprites)
            {
                result.AddRange(s.Sector.ToBytes());
                result.AddRange(s.Size.ToBytes());
            }
            return spriteLocationsPosition.GetPatchedByteArray(result.ToArray());
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

        //// This data stored at offset 0x2E60C in BATTLE.BIN
        //private static IList<SpriteLocation> DefaultSp2Locations = new SpriteLocation[numSp2] {
        //    new SpriteLocation { Sector = 0x0000EB01, Size = 0x00008000 }, // BOM2
        //    new SpriteLocation { Sector = 0x0000EB31, Size = 0x00008000 }, // HYOU2
        //    new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // blank
        //    new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // blank
        //    new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // blank
        //    new SpriteLocation { Sector = 0x0000EAD1, Size = 0x00008000 }, // ARLI2
        //    new SpriteLocation { Sector = 0x0000EBA1, Size = 0x00008000 }, // TORI2
        //    new SpriteLocation { Sector = 0x0000EBB1, Size = 0x00008000 }, // URI2
        //    new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
        //    new SpriteLocation { Sector = 0x0000EB81, Size = 0x00008000 }, // MINOTA2
        //    new SpriteLocation { Sector = 0x0000EB91, Size = 0x00008000 }, // MOL2
        //    new SpriteLocation { Sector = 0x0000EAE1, Size = 0x00008000 }, // BEHI2
        //    new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
        //    new SpriteLocation { Sector = 0x0000EB21, Size = 0x00008000 }, // DORA22
        //    new SpriteLocation { Sector = 0x0000EAF1, Size = 0x00008000 }, // BIBU2
        //    new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
        //    new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
        //    new SpriteLocation { Sector = 0x0000EB11, Size = 0x00008000 }, // DEMON2
        //    new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
        //    new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // BLank
        //    new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
        //    new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
        //    new SpriteLocation { Sector = 0x00000000, Size = 0x00000000 }, // Blank
        //    new SpriteLocation { Sector = 0x0000EB71, Size = 0x00008000 }, // IRON5
        //    new SpriteLocation { Sector = 0x0000EB61, Size = 0x00008000 }, // IRON4
        //    new SpriteLocation { Sector = 0x0000EB41, Size = 0x00008000 }, // IRON2
        //    new SpriteLocation { Sector = 0x0000EB51, Size = 0x00008000 } // IRON3
        //    }.AsReadOnly();

        //// This data stored at offset 0x2DCDC in BATTLE.BIN
        //private static IList<SpriteLocation> DefaultSpriteLocations = new SpriteLocation[numSprites] {
        //    new SpriteLocation { Sector = 0x0000E68D, Size = 0x0000B000 }, // RAMUZA
        //    new SpriteLocation { Sector = 0x0000E6A3, Size = 0x0000B000 }, // RAMUZA2
        //    new SpriteLocation { Sector = 0x0000E6B9, Size = 0x0000B000 }, // RAMUZA3
        //    new SpriteLocation { Sector = 0x0000E0F2, Size = 0x0000B000 }, // DILY
        //    new SpriteLocation { Sector = 0x0000E108, Size = 0x0000B000 }, // DILY2
        //    new SpriteLocation { Sector = 0x0000E11E, Size = 0x0000B000 }, // DILY3
        //    new SpriteLocation { Sector = 0x0000DF8D, Size = 0x0000B000 }, // ARU
        //    new SpriteLocation { Sector = 0x0000E97C, Size = 0x0000B000 }, // ZARU
        //    new SpriteLocation { Sector = 0x0000E0AF, Size = 0x0000B000 }, // DAISU
        //    new SpriteLocation { Sector = 0x0000E676, Size = 0x0000B800 }, // RAGU
        //    new SpriteLocation { Sector = 0x0000E239, Size = 0x0000B000 }, // GORU
        //    new SpriteLocation { Sector = 0x0000E387, Size = 0x0000B000 }, // HIME
        //    new SpriteLocation { Sector = 0x0000E64A, Size = 0x0000B000 }, // ORU
        //    new SpriteLocation { Sector = 0x0000E1CD, Size = 0x0000B000 }, // FYUNE
        //    new SpriteLocation { Sector = 0x0000E6CF, Size = 0x0000B000 }, // REZE
        //    new SpriteLocation { Sector = 0x0000E9BF, Size = 0x0000B800 }, // ZARUMOU
        //    new SpriteLocation { Sector = 0x0000E013, Size = 0x0000B000 }, // BARUNA
        //    new SpriteLocation { Sector = 0x0000E1E3, Size = 0x0000A800 }, // GANDO
        //    new SpriteLocation { Sector = 0x0000E7AC, Size = 0x0000B800 }, // SIMON
        //    new SpriteLocation { Sector = 0x0000E24F, Size = 0x0000B000 }, // GYUMU
        //    new SpriteLocation { Sector = 0x0000E634, Size = 0x0000B000 }, // ORAN
        //    new SpriteLocation { Sector = 0x0000E1F8, Size = 0x0000B000 }, // GARU
        //    new SpriteLocation { Sector = 0x0000E265, Size = 0x0000B000 }, // H61
        //    new SpriteLocation { Sector = 0x0000E134, Size = 0x0000B000 }, // DORA
        //    new SpriteLocation { Sector = 0x0000E660, Size = 0x0000B000 }, // RAFA
        //    new SpriteLocation { Sector = 0x0000E4E8, Size = 0x0000A800 }, // MARA
        //    new SpriteLocation { Sector = 0x0000E178, Size = 0x0000B000 }, // ERU
        //    new SpriteLocation { Sector = 0x0000E18E, Size = 0x00009800 }, // FURAIA
        //    new SpriteLocation { Sector = 0x0000DFE7, Size = 0x0000B000 }, // BARITEN
        //    new SpriteLocation { Sector = 0x0000E3F4, Size = 0x0000B000 }, // KANBA
        //    new SpriteLocation { Sector = 0x0000E040, Size = 0x0000B000 }, // BEIO
        //    new SpriteLocation { Sector = 0x0000E925, Size = 0x0000B000 }, // WIGU
        //    new SpriteLocation { Sector = 0x0000DFFD, Size = 0x0000B000 }, // BARU
        //    new SpriteLocation { Sector = 0x0000E5B0, Size = 0x0000B000 }, // MUSU
        //    new SpriteLocation { Sector = 0x0000E6FC, Size = 0x0000B000 }, // RUDO
        //    new SpriteLocation { Sector = 0x0000E8E3, Size = 0x0000B000 }, // VORU
        //    new SpriteLocation { Sector = 0x0000E27B, Size = 0x0000B000 }, // H75
        //    new SpriteLocation { Sector = 0x0000E291, Size = 0x0000B000 }, // H76
        //    new SpriteLocation { Sector = 0x0000E2A7, Size = 0x0000B000 }, // H77
        //    new SpriteLocation { Sector = 0x0000E2BD, Size = 0x0000B000 }, // H78
        //    new SpriteLocation { Sector = 0x0000E2D3, Size = 0x0000B000 }, // H79
        //    new SpriteLocation { Sector = 0x0000E2E9, Size = 0x0000B000 }, // H80
        //    new SpriteLocation { Sector = 0x0000E2FF, Size = 0x0000B000 }, // H81
        //    new SpriteLocation { Sector = 0x0000E315, Size = 0x0000B000 }, // H82
        //    new SpriteLocation { Sector = 0x0000E32B, Size = 0x0000B000 }, // H83
        //    new SpriteLocation { Sector = 0x0000DFA3, Size = 0x0000B000 }, // ARUFU
        //    new SpriteLocation { Sector = 0x0000E341, Size = 0x0000B000 }, // H85
        //    new SpriteLocation { Sector = 0x0000DFB9, Size = 0x0000B000 }, // ARUMA
        //    new SpriteLocation { Sector = 0x0000DF62, Size = 0x0000B000 }, // AJORA
        //    new SpriteLocation { Sector = 0x0000E081, Size = 0x0000A800 }, // CLOUD
        //    new SpriteLocation { Sector = 0x0000E992, Size = 0x0000B000 }, // ZARU2
        //    new SpriteLocation { Sector = 0x0000DF4C, Size = 0x0000B000 }, // AGURI
        //    new SpriteLocation { Sector = 0x0000E3DE, Size = 0x0000B000 }, // ITEM_W
        //    new SpriteLocation { Sector = 0x0000E7D9, Size = 0x0000C000 }, // SIRO_W
        //    new SpriteLocation { Sector = 0x0000E48D, Size = 0x0000B800 }, // KURO_M
        //    new SpriteLocation { Sector = 0x0000E608, Size = 0x0000B000 }, // ONMYO_M
        //    new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
        //    new SpriteLocation { Sector = 0x0000E796, Size = 0x0000B000 }, // SERIA
        //    new SpriteLocation { Sector = 0x0000E4D2, Size = 0x0000B000 }, // LEDY
        //    new SpriteLocation { Sector = 0x0000E8CC, Size = 0x0000B800 }, // VERI
        //    new SpriteLocation { Sector = 0x0000E461, Size = 0x0000B000 }, // KNIGHT_M
        //    new SpriteLocation { Sector = 0x0000E9A8, Size = 0x0000B800 }, // ZARUE
        //    new SpriteLocation { Sector = 0x0000E93B, Size = 0x0000B000 }, // YUMI_M
        //    new SpriteLocation { Sector = 0x0000E357, Size = 0x0000C000 }, // HASYU

        //    new SpriteLocation { Sector = 0x0000DFCF, Size = 0x0000C000 }, // ARUTE
        //    new SpriteLocation { Sector = 0x0000E48D, Size = 0x0000B800 }, // KURO_M
        //    new SpriteLocation { Sector = 0x0000E4BA, Size = 0x0000C000 }, // KYUKU
        //    new SpriteLocation { Sector = 0x0000E88C, Size = 0x0000B000 }, // TOKI_W
        //    new SpriteLocation { Sector = 0x0000DF35, Size = 0x0000B800 }, // ADORA
        //    new SpriteLocation { Sector = 0x0000E608, Size = 0x0000B000 }, // ONMYO_M
        //    new SpriteLocation { Sector = 0x0000E81D, Size = 0x0000B000 }, // SYOU_W
        //    new SpriteLocation { Sector = 0x0000E6E5, Size = 0x0000B800 }, // REZE_D
        //    new SpriteLocation { Sector = 0x0000E40A, Size = 0x0000C000 }, // KANZEN
        //    new SpriteLocation { Sector = 0x0000E9D6, Size = 0x00009800 }, // 10M
        //    new SpriteLocation { Sector = 0x0000E9E9, Size = 0x00009800 }, // 10W
        //    new SpriteLocation { Sector = 0x0000E9FC, Size = 0x00009800 }, // 20M
        //    new SpriteLocation { Sector = 0x0000EA0F, Size = 0x00009800 }, // 20W
        //    new SpriteLocation { Sector = 0x0000EA22, Size = 0x00009800 }, // 40M
        //    new SpriteLocation { Sector = 0x0000EA35, Size = 0x00009800 }, // 40W
        //    new SpriteLocation { Sector = 0x0000EA48, Size = 0x00009800 }, // 60M
        //    new SpriteLocation { Sector = 0x0000EA5B, Size = 0x00009800 }, // 60W
        //    new SpriteLocation { Sector = 0x0000EA6E, Size = 0x00009800 }, // CYOMON1
        //    new SpriteLocation { Sector = 0x0000EA81, Size = 0x00009800 }, // CYOMON2
        //    new SpriteLocation { Sector = 0x0000EA94, Size = 0x00009800 }, // CYOMON3
        //    new SpriteLocation { Sector = 0x0000EAA7, Size = 0x00009800 }, // CYOMON4
        //    new SpriteLocation { Sector = 0x0000EABA, Size = 0x0000B800 }, // SOURYO
        //    new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
        //    new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
        //    new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
        //    new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
        //    new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
        //    new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
        //    new SpriteLocation { Sector = 0x0000E0C5, Size = 0x0000B000 }, // DAMI
        //    new SpriteLocation { Sector = 0x0000E422, Size = 0x0000A000 }, // KASANEK
        //    new SpriteLocation { Sector = 0x0000E436, Size = 0x0000A000 }, // KASANEM

        //    new SpriteLocation { Sector = 0x0000E4FD, Size = 0x0000B000 }, // MINA_M
        //    new SpriteLocation { Sector = 0x0000E513, Size = 0x0000B000 }, // MINA_W
        //    new SpriteLocation { Sector = 0x0000E3C8, Size = 0x0000B000 }, // ITEM_M
        //    new SpriteLocation { Sector = 0x0000E3DE, Size = 0x0000B000 }, // ITEM_W
        //    new SpriteLocation { Sector = 0x0000E461, Size = 0x0000B000 }, // KNIGHT_M
        //    new SpriteLocation { Sector = 0x0000E477, Size = 0x0000B000 }, // KNIGHT_W
        //    new SpriteLocation { Sector = 0x0000E93B, Size = 0x0000B000 }, // YUMI_M
        //    new SpriteLocation { Sector = 0x0000E951, Size = 0x0000B000 }, // YUMI_W
        //    new SpriteLocation { Sector = 0x0000E558, Size = 0x0000B000 }, // MONK_M
        //    new SpriteLocation { Sector = 0x0000E56E, Size = 0x0000B000 }, // MONK_W
        //    new SpriteLocation { Sector = 0x0000E7C3, Size = 0x0000B000 }, // SIRO_M
        //    new SpriteLocation { Sector = 0x0000E7D9, Size = 0x0000C000 }, // SIRO_W
        //    new SpriteLocation { Sector = 0x0000E48D, Size = 0x0000B800 }, // KURO_M
        //    new SpriteLocation { Sector = 0x0000E4A4, Size = 0x0000B000 }, // KURO_W
        //    new SpriteLocation { Sector = 0x0000E876, Size = 0x0000B000 }, // TOKI_M
        //    new SpriteLocation { Sector = 0x0000E88C, Size = 0x0000B000 }, // TOKI_W
        //    new SpriteLocation { Sector = 0x0000E806, Size = 0x0000B800 }, // SYOU_M
        //    new SpriteLocation { Sector = 0x0000E81D, Size = 0x0000B000 }, // SYOU_W 
        //    new SpriteLocation { Sector = 0x0000E84A, Size = 0x0000B000 }, // THIEF_M
        //    new SpriteLocation { Sector = 0x0000E860, Size = 0x0000B000 }, // THIEF_W
        //    new SpriteLocation { Sector = 0x0000E8F9, Size = 0x0000B000 }, // WAJU_M
        //    new SpriteLocation { Sector = 0x0000E90F, Size = 0x0000B000 }, // WAJU_W 
        //    new SpriteLocation { Sector = 0x0000E608, Size = 0x0000B000 }, // ONMYO_M
        //    new SpriteLocation { Sector = 0x0000E61E, Size = 0x0000B000 }, // ONMYO_W
        //    new SpriteLocation { Sector = 0x0000E1A1, Size = 0x0000B000 }, // FUSUI_M
        //    new SpriteLocation { Sector = 0x0000E1B7, Size = 0x0000B000 }, // FUSUI_W
        //    new SpriteLocation { Sector = 0x0000E712, Size = 0x0000B000 }, // RYU_M
        //    new SpriteLocation { Sector = 0x0000E728, Size = 0x0000B000 }, // RYU_W
        //    new SpriteLocation { Sector = 0x0000E73E, Size = 0x0000B000 }, // SAMU_M
        //    new SpriteLocation { Sector = 0x0000E754, Size = 0x0000B000 }, // SAMU_W
        //    new SpriteLocation { Sector = 0x0000E5C6, Size = 0x0000B000 }, // NINJA_M
        //    new SpriteLocation { Sector = 0x0000E5DC, Size = 0x0000B000 }, // NINJA_W
        //    new SpriteLocation { Sector = 0x0000E76A, Size = 0x0000B000 }, // SAN_M
        //    new SpriteLocation { Sector = 0x0000E780, Size = 0x0000B000 }, // SAN_W
        //    new SpriteLocation { Sector = 0x0000E20E, Size = 0x0000B000 }, // GIN_M
        //    new SpriteLocation { Sector = 0x0000E5F2, Size = 0x0000B000 }, // ODORI_W
        //    new SpriteLocation { Sector = 0x0000E584, Size = 0x0000B000 }, // MONO_M
        //    new SpriteLocation { Sector = 0x0000E59A, Size = 0x0000B000 }, // MONO_W
        //    new SpriteLocation { Sector = 0x0000E096, Size = 0x0000C800 }, // CYOKO
        //    new SpriteLocation { Sector = 0x0000E224, Size = 0x0000A800 }, // GOB
        //    new SpriteLocation { Sector = 0x0000E06C, Size = 0x0000A800 }, // BOM
        //    new SpriteLocation { Sector = 0x0000E39D, Size = 0x0000B000 }, // HYOU
        //    new SpriteLocation { Sector = 0x0000E3B3, Size = 0x0000A800 }, // IKA
        //    new SpriteLocation { Sector = 0x0000E7F1, Size = 0x0000A800 }, // SUKERU
        //    new SpriteLocation { Sector = 0x0000E967, Size = 0x0000A800 }, // YUREI
        //    new SpriteLocation { Sector = 0x0000DF78, Size = 0x0000A800 }, // ARLI 
        //    new SpriteLocation { Sector = 0x0000E8A2, Size = 0x0000B000 }, // TORI
        //    new SpriteLocation { Sector = 0x0000E8B8, Size = 0x0000A000 }, // URI
        //    new SpriteLocation { Sector = 0x0000E44A, Size = 0x0000B800 }, // KI
        //    new SpriteLocation { Sector = 0x0000E529, Size = 0x0000C000 }, // MINOTA
        //    new SpriteLocation { Sector = 0x0000E541, Size = 0x0000B800 }, // MOL
        //    new SpriteLocation { Sector = 0x0000E029, Size = 0x0000B800 }, // BEHI
        //    new SpriteLocation { Sector = 0x0000E14A, Size = 0x0000B800 }, // DORA1
        //    new SpriteLocation { Sector = 0x0000E161, Size = 0x0000B800 }, // DORA2
        //    new SpriteLocation { Sector = 0x0000E056, Size = 0x0000B000 }, // BIBUROS
        //    new SpriteLocation { Sector = 0x0000E36F, Size = 0x0000C000 }, // HEBI
        //    new SpriteLocation { Sector = 0x0000E14A, Size = 0x0000B800 }, // DORA1
        //    new SpriteLocation { Sector = 0x0000E0DB, Size = 0x0000B800 }, // DEMON
        //    new SpriteLocation { Sector = 0x0000E833, Size = 0x0000B800 } // TETSU
        //    }.AsReadOnly();

        private static IList<byte> defaultSpriteFileLocationsBytes = new byte[numSprites*8]
        {
            0x8D, 0xE6, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xA3, 0xE6, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xB9, 0xE6, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xF2, 0xE0, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x08, 0xE1, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x1E, 0xE1, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x8D, 0xDF, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x7C, 0xE9, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xAF, 0xE0, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x76, 0xE6, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0x39, 0xE2, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x87, 0xE3, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x4A, 0xE6, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xCD, 0xE1, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xCF, 0xE6, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xBF, 0xE9, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0x13, 0xE0, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xE3, 0xE1, 0x00, 0x00, 0x00, 0xA8, 0x00, 0x00, 
            0xAC, 0xE7, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 0x4F, 0xE2, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x34, 0xE6, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xF8, 0xE1, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x65, 0xE2, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x34, 0xE1, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x60, 0xE6, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xE8, 0xE4, 0x00, 0x00, 0x00, 0xA8, 0x00, 0x00, 
            0x78, 0xE1, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x8E, 0xE1, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 
            0xE7, 0xDF, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xF4, 0xE3, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x40, 0xE0, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x25, 0xE9, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xFD, 0xDF, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xB0, 0xE5, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xFC, 0xE6, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xE3, 0xE8, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x7B, 0xE2, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x91, 0xE2, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xA7, 0xE2, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xBD, 0xE2, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xD3, 0xE2, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xE9, 0xE2, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xFF, 0xE2, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x15, 0xE3, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x2B, 0xE3, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xA3, 0xDF, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x41, 0xE3, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xB9, 0xDF, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x62, 0xDF, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x81, 0xE0, 0x00, 0x00, 0x00, 0xA8, 0x00, 0x00, 
            0x92, 0xE9, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x4C, 0xDF, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xDE, 0xE3, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xD9, 0xE7, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 
            0x8D, 0xE4, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 0x08, 0xE6, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xFD, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x96, 0xE7, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xD2, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xCC, 0xE8, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0x61, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xA8, 0xE9, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0x3B, 0xE9, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x57, 0xE3, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 
            0xCF, 0xDF, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 0x8D, 0xE4, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0xBA, 0xE4, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 0x8C, 0xE8, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x35, 0xDF, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 0x08, 0xE6, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x1D, 0xE8, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xE5, 0xE6, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0x0A, 0xE4, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 0xD6, 0xE9, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 
            0xE9, 0xE9, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 0xFC, 0xE9, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 
            0x0F, 0xEA, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 0x22, 0xEA, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 
            0x35, 0xEA, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 0x48, 0xEA, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 
            0x5B, 0xEA, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 0x6E, 0xEA, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 
            0x81, 0xEA, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 0x94, 0xEA, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 
            0xA7, 0xEA, 0x00, 0x00, 0x00, 0x98, 0x00, 0x00, 0xBA, 0xEA, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0xFD, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xFD, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xFD, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xFD, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xFD, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xFD, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xC5, 0xE0, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x22, 0xE4, 0x00, 0x00, 0x00, 0xA0, 0x00, 0x00, 
            0x36, 0xE4, 0x00, 0x00, 0x00, 0xA0, 0x00, 0x00, 0xFD, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x13, 0xE5, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xC8, 0xE3, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xDE, 0xE3, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x61, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x77, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x3B, 0xE9, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x51, 0xE9, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x58, 0xE5, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x6E, 0xE5, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xC3, 0xE7, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xD9, 0xE7, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 0x8D, 0xE4, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0xA4, 0xE4, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x76, 0xE8, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x8C, 0xE8, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x06, 0xE8, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0x1D, 0xE8, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x4A, 0xE8, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x60, 0xE8, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xF9, 0xE8, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x0F, 0xE9, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x08, 0xE6, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x1E, 0xE6, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xA1, 0xE1, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xB7, 0xE1, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x12, 0xE7, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x28, 0xE7, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x3E, 0xE7, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x54, 0xE7, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xC6, 0xE5, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xDC, 0xE5, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x6A, 0xE7, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x80, 0xE7, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x0E, 0xE2, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xF2, 0xE5, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x84, 0xE5, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x9A, 0xE5, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x96, 0xE0, 0x00, 0x00, 0x00, 0xC8, 0x00, 0x00, 
            0x24, 0xE2, 0x00, 0x00, 0x00, 0xA8, 0x00, 0x00, 0x6C, 0xE0, 0x00, 0x00, 0x00, 0xA8, 0x00, 0x00, 
            0x9D, 0xE3, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xB3, 0xE3, 0x00, 0x00, 0x00, 0xA8, 0x00, 0x00, 
            0xF1, 0xE7, 0x00, 0x00, 0x00, 0xA8, 0x00, 0x00, 0x67, 0xE9, 0x00, 0x00, 0x00, 0xA8, 0x00, 0x00, 
            0x78, 0xDF, 0x00, 0x00, 0x00, 0xA8, 0x00, 0x00, 0xA2, 0xE8, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0xB8, 0xE8, 0x00, 0x00, 0x00, 0xA0, 0x00, 0x00, 0x4A, 0xE4, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0x29, 0xE5, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 0x41, 0xE5, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0x29, 0xE0, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 0x4A, 0xE1, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0x61, 0xE1, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 0x56, 0xE0, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 
            0x6F, 0xE3, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 0x4A, 0xE1, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 
            0xDB, 0xE0, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00, 0x33, 0xE8, 0x00, 0x00, 0x00, 0xB8, 0x00, 0x00
        }.AsReadOnly();
        private static IList<byte> patchedSpriteFileLocations = new byte[numSprites * 8] {
            0xB0,0x82,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x82,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xF0,0x82,0x03,0x00,0x00,0xB0,0x00,0x00,0x10,0x83,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x30,0x83,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x83,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x83,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x83,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xB0,0x83,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x83,0x03,0x00,0x00,0xB8,0x00,0x00,
            0xF0,0x83,0x03,0x00,0x00,0xB0,0x00,0x00,0x10,0x84,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x30,0x84,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x84,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x84,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x84,0x03,0x00,0x00,0xB8,0x00,0x00,
            0xB0,0x84,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x84,0x03,0x00,0x00,0xA8,0x00,0x00,
            0xF0,0x84,0x03,0x00,0x00,0xB8,0x00,0x00,0x10,0x85,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x30,0x85,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x85,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x85,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x85,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xB0,0x85,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x85,0x03,0x00,0x00,0xA8,0x00,0x00,
            0xF0,0x85,0x03,0x00,0x00,0xB0,0x00,0x00,0x10,0x86,0x03,0x00,0x00,0x98,0x00,0x00,
            0x30,0x86,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x86,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x86,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x86,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xB0,0x86,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x86,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xF0,0x86,0x03,0x00,0x00,0xB0,0x00,0x00,0x10,0x87,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x30,0x87,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x87,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x87,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x87,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xB0,0x87,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x87,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xF0,0x87,0x03,0x00,0x00,0xB0,0x00,0x00,0x10,0x88,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x30,0x88,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x88,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x88,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x88,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xB0,0x88,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x88,0x03,0x00,0x00,0xA8,0x00,0x00,
            0xF0,0x88,0x03,0x00,0x00,0xB0,0x00,0x00,0x10,0x89,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x30,0x89,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x89,0x03,0x00,0x00,0xC0,0x00,0x00,
            0x70,0x89,0x03,0x00,0x00,0xB8,0x00,0x00,0x90,0x89,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xB0,0x89,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x89,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xF0,0x89,0x03,0x00,0x00,0xB0,0x00,0x00,0x10,0x8A,0x03,0x00,0x00,0xB8,0x00,0x00,
            0x30,0x8A,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x8A,0x03,0x00,0x00,0xB8,0x00,0x00,
            0x70,0x8A,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x8A,0x03,0x00,0x00,0xC0,0x00,0x00,
            0xB0,0x8A,0x03,0x00,0x00,0xC0,0x00,0x00,0xD0,0x8A,0x03,0x00,0x00,0xB8,0x00,0x00,
            0xF0,0x8A,0x03,0x00,0x00,0xC0,0x00,0x00,0x10,0x8B,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x30,0x8B,0x03,0x00,0x00,0xB8,0x00,0x00,0x50,0x8B,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x8B,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x8B,0x03,0x00,0x00,0xB8,0x00,0x00,
            0xB0,0x8B,0x03,0x00,0x00,0xC0,0x00,0x00,0xD0,0x8B,0x03,0x00,0x00,0x98,0x00,0x00,
            0xF0,0x8B,0x03,0x00,0x00,0x98,0x00,0x00,0x10,0x8C,0x03,0x00,0x00,0x98,0x00,0x00,
            0x30,0x8C,0x03,0x00,0x00,0x98,0x00,0x00,0x50,0x8C,0x03,0x00,0x00,0x98,0x00,0x00,
            0x70,0x8C,0x03,0x00,0x00,0x98,0x00,0x00,0x90,0x8C,0x03,0x00,0x00,0x98,0x00,0x00,
            0xB0,0x8C,0x03,0x00,0x00,0x98,0x00,0x00,0xD0,0x8C,0x03,0x00,0x00,0x98,0x00,0x00,
            0xF0,0x8C,0x03,0x00,0x00,0x98,0x00,0x00,0x10,0x8D,0x03,0x00,0x00,0x98,0x00,0x00,
            0x30,0x8D,0x03,0x00,0x00,0x98,0x00,0x00,0x50,0x8D,0x03,0x00,0x00,0xB8,0x00,0x00,
            0x70,0x8D,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x8D,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xB0,0x8D,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x8D,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xF0,0x8D,0x03,0x00,0x00,0xB0,0x00,0x00,0x10,0x8E,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x30,0x8E,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x8E,0x03,0x00,0x00,0xA0,0x00,0x00,
            0x70,0x8E,0x03,0x00,0x00,0xA0,0x00,0x00,0x90,0x8E,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xB0,0x8E,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x8E,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xF0,0x8E,0x03,0x00,0x00,0xB0,0x00,0x00,0x10,0x8F,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x30,0x8F,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x8F,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x8F,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x8F,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xB0,0x8F,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x8F,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xF0,0x8F,0x03,0x00,0x00,0xC0,0x00,0x00,0x10,0x90,0x03,0x00,0x00,0xB8,0x00,0x00,
            0x30,0x90,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x90,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x90,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x90,0x03,0x00,0x00,0xB8,0x00,0x00,
            0xB0,0x90,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x90,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xF0,0x90,0x03,0x00,0x00,0xB0,0x00,0x00,0x10,0x91,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x30,0x91,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x91,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x91,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x91,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xB0,0x91,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x91,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xF0,0x91,0x03,0x00,0x00,0xB0,0x00,0x00,0x10,0x92,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x30,0x92,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x92,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x92,0x03,0x00,0x00,0xB0,0x00,0x00,0x90,0x92,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xB0,0x92,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x92,0x03,0x00,0x00,0xB0,0x00,0x00,
            0xF0,0x92,0x03,0x00,0x00,0xB0,0x00,0x00,0x10,0x93,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x30,0x93,0x03,0x00,0x00,0xB0,0x00,0x00,0x50,0x93,0x03,0x00,0x00,0xC8,0x00,0x00,
            0x70,0x93,0x03,0x00,0x00,0xA8,0x00,0x00,0x90,0x93,0x03,0x00,0x00,0xA8,0x00,0x00,
            0xB0,0x93,0x03,0x00,0x00,0xB0,0x00,0x00,0xD0,0x93,0x03,0x00,0x00,0xA8,0x00,0x00,
            0xF0,0x93,0x03,0x00,0x00,0xA8,0x00,0x00,0x10,0x94,0x03,0x00,0x00,0xA8,0x00,0x00,
            0x30,0x94,0x03,0x00,0x00,0xA8,0x00,0x00,0x50,0x94,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x94,0x03,0x00,0x00,0xA0,0x00,0x00,0x90,0x94,0x03,0x00,0x00,0xB8,0x00,0x00,
            0xB0,0x94,0x03,0x00,0x00,0xC0,0x00,0x00,0xD0,0x94,0x03,0x00,0x00,0xB8,0x00,0x00,
            0xF0,0x94,0x03,0x00,0x00,0xB8,0x00,0x00,0x10,0x95,0x03,0x00,0x00,0xB8,0x00,0x00,
            0x30,0x95,0x03,0x00,0x00,0xB8,0x00,0x00,0x50,0x95,0x03,0x00,0x00,0xB0,0x00,0x00,
            0x70,0x95,0x03,0x00,0x00,0xC0,0x00,0x00,0x90,0x95,0x03,0x00,0x00,0xB8,0x00,0x00,
            0xB0,0x95,0x03,0x00,0x00,0xB8,0x00,0x00,0xD0,0x95,0x03,0x00,0x00,0xB8,0x00,0x00 }.AsReadOnly();
    }
}
