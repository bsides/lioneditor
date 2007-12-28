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
using System.Xml;
using LionEditor.Properties;

namespace LionEditor
{
    /// <summary>
    /// Represents a Job/Class a character can have and the stat modifiers that go along with that class
    /// </summary>
    public class Class
    {
        #region Fields

        private static List<Class> classList;
        private static Dictionary<byte, Class> classDict;
        private byte num;
        private string name;
        private string command;
        private int mpm;
        private int hpm;
        private int spm;
        private int pam;
        private int mam;
        private int move;
        private int cev;
        private int hpc;
        private int mpc;
        private int spc;
        private int pac;
        private int mac;
        private int jump;
        private string type;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of all Classes
        /// </summary>
        public static List<Class> ClassList
        {
            get
            {
                if( classList == null )
                {
                    LoadClasses();
                }
                return classList;
            }
        }

        /// <summary>
        /// Gets a dictionary of all Classes, where the Key is the Class's byte value
        /// </summary>
        public static Dictionary<byte, Class> ClassDictionary
        {
            get
            {
                if( classDict == null )
                {
                    LoadClasses();
                }

                return classDict;
            }
        }

        /// <summary>
        /// Gets the byte for this class
        /// </summary>
        public byte Byte
        {
            get { return num; }
        }

        /// <summary>
        /// Gets the name of this class
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the command this Class provides
        /// </summary>
        public string Command
        {
            get { return command; }
        }

        /// <summary>
        /// Gets the MP modifier for this class
        /// </summary>
        public int MPModifier
        {
            get { return mpm; }
        }

        /// <summary>
        /// Gets the HP modifier for this class
        /// </summary>
        public int HPModifier
        {
            get { return hpm; }
        }

        /// <summary>
        /// Gets the Speed modifier for this class
        /// </summary>
        public int SPModifier
        {
            get { return spm; }
        }

        /// <summary>
        /// Gets the Physical Attack modifier for this class
        /// </summary>
        public int PAModifier
        {
            get { return pam; }
        }

        /// <summary>
        /// Gets the Magic Attack modifier for this class
        /// </summary>
        public int MAModifier
        {
            get { return mam; }
        }

        /// <summary>
        /// Gets this class's movement distance
        /// </summary>
        public int Move
        {
            get { return move; }
        }

        /// <summary>
        /// Gets this class's Evade%
        /// </summary>
        public int CEvade
        {
            get { return cev; }
        }

        /// <summary>
        /// Gets the HP constant for this class, used to determine level up/down changes
        /// </summary>
        public int HPConstant
        {
            get { return hpc; }
        }

        /// <summary>
        /// Gets the MP constant for this class, used to determine level up/down changes
        /// </summary>
        public int MPConstant
        {
            get { return mpc; }
        }

        /// <summary>
        /// Gets the Speed constant for this class, used to determine level up/down changes
        /// </summary>
        public int SPConstant
        {
            get { return spc; }
        }

        /// <summary>
        /// Gets the Physical Attack constant for this class, used to determine level up/down changes
        /// </summary>
        public int PAConstant
        {
            get { return pac; }
        }

        /// <summary>
        /// Gets the Magic Attack constant for this class, used to determine level up/down changes
        /// </summary>
        public int MAConstant
        {
            get { return mac; }
        }

        /// <summary>
        /// Gets the jump distance for this class
        /// </summary>
        public int Jump
        {
            get { return jump; }
        }

        /// <summary>
        /// Gets the "type" of class this is (Monster/Generic/Special/Demon)
        /// </summary>
        public string Type
        {
            get { return type; }
        }

        #endregion

        #region Utilities

        private static void LoadClasses()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( Resources.Classes );

            XmlNodeList classes = doc.SelectNodes( "//Class" );
            classDict = new Dictionary<byte, Class>( classes.Count );
            classList = new List<Class>( classes.Count );

            foreach( XmlNode node in classes )
            {
                Class newClass = new Class( node );
                classDict.Add( newClass.Byte, newClass );
                classList.Add( newClass );
            }
        }

        /// <summary>
        /// Converts RawMP into the actual MP
        /// </summary>
        public int ActualMP( uint rawMP )
        {
            return multiply( rawMP, mpm );
        }

        /// <summary>
        /// Converts actual MP into RawMP
        /// </summary>
        public uint GetRawMPFromActualMP( int actualMP )
        {
            return divide( actualMP, mpm );
        }

        /// <summary>
        /// Converts RawHP into actual HP
        /// </summary>
        public int ActualHP( uint rawHP )
        {
            return multiply( rawHP, hpm );
        }

        /// <summary>
        /// Converts actual HP into RawHP
        /// </summary>
        public uint GetRawHPFromActualHP( int actualHP )
        {
            return divide( actualHP, hpm );
        }

        /// <summary>
        /// Converts RawSP into actual speed
        /// </summary>
        public int ActualSP( uint rawSP )
        {
            return multiply( rawSP, spm );
        }

        /// <summary>
        /// Converts actual speed into RawSP
        /// </summary>
        public uint GetRawSPFromActualSP( int actualSP )
        {
            return divide( actualSP, spm );
        }

