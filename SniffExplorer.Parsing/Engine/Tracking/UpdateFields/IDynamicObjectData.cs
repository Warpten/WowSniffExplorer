namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IDynamicObjectData : IUpdateFieldStorage
    {
        public IUpdateField Caster { get; }
        public IUpdateField Bytes { get; }
        public IUpdateField SpellID { get; }
        public IUpdateField Radius { get; }
        public IUpdateField CastTime { get; }
    }
}
