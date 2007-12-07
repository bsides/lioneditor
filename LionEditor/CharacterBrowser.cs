using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

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

                    if (Savegame.IsValidGame(bytes, 0))
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            if (Savegame.IsValidGame(bytes, (int)(Savegame.saveFileSize*i)))
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
                if (Savegame.IsValidGame(bytes, (int)(i * Savegame.saveFileSize)))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
