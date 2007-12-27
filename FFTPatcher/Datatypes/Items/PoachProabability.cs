using System;
using System.Collections.Generic;
using System.Text;
using FFTPatcher.Properties;

namespace FFTPatcher.Datatypes
{
    public class PoachProabability
    {
        public string MonsterName { get; private set; }
        public Item Common { get; set; }
        public Item Uncommon { get; set; }

        public PoachProabability( string name, SubArray<byte> bytes )
        {
            MonsterName = name;
            Common = Item.GetItemAtOffset( bytes[0] );
            Uncommon = Item.GetItemAtOffset( bytes[1] );
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[2];
            result[0] = (byte)(Common.Offset & 0xFF);
            result[1] = (byte)(Uncommon.Offset & 0xFF);
            return result;
        }
    }

    public class AllPoachProbabilities
    {
        public PoachProabability[] PoachProbabilities { get; private set; }

        public AllPoachProbabilities( SubArray<byte> bytes )
        {
            PoachProbabilities = new PoachProabability[48];
            for( int i = 0; i < 48; i++ )
            {
                PoachProbabilities[i] = new PoachProabability( AllJobs.Names[i + 0x5E], new SubArray<byte>( bytes, i * 2, i * 2 + 1 ) );
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 96 );
            foreach( PoachProabability p in PoachProbabilities )
            {
                result.AddRange( p.ToByteArray() );
            }

            return result.ToArray();
        }

        public byte[] ToByteArray( Context context )
        {
            return ToByteArray();
        }

        public string GenerateCodes()
        {
            return Utilities.GenerateCodes( Context.US_PSP, Resources.PoachProbabilitiesBin, this.ToByteArray(), 0x27AFD0 );
        }
    }
}
