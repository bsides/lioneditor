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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using FFTPatcher.Datatypes;
using System.ComponentModel;

namespace FFTPatcher
{
    public partial class MainForm : Form
    {
        private PatchPSXForm patchPsxForm = null;

        private PatchPSXForm PatchPSXForm
        {
            get
            {
                if( patchPsxForm == null )
                {
                    patchPsxForm = new PatchPSXForm();
                }
                return patchPsxForm;
            }
        }

        private PatchPSPForm patchPspForm = null;

        private PatchPSPForm PatchPSPForm
        {
            get
            {
                if( patchPspForm == null )
                {
                    patchPspForm = new PatchPSPForm();
                }
                return patchPspForm;
            }
        }

        #region Constructors (1) 

        public MainForm()
        {
            InitializeComponent();
            openMenuItem.Click += openMenuItem_Click;
            //applySCUSMenuItem.Click += applyMenuItem_Click;
            saveMenuItem.Click += saveMenuItem_Click;
            saveAsPspMenuItem.Click += saveAsPspMenuItem_Click;
            newPSPMenuItem.Click += newPSPMenuItem_Click;
            newPSXMenuItem.Click += newPSXMenuItem_Click;
            exitMenuItem.Click += exitMenuItem_Click;
            aboutMenuItem.Click += aboutMenuItem_Click;
            //applySCUSMenuItem.Enabled = false;
            saveMenuItem.Enabled = false;
            saveAsPspMenuItem.Enabled = false;
            //generateMenuItem.Click += generateMenuItem_Click;
            //generateMenuItem.Enabled = false;
            //generateFontMenuItem.Click += generateFontMenuItem_Click;
            //applyBattleBinMenuItem.Click += applyBattleBinMenuItem_Click;

            extractFFTPackMenuItem.Click += extractFFTPackMenuItem_Click;
            rebuildFFTPackMenuItem.Click += rebuildFFTPackMenuItem_Click;
            decryptMenuItem.Click += decryptMenuItem_Click;

            patchPspIsoMenuItem.Click += patchPspIsoMenuItem_Click;
            patchPsxIsoMenuItem.Click += patchPsxIsoMenuItem_Click;
            cheatdbMenuItem.Click += cheatdbMenuItem_Click;
            openPatchedPsxIso.Click += new EventHandler( openPatchedPsxIso_Click );

            FFTPatch.DataChanged += FFTPatch_DataChanged;
        }

		#endregion Constructors 

		#region Methods (24) 


        private void aboutMenuItem_Click( object sender, EventArgs e )
        {
            About a = new About();
            a.ShowDialog( this );
        }

        private void cheatdbMenuItem_Click( object sender, EventArgs e )
        {
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "CWCheat DB files|cheat.db";
            if( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                try
                {
                    Codes.SaveToFile( saveFileDialog.FileName );
                }
                catch
                {
                    MessageBox.Show( "Could not save file.", "Error", MessageBoxButtons.OK );
                }
            }
        }

        private void decryptMenuItem_Click( object sender, EventArgs e )
        {
            applyPatchOpenFileDialog.Filter = "War of the Lions ISO images (*.iso)|*.iso";
            if( applyPatchOpenFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                try
                {
                    PspIso.DecryptISO( applyPatchOpenFileDialog.FileName );
                }
                catch( NotSupportedException )
                {
                    MessageBox.Show( "File is not a recognized War of the Lions ISO image.", "Error", MessageBoxButtons.OK );
                }
                catch( FileNotFoundException )
                {
                    MessageBox.Show( "Could not open file.", "File not found", MessageBoxButtons.OK );
                }
                catch( Exception )
                {
                    MessageBox.Show( "Could not decrypt file.", "Error", MessageBoxButtons.OK );
                }
            }
        }

        private void exitMenuItem_Click( object sender, System.EventArgs e )
        {
            this.Close();
        }

