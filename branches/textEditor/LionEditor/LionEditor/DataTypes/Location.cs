/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of LionEditor.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace LionEditor
{
    // TODO: Document this file better
    class Location
    {
        private static List<Location> allLocations;
        
        public static List<Location> AllLocations
        {
            get
            {
                if( allLocations == null )
                {
                    allLocations = new List<Location>();
                    for( byte b = 0; b <= 0x2A; b++ )
                    {
                        allLocations.Add( new Location( b ) );
                    }
                }

                return allLocations;
            }
        }

        Locations location;

        public Location( byte b )
        {
            location = (Locations)b;
        }

        public override string ToString()
        {
            MemberInfo[] memInfo = typeof( Locations ).GetMember( location.ToString() );
            if( (memInfo != null) && (memInfo.Length > 0) )
            {
                object[] attrs = memInfo[0].GetCustomAttributes( typeof( DescriptionAttribute ), false );
                if( (attrs != null) && (attrs.Length > 0) )
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return base.ToString();
        }

        static string GetDescription( Enum en )
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember( en.ToString() );
            if( memInfo != null && memInfo.Length > 0 )
            {
                object[] attrs = memInfo[0].GetCustomAttributes( typeof( DescriptionAttribute ),
                                                              false );
                if( attrs != null && attrs.Length > 0 )
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }

        public enum Locations
        {
            [Description( "Lesalia" )]
            Lesalia = 0x00,
            [Description( "Riovanes Castle" )]
            RiovanesCastle = 0x01,
            [Description( "Eagrose Castle" )]
            EagroseCastle = 0x02,
            [Description( "Lionel Castle" )]
            LionelCastle = 0x03,
            [Description( "Limberry Castle" )]
            LimberryCastle = 0x04,
            [Description( "Zeltennia Castle" )]
            ZeltenniaCastle = 0x05,
            [Description( "Gariland" )]
            Gariland = 0x06,
            [Description( "Yardrow" )]
            Yardrow = 0x07,
            [Description( "Gollund" )]
            Gollund = 0x08,
            [Description( "Dorter" )]
            Dorter = 0x09,
            [Description( "Zaland" )]
            Zaland = 0x0A,
            [Description( "Goug" )]
            Goug = 0x0B,
            [Description( "Warjilis" )]
            Warjilis = 0x0C,
            [Description( "Bervenia" )]
            Bervenia = 0x0D,
            [Description( "Sal Ghidos" )]
            SalGhidos = 0x0E,
            [Description( "Ziekden Fortress" )]
            ZiekdenFortress = 0x0F,
            [Description( "Mullonde" )]
            Mullonde = 0x10,
            [Description( "Brigands' Den" )]
            BrigandsDen = 0x11,
            [Description( "Orbonne Monastery" )]
            OrbonneMonastery = 0x12,
            [Description( "Golgollada Gallows" )]
            GolgolladaGallows = 0x13,
            [Description( "Mullonde" )]
            Mullonde2 = 0x14,
            [Description( "Fort Besselat" )]
            FortBesselat = 0x15,
            [Description( "Midlight's Deep" )]
            MidlightsDeep = 0x16,
            [Description( "Nelveska Temple" )]
            NelveskaTemple = 0x17,
            [Description( "Mandalia Plain" )]
            MandaliaPlain = 0x18,
            [Description( "Fovoham Windflats" )]
            FovohamWindflats = 0x19,
            [Description( "The Siedge Weald" )]
            TheSiedgeWeald = 0x1A,
            [Description( "Mount Bervenia" )]
            MountBervenia = 0x1B,
            [Description( "Zeklaus Desert" )]
            ZeklausDesert = 0x1C,
            [Description( "Lenalian Plateau" )]
            LenalianPlateau = 0x1D,
            [Description( "Tchigolith Fenlands" )]
            TchigolithFenlands = 0x1E,
            [Description( "The Yuguewood" )]
            TheYuguewood = 0x1F,
            [Description( "Araguay Woods" )]
            AraguayWoods = 0x20,
            [Description( "Grogh Heights" )]
            GroghHeights = 0x21,
            [Description( "Beddha Sandwaste" )]
            BeddhaSandwaste = 0x22,
            [Description( "Zeirchele Falls" )]
            ZeircheleFalls = 0x23,
            [Description( "Dorvauldar Marsh" )]
            DorvauldarMarsh = 0x24,
            [Description( "Balias Tor" )]
            BaliasTor = 0x25,
            [Description( "Dugeura Pass" )]
            DugeuraPass = 0x26,
            [Description( "Balias Swale" )]
            BaliasSwale = 0x27,
            [Description( "Finnath Creek" )]
            FinnathCreek = 0x28,
            [Description( "Lake Poescas" )]
            LakePoescas = 0x29,
            [Description( "Mount Germinas" )]
            MountGerminas = 0x2A
        }
    }
}
