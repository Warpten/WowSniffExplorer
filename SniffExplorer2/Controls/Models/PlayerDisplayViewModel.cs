using System;
using System.Diagnostics.CodeAnalysis;
using SniffExplorer.Parsing.Engine.Tracking.Entities;

namespace SniffExplorer.UI.Controls.Models
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Used in XAML binding")]
    public partial class PlayerDisplayViewModel : EntityViewModel<Player, PlayerDisplayViewModel>
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