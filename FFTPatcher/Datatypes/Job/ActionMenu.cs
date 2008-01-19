/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;


namespace FFTPatcher.Datatypes
{
    public enum MenuAction
    {
        [Description( "<Default>" )]
        Default,
        [Description( "Item Inventory" )]
        ItemInventory,
        [Description( "Weapon Inventory" )]
        WeaponInventory,
        [Description( "Arithmeticks" )]
        Arithmeticks,
        [Description( "Elements" )]
        Elements,
        [Description( "Blank" )]
        Blank1,
        [Description( "Monster" )]
        Monster,
        [Description( "Katana Inventory" )]
        KatanaInventory,
        [Description( "Attack" )]
        Attack,
        [Description( "Jump" )]
        Jump,
        [Description( "Charge" )]
        Charge,
        [Description( "Defend" )]
        Defend,
        [Description( "Change Equipment" )]
        ChangeEquip,
        [Description( "Unknown" )]
        Unknown2,
        [Description( "Blank" )]
        Blank2,
        [Description( "Unknown" )]
        Unknown3
    }

    public class ActionMenuEntry
    {
        private static List<ActionMenuEntry> allActionMenuEntries;
        public static List<ActionMenuEntry> AllActionMenuEntries
        {
            get
            {
                if( allActionMenuEntries == null )
                {
                    allActionMenuEntries = new List<ActionMenuEntry>( 16 );
                    for( byte i = 0; i < 16; i++ )
                    {
                        allActionMenuEntries.Add( new ActionMenuEntry( i ) );
                    }
                }

                return allActionMenuEntries;
            }
        }

        public MenuAction MenuAction { get; private set; }

        public override string ToString()
        {
            MemberInfo[] memInfo = typeof( MenuAction ).GetMember( MenuAction.ToString() );
            if( (memInfo != null) && (memInfo.Length > 0) )
            {
                object[] attrs = memInfo[0].GetCustomAttributes( typeof( DescriptionAttribute ), false );
                if( (attrs != null) && (attrs.Length > 0) )
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return base.ToString();
        }

        private ActionMenuEntry( byte b )
        {
            MenuAction = (MenuAction)b;
        }

        private ActionMenuEntry( MenuAction a )
        {
            MenuAction = a;
        }
    }

    public class ActionMenu
    {
        public string Name { get; private set; }
        public ActionMenuEntry MenuAction { get; set; }

        public ActionMenu Default { get; private set; }
        public string ActionName
        {
            get { return MenuAction.ToString(); }
        }

        public byte Value { get; private set; }

        public ActionMenu( byte value, string name, MenuAction action, ActionMenu defaults )
        {
            Default = defaults;
            MenuAction = ActionMenuEntry.AllActionMenuEntries[(byte)action];
            Name = name;
            Value = value;
        }

        public ActionMenu( byte value, string name, MenuAction action )
            : this( value, name, action, null )
        {
        }
    }
    
    public class AllActionMenus
    {
        public ActionMenu[] ActionMenus { get; private set; }

        public AllActionMenus( SubArray<byte> bytes )
        {
            byte[] defaultBytes = FFTPatch.Context == Context.US_PSP ? Resources.ActionEventsBin : PSXResources.ActionEventsBin;

            List<ActionMenu> tempActions = new List<ActionMenu>();

            for( int i = 0; i < 0xE0; i++ )
            {
                tempActions.Add( new ActionMenu( (byte)i, SkillSet.DummySkillSets[i].Name, (MenuAction)bytes[i],
                    new ActionMenu( (byte)i, SkillSet.DummySkillSets[i].Name, (MenuAction)defaultBytes[i] ) ) );
            }
            if( FFTPatch.Context == Context.US_PSP )
            {
                tempActions.Add( new ActionMenu( 0xE0, SkillSet.DummySkillSets[0xE0].Name, (MenuAction)bytes[0xE0],
                    new ActionMenu( (byte)0xE0, SkillSet.DummySkillSets[0xE0].Name, (MenuAction)defaultBytes[0xE0] ) ) );
                tempActions.Add( new ActionMenu( 0xE1, SkillSet.DummySkillSets[0xE1].Name, (MenuAction)bytes[0xE1],
                    new ActionMenu( (byte)0xE1, SkillSet.DummySkillSets[0xE1].Name, (MenuAction)defaultBytes[0xE1] ) ) );
                tempActions.Add( new ActionMenu( 0xE2, SkillSet.DummySkillSets[0xE2].Name, (MenuAction)bytes[0xE2],
                    new ActionMenu( (byte)0xE2, SkillSet.DummySkillSets[0xE2].Name, (MenuAction)defaultBytes[0xE2] ) ) );
            }

            ActionMenus = tempActions.ToArray();
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[ActionMenus.Length];

            for( int i = 0; i < ActionMenus.Length; i++ )
            {
                result[i] = (byte)ActionMenus[i].MenuAction.MenuAction;
            }

            return result;
        }

        public byte[] ToByteArray( Context context )
        {
            return ToByteArray();
        }

        public List<string> GenerateCodes()
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                return Codes.GenerateCodes( Context.US_PSP, Resources.ActionEventsBin, this.ToByteArray(), 0x27AC50 );
            }
            else
            {
                return Codes.GenerateCodes( Context.US_PSX, PSXResources.ActionEventsBin, this.ToByteArray(), 0x065CB4 );
            }
        }
    }
}
