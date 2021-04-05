using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Parsing.Versions;

namespace SniffExplorer.Parsing.Types.Resolvers
{
    /// <summary>
    /// Basic implementation of a GUID Resolver. These objects are in charge if identifying parts of a GUID
    /// for later use, depending on game versions. This particular instance is a general-purpose solver.
    /// </summary>
    public class DefaultHighGuidResolver : IHighGuidResolver<ObjectGuid64>, IHighGuidResolver<ObjectGuid128>
    {
        public bool CanResolve(IObjectGUID objectGuid, ClientBuild clientBuild)
            => true;

        public ObjectGuidType Resolve(ObjectGuid128 guid, ClientBuild clientBuild)
        {
            return ObjectGuidType.Null;
        }

        public ObjectGuidType Resolve(ObjectGuid64 guid, ClientBuild clientBuild)
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
                0xF02 => ObjectGuidType.Transport,
                0xF03 => ObjectGuidType.Creature,
                0xF04 => ObjectGuidType.Pet,
                0xF05 => ObjectGuidType.Vehicle,
                _     => ObjectGuidType.Unknown
            };
        }

        public ObjectGuidType Resolve(IObjectGUID guid, ClientBuild clientBuild)
        {
            return guid switch
            {
                ObjectGuid64 legacyObjectGuid => Resolve(legacyObjectGuid, clientBuild),
                ObjectGuid128 objectGuid      => Resolve(objectGuid, clientBuild),
                _                             => ObjectGuidType.Unknown
            };
        }
    }
}