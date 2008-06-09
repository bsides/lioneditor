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
using System.Windows.Forms;

namespace LionEditor
{
    public partial class OptionsEditor : UserControl
    {
        #region Fields

        private bool ignoreChanges = false;
        private Options options;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the options to change
        /// </summary>
        public Options Options
        {
            get { return options; }
            set
            {
                options = value;
                if( options != null )
                {
                    ignoreChanges = true;
                    cursorMovement.SelectedItem = options.CursorMovement;
                    cursorRepeatRate.SelectedItem = options.CursorRepeatRate;
                    multiheightToggleRate.SelectedItem = options.MultiHeightToggleRate;
                    menuCursorSpeed.SelectedItem = options.MenuCursorSpeed;
                    messageSpeed.SelectedItem = options.MessageSpeed;
                    battlePrompts.Checked = options.BattlePrompts;
                    displayAbilityNames.Checked = options.BattlePrompts;
                    displayEffectMessages.Checked = options.DisplayEffectMessages;
                    displayEarnedExpJp.Checked = options.DisplayEarnedExpJp;
                    targetColors.Checked = options.TargetColors;
                    displayUnequippableItems.Checked = options.DisplayUnequippableItems;
                    optimizeOnJobChange.Checked = options.OptimizeOnJobChange;
                    sound.SelectedItem = options.Sound;

                    ignoreChanges = false;
                }
            }
        }

        #endregion

        #region Events

        void optionsChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges )
            {
                options.CursorMovement = (CursorMovement)cursorMovement.SelectedItem;
                options.CursorRepeatRate = (Speed)cursorRepeatRate.SelectedItem;
                options.MultiHeightToggleRate = (MultiheightToggleRate)multiheightToggleRate.SelectedItem;
                options.MenuCursorSpeed = (MenuCursorSpeed)menuCursorSpeed.SelectedItem;
                options.MessageSpeed = (Speed)messageSpeed.SelectedItem;
                options.BattlePrompts = battlePrompts.Checked;
                options.DisplayAbilityNames = displayAbilityNames.Checked;
                options.DisplayEffectMessages = displayEffectMessages.Checked;
                options.DisplayEarnedExpJp = displayEarnedExpJp.Checked;
                options.TargetColors = targetColors.Checked;
                options.DisplayUnequippableItems = displayUnequippableItems.Checked;
                options.OptimizeOnJobChange = optimizeOnJobChange.Checked;
                options.Sound = (Sound)sound.SelectedItem;
                FireDataChangedEvent();
            }
        }

        public event EventHandler DataChangedEvent;

        private void FireDataChangedEvent()
        {
            if( DataChangedEvent != null )
            {
                DataChangedEvent( this, EventArgs.Empty );
            }
        }

        #endregion

        public OptionsEditor()
        {
            InitializeComponent();

            cursorMovement.DataSource = Enum.GetValues( typeof( CursorMovement ) );
            cursorRepeatRate.DataSource = Enum.GetValues( typeof( Speed ) );
            multiheightToggleRate.DataSource = Enum.GetValues( typeof( MultiheightToggleRate ) );
            menuCursorSpeed.DataSource = Enum.GetValues( typeof( MenuCursorSpeed ) );
            messageSpeed.DataSource = Enum.GetValues( typeof( Speed ) );
            sound.DataSource = Enum.GetValues( typeof( Sound ) );

            cursorMovement.SelectedIndexChanged += optionsChanged;
            cursorRepeatRate.SelectedIndexChanged += optionsChanged;
            multiheightToggleRate.SelectedIndexChanged += optionsChanged;
            menuCursorSpeed.SelectedIndexChanged += optionsChanged;
            messageSpeed.SelectedIndexChanged += optionsChanged;
            battlePrompts.CheckStateChanged += optionsChanged;
            displayAbilityNames.CheckStateChanged += optionsChanged;
            displayEffectMessages.CheckStateChanged += optionsChanged;
            displayEarnedExpJp.CheckStateChanged += optionsChanged;
            targetColors.CheckStateChanged += optionsChanged;
            displayUnequippableItems.CheckStateChanged += optionsChanged;
            optimizeOnJobChange.CheckStateChanged += optionsChanged;
            sound.SelectedIndexChanged += optionsChanged;
        }
    }
}
