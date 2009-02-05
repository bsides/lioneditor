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
//#define DONGS

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

		#region Fields (4) 

        private FFTText file;

		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
#if DONGS
            FillPSPFiles();
            FillPSXFiles();
#endif
            InitializeComponent();

            //stringSectionedEditor.Visible = false;

            exitMenuItem.Click += exitMenuItem_Click;
            aboutMenuItem.Click += aboutMenuItem_Click;
            allowedSymbolsMenuItem.Click += allowedSymbolsMenuItem_Click;
            importPsxIsoMenuItem.Click += new EventHandler( importPsxIsoMenuItem_Click );
            importPspIsoMenuItem.Click += new EventHandler( importPspIsoMenuItem_Click );
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

#endregion Constructors 

		#region Methods (20) 

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

        //private void patchMenuItem_Click( object sender, EventArgs e )
        //{
        //    bool oldStringSectionedEditorEnabled = false;//stringSectionedEditor.Enabled;

        //    DoWorkEventHandler doWork =
        //        delegate( object sender1, DoWorkEventArgs args )
        //        {
        //            //File.UpdateIso( sender1 as BackgroundWorker, args );
        //        };
        //    ProgressChangedEventHandler progress =
        //        delegate( object sender2, ProgressChangedEventArgs args )
        //        {
        //            progressBar.Value = args.ProgressPercentage;
        //            if ( args.UserState is string )
        //            {
        //                progressBar.ProgressBarText = (string)args.UserState;
        //            }
        //            else
        //            {
        //                progressBar.ProgressBarText = string.Empty;
        //            }
        //        };
        //    RunWorkerCompletedEventHandler completed = null;
        //    completed =
        //        delegate( object sender3, RunWorkerCompletedEventArgs args )
        //        {
        //            progressBar.Visible = false;
        //            fileMenuItem.Enabled = true;

        //            ( File.Filetype == Context.US_PSX ? psxMenuItem : pspMenuItem ).Enabled = true;

        //            helpMenuItem.Enabled = true;
        //            //stringSectionedEditor.Enabled = oldStringSectionedEditorEnabled;
        //            UseWaitCursor = false;
        //            patchPsxBackgroundWorker.ProgressChanged -= progress;
        //            patchPsxBackgroundWorker.RunWorkerCompleted -= completed;
        //            patchPsxBackgroundWorker.DoWork -= doWork;
        //            if ( args.Error != null )
        //            {
        //                MessageBox.Show( this, "There was an error patching the ISO", "Error" );
        //            }
        //        };


        //    openFileDialog.Filter = File.Filetype == Context.US_PSX ? "ISO images (*.bin)|*.bin" : "ISO images (*.iso)|*.iso";
        //    saveFileDialog.OverwritePrompt = false;
        //    if ( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
        //    {
        //        patchPsxBackgroundWorker.ProgressChanged += progress;
        //        patchPsxBackgroundWorker.RunWorkerCompleted += completed;
        //        patchPsxBackgroundWorker.DoWork += doWork;

        //        fileMenuItem.Enabled = false;

        //        ( File.Filetype == Context.US_PSX ? psxMenuItem : pspMenuItem ).Enabled = false;

        //        helpMenuItem.Enabled = false;
        //        //stringSectionedEditor.Enabled = false;
        //        UseWaitCursor = true;

        //        progressBar.Value = 0;
        //        progressBar.Visible = true;
        //        patchPsxBackgroundWorker.RunWorkerAsync( saveFileDialog.FileName );
        //    }
        //}



		#endregion Methods 
#if DEBUG
        private string[] GetLayoutOfCloseAndNewLines( string s )
        {
            List<string> result = new List<string>();
            int lastIndex = 0;

            while( lastIndex != -1 )
            {
                int lastIndexA = s.IndexOf( @"{Close}", lastIndex );
                int lastIndexB = s.IndexOf( "\r\n", lastIndex );
                if( lastIndexA == -1 && lastIndexB == -1 )
                {
                    lastIndex = -1;
                }
                else if( lastIndexA != -1 && lastIndexB != -1 )
                {
                    lastIndex = Math.Min( lastIndexA, lastIndexB );
                    if( lastIndex == lastIndexA )
                    {
                        lastIndex += @"{Close}".Length;
                        result.Add( @"{Close}" );
                    }
                    else
                    {
                        lastIndex += "\r\n".Length;
                        result.Add( "\r\n" );
                    }
                }
                else if( lastIndexA != -1 )
                {
                    lastIndex = lastIndexA;
                    lastIndex += @"{Close}".Length;
                    result.Add( @"{Close}" );
                }
                else
                {
                    lastIndex = lastIndexB;
                    lastIndex += "\r\n".Length;
                    result.Add( "\r\n" );
                }
            }

            return result.ToArray();
        }
#endif

    }
}