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

        const string titleFormatString = "Shishi Sprite Manager - {0}";

        public MainForm()
        {
            InitializeComponent();
            comboBox1.DataSource = comboBoxFiles;
            List<FrameBinRectangle> realRects = new List<FrameBinRectangle>( rects );
            Comparison<FrameBinRectangle> comp = 
                delegate(FrameBinRectangle a, FrameBinRectangle b)
                {
                    int result = a.Type.ToString().CompareTo(b.Type.ToString());
                    if (result == 0)
                    {
                        result = a.File.ToString().CompareTo(b.File.ToString());
                    }
                    if (result == 0)
                    {
                        result = a.Offset.CompareTo(b.Offset);
                    }
                    return result;
                };
            realRects.Sort( comp );
            rects = new Set<FrameBinRectangle>( realRects );
            listBox1.DataSource = realRects;
            pictureBox1.DragEnter += panel1_DragEnter;
            pictureBox1.DragDrop += panel1_DragDrop;
            pictureBox1.AllowDrop = true;
        }

        private Stream currentStream = null;

        private void openIsoMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.Filter = "ISO files (*.bin, *.iso, *.img)|*.bin;*.iso;*.img";
            openFileDialog.FileName = string.Empty;
            if (openFileDialog.ShowDialog( this ) == DialogResult.OK)
            {
                Stream openedStream = File.Open( openFileDialog.FileName, FileMode.Open, FileAccess.ReadWrite );
                if (openedStream != null)
                {
                    if (openedStream.Length % PatcherLib.Iso.IsoPatch.SectorSizes[PatcherLib.Iso.IsoPatch.IsoType.Mode1] == 0 ||
                         (openedStream.Length % PatcherLib.Iso.IsoPatch.SectorSizes[PatcherLib.Iso.IsoPatch.IsoType.Mode2Form1] == 0 &&
                          (AllSprites.DetectExpansionOfPsxIso( openedStream ) ||
                            MessageBox.Show( this, "ISO needs to be restructured." + Environment.NewLine + "Restructure?", "Restructure ISO?", MessageBoxButtons.OKCancel ) == DialogResult.OK)))
                    {
                        if (currentStream != null)
                        {
                            currentStream.Flush();
                            currentStream.Close();
                            currentStream.Dispose();
                        }
                        currentStream = openedStream;

                        AllSprites s = AllSprites.FromIso( currentStream );
                        allSpritesEditor1.BindTo( s, currentStream );
                        tabControl1.Enabled = true;
                        spriteMenuItem.Enabled = true;
                        sp2Menu.Enabled = true;

                        var otherImages = AllOtherImages.FromIso( currentStream );
                        allOtherImagesEditor1.BindTo( otherImages, currentStream );

                        if ( openedStream.Length % PatcherLib.Iso.IsoPatch.SectorSizes[PatcherLib.Iso.IsoPatch.IsoType.Mode2Form1] == 0 )
                        {
                            frameBin =
                                new PalettedImage4bpp( "frame.bin", 256, 256, 1, Palette.ColorDepth._16bit,
                                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_FRAME_BIN, 0x1000, 256 * 256 / 2 ),
                                    new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_FRAME_BIN, 0x9000, 16 * 2 ) );
                        }
                        else if ( openedStream.Length % PatcherLib.Iso.IsoPatch.SectorSizes[PatcherLib.Iso.IsoPatch.IsoType.Mode1] == 0 )
                        {
                            frameBin =
                                new PalettedImage4bpp( "frame.bin", 256, 256, 1, Palette.ColorDepth._16bit,
                                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_FRAME_BIN, 0x1000, 256 * 256 / 2 ),
                                    new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_FRAME_BIN, 0x9000, 16 * 2 ) );
                        }

                        Text = string.Format( titleFormatString, Path.GetFileName( openFileDialog.FileName ) );
                    }
                    else
                    {
                        openedStream.Close();
                        openedStream.Dispose();
                    }
                }
            }
        }

        protected override void OnClosing( CancelEventArgs e )
        {
            if (currentStream != null)
            {
                currentStream.Flush();
                currentStream.Close();
                currentStream.Dispose();
                currentStream = null;
            }
            base.OnClosing( e );
        }

        private void importSprMenuItem_Click( object sender, EventArgs e )
        {
            Sprite currentSprite = allSpritesEditor1.CurrentSprite;
            openFileDialog.Filter = "FFT Sprite (*.SPR)|*.spr";
            openFileDialog.FileName = string.Empty;
            openFileDialog.CheckFileExists = true;
            if (currentSprite != null && openFileDialog.ShowDialog( this ) == DialogResult.OK)
            {
                try
                {
                    currentSprite.ImportSprite( currentStream, openFileDialog.FileName );
                    allSpritesEditor1.ReloadCurrentSprite();
                }
                catch (SpriteTooLargeException ex)
                {
                    MessageBox.Show( this, ex.Message, "Error" );
                }
            }
        }

        private void exportSprMenuItem_Click( object sender, EventArgs e )
        {
            Sprite currentSprite = allSpritesEditor1.CurrentSprite;
            saveFileDialog.Filter = "FFT Sprite (*.SPR)|*.spr";
            saveFileDialog.FileName = string.Empty;
            saveFileDialog.CreatePrompt = false;
            saveFileDialog.OverwritePrompt = true;
            if (currentSprite != null && saveFileDialog.ShowDialog( this ) == DialogResult.OK)
            {
                File.WriteAllBytes( saveFileDialog.FileName, currentSprite.GetAbstractSpriteFromIso( currentStream ).ToByteArray( 0 ) );
            }
        }

        private void importBmpMenuItem_Click( object sender, EventArgs e )
        {
            Sprite currentSprite = allSpritesEditor1.CurrentSprite;
            openFileDialog.Filter = "8bpp paletted bitmap (*.BMP)|*.bmp";
            openFileDialog.FileName = string.Empty;
            openFileDialog.CheckFileExists = true;
            if (currentSprite != null && openFileDialog.ShowDialog( this ) == DialogResult.OK)
            {
                currentSprite.ImportBitmap( currentStream, openFileDialog.FileName );
                allSpritesEditor1.ReloadCurrentSprite();
            }
        }

        private void exportBmpMenuItem_Click( object sender, EventArgs e )
        {
            Sprite currentSprite = allSpritesEditor1.CurrentSprite;
            saveFileDialog.Filter = "8bpp paletted bitmap (*.BMP)|*.bmp";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.CreatePrompt = false;
            saveFileDialog.FileName = string.Empty;

            if (currentSprite != null && saveFileDialog.ShowDialog( this ) == DialogResult.OK)
            {
                currentSprite.GetAbstractSpriteFromIso( currentStream ).ToBitmap().Save( saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp );
            }
        }

        private void exitMenuItem_Click( object sender, EventArgs e )
        {
            Application.Exit();
        }

        private void importSp2MenuItem_Click( object sender, EventArgs e )
        {
            int index = Int32.Parse( (sender as Menu).Tag.ToString() );
            MonsterSprite sprite = allSpritesEditor1.CurrentSprite.GetAbstractSpriteFromIso( currentStream ) as MonsterSprite;
            if (sprite != null)
            {
                openFileDialog.Filter = "SP2 files (*.SP2)|*.sp2";
                openFileDialog.FileName = string.Empty;
                openFileDialog.CheckFileExists = true;
                if (openFileDialog.ShowDialog( this ) == DialogResult.OK)
                {
                    (allSpritesEditor1.CurrentSprite as CharacterSprite).ImportSp2( currentStream, openFileDialog.FileName, index - 1 );
                    allSpritesEditor1.ReloadCurrentSprite();
                }
            }
        }

        private void exportSp2MenuItem_Click( object sender, EventArgs e )
        {
            int index = Int32.Parse( (sender as Menu).Tag.ToString() );
            MonsterSprite sprite = allSpritesEditor1.CurrentSprite.GetAbstractSpriteFromIso( currentStream ) as MonsterSprite;
            if (sprite != null)
            {
                saveFileDialog.Filter = "SP2 files (*.SP2)|*.sp2";
                saveFileDialog.FileName = string.Empty;
                saveFileDialog.CreatePrompt = false;
                saveFileDialog.OverwritePrompt = true;
                if (saveFileDialog.ShowDialog( this ) == DialogResult.OK)
                {
                    File.WriteAllBytes( saveFileDialog.FileName, sprite.ToByteArray( index ) );
                }
            }
        }

        private void sp2Menu_Popup( object sender, EventArgs e )
        {
            foreach (MenuItem mi in sp2Menu.MenuItems)
            {
                mi.Enabled = false;
            }

            MonsterSprite sprite = allSpritesEditor1.CurrentSprite.GetAbstractSpriteFromIso( currentStream ) as MonsterSprite;
            if (sprite != null && allSpritesEditor1.CurrentSprite is CharacterSprite)
            {
                int numChildren = (allSpritesEditor1.CurrentSprite as CharacterSprite).NumChildren;
                for (int i = 0; i < numChildren; i++)
                {
                    sp2Menu.MenuItems[i * 3].Enabled = true;
                    sp2Menu.MenuItems[i * 3 + 1].Enabled = true;
                }
            }
        }

        private void importImageMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.FileName = string.Empty;
            const string allImagesFilter = "All images (*.png, *.gif, *.jpg, *.bmp, *.tiff)|*.png;*.gif;*.jpg;*.jpeg;*.bmp;*.tiff;*.tif";
            const string pngFilter = "PNG images (*.png)|*.png";
            const string gifFilter = "GIF images (*.gif)|*.gif";
            const string jpgFilter = "JPEG images (*.jpg)|*.jpg;*.jpeg";
            const string bmpFilter = "Bitmap images (*.bmp)|*.bmp";
            const string tifFilter = "TIFF images (*.tiff)|*.tif;*.tiff";
            openFileDialog.Filter = string.Join( "|", new string[] { allImagesFilter, pngFilter, gifFilter, jpgFilter, bmpFilter, tifFilter } );

            if (openFileDialog.ShowDialog( this ) == DialogResult.OK)
            {
                allOtherImagesEditor1.LoadToCurrentImage( openFileDialog.FileName );
            }
        }

        private void exportImageMenuItem_Click( object sender, EventArgs e )
        {
            saveFileDialog.FileName = string.Empty;
            saveFileDialog.Filter = allOtherImagesEditor1.GetCurrentImageFileFilter();

            if (saveFileDialog.ShowDialog( this ) == DialogResult.OK)
            {
                allOtherImagesEditor1.SaveCurrentImage( saveFileDialog.FileName );
            }
        }

        private void tabControl1_SelectedIndexChanged( object sender, EventArgs e )
        {
            bool image = tabControl1.SelectedTab == otherTabPage;
            spriteMenuItem.Visible = !image;
            sp2Menu.Visible = !image;
            imageMenuItem.Visible = image;
        }

        private void importAllImagesMenuItem_Click( object sender, EventArgs e )
        {
            using ( Ionic.Utils.FolderBrowserDialogEx fbd = new Ionic.Utils.FolderBrowserDialogEx() )
            {
                fbd.ShowNewFolderButton = true;
                fbd.ShowFullPathInEditBox = true;
                fbd.ShowEditBox = true;
                fbd.ShowBothFilesAndFolders = false;
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.NewStyle = true;
                Cursor oldCursor = Cursor;

                ProgressChangedEventHandler progressHandler = delegate( object sender2, ProgressChangedEventArgs args2 )
                {
                    MethodInvoker mi = (() => progressBar1.Value = args2.ProgressPercentage);
                    if (progressBar1.InvokeRequired)
                        progressBar1.Invoke( mi );
                    else mi();
                };

                RunWorkerCompletedEventHandler completeHandler = null;

                completeHandler = delegate( object sender1, RunWorkerCompletedEventArgs args1 )
                {
                    MethodInvoker mi = delegate()
                    {
                        var result = args1.Result as AllOtherImages.AllImagesDoWorkResult;
                        tabControl1.Enabled = true;
                        progressBar1.Visible = false;
                        if (oldCursor != null) Cursor = oldCursor;
                        backgroundWorker1.RunWorkerCompleted -= completeHandler;
                        backgroundWorker1.ProgressChanged -= progressHandler;
                        backgroundWorker1.DoWork -= allOtherImagesEditor1.AllOtherImages.LoadAllImages;
                        MessageBox.Show( this, string.Format( "{0} images imported", result.ImagesProcessed ), result.DoWorkResult.ToString(), MessageBoxButtons.OK );
                    };
                    if (InvokeRequired) Invoke( mi );
                    else mi();
                };

                if (fbd.ShowDialog( this ) == DialogResult.OK)
                {
                    progressBar1.Bounds = new Rectangle( ClientRectangle.Left + 10, (ClientRectangle.Height - progressBar1.Height) / 2, ClientRectangle.Width - 20, progressBar1.Height );
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;
                    backgroundWorker1.DoWork += allOtherImagesEditor1.AllOtherImages.LoadAllImages;
                    backgroundWorker1.ProgressChanged += progressHandler;
                    backgroundWorker1.RunWorkerCompleted += completeHandler;
                    backgroundWorker1.WorkerReportsProgress = true;
                    tabControl1.Enabled = false;
                    Cursor = Cursors.WaitCursor;
                    progressBar1.BringToFront();
                    backgroundWorker1.RunWorkerAsync( new AllOtherImages.AllImagesDoWorkData( currentStream, fbd.SelectedPath ) );
                }
            }
        }

        private void dumpAllImagesMenuItem_Click( object sender, EventArgs e )
        {
            using ( Ionic.Utils.FolderBrowserDialogEx fbd = new Ionic.Utils.FolderBrowserDialogEx() )
            {
                fbd.ShowNewFolderButton = true;
                fbd.ShowFullPathInEditBox = true;
                fbd.ShowEditBox = true;
                fbd.ShowBothFilesAndFolders = false;
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.NewStyle = true;
                Cursor oldCursor = Cursor;

                ProgressChangedEventHandler progressHandler = delegate( object sender2, ProgressChangedEventArgs args2 )
                {
                    MethodInvoker mi = (() => progressBar1.Value = args2.ProgressPercentage);
                    if (progressBar1.InvokeRequired)
                        progressBar1.Invoke( mi );
                    else mi();
                };

                RunWorkerCompletedEventHandler completeHandler = null;

                completeHandler = delegate( object sender1, RunWorkerCompletedEventArgs args1 )
                {
                    MethodInvoker mi = delegate()
                    {
                        var result = args1.Result as AllOtherImages.AllImagesDoWorkResult;
                        tabControl1.Enabled = true;
                        progressBar1.Visible = false;
                        if (oldCursor != null) Cursor = oldCursor;
                        backgroundWorker1.RunWorkerCompleted -= completeHandler;
                        backgroundWorker1.ProgressChanged -= progressHandler;
                        backgroundWorker1.DoWork -= allOtherImagesEditor1.AllOtherImages.DumpAllImages;
                        MessageBox.Show(this, string.Format( "{0} images saved", result.ImagesProcessed ), result.DoWorkResult.ToString(), MessageBoxButtons.OK );
                    };
                    if (InvokeRequired) Invoke( mi );
                    else mi();
                };

                if (fbd.ShowDialog( this ) == DialogResult.OK)
                {
                    progressBar1.Bounds = new Rectangle( ClientRectangle.Left+10, (ClientRectangle.Height - progressBar1.Height) / 2, ClientRectangle.Width-20, progressBar1.Height );
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;
                    backgroundWorker1.DoWork += allOtherImagesEditor1.AllOtherImages.DumpAllImages;
                    backgroundWorker1.ProgressChanged += progressHandler;
                    backgroundWorker1.RunWorkerCompleted += completeHandler;
                    backgroundWorker1.WorkerReportsProgress = true;
                    tabControl1.Enabled = false;
                    Cursor = Cursors.WaitCursor;
                    progressBar1.BringToFront();
                    backgroundWorker1.RunWorkerAsync( new AllOtherImages.AllImagesDoWorkData( currentStream, fbd.SelectedPath ) );
                    //backgroundWorker1.RunWorkerCompleted

                    //allOtherImagesEditor1.AllOtherImages.DumpAllImages( currentStream, fbd.SelectedPath );
                }
            }
        }

        struct FrameBinRectangle
        {
            public enum ByteLayout
            {
                _16BitXYWH,
                _8BitXYWH,
                _8BitWHXY,
            }

            public PatcherLib.Iso.PsxIso.Sectors File { get; set; }
            public ByteLayout Layout { get; set; }
            public int Offset { get; set; }
            public PatcherLib.Iso.PsxIso.FrameBinType Type { get; set; }

            public Rectangle GetRectangle(IList<byte> bytes)
            {
                switch (Layout)
                {
                    case ByteLayout._16BitXYWH:
                        return new Rectangle( bytes[0], bytes[2], bytes[4], bytes[6] );
                    case ByteLayout._8BitWHXY:
                        return new Rectangle( bytes[2], bytes[3], bytes[0], bytes[1] );
                    case ByteLayout._8BitXYWH:
                        return new Rectangle( bytes[0], bytes[1], bytes[2], bytes[3] );
                    default:
                        return Rectangle.Empty;
                }
            }

            public override string ToString()
            {
                return string.Format( "{0} - {1} - 0x{2:X4}", Type, File, Offset );
            }
        };

        AbstractImage frameBin;

        Set<FrameBinRectangle> rects = new Set<FrameBinRectangle>(
            new FrameBinRectangle[] {

                new FrameBinRectangle { File  = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xc660, Type = PatcherLib.Iso.PsxIso.FrameBinType.Brave },
                new FrameBinRectangle { File  = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xb3c4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Brave },
                new FrameBinRectangle { File  = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x6620, Type = PatcherLib.Iso.PsxIso.FrameBinType.Brave },
                new FrameBinRectangle { File  = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xf7c8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Brave },
                new FrameBinRectangle { File  = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xfc40, Type = PatcherLib.Iso.PsxIso.FrameBinType.Brave },
                new FrameBinRectangle { File  = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x75d30, Type = PatcherLib.Iso.PsxIso.FrameBinType.Brave },
                new FrameBinRectangle { File  = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xaae3c, Type = PatcherLib.Iso.PsxIso.FrameBinType.Brave },

                new FrameBinRectangle { File  = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAC798, Type = PatcherLib.Iso.PsxIso.FrameBinType.day },
                new FrameBinRectangle { File  = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAC7C0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Orb },
                new FrameBinRectangle { File  = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB42D4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Time },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x10143c, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x101448, Type = PatcherLib.Iso.PsxIso.FrameBinType.Exp },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x10146C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x101478, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x101484, Type = PatcherLib.Iso.PsxIso.FrameBinType.SidewaysArrowThing },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x1014A8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x1014B4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x1014C0, Type = PatcherLib.Iso.PsxIso.FrameBinType.SidewaysArrowThing },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x1014E4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x101508, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility },



            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x10152C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x101538, Type = PatcherLib.Iso.PsxIso.FrameBinType.Exp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x10143C + 0x758C8 + 0x10143c, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x101554, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x101560, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x10156C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x101578, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x101584, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x101590, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x10159C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x1015A8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x1015B4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x1015C0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x1015CC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x1015D8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101554 + 0x759E0 + 0x1015E4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101624 + 0x75AB0 + 0x101624, Type = PatcherLib.Iso.PsxIso.FrameBinType.Enemy },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101624 + 0x75AB0 + 0x101630, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101624 + 0x75AB0 + 0x10163C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Auto },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x1018A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Brave },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x1018B0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Faith },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x1018C8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Orb },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x1018FC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Move },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x101908, Type = PatcherLib.Iso.PsxIso.FrameBinType.Jump },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x101914, Type = PatcherLib.Iso.PsxIso.FrameBinType.Speed },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x101920, Type = PatcherLib.Iso.PsxIso.FrameBinType.WeapPower },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x10192C, Type = PatcherLib.Iso.PsxIso.FrameBinType.R },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x101938, Type = PatcherLib.Iso.PsxIso.FrameBinType.L },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x101944, Type = PatcherLib.Iso.PsxIso.FrameBinType.AT },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x101950, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x10195C, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x101968, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x101974, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaff },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x101980, Type = PatcherLib.Iso.PsxIso.FrameBinType.CDash },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x10198C, Type = PatcherLib.Iso.PsxIso.FrameBinType.SDash },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x101998, Type = PatcherLib.Iso.PsxIso.FrameBinType.ADash },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x1018A4 + 0x75D30 + 0x1019C8, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaffOutline },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101448, Type = PatcherLib.Iso.PsxIso.FrameBinType.Exp },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10146C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101478, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101484, Type = PatcherLib.Iso.PsxIso.FrameBinType.SidewaysArrowThing },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1014A8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1014B4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1014C0, Type = PatcherLib.Iso.PsxIso.FrameBinType.SidewaysArrowThing },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1014E4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101508, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility },



            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10152C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101538, Type = PatcherLib.Iso.PsxIso.FrameBinType.Exp },





            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101554, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101560, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10156C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101578, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101584, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101590, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10159C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1015A8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1015B4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1015C0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1015CC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1015D8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1015E4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101624, Type = PatcherLib.Iso.PsxIso.FrameBinType.Enemy },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101630, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10163C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Auto },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1018A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Brave },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1018B0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Faith },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1018C8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Orb },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1018FC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Move },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101908, Type = PatcherLib.Iso.PsxIso.FrameBinType.Jump },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101914, Type = PatcherLib.Iso.PsxIso.FrameBinType.Speed },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101920, Type = PatcherLib.Iso.PsxIso.FrameBinType.WeapPower },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10192C, Type = PatcherLib.Iso.PsxIso.FrameBinType.R },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101938, Type = PatcherLib.Iso.PsxIso.FrameBinType.L },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101944, Type = PatcherLib.Iso.PsxIso.FrameBinType.AT },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101950, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10195C, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101968, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101974, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaff },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101980, Type = PatcherLib.Iso.PsxIso.FrameBinType.CDash },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10198C, Type = PatcherLib.Iso.PsxIso.FrameBinType.SDash },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101998, Type = PatcherLib.Iso.PsxIso.FrameBinType.ADash },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x1019C8, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaffOutline },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101AA8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101AB4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101AC0, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101ACC, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101AD8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101AE4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101AF0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101AFC, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101B08, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101B14, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101B20, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101B2C, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101B50, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101B5C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101B68, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101B74, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101B80, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101B8C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101B98, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101BA4, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101BB0, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101BBC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101BC8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101BD4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101BE0, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101BEC, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101BF8, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101C04, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101C10, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101C34, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101C40, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101C4C, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101C58, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101C64, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101C70, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101C7C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101C88, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101C94, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101CA0, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101CAC, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101CB8, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101CC4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101AA8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101AB4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101AC0, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101ACC, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101AD8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101AE4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101AF0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101AFC, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101B08, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101B14, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101B20, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101B2C, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101B50, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101B5C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101B68, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101B74, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101B80, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101B8C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101B98, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101BA4, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101BB0, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101BBC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101BC8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101BD4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101BE0, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101BEC, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101BF8, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101C04, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101C10, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101C34, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101C40, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101C4C, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101C58, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101C64, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101C70, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101C7C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101C88, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101C94, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101CA0, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101CAC, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101CB8, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101AA8 + 0x75F34 + 0x101CC4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101D94, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101DA0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Turn },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101DAC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101DB8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ref },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101DC4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101DD0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101DDC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Turn },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101DE8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ref },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101DF4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E00, Type = PatcherLib.Iso.PsxIso.FrameBinType.Stock },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E0C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Turn },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E18, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ref },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E24, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E30, Type = PatcherLib.Iso.PsxIso.FrameBinType.Turn },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E3C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E48, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ref },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E54, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E60, Type = PatcherLib.Iso.PsxIso.FrameBinType.Turn },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E6C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E78, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ref },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E90, Type = PatcherLib.Iso.PsxIso.FrameBinType.Menu },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101E9C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Check },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101EA8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Effect },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = -0x101D94 + 0x76224 + 0x101EB4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Turn },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x77008, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x77014, Type = PatcherLib.Iso.PsxIso.FrameBinType.Max },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x77020, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x7702C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Max },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x77038, Type = PatcherLib.Iso.PsxIso.FrameBinType.Speed },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x77044, Type = PatcherLib.Iso.PsxIso.FrameBinType.R },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x77050, Type = PatcherLib.Iso.PsxIso.FrameBinType.L },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101D94, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101DA0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Turn },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101DAC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101DB8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ref },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101DC4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101DD0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101DDC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Turn },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101DE8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ref },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101DF4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E00, Type = PatcherLib.Iso.PsxIso.FrameBinType.Stock },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E0C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Turn },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E18, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ref },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E24, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E30, Type = PatcherLib.Iso.PsxIso.FrameBinType.Turn },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E3C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E48, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ref },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E54, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E60, Type = PatcherLib.Iso.PsxIso.FrameBinType.Turn },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E6C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E78, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ref },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E90, Type = PatcherLib.Iso.PsxIso.FrameBinType.Menu },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101E9C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Check },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101EA8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Effect },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x101EB4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Turn },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F68, Type = PatcherLib.Iso.PsxIso.FrameBinType.Dead },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F6C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Undead },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F70, Type = PatcherLib.Iso.PsxIso.FrameBinType.Petrify },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F74, Type = PatcherLib.Iso.PsxIso.FrameBinType.Invite },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F78, Type = PatcherLib.Iso.PsxIso.FrameBinType.Darkness },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F7C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Confusion },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Silence },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F84, Type = PatcherLib.Iso.PsxIso.FrameBinType.BloodSuck },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F88, Type = PatcherLib.Iso.PsxIso.FrameBinType.Oil },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F8C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Float },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F90, Type = PatcherLib.Iso.PsxIso.FrameBinType.Reraise },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F94, Type = PatcherLib.Iso.PsxIso.FrameBinType.Transparent },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F98, Type = PatcherLib.Iso.PsxIso.FrameBinType.Berserk },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5F9C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Poison },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FA0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Regen },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FA4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Protect },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FA8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Shell },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FAC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Haste },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FB0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Slow },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FB4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Stop },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FB8, Type = PatcherLib.Iso.PsxIso.FrameBinType.FaithStatus },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FBC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Innocent },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FC0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Charm },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FC4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Sleep },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FC8, Type = PatcherLib.Iso.PsxIso.FrameBinType.DontMove },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FCC, Type = PatcherLib.Iso.PsxIso.FrameBinType.DontAct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FD0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Reflect },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FD4, Type = PatcherLib.Iso.PsxIso.FrameBinType.DeathSentence },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FD8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Stolen },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FDC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Broken },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE5FFC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE6000, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE6004, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE6008, Type = PatcherLib.Iso.PsxIso.FrameBinType.Speed },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE600C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Brave },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE6010, Type = PatcherLib.Iso.PsxIso.FrameBinType.Faith },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE6014, Type = PatcherLib.Iso.PsxIso.FrameBinType.Sword },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE6018, Type = PatcherLib.Iso.PsxIso.FrameBinType.Staff },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE601C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE6020, Type = PatcherLib.Iso.PsxIso.FrameBinType.GIL },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE6024, Type = PatcherLib.Iso.PsxIso.FrameBinType.Exp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN, Layout = FrameBinRectangle.ByteLayout._8BitXYWH, Offset = 0xE6028, Type = PatcherLib.Iso.PsxIso.FrameBinType.Frog },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACF78, Type = PatcherLib.Iso.PsxIso.FrameBinType.Dead },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACF80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Undead },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACFA8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Petrify },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACFB0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Invite },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACFB8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Darkness },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACFC0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Confusion },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACFC8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Silence },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACFD0, Type = PatcherLib.Iso.PsxIso.FrameBinType.BloodSuck },
            //new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACFD8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Dead },
            //new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACFE0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Petrify },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACFE8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Oil },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACFF0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Float },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xACFF8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Reraise },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD000, Type = PatcherLib.Iso.PsxIso.FrameBinType.Transparent },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD008, Type = PatcherLib.Iso.PsxIso.FrameBinType.Berserk },
            //new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD010, Type = PatcherLib.Iso.PsxIso.FrameBinType.Petrify },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD018, Type = PatcherLib.Iso.PsxIso.FrameBinType.Frog },
            //new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD020, Type = PatcherLib.Iso.PsxIso.FrameBinType.Petrify },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD028, Type = PatcherLib.Iso.PsxIso.FrameBinType.Poison },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD030, Type = PatcherLib.Iso.PsxIso.FrameBinType.Regen },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD038, Type = PatcherLib.Iso.PsxIso.FrameBinType.Protect },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD040, Type = PatcherLib.Iso.PsxIso.FrameBinType.Shell },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD048, Type = PatcherLib.Iso.PsxIso.FrameBinType.Haste },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD050, Type = PatcherLib.Iso.PsxIso.FrameBinType.Slow },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD058, Type = PatcherLib.Iso.PsxIso.FrameBinType.Stop },
            //new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD060, Type = PatcherLib.Iso.PsxIso.FrameBinType.Petrify },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD068, Type = PatcherLib.Iso.PsxIso.FrameBinType.FaithStatus },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD070, Type = PatcherLib.Iso.PsxIso.FrameBinType.Innocent },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD078, Type = PatcherLib.Iso.PsxIso.FrameBinType.Charm },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD080, Type = PatcherLib.Iso.PsxIso.FrameBinType.Sleep },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD088, Type = PatcherLib.Iso.PsxIso.FrameBinType.DontMove },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD090, Type = PatcherLib.Iso.PsxIso.FrameBinType.DontAct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD098, Type = PatcherLib.Iso.PsxIso.FrameBinType.Reflect },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xAD0A0, Type = PatcherLib.Iso.PsxIso.FrameBinType.DeathSentence },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB308, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB314, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB320, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB32C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB338, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB344, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB350, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB374, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB380, Type = PatcherLib.Iso.PsxIso.FrameBinType.Exp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB44C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Enemy },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB458, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB464, Type = PatcherLib.Iso.PsxIso.FrameBinType.Auto },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB4F4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB500, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB50C, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB518, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB524, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB530, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB53C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB548, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB554, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB560, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB56C, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB578, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB59C, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5A8, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5B4, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5C0, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5CC, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5D8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5E4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5F0, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5FC, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB608, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB614, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB620, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB62C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB638, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB644, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB650, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB65C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB680, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB68C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB698, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6B0, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6BC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6C8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6D4, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6E0, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6EC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6F8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB704, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB710, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB71C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB728, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB734, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB740, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB764, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB770, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB77C, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB788, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB794, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7A0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7AC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7B8, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7C4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7D0, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7DC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7E8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7F4, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB800, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB80C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB818, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB824, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB848, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB854, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB860, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB86C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB878, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB884, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB890, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB89C, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8A8, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8B4, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8C0, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8CC, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8D8, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB960, Type = PatcherLib.Iso.PsxIso.FrameBinType.Move},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB96C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Jump},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB978, Type = PatcherLib.Iso.PsxIso.FrameBinType.Speed},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB984, Type = PatcherLib.Iso.PsxIso.FrameBinType.WeapPower},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB990, Type = PatcherLib.Iso.PsxIso.FrameBinType.R},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB99C, Type = PatcherLib.Iso.PsxIso.FrameBinType.L},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9A8, Type = PatcherLib.Iso.PsxIso.FrameBinType.AT},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9B4, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9C0, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9CC, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9D8, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaff},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9E4, Type = PatcherLib.Iso.PsxIso.FrameBinType.CDash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9F0, Type = PatcherLib.Iso.PsxIso.FrameBinType.SDash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9FC, Type = PatcherLib.Iso.PsxIso.FrameBinType.ADash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xBA2C, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaffOutline },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB308 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB314 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB320 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB32C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB338 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB344 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB350 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB374 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB380 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Exp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB44C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Enemy },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB458 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB464 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Auto },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB4F4 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB500 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB50C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB518 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB524 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB530 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB53C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB548 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB554 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB560 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB56C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB578 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB59C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5A8 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5B4 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5C0 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5CC - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5D8 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5E4 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5F0 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5FC - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB608 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB614 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB620 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB62C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB638 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB644 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB650 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB65C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB680 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB68C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB698 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6A4 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6B0 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6BC - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6C8 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6D4 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6E0 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6EC - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6F8 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB704 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB710 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB71C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB728 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB734 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB740 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB764 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB770 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB77C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB788 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB794 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7A0 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7AC - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7B8 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7C4 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7D0 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7DC - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7E8 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7F4 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB800 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB80C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB818 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB824 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB848 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB854 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB860 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB86C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB878 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB884 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB890 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB89C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8A8 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8B4 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8C0 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8CC - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8D8 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB960 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Move},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB96C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Jump},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB978 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Speed},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB984 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.WeapPower},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB990 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.R},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB99C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.L},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9A8 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.AT},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9B4 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9C0 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9CC - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9D8 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaff},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9E4 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.CDash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9F0 - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.SDash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9FC - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.ADash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xBA2C - 0xB308 + 0xF70C, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaffOutline },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB308 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB314 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB320 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB32C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB338 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB344 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB350 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB374 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB380 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Exp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB44C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Enemy },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB458 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB464 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Auto },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB4F4 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB500 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB50C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB518 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB524 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB530 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB53C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB548 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB554 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB560 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB56C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB578 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB59C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5A8 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5B4 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5C0 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5CC - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5D8 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5E4 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5F0 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5FC - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB608 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB614 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB620 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB62C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB638 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB644 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB650 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB65C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB680 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB68C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB698 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6A4 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6B0 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6BC - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6C8 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6D4 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6E0 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6EC - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6F8 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB704 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB710 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB71C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB728 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB734 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB740 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB764 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB770 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB77C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB788 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB794 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7A0 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7AC - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7B8 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7C4 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7D0 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7DC - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7E8 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7F4 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB800 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB80C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB818 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB824 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB848 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB854 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB860 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB86C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB878 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB884 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB890 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB89C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8A8 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8B4 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8C0 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8CC - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8D8 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB960 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Move},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB96C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Jump},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB978 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Speed},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB984 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.WeapPower},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB990 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.R},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB99C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.L},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9A8 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.AT},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9B4 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9C0 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9CC - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9D8 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaff},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9E4 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.CDash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9F0 - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.SDash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9FC - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.ADash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xBA2C - 0xB308 + 0xC5A4, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaffOutline },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB308 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB314 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB320 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB32C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB338 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB344 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB350 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB374 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB380 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Exp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB44C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Enemy },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB458 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB464 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Auto },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB4F4 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB500 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB50C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB518 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB524 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB530 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB53C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB548 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB554 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB560 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB56C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB578 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB59C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5A8 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5B4 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5C0 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5CC - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5D8 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5E4 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5F0 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5FC - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB608 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB614 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB620 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB62C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB638 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB644 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB650 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB65C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB680 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB68C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB698 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6A4 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6B0 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6BC - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6C8 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6D4 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6E0 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6EC - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6F8 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB704 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB710 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB71C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB728 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB734 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB740 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB764 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB770 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB77C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB788 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB794 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7A0 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7AC - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7B8 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7C4 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7D0 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7DC - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7E8 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7F4 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB800 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB80C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB818 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB824 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB848 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB854 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB860 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB86C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB878 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB884 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB890 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB89C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8A8 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8B4 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8C0 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8CC - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8D8 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB960 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Move},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB96C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Jump},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB978 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.Speed},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB984 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.WeapPower},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB990 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.R},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB99C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.L},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9A8 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.AT},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9B4 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9C0 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9CC - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9D8 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaff},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9E4 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.CDash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9F0 - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.SDash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9FC - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.ADash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xBA2C - 0xB308 + 0x6564, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaffOutline },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x109FC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Dead },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A04, Type = PatcherLib.Iso.PsxIso.FrameBinType.Undead },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A2C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Petrify },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A34, Type = PatcherLib.Iso.PsxIso.FrameBinType.Invite },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A3C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Darkness},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A44, Type = PatcherLib.Iso.PsxIso.FrameBinType.Confusion},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A4C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Silence},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A54, Type = PatcherLib.Iso.PsxIso.FrameBinType.BloodSuck },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A6C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Oil },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A74, Type = PatcherLib.Iso.PsxIso.FrameBinType.Float},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A7C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Reraise },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Transparent },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A8C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Berserk },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10A9C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Frog },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10AaC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Poison },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10Ab4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Regen},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10AbC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Protect},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10Ac4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Shell },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10AcC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Haste},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10Ad4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Slow},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10AdC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Stop},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10AeC, Type = PatcherLib.Iso.PsxIso.FrameBinType.FaithStatus },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10Af4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Innocent },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10AfC, Type = PatcherLib.Iso.PsxIso.FrameBinType.Charm},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10b04, Type = PatcherLib.Iso.PsxIso.FrameBinType.Sleep},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10b0C, Type = PatcherLib.Iso.PsxIso.FrameBinType.DontMove },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10b14, Type = PatcherLib.Iso.PsxIso.FrameBinType.DontAct},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10b1C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Reflect },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0x10b24, Type = PatcherLib.Iso.PsxIso.FrameBinType.DeathSentence },


            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB308 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB314 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB320 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB32C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB338 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB344 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB350 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB374 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB380 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Exp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB44C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Enemy },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB458 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB464 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Auto },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB4F4 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB500 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB50C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB518 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB524 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB530 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB53C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB548 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB554 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB560 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB56C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB578 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB59C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5A8 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5B4 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5C0 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5CC - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5D8 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5E4 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5F0 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5FC - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB608 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB614 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB620 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB62C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB638 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB644 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB650 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB65C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB680 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB68C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB698 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6A4 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6B0 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6BC - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6C8 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6D4 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6E0 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6EC - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6F8 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB704 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB710 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB71C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB728 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB734 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB740 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB764 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB770 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB77C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB788 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB794 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7A0 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7AC - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7B8 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7C4 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7D0 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7DC - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7E8 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7F4 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB800 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB80C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB818 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB824 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB848 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB854 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB860 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB86C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB878 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB884 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB890 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB89C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8A8 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8B4 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8C0 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8CC - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8D8 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB960 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Move},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB96C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Jump},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB978 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.Speed},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB984 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.WeapPower},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB990 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.R},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB99C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.L},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9A8 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.AT},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9B4 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9C0 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9CC - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9D8 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaff},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9E4 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.CDash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9F0 - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.SDash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9FC - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.ADash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xBA2C - 0xB308 + 0xFB84, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaffOutline },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB308 - 0xB308 + 0xAAD80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB314 - 0xB308 + 0xAAD80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB320 - 0xB308 + 0xAAD80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Bar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB32C - 0xB308 + 0xAAD80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB338 - 0xB308 + 0xAAD80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB344 - 0xB308 + 0xAAD80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB350 - 0xB308 + 0xAAD80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB374 - 0xB308 + 0xAAD80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB380 - 0xB308 + 0xAAD80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Exp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB44C - 0xB308 + 0xAAD80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Enemy },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB458 - 0xB308 + 0xAAD80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Guest },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB464 - 0xB308 + 0xAAD80, Type = PatcherLib.Iso.PsxIso.FrameBinType.Auto },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB4F4 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB500 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB50C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB518 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB524 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB530 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB53C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB548 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB554 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB560 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB56C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB578 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB59C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5A8 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5B4 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5C0 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5CC - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5D8 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5E4 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5F0 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB5FC - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB608 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB614 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB620 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB62C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB638 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB644 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB650 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB65C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB680 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB68C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB698 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6A4 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6B0 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6BC - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6C8 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6D4 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6E0 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6EC - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB6F8 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB704 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB710 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB71C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.ActionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB728 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.ReactionAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB734 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.SupportAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB740 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.MovementAbility},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB764 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB770 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB77C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB788 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB794 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7A0 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Eqp},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7AC - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ability},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7B8 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7C4 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7D0 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Helmet},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7DC - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Armor},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7E8 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Accessory},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB7F4 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB800 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB80C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB818 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB824 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.DoubleStar},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB848 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB854 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB860 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.HelmetOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB86C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.ArmorOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB878 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.AccessoryOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB884 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB890 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB89C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.RightHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8A8 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8B4 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8C0 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHand},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8CC - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.TwoHanded},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB8D8 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.LeftHandOutline},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB960 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Move},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB96C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Jump},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB978 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.Speed},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB984 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.WeapPower},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB990 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.R},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB99C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.L},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9A8 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.AT},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9B4 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9C0 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9CC - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.EV},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9D8 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaff},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9E4 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.CDash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9F0 - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.SDash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xB9FC - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.ADash},
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._16BitXYWH, Offset = 0xBA2C - 0xB308 + 0xAAD80+3*12, Type = PatcherLib.Iso.PsxIso.FrameBinType.SwordAndStaffOutline },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC7E2, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC7E8, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC7EE, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC7F4, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC7FA, Type = PatcherLib.Iso.PsxIso.FrameBinType.ExpNoDot },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC800, Type = PatcherLib.Iso.PsxIso.FrameBinType.Br },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC806, Type = PatcherLib.Iso.PsxIso.FrameBinType.Dot },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC80C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Fa },

            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC812, Type = PatcherLib.Iso.PsxIso.FrameBinType.Hp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC818, Type = PatcherLib.Iso.PsxIso.FrameBinType.Mp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC81E, Type = PatcherLib.Iso.PsxIso.FrameBinType.Ct },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC824, Type = PatcherLib.Iso.PsxIso.FrameBinType.Lv },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC82A, Type = PatcherLib.Iso.PsxIso.FrameBinType.Exp },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC830, Type = PatcherLib.Iso.PsxIso.FrameBinType.Br },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC836, Type = PatcherLib.Iso.PsxIso.FrameBinType.Dot },
            new FrameBinRectangle { File = PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN, Layout = FrameBinRectangle.ByteLayout._8BitWHXY, Offset = 0xAC83C, Type = PatcherLib.Iso.PsxIso.FrameBinType.Fa },


        }, ( a, b ) => (a.Layout == b.Layout && a.File == b.File && a.Offset == b.Offset && a.Type == b.Type) ? 0 : -1 );

        private void UpdateWithFrameBinrectangle( FrameBinRectangle rect )
        {
            ignoreChanges = true;
            comboBox1.SelectedIndex = comboBoxFiles.IndexOf( rect.File );
            _16BitRadioButton.Checked = rect.Layout == FrameBinRectangle.ByteLayout._16BitXYWH;
            _8bitRadioButton.Checked = rect.Layout == FrameBinRectangle.ByteLayout._8BitXYWH;
            _8BitAltRadioButton.Checked = rect.Layout == FrameBinRectangle.ByteLayout._8BitWHXY;
            ignoreChanges = false;
            numericUpDown1.Value = rect.Offset;
        }

        private void UpdateImages( Rectangle rect )
        {
            pictureBox1.BackColor = Color.Black;
            pictureBox2.BackColor = Color.Black;
            if ( pictureBox1.Image != null )
            {
                Image oldImage = pictureBox1.Image;
                pictureBox1.Image = null;
                oldImage.Dispose();
                oldImage = null;
            }
            if ( pictureBox2.Image != null )
            {
                Image oldImage = pictureBox2.Image;
                pictureBox2.Image = null;
                oldImage.Dispose();
                oldImage = null;
            }

            if ( frameBin != null && ( rect.X + rect.Width ) <= 256 && rect.Width > 0 &&
                ( rect.Y + rect.Height ) <= 256 && rect.Height > 0 )
            {
                Bitmap img = frameBin.GetImageFromIso( currentStream );
                Bitmap newImage = new Bitmap( img );
                pictureBox1.Image = newImage;

                Bitmap cropIMage = CropImage( newImage, rect );
                pictureBox2.Image = cropIMage;

                using ( Graphics g = Graphics.FromImage( newImage ) )
                {
                    g.DrawRectangle( Pens.Red, rect );
                }
            }
        }
        bool ignoreChanges = false;
        private void spinner_ValueChanged( object sender, EventArgs e )
        {
            if ( !ignoreChanges )
            {
                listBox1.SelectedIndex = -1;
                UpdateImages( new Rectangle( (int)xSpinner.Value, (int)ySpinner.Value, (int)widthSpinner.Value, (int)heightSpinner.Value ) );
            }
        }

        private static Bitmap CropImage( Bitmap bmp, Rectangle rect )
        {
            Bitmap result = new Bitmap( rect.Width, rect.Height );
            using(Graphics resultG = Graphics.FromImage(result))
            {
                resultG.DrawImage( bmp, new Rectangle( 0, 0, rect.Width, rect.Height ), rect, GraphicsUnit.Pixel );
            }
            return result;
        }

        private void heightSpinner_Enter( object sender, EventArgs e )
        {
            NumericUpDown nud = sender as NumericUpDown;
            nud.Select( 0, Text.Length );
        }

        private void numericUpDown1_ValueChanged( object sender, EventArgs e )
        {
            bool oldIgnoreChanges = ignoreChanges;
            if ( !oldIgnoreChanges )
            {
                listBox1.SelectedIndex = -1;
                ignoreChanges = true;
                if ( selectedFile.HasValue && currentStream != null )
                {
                    PatcherLib.Iso.PsxIso.Sectors file = selectedFile.Value;
                    byte[] bytes = PatcherLib.Iso.PsxIso.ReadFile( currentStream, file,
                        (int)numericUpDown1.Value, 
                            layout == FrameBinRectangle.ByteLayout._16BitXYWH ? 8 : 
                            layout == FrameBinRectangle.ByteLayout._8BitXYWH  ? 4 :
                            layout == FrameBinRectangle.ByteLayout._8BitWHXY  ? 4 : 0 );
                    Rectangle r = Rectangle.Empty;
                    switch (layout)
                    {
                        case FrameBinRectangle.ByteLayout._8BitXYWH:
                            r = new Rectangle(
                                bytes[0], bytes[1],
                                bytes[2], bytes[3] );
                            break;
                        case FrameBinRectangle.ByteLayout._8BitWHXY:
                            r = new Rectangle(
                                bytes[2], bytes[3],
                                bytes[0], bytes[1] );
                            break;
                        case FrameBinRectangle.ByteLayout._16BitXYWH:
                            r = new Rectangle(
                                bytes[0], bytes[2],
                                bytes[4], bytes[6] );
                            break;
                    }

                    heightSpinner.Value = r.Height;
                    widthSpinner.Value = r.Width;
                    xSpinner.Value = r.X;
                    ySpinner.Value = r.Y;

                    UpdateImages( r );
                }

                ignoreChanges = oldIgnoreChanges;
                UpdateTextBox();
            }
        }

        private void plusOneButton_Click( object sender, EventArgs e )
        {
            if ( ShiftPressed() )
                numericUpDown1.Value -= 1;
            else
                numericUpDown1.Value += 1;
        }

        private void plus8Button_Click( object sender, EventArgs e )
        {
            if ( ShiftPressed() )
                numericUpDown1.Value -= 8;
            else
                numericUpDown1.Value += 8;
        }

        private void plus16Button_Click( object sender, EventArgs e )
        {
            if ( ShiftPressed() )
                numericUpDown1.Value -= 16;
            else
                numericUpDown1.Value += 16;
        }

        FrameBinRectangle.ByteLayout layout = FrameBinRectangle.ByteLayout._16BitXYWH;

        private void _16BitRadioButton_CheckedChanged( object sender, EventArgs e )
        {
            if (_16BitRadioButton.Checked)
            {
                layout = FrameBinRectangle.ByteLayout._16BitXYWH;
                numericUpDown1_ValueChanged( sender, e );
            }
        }

        private void _8bitRadioButton_CheckedChanged( object sender, EventArgs e )
        {
            if (_8bitRadioButton.Checked)
            {
                layout = FrameBinRectangle.ByteLayout._8BitXYWH;
                numericUpDown1_ValueChanged( sender, e );
            }
        }

        IList<PatcherLib.Iso.PsxIso.Sectors> comboBoxFiles =
            new PatcherLib.Iso.PsxIso.Sectors[] {
                PatcherLib.Iso.PsxIso.Sectors.BATTLE_BIN,
                PatcherLib.Iso.PsxIso.Sectors.WORLD_WORLD_BIN,
                PatcherLib.Iso.PsxIso.Sectors.EVENT_ATTACK_OUT,
                PatcherLib.Iso.PsxIso.Sectors.EVENT_EQUIP_OUT,
                PatcherLib.Iso.PsxIso.Sectors.EVENT_REQUIRE_OUT,
                PatcherLib.Iso.PsxIso.Sectors.EVENT_DEBUGCHR_OUT,
                PatcherLib.Iso.PsxIso.Sectors.EVENT_BUNIT_OUT,
            };
        private PatcherLib.Iso.PsxIso.Sectors? selectedFile;
        private void comboBox1_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( comboBox1.SelectedIndex >= 0 && comboBox1.SelectedIndex < comboBoxFiles.Count )
            {
                selectedFile = comboBoxFiles[comboBox1.SelectedIndex];
            }
            numericUpDown1_ValueChanged( sender, e );
        }

        private void plus2Button_Click( object sender, EventArgs e )
        {
            if ( ShiftPressed() )
                numericUpDown1.Value -= 2;
            else
                numericUpDown1.Value += 2;
        }

        private void plus4Button_Click( object sender, EventArgs e )
        {
            if ( ShiftPressed() )
                numericUpDown1.Value -= 4;
            else
                numericUpDown1.Value += 4;
        }

        private bool ShiftPressed()
        {
            Keys key = Form.ModifierKeys;
            return ( key & Keys.Shift ) == Keys.Shift || 
                ( key & Keys.ShiftKey ) == Keys.ShiftKey ||
                ( key & Keys.LShiftKey ) == Keys.LShiftKey ||
                ( key & Keys.RShiftKey ) == Keys.RShiftKey;
        }
        private void plus12Button_Click( object sender, EventArgs e )
        {
            if ( ShiftPressed() )
            {
                numericUpDown1.Value -= 12;
            }
            else
            {
                numericUpDown1.Value += 12;
            }
            UpdateTextBox();
        }

        private void listBox1_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( listBox1.SelectedIndex != -1 )
            {
                UpdateWithFrameBinrectangle( rects[listBox1.SelectedIndex] );
                UpdateTextBox();
            }
        }

        private void UpdateTextBox()
        {
            if ( selectedFile.HasValue && currentStream != null )
            {
                int splitAmount = (int)numericUpDown2.Value;
                byte[] bytes = PatcherLib.Iso.PsxIso.ReadFile( currentStream, selectedFile.Value, (int)numericUpDown1.Value, 256 );
                int numGroups = ( 256 + splitAmount - 1 ) / splitAmount;
                IList<IList<byte>> lines = new IList<byte>[numGroups];
                for ( int i = 0; i < numGroups; i++ )
                {
                    lines[i] = bytes.Sub( i * splitAmount, Math.Min( i * splitAmount + splitAmount - 1, bytes.Length - 1 ) );
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach ( IList<byte> line in lines )
                {
                    foreach ( byte b in line )
                    {
                        sb.AppendFormat( "{0:X2} ", b );
                    }
                    sb.AppendLine();
                }

                textBox1.Text = sb.ToString();
            }
        }

        private void numericUpDown2_ValueChanged( object sender, EventArgs e )
        {
            UpdateTextBox();

        }

        private void _8BitAltRadioButton_CheckedChanged( object sender, EventArgs e )
        {
            if (_8BitAltRadioButton.Checked)
            {
                layout = FrameBinRectangle.ByteLayout._8BitWHXY;
                numericUpDown1_ValueChanged( sender, e );
            }
        }
        private void panel1_DragEnter( object sender, DragEventArgs e )
        {
            if (e.Data.GetDataPresent( DataFormats.FileDrop ))
            {
                string[] files = (string[])e.Data.GetData( DataFormats.FileDrop );
                if (files.Length == 1 && System.IO.File.Exists( files[0] ))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void panel1_DragDrop( object sender, DragEventArgs e )
        {
            if (e.Data.GetDataPresent( DataFormats.FileDrop ))
            {
                string[] paths = (string[])e.Data.GetData( DataFormats.FileDrop );
                if (paths.Length == 1 && System.IO.File.Exists( paths[0] ))
                {
                    Dictionary<PatcherLib.Iso.PsxIso.FrameBinType, Rectangle> dict2 = new Dictionary<PatcherLib.Iso.PsxIso.FrameBinType, Rectangle>
                    {
                        { PatcherLib.Iso.PsxIso.FrameBinType.Poison, new Rectangle(0,82,26,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Invite, new Rectangle(27,82,30,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Darkness, new Rectangle(58,82,18,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Confusion, new Rectangle(77,82,31,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.BloodSuck, new Rectangle(108,82,31,10)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Petrify, new Rectangle(140,85,23,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Shell, new Rectangle(0,90,18,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Haste, new Rectangle(18,90,22,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Slow, new Rectangle(40,90,19,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.FaithStatus, new Rectangle(60,90,19,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Innocent, new Rectangle(79,90,28,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Berserk, new Rectangle(107,92,30,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Reflect, new Rectangle(137,93,28,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Stop, new Rectangle(165,93,19,10)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Reraise, new Rectangle(0,98,28,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Charm, new Rectangle(28,98,24,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.DontMove, new Rectangle(52,98,40,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Transparent, new Rectangle(92,100,31,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.DeathSentence, new Rectangle(157,101,22,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Dead, new Rectangle(204,111,11,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Sleep, new Rectangle(181,110,20,10)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Stolen, new Rectangle(0,109,25,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Broken, new Rectangle(25,106,18,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Oil, new Rectangle(43,106,10,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Regen, new Rectangle(53,108,22,10)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Frog, new Rectangle(126,32,20,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Protect, new Rectangle(36,210,31,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.DontAct, new Rectangle(156,204,27,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Float, new Rectangle(65,240,21,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Undead, new Rectangle(142,110,26,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Silence, new Rectangle(75,108,26,8)},
                        { PatcherLib.Iso.PsxIso.FrameBinType.Brave, new Rectangle(124,101,32,9)},
                    };

                    var NoXPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31508, 1 );
                    var NoYPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x3151c, 1 );
                    var NoRightPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31530, 1 );
                    var NoBottomPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31544, 1 );

                    var wXPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x315f8, 1 );
                    var wYPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x3160c, 1 );
                    var wRightPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31620, 1 );
                    var wBottomPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31634, 1 );

                    var loaXPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x316e8, 1 );
                    var loaYPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x316fc, 1 );
                    var loaRightPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31710, 1 );
                    var loaBottomPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31724, 1 );

                    var dXPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x317d8, 1 );
                    var dYPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x317ec, 1 );
                    var dRightPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31800, 1 );
                    var dBottomPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31814, 1 );

                    var iXPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x318c8, 1 );
                    var iYPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x318dc, 1 );
                    var iRightPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x318f0, 1 );
                    var iBottomPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31904, 1 );

                    var nXPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x319b8, 1 );
                    var nYPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x319cc, 1 );
                    var nRightPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x319e0, 1 );
                    var nBottomPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x319f4, 1 );

                    var gXPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31aa8, 1 );
                    var gYPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31abc, 1 );
                    var gRightPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31ad0, 1 );
                    var gBottomPosition = new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.SCUS_942_21, 0x31ae4, 1 );


                    loaXPosition.PatchIso( currentStream, new byte[1] { (byte)(dict2[PatcherLib.Iso.PsxIso.FrameBinType.Float].X + 5) } );
                    loaYPosition.PatchIso( currentStream, new byte[1] { (byte)(dict2[PatcherLib.Iso.PsxIso.FrameBinType.Float].Y + 0) } );
                    loaRightPosition.PatchIso( currentStream, new byte[1] { (byte)(dict2[PatcherLib.Iso.PsxIso.FrameBinType.Float].X + 5 + 12) } );
                    loaBottomPosition.PatchIso( currentStream, new byte[1] { (byte)(dict2[PatcherLib.Iso.PsxIso.FrameBinType.Float].Y + 8) } );

                    iXPosition.PatchIso( currentStream, new byte[1] { (byte)(dict2[PatcherLib.Iso.PsxIso.FrameBinType.Oil].X + 5) } );
                    iYPosition.PatchIso( currentStream, new byte[1] { (byte)(dict2[PatcherLib.Iso.PsxIso.FrameBinType.Oil].Y + 0) } );
                    iRightPosition.PatchIso( currentStream, new byte[1] { (byte)(dict2[PatcherLib.Iso.PsxIso.FrameBinType.Oil].X + 5+3) } );
                    iBottomPosition.PatchIso( currentStream, new byte[1] { (byte)(dict2[PatcherLib.Iso.PsxIso.FrameBinType.Oil].Y + 8) } );


                    using (Bitmap bmp = new Bitmap( paths[0] ))
                    {
                        System.Reflection.FieldInfo fi = frameBin.GetType().GetField( "palettePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance );
                        var palpos = (PatcherLib.Iso.KnownPosition)fi.GetValue( frameBin );

                        IList<byte> palBytes = palpos.ReadIso( currentStream );
                        Palette p = new Palette( palBytes );

                        Dictionary<Color, int> dict = new Dictionary<Color, int>();
                            for (int i = 0; i < p.Colors.Length; i++)
                            {
                                dict[p.Colors[i]] = i;
                            }
                            dict[Color.FromArgb( 255, 0, 0, 0 )] = 0;
                        List<byte> bytes = new List<byte>();
                        try
                        {
                            for (int y = 0; y < bmp.Height; y++)
                            {
                                for (int x = 0; x < bmp.Width; x++)
                                {
                                    Color c = bmp.GetPixel( x, y );
                                    if (dict.ContainsKey( c ))
                                    {
                                        bytes.Add( (byte)dict[c] );
                                    }
                                    else
                                    {
                                    }
                                }
                            }
                        }
                        catch (Exception exc)
                        {
                            
                        }

                        List<byte> realBytes = new List<byte>();
                        for (int i = 0; i < bytes.Count; i += 2)
                        {
                            realBytes.Add( (byte)(((bytes[i+1] & 0xF) << 4) | (bytes[i] & 0xF)) );
                        }

                        System.Reflection.FieldInfo fi2 = frameBin.GetType().GetField( "position", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic );
                        var kp = (PatcherLib.Iso.KnownPosition)fi2.GetValue( frameBin );
                        kp.PatchIso( currentStream, realBytes );

                        foreach (var kvp in dict2)
                        {
                            var rectList = new List<FrameBinRectangle>( rects );
                            var currentRect = rectList.FindAll( fbr => fbr.Type == kvp.Key );
                            Console.Out.WriteLine( "<Patch name=\"Update instructions for {0}\">", kvp.Key );

                            foreach (var r in currentRect)
                            {
                                byte[] pbyte = new byte[0];
                                switch (r.Layout)
                                {
                                    case FrameBinRectangle.ByteLayout._16BitXYWH:
                                        pbyte = new byte[8] {
                                            (byte)kvp.Value.X, 0,
                                            (byte)kvp.Value.Y, 0,
                                            (byte)kvp.Value.Width, 0,
                                            (byte)kvp.Value.Height, 0 };
                                        break;
                                    case FrameBinRectangle.ByteLayout._8BitWHXY:
                                        pbyte = new byte[4] {
                                            (byte)kvp.Value.Width,
                                            (byte)kvp.Value.Height,
                                            (byte)kvp.Value.X,
                                            (byte)kvp.Value.Y,
                                        };
                                        break;
                                    case FrameBinRectangle.ByteLayout._8BitXYWH:
                                        pbyte = new byte[4] {
                                            (byte)kvp.Value.X,
                                            (byte)kvp.Value.Y,
                                            (byte)kvp.Value.Width,
                                            (byte)kvp.Value.Height,
                                        };
                                        break;
                                }

                                PatcherLib.Iso.PsxIso.PatchPsxIso( currentStream,
                                    new PatchedByteArray( r.File, r.Offset, pbyte ) );
                                Console.Out.WriteLine( "<Location file=\"{0}\" offset=\"{1:X2}\">", r.File, r.Offset );
                                foreach (byte b in pbyte)
                                    Console.Out.Write("{0:X2}",b);
                                Console.Out.WriteLine( Environment.NewLine + "</Location>" );
                            }
                            Console.Out.WriteLine( "</Patch>" );
                        }
                        //frameBin.
                    }
                }
            }
        }

        private void paletteSpinner_ValueChanged( object sender, EventArgs e )
        {
            if (currentStream != null && currentStream.Length % PatcherLib.Iso.IsoPatch.SectorSizes[PatcherLib.Iso.IsoPatch.IsoType.Mode2Form1] == 0)
            {
                frameBin =
                    new PalettedImage4bpp( "frame.bin", 256, 256, 1, Palette.ColorDepth._16bit,
                        new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_FRAME_BIN, 0x1000, 256 * 256 / 2 ),
                        new PatcherLib.Iso.PsxIso.KnownPosition( PatcherLib.Iso.PsxIso.Sectors.EVENT_FRAME_BIN, 0x9000 + 16 * 2 * (int)paletteSpinner.Value, 16 * 2 ) );
                UpdateImages( new Rectangle( (int)xSpinner.Value, (int)ySpinner.Value, (int)widthSpinner.Value, (int)heightSpinner.Value ) );
            }
            else if (currentStream != null && currentStream.Length % PatcherLib.Iso.IsoPatch.SectorSizes[PatcherLib.Iso.IsoPatch.IsoType.Mode1] == 0)
            {
                frameBin =
                    new PalettedImage4bpp( "frame.bin", 256, 256, 1, Palette.ColorDepth._16bit,
                        new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_FRAME_BIN, 0x1000, 256 * 256 / 2 ),
                        new PatcherLib.Iso.PspIso.KnownPosition( PatcherLib.Iso.FFTPack.Files.EVENT_FRAME_BIN, 0x9000 + 16 * 2 * (int)paletteSpinner.Value, 16 * 2 ) );
                UpdateImages( new Rectangle( (int)xSpinner.Value, (int)ySpinner.Value, (int)widthSpinner.Value, (int)heightSpinner.Value ) );
            }
        }

    }
}
