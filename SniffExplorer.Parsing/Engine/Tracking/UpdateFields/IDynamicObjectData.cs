using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IDynamicObjectData : IUpdateFieldStorage
    {
        public IUpdateField<IObjectGUID> Caster { get; }
        public IUpdateField<int> Bytes { get; }
        public IUpdateField<int> SpellID { get; }
        public IUpdateField<float> Radius { get; }
        public IUpdateField<int> CastTime { get; }
    }
}
