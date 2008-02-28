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

using System;

namespace FFTPatcher.Datatypes
{
    /// <summary>
    /// Represents an <see cref="Ability"/>'s attributes.
    /// </summary>
    public class AbilityAttributes
    {
        public byte Range;
        public byte Effect;
        public byte Vertical;
        public AbilityFormula Formula { get; set; }
        public byte X;
        public byte Y;
        public byte InflictStatus;
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

        public AbilityAttributes Default { get; set; }

        public AbilityAttributes( string name, UInt16 offset, SubArray<byte> second, AbilityAttributes defaults )
            : this( name, offset, second )
        {
            Default = defaults;
        }

        public AbilityAttributes( string name, UInt16 offset, SubArray<byte> second )
        {
            Name = name;
            Offset = offset;

            Range = second[0];
            Effect = second[1];
            Vertical = second[2];
            Utilities.CopyByteToBooleans( second[3],
                ref Blank6, ref Blank7, ref WeaponRange, ref VerticalFixed, ref VerticalTolerance, ref WeaponStrike, ref Auto, ref TargetSelf );
            TargetSelf = !TargetSelf;
            Utilities.CopyByteToBooleans( second[4],
                ref HitEnemies, ref HitAllies, ref Blank8, ref FollowTarget, ref RandomFire, ref LinearAttack, ref ThreeDirections, ref HitCaster );
            HitEnemies = !HitEnemies;
            FollowTarget = !FollowTarget;
            HitAllies = !HitAllies;
            HitCaster = !HitCaster;
            Utilities.CopyByteToBooleans( second[5],
                ref Reflect, ref Arithmetick, ref Silence, ref Mimic, ref NormalAttack, ref Perservere, ref ShowQuote, ref Unknown5 );
            Silence = !Silence;
            Mimic = !Mimic;
            Utilities.CopyByteToBooleans( second[6],
                ref CounterFlood, ref CounterMagic, ref Direct, ref Shirahadori, ref RequiresSword, ref RequiresMateriaBlade, ref Evadeable, ref Targeting );
            Targeting = !Targeting;
            Elements = new Elements( second[7] );

            byte b = second[8];
            Formula = AbilityFormula.PSPAbilityFormulaHash[second[8]];
            X = second[9];
            Y = second[10];
            InflictStatus = second[11];
            CT = second[12];
            MPCost = second[13];
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[14];
            result[0] = Range;
            result[1] = Effect;
            result[2] = Vertical;
            result[3] = Utilities.ByteFromBooleans( Blank6, Blank7, WeaponRange, VerticalFixed, VerticalTolerance, WeaponStrike, Auto, !TargetSelf );
            result[4] = Utilities.ByteFromBooleans( !HitEnemies, !HitAllies, Blank8, !FollowTarget, RandomFire, LinearAttack, ThreeDirections, !HitCaster );
            result[5] = Utilities.ByteFromBooleans( Reflect, Arithmetick, !Silence, !Mimic, NormalAttack, Perservere, ShowQuote, Unknown5 );
            result[6] = Utilities.ByteFromBooleans( CounterFlood, CounterMagic, Direct, Shirahadori, RequiresSword, RequiresMateriaBlade, Evadeable, !Targeting );
            result[7] = Elements.ToByte();
            result[8] = Formula.Value;
            result[9] = X;
            result[10] = Y;
            result[11] = InflictStatus;
            result[12] = CT;
            result[13] = MPCost;

            return result;
        }

        public bool[] ToBoolArray()
        {
            return new bool[32] { 
                Blank6, Blank7, WeaponRange, VerticalFixed, VerticalTolerance, WeaponStrike, Auto, TargetSelf,
                HitEnemies, HitAllies, Blank8, FollowTarget, RandomFire, LinearAttack, ThreeDirections, HitCaster,
                Reflect, Arithmetick, Silence, Mimic, NormalAttack, Perservere, ShowQuote, Unknown5,
                CounterFlood, CounterMagic, Direct, Shirahadori, RequiresSword, RequiresMateriaBlade,Evadeable, Targeting };
        }
    }
}
