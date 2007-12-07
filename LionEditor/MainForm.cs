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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LionEditor
{
    public partial class MainForm : Form
    {
        #region Fields

        private string filename;
        private Savegame[] games = new Savegame[15];
        private CharacterBrowser characterBrowser;

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            toolBar.ButtonClick += toolBarClick;
            gameSelector.SelectedIndexChanged += SelectedGameChanged;
            openMenuItem.Click += openMenuItem_Click;
            saveMenuItem.Click += saveMenuItem_Click;
            saveAsMenuItem.Click += saveAsMenuItem_Click;
            aboutMenuItem.Click += aboutMenuItem_Click;
            importCharactersMenuItem.Click += importCharactersMenuItem_Click;
        }


        #endregion

        #region Events

        private void importCharactersMenuItem_Click(object sender, EventArgs e)
        {
            if (characterBrowser == null)
            {
                characterBrowser = new CharacterBrowser();
            }

            if (!characterBrowser.Visible)
            {
                characterBrowser.Show();
                characterBrowser.Disposed +=
                    delegate(object dSender, EventArgs de)
                    {
                        characterBrowser = null;
                    };
            }
            else
            {
                characterBrowser.BringToFront();
            }
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog(this);
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            filename = null;
            SaveFile();
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private void SelectedGameChanged(object sender, EventArgs e)
        {
            if (gameSelector.SelectedIndex != -1)
            {
                savegameEditor.Game = gameSelector.SelectedItem as Savegame;
            }
        }

        private void savegameEditor_DataChangedEvent(object sender, EventArgs e)
        {
            saveButton.Enabled = true;
            saveMenuItem.Enabled = true;
        }

        private void toolBarClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == openButton)
            {
                LoadFile();
            }
            else if (e.Button == saveButton)
            {
                if ((filename != null) && (filename != string.Empty))
                {
                    SaveFile();
                    saveButton.Enabled = false;
                }
            }
            else
            {
                // Something terrible happened
                throw new Exception();
            }
        }

        #endregion

        #region Utilities


        private void SaveFile()
        {
            if ((filename == null) || (filename == string.Empty))
            {
                filename = null;
                saveFileDialog.FileName = string.Empty;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filename = saveFileDialog.FileName;
                }
                else
                {
                    return;
                }
            }

            System.IO.FileStream stream = new System.IO.FileStream(filename, System.IO.FileMode.Create);
            for (int i = 0; i < 15; i++)
            {
                if (games[i] != null)
                {
                    stream.Write(games[i].ToByteArray(), 0, (int)Savegame.saveFileSize);
                }
                else
                {
                    for (int j = 0; j < Savegame.saveFileSize; j++)
                    {
                        stream.WriteByte(0xFF);
                    }
                }
            }

            stream.Close();
            saveButton.Enabled = false;
            saveMenuItem.Enabled = false;
        }

        private void LoadFile()
        {
            openFileDialog.FileName = string.Empty;
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                filename = openFileDialog.FileName;
                System.IO.FileStream stream = new System.IO.FileStream(filename, System.IO.FileMode.Open);
                byte[] bytes = new byte[0x2A3C];

                gameSelector.Items.Clear();
                for (int i = 0; i < 15; i++)
                {
                    stream.Read(bytes, 0, 0x2A3C);
                    if (Savegame.IsValidGame(bytes))
                    {
                        games[i] = new Savegame(bytes);
                        gameSelector.Items.Add(games[i]);
                    }
                    else
                    {
                        games[i] = null;
                    }
                }
                stream.Close();

                if (gameSelector.Items.Count > 0)
                {
                    gameSelector.SelectedIndex = 0;
                }

                saveButton.Enabled = false;

                savegameEditor.DataChangedEvent += new EventHandler(savegameEditor_DataChangedEvent);
                saveMenuItem.Enabled = false;
                saveAsMenuItem.Enabled = true;
                gameSelector.Enabled = true;
            }
        }

        #endregion

    }
}