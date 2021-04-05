using System;
using System.Collections.Generic;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Parsing.Types.Resolvers;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Parsing.Helpers.GUIDs
{
    public class GuidResolver
    {
        private readonly List<IHighGuidResolver> _guidResolvers = new()
        {
            new DefaultHighGuidResolver()
        };

        public ParsingContext Context { get; }
        public ClientBuild ClientBuild => Context.ClientBuild;

        public GuidResolver(ParsingContext context, Type[] assemblyTypes)
        {
            Context = context;

            foreach (var resolverType in assemblyTypes)
            {
                if (!typeof(IHighGuidResolver).IsAssignableFrom(resolverType))
                    continue;

                var guidResolver = Activator.CreateInstance(resolverType);
                _guidResolvers.Add((guidResolver as IHighGuidResolver)!);
            }
        }

        public ObjectGuidType ResolveObjectGuidType(IObjectGUID objectGuid, ParsingContext context)
        {
            foreach (var resolver in _guidResolvers)
            {
                if (!resolver.CanResolve(objectGuid, context.ClientBuild))
                    continue;

                var resolvedType = resolver.Resolve(objectGuid, context.ClientBuild);
                if (resolvedType != ObjectGuidType.Unknown)
                    return resolvedType;
            }

            return ObjectGuidType.Unknown;
        }

        public IObjectGUID CreateGUID()
        {
            // Somewhere during WoD, 128-bits GUIDs were introduced.
            if (ClientBuild > ClientBuild.V6_2_4_21315 && ClientBuild.ExpansionType == RealmExpansionType.Retail && ClientBuild.Expansion >= Expansion.WarlordsOfDraenor)
                return new ObjectGuid128(0uL, 0uL, Context);

            return new ObjectGuid64(0uL, Context);
        }
    }
}
