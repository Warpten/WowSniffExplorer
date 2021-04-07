using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Shared.Attributes.UI;

namespace SniffExplorer.UI.Controls.Models
{
    public partial class UnitDisplayViewModel : EntityViewModel<Creature, UnitDisplayViewModel>
    {
        [NotifyingProperty(PropertyName = "EntryFilter")]
        private string _entryFilter;

        public UnitDisplayViewModel()
            : base((unit, model) =>
            {
                if (!string.IsNullOrEmpty(model._nameFilter) && !(unit.Name?.Contains(model._nameFilter) ?? false))
                    return false;

                if (model._entryFilter != null)
                {
                    if (int.TryParse(model._entryFilter, out var value))
                        if (unit.Entry != value)
                            return false;
                }

                return true;
            })
        {

        }
        
        partial void AfterEntryFilterChange() => RefreshView();
    }
}