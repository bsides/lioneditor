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

namespace LionEditor
{
    public enum CursorMovement
    {
        /// <summary>
        /// Pressing the up button moves the cursor at a 
        /// 45-degree angle to the upper right.
        /// </summary>
        TypeA = 0,
        
        /// <summary>
        /// Pressing the up button moves the cursor at a
        /// 45-degree angle to the upper left.
        /// </summary>
        TypeB = 1
    }

    public enum Speed
    {
        /// <summary>
        /// Repeat after 1/8 second, moving
        /// one tile every 1/30 second.
        /// </summary>
        Fast = 0,

        /// <summary>
        /// Repeat after 1/4 second, moving
        /// one tile every 1/15 second.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Repeat after 1/2 second, moving
        /// one tile every 1/10 second.
        /// </summary>
        Slow = 2
    }

    public enum MultiheightToggleRate
    {
        /// <summary>
        /// Switches every 1/2 second.
        /// </summary>
        Fast = 0,

        /// <summary>
        /// Switches every 3/4 second.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Switches every second.
        /// </summary>
        Slow = 2,

        /// <summary>
        /// Auto-toggle is turned off, and cursor movement
        /// between levels is toggled by pressing START.
        /// </summary>
        Off = 3
    }

    public enum MenuCursorSpeed
    {
        /// <summary>
        /// Repeat after 1/8 second, moving one
        /// menu item every 1/30 second.
        /// </summary>
        Fastest = 0,

        /// <summary>
        /// Repeat after 1/6 second, moving one
        /// menu item every 1/30 second.
        /// </summary>
        Faster = 1,

        /// <summary>
        /// Repeat after 1/5 second, moving one
        /// menu item every 1/15 second.
        /// </summary>
        Fast = 2,

        /// <summary>
        /// Repeat after 7/30 second, moving one
        /// menu item every 1/15 second.
        /// </summary>
        Normal = 3,

        /// <summary>
        /// Repeat after 4/15 second, moving one
        /// menu item every 1/10 second.
        /// </summary>
        Slow = 4,

        /// <summary>
        /// Repeat after 9/30 second, moving one
        /// menu item every 1/10 second.
        /// </summary>
        Slower = 5,

        /// <summary>
        /// Repeat after 1/3 second, moving one
        /// menu item every 2/15 second.
        /// </summary>
        Slowest = 6
    }

    public enum Sound
    {
        Mono = 0,
        Stereo = 1,
        Surround = 2
    }

    /// <summary>
    /// Represents the game options
    /// </summary>
    public class Options
    {
        #region Fields

        private CursorMovement cursorMovement;
        private Speed cursorRepeatRate;
        private MultiheightToggleRate multiHeightToggleRate;
        private MenuCursorSpeed menuCursorSpeed;
        private Speed messageSpeed;
        private bool battlePrompts;
        private bool displayAbilityNames;
        private bool displayEffectMessages;
        private bool displayEarnedExpJp;
        private bool targetColors;
        private bool displayUnequippableItems;
        private bool optimizeOnJobChange;
        private Sound sound;
        #endregion

        #region Properties

        /// <summary>
        /// Change the axis for cursor movement on the battlefield.
        /// </summary>
        public CursorMovement CursorMovement
        {
        	get { return cursorMovement; }
        	set { cursorMovement = value; }
        }

        /// <summary>
        /// Adjust cursor speed on the battlefield.
        /// </summary>
        public Speed CursorRepeatRate
        {
        	get { return cursorRepeatRate; }
        	set { cursorRepeatRate = value; }
        }

        /// <summary>
        /// Adjust how fast the cursor moves between two levels,
        /// such as tiles on and below a bridge.
        /// </summary>
        public MultiheightToggleRate MultiHeightToggleRate
        {
        	get { return multiHeightToggleRate; }
        	set { multiHeightToggleRate = value; }
        }

        /// <summary>
        /// Change the speed of the finger cursor in menus.
        /// </summary>
        public MenuCursorSpeed MenuCursorSpeed
        {
        	get { return menuCursorSpeed; }
        	set { menuCursorSpeed = value; }
        }

        /// <summary>
        /// Change the speed at which messages are displayed.
        /// There are 3 speeds to choose from.
        /// </summary>
        public Speed MessageSpeed
        {
        	get { return messageSpeed; }
        	set { messageSpeed = value; }
        }

