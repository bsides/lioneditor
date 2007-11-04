using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace LionEditor.Datatypes
{
    class Class
    {
        byte num;
        public byte Byte
        {
            get { return num; }
        }

        string name;
        public string Name
        {
            get { return name; }
        }

        string command;
        public string Command { get { return command; } }

        int mpm;
        public int MPModifier { get { return mpm; } }

        public int ActualMP( int rawMP )
        {
            return multiply( rawMP, mpm );
        }

        int hpm;
        public int HPModifier { get { return hpm; } }

        public int ActualHP( int rawHP )
        {
            return multiply( rawHP, hpm );
        }

        int spm;
        public int SPModifier { get { return spm; } }

        public int ActualSP( int rawSP )
        {
            return multiply( rawSP, spm );
        }

        int pam;
        public int PAModifier { get { return pam; } }

        public int ActualPA( int rawPA )
        {
            return multiply( rawPA, pam );
        }

        int mam;
        public int MAModifier { get { return mam; } }

        public int ActualMA( int rawMA )
        {
            return multiply( rawMA, mam );
        }

        int move;
        public int Move { get { return move; } }

        int cev;
        public int CEvade { get { return cev; } }

        int hpc;
        public int HPConstant { get { return hpc; } }

        int mpc;
        public int MPConstant { get { return mpc; } }

        int spc;
        public int SPConstant { get { return spc; } }

        int pac;
        public int PAConstant { get { return pac; } }

        int mac;
        public int MAConstant { get { return mac; } }

        int jump;
        public int Jump { get { return jump; } }

        string type;
        public string Type { get { return type; } }

        public struct stats
        {
            public int rawHP;
            public int HP;

            public int rawMP;
            public int MP;

            public int rawMA;
            public int MA;

            public int rawPA;
            public int PA;

            public int rawSp;
            public int Sp;
        }

        public stats LevelUp( int currentLevel, int currentRawHP, int currentRawMP, int currentRawMA, int currentRawPA, int currentRawSp )
        {
            stats stats = new stats();

            stats.rawHP += currentRawHP / (hpc + currentLevel);
            stats.HP = multiply( stats.rawHP, hpm );

            stats.rawMP += currentRawMP / (mpc + currentLevel);
            stats.MP = multiply( stats.rawMP, mpm );

            stats.rawMA += currentRawMA / (mac + currentLevel);
            stats.MA = multiply( stats.rawMA, mam );

            stats.rawPA += currentRawPA / (pac + currentLevel);
            stats.PA = multiply( stats.rawPA, pam );

            stats.rawSp += currentRawSp / (spc + currentLevel);
            stats.Sp = multiply( stats.rawSp, spm );

            return stats;
        }

        public stats LevelDown( int currentLevel, int currentRawHP, int currentRawMP, int currentRawMA, int currentRawPA, int currentRawSp )
        {
            stats stats = new stats();

            stats.rawHP -= currentRawHP / (hpc + currentLevel-1);
            stats.HP = multiply( stats.rawHP, hpm );

            stats.rawMP -= currentRawMP / (mpc + currentLevel-1);
            stats.MP = multiply( stats.rawMP, mpm );

            stats.rawMA -= currentRawMA / (mac + currentLevel-1);
            stats.MA = multiply( stats.rawMA, mam );

            stats.rawPA -= currentRawPA / (pac + currentLevel-1);
            stats.PA = multiply( stats.rawPA, pam );

            stats.rawSp -= currentRawSp / (spc + currentLevel-1);
            stats.Sp = multiply( stats.rawSp, spm );

            return stats;
        }

        public Class( byte value, string name, int hpModifier, int hpConstant, int mpModifier, int mpConstant, int spModifier, int spConstant,
            int paModifier, int paConstant, int maModifier, int maConstant, int move, int jump, int cev, string type, string command )
        {
            this.num = value;
            this.name = name;
            this.hpm = hpModifier;
            this.hpc = hpConstant;
            this.mpm = mpModifier;
            this.mpc = mpConstant;
            this.spm = spModifier;
            this.spc = spConstant;
            this.pam = paModifier;
            this.pac = paConstant;
            this.mam = maModifier;
            this.mac = maConstant;
            this.move = move;
            this.jump = jump;
            this.cev = cev;
            this.type = type;
            this.command = command;
        }

        public Class( XmlNode classNode )
        {
            this.num = byte.Parse( classNode.SelectSingleNode( "//byte" ).InnerXml, System.Globalization.NumberStyles.HexNumber );
            this.name = classNode.SelectSingleNode( "//name" ).InnerXml;
            this.command = classNode.SelectSingleNode( "//command" ).InnerXml;
            this.hpm = Convert.ToInt32( classNode.SelectSingleNode( "//hpm" ).InnerXml );
            this.spm = Convert.ToInt32( classNode.SelectSingleNode( "//spm" ).InnerXml );
            this.pam = Convert.ToInt32( classNode.SelectSingleNode( "//pam" ).InnerXml );
            this.mam = Convert.ToInt32( classNode.SelectSingleNode( "//mam" ).InnerXml );
            this.move = Convert.ToInt32( classNode.SelectSingleNode( "//move" ).InnerXml );
            this.cev = Convert.ToInt32( classNode.SelectSingleNode( "//cev" ).InnerXml );
            this.hpc = Convert.ToInt32( classNode.SelectSingleNode( "//hpc" ).InnerXml );
            this.mpc = Convert.ToInt32( classNode.SelectSingleNode( "//mpc" ).InnerXml );
            this.spc = Convert.ToInt32( classNode.SelectSingleNode( "//spc" ).InnerXml );
            this.pac = Convert.ToInt32( classNode.SelectSingleNode( "//pac" ).InnerXml );
            this.mac = Convert.ToInt32( classNode.SelectSingleNode( "//mac" ).InnerXml );
            this.jump = Convert.ToInt32( classNode.SelectSingleNode( "//jump" ).InnerXml );
            this.type = classNode.SelectSingleNode( "//type" ).InnerXml;
        }


        private int multiply( int rawVal, int multiplier )
        {
            int result = rawVal * multiplier / 1638400;
            if( result < 1 )
            {
                result = 1;
            }

            return result;
        }

        private int divide( int actualVal, int multiplier )
        {
            return (actualVal * 1638400) / multiplier);
        }
    
    
    }
}
