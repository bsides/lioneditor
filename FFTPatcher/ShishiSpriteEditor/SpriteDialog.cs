using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            properCheckbox.CheckedChanged += new EventHandler( properCheckbox_CheckedChanged );
            shapesListBox.DrawItem += new DrawItemEventHandler( shapesListBox_DrawItem );
            shapesListBox.MeasureItem +=new MeasureItemEventHandler(shapesListBox_MeasureItem);
            shapesListBox.SelectedIndexChanged += new EventHandler( shapesListBox_SelectedIndexChanged );
            StartPosition = FormStartPosition.CenterParent;
        }

        private void paletteSelector_SelectedIndexChanged( object sender, EventArgs e )
        {
            spriteViewer1.SetPalette( paletteSelector.SelectedIndex, portraitCheckbox.Checked ? (paletteSelector.SelectedIndex % 8 + 8) : paletteSelector.SelectedIndex );
        }

        public void ShowDialog( AbstractSprite sprite, IWin32Window owner )
        {
            Sprite = sprite;
            spriteViewer1.Sprite = sprite;
            shapesListBox.Visible = sprite.Shape != null;
            Shape s = sprite.Shape;
            if( s != null )
            {
                IList<Frame> f = s.Frames;
                frames = new List<Bitmap>( f.Count );
                foreach( Frame frame in f )
                {
                    frames.Add( frame.GetFrame( spriteViewer1.Sprite ) );
                }

                shapesListBox.BeginUpdate();
                shapesListBox.Items.Clear();
                shapesListBox.Items.AddRange( f.ToArray() );
                shapesListBox.EndUpdate();
            }

            ShowDialog( owner );
        }

        private void portraitCheckbox_CheckedChanged( object sender, EventArgs e )
        {
            spriteViewer1.SetPalette( paletteSelector.SelectedIndex, portraitCheckbox.Checked ? (paletteSelector.SelectedIndex % 8 + 8) : paletteSelector.SelectedIndex );
        }

        private void properCheckbox_CheckedChanged( object sender, EventArgs e )
        {
            spriteViewer1.Proper = properCheckbox.Checked;
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


    }
}
