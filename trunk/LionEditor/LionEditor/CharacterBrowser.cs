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

                    try
                    {
                        bool validGameFound = false;
                        for( int i = 0; (i < 15) && (((i + 1) * Savegame.saveFileSize) < bytes.Length); i++ )
                        {
                            if( Savegame.IsValidPSPGame( bytes, (int)(i * Savegame.saveFileSize) ) )
                            {
                                validGameFound = true;
                                byte[] saveData = new byte[Savegame.saveFileSize];
                                Array.Copy( bytes, i * Savegame.saveFileSize, saveData, 0, Savegame.saveFileSize );

                                Savegame g = new Savegame( saveData );
                                gameNames.Add( g.ToString() );
                                games.Add( g.Characters );
                            }
                        }

                        if (!validGameFound)
                        {
                            // Try GME...
                            foreach( int i in MainForm.ValidateGMEFile( bytes ) )
                            {
                                byte[] saveData = new byte[0x2000];
                                if( (bytes.Length + 1) < 0x2F40 + i * 0x2000 )
                                {
                                    break;
                                }
                                Array.Copy( bytes, 0x2F40 + i * 0x2000, saveData, 0, 0x2000 );

                                Savegame g = new Savegame( saveData );
                                gameNames.Add( g.ToString() );
                                games.Add( g.Characters );
                            }
                        }
                    }
                    catch( Exception )
                    {
                        // Fail silently!!
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
