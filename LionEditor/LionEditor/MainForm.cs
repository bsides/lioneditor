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
using System.IO;
using System.Windows.Forms;

namespace LionEditor
{
    public enum Region
    {
        US,
        Europe,
        Japan
    }

    public partial class MainForm : Form
    {
        #region Fields

        private string filename;
        private Savegame[] games = new Savegame[15];
        private CharacterBrowser characterBrowser;
        private bool usingGME = false;

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
            usMenuItem.Click += memoryStickMenuItem_Click;
            usMenuItem.Tag = LionEditor.Region.US;
            usMenuItem.Checked = true;
            europeMenuItem.Click += memoryStickMenuItem_Click;
            europeMenuItem.Tag = LionEditor.Region.Europe;
            europeMenuItem.Checked = false;
            japanMenuItem.Click += memoryStickMenuItem_Click;
            japanMenuItem.Tag = LionEditor.Region.Japan;
            japanMenuItem.Checked = false;

            openUSMenuItem.Tag = LionEditor.Region.US;
            openUSMenuItem.Click += openRegionMenuItem_Click;
            openEuropeanMenuItem.Tag = LionEditor.Region.Europe;
            openEuropeanMenuItem.Click += openRegionMenuItem_Click;
            openJapaneseMenuItem.Tag = LionEditor.Region.Japan;
            openJapaneseMenuItem.Click += openRegionMenuItem_Click;

            installButton.Click += installButton_Click;
        }

        #endregion

        #region Events

        private void installButton_Click( object sender, EventArgs e )
        {
            InstallPlugin();
        }

        private void openRegionMenuItem_Click( object sender, EventArgs e )
        {
            OpenFromRegion((LionEditor.Region)(((MenuItem)sender).Tag));
        }

