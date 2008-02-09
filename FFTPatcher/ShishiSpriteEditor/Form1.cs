/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Windows.Forms;
using System.IO;
using System;

namespace FFTPatcher.SpriteEditor
{
    public partial class Form1 : Form
    {
        string filename = string.Empty;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            openMenuItem.Click += openMenuItem_Click;
            saveAsMenuItem.Click += new EventHandler( saveAsMenuItem_Click );
            saveMenuItem.Click += new EventHandler( saveMenuItem_Click );
            paletteSelector.SelectedIndexChanged += new EventHandler( paletteSelector_SelectedIndexChanged );
        }

        private void saveMenuItem_Click( object sender, EventArgs e )
        {
            SaveBytes( filename, spriteViewer1.Sprite.ToByteArray() );
        }

        private void saveAsMenuItem_Click( object sender, EventArgs e )
        {
            saveFileDialog.Filter = "Sprite files (*.SPR)|*.SPR";
            saveFileDialog.FilterIndex = 0;
            if( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                SaveBytes( saveFileDialog.FileName, spriteViewer1.Sprite.ToByteArray() );
                filename = saveFileDialog.FileName;
            }
        }

        private void paletteSelector_SelectedIndexChanged( object sender, EventArgs e )
        {
            spriteViewer1.Palette = paletteSelector.SelectedIndex;
        }

        private void openMenuItem_Click( object sender, System.EventArgs e )
        {
            openFileDialog.Filter = "Sprite files (*.SPR)|*.SPR|Secondary sprite files (*.SP2)|*.SP2";
            openFileDialog.FilterIndex = 0;
            
            if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                byte[] bytes = null;
                try
                {
                    bytes = GetBytes( openFileDialog.FileName );
                    paletteSelector.SelectedIndex = 0;
                    spriteViewer1.Sprite = new Sprite( bytes );

                    paletteSelector.Enabled = true;
                    saveMenuItem.Enabled = true;
                    saveAsMenuItem.Enabled = true;
                    importMenuItem.Enabled = true;
                    exportMenuItem.Enabled = true;
                    paletteSaveMenuItem.Enabled = true;
                    paletteOpenMenuItem.Enabled = true;

                    filename = openFileDialog.FileName;
                }
                catch( Exception )
                {
                    MessageBox.Show( "Could not open file.", "Error", MessageBoxButtons.OK );
                }
            }
        }

        private void SaveBytes( string filename, byte[] bytes )
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream( filename, FileMode.Create );
                stream.Write( bytes, 0, bytes.Length );
            }
            catch( Exception )
            {
                MessageBox.Show( "Could not save file.", "Error", MessageBoxButtons.OK );
            }
            finally
            {
                if( stream != null )
                {
                    stream.Flush();
                    stream.Close();
                    stream = null;
                }
            }
        }

        private byte[] GetBytes( string filename )
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream( filename, FileMode.Open );
                byte[] result = new byte[stream.Length];
                stream.Read( result, 0, (int)stream.Length );
                return result;
            }
            catch( Exception )
            {
                throw;
            }
            finally
            {
                if( stream != null )
                {
                    stream.Close();
                    stream = null;
                }
            }
        }
    }
}
