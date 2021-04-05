namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IContainerData : IUpdateFieldStorage
    {
        public IUpdateField NumSlots { get; }
        public IUpdateField AlignPad { get; }
        public IUpdateField[] Slots { get; }
    }
}