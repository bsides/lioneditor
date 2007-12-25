using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using FFTPatcher.Properties;

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
        public string ActionName
        {
            get
            {
                return MenuAction.ToString();
            }
        }
        public byte Value { get; private set; }

        public ActionMenu( byte value, string name, MenuAction action )
        {
            MenuAction = ActionMenuEntry.AllActionMenuEntries[(byte)action];
            Name = name;
            Value = value;
        }
    }
    
    public class AllActionMenus
    {
        public ActionMenu[] ActionMenus { get; private set; }

        public AllActionMenus( SubArray<byte> bytes )
        {
            ActionMenus = new ActionMenu[256];

            for( int i = 0; i < 256; i++ )
            {
                ActionMenus[i] = new ActionMenu( (byte)i, SkillSet.DummySkillSets[i].Name, (MenuAction)bytes[i] );
            }
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[256];

            for( int i = 0; i < 256; i++ )
            {
                result[i] = (byte)ActionMenus[i].MenuAction.MenuAction;
            }

            return result;
        }

        public string GenerateCodes()
        {
            byte[] newBytes = this.ToByteArray();
            byte[] oldBytes = Resources.ActionEventsBin;
            StringBuilder codeBuilder = new StringBuilder();
            for( int i = 0; i < newBytes.Length; i++ )
            {
                if( newBytes[i] != oldBytes[i] )
                {
                    UInt32 addy = (UInt32)(0x27AC50 + i);
                    string code = "_L 0x0" + addy.ToString( "X7" ) + " 0x000000" + newBytes[i].ToString( "X2" );
                    codeBuilder.AppendLine( code );
                }
            }

            return codeBuilder.ToString();
        }
    }
}
