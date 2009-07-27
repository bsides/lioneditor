using PatcherLib.Utilities;
using PatcherLib.Datatypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.SpriteEditor
{
    public class PSXWorldMap : AbstractImage
    {
        public PSXWorldMap()
            : base( "World Map", 496, 368 )
        {
        }

        private static IList<PatcherLib.Iso.PsxIso.KnownPosition> palettePositions = new PatcherLib.Iso.PsxIso.KnownPosition[] {
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 12, 256*2),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 59404, 256*2),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 122892, 256*2),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 155660, 256*2),
        };

        private static IList<PatcherLib.Iso.PsxIso.KnownPosition> topLeft = new PatcherLib.Iso.PsxIso.KnownPosition[] {
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 532, 1972-532),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 1980, 2048-1980),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 2060, 2232-2060),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 2240, 3920-2240),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 3928, 4096-3928),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 4108, 4180-4108),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 4188, 6108-4188),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 6116, 6144-6116),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 6156, 6368-6156),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 6376, 8056-6376),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 8064, 8192-8064),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 8204, 8316-8204),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 8324, 10004-8324),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 10012, 10240-10012),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 10252, 10264-10252),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 10272, 12192-10272),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 12200, 12288-12200),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 12300, 12452-12300),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 12460, 14140-12460),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 14148, 14336-14148),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 14348, 14400-14348),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 14408, 16328-14408),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 16336, 16384-16336),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 16396, 16588-16396),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 16596, 18276-16596),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 18284, 18432-18284),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 18444, 18536-18444),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 18544, 20464-18544),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 20472, 20480-20472),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 20492, 20724-20492),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 20732, 22412-20732),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 22420, 22528-22420),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 22540, 22672-22540),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 22680, 24360-22680),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 24368, 24576-24368),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 24588, 24620-24588),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 24628, 26548-24628),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 26556, 26624-26556),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 26636, 26808-26636),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 26816, 28496-26816),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 28504, 28672-28504),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 28684, 28756-28684),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 28764, 30684-28764),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 30692, 30720-30692),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 30732, 30944-30732),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 30952, 32632-30952),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 32640, 32768-32640),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 32780, 32892-32780),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 32900, 34580-32900),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 34588, 34816-34588),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 34828, 34840-34828),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 34848, 36768-34848),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 36776, 36864-36776),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 36876, 37028-36876),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 37036, 38716-37036),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 38724, 38912-38724),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 38924, 38976-38924),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 38984, 40904-38984),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 40912, 40960-40912),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 40972, 41164-40972),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 41172, 42852-41172),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 42860, 43008-42860),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 43020, 43112-43020),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 43120, 45040-43120),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 45048, 45056-45048),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 45068, 45300-45068),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 45308, 46988-45308),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 46996, 47104-46996),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 47116, 47248-47116),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 47256, 48936-47256),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 48944, 49152-48944),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 49164, 49196-49164),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 49204, 51124-49204),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 51132, 51200-51132),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 51212, 51384-51212),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 51392, 53072-51392),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 53080, 53248-53080),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 53260, 53332-53260),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 53340, 55260-53340),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 55268, 55296-55268),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 55308, 55520-55308),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 55528, 57208-55528),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 57216, 57344-57216),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 57356, 57468-57356),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 57476, 58916-57476) };


        private static IList<PatcherLib.Iso.PsxIso.KnownPosition> topRight = new PatcherLib.Iso.PsxIso.KnownPosition[] {
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 59924, 61204-59924),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 61212, 61440-61212),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 61452, 61480-61452),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 61488, 63280-61488),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 63288, 63488-63288),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 63500, 63556-63500),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 63564, 65356-63564),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 65364, 65536-65364),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 65548, 65632-65548),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 65640, 67432-65640),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 67440, 67584-67440),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 67596, 67708-67596),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 67716, 69508-67716),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 69516, 69632-69516),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 69644, 69784-69644),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 69792, 71584-69792),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 71592, 71680-71592),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 71692, 71860-71692),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 71868, 73660-71868),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 73668, 73728-73668),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 73740, 73936-73740),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 73944, 75736-73944),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 75744, 75776-75744),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 75788, 76012-75788),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 76020, 77812-76020),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 77820, 77824-77820),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 77836, 78088-77836),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 78096, 79632-78096),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 79640, 79872-79640),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 79884, 79908-79884),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 79916, 81708-79916),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 81716, 81920-81716),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 81932, 81984-81932),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 81992, 83784-81992),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 83792, 83968-83792),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 83980, 84060-83980),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 84068, 85860-84068),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 85868, 86016-85868),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 86028, 86136-86028),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 86144, 87936-86144),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 87944, 88064-87944),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 88076, 88212-88076),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 88220, 90012-88220),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 90020, 90112-90020),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 90124, 90288-90124),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 90296, 92088-90296),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 92096, 92160-92096),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 92172, 92364-92172),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 92372, 94164-92372),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 94172, 94208-94172),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 94220, 94440-94220),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 94448, 96240-94448),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 96248, 96256-96248),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 96268, 96516-96268),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 96524, 98060-96524),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 98068, 98304-98068),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 98316, 98336-98316),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 98344, 100136-98344),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 100144, 100352-100144),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 100364, 100412-100364),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 100420, 102212-100420),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 102220, 102400-102220),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 102412, 102488-102412),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 102496, 104288-102496),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 104296, 104448-104296),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 104460, 104564-104460),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 104572, 106364-104572),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 106372, 106496-106372),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 106508, 106640-106508),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 106648, 108440-106648),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 108448, 108544-108448),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 108556, 108716-108556),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 108724, 110516-108724),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 110524, 110592-110524),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 110604, 110792-110604),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 110800, 112592-110800),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 112600, 112640-112600),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 112652, 112868-112652),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 112876, 114668-112876),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 114676, 114688-114676),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 114700, 114944-114700),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 114952, 116488-114952),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 116496, 116736-116496),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 116748, 116764-116748),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 116772, 118564-116772),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 118572, 118784-118572),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 118796, 118840-118796),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 118848, 120640-118848),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 120648, 120832-120648),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 120844, 120916-120844),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 120924, 122204-120924) };

        private static IList<PatcherLib.Iso.PsxIso.KnownPosition> bottomLeft = new PatcherLib.Iso.PsxIso.KnownPosition[] {
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 123412, 124852-123412),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 124860, 124928-124860),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 124940, 125112-124940),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 125120, 126800-125120),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 126808, 126976-126808),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 126988, 127060-126988),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 127068, 128988-127068),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 128996, 129024-128996),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 129036, 129248-129036),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 129256, 130936-129256),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 130944, 131072-130944),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 131084, 131196-131084),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 131204, 132884-131204),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 132892, 133120-132892),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 133132, 133144-133132),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 133152, 135072-133152),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 135080, 135168-135080),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 135180, 135332-135180),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 135340, 137020-135340),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 137028, 137216-137028),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 137228, 137280-137228),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 137288, 139208-137288),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 139216, 139264-139216),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 139276, 139468-139276),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 139476, 141156-139476),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 141164, 141312-141164),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 141324, 141416-141324),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 141424, 143344-141424),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 143352, 143360-143352),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 143372, 143604-143372),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 143612, 145292-143612),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 145300, 145408-145300),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 145420, 145552-145420),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 145560, 147240-145560),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 147248, 147456-147248),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 147468, 147500-147468),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 147508, 149428-147508),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 149436, 149504-149436),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 149516, 149688-149516),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 149696, 151376-149696),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 151384, 151552-151384),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 151564, 151636-151564),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 151644, 153564-151644),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 153572, 153600-153572),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 153612, 153824-153612),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 153832, 154552-153832),
        };

        private static IList<PatcherLib.Iso.PsxIso.KnownPosition> bottomRight = new PatcherLib.Iso.PsxIso.KnownPosition[] {
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 156180, 157460-156180),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 157468, 157696-157468),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 157708, 157736-157708),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 157744, 159536-157744),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 159544, 159744-159544),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 159756, 159812-159756),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 159820, 161612-159820),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 161620, 161792-161620),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 161804, 161888-161804),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 161896, 163688-161896),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 163696, 163840-163696),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 163852, 163964-163852),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 163972, 165764-163972),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 165772, 165888-165772),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 165900, 166040-165900),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 166048, 167840-166048),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 167848, 167936-167848),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 167948, 168116-167948),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 168124, 169916-168124),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 169924, 169984-169924),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 169996, 170192-169996),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 170200, 171992-170200),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 172000, 172032-172000),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 172044, 172268-172044),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 172276, 174068-172276),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 174076, 174080-174076),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 174092, 174344-174092),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 174352, 175888-174352),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 175896, 176128-175896),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 176140, 176164-176140),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 176172, 177964-176172),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 177972, 178176-177972),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 178188, 178240-178188),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 178248, 180040-178248),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 180048, 180224-180048),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 180236, 180316-180236),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 180324, 182116-180324),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 182124, 182272-182124),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 182284, 182392-182284),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 182400, 184192-182400),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 184200, 184320-184200),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 184332, 184468-184332),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 184476, 186268-184476),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 186276, 186368-186276),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 186380, 186544-186380),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 186552, 188344-186552),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 188352, 188416-188352),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 188428, 188620-188428),
            new PatcherLib.Iso.PsxIso.KnownPosition(PatcherLib.Iso.PsxIso.Sectors.WORLD_WLDTEX_TM2, 188628, 189396-188628),
        };

        private static void WritePixelsToBitmap( Palette palette, IList<byte> pixels, System.Drawing.Bitmap destination )
        {
            int width = destination.Width;
            int height = destination.Height;
            for ( int i = 0; i < pixels.Count; i++ )
            {
                destination.SetPixel( i % width, i / width, palette.Colors[pixels[i]] );
            }
        }

        private static void CopyBitmapToBitmap( System.Drawing.Bitmap source, System.Drawing.Bitmap destination, System.Drawing.Point destPoint )
        {
            int count = source.Width * source.Height;
            int width = source.Width;
            int destX = destPoint.X;
            int destY = destPoint.Y;

            for ( int i = 0; i < count; i++ )
            {
                destination.SetPixel( destX + i % width, destY + i / width, source.GetPixel( i % width, i / width ) );
            }
        }

        private static System.Drawing.Size[] sizes = new System.Drawing.Size[] { 
            new System.Drawing.Size(240,240),
            new System.Drawing.Size(256,240),
            new System.Drawing.Size(240,128),
            new System.Drawing.Size(256,128) };
        private static System.Drawing.Point[] positions = new System.Drawing.Point[] {
            new System.Drawing.Point(0,0),
            new System.Drawing.Point(240,0),
            new System.Drawing.Point(0,240),
            new System.Drawing.Point(240,240) };
        private static IList<PatcherLib.Iso.PsxIso.KnownPosition>[] isoPositions = new IList<PatcherLib.Iso.PsxIso.KnownPosition>[] {
            topLeft,
            topRight,
            bottomLeft,
            bottomRight
        };
        protected override void WriteImageToIsoInner( System.IO.Stream iso, System.Drawing.Image image )
        {
            using ( System.Drawing.Bitmap sourceBitmap = new System.Drawing.Bitmap( image ) )
            {
                Set<System.Drawing.Color> colors = GetColors( sourceBitmap );
                if ( colors.Count > 256 )
                {
                    ImageQuantization.OctreeQuantizer q = new ImageQuantization.OctreeQuantizer( 256, 8 );
                    using ( var newBmp = q.Quantize( sourceBitmap ) )
                    {
                        WriteImageToIsoInner( iso, newBmp );
                    }
                }
                else
                {
                    for ( int i = 0; i < 4; i++ )
                    {
                        int width = sizes[i].Width;
                        int height = sizes[i].Height;
                        int xOffset = positions[i].X;
                        int yOffset = positions[i].Y;
                        List<byte> bytes = new List<byte>( width * height );
                        for ( int y = 0; y < height; y++ )
                        {
                            for ( int x = 0; x < width; x++ )
                            {
                                bytes.Add( (byte)colors.IndexOf( sourceBitmap.GetPixel( x + xOffset, y + yOffset ) ) );
                            }
                        }

                        int currentIndex = 0;
                        foreach ( var kp in isoPositions[i] )
                        {
                            kp.PatchIso( iso, bytes.Sub( currentIndex, currentIndex + kp.Length - 1 ) );
                            currentIndex += kp.Length;
                        }
                    }

                    var palBytes = GetPaletteBytes( colors, Palette.ColorDepth._16bit );
                    palettePositions.ForEach( pp => pp.PatchIso( iso, palBytes ) );
                }
            }
        }

        protected override System.Drawing.Bitmap GetImageFromIsoInner( System.IO.Stream iso )
        {
            Palette palette = new Palette( palettePositions[0].ReadIso( iso ), Palette.ColorDepth._16bit );

            System.Drawing.Bitmap result = new System.Drawing.Bitmap( 496, 368 );

            for ( int i = 0; i < 4; i++ )
            {
                using ( System.Drawing.Bitmap bmp = new System.Drawing.Bitmap( sizes[i].Width, sizes[i].Height ) )
                {
                    List<byte> bytes = new List<byte>();
                    isoPositions[i].ForEach( kp => bytes.AddRange( kp.ReadIso( iso ) ) );
                    WritePixelsToBitmap( palette, bytes, bmp );
                    CopyBitmapToBitmap( bmp, result, positions[i] );
                }
            }

            return result;
        }

        public override string FilenameFilter
        {
            get
            {
                return "GIF image (*.gif)|*.gif";
            }
        }

        protected override string SaveFileName
        {
            get { return "WorldMap.png"; }
        }

    }
}
