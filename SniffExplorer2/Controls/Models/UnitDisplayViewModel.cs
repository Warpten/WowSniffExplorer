using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Types.Enums;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.UI;
using SniffExplorer.UI.Commands;

namespace SniffExplorer.UI.Controls.Models
{
    public class DisplayCreature : EntityDisplay<Creature>
    {
        public string Name => Entity.Name;
        public uint Entry => Entity.Entry;
    }

    public partial class UnitDisplayViewModel : EntityViewModel<DisplayCreature, Creature, UnitDisplayViewModel>
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