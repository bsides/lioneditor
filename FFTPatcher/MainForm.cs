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
                FFTPatch.SaveToFile( saveFileDialog.FileName );
            }
        }

        private void applyMenuItem_Click( object sender, System.EventArgs e )
        {
            if( applyPatchOpenFileDialog.ShowDialog() == DialogResult.OK )
            {
                FFTPatch.ApplyPatchesToFile( applyPatchOpenFileDialog.FileName );
            }
        }

        private void openMenuItem_Click( object sender, System.EventArgs e )
        {
            if( openFileDialog.ShowDialog() == DialogResult.OK )
            {
                FFTPatch.Load( openFileDialog.FileName );
            }
        }
    }
}
