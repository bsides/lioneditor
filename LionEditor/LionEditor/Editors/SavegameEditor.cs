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
using System.ComponentModel;
using System.Windows.Forms;

namespace LionEditor
{
    public partial class SavegameEditor : UserControl
    {
        #region Fields

        private Savegame game;
        private bool ignoreChanges = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the game currently being edited
        /// </summary>
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public Savegame Game
        {
            get { return game; }
            set
            {
                game = value;
                if( value != null )
                {
                    ignoreChanges = true;

                    characterCollectionEditor.Enabled = true;

                    tabControl.Enabled = true;

                    UpdateView();

                    ignoreChanges = false;
                }
            }
        }

        #endregion

        #region Events

        private void chronicleEditor_DataChangedEvent(object sender, EventArgs e)
        {
            if( !ignoreChanges )
            {
                game.Kills = chronicleEditor.Kills;
                game.Casualties = chronicleEditor.Casualties;
                game.Timer = chronicleEditor.Timer;
                game.Date = chronicleEditor.Date;
                game.WarFunds = chronicleEditor.WarFunds;
                dataChanged( sender, e );
            }
        }

        void dataChanged( object sender, EventArgs e )
        {
            FireDataChangedEvent();
        }

        public event EventHandler DataChangedEvent;

        private void FireDataChangedEvent()
        {
            if( (DataChangedEvent != null) && (!ignoreChanges) )
            {
                DataChangedEvent( this, EventArgs.Empty );
            }
        }

        #endregion

        #region Utilities

        private void UpdateView()
        {
            characterCollectionEditor.CharacterCollection = game.Characters;

            optionsEditor.Options = game.Options;

            chronicleEditor.Feats = game.Feats;
            chronicleEditor.Wonders = game.Wonders;
            chronicleEditor.Artefacts = game.Artefacts;

            chronicleEditor.Kills = (game.Kills > 9999) ? 9999 : game.Kills;
            chronicleEditor.Casualties = (game.Casualties > 9999) ? 9999 : game.Casualties;
            chronicleEditor.Timer = game.Timer;
            chronicleEditor.WarFunds = game.WarFunds;
            chronicleEditor.Date = game.Date;

            inventoryEditor.Inventory = game.Inventory;
            poachersDenEditor.Inventory = game.PoachersDen;
        }

        #endregion

        public SavegameEditor()
        {
            InitializeComponent();

            tabControl.Enabled = false;

            optionsEditor.DataChangedEvent += dataChanged;
            chronicleEditor.DataChangedEvent += chronicleEditor_DataChangedEvent;
            inventoryEditor.DataChangedEvent += dataChanged;
            poachersDenEditor.DataChangedEvent += dataChanged;
            characterCollectionEditor.DataChangedEvent += dataChanged;

        }

    }
}
