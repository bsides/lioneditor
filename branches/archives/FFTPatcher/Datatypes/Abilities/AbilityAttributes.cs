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
using System.Collections.Generic;

namespace FFTPatcher.Datatypes
{
    /// <summary>
    /// Represents an <see cref="Ability"/>'s attributes.
    /// </summary>
    public class AbilityAttributes : IChangeable, ISupportDigest
    {

        #region Static Fields (1)

        private static readonly string[] valuesToSerialize = new string[] {
            "AnimateMiss","Arithmetick","Auto","Blank6","Blank7","Blank8","CounterFlood","CounterMagic",
            "CT","Direct","Effect","Evadeable","FollowTarget","HitAllies","HitCaster","HitEnemies",
            "InflictStatus","LinearAttack","Mimic","MPCost","NormalAttack","Perservere","RandomFire",
            "Range","Reflect","RequiresMateriaBlade","RequiresSword","Shirahadori","ShowQuote","Silence",
            "Targeting","TargetSelf","ThreeDirections","Vertical","VerticalFixed","VerticalTolerance",
            "WeaponRange","WeaponStrike","X","Y", "Elements", "Formula" };

        #endregion Static Fields

        #region Fields (41)

        public bool AnimateMiss;
        public bool Arithmetick;
        public bool Auto;
        public bool Blank6;
        public bool Blank7;
        public bool Blank8;
        public bool CounterFlood;
        public bool CounterMagic;
        public byte CT;
        private AbilityAttributes defaults;
        public bool Direct;
        public byte Effect;
        public bool Evadeable;
        public bool FollowTarget;
        public bool HitAllies;
        public bool HitCaster;
        public bool HitEnemies;
        [Hex]
        public byte InflictStatus;
        public bool LinearAttack;
        public bool Mimic;
        public byte MPCost;
        public bool NormalAttack;
        public bool Perservere;
        public bool RandomFire;
        public byte Range;
        public bool Reflect;
        public bool RequiresMateriaBlade;
        public bool RequiresSword;
        public bool Shirahadori;
        public bool ShowQuote;
        public bool Silence;
        public bool Targeting;
        public bool TargetSelf;
        public bool ThreeDirections;
        public byte Vertical;
        public bool VerticalFixed;
        public bool VerticalTolerance;
        public bool WeaponRange;
        public bool WeaponStrike;
        public byte X;
        public byte Y;

        #endregion Fields

        #region Properties (7)


        public AbilityAttributes Default
        {
            get { return defaults; }
            set
            {
                defaults = value;
                if( defaults != null )
                {
                    Elements.Default = defaults.Elements;
                }
            }
        }

        public IList<string> DigestableProperties
        {
            get { return valuesToSerialize; }
        }

        public Elements Elements { get; private set; }

        public AbilityFormula Formula { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has changed.
        /// </summary>
        public bool HasChanged
        {
            get
            {
                return Default != null && !Utilities.CompareArrays( ToByteArray(), Default.ToByteArray() );
            }
        }

        public string Name { get; private set; }

        public UInt16 Offset { get; private set; }


        #endregion Properties

        #region Constructors (2)

        public AbilityAttributes( string name, UInt16 offset, IList<byte> second )
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
                ref Reflect, ref Arithmetick, ref Silence, ref Mimic, ref NormalAttack, ref Perservere, ref ShowQuote, ref AnimateMiss );
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

        public AbilityAttributes( string name, UInt16 offset, IList<byte> second, AbilityAttributes defaults )
            : this( name, offset, second )
        {
            if( defaults != null )
            {
                Default = defaults;
                Elements.Default = defaults.Elements;
            }
        }

        #endregion Constructors

        #region Methods (2)


        public bool[] ToBoolArray()
        {
            return new bool[32] { 
                Blank6, Blank7, WeaponRange, VerticalFixed, VerticalTolerance, WeaponStrike, Auto, TargetSelf,
                HitEnemies, HitAllies, Blank8, FollowTarget, RandomFire, LinearAttack, ThreeDirections, HitCaster,
                Reflect, Arithmetick, Silence, Mimic, NormalAttack, Perservere, ShowQuote, AnimateMiss,
                CounterFlood, CounterMagic, Direct, Shirahadori, RequiresSword, RequiresMateriaBlade,Evadeable, Targeting };
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[14];
            result[0] = Range;
            result[1] = Effect;
            result[2] = Vertical;
            result[3] = Utilities.ByteFromBooleans( Blank6, Blank7, WeaponRange, VerticalFixed, VerticalTolerance, WeaponStrike, Auto, !TargetSelf );
            result[4] = Utilities.ByteFromBooleans( !HitEnemies, !HitAllies, Blank8, !FollowTarget, RandomFire, LinearAttack, ThreeDirections, !HitCaster );
            result[5] = Utilities.ByteFromBooleans( Reflect, Arithmetick, !Silence, !Mimic, NormalAttack, Perservere, ShowQuote, AnimateMiss );
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

        public static void Copy( AbilityAttributes source, AbilityAttributes destination )
        {
            destination.Range = source.Range;
            destination.Effect = source.Effect;
            destination.Vertical = source.Vertical;
            source.Elements.CopyTo( destination.Elements );
            destination.Formula = source.Formula;
            destination.X = source.X;
            destination.Y = source.Y;
            destination.InflictStatus = source.InflictStatus;
            destination.CT = source.CT;
            destination.MPCost = source.MPCost;
            destination.Blank6 = source.Blank6;
            destination.Blank7 = source.Blank7;
            destination.WeaponRange = source.WeaponRange;
            destination.VerticalFixed = source.VerticalFixed;
            destination.VerticalTolerance = source.VerticalTolerance;
            destination.WeaponStrike = source.WeaponStrike;
            destination.Auto = source.Auto;
            destination.TargetSelf = source.TargetSelf;

            destination.HitEnemies = source.HitEnemies;
            destination.HitAllies = source.HitAllies;
            destination.Blank8 = source.Blank8;
            destination.FollowTarget = source.FollowTarget;
            destination.RandomFire = source.RandomFire;
            destination.LinearAttack = source.LinearAttack;
            destination.ThreeDirections = source.ThreeDirections;
            destination.HitCaster = source.HitCaster;

            destination.Reflect = source.Reflect;
            destination.Arithmetick = source.Arithmetick;
            destination.Silence = source.Silence;
            destination.Mimic = source.Mimic;
            destination.NormalAttack = source.NormalAttack;
            destination.Perservere = source.Perservere;
            destination.ShowQuote = source.ShowQuote;
            destination.AnimateMiss = source.AnimateMiss;

            destination.CounterFlood = source.CounterFlood;
            destination.CounterMagic = source.CounterMagic;
            destination.Direct = source.Direct;
            destination.Shirahadori = source.Shirahadori;
            destination.RequiresSword = source.RequiresSword;
            destination.RequiresMateriaBlade = source.RequiresMateriaBlade;
            destination.Evadeable = source.Evadeable;
            destination.Targeting = source.Targeting;
        }

        public void CopyTo( AbilityAttributes destination )
        {
            Copy( this, destination );
        }

        #endregion Methods

    }
}
