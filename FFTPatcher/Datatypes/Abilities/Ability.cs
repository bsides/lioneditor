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
using System.Xml;

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

        [PSXDescription( "Mathematics" )]
        [PSPDescription( "Arithmetick" )]
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
    public class Ability : IChangeable, IXmlDigest, ISupportDigest
    {

        #region Static Fields (1)

        private static readonly string[] digestableProperties = new string[] {
            "JPCost", "LearnRate", "AbilityType", "LearnWithJP", "Action",
            "LearnOnHit", "Blank1", "Unknown1", "Unknown2", "Unknown3",
            "Blank2", "Blank3", "Blank4", "Blank5", "Unknown4"};

        #endregion Static Fields

        #region Fields (12)

        private bool action;
        private bool blank1;
        private bool blank2;
        private bool blank3;
        private bool blank4;
        private bool blank5;
        private bool learnOnHit;
        private bool learnWithJP;
        private bool unknown1;
        private bool unknown2;
        private bool unknown3;
        private bool unknown4;

        #endregion Fields

        #region Properties (63)


        public AbilityType AbilityType { get; set; }

        public bool Action { get { return action; } set { action = value; } }

        public bool AIAddStatus { get { return AIFlags.AddStatus; } set { AIFlags.AddStatus = value; } }

        public bool AIBlank { get { return AIFlags.Blank; } set { AIFlags.Blank = value; } }

        public bool AICancelStatus { get { return AIFlags.CancelStatus; } set { AIFlags.CancelStatus = value; } }

        public bool AIDefenseUp { get { return AIFlags.DefenseUp; } set { AIFlags.DefenseUp = value; } }

        public bool AIDirectAttack { get { return AIFlags.DirectAttack; } set { AIFlags.DirectAttack = value; } }

        public AIFlags AIFlags { get; private set; }

        public bool AIHP { get { return AIFlags.HP; } set { AIFlags.HP = value; } }

        public bool AIIgnoreRange { get { return AIFlags.IgnoreRange; } set { AIFlags.IgnoreRange = value; } }

        public bool AILineAttack { get { return AIFlags.LineAttack; } set { AIFlags.LineAttack = value; } }

        public bool AIMagicDefenseUp { get { return AIFlags.MagicDefenseUp; } set { AIFlags.MagicDefenseUp = value; } }

        public bool AIMP { get { return AIFlags.MP; } set { AIFlags.MP = value; } }

        public bool AIRandomHits { get { return AIFlags.RandomHits; } set { AIFlags.RandomHits = value; } }

        public bool AIReflectable { get { return AIFlags.Reflectable; } set { AIFlags.Reflectable = value; } }

        public bool AISilence { get { return AIFlags.Silence; } set { AIFlags.Silence = value; } }

        public bool AIStats { get { return AIFlags.Stats; } set { AIFlags.Stats = value; } }

        public bool AITargetAllies { get { return AIFlags.TargetAllies; } set { AIFlags.TargetAllies = value; } }

        public bool AITargetEnemies { get { return AIFlags.TargetEnemies; } set { AIFlags.TargetEnemies = value; } }

        public bool AITripleAttack { get { return AIFlags.TripleAttack; } set { AIFlags.TripleAttack = value; } }

        public bool AITripleBracelet { get { return AIFlags.TripleBracelet; } set { AIFlags.TripleBracelet = value; } }

        public bool AIUndeadReverse { get { return AIFlags.UndeadReverse; } set { AIFlags.UndeadReverse = value; } }

        public bool AIUnequip { get { return AIFlags.Unequip; } set { AIFlags.Unequip = value; } }

        public bool AIUnknown1 { get { return AIFlags.Unknown1; } set { AIFlags.Unknown1 = value; } }

        public bool AIUnknown2 { get { return AIFlags.Unknown2; } set { AIFlags.Unknown2 = value; } }

        public bool AIUnknown3 { get { return AIFlags.Unknown3; } set { AIFlags.Unknown3 = value; } }

        public bool AIVerticalIncrease { get { return AIFlags.VerticalIncrease; } set { AIFlags.VerticalIncrease = value; } }

        [Hex]
        public byte ArithmetickSkill { get; set; }

        public AbilityAttributes Attributes { get; private set; }

        public bool Blank1 { get { return blank1; } set { blank1 = value; } }

        public bool Blank2 { get { return blank2; } set { blank2 = value; } }

        public bool Blank3 { get { return blank3; } set { blank3 = value; } }

        public bool Blank4 { get { return blank4; } set { blank4 = value; } }

        public bool Blank5 { get { return blank5; } set { blank5 = value; } }

        public byte ChargeBonus { get; set; }

        public byte ChargeCT { get; set; }

        public Ability Default { get; private set; }

        public IList<string> DigestableProperties
        {
            get { return digestableProperties; }
        }

        public Effect Effect { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has changed.
        /// </summary>
        public bool HasChanged
        {
            get
            {
                return
                    (Default != null) &&
                    (AbilityType != Default.AbilityType ||
                    Action != Default.Action ||
                    AIAddStatus != Default.AIAddStatus ||
                    AIBlank != Default.AIBlank ||
                    AICancelStatus != Default.AICancelStatus ||
                    AIDefenseUp != Default.AIDefenseUp ||
                    AIDirectAttack != Default.AIDirectAttack ||
                    AIHP != Default.AIHP ||
                    AIIgnoreRange != Default.AIIgnoreRange ||
                    AILineAttack != Default.AILineAttack ||
                    AIMagicDefenseUp != Default.AIMagicDefenseUp ||
                    AIMP != Default.AIMP ||
                    AIRandomHits != Default.AIRandomHits ||
                    AIReflectable != Default.AIReflectable ||
                    AISilence != Default.AISilence ||
                    AIStats != Default.AIStats ||
                    AITargetAllies != Default.AITargetAllies ||
                    AITargetEnemies != Default.AITargetEnemies ||
                    AITripleAttack != Default.AITripleAttack ||
                    AITripleBracelet != Default.AITripleBracelet ||
                    AIUndeadReverse != Default.AIUndeadReverse ||
                    AIUnequip != Default.AIUnequip ||
                    AIUnknown1 != Default.AIUnknown1 ||
                    AIUnknown2 != Default.AIUnknown2 ||
                    AIUnknown3 != Default.AIUnknown3 ||
                    AIVerticalIncrease != Default.AIVerticalIncrease ||
                    Blank1 != Default.Blank1 ||
                    Blank2 != Default.Blank2 ||
                    Blank3 != Default.Blank3 ||
                    Blank4 != Default.Blank4 ||
                    Blank5 != Default.Blank5 ||
                    JPCost != Default.JPCost ||
                    LearnOnHit != Default.LearnOnHit ||
                    LearnRate != Default.LearnRate ||
                    LearnWithJP != Default.LearnWithJP ||
                    Unknown1 != Default.Unknown1 ||
                    Unknown2 != Default.Unknown2 ||
                    Unknown3 != Default.Unknown3 ||
                    Unknown4 != Default.Unknown4 ||
                    (IsArithmetick && ArithmetickSkill != Default.ArithmetickSkill) ||
                    (IsCharging && (ChargeBonus != Default.ChargeBonus || ChargeCT != Default.ChargeCT)) ||
                    (IsItem && ItemOffset != Default.ItemOffset) ||
                    (IsJumping && (JumpHorizontal != Default.JumpHorizontal || JumpVertical != Default.JumpVertical)) ||
                    (IsNormal && (Attributes.HasChanged || Effect.Value != Default.Effect.Value)) ||
                    (IsOther && OtherID != Default.OtherID) ||
                    (IsThrowing && Throwing != Default.Throwing));
            }
        }

        public bool IsArithmetick { get; private set; }

        public bool IsCharging { get; private set; }

        public bool IsItem { get; private set; }

        public bool IsJumping { get; private set; }

        public bool IsNormal { get; private set; }

        public bool IsOther { get; private set; }

        public bool IsThrowing { get; private set; }

        public Item Item { get; set; }

        public UInt16 ItemOffset
        {
            get
            {
                return (UInt16)(IsItem ? Item.Offset : 0);
            }
            set { Item = Item.GetItemAtOffset( value ); }
        }

        public UInt16 JPCost { get; set; }

        public byte JumpHorizontal { get; set; }

        public byte JumpVertical { get; set; }

        public bool LearnOnHit { get { return learnOnHit; } set { learnOnHit = value; } }

        public byte LearnRate { get; set; }

        public bool LearnWithJP { get { return learnWithJP; } set { learnWithJP = value; } }

        public string Name { get; private set; }

        public UInt16 Offset { get; private set; }

        [Hex]
        public byte OtherID { get; set; }

        public ItemSubType Throwing { get; set; }

        public bool Unknown1 { get { return unknown1; } set { unknown1 = value; } }

        public bool Unknown2 { get { return unknown2; } set { unknown2 = value; } }

        public bool Unknown3 { get { return unknown3; } set { unknown3 = value; } }

        public bool Unknown4 { get { return unknown4; } set { unknown4 = value; } }


        #endregion Properties

        #region Constructors (4)

        private Ability( string name, UInt16 offset, IList<byte> first )
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

            AIFlags = new AIFlags( first.Sub( 4, 6 ) );

            Utilities.CopyByteToBooleans( first[7],
                ref unknown1, ref unknown2, ref unknown3, ref blank2, ref blank3, ref blank4, ref blank5, ref unknown4 );
        }

        public static void CopyCommon( Ability source, Ability destination )
        {
            destination.JPCost = source.JPCost;
            destination.LearnRate = source.LearnRate;
            destination.LearnWithJP = source.LearnWithJP;
            destination.Action = source.Action;
            destination.LearnOnHit = source.LearnOnHit;
            destination.Blank1 = source.Blank1;
            destination.AbilityType = source.AbilityType;
            destination.Unknown1 = source.Unknown1;
            destination.Unknown2 = source.Unknown2;
            destination.Unknown3 = source.Unknown3;
            destination.Unknown4 = source.Unknown4;
            destination.Blank2 = source.Blank2;
            destination.Blank3 = source.Blank3;
            destination.Blank4 = source.Blank4;
            destination.Blank5 = source.Blank5;
            source.AIFlags.CopyTo( destination.AIFlags );
        }

        public void CopyCommonTo( Ability destination )
        {
            CopyCommon( this, destination );
        }

        public void CopyTo( Ability destination )
        {
            Copy( this, destination );
        }

        public static void Copy( Ability source, Ability destination )
        {
            if( (source.IsNormal ^ destination.IsNormal) ||
                (source.IsItem ^ destination.IsItem) ||
                (source.IsThrowing ^ destination.IsThrowing) ||
                (source.IsJumping ^ destination.IsJumping) ||
                (source.IsCharging ^ destination.IsCharging) ||
                (source.IsArithmetick ^ destination.IsArithmetick) ||
                (source.IsOther ^ destination.IsOther) )
            {
                throw new InvalidOperationException( "Cannot convert between ability types" );
            }

            CopyCommon( source, destination );

            if( destination.IsNormal )
            {
                source.Attributes.CopyTo( destination.Attributes );
            }
            if( destination.IsItem )
            {
                destination.ItemOffset = source.ItemOffset;
            }
            if( destination.IsThrowing )
            {
                destination.Throwing = source.Throwing;
            }
            if( destination.IsJumping )
            {
                destination.JumpHorizontal = source.JumpHorizontal;
                destination.JumpVertical = source.JumpVertical;
            }
            if( destination.IsCharging )
            {
                destination.ChargeCT = source.ChargeCT;
                destination.ChargeBonus = source.ChargeBonus;
            }
            if( destination.IsArithmetick )
            {
                destination.ArithmetickSkill = source.ArithmetickSkill;
            }
            if( destination.IsOther )
            {
                destination.OtherID = source.OtherID;
            }
        }

        public Ability( string name, UInt16 offset )
        {
            Name = name;
            Offset = offset;
        }

        public Ability( string name, UInt16 offset, IList<byte> first, IList<byte> second )
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

        public Ability( string name, UInt16 offset, IList<byte> first, IList<byte> second, Ability defaults )
            : this( name, offset, first, second )
        {
            Default = defaults;
            if( IsNormal )
            {
                Attributes.Default = Default.Attributes;
            }
            AIFlags.Default = Default.AIFlags;
        }

        #endregion Constructors

        #region Methods (7)

        public bool[] PropertiesToBoolArray()
        {
            return new bool[12] {
                LearnWithJP, Action, LearnOnHit, Blank1,
                Unknown1, Unknown2, Unknown3, Blank2, Blank3, Blank4, Blank5, Unknown4 };
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

        public void WriteXml( XmlWriter writer )
        {
            if( HasChanged )
            {
                writer.WriteStartElement( GetType().Name );
                writer.WriteAttributeString( "value", Offset.ToString( "X4" ) );
                writer.WriteAttributeString( "name", Name );
                DigestGenerator.WriteXmlDigest( this, writer, false, false );
                DigestGenerator.WriteXmlDigest( AIFlags, writer, true, true );

                if( IsNormal )
                {
                    DigestGenerator.WriteDigestEntry( writer, "Effect", Default.Effect, Effect );
                    DigestGenerator.WriteXmlDigest( Attributes, writer, true, true );
                    if( Attributes.Formula.Value == 0x02 &&
                        (Attributes.Formula.Value != Attributes.Default.Formula.Value || Attributes.InflictStatus != Attributes.Default.InflictStatus) )
                    {
                        writer.WriteStartElement( "CastSpell" );
                        writer.WriteAttributeString( "default", AllAbilities.Names[Attributes.Default.InflictStatus] );
                        writer.WriteAttributeString( "value", AllAbilities.Names[Attributes.InflictStatus] );
                        writer.WriteEndElement();
                    }
                    else if( Attributes.InflictStatus != Attributes.Default.InflictStatus )
                    {
                        writer.WriteStartElement( "InflictStatusDescription" );
                        writer.WriteAttributeString( "default", FFTPatch.InflictStatuses.InflictStatuses[Attributes.Default.InflictStatus].Statuses.ToString() );
                        writer.WriteAttributeString( "value", FFTPatch.InflictStatuses.InflictStatuses[Attributes.InflictStatus].Statuses.ToString() );
                        writer.WriteEndElement();
                    }
                }
                else if( IsItem )
                {
                    DigestGenerator.WriteDigestEntry( writer, "ItemOffset", Default.ItemOffset, ItemOffset, "0x{0:X2}" );
                }
                else if( IsThrowing )
                {
                    DigestGenerator.WriteDigestEntry( writer, "Throwing", Default.Throwing, Throwing );
                }
                else if( IsJumping )
                {
                    DigestGenerator.WriteDigestEntry( writer, "JumpHorizontal", Default.JumpHorizontal, JumpHorizontal );
                    DigestGenerator.WriteDigestEntry( writer, "JumpVertical", Default.JumpVertical, JumpVertical );
                }
                else if( IsCharging )
                {
                    DigestGenerator.WriteDigestEntry( writer, "ChargeCT", Default.ChargeCT, ChargeCT );
                    DigestGenerator.WriteDigestEntry( writer, "ChargeBonus", Default.ChargeBonus, ChargeBonus );
                }
                else if( IsArithmetick )
                {
                    DigestGenerator.WriteDigestEntry( writer, "ArithmetickSkill", Default.ArithmetickSkill, ArithmetickSkill, "0x{0:X2}" );
                }
                else if( IsOther )
                {
                    DigestGenerator.WriteDigestEntry( writer, "OtherID", Default.OtherID, OtherID, "0x{0:X2}" );
                }

                writer.WriteEndElement();
            }
        }



        public override string ToString()
        {
            return (HasChanged ? "*" : "") + Name;
        }


        #endregion Methods

    }
}