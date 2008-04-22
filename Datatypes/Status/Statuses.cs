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
    public class Statuses : ISupportDigest
    {

        #region Static Fields (1)

        public static string[] FieldNames = new string[] {
            "NoEffect","Crystal","Dead","Undead","Charging","Jump","Defending","Performing",
            "Petrify","Invite","Darkness","Confusion","Silence","BloodSuck","DarkEvilLooking","Treasure",
            "Oil","Float","Reraise","Transparent","Berserk","Chicken","Frog","Critical",
            "Poison","Regen","Protect","Shell","Haste","Slow","Stop","Wall",
            "Faith","Innocent","Charm","Sleep","DontMove","DontAct","Reflect","DeathSentence" };

        #endregion Static Fields

        #region Fields (40)

        public bool Berserk;
        public bool BloodSuck;
        public bool Charging;
        public bool Charm;
        public bool Chicken;
        public bool Confusion;
        public bool Critical;
        public bool Crystal;
        public bool DarkEvilLooking;
        public bool Darkness;
        public bool Dead;
        public bool DeathSentence;
        public bool Defending;
        public bool DontAct;
        public bool DontMove;
        public bool Faith;
        public bool Float;
        public bool Frog;
        public bool Haste;
        public bool Innocent;
        public bool Invite;
        public bool Jump;
        public bool NoEffect;
        public bool Oil;
        public bool Performing;
        public bool Petrify;
        public bool Poison;
        public bool Protect;
        public bool Reflect;
        public bool Regen;
        public bool Reraise;
        public bool Shell;
        public bool Silence;
        public bool Sleep;
        public bool Slow;
        public bool Stop;
        public bool Transparent;
        public bool Treasure;
        public bool Undead;
        public bool Wall;

        #endregion Fields

        #region Properties (3)


        public Statuses Default { get; private set; }

        public IList<string> DigestableProperties
        {
            get { return FieldNames }
        }

        public bool HasChanged
        {
            get { return Default != null && !Utilities.CompareArrays( ToBoolArray(), Default.ToBoolArray() ); }
        }


        #endregion Properties

        #region Constructors (2)

        public Statuses( IList<byte> bytes )
        {
            Utilities.CopyByteToBooleans( bytes[0], ref NoEffect, ref Crystal, ref Dead, ref Undead, ref Charging, ref Jump, ref Defending, ref Performing );
            Utilities.CopyByteToBooleans( bytes[1], ref Petrify, ref Invite, ref Darkness, ref Confusion, ref Silence, ref BloodSuck, ref DarkEvilLooking, ref Treasure );
            Utilities.CopyByteToBooleans( bytes[2], ref Oil, ref Float, ref Reraise, ref Transparent, ref Berserk, ref Chicken, ref Frog, ref Critical );
            Utilities.CopyByteToBooleans( bytes[3], ref Poison, ref Regen, ref Protect, ref Shell, ref Haste, ref Slow, ref Stop, ref Wall );
            Utilities.CopyByteToBooleans( bytes[4], ref Faith, ref Innocent, ref Charm, ref Sleep, ref DontMove, ref DontAct, ref Reflect, ref DeathSentence );
        }

        public Statuses( IList<byte> bytes, Statuses defaults )
            : this( bytes )
        {
            Default = defaults;
        }

        #endregion Constructors

        #region Methods (2)


        public bool[] ToBoolArray()
        {
            return new bool[40] { 
                NoEffect, Crystal, Dead, Undead, Charging, Jump, Defending, Performing,
                Petrify, Invite, Darkness, Confusion, Silence, BloodSuck, DarkEvilLooking, Treasure,
                Oil, Float, Reraise, Transparent, Berserk, Chicken, Frog, Critical,
                Poison, Regen, Protect, Shell, Haste, Slow, Stop, Wall,
                Faith, Innocent, Charm, Sleep, DontMove, DontAct, Reflect, DeathSentence };
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[5];
            result[0] = Utilities.ByteFromBooleans( NoEffect, Crystal, Dead, Undead, Charging, Jump, Defending, Performing );
            result[1] = Utilities.ByteFromBooleans( Petrify, Invite, Darkness, Confusion, Silence, BloodSuck, DarkEvilLooking, Treasure );
            result[2] = Utilities.ByteFromBooleans( Oil, Float, Reraise, Transparent, Berserk, Chicken, Frog, Critical );
            result[3] = Utilities.ByteFromBooleans( Poison, Regen, Protect, Shell, Haste, Slow, Stop, Wall );
            result[4] = Utilities.ByteFromBooleans( Faith, Innocent, Charm, Sleep, DontMove, DontAct, Reflect, DeathSentence );
            return result;
        }

        public override string ToString()
        {
            List<string> strings = new List<string>( 40 );
            foreach( string name in FieldNames )
            {
                if( ReflectionHelpers.GetFieldOrProperty<bool>( this, name ) )
                {
                    strings.Add( name );
                }
            }

            return string.Join( " | ", strings.ToArray() );
        }


        #endregion Methods

    }
}
