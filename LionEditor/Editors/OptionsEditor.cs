using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LionEditor.Editors
{
    public partial class OptionsEditor : UserControl
    {
        bool ignoreChanges = false;

        private Options options;
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

        public OptionsEditor()
        {
            InitializeComponent();

            //zodiacComboBox.DataSource = Enum.GetValues( typeof( Zodiac ) );
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
    }
}
