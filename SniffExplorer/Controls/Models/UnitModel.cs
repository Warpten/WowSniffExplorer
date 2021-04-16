using SniffExplorer.Parsing.Engine.Tracking.Entities;

using System;

namespace SniffExplorer.Controls.Models
{
    public partial class UnitModel : EntityModel<UnitEntityDisplay, Unit, UnitModel>
    {
        public UnitModel() : base((entity, self) => {
            if (self.NameFilter != null && entity.Name != null)
            {
                if (!entity.Name.Contains(self.NameFilter, StringComparison.InvariantCultureIgnoreCase))
                    return false;
            }

            return true;
        })
        {
        }
    }
}
