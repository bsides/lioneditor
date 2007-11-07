using System.ComponentModel;

namespace LionEditor
{
    public enum Zodiac
    {
        Aries = 0x00,
        Taurus = 0x10,
        Gemini = 0x20,
        Cancer = 0x30,
        Leo = 0x40,
        Virgo = 0x50,
        Libra = 0x60,
        Scorpio = 0x70,
        Sagittarius = 0x80,
        Capricorn = 0x90,
        Aquarius = 0xA0,
        Pisces = 0xB0,
        Serpentarius = 0xC0
    }

    public enum Gender
    {
        [Description( "Monster" )]
        Monster = 0x20,
        [Description( "Monster" )]
        MonsterAlt = 0x30,

        [Description( "Female" )]
        Female = 0x40,
        [Description( "Female" )]
        FemaleAlt = 0x50,

        [Description( "Male" )]
        Male = 0x80,
        [Description( "Male" )]
        MaleAlt = 0x90
    }
}
