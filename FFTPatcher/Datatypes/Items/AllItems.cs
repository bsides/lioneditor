using System;
using System.Collections.Generic;
using System.Text;
using FFTPatcher.Properties;

namespace FFTPatcher.Datatypes
{
    public class AllItems
    {
        public List<Item> Items { get; private set; }
        private byte[] afterPhoenixDown; 

        public AllItems( SubArray<byte> first, SubArray<byte> second )
        {
            Items = new List<Item>();

            for( UInt16 i = 0; i < 0x80; i++ )
            {
                Items.Add( new Weapon(
                    i,
                    new SubArray<byte>( first, i * 12, (i + 1) * 12 - 1 ),
                    new SubArray<byte>( first, 0xC00 + i * 8, 0xC00 + (i + 1) * 8 - 1 ) ) );
            }
            for( UInt16 i = 0x80; i < 0x90; i++ )
            {
                Items.Add( new Shield(
                    i,
                    new SubArray<byte>( first, i * 12, (i + 1) * 12 - 1 ),
                    new SubArray<byte>( first, 0x1000 + (i - 0x80) * 2, 0x1000 + ((i - 0x80) + 1) * 2 - 1 ) ) );
            }
            for( UInt16 i = 0x90; i < 0xD0; i++ )
            {
                Items.Add( new Armor(
                    i,
                    new SubArray<byte>( first, i * 12, (i + 1) * 12 - 1 ),
                    new SubArray<byte>( first, 0x1020 + (i - 0x90) * 2, 0x1020 + ((i - 0x90) + 1) * 2 - 1 ) ) );
            }
            for( UInt16 i = 0xD0; i < 0xF0; i++ )
            {
                Items.Add( new Accessory(
                    i,
                    new SubArray<byte>( first, i * 12, (i + 1) * 12 - 1 ),
                    new SubArray<byte>( first, 0x10A0 + (i - 0xD0) * 2, 0x10A0 + ((i - 0xD0) + 1) * 2 - 1 ) ) );
            }
            for( UInt16 i = 0xF0; i < 0xFE; i++ )
            {
                Items.Add( new ChemistItem(
                    i,
                    new SubArray<byte>( first, i * 12, (i + 1) * 12 - 1 ),
                    new SubArray<byte>( first, 0x10E0 + (i - 0xF0) * 3, 0x10E0 + ((i - 0xF0) + 1) * 3 - 1 ) ) );
            }
            afterPhoenixDown = new byte[0x18];
            for( int i = 0; i < 0x18; i++ )
            {
                afterPhoenixDown[i] = first[0xBE8 + i];
            }

            for( UInt16 i = 0; i < 0x20; i++ )
            {
                Items.Add( new Weapon(
                    (UInt16)(i + 0x100),
                    new SubArray<byte>( second, i * 12, (i + 1) * 12 - 1 ),
                    new SubArray<byte>( second, 0x2D0 + i * 8, 0x2D0 + (i + 1) * 8 - 1 ) ) );
            }
            for( UInt16 i = 0x20; i < 0x24; i++ )
            {
                Items.Add( new Shield(
                    (UInt16)(i + 0x100),
                    new SubArray<byte>( second, i * 12, (i + 1) * 12 - 1 ),
                    new SubArray<byte>( second, 0x3D0 + (i - 0x20) * 2, 0x3D0 + ((i - 0x20) + 1) * 2 - 1 ) ) );
            }
            for( UInt16 i = 0x24; i < 0x34; i++ )
            {
                Items.Add( new Armor(
                    (UInt16)(i + 0x100),
                    new SubArray<byte>( second, i * 12, (i + 1) * 12 - 1 ),
                    new SubArray<byte>( second, 0x3D8 + (i - 0x24) * 2, 0x3D8 + ((i - 0x24) + 1) * 2 - 1 ) ) );
            }
            for( UInt16 i = 0x34; i < 0x3C; i++ )
            {
                Items.Add( new Accessory(
                    (UInt16)(i + 0x100),
                    new SubArray<byte>( second, i * 12, (i + 1) * 12 - 1 ),
                    new SubArray<byte>( second, 0x3F8 + (i - 0x34) * 2, 0x3F8 + ((i - 0x34) + 1) * 2 - 1 ) ) );
            }
        }

        public byte[] ToFirstByteArray()
        {
            List<byte> result = new List<byte>( 0x1BDC );
            for( int i = 0; i < 0xFE; i++ )
            {
                result.AddRange( Items[i].ToFirstByteArray() );
            }
            result.AddRange( afterPhoenixDown );
            for( int i = 0; i < 0xFE; i++ )
            {
                result.AddRange( Items[i].ToSecondByteArray() );
            }

            return result.ToArray();
        }

        public byte[] ToSecondByteArray()
        {
            List<byte> result = new List<byte>( 0x408 );
            for( int i = 0x100; i < 0x13C; i++ )
            {
                result.AddRange( Items[i - 2].ToFirstByteArray() );
            }
            for( int i = 0x100; i < 0x13C; i++ )
            {
                result.AddRange( Items[i - 2].ToSecondByteArray() );
            }

            return result.ToArray();
        }

        public string GenerateCodes()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append( Utilities.GenerateCodes( Context.US_PSP, Resources.NewItemsBin, this.ToSecondByteArray(), 0x25ADAC ) );
            sb.Append( Utilities.GenerateCodes( Context.US_PSP, Resources.OldItemsBin, this.ToFirstByteArray(), 0x329288 ) );
            return sb.ToString();
        }
    }
}
