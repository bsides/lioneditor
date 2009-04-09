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
        const long expandedIsoLength = 0x20F18D00;
        const long defaultSectorCount = 230151;
        const long expandedSectorCount = 0x20F18D00/2352;


        public Sprite this[int i]
        {
            get { return sprites[i]; }
        }

        public static AllSprites FromPsxIso(Stream iso)
        {
            if (!DetectExpansionOfPsxIso(iso))
            {
                ExpandIso(iso);
            }
            return new AllSprites(AllSpriteAttributes.FromPsxIso(iso), SpriteFileLocations.FromPsxIso(iso));
        }

        struct Time
        {
            private byte min;
            private byte sec;
            private byte f;
            public byte Minutes { get { return min; } }
            public byte Seconds { get { return sec; } }
            public byte Frames { get { return f; } }
            public Time(byte m, byte s, byte f) 
            {
                min = m;
                sec = s;
                this.f = f;
            }

            public Time AddFrame()
            {
                byte newF = (byte)(f + 1);
                byte newS = sec;
                byte newMin = min;

                if ( newF == 74)
                {
                    newF = 0;
                    newS++;
                    if (newS == 60)
                    {
                        newS = 0;
                        newMin++;
                    }
                }
                return new Time(newMin, newS, newF);
            }
            public byte[] ToBCD()
            {
                return new byte[] {
                    (byte)(min/10 * 16 + min%10),
                    (byte)(sec/10 * 16 + sec%10),
                    (byte)(f/10 * 16 + f%10) };
            }
        }

        public static void ExpandIso( Stream iso )
        {
            byte[] expandedBytes = expandedSectorCount.ToBytes();
            byte[] reverseBytes = new byte[4] { expandedBytes[3], expandedBytes[2], expandedBytes[1], expandedBytes[0] };
            PatcherLib.Iso.PsxIso.PatchPsxIso( iso, PatcherLib.Iso.PsxIso.NumberOfSectorsLittleEndian.GetPatchedByteArray( expandedBytes ) );
            PatcherLib.Iso.PsxIso.PatchPsxIso( iso, PatcherLib.Iso.PsxIso.NumberOfSectorsBigEndian.GetPatchedByteArray( reverseBytes ) );
            PatcherLib.Iso.PsxIso.PatchPsxIso( iso, 
                new PatchedByteArray( 
                    (PatcherLib.Iso.PsxIso.Sectors)22, 
                    0xDC, 
                    new byte[] { 0x00, 0x38, 0x00, 0x00, 0x00, 0x00, 0x38, 0x00 } ) );

            // Build directory entry for /DUMMY
            iso.Seek(0x203E6500, SeekOrigin.Begin);
            iso.Write(Properties.Resources.PatchedDummyFolder, 0, Properties.Resources.PatchedDummyFolder.Length);

            // Read old sprites
            var locs = SpriteFileLocations.FromPsxIso(iso);
            byte[][] oldSprites = new byte[NumSprites][];
            for (int i = 0; i < NumSprites; i++)
            {
                var loc = locs[i];
                oldSprites[i] = PatcherLib.Iso.PsxIso.ReadFile(iso, (PatcherLib.Iso.PsxIso.Sectors)loc.Sector, 0, (int)loc.Size);
            }

            // Expand length of ISO
            byte[] anchorBytes = new byte[] { 
                    0x00, 0xFF, 0xFF, 0xFF, 
                    0xFF, 0xFF, 0xFF, 0xFF, 
                    0xFF, 0xFF, 0xFF, 0x00 };
            byte[] sectorBytes = new byte[] {
                0x00, 0x00, 0x08, 0x00,
                0x00, 0x00, 0x08, 0x00 };
            byte[] endOfFileBytes = new byte[] {
                0x00, 0x00, 0x89, 0x00,
                0x00, 0x00, 0x89, 0x00 };
            byte[] emptySector = new byte[2328];
            Time t = new Time(51, 9, 39);
            for (long l = 0x2040B100; l < 0x20F18D00; l += 2352)
            {
                // write 0x00FFFFFF FFFFFFFF FFFFFF00 MM SS FF 02
                  // write 0x00000800 00000800 for sector of file
                  // write 0x00008900 00008900 for last sector of file
                iso.Seek(l, SeekOrigin.Begin);
                iso.Write(anchorBytes,0, anchorBytes.Length);
                iso.Write(t.ToBCD(), 0, 3);
                t = t.AddFrame();
                iso.WriteByte(0x02);
                if ((l - 0x2040B100+2352) % 0x12600 != 0)
                {
                    iso.Write(sectorBytes, 0, 8);
                }
                else
                {
                    iso.Write(endOfFileBytes, 0, 8);
                }
                iso.Write(emptySector, 0, 2328);
            }


            // Copy old sprites to new locations
            List<byte> posBytes = new List<byte>(NumSprites * 8);
            long startSector = 0x2040B100 / 2352;
            for (int i = 0; i < NumSprites; i++)
            {
                uint sector = (uint)(startSector+i*65536/2048);
                byte[] bytes = oldSprites[i];
                byte[] realBytes = new byte[65536];
                bytes.CopyTo(realBytes, 0);
                PatcherLib.Iso.PsxIso.PatchPsxIso(iso, new PatchedByteArray((int)sector, 0, realBytes));
                posBytes.AddRange(sector.ToBytes());
                posBytes.AddRange(((uint)bytes.Length).ToBytes());
            }

            // Update battle.bin
            PatcherLib.Iso.PsxIso.PatchPsxIso(iso, SpriteFileLocations.SpriteLocationsPosition.GetPatchedByteArray(posBytes.ToArray()));
        }

        public static bool DetectExpansionOfPsxIso(Stream iso)
        {
            UInt32 sectors = PatcherLib.Iso.PsxIso.ReadFile(iso, PatcherLib.Iso.PsxIso.NumberOfSectorsLittleEndian).ToUInt32();

            return iso.Length > defaultIsoLength &&
                iso.Length >= expandedIsoLength &&
                sectors > defaultSectorCount &&
                sectors >= expandedSectorCount &&
                //!SpriteFileLocations.IsoHasDefaultSpriteLocations( iso ) &&
                SpriteFileLocations.IsoHasPatchedSpriteLocations( iso );
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
