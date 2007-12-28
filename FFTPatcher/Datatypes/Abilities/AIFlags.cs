namespace FFTPatcher.Datatypes
{
    public class AIFlags
    {
        public bool HP;
        public bool MP;
        public bool CancelStatus;
        public bool AddStatus;
        public bool Stats;
        public bool Unequip;
        public bool TargetEnemies;
        public bool TargetAllies;

        public bool IgnoreRange;
        public bool Reflectable;
        public bool UndeadReverse;
        public bool Unknown1;
        public bool RandomHits;
        public bool Unknown2;
        public bool Unknown3;
        public bool Silence; // inverted

        public bool Blank;
        public bool DirectAttack;
        public bool LineAttack;
        public bool VerticalIncrease; // inverted
        public bool TripleAttack;
        public bool TripleBracelet;
        public bool MagicDefenseUp;
        public bool DefenseUp;

        public AIFlags( SubArray<byte> bytes )
        {
            Utilities.CopyByteToBooleans( bytes[0],
                ref HP, ref MP, ref CancelStatus, ref AddStatus, ref Stats, ref Unequip, ref TargetEnemies, ref TargetAllies );

            Utilities.CopyByteToBooleans( bytes[1],
                ref IgnoreRange, ref Reflectable, ref UndeadReverse, ref Unknown1, ref RandomHits, ref Unknown2, ref Unknown3, ref Silence );
            Silence = !Silence;

            Utilities.CopyByteToBooleans( bytes[2],
                ref Blank, ref DirectAttack, ref LineAttack, ref VerticalIncrease, ref TripleAttack, ref TripleBracelet, ref MagicDefenseUp, ref DefenseUp );
            VerticalIncrease = !VerticalIncrease;
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[3];
            result[0] = Utilities.ByteFromBooleans( HP, MP, CancelStatus, AddStatus, Stats, Unequip, TargetEnemies, TargetAllies );
            result[1] = Utilities.ByteFromBooleans( IgnoreRange, Reflectable, UndeadReverse, Unknown1, RandomHits, Unknown2, Unknown3, !Silence );
            result[2] = Utilities.ByteFromBooleans( Blank, DirectAttack, LineAttack, !VerticalIncrease, TripleAttack, TripleBracelet, MagicDefenseUp, DefenseUp );
            return result;
        }
    }
}
