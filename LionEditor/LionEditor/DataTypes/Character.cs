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
using System.Collections.Generic;
using System.Text;

namespace LionEditor
{
    public class Character : ICloneable
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
        private bool isPresent;
        private bool isDummy = false;

        private static Random rng = new Random();

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
            get { return DecodeName(rawName, 0); }
            set { EncodeName(value, rawName, 0); }
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

        /// <summary>
        /// Whether or not this character is "alive"
        /// </summary>
        public bool IsPresent
        {
            get { return isPresent; }
            set 
            {
                if (isPresent != value)
                {
                    ClearDummy();
                }
                isPresent = value; 
            }
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
            get { return (uint)(job.ActualHP(rawHP) * HPMultiplier + HPBonus); }
            set { rawHP = job.GetRawHPFromActualHP((int)((value - HPBonus) / HPMultiplier)); }
        }

        /// <summary>
        /// Gets the maximum HP this character can have with his current equipment, abilities, and job.
        /// </summary>
        public uint MaxHP
        {
            get { return (uint)(job.ActualHP(0xFFFFFF) * HPMultiplier + HPBonus); }
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
            get { return (uint)(job.ActualMP(rawMP) + MPBonus); }
            set { rawMP = job.GetRawMPFromActualMP((int)(value - MPBonus)); }
        }

        /// <summary>
        /// Gets the maximum MP this character can have with his current equipment and job.
        /// </summary>
        public uint MaxMP
        {
            get { return (uint)(job.ActualMP(0xFFFFFF) + MPBonus); }
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
            get { return (uint)(job.ActualSP(rawSP) + SpeedBonus); }
            set { rawSP = job.GetRawSPFromActualSP((int)(value - SpeedBonus)); }
        }

        /// <summary>
        /// Gets the maximum speed this character can have with his current equipment and job.
        /// </summary>
        public uint MaxSpeed
        {
            get { return (uint)(job.ActualSP(0xFFFFFF) + SpeedBonus); }
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
            get { return (uint)(job.ActualPA(rawPA) + PABonus); }
            set { rawPA = job.GetRawPAFromActualPA((int)(value - PABonus)); }
        }

        /// <summary>
        /// Gets the maximum physical attack this character can have with his current equipment and job.
        /// </summary>
        public uint MaxPA
        {
            get { return (uint)(job.ActualPA(0xFFFFFF) + PABonus); }
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
            get { return (uint)(job.ActualMA(rawMA) + MABonus); }
            set { rawMA = job.GetRawMAFromActualMA((int)(value - MABonus)); }
        }

        /// <summary>
        /// Gets the maximum magic attack this character can have with his current equipment and job.
        /// </summary>
        public uint MaxMA
        {
            get { return (uint)(job.ActualMA(0xFFFFFF) + MABonus); }
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
        /// Builds a dummy character
        /// </summary>
        public Character(int index)
        {
            this.index = (byte)index;
            BuildDummyCharacter();
        }

        /// <summary>
        /// Builds a Character from a 256 byte array
        /// </summary>
        /// <param name="charData"></param>
        /// <param name="index"></param>
        public Character(byte[] charData, int index)
        {
            this.index = (byte)index;
            if (IsValidCharacter(charData))
            {
                BuildCharacter(charData);
            }
            else if (IsGMECharacter(charData))
            {
                BuildCharacterGME(charData);
            }
            else
            {
                BuildDummyCharacter();
            }

        }

        #endregion

        #region Utilities

        private void BuildCharacterGME(byte[] charData)
        {
            spriteSet = SpriteSet.AllSprites[charData[0]];
            isPresent = (charData[1] != 0xFF);

            if (!Class.ClassDictionary.TryGetValue(charData[2], out job))
            {
                job = Class.ClassDictionary[0x4A];
            }
            unknownOffset03 = charData[3];
            gender = (Gender)(charData[4] & 0xE0);
            unknownOffset05 = charData[5];
            zodiacSign = (Zodiac)(charData[6] & 0xF0);

            if (gender == Gender.Monster)
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
                reactAbility = Ability.GetAbilityFromOffset(TwoBytesToUShort(charData[8], charData[9]));
                supportAbility = Ability.GetAbilityFromOffset(TwoBytesToUShort(charData[10], charData[11]));
                movementAbility = Ability.GetAbilityFromOffset(TwoBytesToUShort(charData[12], charData[13]));
                head = Item.GetItemAtOffset(charData[14]);
                body = Item.GetItemAtOffset(charData[15]);
                accessory = Item.GetItemAtOffset(charData[16]);
                rightHand = Item.GetItemAtOffset(charData[17]);
                rightShield = Item.GetItemAtOffset(charData[18]);
                leftHand = Item.GetItemAtOffset(charData[19]);
                leftShield = Item.GetItemAtOffset(charData[20]);
            }

            experience = charData[21];
            level = charData[22];
            bravery = charData[23];
            faith = charData[24];

            rawHP = ThreeBytesToUInt(charData[25], charData[26], charData[27]);
            rawMP = ThreeBytesToUInt(charData[28], charData[29], charData[30]);
            rawSP = ThreeBytesToUInt(charData[31], charData[32], charData[33]);
            rawPA = ThreeBytesToUInt(charData[34], charData[35], charData[36]);
            rawMA = ThreeBytesToUInt(charData[37], charData[38], charData[39]);

            byte[] jaBytes = new byte[173];

            Savegame.CopyArray(charData, jaBytes, 0x28, 0x3C);
            Savegame.CopyArray(charData, jaBytes, 0x64, 0x500 - 0x4BB, 0x0A);
            Savegame.CopyArray(charData, jaBytes, 0x6E, 0x50C - 0x4BB, 0x28);
            Savegame.CopyArray(charData, jaBytes, 0x96, 0x53A - 0x4BB, 0x28);
            jobsAndAbilities = new JobsAndAbilities(jaBytes);

            Savegame.CopyArray(charData, rawName, 0xBE, 15);

            for (int k = 0; k < 19; k++)
            {
                afterName[k] = charData[0xCD + k];
            }
        }

