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
using System.Windows.Forms;

namespace LionEditor
{
    public partial class CharacterBrowser : Form
    {
        #region Fields

        private List<List<Character>> games;
        private List<string> gameNames;

        #endregion

        #region Constructor

        public CharacterBrowser()
        {
            InitializeComponent();
            characterCollectionEditor.ContextMenuEnabled = false;
            characterCollectionEditor.CharacterEditorEnabled = false;

            toolBar.ButtonClick += toolBar_ButtonClick;
            gameSelector.SelectedIndexChanged += gameSelector_SelectedIndexChanged;
        }

        #endregion

        #region Events

        void gameSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            characterCollectionEditor.CharacterCollection = games[gameSelector.SelectedIndex];
        }

        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == openButton)
            {
                DialogResult result = openFileDialog.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    System.IO.FileStream stream = new System.IO.FileStream(openFileDialog.FileName, System.IO.FileMode.Open);

                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    stream.Close();

                    games = new List<List<Character>>();
                    gameNames = new List<string>();

                    gameSelector.Items.Clear();

                    if (Savegame.IsValidPSPGame(bytes, 0))
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            if (Savegame.IsValidPSPGame(bytes, (int)(Savegame.saveFileSize*i)))
                            {
                                Savegame g = new Savegame(bytes);
                                gameNames.Add(g.ToString());
                                games.Add(g.Characters);
                            }
                        }
                    }
                    else 
                    {
                        GMEFile file = GMEFile.ReadGMEFile(bytes);
                        if (file != null)
                        {
                            games = file.Games;
                            gameNames = file.GameNames;
                        }
                    }

                    gameSelector.Items.AddRange(gameNames.ToArray());

                    if (gameSelector.Items.Count > 0)
                    {
                        gameSelector.SelectedIndex = 0;
                    }

                    gameSelector.Enabled = true;
                }
            }
        }

        #endregion

        #region Utilities

        private bool HasAtLeastOneValidSave(byte[] bytes)
        {
            for (int i = 0; i < 15; i++)
            {
                if (Savegame.IsValidPSPGame(bytes, (int)(i * Savegame.saveFileSize)))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
