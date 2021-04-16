using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Types.Enums;

namespace SniffExplorer.Controls.Models
{
    public sealed class PlayerEntityDisplay : EntityDisplay<Player>
    {
        public string? Name => Object.Name;

        public ClassMask Class => Object.Class;
        public RaceMask Race => Object.Race;
        public uint Level => Object.Level;
    }
}