        /// <summary>
        /// Builds an actual character from its binary data
        /// </summary>
        /// <param name="charData"></param>
        private void BuildCharacter(byte[] charData)
        {
            spriteSet = SpriteSet.AllSprites[charData[0]];
            isPresent = (charData[1] != 0xFF);

            if (!Class.ClassDictionary.TryGetValue(charData[2], out job))
            {
                job = Class.ClassDictionary[0x4A];
            }
            unknownOffset03 = charData[3];
            gender = (Gender)(charData[4] & 0xE0);
            unknownOffset05 = charData[5];
            zodiacSign = (Zodiac)(charData[6] & 0xF0);

            if (gender == Gender.Monster)
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
                reactAbility = Ability.GetAbilityFromOffset(TwoBytesToUShort(charData[8], charData[9]));
                supportAbility = Ability.GetAbilityFromOffset(TwoBytesToUShort(charData[10], charData[11]));
                movementAbility = Ability.GetAbilityFromOffset(TwoBytesToUShort(charData[12], charData[13]));
                head = Item.GetItemAtOffset(TwoBytesToUShort(charData[14], charData[15]));
                body = Item.GetItemAtOffset(TwoBytesToUShort(charData[16], charData[17]));
                accessory = Item.GetItemAtOffset(TwoBytesToUShort(charData[18], charData[19]));
                rightHand = Item.GetItemAtOffset(TwoBytesToUShort(charData[20], charData[21]));
                rightShield = Item.GetItemAtOffset(TwoBytesToUShort(charData[22], charData[23]));
                leftHand = Item.GetItemAtOffset(TwoBytesToUShort(charData[24], charData[25]));
                leftShield = Item.GetItemAtOffset(TwoBytesToUShort(charData[26], charData[27]));
            }

            experience = charData[28];
            level = charData[29];
            bravery = charData[30];
            faith = charData[31];

            rawHP = ThreeBytesToUInt(charData[32], charData[33], charData[34]);
            rawMP = ThreeBytesToUInt(charData[35], charData[36], charData[37]);
            rawSP = ThreeBytesToUInt(charData[38], charData[39], charData[40]);
            rawPA = ThreeBytesToUInt(charData[41], charData[42], charData[43]);
            rawMA = ThreeBytesToUInt(charData[44], charData[45], charData[46]);

            byte[] jaBytes = new byte[173];

            Savegame.CopyArray(charData, jaBytes, 0x4BB - 0x48C, 173);
            jobsAndAbilities = new JobsAndAbilities(jaBytes);

            for (int i = 0; i < 15; i++)
            {
                rawName[i] = charData[0xDC + i];
            }

