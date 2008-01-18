/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Collections.Generic;


namespace FFTPatcher.Datatypes
{
    public class ItemAttributes
    {
        public byte Value { get; private set; }

        public byte PA { get; set; }
        public byte MA { get; set; }
        public byte Speed { get; set; }
        public byte Move { get; set; }
        public byte Jump { get; set; }

        public Statuses PermanentStatuses { get; private set; }
        public Statuses StatusImmunity { get; private set; }
        public Statuses StartingStatuses { get; private set; }

        public Elements Absorb { get; private set; }
        public Elements Cancel { get; private set; }
        public Elements Half { get; private set; }
        public Elements Weak { get; private set; }
        public Elements Strong { get; private set; }

        public ItemAttributes Default { get; private set; }

        public ItemAttributes( byte value, SubArray<byte> bytes, ItemAttributes defaults )
        {
            Default = defaults;
            Value = value;
            PA = bytes[0];
            MA = bytes[1];
            Speed = bytes[2];
            Move = bytes[3];
            Jump = bytes[4];

            PermanentStatuses = new Statuses( new SubArray<byte>( bytes, 5, 9 ), defaults == null ? null : defaults.PermanentStatuses );
            StatusImmunity = new Statuses( new SubArray<byte>( bytes, 10, 14 ), defaults == null ? null : defaults.StatusImmunity );
            StartingStatuses = new Statuses( new SubArray<byte>( bytes, 15, 19 ), defaults == null ? null : defaults.StartingStatuses );

            Absorb = new Elements( bytes[20] );
            Cancel = new Elements( bytes[21] );
            Half = new Elements( bytes[22] );
            Weak = new Elements( bytes[23] );
            Strong = new Elements( bytes[24] );
        }

        public ItemAttributes( byte value, SubArray<byte> bytes )
            : this( value, bytes, null )
        {
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 25 );
            result.Add( PA );
            result.Add( MA );
            result.Add( Speed );
            result.Add( Move );
            result.Add( Jump );
            result.AddRange( PermanentStatuses.ToByteArray() );
            result.AddRange( StatusImmunity.ToByteArray() );
            result.AddRange( StartingStatuses.ToByteArray() );
            result.Add( Absorb.ToByte() );
            result.Add( Cancel.ToByte() );
            result.Add( Half.ToByte() );
            result.Add( Weak.ToByte() );
            result.Add( Strong.ToByte() );

            return result.ToArray();
        }

        public override string ToString()
        {
            return Value.ToString( "X2" );
        }
    }

    public class AllItemAttributes
    {
        public ItemAttributes[] ItemAttributes { get; private set; }

        public AllItemAttributes( SubArray<byte> first, SubArray<byte> second )
        {
            List<ItemAttributes> temp = new List<ItemAttributes>( 0x65 );
            byte[] defaultFirst = second == null ? FFTPatcher.Properties.PSXResources.OldItemAttributesBin : Resources.OldItemAttributesBin;
            byte[] defaultSecond = second == null ? null : Resources.NewItemAttributesBin;

            for( byte i = 0; i < 0x50; i++ )
            {
                temp.Add( new ItemAttributes( i, new SubArray<byte>( first, i * 25, (i + 1) * 25 - 1 ),
                    new ItemAttributes( i, new SubArray<byte>( defaultFirst, i * 25, (i + 1) * 25 - 1 ) ) ) );
            }
            if( second != null )
            {
                for( byte i = 0x50; i < 0x65; i++ )
                {
                    temp.Add( new ItemAttributes( i, new SubArray<byte>( second, (i - 0x50) * 25, ((i - 0x50) + 1) * 25 - 1 ),
                        new ItemAttributes( i, new SubArray<byte>( defaultSecond, (i - 0x50) * 25, ((i - 0x50) + 1) * 25 - 1 ) ) ) );
                }
            }

            ItemAttributes = temp.ToArray();
        }

        public byte[] ToFirstByteArray()
        {
            List<byte> result = new List<byte>( 0x50 * 25 );
            for( int i = 0; i < 0x50; i++ )
            {
                result.AddRange( ItemAttributes[i].ToByteArray() );
            }
            return result.ToArray();
        }

        public byte[] ToSecondByteArray()
        {
            List<byte> result = new List<byte>( 0x15 * 25 );
            for( int i = 0x50; i < 0x65; i++ )
            {
                result.AddRange( ItemAttributes[i].ToByteArray() );
            }
            return result.ToArray();
        }

        public List<string> GenerateCodes()
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                List<string> strings = new List<string>();
                strings.AddRange( Codes.GenerateCodes( Context.US_PSP, Resources.NewItemAttributesBin, this.ToSecondByteArray(), 0x25B1B8 ) );
                strings.AddRange( Codes.GenerateCodes( Context.US_PSP, Resources.OldItemAttributesBin, this.ToFirstByteArray(), 0x32A694 ) );
                return strings;
            }
            else
            {
                return Codes.GenerateCodes( Context.US_PSX, FFTPatcher.Properties.PSXResources.OldItemAttributesBin, this.ToFirstByteArray(), 0x0642C4 );
            }
        }
    }
}