        private void extractFFTPackMenuItem_Click( object sender, EventArgs e )
        {
            DoWorkEventHandler doWork =
                delegate( object sender1, DoWorkEventArgs args )
                {
                    FFTPack.DumpToDirectory( openFileDialog.FileName, folderBrowserDialog.SelectedPath, sender1 as BackgroundWorker );
                };
            ProgressChangedEventHandler progress =
                delegate( object sender2, ProgressChangedEventArgs args )
                {
                    progressBar.Visible = true;
                    progressBar.Value = args.ProgressPercentage;
                };
            RunWorkerCompletedEventHandler completed = null;
            completed =
                delegate( object sender3, RunWorkerCompletedEventArgs args )
                {
                    progressBar.Visible = false;
                    Enabled = true;
                    patchPsxBackgroundWorker.ProgressChanged -= progress;
                    patchPsxBackgroundWorker.RunWorkerCompleted -= completed;
                    patchPsxBackgroundWorker.DoWork -= doWork;
                    if( args.Error is Exception )
                    {
                        MessageBox.Show(
                            "Could not extract file.\n" +
                            "Make sure you chose the correct file and that there \n" +
                            "enough room in the destination directory.",
                            "Error", MessageBoxButtons.OK );
                    }
                };

            openFileDialog.Filter = "fftpack.bin|fftpack.bin|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            folderBrowserDialog.Description = "Where should the files be extracted?";

            if( (openFileDialog.ShowDialog( this ) == DialogResult.OK) && (folderBrowserDialog.ShowDialog( this ) == DialogResult.OK) )
            {
                patchPsxBackgroundWorker.ProgressChanged += progress;
                patchPsxBackgroundWorker.RunWorkerCompleted += completed;
                patchPsxBackgroundWorker.DoWork += doWork;

                Enabled = false;
                progressBar.Value = 0;
                progressBar.Visible = true;
                progressBar.BringToFront();
                patchPsxBackgroundWorker.RunWorkerAsync();
            }
        }

        private void FFTPatch_DataChanged( object sender, EventArgs e )
        {
            //applySCUSMenuItem.Enabled = FFTPatch.Context == Context.US_PSX;
            patchPsxIsoMenuItem.Enabled = FFTPatch.Context == Context.US_PSX;
            saveMenuItem.Enabled = true;
            //generateMenuItem.Enabled = true;
            //generateFontMenuItem.Enabled = FFTPatch.Context == Context.US_PSX;
            //applyBattleBinMenuItem.Enabled = FFTPatch.Context == Context.US_PSX;
            //generateMenuItem.Enabled = FFTPatch.Context == Context.US_PSX;

            patchPspIsoMenuItem.Enabled = FFTPatch.Context == Context.US_PSP;
            cheatdbMenuItem.Enabled = FFTPatch.Context == Context.US_PSP;

        }

        private void newPSPMenuItem_Click( object sender, System.EventArgs e )
        {
            FFTPatch.New( Context.US_PSP );
            saveAsPspMenuItem.Enabled = true;
        }

        private void newPSXMenuItem_Click( object sender, System.EventArgs e )
        {
            FFTPatch.New( Context.US_PSX );
            saveAsPspMenuItem.Enabled = true;
        }

        private void openMenuItem_Click( object sender, System.EventArgs e )
        {
            openFileDialog.Filter = "FFTPatcher files (*.fftpatch)|*.fftpatch";
            if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                FFTPatch.LoadPatch( openFileDialog.FileName );
                saveAsPspMenuItem.Enabled = true;
            }
        }


