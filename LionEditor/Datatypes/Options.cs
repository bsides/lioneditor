using System;
using System.Collections.Generic;
using System.Text;

namespace LionEditor
{
    public enum CursorMovement
    {
        TypeA = 0,
        TypeB = 1
    }

    public enum Speed
    {
        Fast = 0,
        Normal = 1,
        Slow = 2
    }

    public enum MultiheightToggleRate
    {
        Fast = 0,
        Normal = 1,
        Slow = 2,
        Off = 3
    }

    public enum MenuCursorSpeed
    {
        Fastest = 0,
        Faster = 1,
        Fast = 2,
        Normal = 3,
        Slow = 4,
        Slower = 5,
        Slowest = 6
    }

    public enum Sound
    {
        Mono = 0,
        Stereo = 1,
        Surround = 2
    }
    public class Options
    {


        private CursorMovement m_CursorMovement;
        public CursorMovement CursorMovement
        {
        	get { return m_CursorMovement; }
        	set { m_CursorMovement = value; }
        }

        private Speed m_CursorRepeatRate;
        public Speed CursorRepeatRate
        {
        	get { return m_CursorRepeatRate; }
        	set { m_CursorRepeatRate = value; }
        }

        private MultiheightToggleRate m_MultiHeightToggleRate;
        public MultiheightToggleRate MultiHeightToggleRate
        {
        	get { return m_MultiHeightToggleRate; }
        	set { m_MultiHeightToggleRate = value; }
        }

        private MenuCursorSpeed m_MenuCursorSpeed;
        public MenuCursorSpeed MenuCursorSpeed
        {
        	get { return m_MenuCursorSpeed; }
        	set { m_MenuCursorSpeed = value; }
        }

        private Speed m_MessageSpeed;
        public Speed MessageSpeed
        {
        	get { return m_MessageSpeed; }
        	set { m_MessageSpeed = value; }
        }

        private bool m_BattlePrompts;
        public bool BattlePrompts
        {
        	get { return m_BattlePrompts; }
        	set { m_BattlePrompts = value; }
        }

        private bool m_DisplayAbilityNames;
        public bool DisplayAbilityNames
        {
        	get { return m_DisplayAbilityNames; }
        	set { m_DisplayAbilityNames = value; }
        }

        private bool m_DisplayEffectMessages;
        public bool DisplayEffectMessages
        {
        	get { return m_DisplayEffectMessages; }
        	set { m_DisplayEffectMessages = value; }
        }

        private bool m_DisplayEarnedExpJp;
        public bool DisplayEarnedExpJp
        {
        	get { return m_DisplayEarnedExpJp; }
        	set { m_DisplayEarnedExpJp = value; }
        }

        private bool m_TargetColors;
        public bool TargetColors
        {
        	get { return m_TargetColors; }
        	set { m_TargetColors = value; }
        }

        private bool m_DisplayUnequippableItems;
        public bool DisplayUnequippableItems
        {
        	get { return m_DisplayUnequippableItems; }
        	set { m_DisplayUnequippableItems = value; }
        }

        private bool m_OptimizeOnJobChange;
        public bool OptimizeOnJobChange
        {
        	get { return m_OptimizeOnJobChange; }
        	set { m_OptimizeOnJobChange = value; }
        }

        private Sound m_Sound;
        public Sound Sound
        {
        	get { return m_Sound; }
        	set { m_Sound = value; }
        }

        public Options( byte[] bytes )
        {
            CursorMovement = (CursorMovement)(bytes[0] & 0x01);
            CursorRepeatRate = (Speed)((bytes[0] & 0x18) >> 3);
            MultiHeightToggleRate = (MultiheightToggleRate)((bytes[0] & 0xC0) >> 6);
            MenuCursorSpeed = (MenuCursorSpeed)((bytes[1] & 0x0E) >> 1);
            MessageSpeed = (Speed)((bytes[1] & 0x30) >> 4);
            BattlePrompts = (bytes[1] & 0x80) == 0;
            DisplayAbilityNames = (bytes[2] & 0x02) == 0;
            DisplayEffectMessages = (bytes[2] & 0x08) == 0;
            Sound = (Sound)((bytes[2] & 0x60) >> 5);
            DisplayUnequippableItems = (bytes[2] & 0x80) == 0;
            DisplayEarnedExpJp = (bytes[3] & 0x02) == 0;
            TargetColors = (bytes[3] & 0x08) == 0;
            OptimizeOnJobChange = (bytes[3] & 0x20) == 0;
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[4];
            result[0] |= (byte)CursorMovement;
            result[0] |= (byte)(((byte)CursorRepeatRate) << 3);
            result[0] |= (byte)(((byte)MultiHeightToggleRate) << 6);

            result[1] |= (byte)(((byte)MenuCursorSpeed) << 1);
            result[1] |= (byte)(((byte)MessageSpeed) << 4);
            result[1] |= (byte)((BattlePrompts ? 0 : 1) << 7);

            result[2] |= (byte)((DisplayAbilityNames ? 0 : 1) << 1);
            result[2] |= (byte)((DisplayEffectMessages ? 0 : 1) << 3);
            result[2] |= (byte)(((byte)Sound) << 5);
            result[2] |= (byte)((DisplayUnequippableItems ? 0 : 1) << 7);

            result[3] |= (byte)((DisplayEarnedExpJp ? 0 : 1) << 1);
            result[3] |= (byte)((TargetColors ? 0 : 1) << 3);
            result[3] |= (byte)((OptimizeOnJobChange ? 0 : 1) << 5);

            return result;
        }

    }
}
