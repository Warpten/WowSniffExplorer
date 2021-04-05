using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Helpers
{
    /// <summary>
    /// This interface provides methods designed to help generate data structures used by entites.
    ///
    /// All the implementations of that interface are generated using source generators.
    /// </summary>
    public interface IEntityHelper
    {
        public ParsingContext Context { get; }

        public IObjectData CreateObjectData(IObjectGUID guid);
        public IItemData CreateItemData(IObjectGUID guid);
        public IContainerData CreateContainerData(IObjectGUID guid);
        public IUnitData CreateUnitData(IObjectGUID guid);
        public IPlayerData CreatePlayerData(IObjectGUID guid);
    }
}