        /// <summary>
        /// Toggle the display of messages explaining 
        /// game controls during battle.
        /// </summary>
        public bool BattlePrompts
        {
        	get { return battlePrompts; }
        	set { battlePrompts = value; }
        }

        /// <summary>
        /// Toggle the display of action and reaction
        /// ability names during battle.
        /// </summary>
        public bool DisplayAbilityNames
        {
        	get { return displayAbilityNames; }
        	set { displayAbilityNames = value; }
        }

        /// <summary>
        /// Toggle the display of effect descriptions
        /// during battle.
        /// </summary>
        public bool DisplayEffectMessages
        {
        	get { return displayEffectMessages; }
        	set { displayEffectMessages = value; }
        }

        /// <summary>
        /// Toggle the display of the experience points
        /// and job points that units earn in battle.
        /// </summary>
        public bool DisplayEarnedExpJp
        {
        	get { return displayEarnedExpJp; }
        	set { displayEarnedExpJp = value; }
        }

        /// <summary>
        /// Cause units to blink in different colors during
        /// battle so that enemies and allies can be easily
        /// identified when selecting a target.
        /// </summary>
        public bool TargetColors
        {
        	get { return targetColors; }
        	set { targetColors = value; }
        }

        /// <summary>
        /// Set whether unequippable items should be 
        /// displayed when selecting a unit's equipment.
        /// </summary>
        public bool DisplayUnequippableItems
        {
        	get { return displayUnequippableItems; }
        	set { displayUnequippableItems = value; }
        }

        /// <summary>
        /// Determine whether a unit's equipment should be 
        /// optimized whenever the unit's job or abilities are
        /// changed.
        /// </summary>
        public bool OptimizeOnJobChange
        {
        	get { return optimizeOnJobChange; }
        	set { optimizeOnJobChange = value; }
        }

        /// <summary>
        /// Set music and sound effects to mono, stereo, or
        /// surround sound.
        /// </summary>
        public Sound Sound
        {
        	get { return sound; }
        	set { sound = value; }
        }

        #endregion

        public Options( byte[] bytes )
        {
            cursorMovement = (CursorMovement)(bytes[0] & 0x01);
            cursorRepeatRate = (Speed)((bytes[0] & 0x18) >> 3);
            multiHeightToggleRate = (MultiheightToggleRate)((bytes[0] & 0xC0) >> 6);
            menuCursorSpeed = (MenuCursorSpeed)((bytes[1] & 0x0E) >> 1);
            messageSpeed = (Speed)((bytes[1] & 0x30) >> 4);
            battlePrompts = (bytes[1] & 0x80) == 0;
            displayAbilityNames = (bytes[2] & 0x02) == 0;
            displayEffectMessages = (bytes[2] & 0x08) == 0;
            sound = (Sound)((bytes[2] & 0x60) >> 5);
            displayUnequippableItems = (bytes[2] & 0x80) == 0;
            displayEarnedExpJp = (bytes[3] & 0x02) == 0;
            targetColors = (bytes[3] & 0x08) == 0;
            optimizeOnJobChange = (bytes[3] & 0x20) == 0;
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[4];
            result[0] |= (byte)cursorMovement;
            result[0] |= (byte)(((byte)cursorRepeatRate) << 3);
            result[0] |= (byte)(((byte)multiHeightToggleRate) << 6);

            result[1] |= (byte)(((byte)menuCursorSpeed) << 1);
            result[1] |= (byte)(((byte)messageSpeed) << 4);
            result[1] |= (byte)((battlePrompts ? 0 : 1) << 7);

            result[2] |= (byte)((displayAbilityNames ? 0 : 1) << 1);
            result[2] |= (byte)((displayEffectMessages ? 0 : 1) << 3);
            result[2] |= (byte)(((byte)sound) << 5);
            result[2] |= (byte)((displayUnequippableItems ? 0 : 1) << 7);

            result[3] |= (byte)((displayEarnedExpJp ? 0 : 1) << 1);
            result[3] |= (byte)((targetColors ? 0 : 1) << 3);
            result[3] |= (byte)((optimizeOnJobChange ? 0 : 1) << 5);

            return result;
        }

    }
}
