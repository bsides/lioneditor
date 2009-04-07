using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FFTPatcher.SpriteEditor
{
    public partial class SpriteEditor : UserControl
    {
        public Sprite Sprite { get; private set;}

        public AbstractSprite AbstractSprite { get; private set; }

        public void BindTo(Sprite sprite, Stream iso)
        {
            Sprite = sprite;
            AbstractSprite = Sprite.GetAbstractSpriteFromPsxIso(iso);
            spriteViewer1.Sprite = AbstractSprite;
            shpComboBox.SelectedItem = sprite.SHP;
            seqComboBox.SelectedItem = sprite.SEQ;
            Enabled = true;
        }

        public SpriteEditor()
        {
            InitializeComponent();
            shpComboBox.DataSource = Enum.GetValues(typeof(SpriteAttributes.SpriteType));
            seqComboBox.DataSource = Enum.GetValues(typeof(SpriteAttributes.SpriteType));
            paletteSelector.SelectedIndex = 0;
        }

        private void PaletteChanged(object sender, EventArgs e)
        {
            spriteViewer1.SetPalette(paletteSelector.SelectedIndex, portraitCheckbox.Checked ? (paletteSelector.SelectedIndex % 8 + 8) : paletteSelector.SelectedIndex);
        }

        private void shpComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void seqComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