        private void openPatchedPsxIso_Click( object sender, EventArgs e )
        {
            openFileDialog.Filter = "ISO images|*.iso;*.bin;*.img";
            if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                FFTPatch.OpenPatchedISO( openFileDialog.FileName );
            }
        }

        private void patchPspIsoMenuItem_Click( object sender, EventArgs e )
        {
            DoWorkEventHandler doWork =
                delegate( object sender1, DoWorkEventArgs args )
                {
                    PspIso.PatchISO( sender1 as BackgroundWorker, args, PatchPSPForm );
                };
            ProgressChangedEventHandler progress =
                delegate( object sender2, ProgressChangedEventArgs args )
                {
                    progressBar.Visible = true;
                    progressBar.Value = args.ProgressPercentage;
                };
            RunWorkerCompletedEventHandler completed = null;
            completed =
                delegate( object sender3, RunWorkerCompletedEventArgs args )
                {
                    progressBar.Visible = false;
                    Enabled = true;
                    patchPsxBackgroundWorker.ProgressChanged -= progress;
                    patchPsxBackgroundWorker.RunWorkerCompleted -= completed;
                    patchPsxBackgroundWorker.DoWork -= doWork;
                    if( args.Error is NotSupportedException )
                    {
                        MessageBox.Show( "File is not a recognized War of the Lions ISO image.", "Error", MessageBoxButtons.OK );
                    }
                    else if( args.Error is FileNotFoundException )
                    {
                        MessageBox.Show( "Could not open file.", "File not found", MessageBoxButtons.OK );
                    }
                    else if( args.Error is Exception )
                    {
                        MessageBox.Show( "Could not decrypt file.", "Error", MessageBoxButtons.OK );
                    }
                };

            if( PatchPSPForm.CustomShowDialog( this ) == DialogResult.OK )
            {
                patchPsxBackgroundWorker.ProgressChanged += progress;
                patchPsxBackgroundWorker.RunWorkerCompleted += completed;
                patchPsxBackgroundWorker.DoWork += doWork;

                Enabled = false;

                progressBar.Value = 0;
                progressBar.Visible = true;

                patchPsxBackgroundWorker.RunWorkerAsync();
            }
        }

        private void patchPsxIsoMenuItem_Click( object sender, EventArgs e )
        {
            DoWorkEventHandler doWork =
                delegate( object sender1, DoWorkEventArgs args )
                {
                    PsxIso.PatchPsxIso( sender1 as BackgroundWorker, args, PatchPSXForm );
                };
            ProgressChangedEventHandler progress =
                delegate( object sender2, ProgressChangedEventArgs args )
                {
                    progressBar.Visible = true;
                    progressBar.Value = args.ProgressPercentage;
                };
            RunWorkerCompletedEventHandler completed = null;
            completed =
                delegate( object sender3, RunWorkerCompletedEventArgs args )
                {
                    progressBar.Visible = false;
                    Enabled = true;
                    patchPsxBackgroundWorker.ProgressChanged -= progress;
                    patchPsxBackgroundWorker.RunWorkerCompleted -= completed;
                    patchPsxBackgroundWorker.DoWork -= doWork;
                    if ( args.Error != null )
                    {
                        MessageBox.Show( this, "There was an error patching the file", "Error" );
                    }
                };

            
            if ( PatchPSXForm.CustomShowDialog( this ) == DialogResult.OK )
            {
                patchPsxBackgroundWorker.ProgressChanged += progress;
                patchPsxBackgroundWorker.RunWorkerCompleted += completed;
                patchPsxBackgroundWorker.DoWork += doWork;
                
                Enabled = false;

                progressBar.Value = 0;
                progressBar.Visible = true;

                patchPsxBackgroundWorker.RunWorkerAsync();
            }
        }

        private void rebuildFFTPackMenuItem_Click( object sender, EventArgs e )
        {
            DoWorkEventHandler doWork =
                delegate( object sender1, DoWorkEventArgs args )
                {
                    FFTPack.MergeDumpedFiles( folderBrowserDialog.SelectedPath, saveFileDialog.FileName, sender1 as BackgroundWorker );
                };
            ProgressChangedEventHandler progress =
                delegate( object sender2, ProgressChangedEventArgs args )
                {
                    progressBar.Visible = true;
                    progressBar.Value = args.ProgressPercentage;
                };
            RunWorkerCompletedEventHandler completed = null;
            completed =
                delegate( object sender3, RunWorkerCompletedEventArgs args )
                {
                    progressBar.Visible = false;
                    Enabled = true;
                    patchPsxBackgroundWorker.ProgressChanged -= progress;
                    patchPsxBackgroundWorker.RunWorkerCompleted -= completed;
                    patchPsxBackgroundWorker.DoWork -= doWork;
                    if( args.Error is Exception )
                    {
                        MessageBox.Show(
                            "Could not merge files.\n" +
                            "Make sure you chose the correct file and that there is\n" +
                            "enough room in the destination directory.",
                            "Error", MessageBoxButtons.OK );
                    }
                };

            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "fftpack.bin|fftpack.bin|All Files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            folderBrowserDialog.Description = "Where are the extracted files?";

            if( (folderBrowserDialog.ShowDialog( this ) == DialogResult.OK) && (saveFileDialog.ShowDialog( this ) == DialogResult.OK) )
            {
                patchPsxBackgroundWorker.ProgressChanged += progress;
                patchPsxBackgroundWorker.RunWorkerCompleted += completed;
                patchPsxBackgroundWorker.DoWork += doWork;

                Enabled = false;
                progressBar.Value = 0;
                progressBar.Visible = true;
                progressBar.BringToFront();
                patchPsxBackgroundWorker.RunWorkerAsync();
            }

        }

        private void saveAsPspMenuItem_Click( object sender, EventArgs e )
        {
            string fn = SavePatch( false );
            if( !string.IsNullOrEmpty( fn ) )
            {
                // HACK
                if( FFTPatch.Context != Context.US_PSP )
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load( fn );
                    FFTPatch.ConvertPsxPatchToPsp( doc );
                    doc.Save( fn );
                    FFTPatch.LoadPatch( fn );
                }
            }
        }

        private void saveMenuItem_Click( object sender, System.EventArgs e )
        {
            SavePatch( true );
        }

        private string SavePatch( bool digest )
        {
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "FFTPatcher files (*.fftpatch)|*.fftpatch";
            if( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                try
                {
                    FFTPatch.SavePatchToFile( saveFileDialog.FileName, FFTPatch.Context, digest );
                    return saveFileDialog.FileName;
                }
                catch( Exception )
                {
                    MessageBox.Show( "Could not open file.", "File not found", MessageBoxButtons.OK );
                }
            }
            return string.Empty;
        }


		#endregion Methods 

    }
}
