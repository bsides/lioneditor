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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PatcherLib.Datatypes;
using System.IO;

namespace FFTPatcher.SpriteEditor
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private Stream currentStream = null;

        private void openIsoMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "ISO files (*.bin, *.iso, *.img)|*.bin;*.iso;*.img";
            openFileDialog.FileName = string.Empty;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Stream openedStream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.ReadWrite);
                if (openedStream != null )
                {
                    if (AllSprites.DetectExpansionOfPsxIso(openedStream) ||
                        MessageBox.Show(this, "ISO needs to be restructured." + Environment.NewLine + "Restructure?", "Restructure ISO?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        if (currentStream != null)
                        {
                            currentStream.Flush();
                            currentStream.Close();
                            currentStream.Dispose();
                        }
                        currentStream = openedStream;

                        AllSprites s = AllSprites.FromPsxIso(currentStream);
                        allSpritesEditor1.BindTo(s, currentStream);
                    }
                    else 
                    {
                        openedStream.Close();
                        openedStream.Dispose();
                    }
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (currentStream != null)
            {
                currentStream.Flush();
                currentStream.Close();
                currentStream.Dispose();
                currentStream = null;
            }
            base.OnClosing(e);
        }

        private void importSprMenuItem_Click( object sender, EventArgs e )
        {

        }

        private void exportSprMenuItem_Click( object sender, EventArgs e )
        {

        }

        private void importBmpMenuItem_Click( object sender, EventArgs e )
        {
        }

        private void exportBmpMenuItem_Click( object sender, EventArgs e )
        {
            Sprite currentSprite = allSpritesEditor1.CurrentSprite;
            saveFileDialog.Filter = "8bpp paletted bitmap (*.BMP)|*.bmp";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.CreatePrompt = false;

            if ( currentSprite != null && saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                currentSprite.GetAbstractSpriteFromPsxIso( currentStream ).ToBitmap().Save( saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp );
            }
        }

        private void exitMenuItem_Click( object sender, EventArgs e )
        {
            Application.Exit();
        }

    }
}
