using System.Security.Cryptography.X509Certificates;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    /// <summary>
    /// The basic entity type. Each game module is supposed to provide an implementation of this class.
    /// </summary>
    public class Object : IObject
    {
        private readonly ParsingContext _context;

        /// <summary>
        /// The GUID of this entity.
        /// </summary>
        public IObjectGUID Guid { get; }

        public IObjectData ObjectData { get; }

        public string? Name => _context.NameCache[Guid];

        public virtual EntityTypeID TypeID => EntityTypeID.Object;

        public IHistory<MovementInfo> MovementInfo { get; } = HistoryFactory.Create<MovementInfo>();

        public Object(IObjectGUID guid, ParsingContext context)
        {
            _context = context;

            Guid = guid;
            ObjectData = context.Helper.UpdateFieldProvider.CreateObjectData(guid)!;
        }

        public virtual void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            ObjectData.ProcessValuesUpdate(packet, updateMask);
        }
    }
}