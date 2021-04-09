using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IGameObjectData : IUpdateFieldStorage
    {
        public IUpdateField<IObjectGUID> CreatedBy { get; }
        public IUpdateField<int>         DisplayID { get; }
        public IUpdateField<uint>        Flags { get; }
        public IUpdateField<float>[]     ParentRotation { get; }
        public IUpdateField<ushort[]>    Dynamic { get; }
        public IUpdateField<int>         Faction { get; }
        public IUpdateField<int>         Level { get; }
        public IUpdateField<byte[]>      Bytes { get; }
    }
}
