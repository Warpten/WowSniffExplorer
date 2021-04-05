using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IUpdateFieldStorage
    {
        public void ProcessValuesUpdate(Packet packet, UpdateMask updateMask);

        public int BitCount { get; }
    }
}