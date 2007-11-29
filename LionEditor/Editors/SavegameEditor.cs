/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of LionEditor.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

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
        bool ignoreChanges = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Savegame Game
        {
            get { return game; }
            set
            {
                game = value;
                if( value != null )
                {
                    ignoreChanges = true;

                    characterEditor.Enabled = true;
                    characterSelector.Enabled = true;
                    tabControl.Enabled = true;

                    UpdateView();

                    ignoreChanges = false;
                }
            }
        }

        private void UpdateView()
        {
            characterSelector.Items.Clear();
            foreach( Character c in game.Characters )
            {
                characterSelector.Items.Add( c, c.Index != 0xFF );
            }
            foreach( Character g in game.Guests )
            {
                characterSelector.Items.Add( g, g.Index != 0xFF );
            }
            characterSelector.SelectedIndex = 0;

            optionsEditor1.Options = game.Options;

            chronicleEditor1.Feats = game.Feats;
            chronicleEditor1.Wonders = game.Wonders;
            chronicleEditor1.Artefacts = game.Artefacts;

            chronicleEditor1.Kills = (game.Kills > 9999) ? 9999 : game.Kills;
            chronicleEditor1.Casualties = (game.Casualties > 9999) ? 9999 : game.Casualties;
            chronicleEditor1.Timer = game.Timer;
            chronicleEditor1.WarFunds = game.WarFunds;
            chronicleEditor1.Date = game.Date;

            inventoryEditor.Inventory = game.Inventory;
            poachersDenEditor.Inventory = game.PoachersDen;
        }

        public SavegameEditor()
        {
            InitializeComponent();

            characterEditor.Enabled = false;
            characterSelector.Enabled = false;
            tabControl.Enabled = false;
            
            characterSelector.SelectedIndexChanged += characterSelector_SelectedIndexChanged;
            characterSelector.CheckOnClick = false;
            characterSelector.ItemCheck += characterSelector_ItemCheck;

            characterEditor.DataChangedEvent += dataChanged;
            optionsEditor1.DataChangedEvent += dataChanged;
            chronicleEditor1.DataChangedEvent += chronicleEditor1_DataChangedEvent;
            inventoryEditor.DataChangedEvent += dataChanged;
            poachersDenEditor.DataChangedEvent += dataChanged;
        }

        void chronicleEditor1_DataChangedEvent( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                game.Kills = chronicleEditor1.Kills;
                game.Casualties = chronicleEditor1.Casualties;
                game.Timer = chronicleEditor1.Timer;
                game.Date = chronicleEditor1.Date;
                game.WarFunds = chronicleEditor1.WarFunds;
                dataChanged( sender, e );
            }
        }

        void dataChanged( object sender, EventArgs e )
        {
            FireDataChangedEvent();
        }

        void characterSelector_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( e.NewValue != CheckState.Indeterminate)
            {
                Character c = characterSelector.Items[e.Index] as Character;
                c.Index = (e.NewValue == CheckState.Checked) ? (byte)e.Index : (byte)0xFF;
                FireDataChangedEvent();
            }
        }

        void characterSelector_SelectedIndexChanged( object sender, EventArgs e )
        {
            Character c = characterSelector.SelectedItem as Character;
            characterEditor.Character = c;
        }

        public event EventHandler DataChangedEvent;

        private void FireDataChangedEvent()
        {
            if( (DataChangedEvent != null) && (!ignoreChanges) )
            {
                DataChangedEvent( this, EventArgs.Empty );
            }
        }
    }
}
