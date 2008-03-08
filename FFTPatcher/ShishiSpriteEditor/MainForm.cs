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

using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace FFTPatcher.SpriteEditor
{
    public partial class MainForm : Form
    {

		#region Fields (1) 

        string filename = string.Empty;

		#endregion Fields 

		#region Constructors (1) 

        public MainForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;

            openMenuItem.Click += openMenuItem_Click;
            saveAsMenuItem.Click += saveAsMenuItem_Click;
            saveMenuItem.Click += saveMenuItem_Click;

            paletteSaveMenuItem.Click += paletteSaveMenuItem_Click;
            paletteOpenMenuItem.Click += paletteOpenMenuItem_Click;

            exportMenuItem.Click += exportMenuItem_Click;
            importMenuItem.Click += importMenuItem_Click;

            paletteSelector.SelectedIndexChanged += paletteSelector_SelectedIndexChanged;

            aboutMenuItem.Click += aboutMenuItem_Click;
        }

		#endregion Constructors 

		#region Methods (11) 


        private void aboutMenuItem_Click( object sender, EventArgs e )
        {
            new About().ShowDialog( this );
        }

        private void exportMenuItem_Click( object sender, EventArgs e )
        {
            saveFileDialog.FileName = string.Empty;
            saveFileDialog.Filter = "Bitmap files (*.BMP)|*.BMP";
            saveFileDialog.FilterIndex = 0;
            if( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                using( Bitmap bmp = spriteViewer1.Sprite.ToBitmap() )
                {
                    bmp.Save( saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp );
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

        private void importMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.FileName = string.Empty;
            openFileDialog.Filter = "Bitmap files (*.BMP)|*.BMP";
            openFileDialog.FilterIndex = 0;
            if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                using( Bitmap b = new Bitmap( openFileDialog.FileName ) )
                {
                    spriteViewer1.Sprite.ImportBitmap( b );
                }
            }
        }

        private void openMenuItem_Click( object sender, System.EventArgs e )
        {
            openFileDialog.FileName = string.Empty;
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

        private void paletteOpenMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.FileName = string.Empty;
            openFileDialog.Filter = "Palette files (*.PAL)|*.PAL";
            openFileDialog.FilterIndex = 0;

            if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                byte[] bytes = null;
                try
                {
                    bytes = GetBytes( openFileDialog.FileName );
                    spriteViewer1.Sprite.Palettes = Palette.FromPALFile( bytes );
                    paletteSelector.SelectedIndex = 0;
                    spriteViewer1.Invalidate();
                }
                catch( Exception )
                {
                    MessageBox.Show( "Could not open file.", "Error", MessageBoxButtons.OK );
                }
            }
        }

        private void paletteSaveMenuItem_Click( object sender, EventArgs e )
        {
            saveFileDialog.FileName = string.Empty;
            saveFileDialog.Filter = "Palette files (*.PAL)|*.PAL";
            saveFileDialog.FilterIndex = 0;
            if( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                SaveBytes( saveFileDialog.FileName, spriteViewer1.Sprite.Palettes.ToPALFile() );
            }
        }

        private void paletteSelector_SelectedIndexChanged( object sender, EventArgs e )
        {
            spriteViewer1.Palette = paletteSelector.SelectedIndex;
        }

        private void saveAsMenuItem_Click( object sender, EventArgs e )
        {
            saveFileDialog.FileName = filename;
            saveFileDialog.Filter = spriteViewer1.Sprite.SPR ? "Sprite files (*.SPR)|*.SPR" : "Secondary sprite files (*.SP2)|*.SP2";
            saveFileDialog.FilterIndex = 0;
            if( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                SaveBytes( saveFileDialog.FileName, spriteViewer1.Sprite.ToByteArray() );
                filename = saveFileDialog.FileName;
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

        private void saveMenuItem_Click( object sender, EventArgs e )
        {
            SaveBytes( filename, spriteViewer1.Sprite.ToByteArray() );
        }


		#endregion Methods 

    }
}