            for (int k = 0; k < 21; k++)
            {
                afterName[k] = charData[0xEB + k];
            }
        }

        /// <summary>
        /// Builds a character with "default" stats
        /// </summary>
        private void BuildDummyCharacter()
        {
            accessory = Item.GetItemAtOffset(0);
            body = Item.GetItemAtOffset(0);
            bravery = 0;
            experience = 0;
            faith = 0;

            gender = ((rng.Next() % 2) == 0) ? Gender.Male : Gender.Female;
            Name = string.Empty;

            head = Item.GetItemAtOffset(0);
            isPresent = false;
            job = Class.ClassDictionary[0x00];

            jobLevels = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            jobsUnlocked = new byte[] { 0x00, 0x00, 0x00 };
            jp = new ushort[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            totalJP = new ushort[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            jobsAndAbilities = new JobsAndAbilities(new byte[173]);

            leftHand = Item.GetItemAtOffset(0);
            leftShield = Item.GetItemAtOffset(0);
            level = 0;
            movementAbility = Ability.GetAbilityFromOffset(0);
            rawHP = Job.GetRawHPFromActualHP(0);
            rawMA = Job.GetRawMAFromActualMA(0);
            rawMP = Job.GetRawMPFromActualMP(0);
            rawPA = Job.GetRawPAFromActualPA(0);
            rawSP = Job.GetRawSPFromActualSP(0);
            reactAbility = Ability.GetAbilityFromOffset(0);
            rightHand = Item.GetItemAtOffset(0);
            rightShield = Item.GetItemAtOffset(0);
            secondaryAction = SecondaryAction.ActionDictionary[0x00];
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    skillsUnlocked[i, j] = 0x00;
                }
            }

            spriteSet = SpriteSet.AllSprites[0x00];
            supportAbility = Ability.GetAbilityFromOffset(0);
            unknownOffset03 = 0x00;
            unknownOffset05 = 0x00;
            zodiacSign = (Zodiac)0;
            OnProposition = false;
            Kills = 0;

            isDummy = true;
        }

        /// <summary>
        /// Takes two little endian bytes and turns them into a ushort
        /// </summary>
        /// <param name="one">least significant byte</param>
        /// <param name="two">most significant byte</param>
        /// <returns></returns>
        private static ushort TwoBytesToUShort(byte one, byte two)
        {
            return (ushort)((two << 8) + one);
        }

        /// <summary>
        /// Takes three little endian bytes and turns them into a uint
        /// </summary>
        /// <param name="one">least significant byte</param>
        /// <param name="two"></param>
        /// <param name="three">most significant byte</param>
        /// <returns></returns>
        private static uint ThreeBytesToUInt(byte one, byte two, byte three)
        {
            return (uint)(((uint)three << 16) + ((uint)two << 8) + (uint)one);
        }

        /// <summary>
        /// Checks valid ranges of some bytes to determine if the byte array represents a valid character
        /// </summary>
        private static bool IsValidCharacter(byte[] charData)
        {
            return (charData.Length == 0x100) 
                && ((charData[0] > 0) && (charData[0] < SpriteSet.AllSprites.Count))
                && (((charData[1] >= 0) && (charData[1] < 28)) || (charData[1] == 0xFF))
                && ((charData[2] > 0) && (Class.ClassDictionary.ContainsKey(charData[2])))
                && (Enum.IsDefined(typeof(Gender), charData[4] & 0xE0))
                && (Enum.IsDefined(typeof(Zodiac), charData[6] & 0xF0))
                && (((Gender)charData[4] == Gender.Monster) ||
                      ((SecondaryAction.ActionDictionary.ContainsKey(charData[7]))
                    && (TwoBytesToUShort(charData[8], charData[9]) < 512)
                    && (TwoBytesToUShort(charData[10], charData[11]) < 512)
                    && (TwoBytesToUShort(charData[12], charData[13]) < 512)
                    && (TwoBytesToUShort(charData[14], charData[15]) < 316)
                    && (TwoBytesToUShort(charData[16], charData[17]) < 316)
                    && (TwoBytesToUShort(charData[18], charData[19]) < 316)
                    && (TwoBytesToUShort(charData[20], charData[21]) < 316)
                    && (TwoBytesToUShort(charData[22], charData[23]) < 316)
                    && (TwoBytesToUShort(charData[24], charData[25]) < 316)
                    && (TwoBytesToUShort(charData[26], charData[27]) < 316)))
                && (charData[28] < 100)
                && ((charData[29] >= 0) && (charData[29] < 100))
                && ((charData[30] >= 0) && (charData[30] <= 100))
                && ((charData[31] >= 0) && (charData[31] <= 100));
        }

        /// <summary>
        /// Checks valid ranges of some bytes to determien if the byte array represents a FFT PSX character
        /// </summary>
        private static bool IsGMECharacter(byte[] charData)
        {
            return (charData.Length == 0xE0)
                && (((charData[1] >= 0) && (charData[1] < 20)) || (charData[1] == 0xFF))
                && ((charData[2] > 0) && (Class.ClassDictionary.ContainsKey(charData[2])))
                && (Enum.IsDefined(typeof(Gender), charData[4] & 0xE0))
                && (Enum.IsDefined(typeof(Zodiac), charData[6] & 0xF0))
                && (((Gender)charData[4] == Gender.Monster) ||
                      ((SecondaryAction.ActionDictionary.ContainsKey(charData[7]))
                    && (TwoBytesToUShort(charData[8], charData[9]) < 512)
                    && (TwoBytesToUShort(charData[10], charData[11]) < 512)
                    && (TwoBytesToUShort(charData[12], charData[13]) < 512)))
                && (charData[21] < 100)
                && (charData[22] < 100)
                && (charData[23] <= 100)
                && (charData[24] <= 100)
                && (((charData[0x64] & 0xF0) >> 4) <= 8)
                && ((charData[0x64] & 0x0F) <= 8)
                && (((charData[0x65] & 0xF0) >> 4) <= 8)
                && ((charData[0x65] & 0x0F) <= 8)
                && (((charData[0x66] & 0xF0) >> 4) <= 8)
                && ((charData[0x66] & 0x0F) <= 8)
                && (((charData[0x67] & 0xF0) >> 4) <= 8)
                && ((charData[0x67] & 0x0F) <= 8)
                && (((charData[0x68] & 0xF0) >> 4) <= 8)
                && ((charData[0x68] & 0x0F) <= 8)
                && (((charData[0x69] & 0xF0) >> 4) <= 8)
                && ((charData[0x69] & 0x0F) <= 8)
                && (((charData[0x6A] & 0xF0) >> 4) <= 8)
                && ((charData[0x6A] & 0x0F) <= 8)
                && (((charData[0x6B] & 0xF0) >> 4) <= 8)
                && ((charData[0x6B] & 0x0F) <= 8)
                && (((charData[0x6C] & 0xF0) >> 4) <= 8)
                && ((charData[0x6C] & 0x0F) <= 8)
                && (((charData[0x6D] & 0xF0) >> 4) <= 8)
                && ((charData[0x6D] & 0x0F) <= 8)
                && (TwoBytesToUShort(charData[0x6E], charData[0x6F]) <= 9999)
                && (TwoBytesToUShort(charData[0x70], charData[0x71]) <= 9999)
                && (TwoBytesToUShort(charData[0x72], charData[0x73]) <= 9999)
                && (TwoBytesToUShort(charData[0x74], charData[0x75]) <= 9999)
                && (TwoBytesToUShort(charData[0x76], charData[0x77]) <= 9999)
                && (TwoBytesToUShort(charData[0x78], charData[0x79]) <= 9999)
                && (TwoBytesToUShort(charData[0x7A], charData[0x7B]) <= 9999)
                && (TwoBytesToUShort(charData[0x7C], charData[0x7D]) <= 9999)
                && (TwoBytesToUShort(charData[0x7E], charData[0x7F]) <= 9999)
                && (TwoBytesToUShort(charData[0x80], charData[0x81]) <= 9999)
                && (TwoBytesToUShort(charData[0x82], charData[0x83]) <= 9999)
                && (TwoBytesToUShort(charData[0x84], charData[0x85]) <= 9999)
                && (TwoBytesToUShort(charData[0x86], charData[0x87]) <= 9999)
                && (TwoBytesToUShort(charData[0x88], charData[0x89]) <= 9999)
                && (TwoBytesToUShort(charData[0x8A], charData[0x8B]) <= 9999)
                && (TwoBytesToUShort(charData[0x8C], charData[0x8D]) <= 9999)
                && (TwoBytesToUShort(charData[0x8E], charData[0x8F]) <= 9999)
                && (TwoBytesToUShort(charData[0x90], charData[0x91]) <= 9999)
                && (TwoBytesToUShort(charData[0x92], charData[0x93]) <= 9999)
                && (TwoBytesToUShort(charData[0x94], charData[0x95]) <= 9999)
                && (TwoBytesToUShort(charData[0x96], charData[0x97]) <= 9999)
                && (TwoBytesToUShort(charData[0x98], charData[0x99]) <= 9999)
                && (TwoBytesToUShort(charData[0x9A], charData[0x9B]) <= 9999)
                && (TwoBytesToUShort(charData[0x9C], charData[0x9D]) <= 9999)
                && (TwoBytesToUShort(charData[0x9E], charData[0x9F]) <= 9999)
                && (TwoBytesToUShort(charData[0xA0], charData[0xA1]) <= 9999)
                && (TwoBytesToUShort(charData[0xA2], charData[0xA3]) <= 9999)
                && (TwoBytesToUShort(charData[0xA4], charData[0xA5]) <= 9999)
                && (TwoBytesToUShort(charData[0xA6], charData[0xA7]) <= 9999)
                && (TwoBytesToUShort(charData[0xA8], charData[0xA9]) <= 9999)
                && (TwoBytesToUShort(charData[0xAA], charData[0xAB]) <= 9999)
                && (TwoBytesToUShort(charData[0xAC], charData[0xAD]) <= 9999)
                && (TwoBytesToUShort(charData[0xAE], charData[0xAF]) <= 9999)
                && (TwoBytesToUShort(charData[0xB0], charData[0xB1]) <= 9999)
                && (TwoBytesToUShort(charData[0xB2], charData[0xB3]) <= 9999)
                && (TwoBytesToUShort(charData[0xB4], charData[0xB5]) <= 9999)
                && (TwoBytesToUShort(charData[0xB6], charData[0xB7]) <= 9999)
                && (TwoBytesToUShort(charData[0xB8], charData[0xB9]) <= 9999)
                && (TwoBytesToUShort(charData[0xBA], charData[0xBB]) <= 9999)
                && (TwoBytesToUShort(charData[0xBC], charData[0xBD]) <= 9999);
        }

        /// <summary>
        /// Decodes a name from FFT's dumb character encoding
        /// </summary>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static string DecodeName(byte[] source, int start)
        {
            StringBuilder sb = new StringBuilder();
            int k = start;
            while (source[k] != 0xFE)
            {
                sb.Append(characterMap[source[k]]);
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
        public static void EncodeName(string name, byte[] destination, int start)
        {
            int i = 0;
            List<char> charList = new List<char>(characterMap);

            foreach (char c in name)
            {
                destination[i + start] = (byte)(charList.IndexOf(c) & 0xFF);
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
            result[1] = (byte)(isPresent ? index : 0xFF);

            if (isDummy)
            {
                return result;
            }

            result[0] = spriteSet.Value;
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

            Array.Copy(head.ToByte(), 0, result, 14, 2);
            Array.Copy(body.ToByte(), 0, result, 16, 2);
            Array.Copy(accessory.ToByte(), 0, result, 18, 2);
            Array.Copy(rightHand.ToByte(), 0, result, 20, 2);
            Array.Copy(rightShield.ToByte(), 0, result, 22, 2);
            Array.Copy(leftHand.ToByte(), 0, result, 24, 2);
            Array.Copy(leftShield.ToByte(), 0, result, 26, 2);
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

            Array.Copy(jobsAndAbilities.ToByteArray(), 0, result, 47, 173);
            Array.Copy(rawName, 0, result, 0xDC, 15);

            if (Name == string.Empty)
            {
                result[0xDC] = 0x00;
            }

            Array.Copy(afterName, 0, result, 0xEB, 21);

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

        /// <summary>
        /// Clones this character
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Character(this.ToByteArray(), this.index);
        }

        public string GetRandomName()
        {
            switch (gender)
            {
                case Gender.Female:
                    return RandomNames.GetRandomFemaleName();
                case Gender.Male:
                    return RandomNames.GetRandomMaleName();
                case Gender.Monster:
                    return RandomNames.GetRandomMonsterName();
                default:
                    // Can't happen...
                    return string.Empty;
            }
        }

        public void ClearDummy()
        {
            isDummy = false;
        }

        #endregion
    }

    public class BadCharacterDataException : Exception
    {
    }
}