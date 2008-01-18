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
    public class InflictStatus
    {
        public byte Value { get; private set; }
        public bool AllOrNothing;
        public bool Random;
        public bool Separate;
        public bool Cancel;
        public bool Blank1;
        public bool Blank2;
        public bool Blank3;
        public bool Blank4;
        public Statuses Statuses { get; private set; }
        public InflictStatus Default { get; private set; }

        public InflictStatus( byte value, SubArray<byte> bytes )
            : this( value, bytes, null )
        {
        }

        public InflictStatus( byte value, SubArray<byte> bytes, InflictStatus defaults )
        {
            Default = defaults;
            Value = value;
            Utilities.CopyByteToBooleans( bytes[0], ref AllOrNothing, ref Random, ref Separate, ref Cancel, ref Blank1, ref Blank2, ref Blank3, ref Blank4 );
            Statuses = new Statuses( new SubArray<byte>( bytes, 1, 5 ), defaults == null ? null : defaults.Statuses );
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 6 );
            result.Add( Utilities.ByteFromBooleans( AllOrNothing, Random, Separate, Cancel, Blank1, Blank2, Blank3, Blank4 ) );
            result.AddRange( Statuses.ToByteArray() );
            return result.ToArray();
        }
        
        public bool[] ToBoolArray()
        {
            return new bool[8] { 
                AllOrNothing, Random, Separate, Cancel, Blank1, Blank2, Blank3, Blank4 };
        }

        public override string ToString()
        {
            return Value.ToString( "X2" );
        }
    }

    public class AllInflictStatuses
    {
        public InflictStatus[] InflictStatuses { get; private set; }

        public AllInflictStatuses( SubArray<byte> bytes )
        {
            byte[] defaultBytes = FFTPatch.Context == Context.US_PSP ? Resources.InflictStatusesBin : FFTPatcher.Properties.PSXResources.InflictStatusesBin;
            InflictStatuses = new InflictStatus[0x80];
            for( int i = 0; i < 0x80; i++ )
            {
                InflictStatuses[i] = new InflictStatus( (byte)i, new SubArray<byte>( bytes, i * 6, (i + 1) * 6 - 1 ),
                    new InflictStatus( (byte)i, new SubArray<byte>( defaultBytes, i * 6, (i + 1) * 6 - 1 ) ) );
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 0x280 );
            foreach( InflictStatus i in InflictStatuses )
            {
                result.AddRange( i.ToByteArray() );
            }

            return result.ToArray();
        }

        public List<string> GenerateCodes()
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                return Codes.GenerateCodes( Context.US_PSP, Resources.InflictStatusesBin, this.ToByteArray(), 0x32A394 );
            }
            else
            {
                return Codes.GenerateCodes( Context.US_PSX, FFTPatcher.Properties.PSXResources.InflictStatusesBin, this.ToByteArray(), 0x063FC4 );
            }
        }
    }
}
