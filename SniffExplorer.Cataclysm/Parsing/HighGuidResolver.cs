using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Parsing.Types.Resolvers;
using SniffExplorer.Parsing.Versions;

namespace SniffExplorer.Cataclysm.Parsing
{
    public class HighGuidResolver : IHighGuidResolver
    {
        public bool CanResolve(IObjectGUID objectGuid, ClientBuild clientBuild)
            => clientBuild.Expansion == Expansion.Cataclysm;

        public ObjectGuidType Resolve(IObjectGUID guid, ClientBuild clientBuild)
        {
            if (guid.Parts[0] == 0)
                return ObjectGuidType.Null;

            var highGUID = (guid.Parts[0] & 0xF0F0000000000000) >> 52;
            return highGUID switch
            {
                0x000 => ObjectGuidType.Player,
                0x101 => ObjectGuidType.Battleground,
                0x104 => ObjectGuidType.InstanceSave,
                0x105 => ObjectGuidType.Party,
                0x109 => ObjectGuidType.Battleground, // 2
                0x10E => ObjectGuidType.Unknown,
                0x10F => ObjectGuidType.Guild,
                0x400 => ObjectGuidType.Item,
                0xF00 => ObjectGuidType.DynamicObject,
                0xF01 => ObjectGuidType.GameObject,
                0x10C => ObjectGuidType.Transport, // MO Transport
                0xF02 => ObjectGuidType.Transport,
                0xF03 => ObjectGuidType.Creature,
                0xF04 => ObjectGuidType.Pet,
                0xF05 => ObjectGuidType.Vehicle,
                // 
                _ => (highGUID & 0xF00) switch
                {
                    // TODO: Players sometimes have high guid 0x048 for reasons
                    // TODO: unbeknownst to me. Just check the high byte. If zero,
                    // TODO: the high guid will be 0x0??, aka Player.
                    0x000 => ObjectGuidType.Player,
                    _     => ObjectGuidType.Unknown
                }
            };
        }
    }
}
