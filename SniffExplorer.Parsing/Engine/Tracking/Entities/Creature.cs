using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public class Creature : Unit
    {
        public uint Entry => Guid.Entry!.Value;

        public Creature(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
        }
    }
}