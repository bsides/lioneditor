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
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using FFTPatcher.Datatypes;


namespace FFTPatcher.TextEditor
{
    public partial class MainForm : Form
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            exitMenuItem.Click += exitMenuItem_Click;
            aboutMenuItem.Click += aboutMenuItem_Click;
            allowedSymbolsMenuItem.Click += allowedSymbolsMenuItem_Click;
            importPsxIsoMenuItem.Click += new EventHandler( importPsxIsoMenuItem_Click );
            importPspIsoMenuItem.Click += new EventHandler( importPspIsoMenuItem_Click );
            saveMenuItem.Click += new EventHandler( saveMenuItem_Click );
            openMenuItem.Click += new EventHandler( openMenuItem_Click );
        }

        void openMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.Filter = "FFTText files (*.ffttext)|*.ffttext";
            if ( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                LoadFile( FFTTextFactory.GetFilesXml( openFileDialog.FileName ) );
            }
        }

        void saveMenuItem_Click( object sender, EventArgs e )
        {
            saveFileDialog.Filter = "FFTText files (*.ffttext)|*.ffttext";
            if (saveFileDialog.ShowDialog( this) == DialogResult.OK)
            {
                FFTTextFactory.WriteXml( internalFile, saveFileDialog.FileName );
            }
        }

        void importPspIsoMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.Filter = "ISO files (*.iso)|*.iso";
            openFileDialog.FileName = string.Empty;
            if ( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                LoadFile( FFTText.ReadPSPIso( openFileDialog.FileName ) );
            }
        }

        void importPsxIsoMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.Filter = "ISO files (*.iso, *.bin, *.img)|*.iso;*.bin;*.img";
            openFileDialog.FileName = string.Empty;
            if ( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                LoadFile( FFTText.ReadPSXIso( openFileDialog.FileName ) );
            }
        }


        private FFTText internalFile;

        private void LoadFile( FFTText file )
        {
            internalFile = file;
            textMenuItem.MenuItems.Clear();
            foreach ( IFile ifile in file.Files )
            {
                MenuItem mi = new MenuItem( ifile.DisplayName, fileClick );
                mi.Tag = ifile;
                textMenuItem.MenuItems.Add( mi );
            }

            fileClick( textMenuItem.MenuItems[0], EventArgs.Empty );
            textMenuItem.Enabled = true;
            saveMenuItem.Enabled = true;
        }

        private void fileClick( object sender, EventArgs e )
        {
            MenuItem senderItem = sender as MenuItem;
            IFile file = senderItem.Tag as IFile;
            fileEditor1.BindTo( file );
            foreach ( MenuItem mi in senderItem.Parent.MenuItems )
            {
                mi.Checked = false;
            }
            senderItem.Checked = true;

        }

        private void aboutMenuItem_Click( object sender, EventArgs e )
        {
            using( About a = new About() )
                a.ShowDialog( this );
        }

        private void allowedSymbolsMenuItem_Click( object sender, EventArgs e )
        {
            CharmapForm.Show( internalFile.CharMap );
        }

        private void exitMenuItem_Click( object sender, EventArgs e )
        {
            Application.Exit();
        }

    }
}