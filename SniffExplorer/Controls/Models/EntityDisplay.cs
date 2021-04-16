using JetBrains.Annotations;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Controls.Models
{
    public abstract class EntityDisplay<T> where T : IEntity
    {
        [NotNull] public T Object { get; init; }

        public IObjectGUID Guid => Object.Guid;
    }
}