        /// <summary>
        /// Converts RawPA into actual physical attack
        /// </summary>
        public int ActualPA( uint rawPA )
        {
            return multiply( rawPA, pam );
        }

        /// <summary>
        /// Converts actual physical attack into RawPA
        /// </summary>
        public uint GetRawPAFromActualPA( int actualPA )
        {
            return divide( actualPA, pam );
        }

        /// <summary>
        /// Converts RawMA into actual magic attack
        /// </summary>
        public int ActualMA( uint rawMA )
        {
            return multiply( rawMA, mam );
        }

        /// <summary>
        /// Converts actual magic attack into RawMA
        /// </summary>
        public uint GetRawMAFromActualMA( int actualMA )
        {
            return divide( actualMA, mam );
        }

        private int multiply( uint rawVal, int multiplier )
        {
            uint result = (uint)(rawVal * multiplier / 1638400);
            if( result < 1 )
            {
                result = 1;
            }

            return (int)result;
        }

        private uint divide( int value, int multiplier )
        {
            if (multiplier == 0)
            {
                return 0;
            }
            else
            {
                uint result = (uint)(((uint)value * 1638400) / (uint)multiplier);
                return result;
            }
        }

        public override string ToString()
        {
            return string.Format( "{0} ({1:X02})", Name, Byte );
        }

        #endregion

        #region Level Up/down stuff not yet implemented

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
            stats.HP = (int)multiply( (uint)stats.rawHP, hpm );

            stats.rawMP += currentRawMP / (mpc + currentLevel);
            stats.MP = (int)multiply( (uint)stats.rawMP, mpm );

            stats.rawMA += currentRawMA / (mac + currentLevel);
            stats.MA = (int)multiply( (uint)stats.rawMA, mam );

            stats.rawPA += currentRawPA / (pac + currentLevel);
            stats.PA = (int)multiply( (uint)stats.rawPA, pam );

            stats.rawSp += currentRawSp / (spc + currentLevel);
            stats.Sp = (int)multiply( (uint)stats.rawSp, spm );

            return stats;
        }

        public stats LevelDown( int currentLevel, int currentRawHP, int currentRawMP, int currentRawMA, int currentRawPA, int currentRawSp )
        {
            stats stats = new stats();

            stats.rawHP -= currentRawHP / (hpc + currentLevel - 1);
            stats.HP = multiply( (uint)stats.rawHP, hpm );

            stats.rawMP -= currentRawMP / (mpc + currentLevel - 1);
            stats.MP = multiply( (uint)stats.rawMP, mpm );

            stats.rawMA -= currentRawMA / (mac + currentLevel - 1);
            stats.MA = multiply( (uint)stats.rawMA, mam );

            stats.rawPA -= currentRawPA / (pac + currentLevel - 1);
            stats.PA = multiply( (uint)stats.rawPA, pam );

            stats.rawSp -= currentRawSp / (spc + currentLevel - 1);
            stats.Sp = multiply( (uint)stats.rawSp, spm );

            return stats;
        }

        #endregion

        #region Constructors

        private Class( byte value, string name, int hpModifier, int hpConstant, int mpModifier, int mpConstant, int spModifier, int spConstant,
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

        private Class( XmlNode classNode )
        {
            this.num = Convert.ToByte( classNode.SelectSingleNode( "byte" ).InnerText, 16 );
            this.name = classNode.SelectSingleNode( "name" ).InnerXml;
            this.command = classNode.SelectSingleNode( "command" ).InnerXml;
            this.hpm = Convert.ToInt32( classNode.SelectSingleNode( "hpm" ).InnerXml );
            this.mpm = Convert.ToInt32( classNode.SelectSingleNode( "mpm" ).InnerXml );
            this.spm = Convert.ToInt32( classNode.SelectSingleNode( "spm" ).InnerXml );
            this.pam = Convert.ToInt32( classNode.SelectSingleNode( "pam" ).InnerXml );
            this.mam = Convert.ToInt32( classNode.SelectSingleNode( "mam" ).InnerXml );
            this.move = Convert.ToInt32( classNode.SelectSingleNode( "move" ).InnerXml );
            this.cev = Convert.ToInt32( classNode.SelectSingleNode( "cev" ).InnerXml );
            this.hpc = Convert.ToInt32( classNode.SelectSingleNode( "hpc" ).InnerXml );
            this.mpc = Convert.ToInt32( classNode.SelectSingleNode( "mpc" ).InnerXml );
            this.spc = Convert.ToInt32( classNode.SelectSingleNode( "spc" ).InnerXml );
            this.pac = Convert.ToInt32( classNode.SelectSingleNode( "pac" ).InnerXml );
            this.mac = Convert.ToInt32( classNode.SelectSingleNode( "mac" ).InnerXml );
            this.jump = Convert.ToInt32( classNode.SelectSingleNode( "jump" ).InnerXml );
            this.type = classNode.SelectSingleNode( "type" ).InnerXml;
        }

        #endregion

    }
}
