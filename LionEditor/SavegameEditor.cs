using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LionEditor
{
    public partial class SavegameEditor : UserControl
    {
        Savegame game;

        public Savegame Game
        {
            get { return game; }
            set
            {
                game = value;
                UpdateView();
            }
        }

        private void UpdateView()
        {
            foreach( Character c in game.Characters )
            {
                characterSelector.Items.Add( c, c.Index != 0xFF );
            }
            foreach( Character g in game.Guests )
            {
                characterSelector.Items.Add( g, g.Index != 0xFF );
            }
        }

        public SavegameEditor()
        {
            InitializeComponent();
            if( LicenseManager.UsageMode != LicenseUsageMode.Designtime )
            {
                System.IO.FileStream stream = new System.IO.FileStream( "FFTA.SYS", System.IO.FileMode.Open );
                byte[] bytes = new byte[0x2A3C];
                stream.Read( bytes, 0, 0x2A3C );
                stream.Close();

                Game = new Savegame( bytes );
            }
            else
            {
                characterEditor1.Enabled = false;
                characterSelector.Enabled = false;
                tabControl1.Enabled = false;
            }

            characterSelector.SelectedIndexChanged += characterSelector_SelectedIndexChanged;
            characterSelector.CheckOnClick = false;
            characterSelector.ItemCheck += characterSelector_ItemCheck;
        }

        void characterSelector_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( e.NewValue != CheckState.Indeterminate)
            {
                Character c = characterSelector.Items[e.Index] as Character;
                c.Index = (e.NewValue == CheckState.Checked) ? (byte)e.Index : (byte)0xFF;
            }
        }

        void characterSelector_SelectedIndexChanged( object sender, EventArgs e )
        {
            Character c = characterSelector.SelectedItem as Character;
            characterEditor1.Character = c;
        }
    }
}
