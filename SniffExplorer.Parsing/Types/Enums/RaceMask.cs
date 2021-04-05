using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniffExplorer.Parsing.Types.Enums
{
    [Flags]
    public enum RaceMask : ulong
    {
        Human               = 0x0000000001,
        Orc                 = 0x0000000002,
        Dwarf               = 0x0000000004,
        NightElf            = 0x0000000008,
        Undead              = 0x0000000010,
        Tauren              = 0x0000000020,
        Gnome               = 0x0000000040,
        Troll               = 0x0000000080,
        Goblin              = 0x0000000100,
        BloodElf            = 0x0000000200,
        Draenei             = 0x0000000400,
        FelOrc              = 0x0000000800,
        Naga                = 0x0000001000,
        Broken              = 0x0000002000,
        Skeleton            = 0x0000004000,
        Vrykul              = 0x0000008000,
        Tuskarr             = 0x0000010000,
        ForestTroll         = 0x0000020000,
        Taunka              = 0x0000040000,
        NorthrendSkeleton   = 0x0000080000,
        IceTroll            = 0x0000100000,
        Worgen              = 0x0000200000,
        Gilnean             = 0x0000400000, // Human
        PandarenNeutral     = 0x0000800000,
        PandarenAlliance    = 0x0001000000,
        PandarenHorde       = 0x0002000000,
        Nightborne          = 0x0004000000,
        HighmountainTauren  = 0x0008000000,
        VoidElf             = 0x0010000000,
        LightforgedDraenei  = 0x0020000000,
        ZandalariTroll      = 0x0040000000,
        KulTiran            = 0x0080000000,
        ThinHuman           = 0x0100000000,
        DarkIronDwarf       = 0x0200000000,
        Vulpera             = 0x0400000000,
        MagharOrc           = 0x0800000000,
        Mechagnome          = 0x1000000000,
    }
}
