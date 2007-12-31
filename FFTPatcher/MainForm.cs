/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

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
using System.IO;
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            openMenuItem.Click += openMenuItem_Click;
            applyMenuItem.Click += applyMenuItem_Click;
            saveMenuItem.Click += saveMenuItem_Click;
            newPSPMenuItem.Click += newPSPMenuItem_Click;
            newPSXMenuItem.Click += newPSXMenuItem_Click;
            exitMenuItem.Click += exitMenuItem_Click;
            openModifiedMenuItem.Click += openModifiedMenuItem_Click;
            applyMenuItem.Enabled = false;
            saveMenuItem.Enabled = false;

            FFTPatch.DataChanged += FFTPatch_DataChanged;
        }

        private void FFTPatch_DataChanged( object sender, EventArgs e )
        {
            applyMenuItem.Enabled = FFTPatch.Context == Context.US_PSX;
            saveMenuItem.Enabled = true;
        }

        private void openModifiedMenuItem_Click( object sender, System.EventArgs e )
        {
            openFileDialog.Filter = "SCUS_942.21|SCUS_942.21";
            if( openFileDialog.ShowDialog() == DialogResult.OK )
            {
                try
                {
                    FFTPatch.OpenModifiedPSXFile( openFileDialog.FileName );
                }
                catch( InvalidDataException )
                {
                    MessageBox.Show(
                        "Could not find patch locations.\n" +
                        "Ensure that you have selected SCUS_942.21 if you are patching Final Fantasy Tactics",
                        "Invalid data", MessageBoxButtons.OK );
                }
                catch( FileNotFoundException )
                {
                    MessageBox.Show( "Could not open file.", "File not found", MessageBoxButtons.OK );
                }
            }
        }

        private void exitMenuItem_Click( object sender, System.EventArgs e )
        {
            this.Close();
        }

        private void newPSXMenuItem_Click( object sender, System.EventArgs e )
        {
            FFTPatch.New( Context.US_PSX );
        }

        private void newPSPMenuItem_Click( object sender, System.EventArgs e )
        {
            FFTPatch.New( Context.US_PSP );
        }

        private void saveMenuItem_Click( object sender, System.EventArgs e )
        {
            if( saveFileDialog.ShowDialog() == DialogResult.OK )
            {
                try
                {
                    FFTPatch.SaveToFile( saveFileDialog.FileName );
                }
                catch (Exception)
                {
                    MessageBox.Show( "Could not open file.", "File not found", MessageBoxButtons.OK );
                }
            }
        }

        private void applyMenuItem_Click( object sender, System.EventArgs e )
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                applyPatchOpenFileDialog.Filter = "ISO Files (*.iso) |*.iso";
            }
            else
            {
                applyPatchOpenFileDialog.Filter = "SCUS_942.21|SCUS_942.21";
            }

            if( applyPatchOpenFileDialog.ShowDialog() == DialogResult.OK )
            {
                try
                {
                    FFTPatch.ApplyPatchesToFile( applyPatchOpenFileDialog.FileName );
                    MessageBox.Show( "Patch complete!", "Finished", MessageBoxButtons.OK );
                }
                catch( InvalidDataException )
                {
                    MessageBox.Show(
                        "Could not find patch locations.\n" +
                        "Ensure that the file is an ISO image if you are patching War of the Lions." +
                        "Ensure that you have selected SCUS_942.21 if you are patching Final Fantasy Tactics",
                        "Invalid data", MessageBoxButtons.OK );
                }
                catch( FileNotFoundException )
                {
                    MessageBox.Show( "Could not open file.", "File not found", MessageBoxButtons.OK );
                }
            }
        }

        private void openMenuItem_Click( object sender, System.EventArgs e )
        {
            openFileDialog.Filter = "FFTPatcher files (*.fftpatch)|*.fftpatch";
            if( openFileDialog.ShowDialog() == DialogResult.OK )
            {
                FFTPatch.Load( openFileDialog.FileName );
            }
        }
    }
}
