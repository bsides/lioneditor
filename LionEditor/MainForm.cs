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
        Savegame[] games = new Savegame[15];

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
                savegameEditor1.Game = comboBox1.SelectedItem as Savegame;
            }
        }

        private bool CheckValidGame( byte[] bytes )
        {
            return (bytes[0] == 'S')
                && (bytes[1] == 'C')
                && ((bytes[0x48D] == 0x00) || (bytes[0x48D] == 0xFF))
                && ((bytes[0x58D] == 0x01) || (bytes[0x58D] == 0xFF))
                && ((bytes[0x68D] == 0x02) || (bytes[0x68D] == 0xFF))
                && ((bytes[0x78D] == 0x03) || (bytes[0x78D] == 0xFF))
                && ((bytes[0x88D] == 0x04) || (bytes[0x88D] == 0xFF))
                && ((bytes[0x98D] == 0x05) || (bytes[0x98D] == 0xFF))
                && ((bytes[0xA8D] == 0x06) || (bytes[0xA8D] == 0xFF))
                && ((bytes[0xB8D] == 0x07) || (bytes[0xB8D] == 0xFF))
                && ((bytes[0xC8D] == 0x08) || (bytes[0xC8D] == 0xFF))
                && ((bytes[0xD8D] == 0x09) || (bytes[0xD8D] == 0xFF))
                && ((bytes[0xE8D] == 0x0A) || (bytes[0xE8D] == 0xFF))
                && ((bytes[0xF8D] == 0x0B) || (bytes[0xF8D] == 0xFF))
                && ((bytes[0x108D] == 0x0C) || (bytes[0x108D] == 0xFF))
                && ((bytes[0x118D] == 0x0D) || (bytes[0x118D] == 0xFF))
                && ((bytes[0x128D] == 0x0E) || (bytes[0x128D] == 0xFF))
                && ((bytes[0x138D] == 0x0F) || (bytes[0x138D] == 0xFF))
                && ((bytes[0x148D] == 0x10) || (bytes[0x148D] == 0xFF))
                && ((bytes[0x158D] == 0x11) || (bytes[0x158D] == 0xFF))
                && ((bytes[0x168D] == 0x12) || (bytes[0x168D] == 0xFF))
                && ((bytes[0x178D] == 0x13) || (bytes[0x178D] == 0xFF))
                && ((bytes[0x188D] == 0x14) || (bytes[0x188D] == 0xFF))
                && ((bytes[0x198D] == 0x15) || (bytes[0x198D] == 0xFF))
                && ((bytes[0x1A8D] == 0x16) || (bytes[0x1A8D] == 0xFF))
                && ((bytes[0x1B8D] == 0x17) || (bytes[0x1B8D] == 0xFF))
                && ((bytes[0x1C8D] == 0x18) || (bytes[0x1C8D] == 0xFF))
                && ((bytes[0x1D8D] == 0x19) || (bytes[0x1D8D] == 0xFF))
                && ((bytes[0x1E8D] == 0x1A) || (bytes[0x1E8D] == 0xFF))
                && ((bytes[0x1F8D] == 0x1B) || (bytes[0x1F8D] == 0xFF))
                && (bytes[0x2A3A] == 0xFF)
                && (bytes[0x2A3B] == 0xFF);
        }

        private void SaveFile()
        {
            System.IO.FileStream stream = new System.IO.FileStream( filename, System.IO.FileMode.Create );
            for( int i = 0; i < 15; i++ )
            {
                if( games[i] != null )
                {
                    stream.Write( games[i].ToByteArray(), 0, (int)Savegame.saveFileSize );
                }
                else
                {
                    for( int j = 0; j < Savegame.saveFileSize; j++ )
                    {
                        stream.WriteByte( 0xFF );
                    }
                }
            }
        }

        private void LoadFile()
        {
            System.IO.FileStream stream = new System.IO.FileStream( filename, System.IO.FileMode.Open );
            byte[] bytes = new byte[0x2A3C];

            comboBox1.Items.Clear();
            for( int i = 0; i < 15; i++ )
            {
                stream.Read( bytes, 0, 0x2A3C );
                if( CheckValidGame( bytes ) )
                {
                    games[i] = new Savegame( bytes );
                    comboBox1.Items.Add( games[i] );
                }
                else
                {
                    games[i] = null;
                }
            }
            stream.Close();

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
                if( (filename != null) && (filename != string.Empty) )
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
    }
}