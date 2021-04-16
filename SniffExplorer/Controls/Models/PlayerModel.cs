using System;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Types.Enums;
using SniffExplorer.Shared.Attributes.UI;

namespace SniffExplorer.Controls.Models
{
    public partial class PlayerModel : EntityModel<PlayerEntityDisplay, Player, PlayerModel>
    {
        public PlayerModel() : base((entity, self) => {
            if (self.NameFilter != null && entity.Name != null)
            {
                if (!entity.Name.Contains(self.NameFilter, StringComparison.InvariantCultureIgnoreCase))
                    return false;
            }

            if (!self.Races.HasFlag(entity.Race))
                return false;

            if (!self.Classes.HasFlag(entity.Class))
                return false;
            
            return true;
        })
        {
        }

        [EnumFlagProperty]
        public RaceMask Races { get; private set; } = RaceMask.Human |
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

        [EnumFlagProperty]
        public ClassMask Classes { get; private set; } = ClassMask.Warrior |
                                                         ClassMask.Paladin |
                                                         ClassMask.Hunter |
                                                         ClassMask.Rogue |
                                                         ClassMask.Priest |
                                                         ClassMask.DeathKnight |
                                                         ClassMask.Shaman |
                                                         ClassMask.Mage |
                                                         ClassMask.Warlock |
                                                         ClassMask.Monk |
                                                         ClassMask.Druid |
                                                         ClassMask.DemonHunter;
    }
}
