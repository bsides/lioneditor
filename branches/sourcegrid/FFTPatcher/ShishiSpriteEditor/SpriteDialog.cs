using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FFTPatcher.SpriteEditor
{
    public partial class SpriteDialog : Form
    {
        public AbstractSprite Sprite { get; private set; }
        private IList<Bitmap> frames = null;

        public SpriteDialog()
        {
            InitializeComponent();
            paletteSelector.SelectedIndexChanged += new EventHandler( paletteSelector_SelectedIndexChanged );
            portraitCheckbox.CheckedChanged += new EventHandler( portraitCheckbox_CheckedChanged );
            shapesListBox.DrawItem += new DrawItemEventHandler( shapesListBox_DrawItem );
            shapesListBox.MeasureItem += new MeasureItemEventHandler(shapesListBox_MeasureItem);
            shapesListBox.SelectedIndexChanged += new EventHandler( shapesListBox_SelectedIndexChanged );
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
        }

        private void paletteSelector_SelectedIndexChanged( object sender, EventArgs e )
        {
            spriteViewer1.SetPalette( paletteSelector.SelectedIndex, portraitCheckbox.Checked ? (paletteSelector.SelectedIndex % 8 + 8) : paletteSelector.SelectedIndex );
        }

        private new DialogResult ShowDialog()
        {
            return DialogResult.Cancel;
        }

        private new DialogResult ShowDialog( IWin32Window owner )
        {
            return DialogResult.Cancel;
        }

        public void ShowDialog( AbstractSprite sprite, IWin32Window owner )
        {
            if ( Sprite != null )
            {
                Sprite.PixelsChanged -= sprite_PixelsChanged;
            }

            Sprite = sprite;
            spriteViewer1.Location = new Point( 3, 3 );
            spriteViewer1.Sprite = sprite;
            shapesListBox.Visible = sprite.Shape != null;
            paletteSelector.SelectedIndex = 0;
            Text = sprite.Name;

            sp2Menu.Visible = sprite is MonsterSprite;
            sp2Menu.Enabled = sprite is MonsterSprite;

            UpdateFrames();

            sprite.PixelsChanged += new EventHandler( sprite_PixelsChanged );

            base.ShowDialog( owner );
        }

        private void sprite_PixelsChanged( object sender, EventArgs e )
        {
            UpdateFrames();
            spriteViewer1.Sprite = null;
            spriteViewer1.Sprite = Sprite;
        }

        private void UpdateFrames()
        {
            Shape s = Sprite.Shape;
            if ( s != null )
            {
                IList<Frame> f = s.Frames;
                frames = new List<Bitmap>( f.Count );
                foreach ( Frame frame in f )
                {
                    frames.Add( frame.GetFrame( spriteViewer1.Sprite ) );
                }

                shapesListBox.BeginUpdate();
                shapesListBox.Items.Clear();
                shapesListBox.Items.AddRange( f.ToArray() );
                shapesListBox.EndUpdate();
            }
        }

        private void portraitCheckbox_CheckedChanged( object sender, EventArgs e )
        {
            spriteViewer1.SetPalette( paletteSelector.SelectedIndex, portraitCheckbox.Checked ? (paletteSelector.SelectedIndex % 8 + 8) : paletteSelector.SelectedIndex );
        }

        private void shapesListBox_DrawItem( object sender, DrawItemEventArgs e )
        {
            if( shapesListBox.Items.Count > 0 && spriteViewer1.Sprite != null )
            {
                if( (e.State & DrawItemState.Selected) == DrawItemState.Selected )
                {
                    e.Graphics.FillRectangle( SystemBrushes.Highlight, e.Bounds );
                }
                else
                {
                    e.Graphics.FillRectangle( SystemBrushes.Window, e.Bounds );
                }

                Bitmap f = frames[e.Index];
                e.Graphics.DrawImage( f, new Point( e.Bounds.Location.X + 10, e.Bounds.Location.Y + 10 ) );
            }
        }

        private void shapesListBox_MeasureItem( object sender, MeasureItemEventArgs e )
        {
            e.ItemHeight = 180;
            e.ItemWidth = 230;
        }

        private void shapesListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            Frame f = shapesListBox.Items[shapesListBox.SelectedIndex] as Frame;
            if( f != null )
            {
                spriteViewer1.HighlightTiles( f.Tiles );
            }
        }

        private void importBmpMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.FileName = string.Empty;
            openFileDialog.Filter = "Bitmap files (*.BMP)|*.BMP";
            openFileDialog.FilterIndex = 0;
            if ( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                using ( Bitmap b = new Bitmap( openFileDialog.FileName ) )
                {
                    bool bad = false;
                    bool success = DoFileOperation( () =>
                        Sprite.ImportBitmap( b, out bad ) );

                    if ( success && bad )
                    {
                        MessageBox.Show( this, "The imported file had some pixels that weren't in the first 16 palette entries.", "Warning", MessageBoxButtons.OK );
                    }
                }

                UpdateFrames();
                shapesListBox.Invalidate();
            }
        }

        private void exportBmpMenuItem_Click( object sender, EventArgs e )
        {
            saveFileDialog.FileName = string.Empty;
            saveFileDialog.Filter = "Bitmap files (*.BMP)|*.BMP";
            saveFileDialog.FilterIndex = 0;
            if ( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                DoFileOperation( () =>
                    Sprite.ToBitmap().Save( saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp ) );
            }
        }

        private void closeMenuItem_Click( object sender, EventArgs e )
        {
            Close();
        }

        private bool DoFileOperation( MethodInvoker action )
        {
            try
            {
                action();
                return true;
            }
            catch ( PathTooLongException )
            {
                MessageBox.Show( this, "Path too long" );
                return false;
            }
            catch ( IOException )
            {
                MessageBox.Show( this, "IO Error" );
                return false;
            }
            catch ( UnauthorizedAccessException )
            {
                MessageBox.Show( this, "Unauthorized access" );
                return false;
            }
            catch ( System.Security.SecurityException )
            {
                MessageBox.Show( this, "Security error" );
                return false;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        private void importSprMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.Filter = Sprite.Filenames[0] + "|" + Sprite.Filenames[0];
            if ( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                Sprite.ImportSPR( File.ReadAllBytes( openFileDialog.FileName ) );
            }
        }

        private void ShowSaveFile( int whichFile, string filter )
        {
            saveFileDialog.Filter = filter;
            saveFileDialog.FileName = string.Empty;
            if ( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                IList<byte[]> bytes = Sprite.ToByteArrays();
                DoFileOperation( () =>
                    File.WriteAllBytes( saveFileDialog.FileName, bytes[whichFile] ) );
            }
        }

        private const string sprFilter = "SPR files (*.spr)|*.spr";

        private void exportSprMenuItem_Click( object sender, EventArgs e )
        {
            ShowSaveFile( 0, sprFilter );
        }

        private void ShowOpenSP2( int whichSp2, string filter )
        {
            saveFileDialog.Filter = filter;
            saveFileDialog.FileName = string.Empty;
            if ( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                ( Sprite as MonsterSprite ).ImportSP2( File.ReadAllBytes( openFileDialog.FileName ), whichSp2 );
            }
        }

        private void importSp2MenuItem_Click( object sender, EventArgs e )
        {
            ShowOpenSP2( 0, sp2Filter );
        }

        private const string sp2Filter = "SP2 files (*.sp2)|*.sp2";

        private void exportSp2MenuItem_Click( object sender, EventArgs e )
        {
            ShowSaveFile( 1, sp2Filter );
        }

        private void importSp2bMenuItem_Click( object sender, EventArgs e )
        {
            ShowOpenSP2( 1, sp2Filter );
        }

        private void exportSp2bMenuItem_Click( object sender, EventArgs e )
        {
            ShowSaveFile( 2, sp2Filter );
        }

        private void importSp2cMenuItem_Click( object sender, EventArgs e )
        {
            ShowOpenSP2( 2, sp2Filter );
        }

        private void exportSp2cMenuItem_Click( object sender, EventArgs e )
        {
            ShowSaveFile( 3, sp2Filter );
        }

        private void importSp2dMenuItem_Click( object sender, EventArgs e )
        {
            ShowOpenSP2( 3, sp2Filter );
        }

        private void exportSp2dMenuItem_Click( object sender, EventArgs e )
        {
            ShowSaveFile( 4, sp2Filter );
        }

        private void sp2Menu_Popup( object sender, EventArgs e )
        {
            bool vis = Sprite.Height > ( 488 + 256 );
            importSp2MenuItem.Text = string.Format( "&Import {0}...", Sprite.Filenames[1] );
            exportSp2MenuItem.Text = string.Format( "E&xport {0}...", Sprite.Filenames[1] );

            MenuItem[] imports = new MenuItem[] { importSp2bMenuItem, importSp2cMenuItem, importSp2dMenuItem };
            MenuItem[] exports = new MenuItem[] { exportSp2bMenuItem, exportSp2cMenuItem, exportSp2dMenuItem };
            MenuItem[] separators = new MenuItem[] { sp2Separator1, sp2Separator2, sp2Separator3 };

            imports.ForEach( m => m.Visible = vis );
            exports.ForEach( m => m.Visible = vis );
            separators.ForEach( m => m.Visible = vis );

            if ( vis )
            {
                for ( int i = 0; i < 3; i++ )
                {
                    imports[i].Text = string.Format( "Import {0}...", Sprite.Filenames[2 + i] );
                    exports[i].Text = string.Format( "Export {0}...", Sprite.Filenames[2 + i] );
                }
            }
        }

        private void sprMenu_Popup( object sender, EventArgs e )
        {
            importSprMenuItem.Text = string.Format( "&Import SPR..." );
            exportSprMenuItem.Text = string.Format( "E&xport SPR..." );
        }

        private void exportBmpCurrentPalette_Click( object sender, EventArgs e )
        {
            saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp";
            saveFileDialog.FileName = string.Empty;
            if( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                using( Bitmap bmp = Sprite.To4bppBitmapUncached( paletteSelector.SelectedIndex ) )
                {
                    bmp.Save( saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp );
                }
            }
        }
    }
}
