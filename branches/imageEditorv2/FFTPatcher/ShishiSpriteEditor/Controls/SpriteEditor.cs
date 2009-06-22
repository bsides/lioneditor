using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PatcherLib.Utilities;

namespace FFTPatcher.SpriteEditor
{
    public partial class SpriteEditor : UserControl
    {
        public Sprite Sprite { get; private set;}

        public AbstractSprite AbstractSprite { get; private set; }
        bool ignoreChanges = true;

        private Stream iso;
        public void BindTo(Sprite sprite, IList<int> sharedSPRs, Stream iso)
        {
            ignoreChanges = true;
            this.iso = iso;
            Sprite = sprite;
            ReloadSprite();

            if (sharedSPRs.Count > 0)
            {
                StringBuilder sb = new StringBuilder("WARNING: This sprite shares an SPR with: ");
                foreach (int i in sharedSPRs)
                {
                    sb.AppendFormat("{0:X2} ", i);
                }
                sharedLabel.Text = sb.ToString();
                sharedLabel.Visible = true;
            }
            else
            {
                sharedLabel.Visible = false;
            }

            Enabled = true;
            ignoreChanges = false;
        }

        public void ReloadSprite()
        {
            bool oldIgnoreChanges = ignoreChanges;
            ignoreChanges = true;
            AbstractSprite = Sprite.GetAbstractSpriteFromIso( iso );
            spriteViewer1.Sprite = AbstractSprite;
            shpComboBox.SelectedItem = Sprite.SHP;
            seqComboBox.SelectedItem = Sprite.SEQ;
            flyingCheckbox.Checked = Sprite.Flying;
            flagsCheckedListBox.BeginUpdate();
            flagsCheckedListBox.SetItemChecked( 0, Sprite.Flag1 );
            flagsCheckedListBox.SetItemChecked( 1, Sprite.Flag2 );
            flagsCheckedListBox.SetItemChecked( 2, Sprite.Flag3 );
            flagsCheckedListBox.SetItemChecked( 3, Sprite.Flag4 );
            flagsCheckedListBox.SetItemChecked( 4, Sprite.Flag5 );
            flagsCheckedListBox.SetItemChecked( 5, Sprite.Flag6 );
            flagsCheckedListBox.SetItemChecked( 6, Sprite.Flag7 );
            flagsCheckedListBox.SetItemChecked( 7, Sprite.Flag8 );
            flagsCheckedListBox.EndUpdate();
            UpdateShapes();
            UpdateAnimationTab();
            maxSizeLabel.Visible = true;
            maxSizeLabel.Text = string.Format("Max SPR size:" + Environment.NewLine + "{0} bytes", Sprite.Size);
            ignoreChanges = oldIgnoreChanges;
        }

        private Shape currentShape;
        private void UpdateShapes()
        {
            bool oldIgnoreChanges = ignoreChanges;
            ignoreChanges = true;
            if ( Shape.Shapes.ContainsKey( Sprite.SHP ) )
            {
                numericUpDown1.Enabled = true;
                pictureBox1.Enabled = true;

                currentShape = Shape.Shapes[Sprite.SHP];
                numericUpDown1.Value = 1;
                numericUpDown1.Maximum = currentShape.Frames.Count - 1;
            }
            else
            {
                numericUpDown1.Enabled = false;
                pictureBox1.Enabled = false;
                currentShape = null;
            }
            UpdatePictureBox();

            ignoreChanges = oldIgnoreChanges;
        }

        public SpriteEditor()
        {
            InitializeComponent();
            var s = new List<SpriteType>( (SpriteType[])Enum.GetValues( typeof( SpriteType ) ) );
            s.Remove(SpriteType.RUKA);

            shpComboBox.DataSource = s.ToArray();
            seqComboBox.DataSource = Enum.GetValues(typeof(SpriteType));
            paletteSelector.SelectedIndex = 0;
            //pictureBox1.MinimumSize = Frame.DefaultFrameSize + pictureBox1.Padding.Size;
            //animationViewer1.MinimumSize = Frame.DefaultFrameSize + animationViewer1.Padding.Size + new Size( 0, 40 );
            numericUpDown2.ValueChanged += numericUpDown2_SelectedIndexChanged;
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
        }

        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == animationTabPage && Sequence.Sequences.ContainsKey((SpriteType)seqComboBox.SelectedItem))
            {
                numericUpDown2_SelectedIndexChanged(null, EventArgs.Empty);
                animationViewer1.Play();
                spriteViewer1.HighlightTiles(new Tile[0]);
            }
            else
            {
                animationViewer1.Pause();
                UpdatePictureBox();
            }
        }

        private void PaletteChanged(object sender, EventArgs e)
        {
            if (!ignoreChanges)
                spriteViewer1.SetPalette(paletteSelector.SelectedIndex, portraitCheckbox.Checked ? (paletteSelector.SelectedIndex % 8 + 8) : paletteSelector.SelectedIndex);
        }

        private void shpComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( !ignoreChanges )
            {
                Sprite.SetSHP( iso, (SpriteType)shpComboBox.SelectedItem );
                ReloadSprite();
                UpdateAnimationTab();
            }
        }

        private void seqComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( !ignoreChanges )
            {
                Sprite.SetSEQ( iso, (SpriteType)seqComboBox.SelectedItem );
                UpdateAnimationTab();
            }
        }

        private void flyingCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!ignoreChanges)
                Sprite.SetFlying(iso, flyingCheckbox.Checked);
        }

        private void flagsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!ignoreChanges)
                Sprite.SetFlag(iso, e.Index, e.NewValue == CheckState.Checked);
        }

        private void UpdatePictureBox()
        {
            Image i = pictureBox1.Image;
            if (currentShape != null)
            {
                Frame currentFrame = currentShape.Frames[(int)numericUpDown1.Value];
                pictureBox1.Image = currentFrame.GetFrame(Sprite.GetAbstractSpriteFromIso(iso));
                if (tabControl1.SelectedTab == framesTabPage)
                {
                    spriteViewer1.HighlightTiles(currentFrame.Tiles);
                }
                else
                {
                    spriteViewer1.HighlightTiles(new Tile[0]);
                }
            }
            else
            {
                pictureBox1.Image = null;
            }

            if (i != null)
            {
                i.Dispose();
            }
        }

        void numericUpDown2_SelectedIndexChanged( object sender, EventArgs e )
        {
            Sequence seq = currentSequences[(int)numericUpDown2.Value];
            IList<Bitmap> bmps;
            IList<double> delays;
            seq.BuildAnimation( spriteViewer1.Sprite, out bmps, out delays );
            animationViewer1.ShowAnimation(bmps, delays, tabControl1.SelectedTab == animationTabPage);
        }

        private IList<Sequence> currentSequences;

        private void UpdateAnimationTab()
        {
            if ( Sequence.Sequences.ContainsKey( (SpriteType)seqComboBox.SelectedItem ) )
            {
                IList<Sequence> sequences = Sequence.Sequences[(SpriteType)seqComboBox.SelectedItem];
                currentSequences = sequences;
                numericUpDown2.Minimum = 0;
                numericUpDown2.Maximum = sequences.Count - 1;
                numericUpDown2.Value = 0;
                numericUpDown2.Enabled = true;
                animationViewer1.Enabled = true;
            }
            else
            {
                animationViewer1.Pause();
                animationViewer1.Enabled = false;
                numericUpDown2.Enabled = false;
            }
        }

        private void numericUpDown1_ValueChanged( object sender, EventArgs e )
        {
            if ( !ignoreChanges && currentShape != null )
            {
                UpdatePictureBox();
            }
        }

    }
}
