using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public class AbilityAttributes
    {
        public byte Range;
        public byte Effect;
        public byte Vertical;
        public byte Formula;
        public byte X;
        public byte Y;
        public byte StatusMagic;
        public byte CT;
        public byte MPCost;

        public bool Blank6;
        public bool Blank7;
        public bool WeaponRange;
        public bool VerticalFixed;
        public bool VerticalTolerance;
        public bool WeaponStrike;
        public bool Auto;
        public bool TargetSelf; // inverted

        public bool HitEnemies; // inverted
        public bool HitAllies; // inverted
        public bool Blank8;
        public bool FollowTarget; // inverted
        public bool RandomFire;
        public bool LinearAttack;
        public bool ThreeDirections;
        public bool HitCaster; // inerted

        public bool Reflect;
        public bool Arithmetick;
        public bool Silence; // inverted
        public bool Mimic; // inverted
        public bool NormalAttack;
        public bool Perservere;
        public bool ShowQuote;
        public bool Unknown5;

        public bool CounterFlood;
        public bool CounterMagic;
        public bool Direct;
        public bool Shirahadori;
        public bool RequiresSword;
        public bool RequiresMateriaBlade;
        public bool Evadeable;
        public bool Targeting; // inverted

        public Elements Elements { get; private set; }

        public string Name { get; private set; }
        public UInt16 Offset { get; private set; }

        public AbilityAttributes(string name, UInt16 offset, SubArray<byte> second)
        {
            Name = name;
            Offset = offset;

            Range = second[0];
            Effect = second[1];
            Vertical = second[2];
            Utilities.CopyByteToBooleans(second[3],
                ref Blank6, ref Blank7, ref WeaponRange, ref VerticalFixed, ref VerticalTolerance, ref WeaponStrike, ref Auto, ref TargetSelf);
            TargetSelf = !TargetSelf;
            Utilities.CopyByteToBooleans(second[4],
                ref HitEnemies, ref HitAllies, ref Blank8, ref FollowTarget, ref RandomFire, ref LinearAttack, ref ThreeDirections, ref HitCaster);
            HitEnemies = !HitEnemies;
            FollowTarget = !FollowTarget;
            HitAllies = !HitAllies;
            HitCaster = !HitCaster;
            Utilities.CopyByteToBooleans(second[5],
                ref Reflect, ref Arithmetick, ref Silence, ref Mimic, ref NormalAttack, ref Perservere, ref ShowQuote, ref Unknown5);
            Silence = !Silence;
            Mimic = !Mimic;
            Utilities.CopyByteToBooleans(second[6],
                ref CounterFlood, ref CounterMagic, ref Direct, ref Shirahadori, ref RequiresSword, ref RequiresMateriaBlade, ref Evadeable, ref Targeting);
            Targeting = !Targeting;
            Elements = new Elements(second[7]);

            Formula = second[8];
            X = second[9];
            Y = second[10];
            StatusMagic = second[11];
            CT = second[12];
            MPCost = second[13];
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[14];
            result[0] = Range;
            result[1] = Effect;
            result[2] = Vertical;
            result[3] = Utilities.ByteFromBooleans(Blank6, Blank7, WeaponRange, VerticalFixed, VerticalTolerance, WeaponStrike, Auto, !TargetSelf);
            result[4] = Utilities.ByteFromBooleans(!HitEnemies, !HitAllies, Blank8, !FollowTarget, RandomFire, LinearAttack, ThreeDirections, !HitCaster);
            result[5] = Utilities.ByteFromBooleans(Reflect, Arithmetick, !Silence, !Mimic, NormalAttack, Perservere, ShowQuote, Unknown5);
            result[6] = Utilities.ByteFromBooleans(CounterFlood, CounterMagic, Direct, Shirahadori, RequiresSword, RequiresMateriaBlade, Evadeable, !Targeting);
            result[7] = Elements.ToByte();
            result[8] = Formula;
            result[9] = X;
            result[10] = Y;
            result[11] = StatusMagic;
            result[12] = CT;
            result[13] = MPCost;

            return result;
        }
    }
}
