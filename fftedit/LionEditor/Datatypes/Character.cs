using System;
using System.Text;
using System.Collections.Generic;

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

        byte spriteSet;
        byte index;
        string name;
        Class job;
        bool isGuest;
        Gender gender;
        Zodiac zodiacSign;

        SecondaryAction secondaryAction;

        Ability reactAbility;
        Ability supportAbility;
        Ability movementAbility;

        Item head;
        Item body;
        Item accessory;
        Item rightHand;
        Item rightShield;
        Item leftHand;
        Item leftShield;

        byte experience;
        byte level;
        byte brave;
        byte faith;

        int rawHP;
        int rawMP;
        int rawSP;
        int rawPA;
        int rawMA;

        byte unknownOffset03;
        byte unknownOffset05;

        byte[] jobsUnlocked = new byte[3];
        byte[,] skillsUnlocked = new byte[22, 3];

        byte[] jobLevels = new byte[12];
        ushort[] JP = new ushort[23];
        ushort[] totalJP = new ushort[23];

        byte[] afterName = new byte[21];

        /// <summary>
        /// Builds a Character from a 256 byte array
        /// </summary>
        /// <param name="charData"></param>
        public Character( byte[] charData )
        {
            spriteSet = charData[0];
            index = charData[1];
            job = Class.ClassDictionary[charData[2]];
            unknownOffset03 = charData[3];
            gender = (Gender)charData[4];
            unknownOffset05 = charData[5];
            zodiacSign = (Zodiac)charData[6];

            secondaryAction = SecondaryAction.ActionDictionary[charData[7]];

            reactAbility = new Ability( (ushort)(charData[9] << 8 + charData[8]) );
            supportAbility = new Ability( (ushort)(charData[11] << 8 + charData[10]) );
            movementAbility = new Ability( (ushort)(charData[13] << 8 + charData[12]) );
            head = new Item( (ushort)(charData[15] << 8 + charData[14]) );
            body = new Item( (ushort)(charData[17] << 8 + charData[16]) );
            accessory = new Item( (ushort)(charData[19] << 8 + charData[18]) );
            rightHand = new Item( (ushort)(charData[21] << 8 + charData[20]) );
            rightShield = new Item( (ushort)(charData[23] << 8 + charData[22]) );
            leftHand = new Item( (ushort)(charData[25] << 8 + charData[24]) );
            leftShield = new Item( (ushort)(charData[27] << 8 + charData[26]) );
            experience = charData[28];
            level = charData[29];
            brave = charData[30];
            faith = charData[31];

            rawHP = charData[34] << 16 + charData[33] << 8 + charData[32];
            rawMP = charData[37] << 16 + charData[36] << 8 + charData[35];
            rawSP = charData[40] << 16 + charData[39] << 8 + charData[38];
            rawPA = charData[43] << 16 + charData[42] << 8 + charData[41];
            rawMA = charData[46] << 16 + charData[45] << 8 + charData[44];

            jobsUnlocked[0] = charData[47];
            jobsUnlocked[1] = charData[48];
            jobsUnlocked[2] = charData[49];

            for( int i = 0; i < 22; i++ )
            {
                for( int j = 0; j < 3; j++ )
                {
                    skillsUnlocked[i, j] = charData[50 + 3 * i + j];
                }
            }

            for (int i = 0; i < 12; i++)
            {
                jobLevels[i] = charData[0x74 + i];
            }

            for( int i = 0; i < 23; i++ )
            {
                JP[i] = (ushort)(charData[0x80 + 2 * i + 1] << 8 + charData[0x80 + 2 * i]);
            }
            for( int i = 0; i < 23; i++ )
            {
                totalJP[i] = (ushort)(charData[0xAE + 2 * i + 1] << 8 + charData[0xAE + 2 * i]);
            }

            StringBuilder sb = new StringBuilder();

            int k = 0xDC;
            while( charData[k] != 0xFE )
            {
                sb.Append( characterMap[charData[k]] );
            }

            name = sb.ToString();

            for( k = 0; k < 21; k++ )
            {
                afterName[k] = charData[0xEB + k];
            }
        }


        public byte[] ToByteArray()
        {
            byte[] result = new byte[256];
            result[0] = spriteSet;
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
            result[30] = brave;
            result[31] = faith;

            result[32] = (byte)(rawHP & 0xFF);
            result[33] = (byte)((rawHP & 0xFF00) >> 8);
            result[34] = (byte)((rawHP & 0xFF0000) >> 8);

            result[35] = (byte)(rawMP & 0xFF);
            result[36] = (byte)((rawMP & 0xFF00) >> 8);
            result[37] = (byte)((rawMP & 0xFF0000) >> 8);

            result[38] = (byte)(rawSP & 0xFF);
            result[39] = (byte)((rawSP & 0xFF00) >> 8);
            result[40] = (byte)((rawSP & 0xFF0000) >> 8);

            result[41] = (byte)(rawPA & 0xFF);
            result[42] = (byte)((rawPA & 0xFF00) >> 8);
            result[43] = (byte)((rawPA & 0xFF0000) >> 8);

            result[44] = (byte)(rawMA & 0xFF);
            result[45] = (byte)((rawMA & 0xFF00) >> 8);
            result[46] = (byte)((rawMA & 0xFF0000) >> 8);

            result[47] = jobsUnlocked[0];
            result[48] = jobsUnlocked[1];
            result[49] = jobsUnlocked[2];

            for( int i = 0; i < 22; i++ )
            {
                for( int j = 0; j < 3; j++ )
                {
                    result[50 + 3 * i + j] = skillsUnlocked[i, j];
                }
            }

            for( int i = 0; i < 12; i++ )
            {
                result[0x74 + i] = jobLevels[i];
            }

            for( int i = 0; i < 23; i++ )
            {
                result[0x80 + 2 * i] = (byte)(JP[i] & 0xFF);
                result[0x80 + 2 * i + 1] = (byte)((JP[i] & 0xFF00) >> 8);
            }

            for( int i = 0; i < 23; i++ )
            {
                result[0xAE + 2 * i] = (byte)(totalJP[i] & 0xFF);
                result[0xAE + 2 * i + 1] = (byte)((totalJP[i] & 0xFF00) >> 8);
            }

            List<char> charList = new List<char>( characterMap );

            int k = 0xDC;
            foreach( char c in name )
            {
                result[k] = (byte)(charList.IndexOf( c ) & 0xFF);
                k++;
            }
            result[k] = 0xFE;

            for( k = 0; k < 21; k++ )
            {
                result[0xEB + k] = afterName[k];
            }

            return result;
        }
    }


}