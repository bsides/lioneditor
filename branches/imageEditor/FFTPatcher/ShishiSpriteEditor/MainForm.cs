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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace FFTPatcher.SpriteEditor
{
    public partial class MainForm : Form
    {

        #region Fields (3)

        string filename = string.Empty;
        private List<Shape> shapes;
        private SpriteDialog dialog = new SpriteDialog();

        FFTPatcher.Controls.ProgressBarWithText progressBar = new FFTPatcher.Controls.ProgressBarWithText();
        #endregion Fields

        #region Constructors (1)

        public MainForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;

            openMenuItem.Click += openMenuItem_Click;
            saveMenuItem.Click += saveMenuItem_Click;

            progressBar.Visible = false;
            progressBar.Enabled = false;
            progressBar.ProgressBarBlockSpace = 0;
            progressBar.ProgressBarBlockWidth = 1;
            progressBar.ProgressBarFillColor = Color.Blue;
            progressBar.ForeColor = Color.White;
            Controls.Add( progressBar );

            //paletteSaveMenuItem.Click += paletteSaveMenuItem_Click;
            //paletteOpenMenuItem.Click += paletteOpenMenuItem_Click;

            //exportMenuItem.Click += exportMenuItem_Click;
            //importMenuItem.Click += importMenuItem_Click;

            //paletteSelector.SelectedIndexChanged += paletteSelector_SelectedIndexChanged;
            //portraitCheckbox.CheckedChanged += portraitCheckbox_CheckedChanged;
            //properCheckbox.CheckedChanged += new EventHandler( properCheckbox_CheckedChanged );

            aboutMenuItem.Click += aboutMenuItem_Click;
            exitMenuItem.Click += exitMenuItem_Click;
            importPspMenuItem.Click += new EventHandler( importPspMenuItem_Click );
            importPsxMenuItem.Click += new EventHandler( importPsxMenuItem_Click );

            //shapesListBox.MeasureItem += new MeasureItemEventHandler( shapesListBox_MeasureItem );
            //shapesListBox.DrawItem += new DrawItemEventHandler( shapesListBox_DrawItem );
            //shapesListBox.SelectedIndexChanged += new EventHandler( shapesListBox_SelectedIndexChanged );

            fullSpriteSetEditor1.ImageActivated += new EventHandler<FullSpriteSetEditor.ImageEventArgs>( fullSpriteSetEditor1_ImageActivated );
            BuildShapes();
            //shapesComboBox.Items.AddRange( shapes.ToArray() );
            //shapesComboBox.SelectedIndexChanged += new EventHandler( shapesComboBox_SelectedIndexChanged );
        }

        void fullSpriteSetEditor1_ImageActivated( object sender, FullSpriteSetEditor.ImageEventArgs e )
        {
            dialog.ShowDialog( e.Sprite, this );
        }

        #endregion Constructors

        #region Methods (19)


        private void aboutMenuItem_Click( object sender, EventArgs e )
        {
            new About().ShowDialog( this );
        }

        private void BuildShapes()
        {
            shapes = new List<Shape>();
            shapes.Add( Shape.ARUTE );
            shapes.Add( Shape.TYPE1 );
            shapes.Add( Shape.TYPE2 );
            shapes.Add( Shape.CYOKO );
            shapes.Add( Shape.KANZEN );
        }

        private void exitMenuItem_Click( object sender, EventArgs e )
        {
            Application.Exit();
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

        void importPsxMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.FileName = string.Empty;
            openFileDialog.Filter = "PSX ISO files (*.iso, *.img, *.bin)|*.iso;*.img;*.bin";
            openFileDialog.FilterIndex = 0;

            if ( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                fullSpriteSetEditor1.LoadFullSpriteSet( FullSpriteSet.FromPsxISO( openFileDialog.FileName ) );
            }
        }

        void importPspMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.FileName = string.Empty;
            openFileDialog.Filter = "PSP ISO files (*.iso)|*.iso";
            openFileDialog.FilterIndex = 0;

            if ( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                fullSpriteSetEditor1.LoadFullSpriteSet( FullSpriteSet.FromPspISO( openFileDialog.FileName ) );
            }
        }

        public delegate void ProgressReport( int percent, string message );

        private void openMenuItem_Click( object sender, System.EventArgs e )
        {
            openFileDialog.FileName = string.Empty;
            openFileDialog.Filter = "Shishi Sprite Manager files (*.shishi)|*.shishi";
            openFileDialog.FilterIndex = 0;

            if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                DoWorkEventHandler doWork =
                    delegate( object o, DoWorkEventArgs args )
                    {
                        FullSpriteSet set = FullSpriteSet.FromShishiFile( openFileDialog.FileName, o as BackgroundWorker );
                        if ( fullSpriteSetEditor1.InvokeRequired )
                        {
                            fullSpriteSetEditor1.Invoke( new MethodInvoker( delegate() { fullSpriteSetEditor1.LoadFullSpriteSet( set ); } ) );
                        }
                        else
                        {
                            fullSpriteSetEditor1.LoadFullSpriteSet( set );
                        }
                    };
                ProgressChangedEventHandler changed =
                    delegate( object o3, ProgressChangedEventArgs args3 )
                    {
                        progressBar.Value = args3.ProgressPercentage;
                        progressBar.ProgressBarText = args3.UserState as string;
                    };
                RunWorkerCompletedEventHandler completed = null;
                completed = 
                    delegate( object o2, RunWorkerCompletedEventArgs args2 )
                    {
                        backgroundWorker1.DoWork -= doWork;
                        backgroundWorker1.ProgressChanged -= changed;
                        backgroundWorker1.RunWorkerCompleted -= completed;
                        progressBar.Visible = false;
                        progressBar.Enabled = false;
                        Enabled = true;
                    };
                Enabled = false;
                progressBar.Bounds =
                    new Rectangle(
                        ClientRectangle.Left + 10,
                        ( ClientRectangle.Height - progressBar.Height ) / 2, 
                        ClientRectangle.Width - 20, 
                        progressBar.Height );
                progressBar.Enabled = false;
                progressBar.Value = 0;
                progressBar.ProgressBarText = string.Empty;
                progressBar.Visible = true;
                progressBar.BringToFront();

                backgroundWorker1.DoWork += doWork;
                backgroundWorker1.ProgressChanged += changed;
                backgroundWorker1.RunWorkerCompleted += completed;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void paletteOpenMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.FileName = string.Empty;
            openFileDialog.Filter = "Palette files (*.PAL)|*.PAL";
            openFileDialog.FilterIndex = 0;

            if ( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                byte[] bytes = null;
                try
                {
                    bytes = GetBytes( openFileDialog.FileName );
                    //spriteViewer1.Sprite.Palettes = Palette.FromPALFile( bytes );
                    //paletteSelector.SelectedIndex = 0;
                    //spriteViewer1.Invalidate();
                    //if( shapesComboBox.SelectedIndex != -1 )
                    //{
                    //    shapesComboBox_SelectedIndexChanged( null, EventArgs.Empty );
                    //}
                }
                catch ( Exception )
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
                //SaveBytes( saveFileDialog.FileName, spriteViewer1.Sprite.Palettes.ToPALFile() );
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
            saveFileDialog.FileName = string.Empty;
            saveFileDialog.Filter = "Shishi Sprite Manager files (*.shishi)|*.shishi";
            saveFileDialog.FilterIndex = 0;
            if ( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                fullSpriteSetEditor1.FullSpriteSet.SaveShishiFile( saveFileDialog.FileName );
            }
        }

        #endregion Methods

        private void fileMenu_Popup( object sender, EventArgs e )
        {
            openMenuItem.Enabled = true;
            saveMenuItem.Enabled = fullSpriteSetEditor1.FullSpriteSet != null;
        }

        private void pspMenuItem_Popup( object sender, EventArgs e )
        {
            patchPspMenuItem.Enabled = fullSpriteSetEditor1.FullSpriteSet != null;
            importPspMenuItem.Enabled = true;
        }

        private void psxMenuItem_Popup( object sender, EventArgs e )
        {
            patchPsxMenuItem.Enabled = fullSpriteSetEditor1.FullSpriteSet != null;
            importPsxMenuItem.Enabled = true;
        }

    }
}
