using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher.Datatypes
{
    public class Statuses
    {
        public bool NoEffect;
        public bool Crystal;
        public bool Dead;	
        public bool Undead;
        public bool Charging;
        public bool Jump;
        public bool Defending;
        public bool Performing;
        public bool Petrify;
        public bool Invite;
        public bool Darkness;
        public bool Confusion;
        public bool Silence;
        public bool BloodSuck;
        public bool DarkEvilLooking;
        public bool Treasure;
        public bool Oil;
        public bool Float;
        public bool Reraise;
        public bool Transparent;
        public bool Berserk;
        public bool Chicken;
        public bool Frog;
        public bool Critical;
        public bool Poison;
        public bool Regen;
        public bool Protect;
        public bool Shell;
        public bool Haste;
        public bool Slow;
        public bool Stop;
        public bool Wall;
        public bool Faith;
        public bool Innocent;
        public bool Charm;
        public bool Sleep;
        public bool DontMove;
        public bool DontAct;
        public bool Reflect;
        public bool DeathSentence;

        public Statuses(SubArray<byte> bytes)
        {
            Utilities.CopyByteToBooleans(bytes[0], ref NoEffect, ref Crystal, ref Dead, ref Undead, ref Charging, ref Jump, ref Defending, ref Performing);
            Utilities.CopyByteToBooleans(bytes[1], ref Petrify, ref Invite, ref Darkness, ref Confusion, ref Silence, ref BloodSuck, ref DarkEvilLooking, ref Treasure);
            Utilities.CopyByteToBooleans(bytes[2], ref Oil, ref Float, ref Reraise, ref Transparent, ref Berserk, ref Chicken, ref Frog, ref Critical);
            Utilities.CopyByteToBooleans(bytes[3], ref Poison, ref Regen, ref Protect, ref Shell, ref Haste, ref Slow, ref Stop, ref Wall);
            Utilities.CopyByteToBooleans(bytes[4], ref Faith, ref Innocent, ref Charm, ref Sleep, ref DontMove, ref DontAct, ref Reflect, ref DeathSentence);
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[5];
            result[0] = Utilities.ByteFromBooleans(NoEffect, Crystal, Dead, Undead, Charging, Jump, Defending, Performing);
            result[1] = Utilities.ByteFromBooleans(Petrify, Invite, Darkness, Confusion, Silence, BloodSuck, DarkEvilLooking, Treasure);
            result[2] = Utilities.ByteFromBooleans(Oil, Float, Reraise, Transparent, Berserk, Chicken, Frog, Critical);
            result[3] = Utilities.ByteFromBooleans(Poison, Regen, Protect, Shell, Haste, Slow, Stop, Wall);
            result[4] = Utilities.ByteFromBooleans(Faith, Innocent, Charm, Sleep, DontMove, DontAct, Reflect, DeathSentence);
            return result;
        }
    }
}
