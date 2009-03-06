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
    public static class RandomNames
    {
        #region Fields

        private static List<string> maleNames;
        private static List<string> femaleNames;
        private static List<string> monsterNames;
        private static Random rng = new Random();

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of generic male names
        /// </summary>
        public static List<string> MaleNames
        {
            get
            {
                if (maleNames == null)
                {
                    GetNamesFromFile();
                }
                return maleNames;
            }
        }

        /// <summary>
        /// Gets a list of generic female names
        /// </summary>
        public static List<string> FemaleNames
        {
            get
            {
                if (femaleNames == null)
                {
                    GetNamesFromFile();
                }

                return femaleNames;
            }
        }

        /// <summary>
        /// Gets a list of generic monster names
        /// </summary>
        public static List<string> MonsterNames
        {
            get
            {
                if (monsterNames == null)
                {
                    GetNamesFromFile();
                }

                return monsterNames;
            }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Gets all of the random names from the resources. 
        /// </summary>
        private static void GetNamesFromFile()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Resources.RandomNames);

            maleNames = new List<string>(256);
            foreach (XmlNode maleNode in doc.SelectNodes("//male/name"))
            {
                maleNames.Add(maleNode.InnerXml);
            }

            femaleNames = new List<string>(256);
            foreach (XmlNode femaleNode in doc.SelectNodes("//female/name"))
            {
                femaleNames.Add(femaleNode.InnerXml);
            }

            monsterNames = new List<string>(256);
            foreach (XmlNode monsterNode in doc.SelectNodes("//monster/name"))
            {
                monsterNames.Add(monsterNode.InnerXml);
            }
        }

        /// <summary>
        /// Gets a random male name
        /// </summary>
        public static string GetRandomMaleName()
        {
            return MaleNames[rng.Next(0, 255)];
        }

        /// <summary>
        /// Gets a random female name
        /// </summary>
        public static string GetRandomFemaleName()
        {
            return FemaleNames[rng.Next(0, 255)];
        }

        /// <summary>
        /// Gets a random monster name
        /// </summary>
        public static string GetRandomMonsterName()
        {
            return MonsterNames[rng.Next(0, 255)];
        }

        #endregion
    }
}
