using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace LionEditor
{
    public struct Item
    {
        ItemType Type;

        ItemSubType SubType;

        UInt16 Offset;
    }

    enum ItemType
    {
        [Description("Hand")]
        Hand,

        [Description("Item")]
        Item,

        [Description("Head")]
        Head,

        [Description("Body")]
        Body,

        [Description("Accessory")]
        Accessory
    }

    enum ItemSubType
    {
        [Description("Knife")]
        Knife,
        
        [Description("NinjaB lade")]
        NinjaBlade,
        
        [Description("Sword")]
        Sword,
        
        [Description("Knight's Sword")]
        KnightsSword,
        
        [Description("Katana")]
        Katana,
        
        [Description("Axe")]
        Axe,

        [Description("Rod")]
        Rod,
        
        [Description("Staff")]
        Staff,
        
        [Description("Flail")]
        Flail,
        
        [Description("Gun")]
        Gun,
        
        [Description("Crossbow")]
        Crossbow,
        
        [Description("Bow")]
        Bow,
        
        [Description("Instrument")]
        Instrument,
        
        [Description("Book")]
        Book,
        
        [Description("Polearm")]
        Polearm,
        
        [Description("Pole")]
        Pole,
        
        [Description("Bag")]
        Bag,
        
        [Description("Cloth")]
        Cloth,
        
        [Description("Throwing")]
        Throwing,
        
        [Description("Bomb")]
        Bomb,
        
        [Description("Shield")]
        Shield,
        
        [Description("Helmet")]
        Helmet,
        
        [Description("Hat")]
        Hat,
        
        [Description("Hair Adornment")]
        HairAdornment,
        
        [Description("Armor")]
        Armor,
        
        [Description("Clothing")]
        Clothing,
        
        [Description("Robe")]
        Robe,
        
        [Description("Shoes")]
        Shoes,
        
        [Description("Armguard")]
        Armguard,
        
        [Description("Ring")]
        Ring,
        
        [Description("Armlet")]
        Armlet,
        
        [Description("Cloak")]
        Cloak,
        
        [Description("Perfume")]
        Perfume,
        
        [Description("Fell Sword")]
        FellSword,
        
        [Description("Lip Rouge")]
        LipRouge
    }

}
