using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public interface IEntity
    {
        public IObjectGUID Guid { get; }
        public IHistory<MovementInfo> MovementInfo { get; }
        
        /// <summary>
        /// Object type as seen in JamCliObjCreate.
        /// </summary>
        public EntityTypeID TypeID { get; }
        
        public void ProcessValuesUpdate(Packet packet, UpdateMask updateMask);
    }
}
