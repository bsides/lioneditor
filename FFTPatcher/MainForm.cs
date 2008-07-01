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

        #region Constructors (1)

        public MainForm()
        {
            InitializeComponent();
            openMenuItem.Click += openMenuItem_Click;
            applySCUSMenuItem.Click += applyMenuItem_Click;
            saveMenuItem.Click += saveMenuItem_Click;
            saveAsPspMenuItem.Click += saveAsPspMenuItem_Click;
            newPSPMenuItem.Click += newPSPMenuItem_Click;
            newPSXMenuItem.Click += newPSXMenuItem_Click;
            exitMenuItem.Click += exitMenuItem_Click;
            openModifiedMenuItem.Click += openModifiedMenuItem_Click;
            aboutMenuItem.Click += aboutMenuItem_Click;
            applySCUSMenuItem.Enabled = false;
            saveMenuItem.Enabled = false;
            saveAsPspMenuItem.Enabled = false;
            generateMenuItem.Click += generateMenuItem_Click;
            generateMenuItem.Enabled = false;
            generateFontMenuItem.Click += generateFontMenuItem_Click;
            applyBattleBinMenuItem.Click += applyBattleBinMenuItem_Click;

            extractFFTPackMenuItem.Click += extractFFTPackMenuItem_Click;
            rebuildFFTPackMenuItem.Click += rebuildFFTPackMenuItem_Click;
            decryptMenuItem.Click += decryptMenuItem_Click;

            patchPspIsoMenuItem.Click += patchPspIsoMenuItem_Click;
            patchPsxIsoMenuItem.Click += patchPsxIsoMenuItem_Click;
            cheatdbMenuItem.Click += cheatdbMenuItem_Click;

            FFTPatch.DataChanged += FFTPatch_DataChanged;
        }

        #endregion Constructors

        #region Methods (19)


        private void aboutMenuItem_Click( object sender, EventArgs e )
        {
            About a = new About();
            a.ShowDialog( this );
        }

        private void applyBattleBinMenuItem_Click( object sender, EventArgs e )
        {
            applyPatchOpenFileDialog.Filter = "BATTLE.BIN|BATTLE.BIN";
            if( applyPatchOpenFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                try
                {
                    FFTPatch.PatchBattleBin( applyPatchOpenFileDialog.FileName );
                    MessageBox.Show( "Patch complete!", "Finished", MessageBoxButtons.OK );
                }
                catch( InvalidDataException )
                {
                    MessageBox.Show(
                        "Could not find patch locations.\n" +
                        //"Ensure that the file is an ISO image if you are patching War of the Lions." +
                        "Ensure that you have selected BATTLE.BIN if you are patching Final Fantasy Tactics",
                        "Invalid data", MessageBoxButtons.OK );
                }
                catch( FileNotFoundException )
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

            if( applyPatchOpenFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                try
                {
                    FFTPatch.ApplyPatchesToExecutable( applyPatchOpenFileDialog.FileName );
                    MessageBox.Show( "Patch complete!", "Finished", MessageBoxButtons.OK );
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
            openFileDialog.Filter = "fftpack.bin|fftpack.bin|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            folderBrowserDialog.Description = "Where should the files be extracted?";

            bool oldEnabled = fftPatchEditor1.Enabled;

            if( (openFileDialog.ShowDialog( this ) == DialogResult.OK) && (folderBrowserDialog.ShowDialog( this ) == DialogResult.OK) )
            {
                try
                {
                    FFTPack.FileProgress += FFTPack_FileProgress;
                    progressBar.Visible = true;
                    progressBar.BringToFront();
                    fftPatchEditor1.Enabled = false;
                    FFTPack.DumpToDirectory( openFileDialog.FileName, folderBrowserDialog.SelectedPath );
                }
                catch( Exception )
                {
                    MessageBox.Show(
                        "Could not extract file.\n" +
                        "Make sure you chose the correct file and that there \n" +
                        "enough room in the destination directory.",
                        "Error", MessageBoxButtons.OK );
                }
                finally
                {
                    FFTPack.FileProgress -= FFTPack_FileProgress;
                    progressBar.SendToBack();
                    progressBar.Visible = false;
                    fftPatchEditor1.Enabled = oldEnabled;
                }
            }
        }

        private void FFTPack_FileProgress( object sender, ProgressEventArgs e )
        {
            progressBar.Location = new Point( (Width - progressBar.Width) / 2, (Height - progressBar.Height) / 2 );
            progressBar.Maximum = e.TotalTasks;
            progressBar.Minimum = 0;
            progressBar.Value = e.TasksComplete;
        }

        private void FFTPatch_DataChanged( object sender, EventArgs e )
        {
            applySCUSMenuItem.Enabled = FFTPatch.Context == Context.US_PSX;
            patchPsxIsoMenuItem.Enabled = FFTPatch.Context == Context.US_PSX;
            saveMenuItem.Enabled = true;
            generateMenuItem.Enabled = true;
            generateFontMenuItem.Enabled = FFTPatch.Context == Context.US_PSX;
            applyBattleBinMenuItem.Enabled = FFTPatch.Context == Context.US_PSX;
            generateMenuItem.Enabled = FFTPatch.Context == Context.US_PSX;

            patchPspIsoMenuItem.Enabled = FFTPatch.Context == Context.US_PSP;
            cheatdbMenuItem.Enabled = FFTPatch.Context == Context.US_PSP;

        }

        private void generateFontMenuItem_Click( object sender, EventArgs e )
        {
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "FONT.BIN|FONT.BIN";
            if( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                try
                {
                    FFTPatch.SaveFontsAs( saveFileDialog.FileName );
                }
                catch( Exception )
                {
                    MessageBox.Show( "Could not open or create file.", "File not found", MessageBoxButtons.OK );
                }
            }
        }

        private void generateMenuItem_Click( object sender, EventArgs e )
        {
            folderBrowserDialog.Description = "Where should the files be exported?\nAny files in the folder you choose with the n" +
                "ames ENTD1.ENT, ENTD2.ENT, ENTD3.ENT, or ENTD4.ENT will be overwritte" +
                "n.";
            if( folderBrowserDialog.ShowDialog( this ) == DialogResult.OK )
            {
                try
                {
                    FFTPatch.ExportENTDFiles( folderBrowserDialog.SelectedPath );
                }
                catch( Exception )
                {
                    MessageBox.Show( "Could not save files.", "Error", MessageBoxButtons.OK );
                }
            }
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

        private void openModifiedMenuItem_Click( object sender, System.EventArgs e )
        {
            openFileDialog.Filter = "SCUS_942.21|SCUS_942.21";
            if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                try
                {
                    FFTPatch.OpenModifiedPSXFile( openFileDialog.FileName );
                    saveAsPspMenuItem.Enabled = true;
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

        private void patchPspIsoMenuItem_Click( object sender, EventArgs e )
        {
            applyPatchOpenFileDialog.Filter = "War of the Lions ISO images (*.iso)|*.iso";
            if( applyPatchOpenFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                try
                {
                    PspIso.PatchISO( applyPatchOpenFileDialog.FileName );
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

        private void patchPsxIsoMenuItem_Click( object sender, EventArgs e )
        {
            DoWorkEventHandler doWork =
                delegate( object sender1, DoWorkEventArgs args )
                {
                    FFTPatch.PatchPsxIso( sender1 as BackgroundWorker, args );
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

            saveFileDialog.OverwritePrompt = false;
            if ( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                patchPsxBackgroundWorker.ProgressChanged += progress;
                patchPsxBackgroundWorker.RunWorkerCompleted += completed;
                patchPsxBackgroundWorker.DoWork += doWork;
                
                Enabled = false;

                progressBar.Value = 0;
                progressBar.Visible = true;

                patchPsxBackgroundWorker.RunWorkerAsync( saveFileDialog.FileName );
            }
        }


        private void patchPsxBackgroundWorker_DoWork( object sender, DoWorkEventArgs e )
        {
            FFTPatch.PatchPsxIso( sender as BackgroundWorker, e );
        }

        private void PatchDataReceived( object sender, DataReceivedEventArgs e )
        {
        }

        private void PatchFinished( object sender, EventArgs e )
        {
            Process p = sender as Process;
            if( p != null && p.ExitCode != 0 )
            {
                MethodInvoker mii = new MethodInvoker(
                    delegate()
                    {
                        MessageBox.Show( "Error while patching", "Error", MessageBoxButtons.OK );
                    } );
                if( InvokeRequired )
                {
                    Invoke( mii );
                }
                else
                {
                    mii();
                }
            }

            MethodInvoker mi = new MethodInvoker(
                delegate()
                {
                    Enabled = true;
                } );
            if( fftPatchEditor1.InvokeRequired )
            {
                Invoke( mi );
            }
            else
            {
                mi();
            }
        }

        private void rebuildFFTPackMenuItem_Click( object sender, EventArgs e )
        {
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "fftpack.bin|fftpack.bin|All Files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            folderBrowserDialog.Description = "Where are the extracted files?";
            bool oldEnabled = fftPatchEditor1.Enabled;

            if( (folderBrowserDialog.ShowDialog( this ) == DialogResult.OK) && (saveFileDialog.ShowDialog( this ) == DialogResult.OK) )
            {
                try
                {
                    FFTPack.FileProgress += FFTPack_FileProgress;
                    progressBar.Visible = true;
                    fftPatchEditor1.Enabled = false;

                    FFTPack.MergeDumpedFiles( folderBrowserDialog.SelectedPath, saveFileDialog.FileName );
                }
                catch( Exception )
                {
                    MessageBox.Show(
                        "Could not merge files.\n" +
                        "Make sure you chose the correct path and that there \n" +
                        "enough room in the destination location.",
                        "Error", MessageBoxButtons.OK );
                }
                finally
                {
                    FFTPack.FileProgress -= FFTPack_FileProgress;
                    progressBar.Visible = false;
                    fftPatchEditor1.Enabled = oldEnabled;
                }
            }
        }

        private void saveMenuItem_Click( object sender, System.EventArgs e )
        {
            SavePatch( true );
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
