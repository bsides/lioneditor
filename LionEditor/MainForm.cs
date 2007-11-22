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
        string filename;
        bool modified = false;
        Savegame[] games = new Savegame[5];

        public MainForm()
        {
            InitializeComponent();
            toolBar1.ButtonClick += new ToolBarButtonClickEventHandler( toolBar1_ButtonClick );
            comboBox1.SelectedIndexChanged += new EventHandler( comboBox1_SelectedIndexChanged );
        }

        void comboBox1_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( comboBox1.SelectedIndex != -1 )
            {
                savegameEditor1.Game = games[comboBox1.SelectedIndex];
            }
        }

        private void LoadFile()
        {
            System.IO.FileStream stream = new System.IO.FileStream( filename, System.IO.FileMode.Open );
            byte[] bytes = new byte[0x2A3C];

            for( int i = 0; i < 5; i++ )
            {
                stream.Read( bytes, 0, 0x2A3C );
                games[i] = new Savegame( bytes );
            }
            stream.Close();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange( games );
            comboBox1.SelectedIndex = 0;

            saveButton.Enabled = false;

            savegameEditor1.DataChangedEvent += new EventHandler( savegameEditor1_DataChangedEvent );
        }

        void savegameEditor1_DataChangedEvent( object sender, EventArgs e )
        {
            saveButton.Enabled = true;
        }

        void toolBar1_ButtonClick( object sender, ToolBarButtonClickEventArgs e )
        {
            if( e.Button == openButton )
            {
                openFileDialog.FileName = string.Empty;
                DialogResult result = openFileDialog.ShowDialog();
                if( result == DialogResult.OK )
                {
                    filename = openFileDialog.FileName;
                    LoadFile();
                }
            }
            else if( e.Button == saveButton )
            {
            }
            else
            {
                // Something terrible happened
                throw new Exception();
            }
        }
    }
}