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

namespace FFTPatcher.SpriteEditor
{
    public partial class MainForm : Form
    {

        #region Fields (3)

        string filename = string.Empty;
        private List<Shape> shapes;
        private SpriteDialog dialog = new SpriteDialog();

        #endregion Fields

        #region Constructors (1)

        public MainForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;

            openMenuItem.Click += openMenuItem_Click;
            saveMenuItem.Click += saveMenuItem_Click;

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

        private void exportMenuItem_Click( object sender, EventArgs e )
        {
            saveFileDialog.FileName = string.Empty;
            saveFileDialog.Filter = "Bitmap files (*.BMP)|*.BMP";
            saveFileDialog.FilterIndex = 0;
            if( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                //using( Bitmap bmp = spriteViewer1.Sprite.ToBitmap() )
                //{
                //    bmp.Save( saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp );
                //}
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
                    bool bad = false;
                    //spriteViewer1.Sprite.ImportBitmap( b, out bad );
                    //spriteViewer1.Invalidate();
                    if( bad )
                    {
                        MessageBox.Show( this, "The imported file had some pixels that weren't in the first 16 palette entries.", "Warning", MessageBoxButtons.OK );
                    }
                }
                //if( shapesComboBox.SelectedIndex != -1 )
                //{
                //    shapesComboBox_SelectedIndexChanged( null, EventArgs.Empty );
                //}
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

        private void openMenuItem_Click( object sender, System.EventArgs e )
        {
            openFileDialog.FileName = string.Empty;
            openFileDialog.Filter = "Shishi Sprite Manager files (*.shishi)|*.shishi";
            openFileDialog.FilterIndex = 0;

            if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                fullSpriteSetEditor1.LoadFullSpriteSet( FullSpriteSet.FromShishiFile( openFileDialog.FileName ) );
                int j = fullSpriteSetEditor1.FullSpriteSet.Sprites.Count;
                for ( int i = 0; i < j; i++ )
                {
                    fullSpriteSetEditor1.FullSpriteSet.Thumbnails.Images[i].Save( fullSpriteSetEditor1.FullSpriteSet.Sprites[i].Name + ".thumb.png", System.Drawing.Imaging.ImageFormat.Png );
                    //fullSpriteSetEditor1.FullSpriteSet.Sprites[i].ToBitmap().Save( fullSpriteSetEditor1.FullSpriteSet.Sprites[i].Name + ".png", System.Drawing.Imaging.ImageFormat.Png );
                }
                //foreach ( var s in fullSpriteSetEditor1.FullSpriteSet.Thumbnails.Images )
                //{
                //    (s as Image).Save(
                //}
                //foreach ( var s in new FullSpriteSet( openFileDialog.FileName, FFTPatcher.Datatypes.Context.US_PSX ).Sprites )
                //{
                //    if ( !( s is ShortSprite ) )
                //    {
                //        s.GetThumbnail().Save( s.Name + ".png", System.Drawing.Imaging.ImageFormat.Png );
                //    }
                //}
                //byte[] bytes = null;
                //try
                //{
                //    bytes = GetBytes( openFileDialog.FileName );
                //    spriteViewer1.SetPalette( 0, 8 );
                //    paletteSelector.SelectedIndex = 0;
                //    spriteViewer1.Sprite = new AbstractSprite( bytes );
                //    if( shapesComboBox.SelectedIndex != -1 )
                //    {
                //        shapesComboBox_SelectedIndexChanged( null, EventArgs.Empty );
                //    }

                //    paletteSelector.Enabled = true;
                //    properCheckbox.Enabled = true;
                //    portraitCheckbox.Enabled = true;
                //    saveMenuItem.Enabled = true;
                //    saveAsMenuItem.Enabled = true;
                //    importMenuItem.Enabled = true;
                //    exportMenuItem.Enabled = true;
                //    paletteSaveMenuItem.Enabled = true;
                //    paletteOpenMenuItem.Enabled = true;
                //    shapesComboBox.Enabled = true;

                //    filename = openFileDialog.FileName;
                //}
                //catch( Exception )
                //{
                //    MessageBox.Show( "Could not open file.", "Error", MessageBoxButtons.OK );
                //}
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
                    //spriteViewer1.Sprite.Palettes = Palette.FromPALFile( bytes );
                    //paletteSelector.SelectedIndex = 0;
                    //spriteViewer1.Invalidate();
                    //if( shapesComboBox.SelectedIndex != -1 )
                    //{
                    //    shapesComboBox_SelectedIndexChanged( null, EventArgs.Empty );
                    //}
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

        private void shapesComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            //if( shapesComboBox.SelectedIndex != -1 )
            //{
            //    Shape s = shapesComboBox.Items[shapesComboBox.SelectedIndex] as Shape;
            //    if( s != null )
            //    {
            //        IList<Frame> f = s.Frames;
            //        frames = new List<Bitmap>( f.Count );
            //        foreach( Frame frame in f )
            //        {
            //            frames.Add( frame.GetFrame( spriteViewer1.Sprite ) );
            //        }

            //        shapesListBox.BeginUpdate();
            //        shapesListBox.Items.Clear();
            //        shapesListBox.Items.AddRange( f.ToArray() );
            //        shapesListBox.EndUpdate();
            //    }
            //}
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
