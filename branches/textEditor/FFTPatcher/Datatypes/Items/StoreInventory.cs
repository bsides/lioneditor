using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public enum Shops
    {
        Lesalia = 0x8000,
        Riovanes = 0x4000,
        Igros = 0x2000,
        Lionel = 0x1000,
        Limberry = 0x0800,
        Zeltennia = 0x0400,
        Gariland = 0x0200,
        Yardrow = 0x0100,
        Goland = 0x0080,
        Dorter = 0x0040,
        Zaland = 0x0020,
        Goug = 0x0010,
        Warjilis = 0x0008,
        Bervenia = 0x0004,
        Zarghidas = 0x0002,
        None = 0x0001
    }

    public class StoreInventory : IChangeable, ISupportDigest
    {
        private Dictionary<Item, bool> items = new Dictionary<Item, bool>();
        private IList<Item> itemsList;
        private string name;

        private Context ourContext = Context.Default;

        public Shops WhichStore { get; private set; }

        public bool this[int index]
        {
            get { return items[itemsList[index]]; }
            set { items[itemsList[index]] = value; }
        }

        public bool this[Item key]
        {
            get { return items[key]; }
            set { items[key] = value; }
        }

        public StoreInventory Default { get; private set; }

        public StoreInventory( Context context, Shops whichStore, IList<byte> bytes, IList<byte> defaultBytes )
            : this( context, whichStore, bytes )
        {
            Default = new StoreInventory( context, whichStore, defaultBytes );
        }

        public StoreInventory( Context context, Shops whichStore, IList<byte> bytes )
        {
            WhichStore = whichStore;
            ourContext = context;
            itemsList = context == Context.US_PSP ? Item.PSPDummies.Sub( 0, 255 ) : Item.PSXDummies;
            for ( int i = 0; i < 256; i++ )
            {
                UInt16 currentShort = (UInt16)( bytes[i * 2] * 256 + bytes[i * 2 + 1] );
                items[itemsList[i]] = ( currentShort & (int)whichStore ) > 0;
            }
            name = context == Context.US_PSP ? PSPResources.ShopNames[whichStore] : PSXResources.ShopNames[whichStore];
        }

        public void UpdateByteArray( IList<byte> bytes )
        {
            System.Diagnostics.Debug.Assert( bytes.Count == 256 * 2 );
            int inc = 1;
            if ( (int)WhichStore >= 0x0100 )
            {
                inc = 0;
            }

            byte or = (byte)( ( (int)WhichStore ) >> ( ( 1 - inc ) * 8 ) );

            for ( int i = 0; i < 256; i++ )
            {
                if ( items[itemsList[i]] )
                {
                    bytes[i * 2 + inc] |= or;
                }
                else
                {
                    bytes[i * 2 + inc] &= ( (byte)( ~or ) );
                }
            }
        }

        public override string ToString()
        {
            return (HasChanged ? "*" : "") + name;
        }

        public IList<string> DigestableProperties
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasChanged
        {
            get { return Default != null && itemsList.Exists( i => items[i] != Default.items[i] ); }
        }

    }

    public class AllStoreInventories : PatchableFile
    {
        public AllStoreInventories Default { get; private set; }
        private Context ourContext;

        public IList<StoreInventory> Stores { get; private set; }
        public IDictionary<Shops, StoreInventory> StoresDict { get; private set; }

        public AllStoreInventories( Context context, IList<byte> bytes, IList<byte> defaultBytes )
        {
            if (defaultBytes != null)
            {
                Default = new AllStoreInventories( context, defaultBytes );
            }

            ourContext = context;
            Shops[] shops = (Shops[])Enum.GetValues( typeof( Shops ) );
            List<StoreInventory> stores = new List<StoreInventory>( shops.Length );
            Dictionary<Shops, StoreInventory> storesDict = new Dictionary<Shops, StoreInventory>( shops.Length );
            foreach ( Shops s in shops )
            {
                StoreInventory si = null;
                if ( defaultBytes != null )
                {
                    si = new StoreInventory( context, s, bytes, defaultBytes );
                }
                else
                {
                    si = new StoreInventory( context, s, bytes );
                }
                stores.Add( si );
                storesDict.Add( s, si );
            }

            Stores = stores.AsReadOnly();
            StoresDict = new ReadOnlyDictionary<Shops, StoreInventory>( storesDict, false );
        }

        public AllStoreInventories( Context context, IList<byte> bytes )
            : this( context, bytes, null )
        {
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[256 * 2];

            Stores.ForEach( s => s.UpdateByteArray( result ) );

            return result;
        }

        public override IList<PatchedByteArray> GetPatches( Context context )
        {
            List<PatchedByteArray> result = new List<PatchedByteArray>( 2 );
            byte[] bytes = ToByteArray();
            if ( context == Context.US_PSP )
            {
                result.Add( new PatchedByteArray( PspIso.Files.PSP_GAME.SYSDIR.BOOT_BIN, 0x2DC8D0, bytes ) );
                result.Add( new PatchedByteArray( PspIso.Files.PSP_GAME.SYSDIR.EBOOT_BIN, 0x2DC8D0, bytes ) );
            }
            else
            {
                result.Add( new PatchedByteArray( PsxIso.StoreInventories.Sector, PsxIso.StoreInventories.StartLocation, bytes ) );
            }

            return result.AsReadOnly();
        }

        public override bool HasChanged
        {
            get { return Stores.Exists( s => s.HasChanged ); }
        }
    }

}
