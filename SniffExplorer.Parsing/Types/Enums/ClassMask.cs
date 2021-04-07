using System;

namespace SniffExplorer.Parsing.Types.Enums
{
    /// <summary>
    /// This is <b>not</b> the game's classmask. This is used exclusively for filtering.
    /// </summary>
    [Flags]
    public enum ClassMask : ushort
    {
        Warrior     = 0x0001,
        Paladin     = 0x0002,
        Hunter      = 0x0004,
        Rogue       = 0x0008,
        Priest      = 0x0010,
        DeathKnight = 0x0020,
        Shaman      = 0x0040,
        Mage        = 0x0080,
        Warlock     = 0x0100,
        Monk        = 0x0200,
        Druid       = 0x0400,
        DemonHunter = 0x0800
    }

    public enum Class
    {
        Warrior     = 1,
        Paladin     = 2,
        Hunter      = 3,
        Rogue       = 4,
        Priest      = 5,
        DeathKnight = 6,
        Shaman      = 7,
        Mage        = 8,
        Warlock     = 9,
        Monk        = 10,
        Druid       = 11,
        DemonHunter = 12
    }
}
