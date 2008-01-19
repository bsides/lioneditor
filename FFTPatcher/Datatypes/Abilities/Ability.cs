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

    /// <summary>
    /// Represents an ability and its attributes.
    /// </summary>
    public class Ability
    {
        #region Fields

        private bool learnWithJP; // inverted
        private bool action;
        private bool learnOnHit;
        private bool blank1;

        private bool unknown1;
        private bool unknown2;
        private bool unknown3;
        private bool blank2;
        private bool blank3;
        private bool blank4;
        private bool blank5;
        private bool unknown4;

        #endregion

        #region Properties

        public Ability Default { get; private set; }

        #region Common

        public string Name { get; private set; }
        public UInt16 Offset { get; private set; }
        public UInt16 JPCost { get; set; }
        public byte LearnRate { get; set; }

        public bool LearnWithJP { get { return learnWithJP; } set { learnWithJP = value; } }
        public bool Action { get { return action; } set { action = value; } }
        public bool LearnOnHit { get { return learnOnHit; } set { learnOnHit = value; } }
        public bool Blank1 { get { return blank1; } set { blank1 = value; } }

        public AbilityType AbilityType { get; set; }
        public AIFlags AIFlags { get; private set; }

        #region AI flags

        public bool AIHP { get { return AIFlags.HP; } set { AIFlags.HP = value; } }
        public bool AIMP { get { return AIFlags.MP; } set { AIFlags.MP = value; } }
        public bool AICancelStatus { get { return AIFlags.CancelStatus; } set { AIFlags.CancelStatus = value; } }
        public bool AIAddStatus { get { return AIFlags.AddStatus; } set { AIFlags.AddStatus = value; } }
        public bool AIStats { get { return AIFlags.Stats; } set { AIFlags.Stats = value; } }
        public bool AIUnequip { get { return AIFlags.Unequip; } set { AIFlags.Unequip = value; } }
        public bool AITargetEnemies { get { return AIFlags.TargetEnemies; } set { AIFlags.TargetEnemies = value; } }
        public bool AITargetAllies { get { return AIFlags.TargetAllies; } set { AIFlags.TargetAllies = value; } }

        public bool AIIgnoreRange { get { return AIFlags.IgnoreRange; } set { AIFlags.IgnoreRange = value; } }
        public bool AIReflectable { get { return AIFlags.Reflectable; } set { AIFlags.Reflectable = value; } }
        public bool AIUndeadReverse { get { return AIFlags.UndeadReverse; } set { AIFlags.UndeadReverse = value; } }
        public bool AIUnknown1 { get { return AIFlags.Unknown1; } set { AIFlags.Unknown1 = value; } }
        public bool AIRandomHits { get { return AIFlags.RandomHits; } set { AIFlags.RandomHits = value; } }
        public bool AIUnknown2 { get { return AIFlags.Unknown2; } set { AIFlags.Unknown2 = value; } }
        public bool AIUnknown3 { get { return AIFlags.Unknown3; } set { AIFlags.Unknown3 = value; } }
        public bool AISilence { get { return AIFlags.Silence; } set { AIFlags.Silence = value; } }

        public bool AIBlank { get { return AIFlags.Blank; } set { AIFlags.Blank = value; } }
        public bool AIDirectAttack { get { return AIFlags.DirectAttack; } set { AIFlags.DirectAttack = value; } }
        public bool AILineAttack { get { return AIFlags.LineAttack; } set { AIFlags.LineAttack = value; } }
        public bool AIVerticalIncrease { get { return AIFlags.VerticalIncrease; } set { AIFlags.VerticalIncrease = value; } }
        public bool AITripleAttack { get { return AIFlags.TripleAttack; } set { AIFlags.TripleAttack = value; } }
        public bool AITripleBracelet { get { return AIFlags.TripleBracelet; } set { AIFlags.TripleBracelet = value; } }
        public bool AIMagicDefenseUp { get { return AIFlags.MagicDefenseUp; } set { AIFlags.MagicDefenseUp = value; } }
        public bool AIDefenseUp { get { return AIFlags.DefenseUp; } set { AIFlags.DefenseUp = value; } }

        #endregion

        public bool Unknown1 { get { return unknown1; } set { unknown1 = value; } }
        public bool Unknown2 { get { return unknown2; } set { unknown2 = value; } }
        public bool Unknown3 { get { return unknown3; } set { unknown3 = value; } }
        public bool Blank2 { get { return blank2; } set { blank2 = value; } }
        public bool Blank3 { get { return blank3; } set { blank3 = value; } }
        public bool Blank4 { get { return blank4; } set { blank4 = value; } }
        public bool Blank5 { get { return blank5; } set { blank5 = value; } }
        public bool Unknown4 { get { return unknown4; } set { unknown4 = value; } }

        #endregion

        #region Specialty

        #region Normal

        public bool IsNormal { get; private set; }
        public AbilityAttributes Attributes { get; private set; }

        #endregion Normal

        #region Item

        public bool IsItem { get; private set; }
        public UInt16 ItemOffset
        {
            get 
            {
                return (UInt16)(IsItem ? Item.Offset : 0);
            }
            set { Item = Item.GetItemAtOffset( value ); }
        }
        public Item Item { get; set; }

        #endregion Item

        #region Throwing

        public bool IsThrowing { get; private set; }
        public ItemSubType Throwing { get; set; }

        #endregion Throwing

        #region Jumping

        public bool IsJumping { get; private set; }
        public byte JumpHorizontal { get; set; }
        public byte JumpVertical { get; set; }

        #endregion Jumping

        #region Charging

        public bool IsCharging { get; private set; }
        public byte ChargeCT { get; set; }
        public byte ChargeBonus { get; set; }

        #endregion Charging

        #region Arithmeticks

        public bool IsArithmetick { get; private set; }
        public byte ArithmetickSkill { get; set; }

        #endregion Arithmeticks

        #region Other

        public bool IsOther { get; private set; }
        public byte OtherID { get; set; }

        #endregion Other

        #endregion Specialty

        #endregion

        public Ability( string name, UInt16 offset )
        {
            Name = name;
            Offset = offset;
        }

        private Ability( string name, UInt16 offset, SubArray<byte> first )
        {
            Name = name;
            Offset = offset;
            JPCost = Utilities.BytesToUShort( first[0], first[1] );
            LearnRate = first[2];

            bool dummy = false;
            Utilities.CopyByteToBooleans( first[3],
                ref learnWithJP, ref action, ref learnOnHit, ref blank1, ref dummy, ref dummy, ref dummy, ref dummy );
            learnWithJP = !learnWithJP;

            AbilityType = (AbilityType)(first[3] & 0x0F);

            AIFlags = new AIFlags( new SubArray<byte>( first, 4, 6 ) );

            Utilities.CopyByteToBooleans( first[7],
                ref unknown1, ref unknown2, ref unknown3, ref blank2, ref blank3, ref blank4, ref blank5, ref unknown4 );
        }

        public Ability( string name, UInt16 offset, SubArray<byte> first, SubArray<byte> second, Ability defaults )
            : this( name, offset, first, second )
        {
            Default = defaults;
            if( IsNormal )
            {
                Attributes.Default = Default.Attributes;
            }
        }

        public Ability( string name, UInt16 offset, SubArray<byte> first, SubArray<byte> second )
            : this( name, offset, first )
        {
            IsNormal = ((offset >= 0x000) && (offset <= 0x16F));
            IsItem = ((offset >= 0x170) && (offset <= 0x17D));
            IsThrowing = ((offset >= 0x17E) && (offset <= 0x189));
            IsJumping = ((offset >= 0x18A) && (offset <= 0x195));
            IsCharging = ((offset >= 0x196) && (offset <= 0x19D));
            IsArithmetick = ((offset >= 0x19E) && (offset <= 0x1A5));
            IsOther = (offset >= 0x1A6);

            if( IsNormal )
            {
                Attributes = new AbilityAttributes( name, offset, second );
            }
            if( IsItem )
            {
                ItemOffset = second[0];
            }
            if( IsThrowing )
            {
                Throwing = (ItemSubType)second[0];
            }
            if( IsJumping )
            {
                JumpHorizontal = second[0];
                JumpVertical = second[1];
            }
            if( IsCharging )
            {
                ChargeCT = second[0];
                ChargeBonus = second[1];
            }
            if( IsArithmetick )
            {
                ArithmetickSkill = second[0];
            }
            if( IsOther )
            {
                OtherID = second[0];
            }
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[8];
            result[0] = (byte)(JPCost & 0xFF);
            result[1] = (byte)(JPCost >> 8);
            result[2] = LearnRate;
            result[3] = Utilities.ByteFromBooleans( !learnWithJP, action, learnOnHit, blank1, false, false, false, false );
            result[3] |= (byte)AbilityType;
            Array.Copy( AIFlags.ToByteArray(), 0, result, 4, 3 );
            result[7] = Utilities.ByteFromBooleans( unknown1, unknown2, unknown3, blank2, blank3, blank4, blank5, unknown4 );

            return result;
        }

        public byte[] ToByteArray( Context context )
        {
            switch( context )
            {
                case Context.US_PSX:
                    return ToByteArray();
                default:
                    return ToByteArray();
            }
        }

        public byte[] ToSecondByteArray( Context context )
        {
            switch( context )
            {
                case Context.US_PSX:
                    return ToSecondByteArray();
                default:
                    return ToSecondByteArray();
            }
        }

        public byte[] ToSecondByteArray()
        {
            if( IsNormal )
                return Attributes.ToByteArray();
            if( IsItem )
                return new byte[] { (byte)(Item.Offset & 0xFF) };
            if( IsThrowing )
                return new byte[] { (byte)Throwing };
            if( IsJumping )
                return new byte[] { JumpHorizontal, JumpVertical };
            if( IsCharging )
                return new byte[] { ChargeCT, ChargeBonus };
            if( IsArithmetick )
                return new byte[] { ArithmetickSkill };
            if( IsOther )
                return new byte[] { OtherID };
            return null;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool[] PropertiesToBoolArray()
        {
            return new bool[12] {
                LearnWithJP, Action, LearnOnHit, Blank1,
                Unknown1, Unknown2, Unknown3, Blank2, Blank3, Blank4, Blank5, Unknown4 };
        }
    }
}
