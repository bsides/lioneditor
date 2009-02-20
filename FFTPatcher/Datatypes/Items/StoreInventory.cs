using System;
using System.Collections.Generic;
using PatcherLib;
using PatcherLib.Datatypes;
using PatcherLib.Utilities;

namespace FFTPatcher.Datatypes
{
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

        public Shops this[Item i]
        {
            get
            {
                Shops result = Shops.Empty;
                foreach( var s in Stores )
                {
                    if( s[i] )
                    {
                        result |= s.WhichStore;
                    }
                }
                return result;
            }
            set
            {
                foreach( var s in shops )
                {
                    StoresDict[s][i] = ((value & s) == s);
                }
            }
        }

        private Shops[] shops = new Shops[16] { Shops.Bervenia, Shops.Dorter, Shops.Gariland, Shops.Goland, Shops.Goug, Shops.Igros, 
                                    Shops.Lesalia,Shops.Limberry, Shops.Lionel, Shops.None, Shops.Riovanes, Shops.Warjilis, 
                                    Shops.Yardrow, Shops.Zaland, Shops.Zarghidas, Shops.Zeltennia };

        public AllStoreInventories( Context context, IList<byte> bytes, IList<byte> defaultBytes )
        {
            if (defaultBytes != null)
            {
                Default = new AllStoreInventories( context, defaultBytes );
            }

            ourContext = context;
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
                result.Add( new PatchedByteArray( PatcherLib.Iso.PspIso.Sectors.PSP_GAME_SYSDIR_BOOT_BIN, 0x2DC8D0, bytes ) );
                result.Add( new PatchedByteArray( PatcherLib.Iso.PspIso.Sectors.PSP_GAME_SYSDIR_EBOOT_BIN, 0x2DC8D0, bytes ) );
            }
            else
            {
                result.Add( new PatchedByteArray( PatcherLib.Iso.PsxIso.StoreInventories.Sector, PatcherLib.Iso.PsxIso.StoreInventories.StartLocation, bytes ) );
            }

            return result.AsReadOnly();
        }

        public override bool HasChanged
        {
            get { return Stores.Exists( s => s.HasChanged ); }
        }
    }

}