        private void memoryStickMenuItem_Click(object sender, EventArgs e)
        {
            MenuItem selectedItem = null;
            foreach (MenuItem item in memoryStickMenu.MenuItems)
            {
                if (item == sender)
                {
                    item.Checked = true;
                    selectedItem = item;
                }
                else
                {
                    item.Checked = false;
                }
            }
            if (selectedItem != null)
            {
                OpenFromRegion((LionEditor.Region)selectedItem.Tag);
            }
        }

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
                SaveFile();
                saveButton.Enabled = false;
            }
            else if (e.Button == openMemoryStickButton)
            {
                foreach (MenuItem item in memoryStickMenu.MenuItems)
                {
                    if (item.Checked)
                    {
                        OpenFromRegion((LionEditor.Region)item.Tag);
                        break;
                    }
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

        private void InstallPlugin()
        {
            bool success = false;
            bool cancel = false;

            while ((!success) && (!cancel))
            {
                DialogResult result = folderBrowserDialog.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    string path = folderBrowserDialog.SelectedPath;

                    switch (MemoryStickUtilities.InstallPlugin(path))
                    {
                        case MemoryStickUtilities.InstallResult.Failure:
                            MessageBox.Show(this, "Installation failed", "Failed", MessageBoxButtons.OK);
                            cancel = true;
                            break;
                        case MemoryStickUtilities.InstallResult.NotMemoryStickRoot:
                            if (MessageBox.Show(this, 
                                                "Selected path is not a Memory Stick.", 
                                                "Invalid path", 
                                                MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                            {
                                cancel = true;
                            }
                            break;
                        case MemoryStickUtilities.InstallResult.Success:
                            MessageBox.Show(this, 
                                            "Plugin successfully installed!\nRemember to enable the plugin in the Recovery Menu", 
                                            "Success", 
                                            MessageBoxButtons.OK);
                            success = true;
                            break;
                    }
                }
                else
                {
                    cancel = true;
                }
            }
        }

        private void SaveFile()
        {
            if ((filename == null) || (filename == string.Empty) || (usingGME))
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

        private void TryReadFile(byte[] bytes)
        {
            try
            {
                bool validGameFound = false;
                for (int i = 0; (i < 15) && (((i+1) * Savegame.saveFileSize) < bytes.Length); i++)
                {
                    if (Savegame.IsValidPSPGame(bytes, (int)(i * Savegame.saveFileSize)))
                    {
                        usingGME = false;
                        validGameFound = true;
                        byte[] saveData = new byte[Savegame.saveFileSize];
                        Array.Copy(bytes, i * Savegame.saveFileSize, saveData, 0, Savegame.saveFileSize);

                        games[i] = new Savegame(saveData);
                        gameSelector.Items.Add(games[i]);
                    }
                    else
                    {
                        games[i] = null;
                    }
                }

                //if (!validGameFound)
                // TODO: fix GME importing
                if (false)
                {
                    // Try GME...
                    usingGME = true;
                    foreach (int i in ValidateGMEFile(bytes))
                    {
                        byte[] saveData = new byte[0x2000];
                        if ((bytes.Length + 1) < 0x2F40 + i * 0x2000)
                        {
                            break;
                        }
                        Array.Copy(bytes, 0x2F40 + i * 0x2000, saveData, 0, 0x2000);

                        games[i] = new Savegame(saveData);
                        gameSelector.Items.Add(games[i]);
                    }
                }
            }
            catch (Exception)
            {
                // Fail silently!!
            }
        }

        /// <summary>
        /// Validates a GME file and returns which blocks have FFT data
        /// </summary>
        public static List<int> ValidateGMEFile(byte[] bytes)
        {
            List<int> result = new List<int>();
            if ((bytes[0] == '1') &&
                (bytes[1] == '2') &&
                (bytes[2] == '3') &&
                (bytes[3] == '-') &&
                (bytes[4] == '4') &&
                (bytes[5] == '5') &&
                (bytes[6] == '6') &&
                (bytes[7] == '-') &&
                (bytes[8] == 'S') &&
                (bytes[9] == 'T') &&
                (bytes[10] == 'D') &&
                (bytes[11] == 0) &&
                (bytes[12] == 0) &&
                (bytes[13] == 0) &&
                (bytes[14] == 0) &&
                (bytes[15] == 0) &&
                (bytes[0xF40] == 'M') &&
                (bytes[0xF41] == 'C') &&
                (bytes[0xFBF] == 0x0E))
            {
                // Valid GME file

                // Check index frame for each block
                for (int i = 0; i < 15; i++)
                {
                    int offset = i * 128;
                    if ((bytes[0xFC0 + offset] == 0x51) &&
                        (bytes[0xFC4 + offset] == 0x00) &&
                        (bytes[0xFC5 + offset] == 0x20) &&
                        (bytes[0xFCA + offset] == 'B') &&
                        (bytes[0xFCB + offset] == 'A') &&
                        (bytes[0xFCC + offset] == 'S') &&
                        (bytes[0xFCD + offset] == 'C') &&
                        (bytes[0xFCE + offset] == 'U') &&
                        (bytes[0xFCF + offset] == 'S') &&
                        (bytes[0xFD0 + offset] == '-') &&
                        (bytes[0xFD1 + offset] == '9') &&
                        (bytes[0xFD2 + offset] == '4') &&
                        (bytes[0xFD3 + offset] == '2') &&
                        (bytes[0xFD4 + offset] == '2') &&
                        (bytes[0xFD5 + offset] == '1') &&
                        (bytes[0xFD6 + offset] == 'F') &&
                        (bytes[0xFD7 + offset] == 'F') &&
                        (bytes[0xFD8 + offset] == 'T'))
                    {
                        result.Add(i);
                    }

                }
            }

            return result;
        }

        private void LoadFile(string path)
        {
            System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            stream.Close();

            gameSelector.Items.Clear();

            TryReadFile(bytes);

            if (gameSelector.Items.Count > 0)
            {
                gameSelector.SelectedIndex = 0;
                saveButton.Enabled = false;

                savegameEditor.DataChangedEvent += savegameEditor_DataChangedEvent;
                saveMenuItem.Enabled = false;
                saveAsMenuItem.Enabled = true;
                gameSelector.Enabled = true;
            }
        }

        private void LoadFile()
        {
            openFileDialog.FileName = string.Empty;
            DialogResult result = openFileDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                filename = openFileDialog.FileName;
                LoadFile(filename);
            }
        }

        private void OpenFromRegion(Region region)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                filename = MemoryStickUtilities.GetSavePath(folderBrowserDialog.SelectedPath, region);
                if (filename == string.Empty)
                {
                    MessageBox.Show(this,
                                    "Selected path is not a Memory Stick.",
                                    "Invalid path",
                                    MessageBoxButtons.OK);
                }
                else if (!File.Exists(filename))
                {
                    filename = string.Empty;
                    MessageBox.Show(this,
                                    string.Format("{0} file not found!", region),
                                    "File not found",
                                    MessageBoxButtons.OK);
                }
                else
                {
                    LoadFile(filename);
                }
            }
        }

        #endregion
    }
}