using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public enum AbilityType
    {
        Blank,
        Normal,
        Item,
        Throwing,
        Jumping,
        Charging,
        Arithmetick,
        Reaction,
        Support,
        Movement,
        Unknown1,
        Unknown2,
        Unknown3,
        Unknown4,
        Unknown5,
        Unknown6
    }

    public class Ability
    {
        public string Name { get; private set; }
        public UInt16 Offset { get; private set; }
        public UInt16 JPCost { get; set; }
        public byte LearnRate { get; set; }

        public bool LearnWithJP; // inverted
        public bool Action;
        public bool LearnOnHit;
        public bool Blank1;

        public AbilityType AbilityType { get; set; }
        public AIFlags AIFlags { get; private set; }

        public bool Unknown1;
        public bool Unknown2;
        public bool Unknown3;
        public bool Blank2;
        public bool Blank3;
        public bool Blank4;
        public bool Blank5;
        public bool Unknown4;


        public bool HasSecond { get; private set; }

        public Ability(string name, UInt16 offset, SubArray<byte> first)
        {
            Name = name;
            Offset = offset;
            JPCost = Utilities.BytesToUShort(first[0], first[1]);
            LearnRate = first[2];

            bool dummy = false;
            Utilities.CopyByteToBooleans(first[3], 
                ref LearnWithJP, ref Action, ref LearnOnHit, ref Blank1, ref dummy, ref dummy, ref dummy, ref dummy);
            LearnWithJP = !LearnWithJP;

            AbilityType = (AbilityType)(first[3] & 0x0F);

            AIFlags = new AIFlags(new SubArray<byte>(first, 4, 6));

            Utilities.CopyByteToBooleans(first[7], 
                ref Unknown1, ref Unknown2, ref Unknown3, ref Blank2, ref Blank3, ref Blank4, ref Blank5, ref Unknown4);
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[8];
            result[0] = (byte)(JPCost & 0xFF);
            result[1] = (byte)(JPCost >> 8);
            result[2] = LearnRate;
            result[3] = Utilities.ByteFromBooleans(!LearnWithJP, Action, LearnOnHit, Blank1, false, false, false, false);
            result[3] |= (byte)AbilityType;
            Array.Copy(AIFlags.ToByteArray(), 0, result, 4, 3);
            result[7] = Utilities.ByteFromBooleans(Unknown1, Unknown2, Unknown3, Blank2, Blank3, Blank4, Blank5, Unknown4);

            return result;
        }

    }
}
