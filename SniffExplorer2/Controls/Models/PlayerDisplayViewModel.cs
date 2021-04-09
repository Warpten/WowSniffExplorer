using System;
using System.Diagnostics.CodeAnalysis;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Types.Enums;

namespace SniffExplorer.UI.Controls.Models
{
    public class DisplayPlayer : EntityDisplay<Player>
    {
        public ClassMask Class => Entity.Class;
        public RaceMask Race => Entity.Race;
        public uint Level => Entity.Level;
        public string Name => Entity.Name;
    }

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Used in XAML binding")]
    public partial class PlayerDisplayViewModel : EntityViewModel<DisplayPlayer, Player, PlayerDisplayViewModel>
    {
        public ClassFilters Classes { get; } = new();
        public RaceFilters Races { get; } = new();
        
        public PlayerDisplayViewModel()
            : base((entity, model) =>
            {
                if (!model.Classes.Value.HasFlag(entity.Class))
                    return false;

                if (!model.Races.Value.HasFlag(entity.Race))
                    return false;

                if (!string.IsNullOrEmpty(model._nameFilter) && (!entity.Name?.Contains(model._nameFilter, StringComparison.InvariantCultureIgnoreCase) ?? false))
                    return false;

                return true;
            })
        {
            Classes.PropertyChanged += (s, _) => RefreshView();
            Races.PropertyChanged += (s, _) => RefreshView();
        }

    }
}