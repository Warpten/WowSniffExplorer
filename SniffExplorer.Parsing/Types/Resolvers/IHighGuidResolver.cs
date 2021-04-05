using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Parsing.Versions;

namespace SniffExplorer.Parsing.Types.Resolvers
{
    public interface IHighGuidResolver
    {
        public bool CanResolve(IObjectGUID objectGuid, ClientBuild clientBuild);
        public ObjectGuidType Resolve(IObjectGUID guid, ClientBuild clientBuild);
    }

    public interface IHighGuidResolver<in T> : IHighGuidResolver where T : IObjectGUID
    {
        public ObjectGuidType Resolve(T guid, ClientBuild clientBuild);
    }
}
