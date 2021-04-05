using SniffExplorer.Parsing.Types.Enums;
using SniffExplorer.Shared.Attributes.UI;

namespace SniffExplorer.UI.Controls.Models
{
    public partial class RaceFilters
    {
        [EnumFlagProperty]
        public RaceMask Value { get; set; } = RaceMask.Human |
                                              RaceMask.Orc |
                                              RaceMask.Dwarf |
                                              RaceMask.NightElf |
                                              RaceMask.Undead |
                                              RaceMask.Tauren |
                                              RaceMask.Gnome |
                                              RaceMask.Troll |
                                              RaceMask.Goblin |
                                              RaceMask.BloodElf |
                                              RaceMask.Draenei |
                                              RaceMask.Worgen |
                                              RaceMask.PandarenNeutral |
                                              RaceMask.PandarenAlliance |
                                              RaceMask.PandarenHorde |
                                              RaceMask.Nightborne |
                                              RaceMask.HighmountainTauren |
                                              RaceMask.VoidElf |
                                              RaceMask.LightforgedDraenei |
                                              RaceMask.ZandalariTroll |
                                              RaceMask.KulTiran |
                                              RaceMask.DarkIronDwarf |
                                              RaceMask.Vulpera |
                                              RaceMask.MagharOrc |
                                              RaceMask.Mechagnome;
    }
}