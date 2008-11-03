using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public class MoveFindItem : IChangeable
    {
        public MoveFindItem Default { get; private set; }

        public byte X { get; set; }
        public byte Y { get; set; }
        public Item CommonItem { get; set; }
        public Item RareItem { get; set; }
        public byte Trap { get; set; }
        public bool Unknown1 { get; set; }
        public bool Unknown2 { get; set; }
        public bool Unknown3 { get; set; }
        public bool Unknown4 { get; set; }
        public bool SteelNeedle { get; set; }
        public bool SleepingGas { get; set; }
        public bool Deathtrap { get; set; }
        public bool Degenerator { get; set; }

        public MoveFindItem( IList<byte> bytes, MoveFindItem def )
            : this( bytes )
        {
            this.Default = def;
        }

        public MoveFindItem( IList<byte> bytes )
        {
            X = (byte)( ( bytes[0] & 0xF0 ) >> 4 );
            Y = (byte)( bytes[0] & 0x0F );
            CommonItem = Item.DummyItems[bytes[3]];
            RareItem = Item.DummyItems[bytes[2]];
            bool[] b = Utilities.BooleansFromByteMSB( bytes[1] );
            Unknown1 = b[0];
            Unknown2 = b[1];
            Unknown3 = b[2];
            Unknown4 = b[3];
            SteelNeedle = b[4];
            SleepingGas = b[5];
            Deathtrap = b[6];
            Degenerator = b[7];
        }

        public IList<byte> ToByteArray()
        {
            return new byte[] { 
                (byte)( ( ( X & 0x0F ) << 4 ) | ( Y & 0x0F ) ), 
                Utilities.ByteFromBooleans(Unknown1, Unknown2, Unknown3, Unknown4, SteelNeedle, SleepingGas, Deathtrap, Degenerator),
                (byte)(RareItem.Offset & 0xFF),
                (byte)(CommonItem.Offset & 0xFF) };
        }

        public bool HasChanged
        {
            get
            {
                return Default != null && (
                    X != Default.X ||
                    Y != Default.Y ||
                    CommonItem.Offset != Default.CommonItem.Offset ||
                    RareItem.Offset != Default.RareItem.Offset ||
                    ( Unknown1 ^ Default.Unknown1 ) ||
                    ( Unknown2 ^ Default.Unknown2 ) ||
                    ( Unknown3 ^ Default.Unknown3 ) ||
                    ( Unknown4 ^ Default.Unknown4 ) ||
                    ( SteelNeedle ^ Default.SteelNeedle ) ||
                    ( SleepingGas ^ Default.SleepingGas ) ||
                    ( Deathtrap ^ Default.Deathtrap ) ||
                    ( Degenerator ^ Default.Degenerator ) );

            }
        }
    }

    public class MapMoveFindItems : IChangeable
    {
        public IList<MoveFindItem> Items { get; private set; }
        public MapMoveFindItems Default { get; private set; }
        public string Name { get; private set; }

        public MapMoveFindItems( IList<byte> bytes, string name, MapMoveFindItems def )
        {
            Default = def;
            Name = name;

            Items = new List<MoveFindItem>( 4 );
            if ( Default != null )
            {
                for ( int i = 0; i < 4; i++ )
                {
                    Items.Add( new MoveFindItem( bytes.Sub( i * 4, ( i + 1 ) * 4 - 1 ), def.Items[i] ) );
                }
            }
            else
            {
                for ( int i = 0; i < 4; i++ )
                {
                    Items.Add( new MoveFindItem( bytes.Sub( i * 4, ( i + 1 ) * 4 - 1 ) ) );
                }
            }
        }

        public MapMoveFindItems( IList<byte> bytes, string name )
            : this( bytes, name, null )
        {
        }

        public IList<byte> ToByteArray()
        {
            List<byte> result = new List<byte>( 4 * 4 );
            foreach ( MoveFindItem item in Items )
            {
                result.AddRange( item.ToByteArray() );
            }
            return result;
        }

        public bool HasChanged
        {
            get { return Default != null && Items.Exists( item => item.HasChanged ); }
        }

        public override string ToString()
        {
            return ( HasChanged ? "*" : "" ) + Name;
        }
    }

    public class AllMoveFindItems : IChangeable
    {
        public MapMoveFindItems[] MoveFindItems { get; private set; }

        public AllMoveFindItems Default { get; private set; }

        public AllMoveFindItems( IList<byte> bytes, AllMoveFindItems def )
        {
            Default = def;

            List<MapMoveFindItems> moveFindItems = new List<MapMoveFindItems>( 128 * 4 );
            if ( Default == null )
            {
                for ( int i = 0; i < 128; i++ )
                {
                    moveFindItems.Add( new MapMoveFindItems( bytes.Sub( i * 4 * 4, ( i + 1 ) * 4 * 4 - 1 ), names[i] ) );
                }
            }
            else
            {
                for ( int i = 0; i < 128; i++ )
                {
                    moveFindItems.Add( new MapMoveFindItems( bytes.Sub( i * 4 * 4, ( i + 1 ) * 4 * 4 - 1 ), names[i], def.MoveFindItems[i] ) );
                }
            }
            MoveFindItems = moveFindItems.ToArray();
        }

        public AllMoveFindItems( IList<byte> bytes )
            : this( bytes, null )
        {
        }


        public bool HasChanged
        {
            get { return Default != null && MoveFindItems.Exists( item => item.HasChanged ); }
        }

        public IList<byte> ToByteArray()
        {
            List<byte> result = new List<byte>( 4 * 4 * 128 );
            foreach ( MapMoveFindItems items in MoveFindItems )
            {
                result.AddRange( items.ToByteArray() );
            }

            return result;
        }

        string[] names = new string[128] {
"(No name)",
"At main gate of Igros Castle",
"Back gate of Lesalia Castle",
"Hall of St. Murond Temple",
"Office of Lesalia Castle",
"Roof of Riovanes Castle",
"At the gate of Riovanes Castle",
"Inside of Riovanes Castle",
"Riovanes Castle",
"Citadel of Igros Castle",
"Inside of Igros Castle",
"Office of Igros Castle",
"At the gate of Lionel Castle",
"Inside of Lionel Castle",
"Office of Lionel Castle",
"At the gate of Limberry Castle",
"Inside of Limberry Castle",
"Underground cemetery of Limberry Castle",
"Office of Limberry Castle",
"At the gate of Limberry Castle",
"Inside of Zeltennia Castle",
"Zeltennia Castle",
"Magic City Gariland",
"Beoulve residence",
"Military Academy's Auditorium",
"Yardow Fort City",
"Weapon storage of Yardow",
"Goland Coal City",
"Colliery underground First floor",
"Colliery underground Second floor",
"Colliery underground Third floor",
"Dorter Trade City",
"Slums in Dorter",
"Hospital in slums",
"Cellar of Sand Mouse",
"Zaland Fort City",
"Church outside the town",
"Ruins outside Zaland",
"Goug Machine City",
"Underground passage in Goland",
"Slums in Goug",
"Besrodio's house",
"Warjilis Trade City",
"Port of Warjilis",
"Bervenia Free City",
"Ruins of Zeltennia Castle's church",
"Cemetery of Heavenly Knight, Balbanes",
"Zarghidas Trade City",
"Slums of Zarghidas",
"Fort Zeakden",
"St. Murond Temple",
"St. Murond Temple",
"Chapel of St. Murond Temple",
"Entrance to Death City",
"Lost Sacred Precincts",
"Graveyard of Airships",
"Orbonne Monastery",
"Underground Book Storage First Floor",
"Underground Book Storage Second Floor",
"Underground Book Storage Third Floor",
"Underground Book Storage Fourth Floor",
"Underground Book Storage Fifth Floor",
"Chapel of Orbonne Monastery",
"Golgorand Execution Site",
"In front of Bethla Garrison's Sluice",
"Granary of Bethla Garrison",
"South Wall of Bethla Garrison",
"North Wall of Bethla Garrison",
"Bethla Garrison",
"Murond Death City",
"Nelveska Temple",
"Dolbodar Swamp",
"Fovoham Plains",
"Inside of windmill Shed",
"Sweegy Woods",
"Bervenia Volcano",
"Zeklaus Desert",
"Lenalia Plateau",
"Zigolis Swamp",
"Yuguo Woods",
"Araguay Woods",
"Grog Hill",
"Bed Desert",
"Zirekile Falls",
"Bariaus Hill",
"Mandalia Plains",
"Doguola Pass",
"Bariaus Valley",
"Finath River",
"Poeskas Lake",
"Germinas Peak",
"Thieves Fort",
"Igros·Beoulve residence",
"Broke down shed·Wooden building",
"Broke down shed·Stone building",
"Church",
"Pub",
"Inside castle gate in Lesalia",
"Outside castle gate in Lesalia",
"Main street of Lesalia",
"Public cemetary",
"For tutorial 1",
"For tutorial 2",
"Windmill shed",
"A room of Beoulve residence",
"terminate",
"delta",
"nogias",
"voyage",
"bridge",
"valkyries",
"mlapan",
"tiger",
"horror",
"end",
"Banished fort",
"(No name) -- Battle Arena",
"(No name) -- Checkerboard Wall",
"(No name) -- Checkerboard Wall ???",
"(No name) -- Checkerboard Wall Waterland",
"(Garbled name) -- Sloped Checkerboard",
"","","","","","",""
};
    }
}
