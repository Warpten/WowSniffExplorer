using SniffExplorer.Parsing.Engine.Tracking.Entities;

namespace SniffExplorer.Controls.Models
{
    public sealed class UnitEntityDisplay : EntityDisplay<Unit>
    {
        public string? Name => Object.Name;
        public uint Entry => Guid.Entry!.Value;
    }
}