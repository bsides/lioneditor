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
using System.Collections.Specialized;

namespace LionEditor.Datatypes
{
    public class GMEFile
    {
        #region Fields

        private StringCollection gameNames = new StringCollection();
        private List<List<Character>> games = new List<List<Character>>(15);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the names for each game in this file
        /// </summary>
        public StringCollection GameNames
        {
            get { return gameNames; }
        }

        /// <summary>
        /// Gets the list of characters for each game in this file
        /// </summary>
        public List<List<Character>> Games
        {
            get { return games; }
        }

        #endregion

        #region Constructor

        private GMEFile()
        {
        }

        #endregion

        #region Utilities

        public static GMEFile ReadGMEFile(byte[] bytes)
        {
            GMEFile result = new GMEFile();
            for (int i = 0; i < 15; i++)
            {
                try
                {
                    List<Character> characters = new List<Character>(20);

                    for (int j = 0; j < 20; j++)
                    {
                        byte[] character = new byte[0xE0];
                        Savegame.CopyArray(bytes, character, (0x2F40 + 0x2000 * i) + (0x484 + 0xE0 * j), 0xE0);
                        characters.Add(new Character(character, j)); ;
                    }

                    string gameName = BuildSaveGameString(bytes, i);

                    result.gameNames.Add(gameName);
                    result.games.Add(characters);
                }
                catch (Exception)
                {
                    // Ignore saves in this file that can't be processed
                }
            }

            return result;
        }

        #endregion

        private static string BuildSaveGameString(byte[] bytes, int gameIndex)
        {
            string name = Character.DecodeName(bytes, (0x2F40 + 0x2000 * gameIndex) + 0x101);

            uint timer =
                (uint)((uint)bytes[(0x2F40 + 0x2000 * gameIndex) + 0x120] +
                (uint)((uint)bytes[(0x2F40 + 0x2000 * gameIndex) + 0x121] << 8) +
                (uint)((uint)bytes[(0x2F40 + 0x2000 * gameIndex) + 0x122] << 16) +
                (uint)((uint)bytes[(0x2F40 + 0x2000 * gameIndex) + 0x123] << 24));
            string date = string.Format("{0} {1}", (NotStupidMonths)bytes[(0x2F40 + 0x2000 * gameIndex) + 0x114], bytes[(0x2F40 + 0x2000 * gameIndex) + 0x115]);

            TimeSpan span = new TimeSpan((long)((long)timer * 10000000));
            string time = string.Format("{0:000}:{1:00}:{2:00}", (int)span.TotalHours, span.Minutes, span.Seconds);

            string loc = LionEditor.Location.AllLocations[bytes[(0x2F40 + 0x2000 * gameIndex) + 0x116]].ToString();

            return string.Format("{0} ({1}) [{2}] ~{3}~", name, time, date, loc);
        }

        private enum NotStupidMonths
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }
    }
}
