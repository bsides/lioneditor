/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of LionEditor.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LionEditor
{
    public class Character
    {
        #region Character Map

        public static char[] characterMap = new char[256] {
            '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F',
            'G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V',
            'W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l',
            'm','n','o','p','q','r','s','t','u','v','w','x','y','z','!',
            '\x3042', // hiragana A
            '?',
            '\x3044', // hiragana I
            '+',
            '\x3046', // hiragana U
            '/',
            '\x3048', // hiragana E
            ':',
            '\x304A', // hiragana O
            '\x304B','\x304C','\x304D','\x304E','\x304F','\x3050','\x3051','\x3052','\x3053','\x3054', // KA GA KI GI KU GU KE GE KO GO
            '\x3055','\x3056','\x3057','\x3058','\x3059','\x305A','\x305B','\x305C','\x305D','\x305E', // SA ZA SHI JI SU ZU SE ZE SO ZO
            '\x305F','\x3060','\x3061', // TA DA CHI
            '.',
            '\x3063', // little TSU
            '\x3064','\x3065','\x3066','\x3067','\x3068','\x3069', // TSU DZU TE DE TO DO
            '\x306A','\x306B','\x306C','\x306D','\x306E', // NA NI NU NE NO
            '\x306F','\x3070','\x3071','\x3072','\x3073','\x3074','\x3075','\x3076','\x3077','\x3078','\x3079','\x307A','\x307B','\x307C','\x307D', // HA BA PA HI BI PI FU BU PU HE BE PE HO BO PO
            '\x307E','\x307F','\x3080','\x3081','\x3082', // MA MI MU ME MO
            '\x3083', // little YA
            '\x3084', // YA
            '\x3085', // little YU
            '\x3086', // YU
            '\x3087', // little YO
            '\x3088', // YO
            '\x3089','\x308A','\x308B','\x308C','\x308D', // RA RI RU RE RO
            '-',
            '\x308F', // WA
            '(',')',
            '\x3092', // WO 
            '\x3093', // N
            '"',
            '\x30A2', // katakana A
            '\'',
            '\x30A4', // katakana I
            ' ',
            '\x30A6', // katakana U
            '\x30A7', // katakana little E
            '\x30A8', // katakana E
            '\x30A9', // katakana little O
            '\x30AA', // katakana O
            '\x30AB','\x30AC','\x30AD','\x30AE','\x30AF','\x30B0','\x30B1','\x30B2','\x30B3','\x30B4', // KA GA KI GI KU GU KE GE KO GO
            '\x30B5','\x30B6','\x30B7','\x30B8','\x30B9','\x30BA','\x30BB','\x30BC','\x30BD','\x30BE', // SA ZA SHI JI SU ZU SE ZE SO ZO
            '\x30BF','\x30C0','\x30C1', // TA DA CHI
            '\x266A', // musical note
            '\x30C3', // little TSU
            '\x30C4', // TSU
            '*', // really a star
            '\x30C6','\x30C7','\x30C8','\x30C9', // TE DE TO DO
            '\x30CA','\x30CB','\x30CC','\x30CD','\x30CE', // NA NI NU NE NO
            '\x30CF','\x30D0','\x30D1','\x30D2','\x30D3','\x30D4','\x30D5','\x30D6','\x30D7','\x30D8','\x30D9','\x30DA','\x30DB','\x30DC','\x30DD', // HA BA PA HI BI PI FU BU PU HE BE PE HO BO PO
            '\x30DE','\x30DF','\x30E0', // MA MI MU
            // Everything after katakana "mu" seems to be bugged garbage
            '\x30E1','\x30E2','\x30E3','\x30E4','\x30E5','\x30E6','\x30E7','\x30E8','\x30E9','\x30EA','\x30EB','\x30EC','\x30ED','\x30EE','\x30EF',
            '#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#',
            '#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#' };

        #endregion

        #region Fields

        private SpriteSet spriteSet;
        private byte index;
        private byte[] rawName = new byte[15];
        private Class job;
        private Gender gender;
        private Zodiac zodiacSign;
        private SecondaryAction secondaryAction;
        private Ability reactAbility;
        private Ability supportAbility;
        private Ability movementAbility;
        private Item head;
        private Item body;
        private Item accessory;
        private Item rightHand;
        private Item rightShield;
        private Item leftHand;
        private Item leftShield;
        private byte experience;
        private byte level;
        private byte bravery;
        private byte faith;
        private uint rawHP;
        private uint rawMP;
        private uint rawSP;
        private uint rawPA;
        private uint rawMA;
        private JobsAndAbilities jobsAndAbilities;
        private byte unknownOffset03;
        private byte unknownOffset05;
        private byte[] jobsUnlocked = new byte[3];
        private byte[,] skillsUnlocked = new byte[22, 3];
        private byte[] jobLevels = new byte[12];
        private ushort[] jp = new ushort[23];
        private ushort[] totalJP = new ushort[23];
        private byte[] afterName = new byte[21];

        #endregion

        #region Properties

        #region Identity

        /// <summary>
        /// Gets or sets this character's <see cref="SpriteSet"/>
        /// </summary>
        public SpriteSet SpriteSet
        {
            get { return spriteSet; }
            set { spriteSet = value; }
        }

        /// <summary>
        /// Gets or sets this character's index on the party screen
        /// </summary>
        public byte Index
        {
            get { return index; }
            set { index = value; }
        }

        /// <summary>
        /// Gets or sets this character's name
        /// </summary>
        public string Name
        {
            get { return DecodeName( rawName, 0 ); }
            set { EncodeName( value, rawName, 0 ); }
        }

        /// <summary>
        /// Gets or sets this character's Job
        /// </summary>
        public Class Job
        {
            get { return job; }
            set { job = value; }
        }

        /// <summary>
        /// Gets or sets this character's <see cref="Gender"/>
        /// </summary>
        public Gender Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        /// <summary>
        /// Gets or sets this characters <see cref="Zodiac"/>
        /// </summary>
        public Zodiac ZodiacSign
        {
            get { return zodiacSign; }
            set { zodiacSign = value; }
        }

        #endregion Identity

        #region Abilities

        /// <summary>
        /// Gets or sets this character's Secondary Action
        /// </summary>
        public SecondaryAction SecondaryAction
        {
            get { return secondaryAction; }
            set { secondaryAction = value; }
        }

        /// <summary>
        /// Gets or sets this character's Reaction Ability
        /// </summary>
        public Ability ReactAbility
        {
            get { return reactAbility; }
            set { reactAbility = value; }
        }

        /// <summary>
        /// Gets or sets this character's Support Ability
        /// </summary>
        public Ability SupportAbility
        {
            get { return supportAbility; }
            set { supportAbility = value; }
        }

        /// <summary>
        /// Gets or sets this character's Movement Ability
        /// </summary>
        public Ability MovementAbility
        {
            get { return movementAbility; }
            set { movementAbility = value; }
        }

        #endregion Abilities

        #region Equipment

        /// <summary>
        /// Gets or sets this character's equipped helmet
        /// </summary>
        public Item Head
        {
            get { return head; }
            set { head = value; }
        }

        /// <summary>
        /// Gets or sets this character's equipped armor
        /// </summary>
        public Item Body
        {
            get { return body; }
            set { body = value; }
        }

        /// <summary>
        /// Gets or sets this character's equipped accessory
        /// </summary>
        public Item Accessory
        {
            get { return accessory; }
            set { accessory = value; }
        }

        /// <summary>
        /// Gets or sets this character's equipped right-hand weapon
        /// </summary>
        public Item RightHand
        {
            get { return rightHand; }
            set { rightHand = value; }
        }

        /// <summary>
        /// Gets this character's right-hand weapon's power
        /// </summary>
        public uint RPower
        {
            get { return rightHand.Power; }
        }

        /// <summary>
        /// Gets or sets this character's equipped right-hand shield
        /// </summary>
        public Item RightShield
        {
            get { return rightShield; }
            set { rightShield = value; }
        }

        /// <summary>
        /// Gets this character's right-hand shield's block rate
        /// </summary>
        public uint RBlockRate
        {
            get { return rightHand.BlockRate; }
        }

        /// <summary>
        /// Gets or sets this character's equipped left-hand weapon
        /// </summary>
        public Item LeftHand
        {
            get { return leftHand; }
            set { leftHand = value; }
        }

        /// <summary>
        /// Gets this character's left-hand weapon's power
        /// </summary>
        public uint LPower
        {
            get { return leftHand.Power; }
        }

        /// <summary>
        /// Gets or sets this character's equipped left-hand shield
        /// </summary>
        public Item LeftShield
        {
            get { return leftShield; }
            set { leftShield = value; }
        }

        /// <summary>
        /// Gets this character's left-hand shield's block rate
        /// </summary>
        public uint LBlockRate
        {
            get { return leftHand.BlockRate; }
        }

        #endregion

        #region Stats

        /// <summary>
        /// Gets or sets this character's experience
        /// </summary>
        public byte Experience
        {
            get { return experience; }
            set { experience = value; }
        }

        /// <summary>
        /// Gets or sets this character's level
        /// </summary>
        public byte Level
        {
            get { return level; }
            set { level = value; }
        }

        /// <summary>
        /// Gets or sets this character's bravery
        /// </summary>
        public byte Bravery
        {
            get { return bravery; }
            set { bravery = value; }
        }

        /// <summary>
        /// Gets or sets this character's faith
        /// </summary>
        public byte Faith
        {
            get { return faith; }
            set { faith = value; }
        }

        #region HP

        /// <summary>
        /// Gets this character's raw HP stat
        /// </summary>
        public uint RawHP
        {
            get { return rawHP; }
        }

        /// <summary>
        /// Gets the HP bonus provided by this character's equipment
        /// </summary>
        public uint HPBonus
        {
            get
            {
                // TODO: determine if the equipment must be in the "right place" in order to receive bonus
                return
                    rightHand.HPBonus +
                    rightShield.HPBonus +
                    leftHand.HPBonus +
                    leftShield.HPBonus +
                    head.HPBonus +
                    body.HPBonus +
                    accessory.HPBonus;
            }
        }

        /// <summary>
        /// Gets the HP multiplier provided by this character's abilities
        /// </summary>
        public decimal HPMultiplier
        {
            // TODO: determine if the abilities must be in the "right place" in order to receive bonus
            get { return ((decimal)(100 + supportAbility.HPMultiplier + reactAbility.HPMultiplier + movementAbility.HPMultiplier)) / 100; }
        }

        /// <summary>
        /// Gets or sets the "human readable" HP based on <see cref="RawHP"/>, <see cref="HPBonus"/>, and <see cref="HPMultiplier"/>.
        /// </summary>
        public uint HP
        {
            get { return (uint)(job.ActualHP( rawHP ) * HPMultiplier + HPBonus); }
            set { rawHP = job.GetRawHPFromActualHP( (int)((value - HPBonus) / HPMultiplier) ); }
        }

        /// <summary>
        /// Gets the maximum HP this character can have with his current equipment, abilities, and job.
        /// </summary>
        public uint MaxHP
        {
            get { return (uint)(job.ActualHP( 0xFFFFFF ) * HPMultiplier + HPBonus); }
        }

        #endregion HP

        #region MP

        /// <summary>
        /// Gets this character's raw MP stat
        /// </summary>
        public uint RawMP
        {
            get { return rawMP; }
        }

        /// <summary>
        /// Gets the MP bonus provided by this character's equipment
        /// </summary>
        public uint MPBonus
        {
            get
            {
                // TODO: determine if the equipment must be in the "right place" in order to receive bonus
                return
                    rightHand.MPBonus +
                    rightShield.MPBonus +
                    leftHand.MPBonus +
                    leftShield.MPBonus +
                    head.MPBonus +
                    body.MPBonus +
                    accessory.MPBonus;
            }
        }

        /// <summary>
        /// Gets or sets the "human readable" MP based on <see cref="RawMP"/> and <see cref="MPBonus"/>.
        /// </summary>
        public uint MP
        {
            get { return (uint)(job.ActualMP( rawMP ) + MPBonus); }
            set { rawMP = job.GetRawMPFromActualMP( (int)(value - MPBonus) ); }
        }

        /// <summary>
        /// Gets the maximum MP this character can have with his current equipment and job.
        /// </summary>
        public uint MaxMP
        {
            get { return (uint)(job.ActualMP( 0xFFFFFF ) + MPBonus); }
        }

        #endregion MP

        #region Speed

        /// <summary>
        /// Gets this character's raw speed stat
        /// </summary>
        public uint RawSP
        {
            get { return rawSP; }
        }

        /// <summary>
        /// Gets the speed bonus provided by this character's equipment
        /// </summary>
        public uint SpeedBonus
        {
            get
            {
                // TODO: determine if the equipment must be in the "right place" in order to receive bonus
                return
                    rightHand.SpeedBonus +
                    rightShield.SpeedBonus +
                    leftHand.SpeedBonus +
                    leftShield.SpeedBonus +
                    head.SpeedBonus +
                    body.SpeedBonus +
                    accessory.SpeedBonus;
            }
        }

        /// <summary>
        /// Gets or sets the "human readable" speed based on <see cref="RawSP"/> and <see cref="SpeedBonus"/>
        /// </summary>
        public uint Speed
        {
            get { return (uint)(job.ActualSP( rawSP ) + SpeedBonus); }
            set { rawSP = job.GetRawSPFromActualSP( (int)(value - SpeedBonus) ); }
        }

        /// <summary>
        /// Gets the maximum speed this character can have with his current equipment and job.
        /// </summary>
        public uint MaxSpeed
        {
            get { return (uint)(job.ActualSP( rawSP ) + SpeedBonus); }
        }

        #endregion Speed

        #region Physical Attack

        /// <summary>
        /// Gets this character's raw physical attack stat
        /// </summary>
        public uint RawPA
        {
            get { return rawPA; }
        }

        /// <summary>
        /// Gets the physical attack bonus provided by this character's equipment
        /// </summary>
        public uint PABonus
        {
            get
            {
                // TODO: determine if the equipment must be in the "right place" in order to receive bonus
                return
                    rightHand.PABonus +
                    leftHand.PABonus +
                    rightShield.PABonus +
                    leftShield.PABonus +
                    head.PABonus +
                    body.PABonus +
                    accessory.PABonus;
            }
        }

        /// <summary>
        /// Gets or sets the "human readable" physical attack based on <see cref="RawPA"/> and <see cref="PABonus"/>.
        /// </summary>
        public uint PA
        {
            get { return (uint)(job.ActualPA( rawPA ) + PABonus); }
            set { rawPA = job.GetRawPAFromActualPA( (int)(value - PABonus) ); }
        }

        /// <summary>
        /// Gets the maximum physical attack this character can have with his current equipment and job.
        /// </summary>
        public uint MaxPA
        {
            get { return (uint)(job.ActualPA( 0xFFFFFF ) + PABonus); }
        }

        #endregion Pysical Attack

        #region Magic Attack

        /// <summary>
        /// Gets this character's raw magic attack stat
        /// </summary>
        public uint RawMA
        {
            get { return rawMA; }
        }

        /// <summary>
        /// Gets the magic attack bonus provided by this character's equipment
        /// </summary>
        public uint MABonus
        {
            get
            {
                // TODO: determine if the equipment must be in the "right place" in order to receive bonus
                return
                    rightHand.MABonus +
                    rightShield.MABonus +
                    leftHand.MABonus +
                    leftShield.MABonus +
                    head.MABonus +
                    body.MABonus +
                    accessory.MABonus;
            }
        }

        /// <summary>
        /// Gets or sets the "human readable" magic attack based on <see cref="RawMA"/> and <see cref="MABonus"/>.
        /// </summary>
        public uint MA
        {
            get { return (uint)(job.ActualMA( rawMA ) + MABonus); }
            set { rawMA = job.GetRawMAFromActualMA( (int)(value - MABonus) ); }
        }

        /// <summary>
        /// Gets the maximum magic attack this character can have with his current equipment and job.
        /// </summary>
        public uint MaxMA
        {
            get { return (uint)(job.ActualMA( 0xFFFFFF ) + MABonus); }
        }

        #endregion Magic Attack

        #region Move

        /// <summary>
        /// Gets the movement bonus provided by this character's equipment and abilities
        /// </summary>
        public uint MoveBonus
        {
            get
            {
                // TODO: determine if the equipment/abilities must be in the "right place" in order to receive bonus
                return
                    reactAbility.MoveBonus +
                    supportAbility.MoveBonus +
                    movementAbility.MoveBonus +
                    accessory.MoveBonus +
                    leftHand.MoveBonus +
                    leftShield.MoveBonus +
                    rightHand.MoveBonus +
                    rightShield.MoveBonus +
                    head.MoveBonus +
                    body.MoveBonus;
            }
        }

        /// <summary>
        /// Gets this character's movement distance based on his current Job and <see cref="MoveBonus"/>
        /// </summary>
        public uint Move
        {
            get { return (uint)(job.Move + MoveBonus); }
        }

        #endregion Move

        #region Jump

        /// <summary>
        /// Gets the jump bonus provided by this character's equipment and abilities
        /// </summary>
        public uint JumpBonus
        {
            get
            {
                // TODO: determine if the equipment/abilities must be in the "right place" in order to receive bonus
                return
                    reactAbility.JumpBonus +
                    supportAbility.JumpBonus +
                    movementAbility.JumpBonus +
                    accessory.JumpBonus +
                    head.JumpBonus +
                    rightHand.JumpBonus +
                    rightShield.JumpBonus +
                    leftHand.JumpBonus +
                    leftShield.JumpBonus +
                    body.JumpBonus;
            }
        }

        /// <summary>
        /// Gets this character's jump distance based on his current Job and <see cref="JumpBonus"/>
        /// </summary>
        public uint Jump
        {
            get { return (uint)(job.Jump + JumpBonus); }
        }

        #endregion Jump

        /// <summary>
        /// Gets this character's CEvade%, based on his Job
        /// </summary>
        public uint PhysicalCEV
        {
            get { return (uint)job.CEvade; }
        }

        /// <summary>
        /// Gets this character's Magic CEvade%
        /// </summary>
        /// <remarks>
        /// This value is always 0.
        /// </remarks>
        public uint MagicCEV
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets this character's Shield Evade%, based on his equipment
        /// </summary>
        public uint PhysicalSEV
        {
            get
            {
                // TODO: determine if the equipment must be in the "right place" in order to affect stat
                return
                    rightShield.PhysicalSEV +
                    rightHand.PhysicalSEV +
                    leftHand.PhysicalSEV +
                    leftShield.PhysicalSEV +
                    head.PhysicalSEV +
                    body.PhysicalSEV +
                    accessory.PhysicalSEV;
            }
        }

        /// <summary>
        /// Gets this character's Magic Shield Evade%, based on his equipment
        /// </summary>
        public uint MagicSEV
        {
            get
            {
                // TODO: determine if the equipment must be in the "right place" in order to affect stat
                return
                    rightHand.MagicSEV +
                    rightShield.MagicSEV +
                    leftHand.MagicSEV +
                    leftShield.MagicSEV +
                    head.MagicSEV +
                    body.MagicSEV +
                    accessory.MagicSEV;
            }
        }

        /// <summary>
        /// Gets this character's physical accessory evade%, based on his equipment
        /// </summary>
        public uint PhysicalAEV
        {
            get
            {
                // TODO: determine if the equipment must be in the "right place" in order to affect stat
                return
                    rightHand.PhysicalAEV +
                    rightShield.PhysicalAEV +
                    leftHand.PhysicalAEV +
                    leftShield.PhysicalAEV +
                    head.PhysicalAEV +
                    body.PhysicalAEV +
                    accessory.PhysicalAEV;
            }
        }

        /// <summary>
        /// Gets this character's magic accessory evade%, based on his equipment
        /// </summary>
        public uint MagicAEV
        {
            get
            {
                // TODO: determine if the equipment must be in the "right place" in order to affect stat
                return
                    rightHand.MagicAEV +
                    rightShield.MagicAEV +
                    leftHand.MagicAEV +
                    leftShield.MagicAEV +
                    head.MagicAEV +
                    body.MagicAEV +
                    accessory.MagicAEV;
            }
        }

        #endregion Stats

        #region Jobs/Skills

        public JobsAndAbilities JobsAndAbilities
        {
            get { return jobsAndAbilities; }
        }

        public byte[] JobsUnlocked
        {
            get { return jobsUnlocked; }
            set { jobsUnlocked = value; }
        }

        public byte[,] SkillsUnlocked
        {
            get { return skillsUnlocked; }
            set { skillsUnlocked = value; }
        }

        public byte[] JobLevels
        {
            get { return jobLevels; }
            set { jobLevels = value; }
        }

        public ushort[] JP
        {
            get { return jp; }
            set { jp = value; }
        }

        public ushort[] TotalJP
        {
            get { return totalJP; }
            set { totalJP = value; }
        }

        #endregion Jobs/Skills

        public bool OnProposition
        {
            get { return afterName[0x03] == 0x01; }
            set { afterName[0x03] = (byte)(value ? 0x01 : 0x00); }
        }

        public byte Kills
        {
            get { return afterName[0x06]; }
            set { afterName[0x06] = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Builds a Character from a 256 byte array
        /// </summary>
        /// <param name="charData"></param>
        public Character( byte[] charData )
        {
            try
            {
                spriteSet = SpriteSet.AllSprites[charData[0]];
                index = charData[1];
                if( !Class.ClassDictionary.TryGetValue( charData[2], out job ) )
                {
                    job = Class.ClassDictionary[0x4A];
                }
                unknownOffset03 = charData[3];
                gender = (Gender)(charData[4] & 0xE0);
                unknownOffset05 = charData[5];
                zodiacSign = (Zodiac)(charData[6] & 0xF0);

                if( gender == Gender.Monster )
                {
                    secondaryAction = SecondaryAction.ActionDictionary[0x00];
                    supportAbility = Ability.AbilityList[0];
                    reactAbility = Ability.AbilityList[0];
                    movementAbility = Ability.AbilityList[0];
                    head = Item.ItemList[0];
                    body = Item.ItemList[0];
                    accessory = Item.ItemList[0];
                    rightHand = Item.ItemList[0];
                    rightShield = Item.ItemList[0];
                    leftHand = Item.ItemList[0];
                    leftShield = Item.ItemList[0];
                }
                else
                {
                    secondaryAction = SecondaryAction.ActionDictionary[charData[7]];
                    reactAbility = new Ability( (ushort)((charData[9] << 8) + charData[8]) );
                    supportAbility = new Ability( (ushort)((charData[11] << 8) + charData[10]) );
                    movementAbility = new Ability( (ushort)((charData[13] << 8) + charData[12]) );
                    head = new Item( (ushort)((ushort)(charData[15] << 8) + charData[14]) );
                    body = new Item( (ushort)((ushort)(charData[17] << 8) + charData[16]) );
                    accessory = new Item( (ushort)((ushort)(charData[19] << 8) + charData[18]) );
                    rightHand = new Item( (ushort)((ushort)(charData[21] << 8) + charData[20]) );
                    rightShield = new Item( (ushort)((ushort)(charData[23] << 8) + charData[22]) );
                    leftHand = new Item( (ushort)((ushort)(charData[25] << 8) + charData[24]) );
                    leftShield = new Item( (ushort)((ushort)(charData[27] << 8) + charData[26]) );
                }

                experience = charData[28];
                level = charData[29];
                bravery = charData[30];
                faith = charData[31];

                rawHP = (uint)(((uint)charData[34] << 16) + ((uint)charData[33] << 8) + (uint)charData[32]);
                rawMP = (uint)(((uint)charData[37] << 16) + ((uint)charData[36] << 8) + (uint)charData[35]);
                rawSP = (uint)(((uint)charData[40] << 16) + ((uint)charData[39] << 8) + (uint)charData[38]);
                rawPA = (uint)(((uint)charData[43] << 16) + ((uint)charData[42] << 8) + (uint)charData[41]);
                rawMA = (uint)(((uint)charData[46] << 16) + ((uint)charData[45] << 8) + (uint)charData[44]);

                byte[] jaBytes = new byte[173];

                Savegame.CopyArray( charData, jaBytes, 0x4BB - 0x48C, 173 );
                jobsAndAbilities = new JobsAndAbilities( jaBytes );

                for( int i = 0; i < 15; i++ )
                {
                    rawName[i] = charData[0xDC + i];
                }

                for( int k = 0; k < 21; k++ )
                {
                    afterName[k] = charData[0xEB + k];
                }
            }
            catch( Exception )
            {
                throw new BadCharacterDataException();
            }
        }

        /// <summary>
        /// Creates a default character at the specified index
        /// </summary>
        /// <param name="index"></param>
        public Character( int index )
        {
            accessory = new Item( 0 );
            body = new Item( 0 );
            bravery = 50;
            experience = 0;
            faith = 50;
            gender = Gender.Male;
            head = new Item( 0 );
            index = (byte)index;
            job = Class.ClassDictionary[0x4A];

            jobLevels = new byte[] { 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11 };
            jobsUnlocked = new byte[] { 0xC0, 0x00, 0x00 };
            jp = new ushort[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            totalJP = new ushort[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            byte[] jobBytes = new byte[] {
                0xC0, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x00, 0x00, 0x00,
                0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            jobsAndAbilities = new JobsAndAbilities( jobBytes );

            leftHand = new Item( 0 );
            leftShield = new Item( 0 );
            level = 1;
            movementAbility = new Ability( 0 );
            Name = "##########";
            rawHP = Job.GetRawHPFromActualHP( 50 );
            rawMA = Job.GetRawMAFromActualMA( 11 );
            rawMP = Job.GetRawMPFromActualMP( 10 );
            rawPA = Job.GetRawPAFromActualPA( 5 );
            rawSP = Job.GetRawSPFromActualSP( 6 );
            reactAbility = new Ability( 0 );
            rightHand = new Item( 0 );
            rightShield = new Item( 0 );
            secondaryAction = SecondaryAction.ActionDictionary[0x00];
            for( int i = 0; i < 22; i++ )
            {
                for( int j = 0; j < 3; j++ )
                {
                    skillsUnlocked[i, j] = 0x00;
                }
            }

            spriteSet = SpriteSet.AllSprites[0x80];
            supportAbility = new Ability( 0 );
            unknownOffset03 = 0x00;
            unknownOffset05 = 0x00;
            zodiacSign = Zodiac.Aries;
            OnProposition = false;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Decodes a name from FFT's dumb character encoding
        /// </summary>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static string DecodeName( byte[] source, int start )
        {
            StringBuilder sb = new StringBuilder();
            int k = start;
            while( source[k] != 0xFE )
            {
                sb.Append( characterMap[source[k]] );
                k++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Encodes a name into FFT's dumb character encoding
        /// </summary>
        /// <param name="name"></param>
        /// <param name="destination"></param>
        /// <param name="start"></param>
        public static void EncodeName( string name, byte[] destination, int start )
        {
            int i = 0;
            List<char> charList = new List<char>( characterMap );

            foreach( char c in name )
            {
                destination[i + start] = (byte)(charList.IndexOf( c ) & 0xFF);
                i++;
            }
            destination[i + start] = 0xFE;
        }

        /// <summary>
        /// Creates a byte array for this character
        /// </summary>
        /// <returns>byte[] of length 256</returns>
        public byte[] ToByteArray()
        {
            byte[] result = new byte[256];
            result[0] = spriteSet.Value;
            result[1] = index;
            result[2] = job.Byte;
            result[3] = unknownOffset03;
            result[4] = (byte)gender;
            result[5] = unknownOffset05;
            result[6] = (byte)zodiacSign;
            result[7] = secondaryAction.Byte;
            result[8] = (byte)(reactAbility.Value & 0xFF);
            result[9] = (byte)((reactAbility.Value & 0xFF00) >> 8);
            result[10] = (byte)(supportAbility.Value & 0xFF);
            result[11] = (byte)((supportAbility.Value & 0xFF00) >> 8);
            result[12] = (byte)(movementAbility.Value & 0xFF);
            result[13] = (byte)((movementAbility.Value & 0xFF00) >> 8);

            result[14] = head.ToByte()[0];
            result[15] = head.ToByte()[1];
            result[16] = body.ToByte()[0];
            result[17] = body.ToByte()[1];
            result[18] = accessory.ToByte()[0];
            result[19] = accessory.ToByte()[1];
            result[20] = rightHand.ToByte()[0];
            result[21] = rightHand.ToByte()[1];
            result[22] = rightShield.ToByte()[0];
            result[23] = rightShield.ToByte()[1];
            result[24] = leftHand.ToByte()[0];
            result[25] = leftHand.ToByte()[1];
            result[26] = leftShield.ToByte()[0];
            result[27] = leftShield.ToByte()[1];
            result[28] = experience;
            result[29] = level;
            result[30] = bravery;
            result[31] = faith;

            result[32] = (byte)(rawHP & 0xFF);
            result[33] = (byte)((rawHP & 0xFF00) >> 8);
            result[34] = (byte)((rawHP & 0xFF0000) >> 16);

            result[35] = (byte)(rawMP & 0xFF);
            result[36] = (byte)((rawMP & 0xFF00) >> 8);
            result[37] = (byte)((rawMP & 0xFF0000) >> 16);

            result[38] = (byte)(rawSP & 0xFF);
            result[39] = (byte)((rawSP & 0xFF00) >> 8);
            result[40] = (byte)((rawSP & 0xFF0000) >> 16);

            result[41] = (byte)(rawPA & 0xFF);
            result[42] = (byte)((rawPA & 0xFF00) >> 8);
            result[43] = (byte)((rawPA & 0xFF0000) >> 16);

            result[44] = (byte)(rawMA & 0xFF);
            result[45] = (byte)((rawMA & 0xFF00) >> 8);
            result[46] = (byte)((rawMA & 0xFF0000) >> 16);

            byte[] jaBytes = jobsAndAbilities.ToByteArray();
            for( int i = 0; i < 173; i++ )
            {
                result[47 + i] = jaBytes[i];
            }

            for( int i = 0; i < 15; i++ )
            {
                result[0xDC + i] = rawName[i];
            }

            for( int k = 0; k < 21; k++ )
            {
                result[0xEB + k] = afterName[k];
            }

            return result;
        }

        /// <summary>
        /// This character's <see cref="Name"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }

    public class BadCharacterDataException : Exception
    {
    }
}