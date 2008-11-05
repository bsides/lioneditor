using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public class MoveFindItem : IChangeable, ISupportDigest
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
        private static string[] digestableProperties = new string[] {
            "X", "Y", "CommonItem", "RareItem",
            "Unknown1", "Unknown2", "Unknown3", "Unknown4", 
            "SteelNeedle", "SleepingGas", "Deathtrap", "Degenerator" };

        public IList<string> DigestableProperties
        {
            get { return digestableProperties; }
        }

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

    public class MapMoveFindItems : IChangeable, IXmlDigest
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

        #region IXmlDigest Members

        public void WriteXml( System.Xml.XmlWriter writer )
        {
            if( HasChanged )
            {
                writer.WriteStartElement( GetType().Name );
                writer.WriteAttributeString( "name", Name );

                DigestGenerator.WriteDigestEntry( writer, "Tile1", Default.Items[0], Items[0] );
                DigestGenerator.WriteDigestEntry( writer, "Tile2", Default.Items[1], Items[1] );
                DigestGenerator.WriteDigestEntry( writer, "Tile3", Default.Items[2], Items[2] );
                DigestGenerator.WriteDigestEntry( writer, "Tile4", Default.Items[3], Items[3] );

                writer.WriteEndElement();
            }
        }

        #endregion
    }

    public class AllMoveFindItems : PatchableFile, IChangeable, IXmlDigest
    {
        public MapMoveFindItems[] MoveFindItems { get; private set; }

        public AllMoveFindItems Default { get; private set; }

        public AllMoveFindItems( Context context, IList<byte> bytes, AllMoveFindItems def )
        {
            Default = def;
            const int numMaps = 128;
            IList<string> names = context == Context.US_PSP ? PSPResources.MapNames : PSXResources.MapNames;

            List<MapMoveFindItems> moveFindItems = new List<MapMoveFindItems>( numMaps * 4 );
            if ( Default == null )
            {
                for ( int i = 0; i < numMaps; i++ )
                {
                    moveFindItems.Add( new MapMoveFindItems( bytes.Sub( i * 4 * 4, ( i + 1 ) * 4 * 4 - 1 ), names[i] ) );
                }
            }
            else
            {
                for ( int i = 0; i < numMaps; i++ )
                {
                    moveFindItems.Add( new MapMoveFindItems( bytes.Sub( i * 4 * 4, ( i + 1 ) * 4 * 4 - 1 ), names[i], def.MoveFindItems[i] ) );
                }
            }
            MoveFindItems = moveFindItems.ToArray();
        }

        public AllMoveFindItems( Context context, IList<byte> bytes )
            : this( context, bytes, null )
        {
        }

        public List<string> GenerateCodes()
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                return Codes.GenerateCodes( Context.US_PSP, PSPResources.MoveFind, this.ToByteArray(), 0x274754 );
            }
            else
            {
                return new List<string>();
            }
        }


        public override bool HasChanged
        {
            get { return Default != null && MoveFindItems.Exists( item => item.HasChanged ); }
        }

        public byte[] ToByteArray()
        {
            int numMaps = FFTPatch.Context == Context.US_PSP ? 134 : 128;
            List<byte> result = new List<byte>( 4 * 4 * numMaps );
            foreach ( MapMoveFindItems items in MoveFindItems )
            {
                result.AddRange( items.ToByteArray() );
            }

            return result.ToArray();
        }


        public void WriteXml( System.Xml.XmlWriter writer )
        {
            if( HasChanged )
            {
                writer.WriteStartElement( this.GetType().Name );
                writer.WriteAttributeString( "changed", HasChanged.ToString() );
                foreach( MapMoveFindItems m in MoveFindItems )
                {
                    m.WriteXml( writer );
                }
                writer.WriteEndElement();
            }
        }

        public override IList<PatchedByteArray> GetPatches( Context context )
        {
            List<PatchedByteArray> result = new List<PatchedByteArray>();
            byte[] bytes = ToByteArray();

            if( context == Context.US_PSX )
            {
                result.Add( new PatchedByteArray( PsxIso.Sectors.BATTLE_BIN, 0x08EE74, bytes ) );
            }
            else if( context == Context.US_PSP )
            {
                result.Add( new PatchedByteArray( PspIso.Sectors.PSP_GAME_SYSDIR_BOOT_BIN, 0x2707A8, bytes ) );
                result.Add( new PatchedByteArray( PspIso.Sectors.PSP_GAME_SYSDIR_EBOOT_BIN, 0x2707A8, bytes ) );
            }

            return result;
        }



    }
}
