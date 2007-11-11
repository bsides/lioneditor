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
        public static char[] characterMap = new char[] {
            '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F',
            'G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V',
            'W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l',
            'm','n','o','p','q','r','s','t','u','v','w','x','y','z','!','_',
            '?','_','+','_','/','_',':','_','_','_','_','_','_','_','_','_',
            '_','_','_','_','_','_','_','_','_','_','_','_','_','_','_','.',
            '_','_','_','_','_','_','_','_','_','_','_','_','_','_','_','_',
            '_','_','_','_','_','_','_','_','_','_','_','_','_','_','_','_',
            '_','_','_','_','_','_','_','_','_','_','_','-','_','(',')','_',
            '_','"','_','\'','_',' ','_','_','_','_','_','_','_','_','_','_',
            '_','_','_','_','_','_','_','_','_','_','_','_','_','_','_','_',
            '_','_','_','_','_','*','_','_','_','_','_','_','_','_','_','_',
            '_','_','_','_','_','_','_','_','_','_','_','_','_','_','_','_',
            '_','_','_','_','_','_','_','_','_','_','_','_','_','_','_','_',
            '_','_','_','_','_','_','_','_','_','_','_','_','_','_','_','_',
            '_','_','_','_','_','_','_','_','_','_','_','_','_','_','_','_' };

        private byte m_spriteSet;
        public byte SpriteSet
        {
        	get { return m_spriteSet; }
            private set { m_spriteSet = value; }
        }

        private byte m_index;
        public byte Index
        {
        	get { return m_index; }
            private set { m_index = value; }
        }

        private string m_name;
        public string Name
        {
        	get { return m_name; }
            private set { m_name = value; }
        }

        private Class m_job;
        public Class Job
        {
        	get { return m_job; }
            private set { m_job = value; }
        }

        private bool m_isGuest;
        public bool IsGuest
        {
        	get { return m_isGuest; }
            private set { m_isGuest = value; }
        }

        private Gender m_gender;
        public Gender Gender
        {
        	get { return m_gender; }
            private set { m_gender = value; }
        }

        private Zodiac m_zodiacSign;
        public Zodiac ZodiacSign
        {
        	get { return m_zodiacSign; }
        	private set { m_zodiacSign = value; }
        }


        private SecondaryAction m_secondaryAction;
        public SecondaryAction SecondaryAction
        {
        	get { return m_secondaryAction; }
        	private set { m_secondaryAction = value; }
        }


        private Ability m_ReactAbility;
        public Ability ReactAbility
        {
        	get { return m_ReactAbility; }
        	private set { m_ReactAbility = value; }
        }

        private Ability m_SupportAbility;
        public Ability SupportAbility
        {
        	get { return m_SupportAbility; }
        	private set { m_SupportAbility = value; }
        }

        private Ability m_MovementAbility;
        public Ability MovementAbility
        {
        	get { return m_MovementAbility; }
        	private set { m_MovementAbility = value; }
        }


        private Item m_Head;
        public Item Head
        {
        	get { return m_Head; }
        	private set { m_Head = value; }
        }

        private Item m_Body;
        public Item Body
        {
        	get { return m_Body; }
        	private set { m_Body = value; }
        }

        private Item m_Accessory;
        public Item Accessory
        {
        	get { return m_Accessory; }
        	private set { m_Accessory = value; }
        }

        private Item m_RightHand;
        public Item RightHand
        {
        	get { return m_RightHand; }
        	private set { m_RightHand = value; }
        }

        private Item m_RightShield;
        public Item RightShield
        {
        	get { return m_RightShield; }
        	private set { m_RightShield = value; }
        }

        private Item m_LeftHand;
        public Item LeftHand
        {
        	get { return m_LeftHand; }
        	private set { m_LeftHand = value; }
        }

        private Item m_LeftShield;
        public Item LeftShield
        {
        	get { return m_LeftShield; }
        	private set { m_LeftShield = value; }
        }


        private byte m_Experience;
        public byte Experience
        {
        	get { return m_Experience; }
        	private set { m_Experience = value; }
        }

        private byte m_Level;
        public byte Level
        {
        	get { return m_Level; }
        	private set { m_Level = value; }
        }

        private byte m_Brave;
        public byte Brave
        {
        	get { return m_Brave; }
        	private set { m_Brave = value; }
        }

        private byte m_Faith;
        public byte Faith
        {
        	get { return m_Faith; }
        	private set { m_Faith = value; }
        }


        private uint m_RawHP;
        public uint RawHP
        {
        	get { return m_RawHP; }
        	private set { m_RawHP = value; }
        }

        private uint m_RawMP;
        public uint RawMP
        {
        	get { return m_RawMP; }
        	private set { m_RawMP = value; }
        }

        private uint m_RawSP;
        public uint RawSP
        {
        	get { return m_RawSP; }
        	private set { m_RawSP = value; }
        }

        private uint m_RawPA;
        public uint RawPA
        {
        	get { return m_RawPA; }
        	private set { m_RawPA = value; }
        }

        private uint m_RawMA;
        public uint RawMA
        {
        	get { return m_RawMA; }
        	private set { m_RawMA = value; }
        }


        private byte m_UnknownOffset03;
        public byte UnknownOffset03
        {
        	get { return m_UnknownOffset03; }
        	private set { m_UnknownOffset03 = value; }
        }

        private byte m_UnknownOffset05;
        public byte UnknownOffset05
        {
        	get { return m_UnknownOffset05; }
        	private set { m_UnknownOffset05 = value; }
        }


        private byte[] m_JobsUnlocked = new byte[3];
        public byte[] JobsUnlocked
        {
        	get { return m_JobsUnlocked; }
        	private set { m_JobsUnlocked = value; }
        }

        private byte[,] m_SkillsUnlocked = new byte[22, 3];
        public byte[,] SkillsUnlocked
        {
        	get { return m_SkillsUnlocked; }
        	private set { m_SkillsUnlocked = value; }
        }


        private byte[] m_JobLevels = new byte[12];
        public byte[] JobLevels
        {
        	get { return m_JobLevels; }
        	private set { m_JobLevels = value; }
        }

        private ushort[] m_JP = new ushort[23];
        public ushort[] JP
        {
        	get { return m_JP; }
        	private set { m_JP = value; }
        }

        private ushort[] m_TotalJP = new ushort[23];
        public ushort[] TotalJP
        {
        	get { return m_TotalJP; }
        	private set { m_TotalJP = value; }
        }


        private byte[] m_AfterName = new byte[21];
        public byte[] AfterName
        {
        	get { return m_AfterName; }
        	private set { m_AfterName = value; }
        }


        /// <summary>
        /// Builds a Character from a 256 byte array
        /// </summary>
        /// <param name="charData"></param>
        public Character( byte[] charData )
        {
            SpriteSet = charData[0];
            Index = charData[1];
            Job = Class.ClassDictionary[charData[2]];
            UnknownOffset03 = charData[3];
            Gender = (Gender)charData[4];
            UnknownOffset05 = charData[5];
            ZodiacSign = (Zodiac)charData[6];

            SecondaryAction = SecondaryAction.ActionDictionary[charData[7]];

            ReactAbility = new Ability( (ushort)((charData[9] << 8) + charData[8]) );
            SupportAbility = new Ability( (ushort)((charData[11] << 8) + charData[10]) );
            MovementAbility = new Ability( (ushort)((charData[13] << 8) + charData[12]) );
            ushort I = charData[17];
            ushort J = (ushort)(I << 8);
            ushort K = charData[16];
            ushort L = (ushort)(J + K);
            Head = new Item( (ushort)((ushort)(charData[15] << 8) + charData[14]) );
            Body = new Item( (ushort)((ushort)(charData[17] << 8) + charData[16]) );
            Accessory = new Item( (ushort)((ushort)(charData[19] << 8) + charData[18]) );
            RightHand = new Item( (ushort)((ushort)(charData[21] << 8) + charData[20]) );
            RightShield = new Item( (ushort)((ushort)(charData[23] << 8) + charData[22]) );
            LeftHand = new Item( (ushort)((ushort)(charData[25] << 8) + charData[24]) );
            LeftShield = new Item( (ushort)((ushort)(charData[27] << 8) + charData[26]) );
            Experience = charData[28];
            Level = charData[29];
            Brave = charData[30];
            Faith = charData[31];

            RawHP = (uint)(((uint)charData[34] << 16) + ((uint)charData[33] << 8) + (uint)charData[32]);
            RawMP = (uint)(((uint)charData[37] << 16) + ((uint)charData[36] << 8) + (uint)charData[35]);
            RawSP = (uint)(((uint)charData[40] << 16) + ((uint)charData[39] << 8) + (uint)charData[38]);
            RawPA = (uint)(((uint)charData[43] << 16) + ((uint)charData[42] << 8) + (uint)charData[41]);
            RawMA = (uint)(((uint)charData[46] << 16) + ((uint)charData[45] << 8) + (uint)charData[44]);

            JobsUnlocked[0] = charData[47];
            JobsUnlocked[1] = charData[48];
            JobsUnlocked[2] = charData[49];

            for( int i = 0; i < 22; i++ )
            {
                for( int j = 0; j < 3; j++ )
                {
                    SkillsUnlocked[i, j] = charData[50 + 3 * i + j];
                }
            }

            for (int i = 0; i < 12; i++)
            {
                JobLevels[i] = charData[0x74 + i];
            }

            for( int i = 0; i < 23; i++ )
            {
                JP[i] = (ushort)((ushort)(charData[0x80 + 2 * i + 1] << 8) + charData[0x80 + 2 * i]);
            }
            for( int i = 0; i < 23; i++ )
            {
                TotalJP[i] = (ushort)((ushort)(charData[0xAE + 2 * i + 1] << 8) + charData[0xAE + 2 * i]);
            }

            StringBuilder sb = new StringBuilder();

            int k = 0xDC;
            while( charData[k] != 0xFE )
            {
                sb.Append( characterMap[charData[k]] );
                k++;
            }

            Name = sb.ToString();

            for( k = 0; k < 21; k++ )
            {
                AfterName[k] = charData[0xEB + k];
            }
        }


        public byte[] ToByteArray()
        {
            byte[] result = new byte[256];
            result[0] = SpriteSet;
            result[1] = Index;
            result[2] = Job.Byte;
            result[3] = UnknownOffset03;
            result[4] = (byte)Gender;
            result[5] = UnknownOffset05;
            result[6] = (byte)ZodiacSign;
            result[7] = SecondaryAction.Byte;
            result[8] = (byte)(ReactAbility.Value & 0xFF);
            result[9] = (byte)((ReactAbility.Value & 0xFF00) >> 8);
            result[10] = (byte)(SupportAbility.Value & 0xFF);
            result[11] = (byte)((SupportAbility.Value & 0xFF00) >> 8);
            result[12] = (byte)(MovementAbility.Value & 0xFF);
            result[13] = (byte)((MovementAbility.Value & 0xFF00) >> 8);

            result[14] = Head.ToByte()[0];
            result[15] = Head.ToByte()[1];
            result[16] = Body.ToByte()[0];
            result[17] = Body.ToByte()[1];
            result[18] = Accessory.ToByte()[0];
            result[19] = Accessory.ToByte()[1];
            result[20] = RightHand.ToByte()[0];
            result[21] = RightHand.ToByte()[1];
            result[22] = RightShield.ToByte()[0];
            result[23] = RightShield.ToByte()[1];
            result[24] = LeftHand.ToByte()[0];
            result[25] = LeftHand.ToByte()[1];
            result[26] = LeftShield.ToByte()[0];
            result[27] = LeftShield.ToByte()[1];
            result[28] = Experience;
            result[29] = Level;
            result[30] = Brave;
            result[31] = Faith;

            result[32] = (byte)(RawHP & 0xFF);
            result[33] = (byte)((RawHP & 0xFF00) >> 8);
            result[34] = (byte)((RawHP & 0xFF0000) >> 16);

            result[35] = (byte)(RawMP & 0xFF);
            result[36] = (byte)((RawMP & 0xFF00) >> 8);
            result[37] = (byte)((RawMP & 0xFF0000) >> 16);

            result[38] = (byte)(RawSP & 0xFF);
            result[39] = (byte)((RawSP & 0xFF00) >> 8);
            result[40] = (byte)((RawSP & 0xFF0000) >> 16);

            result[41] = (byte)(RawPA & 0xFF);
            result[42] = (byte)((RawPA & 0xFF00) >> 8);
            result[43] = (byte)((RawPA & 0xFF0000) >> 16);

            result[44] = (byte)(RawMA & 0xFF);
            result[45] = (byte)((RawMA & 0xFF00) >> 8);
            result[46] = (byte)((RawMA & 0xFF0000) >> 16);

            result[47] = JobsUnlocked[0];
            result[48] = JobsUnlocked[1];
            result[49] = JobsUnlocked[2];

            for( int i = 0; i < 22; i++ )
            {
                for( int j = 0; j < 3; j++ )
                {
                    result[50 + 3 * i + j] = SkillsUnlocked[i, j];
                }
            }

            for( int i = 0; i < 12; i++ )
            {
                result[0x74 + i] = JobLevels[i];
            }

            for( int i = 0; i < 23; i++ )
            {
                result[0x80 + 2 * i] = (byte)(JP[i] & 0xFF);
                result[0x80 + 2 * i + 1] = (byte)((JP[i] & 0xFF00) >> 8);
            }

            for( int i = 0; i < 23; i++ )
            {
                result[0xAE + 2 * i] = (byte)(TotalJP[i] & 0xFF);
                result[0xAE + 2 * i + 1] = (byte)((TotalJP[i] & 0xFF00) >> 8);
            }

            List<char> charList = new List<char>( characterMap );

            int k = 0xDC;
            foreach( char c in Name )
            {
                result[k] = (byte)(charList.IndexOf( c ) & 0xFF);
                k++;
            }
            result[k] = 0xFE;

            for( k = 0; k < 21; k++ )
            {
                result[0xEB + k] = AfterName[k];
            }

            return result;
        }
    }


}